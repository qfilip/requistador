using MapsterMapper;
using Requistador.DataAccess.Contexts;
using Requistador.Domain.Entities;
using Requistador.Logic.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Requistador.Logic.Commands.Request
{
    public class AddRequestCommand : BaseCommand<Guid>
    {
        private readonly AppRequest Request;
        public AddRequestCommand(AppRequest request)
        {
            Request = request;
        }

        
        public class Handler : BaseHandler<AddRequestCommand, Guid>
        {
            public Handler(RequestDbContext requestDbContext, IMapper mapper): base(requestDbContext, mapper) {}

            public override async Task<Guid> Handle(AddRequestCommand command, CancellationToken cancellationToken)
            {
                var result = await Task.FromResult(_requestDbContext.Insert(command.Request));
                return result;
            }
        }
    }
}
