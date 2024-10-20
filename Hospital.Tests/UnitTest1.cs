using FakeItEasy;
using HospitalAppAPI.Cahceing;
using HospitalAppAPI.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System.Text.Json;

namespace Hospital.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var redisCache = A.Fake<IRedisCahe>();
            // Specify the type for GetData<T> and return null for it
            A.CallTo(() => redisCache.GetData<string>(A<string>.Ignored)).Returns(null);
        }

        [Fact]
        public async Task Test_GetAllUsers_Returns_ValidResult()
        {
            // Arrange
            var redisCache = A.Fake<IRedisCahe>();

            // Set up redisCache to return a valid list
            var fakeIdentityUserList = new List<IdentityUser>
    {
        new IdentityUser { UserName = "testuser1" },
        new IdentityUser { UserName = "testuser2" }
    };

            // Mock the redisCache to return a non-null list
            A.CallTo(() => redisCache.GetData<IEnumerable<IdentityUser>>(A<string>.Ignored))
                .Returns(fakeIdentityUserList);

            // Pass the mocked redisCache to the controller

            // Act

            // Assert
        }
    }
}