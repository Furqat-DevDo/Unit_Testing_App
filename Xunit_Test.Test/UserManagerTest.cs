global using Xunit;
namespace Xunit_Test.Test
{
    public class UserManagementTest
    {
        [Fact]
        public void Add_CreateUser()
        {
            // Arrange
            var userManagement = new UserManagement();

            // Act
            userManagement.Add(new(
                    "Furqat", "Abduvasikov"
            ));

            // Assert
            var savedUser = Assert.Single(userManagement.AllUsers);
            Assert.NotNull(savedUser);
            Assert.Equal("Furqat", savedUser.FirstName);
            Assert.Equal("Abduvasikov", savedUser.LastName);
            Assert.NotEmpty(savedUser.Phone);
            Assert.False(savedUser.VerifiedEmail);
        }

        [Fact]
        public void Verify_VerifyEmailAddress()
        {
            // Arrange
            var userManagement = new UserManagement();

            // Act
            userManagement.Add(new(
                    "Furqat", "Abduvasikov"
            ));

            var firstUser = userManagement.AllUsers.ToList().First();
            userManagement.VerifyEmail(firstUser.Id);

            // Assert
            var savedUser = Assert.Single(userManagement.AllUsers);
            Assert.True(savedUser.VerifiedEmail);
        }

        [Fact]
        public void Update_UpdateMobileNumber()
        {
            // Arrange
            var userManagement = new UserManagement();

            // Act
            userManagement.Add(new(
                    "Furqat", "Abduvasikov"
            ));

            var firstUser = userManagement.AllUsers.ToList().First();
            firstUser.Phone = "+998998777878";
            userManagement.UpdatePhone(firstUser);

            // Assert
            var savedUser = Assert.Single(userManagement.AllUsers);
            Assert.Equal("+998998777878", savedUser.Phone);
        }
    }
}