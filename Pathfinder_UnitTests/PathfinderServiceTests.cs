using PathfinderApi.Models;

namespace Pathfinder_UnitTests
{
    /// <summary>
    /// This file contains unit tests for our Pathfinder class, which only has the FindPath method
    /// All units tests follow the best practices outlined here: https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
    /// </summary>
    public class PathfinderServiceTests
    {
        #region FindPath() Tests

        /// <summary>
        /// xUnit tests can use an IEnumerable to provide MemberData for a Theory.
        /// This is data containing the destination, and the path that should be returned.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetPathTestData()
        {
            yield return new object[] { "PAN", new List<string> { "USA", "MEX", "GTM", "HND", "NIC", "CRI", "PAN" } };
            yield return new object[] { "BLZ", new List<string> { "USA", "MEX", "BLZ" } };
            yield return new object[] { "CAN", new List<string> { "USA", "CAN" } };
        }

        #region Successes
        [Theory]
        [MemberData(nameof(GetPathTestData))]
        public void FindPath_CountryCode_ReturnsValidPath(string countryCode, List<string> expectedResult)
        {
            // Arrange
            var pathfinderSvc = new PathfinderService();

            // Act
            var result = pathfinderSvc.FindPath(countryCode);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void FindPath_StartPoint_ReturnsStart()
        {
            // Arrange
            var pathfinderSvc = new PathfinderService();
            var startLocation = "USA";

            // Act
            var result = pathfinderSvc.FindPath(startLocation);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            Assert.Equal(["USA"], result);
        }

        #endregion

        #region Exceptions
        [Fact]
        public void FindPath_Null_ThrowsException()
        {
            // Arrange
            var pathfinderSvc = new PathfinderService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => pathfinderSvc.FindPath(null));
        }

        [Fact]
        public void FindPath_EmptyString_ThrowsException()
        {
            // Arrange
            var pathfinderSvc = new PathfinderService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => pathfinderSvc.FindPath(string.Empty));
        }

        [Theory]
        [InlineData("A")]
        [InlineData("USD")]
        public void FindPath_InvalidCountryCodes_ThrowsException(string invalidCountryCode)
        {
            // Arrange
            var pathfinderSvc = new PathfinderService();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => pathfinderSvc.FindPath(invalidCountryCode));
        }
        #endregion

        #endregion
    }
}