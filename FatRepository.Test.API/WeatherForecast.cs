using System.ComponentModel.DataAnnotations;

namespace FatRepository.SQLServer.Test.API
{
    public class WeatherForecast
    {
        [Key]
        public int Id { get; set; }
        public string Location { get; set; }
        public int Weather { get; set; }
    }
}