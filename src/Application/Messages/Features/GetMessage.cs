using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Messages.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Messages.Features;

public static class GetMessage
{
    public record MessageQuery(Guid Id) : IRequest<ChannelMessageDto>;

    public class QueryHandler : IRequestHandler<MessageQuery, ChannelMessageDto>
    {
        private readonly IDoveDbContext _context;
        private readonly IMapper _mapper;

        public QueryHandler(IDoveDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ChannelMessageDto> Handle(MessageQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.ChannelMessages
                .ProjectTo<ChannelMessageDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (result is null)
                throw new NotFoundException("Message", request.Id);

            return result;
        }
    }
}