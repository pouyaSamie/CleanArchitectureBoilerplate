using AutoMapper;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Persistence;
using System;

namespace Application.UnitTest.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        protected IStringLocalizer<Result> _localizer;
        protected ICurrentUserService _currentUser;
        protected IMapper _mapper;

        public CommandTestBase()
        {
            _currentUser = MockIUserService();
            _context = ContextFactory.Create(_currentUser);


            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            MockIStringSerialize();

        }
        private void MockIStringSerialize()
        {
            var options = Options.Create(new LocalizationOptions());
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            _localizer = new StringLocalizer<Result>(factory);


        }

        private ICurrentUserService MockIUserService()
        {
            var mock = new Mock<ICurrentUserService>();
            mock.Setup(x => x.UserId).Returns(1);
            mock.Setup(x => x.IsAuthenticated).Returns(true);
            return mock.Object;
        }
        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);

        }
        ~CommandTestBase()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ContextFactory.Destroy(_context);
            }
            // free native resources if there are any.

        }
    }
}
