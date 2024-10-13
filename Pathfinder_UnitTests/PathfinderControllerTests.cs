using Microsoft.AspNetCore.Mvc;

namespace Pathfinder_UnitTests
{
    /// <summary>
    /// These are unit tests for the controller, PathfinderController.
    /// These are similar to the unit tests for the controller, but we expect HTTP responses and we do not expect exceptions to be thrown.
    /// </summary>
    public class PathfinderControllerTests
    {
        #region FindPath() Tests
        public static IEnumerable<object[]> GetPathTestData()
        {
            yield return new object[] { "PAN", new List<string> { "USA", "MEX", "GTM", "HND", "NIC", "CRI", "PAN" } };
            yield return new object[] { "BLZ", new List<string> { "USA", "MEX", "BLZ" } };
            yield return new object[] { "CAN", new List<string> { "USA", "CAN" } };
        }

        #region Returns 200 Ok

        [Theory]
        [MemberData(nameof(GetPathTestData))]
        public void FindPath_CountryCode_Returns200ValidPath(string countryCode, List<string> expectedResult)
        {
            // Arrange
            var pathfinderController = PathfinderSetup.GetController();

            // Act
            var result = pathfinderController.FindPath(countryCode) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedResult, result.Value);
        }

        [Fact]
        public void FindPath_StartPoint_Returns200Start()
        {
            // Arrange
            var pathfinderController = PathfinderSetup.GetController();
            var startLocation = "USA";

            // Act
            var result = pathfinderController.FindPath(startLocation) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(new List<string> { "USA" }, result.Value);
        }
        #endregion

        #region 400 Bad Request
        [Fact]
        public void FindPath_Null_Returns400()
        {
            // Arrange
            var pathfinderController = PathfinderSetup.GetController();

            // Act
            var result = pathfinderController.FindPath(null) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void FindPath_EmptyString_Returns400()
        {
            // Arrange
            var pathfinderController = PathfinderSetup.GetController();

            // Act
            var result = pathfinderController.FindPath(string.Empty) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("USD")]
        public void FindPath_InvalidCountryCodes_Returns400(string invalidCountryCode)
        {
            // Arrange
            var pathfinderController = PathfinderSetup.GetController();

            // Act
            var result = pathfinderController.FindPath(invalidCountryCode) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
        #endregion

        #endregion
    }
}
