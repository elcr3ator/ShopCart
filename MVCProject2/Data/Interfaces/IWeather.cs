using MVCProject2.Data.Models;

namespace MVCProject2.Data.Interfaces
{
    public interface IWeather
    {
        IEnumerable<Weather> weathers { get; }
    }
}
