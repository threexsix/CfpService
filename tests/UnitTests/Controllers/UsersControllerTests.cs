using CfpService.Application.Services.Application;
using CfpService.Contracts.Dtos.Application;
using CfpService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace tests.UnitTests.Controllers;

public class UserControllerTests
{
    private readonly Mock<IApplicationService> _applicationServiceMock;
    private readonly UsersController _controller;

    public UserControllerTests()
    {
        _applicationServiceMock = new Mock<IApplicationService>();
        _controller = new UsersController(_applicationServiceMock.Object);
    }

    [Fact]
    public void GetCurrentApplication_ExistingUnsubmittedApplication_ReturnsOkWithApplication()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedApplication = new GetApplicationDto(
            Guid.NewGuid(),
            userId,
            "report",
            "name",
            null,
            "outline"
        );

        _applicationServiceMock.Setup(x => x.GetUserUnSubmittedApplication(userId)).Returns(expectedApplication);

        // Act
        var result = _controller.GetCurrentApplication(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var applicationDto = Assert.IsType<GetApplicationDto>(okResult.Value);
        Assert.Equal(expectedApplication, applicationDto);
    }

    [Fact]
    public void GetCurrentApplication_NoUnsubmittedApplication_ReturnsNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _applicationServiceMock.Setup(x => x.GetUserUnSubmittedApplication(userId)).Returns((GetApplicationDto)null);

        // Act
        var result = _controller.GetCurrentApplication(userId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}