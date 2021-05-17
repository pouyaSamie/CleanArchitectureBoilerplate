using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Application.Common.Interfaces;
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Classes
{
    public class CommonCRUD
    {

        protected readonly IApplicationDbContext DbContext;
        protected readonly IMediator Madiator;
        protected readonly IStringLocalizer<Result> Localizer;
        protected readonly IMapper Mapper;


        public CommonCRUD(IApplicationDbContext context, IMediator madiator, IStringLocalizer<Result> localizer, IMapper mapper)
        {
            DbContext = context;
            Madiator = madiator;
            Localizer = localizer;
            Mapper = mapper;
        }

    }
}
