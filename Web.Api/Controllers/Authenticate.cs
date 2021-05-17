using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Web.Api.Authentication;
using Infrastructure.Identity;
using Infrastructure.Identity.Interfaces;
using System.Threading.Tasks;
using Web.Api.Authentication;

namespace Web.Api.Controllers
{
    [Route("api/auth")]
    public class Authenticate : Controller
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        protected readonly IMapper Mapper;
        //private readonly JwtTokenConfig _tokenConfig;

        //public Authenticate(UserManager<ApplicationUser> userManager, JwtTokenConfig jwtTokenConfig)
        //{
        //_userManager = userManager;
        //    //_tokenConfig = jwtTokenConfig;

        //}
        public Authenticate(IJwtFactory jwtFactory, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _jwtFactory = jwtFactory;
            _userManager = userManager;
            Mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                return Ok(await _jwtFactory.GetTokenAsync(model.UserName, model.Password));
            }
            catch (System.Exception ex)
            {

                throw ex;
            }


        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var user = Mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            return Ok(result);
        }

    }
}

