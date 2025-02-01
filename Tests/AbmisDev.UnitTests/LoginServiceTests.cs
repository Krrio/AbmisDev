using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend;
using backend.Data;
using backend.Data.Dtos;
using backend.Data.Models;
using backend.Services;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AbmisDev.UnitTests
{
    public class LoginServiceTests
    {
         private DbContextOptions<AppDbContext> GetDbOptions(){
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }
        [Fact]
        public async Task AuthenticateUserAsync_WithCorrectData_ReturnsUser()
        {
            // Arrange
            var options = GetDbOptions();
            using (var context = new AppDbContext(options))
            {
                context.Users.Add(new User {Email = "test@example.com", Password="secret123"});
                await context.SaveChangesAsync();

                var service = new LoginService(context, null);

                // Act 
                var result = await service.AuthenticateUserAsync(new LoginRequestDto {Email = "test@example.com" , Password = "secret123"});

                // Assert
                result.Should().NotBeNull();
                result!.Email.Should().Be("test@example.com");
            }
        }
        [Fact]
        public async Task AuthenticateUserAsync_WithUserNotFound_ReturnsNull()
        {
            // Arrange
            var options = GetDbOptions();
            using(var context = new AppDbContext(options))
            {
                var service = new LoginService(context, null);

                // Act
                var result = await service.AuthenticateUserAsync(new LoginRequestDto
                {
                    Email = "test@example",
                    Password="123415124"
                });

                // Assert
                result.Should().BeNull();

            }
        }
        [Fact]
        public async Task GenerateJwtToken_ReturnsValidToken_WithExpectedClaims()
        {
            // Arrange
            var options = GetDbOptions();
            using (var context = new AppDbContext(options))
            {
                // Przygotowanie ustawień JWT
                var jwtSettings = new JwtSettings
                {
                    SecretKey = "YourVerySecureAndLongSecretKey12345", 
                    Issuer = "TestIssuer",
                    Audience = "TestAudience",
                    ExpiryMinutes = 60
                };

                var service = new LoginService(context, jwtSettings);

                // Przygotowanie przykładowego użytkownika
                var user = new User
                {
                    Id = 1, 
                    Email = "test@example.com",
                    Password = "secret123" 
                };

                // Act
                var token = service.GenerateJwtToken(user);

                // Assert 
                token.Should().NotBeNullOrEmpty();

                
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                jwtToken.Should().NotBeNull();

                var subClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
                subClaim.Should().NotBeNull();
                subClaim.Value.Should().Be(user.Id.ToString());

                var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                emailClaim.Should().NotBeNull();
                emailClaim.Value.Should().Be(user.Email);

                jwtToken.Issuer.Should().Be(jwtSettings.Issuer);
                jwtToken.Audiences.Should().Contain(jwtSettings.Audience);
            }
        }
        [Fact]
        public async Task AuthenticateUserAsync_WithIncorrectPassword_ReturnsNull()
        {
            // Arrange
            var options = GetDbOptions();
            using(var context = new AppDbContext(options))
            {
                context.Users.Add(new User {Email = "test@example.com", Password="secret123"});

                await context.SaveChangesAsync();

                var service = new LoginService(context, null);

                // Act
                var result = await service.AuthenticateUserAsync(new LoginRequestDto
                {
                    Email = "test@example.com",
                    Password="123415124"
                });

                // Assert
                result.Should().BeNull();
                
            }
        }

    }
}