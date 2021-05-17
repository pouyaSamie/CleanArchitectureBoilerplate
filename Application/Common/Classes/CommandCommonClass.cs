using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Application.Common.Classes
{
    public class CommandCommonClass: CommonCRUD
    {

        public CommandCommonClass(IApplicationDbContext context, IMediator mediator, IStringLocalizer<Result> localizer, IMapper mapper)
            :base(context,mediator,localizer,mapper)
        {}

    }
}
