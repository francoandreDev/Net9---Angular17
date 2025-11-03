// ***********************************************************************
// Assembly         : MiniApp.Tests
// Author           : francoandreDev
// Created          : 2025-11-03
// Description      : Unit tests for the User model class to validate property initialization,
//                    validation logic, and string formatting behavior.
// ***********************************************************************

using MiniApp.Models.Users;

namespace MiniApp.Tests.Models.Users
{
    /// <summary>
    /// 🧩 Contains comprehensive unit tests for the <see cref="User"/> model.
    /// Validates proper initialization, field validation, and string output formatting.
    /// </summary>
    public class UserTests
    {
        #region 🏗️ Constructor Tests

        /// <summary>
        /// ✅ Ensures that the constructor properly initializes all properties.
        /// </summary>
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            int id = 1;
            string username = "franco";
            string email = "franco@example.com";

            // Act
            var user = new User(id, username, email);

            // Assert
            Assert.Equal(id, user.Id);
            Assert.Equal(username, user.Username);
            Assert.Equal(email, user.Email);
        }

        #endregion

        #region 🧠 Validation Tests

        /// <summary>
        /// ✅ Checks that a valid user instance passes the <see cref="User.IsValid"/> check.
        /// </summary>
        [Fact]
        public void IsValid_ShouldReturnTrue_WhenUserIsValid()
        {
            // Arrange
            var user = new User(1, "alice", "alice@mail.com");

            // Act
            bool result = user.IsValid();

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// ❌ Ensures that invalid users (bad ID, username, or email) fail validation.
        /// </summary>
        [Theory]
        [InlineData(0, "john", "john@mail.com")]          // invalid id
        [InlineData(-5, "john", "john@mail.com")]         // invalid id
        [InlineData(1, "", "john@mail.com")]              // empty username
        [InlineData(1, "   ", "john@mail.com")]           // whitespace username
        [InlineData(1, "john", "")]                       // empty email
        [InlineData(1, "john", "   ")]                    // whitespace email
        [InlineData(1, "john", "johnmail.com")]           // missing '@'
        [InlineData(1, "john", "@mail.com")]              // incomplete address
        public void IsValid_ShouldReturnFalse_WhenInvalid(int id, string username, string email)
        {
            // Arrange
            var user = new User(id, username, email);

            // Act
            bool result = user.IsValid();

            // Assert
            Assert.False(result);
        }

        #endregion

        #region 🧾 ToString Tests

        /// <summary>
        /// ✅ Verifies that <see cref="User.ToString"/> returns a properly formatted string.
        /// </summary>
        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var user = new User(1, "alice", "alice@mail.com");

            // Act
            string result = user.ToString();

            // Assert
            Assert.Equal("alice (alice@mail.com)", result);
        }

        /// <summary>
        /// ✅ Confirms <see cref="User.ToString"/> works correctly with special characters in email.
        /// </summary>
        [Fact]
        public void ToString_ShouldWorkWithSpecialCharacters()
        {
            // Arrange
            var user = new User(2, "Bob_Smith", "bob.smith+dev@mail.co.uk");

            // Act
            string result = user.ToString();

            // Assert
            Assert.Equal("Bob_Smith (bob.smith+dev@mail.co.uk)", result);
        }

        #endregion
    }
}
