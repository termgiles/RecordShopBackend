using RecordShopBackend;
using Moq;
using FluentAssertions;
using RecordShopBackend.Service;
using RecordShopBackend.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RecordShopBackendTests
{
    public class ControllerTests
    {
        private Mock<IRecordShopService> _mockService;
        private RecordShopController _controller;

        List<Album> testAlbums = new List<Album>
            {
                new Album { Id = 1, Artist = "L'Ed Sheeran", Genre = "La Pop", Information = "un album du chanteur Ed Sheeran nomm� d'apr�s un symbiole math�matique", Name = "L'addition", Released = 2011},
                new Album{ Id = 2, Artist = "L'Ed Sheeran", Genre = "La Pop", Information = "un album du chanteur Ed Sheeran nomm� d'apr�s un symbiole math�matique", Name = "La multiplication", Released = 2014},
            };

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
            _mockService.Setup(r => r.ReturnAllAlbums()).Returns(testAlbums);

            // Act
            var result = _controller.GetAllAlbums();

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.As<OkObjectResult>().Value.Should().Be(testAlbums);
        }

        [Test]
        public void GetAlbumById_ReturnsOkWithFoundAlbums()
        {
            // Arrange
            var expected = new AlbumReturn { Found = true, ReturnedObject = testAlbums[0] };
            _mockService.Setup(r => r.ReturnAlbumById(1)).Returns(expected);

            // Act
            var result = _controller.GetAlbumById(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expected.ReturnedObject);
        }

        [Test]
        public void GetAlbumById_QueriesServiceLayerOnce()
        {
            // Arrange
            var expected = new AlbumReturn { Found = true, ReturnedObject = testAlbums[0] };
            _mockService.Setup(r => r.ReturnAlbumById(1)).Returns(expected);

            // Act
            var result = _controller.GetAlbumById(1);

            // Assert
            _mockService.Verify(r => r.ReturnAlbumById(1), Times.Once());

        }

        [Test]
        public void GetAlbumById_Returns404IfNoAlbum()
        {
            // Arrange
            var expected = new AlbumReturn { Found = false, ReturnedObject = null };
            _mockService.Setup(r => r.ReturnAlbumById(70)).Returns(expected);

            // Act
            var result = _controller.GetAlbumById(70);

            // Assert
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);;
        }

        [Test]
        public void PutAlbumById_QueriesServiceLayerOnce()
        {
            // Arrange
            var expected = new AlbumReturn { Found = true, ReturnedObject = testAlbums[0] };
            AlbumModification mod = new AlbumModification();
            _mockService.Setup(r => r.AmmendAlbumById(1, mod)).Returns(expected);

            // Act
            var result = _controller.PutAlbumById(1, mod);

            // Assert
            _mockService.Verify(r => r.AmmendAlbumById(1, mod), Times.Once());

        }

        [Test]
        public void PutAlbumById_Returns404IfNoAlbum()
        {
            // Arrange
            var expected = new AlbumReturn { Found = false, ReturnedObject = null };
            AlbumModification mod = new AlbumModification();
            _mockService.Setup(r => r.AmmendAlbumById(70, mod)).Returns(expected);


            // Act
            var result = _controller.PutAlbumById(70, mod);

            // Assert
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Test]
        public void DeleteAlbumById_QueriesServiceLayerOnce()
        {
            // Arrange
            var expected = true;
            _mockService.Setup(r => r.RemoveAlbumById(1)).Returns(expected);

            // Act
            var result = _controller.DeleteAlbumById(1);

            //Assert
            _mockService.Verify(r => r.RemoveAlbumById(1), Times.Once());

        }

        [Test]
        public void DeleteAlbumById_Returns204IfFoundIsTrue()
        {
            // Arrange
            var expected = true;
            _mockService.Setup(r => r.RemoveAlbumById(1)).Returns(expected);

            // Act
            var result = _controller.DeleteAlbumById(1);

            //Assert
            result.Should().BeOfType<NoContentResult>()
               .Which.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }

        [Test]
        public void DeleteAlbumById_Returns404IfFoundIsFalse()
        {
            // Arrange
            var expected = false;
            _mockService.Setup(r => r.RemoveAlbumById(70)).Returns(expected);

            // Act
            var result = _controller.DeleteAlbumById(70);

            //Assert
            result.Should().BeOfType<NotFoundResult>()
               .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Test]
        public void PostAlbum_FalseNotNull_Returns201()
        {
            // Arrange
            var originalWork = new Album { Artist = "Lady Gaga", Genre = "La Pop", Information = "un album de la chanteuse Lady Gaga", Released = 2008, Name = "nouveau album" };
            AlbumReturn returned = new AlbumReturn { Found = false, ReturnedObject = originalWork };
            _mockService.Setup(p => p.AddAlbum(originalWork)).Returns(returned);

            // Act
            var result = _controller.PostAlbum(originalWork);

            //
            result.Should().BeOfType<CreatedResult>();
          

        }

        [Test]
        public void PostAlbum_TrueNotNull_Returns400()
        {
            // Arrange
            var originalWork = new Album { Artist = "Lady Gaga", Genre = "La Pop", Information = "un album de la chanteuse Lady Gaga", Released = 2008, Name = "nouveau album" };
            AlbumReturn returned = new AlbumReturn { Found = true, ReturnedObject = originalWork };
            _mockService.Setup(p => p.AddAlbum(originalWork)).Returns(returned);

            // Act
            var result = _controller.PostAlbum(originalWork);

            //
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);


        }

        [Test]
        public void PostAlbum_TrueAndNull_Returns400()
        {
            // Arrange
            var originalWork = new Album { Genre = "La Pop", Information = "un album de la chanteuse Lady Gaga", Released = 2008, Name = "nouveau album" };
            AlbumReturn returned = new AlbumReturn { Found = true, ReturnedObject = null };
            _mockService.Setup(p => p.AddAlbum(originalWork)).Returns(returned);

            // Act
            var result = _controller.PostAlbum(originalWork);

            //
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);


        }

        [Test]
        public void PostAlbum_QueriesServiceLayerOnce()
        {
            // Arrange
            var originalWork = new Album { Genre = "La Pop", Information = "un album de la chanteuse Lady Gaga", Released = 2008, Name = "nouveau album" };
            AlbumReturn returned = new AlbumReturn { Found = true, ReturnedObject = null };
            _mockService.Setup(p => p.AddAlbum(originalWork)).Returns(returned);

            // Act
            var result = _controller.PostAlbum(originalWork);

            //Assert
            _mockService.Verify(r => r.AddAlbum(originalWork), Times.Once());

        }
    }
}
