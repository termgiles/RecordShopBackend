using RecordShopBackend;
using Moq;
using FluentAssertions;
using RecordShopBackend.Service;
using RecordShopBackend.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RecordShopBackendTests
{
    public class ControllerTests
    {
        private Mock<IRecordShopService> _mockService;
        private RecordShopController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IRecordShopService>();
            _controller = new RecordShopController(_mockService.Object);
        }

        [Test]
        public void BaseRequest_ReturnsOkWithWelcomeMessage()
        {
            // Arragne
            _mockService.Setup(r => r.ReturnWelcomeMessage()).Returns("test welcome message");

            // Act
            var result = _controller.Welcome();

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.As<OkObjectResult>().Value.Should().Be("test welcome message");
        }

        [Test]
        public void GetAllAlbums_QueriesServiceLAyer_Once()
        {
            // Act
            _controller.GetAllAlbums();

            //Assert
            _mockService.Verify(r => r.ReturnAllAlbums(), Times.Once());

        }

        [Test]
        public void GetAllAlbums_ReturnsOkWithAlbums()
        {
            // Arrange
            var testAlbums = new List<Album>
            {
                new Album { Id = 1, Artist = "L'Ed Sheeran", Genre = "La Pop", Information = "un album du chanteur Ed Sheeran nommé d'après un symbiole mathématique", Name = "L'addition", Released = 2011},
                new Album{ Id = 2, Artist = "L'Ed Sheeran", Genre = "La Pop", Information = "un album du chanteur Ed Sheeran nommé d'après un symbiole mathématique", Name = "La multiplication", Released = 2014},
            };
            _mockService.Setup(r => r.ReturnAllAlbums()).Returns(testAlbums);

            // Act
            var result = _controller.GetAllAlbums();

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.As<OkObjectResult>().Value.Should().Be(testAlbums);
        }
    }
}
