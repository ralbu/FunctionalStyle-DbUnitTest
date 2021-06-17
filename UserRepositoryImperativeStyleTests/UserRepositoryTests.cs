using System;
using AutoFixture;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UserRepositoryImperativeStyleTests
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

            using (var userContext = new UserContext(_options))
            {
                userContext.AddRange(usersToInsert);
                userContext.SaveChanges();
            }

            using (var userContext = new UserContext(_options))
            {
                var sut = new UserRepository(userContext);
                var result = sut.GetUsers();

                Assert.Equal(expectedUserCount, result.Count);
            }
        }
    }
}