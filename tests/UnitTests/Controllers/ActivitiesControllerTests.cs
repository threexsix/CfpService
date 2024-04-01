using CfpService.Controllers;
using CfpService.Dtos.Activity;
using CfpService.Services.Activity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace tests.UnitTests.Controllers;

public class ActivitiesControllerTests
{
    private readonly Mock<IActivityService> _activitiesServiceMock;
    private readonly ActivitiesController _controller;

    public ActivitiesControllerTests()
    {
        _activitiesServiceMock = new Mock<IActivityService>();
        _controller = new ActivitiesController(_activitiesServiceMock.Object);
    }

    [Fact]
    public void GetAllActivities_ReturnsOkResultWithActivitiesList()
    {
        // Arrange
        var expectedActivities = new List<GetActivityDto>
        {
            new GetActivityDto("Report", "Доклад, 35-45 минут"),
            new GetActivityDto("Masterclass", "Мастеркласс, 1-2 часа"),
            new GetActivityDto("Discussion", "Дискуссия / круглый стол, 40-50 минут")
        };

        _activitiesServiceMock.Setup(service => service.GetAllActivities())
            .Returns(expectedActivities);

        // Act
        var result = _controller.GetAllActivities();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var activities = Assert.IsType<List<GetActivityDto>>(okResult.Value);
        Assert.Equal(3, activities.Count); // Проверяем, что в списке действительно 3 элемента
        Assert.Equal("Report", activities[0].Name);
        Assert.Equal("Доклад, 35-45 минут", activities[0].Description);
        Assert.Equal("Masterclass", activities[1].Name);
        Assert.Equal("Мастеркласс, 1-2 часа", activities[1].Description);
        Assert.Equal("Discussion", activities[2].Name);
        Assert.Equal("Дискуссия / круглый стол, 40-50 минут", activities[2].Description);
    }
}