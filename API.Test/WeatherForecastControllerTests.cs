using Xunit;
using API.Controllers;
using Microsoft.Extensions.Logging.Abstractions;

namespace API.Test;

public class WeatherForecastControllersTests
{
    NullLogger<WeatherForecastController> _logger;
    public WeatherForecastControllersTests()
    {
        _logger = new NullLogger<WeatherForecastController>();
    }
    [Fact]
    public void GetWeatherForecast_ReturnsValues()
    {
        //Arrange
        var controller = new WeatherForecastController(_logger);

        //Act
        var result = controller.Get();

        //Assert
        Assert.NotNull(result);

    }

}