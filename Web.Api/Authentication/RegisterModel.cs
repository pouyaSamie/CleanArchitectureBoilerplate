using AutoMapper;
using Application.Common.Mappings;
using Infrastructure.Identity;
using System;

namespace Web.Api.Authentication
{
    public class RegisterModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public DateTime BirthDay { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterModel, ApplicationUser>()
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.EmailAddress));

        }
    }

}
