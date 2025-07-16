using ApiGeoTestWork.Services;
using Microsoft.AspNetCore.Mvc;
using ApiGeoTestWork.Models;
using GeoCoordinatePortable;

namespace ApiGeoTestWork.Controllers
{
    // Контроллер для работы с API
    // Работает, когда отправляются URL, в README расписал как можно отправлять запросы
    // Функции такие же как и в HomeController
    [ApiController]
    [Route("api/kml")]
    public class GeoController : ControllerBase
    {

        public List<GeoInformationClass> modelGeoInfo = _kmlService.GetGeoAllInfo();
        
        // Вывод из файла fields (создал для проверки)
        [HttpGet("fields")]
        public IActionResult GetFieldsData()
        {
            try
            {
                var model = _kmlService.GetInfoFields();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // Вывод из файла centroids (создал для проверки)
        [HttpGet("centroids")]
        public IActionResult GetCentroidsData()
        {
            var model = _kmlService.GetInfoCentroids();
            return Ok(model);
        }
        
        // Получение всех данных (1 функция)
        [HttpGet("all")]
        public IActionResult GetAllData()
        {
            return Ok(modelGeoInfo);
        }

        // Получение площади по id (2 функция)
        [HttpGet("square/{id}")]
        public IActionResult GetSquare(int id)
        {
            if (id < modelGeoInfo.Count)
            {
                return Ok(modelGeoInfo[id - 1].Size);
            }
            else
                return StatusCode(500);
        }

        // Получение дистанции между данной точкой и центром поля (3 функция)
        [HttpGet("distance/{id}&{givenLat}&{givenLng}")]
        public IActionResult GetDisctance(int id, double givenLat, double givenLng)
        {
            if(id < modelGeoInfo.Count)
            {
                var pointCenter = new GeoCoordinate(modelGeoInfo[id - 1].Location.Center.lat, modelGeoInfo[id].Location.Center.lng);
                var pointGiven = new GeoCoordinate(givenLat, givenLng);
                return Ok(Math.Round(pointCenter.GetDistanceTo(pointGiven), 2) + " метров");
            }
            else
                return StatusCode(500);
        }

        // Находиться ли точка в одном из полигонов (4 функция)
        [HttpGet("belonging/{givenLat}&{givenLng}")]
        public IActionResult GetBelonging(double givenLat, double givenLng)
        {
            var result = PointAffiliationService.CheckPointLocation(modelGeoInfo, new NetTopologySuite.Geometries.Point(givenLat, givenLng));
            if (result.IsInside) return Ok($"Id - {result.Id}\nName - {result.Name}");
            else return Ok(result.IsInside);
        }
    }
}
