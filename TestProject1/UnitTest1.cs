using NUnit.Framework;
using Mercury.Test.WebApi.Controllers;

namespace Mercury.Test.WebApi.Tests.Controllers {
    [TestFixture]
    public class WeatherForecastControllerTests {
        private WeatherForecastController _controller;

        private static readonly string[] ValidSummaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm",
            "Balmy", "Hot", "Sweltering", "Scorching", "Extremely Hot"
        };

        [SetUp]
        public void SetUp()
        {
            _controller = new WeatherForecastController();
        }

        [Test]
        public void Get_ReturnsExactlyFiveForecasts()
        {
            var result = _controller.Get();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(5));
        }

        [Test]
        public void Get_ReturnsDatesStartingFromTomorrowInSequence()
        {
            var result = _controller.Get().ToArray();
            var today = DateOnly.FromDateTime(DateTime.Now);

            for(int i = 0; i < result.Length; i++)
            {
                var expectedDate = today.AddDays(i + 1);
                Assert.That(result[i].Date, Is.EqualTo(expectedDate));
            }
        }

        [Test]
        public void Get_TemperatureIsWithinExpectedRange()
        {
            var result = _controller.Get();

            Assert.That(result.All(f => f.TemperatureC >= -20 && f.TemperatureC < 55), Is.True);
        }

        [Test]
        public void Get_SummaryIsAlwaysFromValidList()
        {
            var result = _controller.Get();

            Assert.That(result.All(f => ValidSummaries.Contains(f.Summary)), Is.True);
        }

        [Test]
        public void Get_ReturnsNonNullSummaryForEachForecast()
        {
            var result = _controller.Get();

            Assert.That(result.All(f => !string.IsNullOrEmpty(f.Summary)), Is.True);
        }

        [Test]
        public void Get_MultipleCallsProduceIndependentResults()
        {
            // No garantiza valores distintos (por el rango de Random),
            // pero valida que cada llamada genera una nueva colección de 5 elementos
            var result1 = _controller.Get().ToArray();
            var result2 = _controller.Get().ToArray();

            Assert.That(result1.Length, Is.EqualTo(5));
            Assert.That(result2.Length, Is.EqualTo(5));
        }
    }
}