﻿using System;
using System.Threading.Tasks;
using DFC.App.Help.Models.Cosmos;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DFC.App.Help.IntegrationTests.ServiceTests.HelpPageServiceTests
{
    [TestFixture]
    public class HelpPageServiceGetByNameTests : BaseHelpPageServiceTests
    {
        [Test]
        [Category("HelpPageService.GetByName")]
        public async Task HelpPageService_GetByName_ReturnsSuccess_WhenHelpPageExists()
        {
            // arrange
            var name = ValidNameValue + "_GetByName".ToLower();
            var helpPageModel = new HelpPageModel()
            {
                CanonicalName = name + "_" + Guid.NewGuid().ToString(),
                DocumentId = Guid.NewGuid()
            };
            var helpPageService = _serviceProvider.GetService<Services.IHelpPageService>();

            await helpPageService.CreateAsync(helpPageModel);

            // act
            var result = await helpPageService.GetByNameAsync(helpPageModel.CanonicalName);

            // assert
            result.DocumentId.Should().Be(helpPageModel.DocumentId);
            result.CanonicalName.Should().Be(helpPageModel.CanonicalName);
        }

        [Test]
        [Category("HelpPageService.GetByName")]
        public async Task HelpPageService_GetByName_ReturnsNull_WhenHelpPageDoesNotExist()
        {
            // arrange
            var helpPageService = _serviceProvider.GetService<Services.IHelpPageService>();

            // act
            var result = await helpPageService.GetByNameAsync(Guid.NewGuid().ToString());

            // assert
            result.Should().BeNull();
        }
    }
}
