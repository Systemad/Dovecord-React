using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dovecord.Databases;
using Dovecord.Domain.Messages.Dto;
using Dovecord.Exceptions;
using Dovecord.Extensions.Services;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dovecord.Domain.Messages.Features;

public static class AddMessage
{
    public record AddMessageCommand(MessageManipulationDto MessageToAdd) : IRequest<ChannelMessageDto>;

    public class Handler : IRequestHandler<AddMessageCommand, ChannelMessageDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        
        public Handler(DoveDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        
        /*
         *     public string? Content { get; set; }
    public Guid ChannelId { get; set; }
         */
        
        /*
         *     public Guid Id { get; set; }
    public string? Content { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsEdit { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    
    public Guid ChannelId { get; set; }
    public Channel Channel { get; set; }
        
    public Guid? ServerId { get; set; }

    public Guid? AuthorId { get; set; }
         */
        public async Task<ChannelMessageDto> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            var message = _mapper.Map<ChannelMessage>(request.MessageToAdd);
            message.AuthorId = Guid.Parse(_currentUserService.UserId);
            message.CreatedBy = _currentUserService.Username;
            message.CreatedOn = DateTime.Now;

            var channel = await _context.Channels
                //.Where(x => x.Id == request.MessageToAdd.ChannelId)
                .Include(m => m.Messages)
                .AsTracking()
                .FirstAsync(cancellationToken);
            //.SingleOrDefaultAsync(cancellationToken);

            if (channel is null)
                throw new NotFoundException("Channel", channel.Id);
            
            channel.Messages.Add(message);
            
            //if(channel.Type == 0)
            await _context.SaveChangesAsync(cancellationToken);

            return await _context.ChannelMessages
                .ProjectTo<ChannelMessageDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == message.Id, cancellationToken);
        }
    }
}