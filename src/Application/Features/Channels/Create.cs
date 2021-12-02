using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Channels;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Dtos.Channel;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Channels;

public class Create
{
    public record CreateChannelCommand(ChannelManipulationDto ChannelToAdd) : IRequest<ChannelDto>;

    public class QueryHandler : IRequestHandler<CreateChannelCommand, ChannelDto>
    {
        private readonly DoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(DoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<ChannelDto> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = _mapper.Map<Channel>(request.ChannelToAdd);
            _context.Channels.Add(channel);
            await _context.SaveChangesAsync(cancellationToken);

            // TODO: Setup automapper
            return await _context.Channels
                .ProjectTo<ChannelDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.ChannelId == channel.Id);

        }
    }
}