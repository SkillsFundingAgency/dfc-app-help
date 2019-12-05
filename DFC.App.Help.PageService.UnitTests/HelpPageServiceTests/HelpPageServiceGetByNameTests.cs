using DFC.App.Help.Data;
using DFC.App.Help.Data.Contracts;
using FakeItEasy;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.Help.PageService.UnitTests.HelpPageServiceTests
{
    [Trait("Category", "Page Service Unit Tests")]
    public class HelpPageServiceGetByNameTests
    {
        private const string CanonicalName = "name1";
        private readonly ICosmosRepository<HelpPageModel> repository;
        private readonly IHelpPageService helpPageService;

        public HelpPageServiceGetByNameTests()
        {
            repository = A.Fake<ICosmosRepository<HelpPageModel>>();
            helpPageService = new HelpPageService(repository);
        }

        [Fact]
        public async Task HelpPageServiceGetByNameReturnsSuccess()
        {
            // arrange
            var expectedResult = A.Fake<HelpPageModel>();
            A.CallTo(() => repository.GetAsync(A<Expression<Func<HelpPageModel, bool>>>.Ignored)).Returns(expectedResult);

            // act
            var result = await helpPageService.GetByNameAsync(CanonicalName).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<HelpPageModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public async Task HelpPageServiceGetByNameReturnsArgumentNullExceptionWhenNullIsUsed()
        {
            // arrange

            // act
            var exceptionResult = await Assert.ThrowsAsync<ArgumentNullException>(async () => await helpPageService.GetByNameAsync(null).ConfigureAwait(false)).ConfigureAwait(false);

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: canonicalName", exceptionResult.Message);
        }

        [Fact]
        public async Task HelpPageServiceGetByNameReturnsNullWhenMissingRepository()
        {
            // arrange
            A.CallTo(() => repository.GetAsync(A<Expression<Func<HelpPageModel, bool>>>.Ignored)).Returns((HelpPageModel)null);

            // act
            var result = await helpPageService.GetByNameAsync(CanonicalName).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<HelpPageModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Null(result);
        }
    }
}