using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Together.Activity.Application.Commands
{
    public class JoinActivityCommandHandler : IRequestHandler<JoinActivityCommand, bool>
    {


        public async Task<bool> Handle(JoinActivityCommand request, CancellationToken cancellationToken)
        {
            return true;
        }
    }
}
