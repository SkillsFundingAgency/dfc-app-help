using DFC.App.Help.Data;
using DFC.App.Help.Data.Contracts;
using FakeItEasy;
using System;
using System.Linq.Expressions;
using System.Net;
using Xunit;

namespace DFC.App.Help.PageService.UnitTests.HelpPageServiceTests
{
    [Trait("Category", "Page Service Unit Tests")]
    public class HelpPageServiceUpdateTests
    {
        [Fact]
        public void HelpPageServiceUpdateReturnsSuccessWhenHelpPageReplaced()
        {
            // arrange
            var repository = A.Fake<ICosmosRepository<HelpPageModel>>();
            var helpPageModel = A.Fake<HelpPageModel>();
            var expectedResult = A.Fake<HelpPageModel>();

            A.CallTo(() => repository.UpdateAsync(helpPageModel.DocumentId, helpPageModel)).Returns(HttpStatusCode.OK);
            A.CallTo(() => repository.GetAsync(A<Expression<Func<HelpPageModel, bool>>>.Ignored)).Returns(expectedResult);

            var helpPageService = new HelpPageService(repository, A.Fake<IDraftHelpPageService>());

            // act
            var result = helpPageService.ReplaceAsync(helpPageModel).Result;

            // assert
            A.CallTo(() => repository.UpdateAsync(helpPageModel.DocumentId, helpPageModel)).MustHaveHappenedOnceExactly();
            A.CallTo(() => repository.GetAsync(A<Expression<Func<HelpPageModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            A.Equals(result, expectedResult);
        }

        [Fact]
        public async System.Threading.Tasks.Task HelpPageServiceUpdateReturnsArgumentNullExceptionWhenNullIsUsed()
        {
            // arrange
            var repository = A.Fake<ICosmosRepository<HelpPageModel>>();
            var helpPageService = new HelpPageService(repository, A.Fake<IDraftHelpPageService>());

            // act
            var exceptionResult = await Assert.ThrowsAsync<ArgumentNullException>(async () => await helpPageService.ReplaceAsync(null).ConfigureAwait(false)).ConfigureAwait(false);

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: helpPageModel", exceptionResult.Message);
        }

        [Fact]
        public void HelpPageServiceUpdateReturnsNullWhenHelpPageNotReplaced()
        {
            // arrange
            var repository = A.Fake<ICosmosRepository<HelpPageModel>>();
            var helpPageModel = A.Fake<HelpPageModel>();
            var expectedResult = A.Dummy<HelpPageModel>();

            A.CallTo(() => repository.UpdateAsync(helpPageModel.DocumentId, helpPageModel)).Returns(HttpStatusCode.BadRequest);

            var helpPageService = new HelpPageService(repository, A.Fake<IDraftHelpPageService>());

            // act
            var result = helpPageService.ReplaceAsync(helpPageModel).Result;

            // assert
            A.CallTo(() => repository.UpdateAsync(helpPageModel.DocumentId, helpPageModel)).MustHaveHappenedOnceExactly();
            A.CallTo(() => repository.GetAsync(A<Expression<Func<HelpPageModel, bool>>>.Ignored)).MustNotHaveHappened();
            A.Equals(result, expectedResult);
        }

        [Fact]
        public void HelpPageServiceUpdateReturnsNullWhenMissingRepository()
        {
            // arrange
            var repository = A.Dummy<ICosmosRepository<HelpPageModel>>();
            var helpPageModel = A.Fake<HelpPageModel>();
            HelpPageModel expectedResult = null;

            A.CallTo(() => repository.UpdateAsync(helpPageModel.DocumentId, helpPageModel)).Returns(HttpStatusCode.FailedDependency);

            var helpPageService = new HelpPageService(repository, A.Fake<IDraftHelpPageService>());

            // act
            var result = helpPageService.ReplaceAsync(helpPageModel).Result;

            // assert
            A.CallTo(() => repository.UpdateAsync(helpPageModel.DocumentId, helpPageModel)).MustHaveHappenedOnceExactly();
            A.CallTo(() => repository.GetAsync(A<Expression<Func<HelpPageModel, bool>>>.Ignored)).MustNotHaveHappened();
            A.Equals(result, expectedResult);
        }
    }
}
