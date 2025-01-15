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
        [SetUp]
        public void Setup()
        {

            //var options = new DbContextOptionsBuilder<RecordShopDbContext>()
            //    .UseInMemoryDatabase(databaseName: "mockDb")
            //    .Options;

            //using (var context = new RecordShopDbContext(options))
            //{
            //    context.Albums.Add(new Album { Id = 1, Artist = "Lady Gaga", Genre = "La Pop", Information = "un album de la chanteuse Lady Gaga", Name = "La renommee", Released = 2008 });
            //    context.Albums.Add(new Album { Id = 2, Artist = "Lady Gaga", Genre = "La Pop", Information = "un autre album de la chanteuse Lady Gaga", Name = "nee de cette facon", Released = 2011 });
            //    context.SaveChanges();
            //}
           
            //using(var context = new RecordShopDbContext(options))
            //{
            //    RecordShopRepository _mockRepository = new RecordShopRepository(context);
            //    var output = _mockRepository.RetrieveAllAlbums();
            //    output.Should().BeEquivalentTo(new List<Album>());
            //}
         }

        [Test]
        public void _db_Invokes()
        {
            var options = new DbContextOptionsBuilder<RecordShopDbContext>()
             .UseInMemoryDatabase(databaseName: "mockDb")
             .Options;

            using (var context = new RecordShopDbContext(options))
            {
                context.Albums.Add(new Album { Id = 1, Artist = "Lady Gaga", Genre = "La Pop", Information = "un album de la chanteuse Lady Gaga", Name = "La renommee", Released = 2008 });
                context.Albums.Add(new Album { Id = 2, Artist = "Lady Gaga", Genre = "La Pop", Information = "un autre album de la chanteuse Lady Gaga", Name = "nee de cette facon", Released = 2011 });
                context.SaveChanges();
            }

            using (var context = new RecordShopDbContext(options))
            {
                RecordShopRepository _mockRepository = new RecordShopRepository(context);
                var output = _mockRepository.RetrieveAllAlbums();
                output.Should().BeEquivalentTo(new List<Album>());
            }
        }
    }

}
