// using CfpService.Application.Services.Application;
// using CfpService.Contracts.Dtos.Application;
// using CfpService.Controllers;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
//
// namespace tests.UnitTests.Controllers;
//
// public class ApplicationsControllerTests
// {
//     private readonly Mock<IApplicationService> _applicationServiceMock;
//     private readonly ApplicationsController _controller;
//
//     public ApplicationsControllerTests()
//     {
//         _applicationServiceMock = new Mock<IApplicationService>();
//         _controller = new ApplicationsController(_applicationServiceMock.Object);
//     }
//
//     [Fact]
//     public void GetById_ExistingId_ReturnsOkResultWithApplicationDto()
//     {
//         // Arrange
//         var applicationId = Guid.NewGuid();
//         var expectedApplication = new GetApplicationDto
//         (
//             applicationId,
//             Guid.NewGuid(),
//             null,
//             null,
//             null,
//             null
//         );
//         
//         _applicationServiceMock.Setup(service => service.ExistByApplicationId(applicationId)).Returns(true);
//         _applicationServiceMock.Setup(service => service.GetApplicationById(applicationId)).Returns(expectedApplication);
//
//         // Act
//         var result = _controller.GetById(applicationId);
//
//         // Assert
//         var okResult = Assert.IsType<OkObjectResult>(result.Result);
//         var returnedApplication = Assert.IsType<GetApplicationDto>(okResult.Value);
//         Assert.Equal(expectedApplication.Id, returnedApplication.Id);
//     }
//
//     [Fact]
//     public void GetById_NonExistingId_ReturnsNotFound()
//     {
//         // Arrange
//         var applicationId = Guid.NewGuid();
//         _applicationServiceMock.Setup(service => service.ExistByApplicationId(applicationId)).Returns(false);
//
//         // Act
//         var result = _controller.GetById(applicationId);
//
//         // Assert
//         Assert.IsType<NotFoundResult>(result.Result);
//     }
//
//     [Fact]
//     public void PostApplication_UserHasUnsubmittedApplication_ReturnsBadRequest()
//     {
//         // Arrange
//         var dto = new PostApplicationDto
//         (
//             Guid.NewGuid(),
//             null,
//             null,
//             null,
//             null
//         );
//         _applicationServiceMock.Setup(service => service.ExistUnsubmittedFromUser(dto.Author)).Returns(true);
//
//         // Act
//         var result = _controller.PostApplication(dto);
//
//         // Assert
//         Assert.IsType<BadRequestObjectResult>(result.Result);
//     }
//     
//     [Fact]
//     public void Delete_NonExistingApplication_ReturnsNotFound()
//     {
//         // Arrange
//         var applicationId = Guid.NewGuid();
//         _applicationServiceMock.Setup(service => service.ExistByApplicationId(applicationId)).Returns(false);
//
//         // Act
//         var result = _controller.Delete(applicationId);
//
//         // Assert
//         Assert.IsType<NotFoundResult>(result);
//     }
// }