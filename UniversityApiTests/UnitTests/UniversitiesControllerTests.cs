using Microsoft.AspNetCore.Mvc;
using Moq;
using UniversityApi.ApiModel;
using UniversityApi.Controllers;
using UniversityApiTests.FakeData;
using UniversityDataLibrary;

namespace UniversityApiTests.UnitTests
{
    public class UniversitiesControllerTests
    {
        [Fact]
        public void Index_ReturnsAListOfUniversity()
        {
            //arrange

            var mockRepo = new Mock<UniversityDataDbContext>();
            mockRepo.Setup(x => x.Universities.ToList()).Returns(UniversityTestData.GetUniversityList);
            var controller = new UniversitiesController(mockRepo.Object);

            //act
            var result = controller.GetUniversities();
            //assert

            var actionResult = Assert.IsAssignableFrom<IActionResult>(result);
            var model = Assert.IsAssignableFrom<List<UniversityApiModel>>(actionResult);
            Assert.Single(model);

        }
    }
}
