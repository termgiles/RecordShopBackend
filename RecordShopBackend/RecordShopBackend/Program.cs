
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using RecordShopBackend.Database;
using RecordShopBackend.Repository;
using RecordShopBackend.Service;

namespace RecordShopBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add scoped
            builder.Services.AddScoped<IRecordShopRepository, RecordShopRepository>();
            builder.Services.AddScoped<IRecordShopService,RecordShopService>();

            // Database configuration
            //string connectionString = builder.Configuration.GetConnectionString("InMemoryConnection");
            //builder.Services.AddDbContext<RecordShopDbContext>(options => options.UseSqlServer(connectionString));

            if (builder.Configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                builder.Services.AddDbContext<RecordShopDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
