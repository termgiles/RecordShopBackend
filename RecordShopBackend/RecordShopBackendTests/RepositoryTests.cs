using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using RecordShopBackend;
using RecordShopBackend.Service;
using RecordShopBackend.Controllers;
using RecordShopBackend.Database;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using RecordShopBackend.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;


namespace RecordShopBackendTests
{
    internal class RepositoryTests
    {
        //private readonly RecordShopDbContext _dbContext;
        DbContextOptions<RecordShopDbContext> options = new DbContextOptionsBuilder<RecordShopDbContext>()
        .UseInMemoryDatabase(databaseName: "mockDb")
        .Options;


        private List<Album> testAlbums = new List<Album> { (new Album { Id = 1, Artist = "Lady Gaga", Genre = "La Pop", Information = "un album de la chanteuse Lady Gaga", Name = "La renommee", Released = 2008 }),
        new Album { Id = 2, Artist = "Lady Gaga", Genre = "La Pop", Information = "un autre album de la chanteuse Lady Gaga", Name = "nee de cette facon", Released = 2011 } };


        [SetUp]
        public void Setup()
        {

            using (var context = new RecordShopDbContext(options))
            {
                //foreach (var album in context.Albums)
                //{
                //    context.Albums.Remove(album);
                //}
                if (!context.Albums.Any())
                {
                    context.Albums.AddRange(testAlbums);
                    context.SaveChanges();
                }
            }

        }

        [Test, Order(1)]
        public void RetrieveAllAlbums_ReturnsFullAlbumList()
        {

            // Act
            using (var context = new RecordShopDbContext(options))
            {
                RecordShopRepository _mockRepository = new RecordShopRepository(context);
                var output = _mockRepository.RetrieveAllAlbums();

                //Assert
                output.Should().BeEquivalentTo(testAlbums);
            }
        }

        [Test, Order(2)]
        public void RetrieveAlbumById_ReturnsTrueWithCorrectAlbum()
        {

            // Act
            using (var context = new RecordShopDbContext(options))
            {
                RecordShopRepository _mockRepository = new RecordShopRepository(context);
                var output = _mockRepository.RetrieveAlbumById(1);

                //Assert
                output.Found.Should().BeTrue();
                output.ReturnedObject.Should().BeEquivalentTo(testAlbums[0]);
            }
        }

        [Test]
        public void RetrieveAlbumById_ReturnsFalseAndNullIfNoAlbum()
        {
            // Act
            using (var context = new RecordShopDbContext(options))
            {
                RecordShopRepository _mockRepository = new RecordShopRepository(context);
                var output = _mockRepository.RetrieveAlbumById(70);

                //Assert
                output.Found.Should().BeFalse();
                output.ReturnedObject.Should().BeNull();
            }
        }

        [Test]
        public void PutAlbumById_ReturnsModifiedAlbumAndTrue()
        {
            // Arrange
            AlbumModification ammendments = new AlbumModification { Genre = "Art", Released = 2025 };
            string expected = "Art";

            //Act
            using (var context = new RecordShopDbContext(options))
            {
                RecordShopRepository _mockRepository = new RecordShopRepository(context);
                var output = _mockRepository.UpdateAlbumById(2, ammendments);

            //Assert
                output.Found.Should().BeTrue();
                output.ReturnedObject.Genre.Should().Be(expected);
            }

        }

        [Test]
        public void PutAlbumById_ReturnsFalseIfNoAlbum()
        {
            // Arrange
            AlbumModification ammendments = new AlbumModification { Artist = "Madamme Gaga" };

            //Act
            using (var context = new RecordShopDbContext(options))
            {
                RecordShopRepository _mockRepository = new RecordShopRepository(context);
                var output = _mockRepository.UpdateAlbumById(70, ammendments);

            //Assert
                output.Found.Should().BeFalse();
                output.ReturnedObject.Should().BeNull();
            }

        }

        [Test]
        public void DeleteAlbumById_ReturnsTrueIfAlbumExists()
        {
    
            //Act
            using (var context = new RecordShopDbContext(options))
            {
                RecordShopRepository _mockRepository = new RecordShopRepository(context);
                bool output = _mockRepository.DeleteAlbumById(1);

            //Assert
                output.Should().BeTrue();
                _mockRepository.RetrieveAlbumById(1).Found.Should().BeFalse();
            }

        }
        [Test]
        public void DeleteAlbumById_ReturnsFalseIfNoAlbum()
        {

            //Act
            using (var context = new RecordShopDbContext(options))
            {
                RecordShopRepository _mockRepository = new RecordShopRepository(context);
                bool output = _mockRepository.DeleteAlbumById(70);

            //Assert
                output.Should().BeFalse();
            }

        }

        [Test]
        public void CreateAlbum_ReturnsTrueIfAlreadyExists()
        {

            // Arrange
            Album duplicateAlbum = new Album { Id = 1, Artist = "Lady Gaga", Genre = "La Pop", Information = "un album de la chanteuse Lady Gaga", Name = "La renommee", Released = 2008 };

            //Act
            using (var context = new RecordShopDbContext(options))
            {
                RecordShopRepository _mockRepository = new RecordShopRepository(context);
                var output = _mockRepository.CreateAlbum(duplicateAlbum);

                //Assert
                output.Found.Should().BeTrue();
            }

        }

        [Test]
        public void CreateAlbum_returnsFalseIfNoAlbum()
        {
            // Arrange
            Album originalWork = new Album { Id = 3, Artist = "Lady Gaga", Genre = "La Pop", Information = "un album de la chanteuse Lady Gaga", Name = "nouveau album", Released = 2025 };

            //Act
            using (var context = new RecordShopDbContext(options))
            {
                RecordShopRepository _mockRepository = new RecordShopRepository(context);
                var output = _mockRepository.CreateAlbum(originalWork);

                //Assert
                output.Found.Should().BeFalse();
                output.ReturnedObject.Should().BeEquivalentTo(originalWork);
            }
        }

        [Test]
        public void RetrieveAlbumQuery_ReturnsFullListEmptyQuery()
        {
            //Arrange
            AlbumModification emptyQuery = new AlbumModification();
            List<Album> output;
            List<Album> expected;
            using (var context = new RecordShopDbContext(options))
            {
                RecordShopRepository _mocRepository = new RecordShopRepository(context);
                expected = _mocRepository.RetrieveAllAlbums();
            }

            //Act
            using (var context = new RecordShopDbContext(options))
            {
                RecordShopRepository _mocRepository = new RecordShopRepository(context);
                output = _mocRepository.RetrieveAlbumQuery(emptyQuery);
            }

            //Assert
            output.Count.Should().Be(expected.Count);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            using (var context = new RecordShopDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Dispose();
            }
        }
    }
}
