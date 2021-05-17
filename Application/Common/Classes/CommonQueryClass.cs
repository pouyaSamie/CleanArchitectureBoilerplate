using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Application.Common.Classes
{
    public class CommonQueryClass:CommonCRUD
    {
        #region Properies

        #endregion

        #region Constructors
        public CommonQueryClass(IApplicationDbContext context, IMediator mediator, IStringLocalizer<Result> localizer, IMapper mapper)
            : base(context, mediator, localizer, mapper)
        { }
        #endregion
    }
}
