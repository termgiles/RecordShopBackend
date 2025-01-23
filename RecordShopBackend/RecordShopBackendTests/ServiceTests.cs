using RecordShopBackend;
using Moq;
using FluentAssertions;
using RecordShopBackend.Service;
using RecordShopBackend.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using RecordShopBackend.Repository;
namespace RecordShopBackendTests
{
    public class ServiceTests 
    {
        private Mock<IRecordShopRepository> _mockRepository;
        private RecordShopService _service;

        private List<Album> testAlbums = new List<Album> { (new Album { Id = 1, Artist = "Lady Gaga", Genre = "La Pop", Information = "un album de la chanteuse Lady Gaga", Name = "La renommee", Released = 2008 }),
        new Album { Id = 2, Artist = "Lady Gaga", Genre = "La Pop", Information = "un autre album de la chanteuse Lady Gaga", Name = "nee de cette facon", Released = 2011 } };

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IRecordShopRepository>();
            _service = new RecordShopService(_mockRepository.Object);
        }
        #region WelcomeMessage
        [Test]
        public void ReturnWelcomeMessage_ReturnsCorrectString()
        {
            // Arrange
            string expected = "Welcome to the Record Shop Database";

            // Act
            var output = _service.ReturnWelcomeMessage();

            //Assert

            output.Should().Be(expected);
        }
        #endregion

        #region ReturnAll
        [Test]
        public void ReturnAllAlbums_ReturnsAlbumListWithOneCall()
        {
            // Arrange
            var expected = testAlbums;
            _mockRepository.Setup(r => r.RetrieveAllAlbums()).Returns(testAlbums);

            //Act
            var output = _service.ReturnAllAlbums();

            //Assert
            output.Should().BeEquivalentTo(expected);
            _mockRepository.Verify(r => r.RetrieveAllAlbums(), Times.Once);
        }
        #endregion

        #region ReturnAlbumById
        [Test]
        public void ReturnAlbumById_ReturnsAlbumReturnWithOneCall()
        {
            // Arrange
            var expected = new AlbumReturn {Found = true, ReturnedObject = testAlbums[0] };
            _mockRepository.Setup(r => r.RetrieveAlbumById(1)).Returns(expected);

            //Act
            var output = _service.ReturnAlbumById(1);

            //Assert
            output.Should().BeEquivalentTo(expected);
            _mockRepository.Verify(r => r.RetrieveAlbumById(1), Times.Once);
        }
        #endregion

        #region AmmendAlbumById
        [Test]
        public void AmmendAlbumById_CallsRepositoryOnce()
        {
            // Arrange
            var expected = new AlbumReturn { Found = true, ReturnedObject = testAlbums[0] };
            AlbumModification mod = new AlbumModification();
            _mockRepository.Setup(r => r.UpdateAlbumById(1, mod)).Returns(expected);

            //Act
            var output = _service.AmmendAlbumById(1, mod);

            //Assert
            _mockRepository.Verify(r => r.UpdateAlbumById(1, mod), Times.Once);
        }
        #endregion

        #region RemoveAlbumById
        [Test]
        public void RemoveAlbumById_CallsRepositoryOnce()
        {
            // Arrange
            var expected = true;
            _mockRepository.Setup(r => r.DeleteAlbumById(1)).Returns(expected);

            //Act
            var output = _service.RemoveAlbumById(1);
         
            //Assert
            _mockRepository.Verify(r => r.DeleteAlbumById(1), Times.Once);
        }
        #endregion

        #region Add+ValidateAlbum
        [Test]
        public void AddAlbum_RejectsAlbumWithNoName()
        {
            // Arrange
            var originalWork = new AlbumModification { Artist = "Lady Gaga", Genre = "La Pop", Information = "un album de la chanteuse Lady Gaga", Released = 2008 };

            // Act
            var result = _service.AddAlbum(originalWork);

            // Assert
            (result.ReturnedObject == null).Should().BeTrue();
        }

        #endregion


        //Album valid tests



    }
}
