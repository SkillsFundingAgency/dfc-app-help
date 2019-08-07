using DFC.App.Help.Data;
using DFC.App.Help.Data.Contracts;
using FakeItEasy;
using System.Collections.Generic;
using Xunit;

namespace DFC.App.Help.PageService.UnitTests.HelpPageServiceTests
{
    [Trait("Category", "Page Service Unit Tests")]
    public class HelpPageServiceGetAllTests
    {
        [Fact]
        public void HelpPageServiceGetAllListReturnsSuccess()
        {
            // arrange
            var repository = A.Fake<ICosmosRepository<HelpPageModel>>();
            var expectedResults = A.CollectionOfFake<HelpPageModel>(2);

            A.CallTo(() => repository.GetAllAsync()).Returns(expectedResults);

            var helpPageService = new HelpPageService(repository, A.Fake<IDraftHelpPageService>());

            // act
            var results = helpPageService.GetAllAsync().Result;

            // assert
            A.CallTo(() => repository.GetAllAsync()).MustHaveHappenedOnceExactly();
            A.Equals(results, expectedResults);
        }

        [Fact]
        public void HelpPageServiceGetAllListReturnsNullWhenMissingRepository()
        {
            // arrange
            var repository = A.Dummy<ICosmosRepository<HelpPageModel>>();
            IEnumerable<HelpPageModel> expectedResults = null;

            A.CallTo(() => repository.GetAllAsync()).Returns(expectedResults);

            var helpPageService = new HelpPageService(repository, A.Fake<IDraftHelpPageService>());

            // act
            var results = helpPageService.GetAllAsync().Result;

            // assert
            A.CallTo(() => repository.GetAllAsync()).MustHaveHappenedOnceExactly();
            A.Equals(results, expectedResults);
        }
    }
}
