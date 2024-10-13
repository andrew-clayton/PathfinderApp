using PathfinderApi.Models;

namespace Pathfinder_UnitTests
{
    /// <summary>
    /// This file contains unit tests for our Pathfinder class, which only has the FindPath method
    /// All units tests follow the best practices outlined here: https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
    /// </summary>
    public class PathfinderTests
    {
        #region FindPath() Tests
        public static IEnumerable<object[]> GetPathTestData()
        {
            yield return new object[] { "PAN", new List<string> { "USA", "MEX", "GTM", "HND", "NIC", "CRI", "PAN" } };
            yield return new object[] { "BLZ", new List<string> { "USA", "MEX", "BLZ" } };
            yield return new object[] { "CAN", new List<string> { "USA", "CAN" } };
        }

        [Theory]
        [MemberData(nameof(GetPathTestData))]
        public void FindPath_CountryCode_ReturnsValidPath(string countryCode, List<string> expectedResult)
        {
            // Act
            var result = Pathfinder.FindPath(countryCode);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void FindPath_Null_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Pathfinder.FindPath(null));
        }

        [Fact]
        public void FindPath_EmptyString_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Pathfinder.FindPath(string.Empty));
        }

        [Theory]
        [InlineData("A")]
        [InlineData("USD")]
        public void FindPath_InvalidCountryCodes_ThrowsException(string invalidCountryCode)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => Pathfinder.FindPath(invalidCountryCode));
        }

        [Fact]
        public void FindPath_StartPoint_ReturnsStart()
        {
            // Arrange
            var startLocation = "USA";

            // Act
            var result = Pathfinder.FindPath(startLocation);

            // Assert
            Assert.Equal(1, result.Count);
            Assert.Equal(["USA"], result);
        }
        #endregion
    }
}