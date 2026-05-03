using IncidentAPI.Classes;

namespace AppTests
{
    [Trait("Category", "Unit")]
    public class SumTests
    {
        [Fact]
        public void Sum_PositiveNumbers_ReturnsCorrectResult()
        {
            var mathematics = new Mathematics();
            var result = mathematics.Sum(5, 10);
            Assert.Equal(15, result);
        }
        [Fact]
        public void Sum_NegativeAndPositiveNumbers_ReturnsCorrectResult()
        {
            var mathematics = new Mathematics();
            var result = mathematics.Sum(-3, 7);
            Assert.Equal(4, result);
        }
        [Fact]
        public void Sum_NegativeNumbers_ReturnsCorrectResult()
        {
            var mathematics = new Mathematics();
            var result = mathematics.Sum(-6, -21);
            Assert.Equal(-27, result);
        }
    }
    [Trait("Category", "Unit")]
    public class FactorialTests
    {   
        [Fact]
        public void Factorial_NegativeInteger_ThrowsArgumentException()
        {
            var mathematics = new Mathematics();
            Assert.Throws<ArgumentException>(() => mathematics.Factorial(-3));
        }

        [Theory]
        [InlineData(5, 120)]
        [InlineData(1, 1)]
        [InlineData(0, 1)]
        public void Factorial_ValidInputs_ReturnsExpectedResult(int input, int expected)
        {
            var mathematics = new Mathematics();
            var result = mathematics.Factorial(input);
            Assert.Equal(expected, result);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(-3)]
        [InlineData(-10)]
        public void Factorial_NegativeInputs_ThrowsArgumentException(int input)
        {
            var mathematics = new Mathematics();
            Assert.Throws<ArgumentException>(() => mathematics.Factorial(input));
        }
    }
}
