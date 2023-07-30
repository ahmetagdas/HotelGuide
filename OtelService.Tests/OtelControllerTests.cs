using Data.Entity;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using OtelService.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OtelService.Tests
{
    public class OtelControllerTests
    {
        private OtelController GetOtelController(OtelDbContext dbContext)
        {
            // Mock IPublishEndpoint'i oluşturun
            var publishEndpointMock = new Mock<IPublishEndpoint>();

            return new OtelController(dbContext, publishEndpointMock.Object);
        }

        private void SetupDatabase(OtelDbContext dbContext)
        {
            // Test için örnek Otel verilerini ekleyin
            var oteller = new List<Otel>
            {
                new Otel { Id = 1, Ad = "Test Otel 1" },
                new Otel { Id = 2, Ad = "Test Otel 2" }
            };
            dbContext.Oteller.AddRange(oteller);
            dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetOteller_ShouldReturnOkResultWithOtellerList()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OtelDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new OtelDbContext(options);
            SetupDatabase(dbContext); // Geçici veritabanına örnek Otel verilerini ekleyin
            var controller = GetOtelController(dbContext);

            // Act
            var result = await controller.GetOteller();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var oteller = Assert.IsAssignableFrom<List<Otel>>(okResult.Value);
            Assert.Equal(2, oteller.Count); // Örnek verileri eklediğimiz için 2 otel olmalı
        }

        [Fact]
        public async Task GetOtelById_ShouldReturnOkResultWithOtel()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OtelDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new OtelDbContext(options);
            var controller = GetOtelController(dbContext);
            var otelId = 1; // Örnek bir Otel Id
            var otel = new Otel { Id = otelId, Ad = "Test Otel" };
            dbContext.Oteller.Add(otel);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await controller.GetOtelById(otelId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultOtel = Assert.IsType<Otel>(okResult.Value);
            Assert.Equal(otel.Id, resultOtel.Id);
            Assert.Equal(otel.Ad, resultOtel.Ad);
        }

        [Fact]
        public async Task GetOtelById_ShouldReturnNotFoundResultWhenOtelNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OtelDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new OtelDbContext(options);
            var controller = GetOtelController(dbContext);
            var otelId = 1; // Geçersiz bir Otel Id

            // Act
            var result = await controller.GetOtelById(otelId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateOtel_ShouldReturnCreatedAtActionResultWithOtel()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OtelDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new OtelDbContext(options);
            var controller = GetOtelController(dbContext);
            var otel = new Otel { Ad = "Test Otel" };

            // Act
            var result = await controller.CreateOtel(otel);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var resultOtel = Assert.IsType<Otel>(createdAtActionResult.Value);
            Assert.Equal(otel.Ad, resultOtel.Ad);

            // Verify that the Otel is added to the context
            var otelInDb = await dbContext.Oteller.FindAsync(resultOtel.Id);
            Assert.Equal(otel.Ad, otelInDb.Ad);
        }

        [Fact]
        public async Task UpdateOtel_ShouldReturnNoContentResultWhenSuccessful()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OtelDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new OtelDbContext(options);
            var controller = GetOtelController(dbContext);
            var otelId = 1; // Örnek bir Otel Id
            var otel = new Otel { Id = otelId, Ad = "Test Otel" };
            dbContext.Oteller.Add(otel);
            await dbContext.SaveChangesAsync();

            // Modify the otel's data
            otel.Ad = "Updated Otel";

            // Act
            var result = await controller.UpdateOtel(otelId, otel);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var updatedOtel = await dbContext.Oteller.FindAsync(otelId);
            Assert.Equal(otel.Ad, updatedOtel.Ad);
        }

        [Fact]
        public async Task UpdateOtel_ShouldReturnBadRequestResultWhenOtelIdMismatch()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OtelDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new OtelDbContext(options);
            var controller = GetOtelController(dbContext);
            var otelId = 1; // Örnek bir Otel Id
            var otel = new Otel { Id = otelId, Ad = "Test Otel" };
            dbContext.Oteller.Add(otel);
            await dbContext.SaveChangesAsync();

            // Modify the otel's data
            otel.Ad = "Updated Otel";

            // Act
            var result = await controller.UpdateOtel(999, otel); // Geçersiz bir Otel Id

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        // Diğer unit testler...

        [Fact]
        public async Task DeleteOtel_ShouldReturnNoContentResultWhenSuccessful()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OtelDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new OtelDbContext(options);
            var controller = GetOtelController(dbContext);
            var otelId = 1; // Örnek bir Otel Id
            var otel = new Otel { Id = otelId, Ad = "Test Otel" };
            dbContext.Oteller.Add(otel);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await controller.DeleteOtel(otelId);

            // Assert
            Assert.IsType<NoContentResult>(result);

            // Verify that the Otel is removed from the context
            var deletedOtel = await dbContext.Oteller.FindAsync(otelId);
            Assert.Null(deletedOtel);
        }

        [Fact]
        public async Task DeleteOtel_ShouldReturnNotFoundResultWhenOtelNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OtelDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new OtelDbContext(options);
            var controller = GetOtelController(dbContext);
            var otelId = 1; // Geçersiz bir Otel Id

            // Act
            var result = await controller.DeleteOtel(otelId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetOtelYetkilileri_ShouldReturnOkResultWithYetkililerList()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OtelDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new OtelDbContext(options);
            var controller = GetOtelController(dbContext);

            // Act
            var result = await controller.GetOtelYetkilileri();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var yetkililer = Assert.IsAssignableFrom<List<OtelYetkilisi>>(okResult.Value);
            Assert.NotEmpty(yetkililer);
        }

        [Fact]
        public async Task GetOtelDetay_ShouldReturnOkResultWithOtelDetay()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OtelDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new OtelDbContext(options);
            var controller = GetOtelController(dbContext);
            var otelId = 1; // Örnek bir Otel Id
            var otel = new Otel
            {
                Id = otelId,
                Ad = "Test Otel",
                Yetkililer = new List<OtelYetkilisi>
                {
                    new OtelYetkilisi { Id = 1, Ad = "Ali", Soyad = "Taşdeviren", FirmaUnvan = "Yönetici" },
                    new OtelYetkilisi { Id = 2, Ad = "Ayşe", Soyad = "Kaplan", FirmaUnvan = "Recepsiyon" }
                },
                IletisimBilgileri = new List<IletisimBilgisi>
                {
                    new IletisimBilgisi { Id = 1, Adres = "Test Adres 1", Telefon = "1111111111", BilgiIcerigi = "Test"  },
                    new IletisimBilgisi { Id = 2, Adres = "Test Adres 2", Telefon = "2222222222" , BilgiIcerigi = "Test 2"}
                }
            };
            dbContext.Oteller.Add(otel);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await controller.GetOtelDetay(otelId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultOtel = Assert.IsType<Otel>(okResult.Value);
            Assert.Equal(otel.Id, resultOtel.Id);
            Assert.Equal(otel.Ad, resultOtel.Ad);
            Assert.NotNull(resultOtel.Yetkililer);
            Assert.NotNull(resultOtel.IletisimBilgileri);
        }

        [Fact]
        public async Task GetOtelDetay_ShouldReturnNotFoundResultWhenOtelNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<OtelDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new OtelDbContext(options);
            var controller = GetOtelController(dbContext);
            var otelId = 1; // Geçersiz bir Otel Id

            // Act
            var result = await controller.GetOtelDetay(otelId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

    }
}

