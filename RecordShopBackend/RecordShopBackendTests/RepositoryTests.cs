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
                if (!context.Albums.Any())
                {
                    context.Albums.AddRange(testAlbums);
                    context.SaveChanges();
                }
            }

        }

        [Test]
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

        [Test]
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
    }

}
