using System;
using AutoFixture;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UserRepositoryTests
{
    public class UserRepositoryTests
    {
        private readonly DbContextOptions<UserContext> _options;
        private readonly Fixture _fixture = new Fixture();

        public UserRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public void Get_Users_from_database()
        {
            var expectedUserCount = 9;
            var usersToInsert = _fixture.CreateMany<User>(expectedUserCount);

            var dbTester = new DbContextTester<UserContext>(_options);

            dbTester.AddRange(usersToInsert);

            dbTester.Assert(context =>
            {
                var sut = new UserRepository(context);
                var result = sut.GetUsers();

                Assert.Equal(expectedUserCount, result.Count);
            });
        }
    }
}