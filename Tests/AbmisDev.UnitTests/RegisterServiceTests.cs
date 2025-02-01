using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using backend.Data;
using backend.Data.Dtos;
using backend.Data.Models;
using backend.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AbmisDev.UnitTests
{
    public class RegisterServiceTests
    {
        private DbContextOptions<AppDbContext> GetDbOptions(){
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }
        [Fact]
        public async Task RegisterNewUserAsync_CreatesNewUser_WhenDataIsValid()
        {
            // Arrange
            var options = GetDbOptions();
            using(var context = new AppDbContext(options)){
                var service = new RegisterService(context);
                var request = new RegisterRequestDto{
                    Email = "text@example.com",
                    Password = "secret123",
                    ConfirmPassword = "secret123"
                };

                // Act
                var result = await service.RegisterUserAsync(request);

                // Assert
                result.Should().BeTrue();
                var user = await context.Users.FirstOrDefaultAsync( u => u.Email == request.Email);
                user.Should().NotBeNull();
                user.Email.Should().Be(request.Email);
                user.Password.Should().Be(request.Password);
            }
        }
        [Fact]
        public async Task RegusterUserAsync_ThrowsException_WhenUserAlreadyExists()
        {
            // Arrange
            var options = GetDbOptions();
            using (var context = new AppDbContext(options))
            {
                context.Users.Add(new User {Email = "test@example.com", Password = "secret123"});
                await context.SaveChangesAsync();
                
                var service = new RegisterService(context);
                var request = new RegisterRequestDto
                {
                    Email = "test@example.com",
                    Password = "secret123",
                    ConfirmPassword = "secret123"
                };

                // Act & Assert
                await FluentActions
                    .Invoking(() => service.RegisterUserAsync(request))
                    .Should().ThrowAsync<Exception>()
                    .WithMessage("Ten adres email jest już zajęty!");

            }
        }
        [Fact]
        public async Task RegisterUserAsync_WithNoCorrectEmail_ReturnsException()
        {
            // Arrange 
            var options = GetDbOptions();
            using(var context = new AppDbContext(options))
            {
                context.Users.Add(new User {Email = "text@example.com", Password = "secret123"});
                await context.SaveChangesAsync();

                var service = new RegisterService(context);
                var request = new RegisterRequestDto
                {
                    Email = "textexample.com",
                    Password = "secret123",
                    ConfirmPassword = "secret123"
                };
            
                // Act & Assert
                await FluentActions
                .Invoking(() => service.RegisterUserAsync(request))
                .Should().ThrowAsync<Exception>()
                .WithMessage("Niepoprawny format adresu email!");
                
            }
        }
        [Fact]
        public async Task RegisterUserAsync_WithEmptyOrNullEmail_ThrowsException()
        {
            // Arrange
            var options = GetDbOptions();
            using(var context = new AppDbContext(options))
            {
                var service = new RegisterService(context);
                var request = new RegisterRequestDto
                {
                    Email = "",
                    Password = "secret123",
                    ConfirmPassword = "secret123"
                };

                // Act & Assert
                await FluentActions
                    .Invoking(() => service.RegisterUserAsync(request))
                    .Should().ThrowAsync<Exception>()
                    .WithMessage("Niepoprawny format adresu email!");
            }
        }
        [Fact]
        public async Task RegisterUserAsync_WithNullEmail_ThrowsException()
        {
            // Arrange
            var options = GetDbOptions();
            using(var context = new AppDbContext(options))
            {
                var service = new RegisterService(context);
                var request = new RegisterRequestDto
                {
                    Email = null,
                    Password = "secret123",
                    ConfirmPassword = "secret123"
                };
            
                //Act & Assert
                await FluentActions
                    .Invoking(() => service.RegisterUserAsync(request))
                    .Should().ThrowAsync<Exception>()
                    .WithMessage("Nie podano wymaganych danych!");
            }
        }
        [Fact]
        public async Task RegisterUserAsync_WithDifferentPasswords_ThrowsException()
        {
            // Arrange
            var options = GetDbOptions();
            using(var context = new AppDbContext(options))
            {
                var service = new RegisterService(context);
                var request = new RegisterRequestDto
                {
                    Email = "test@gmail.com",
                    Password = "secret123",
                    ConfirmPassword = "secret1234"
                };

                // Act & Assert
                await FluentActions
                    .Invoking(() => service.RegisterUserAsync(request))
                    .Should().ThrowAsync<Exception>()
                    .WithMessage("Hasła nie są takie same!");
            }
        }
    }
}