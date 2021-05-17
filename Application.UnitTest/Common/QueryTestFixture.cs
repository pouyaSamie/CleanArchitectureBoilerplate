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
using Xunit;

namespace Application.UnitTest.Common
{
    public class QueryTestFixture : IDisposable
    {
        public readonly ApplicationDbContext Context;
        public IMapper Mapper { get; private set; }
        public IStringLocalizer<Result> Localizer;
        protected ICurrentUserService _currentUser;
        public QueryTestFixture()
        {
            Context = ContextFactory.Create(_currentUser);

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
            MockIStringSerialize();
        }

        private void MockIStringSerialize()
        {
            var options = Options.Create(new LocalizationOptions());
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            Localizer = new StringLocalizer<Result>(factory);


        }

        private void MockIUserService()
        {
            var mock = new Mock<ICurrentUserService>();
            mock.Setup(x => x.UserId).Returns(1);
            mock.Setup(x => x.IsAuthenticated).Returns(true);
            _currentUser = mock.Object;
        }

        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);

        }
        ~QueryTestFixture()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ContextFactory.Destroy(Context);
            }
            // free native resources if there are any.

        }

    }
    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}

