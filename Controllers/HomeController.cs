using ApiGeoTestWork.Models;
using ApiGeoTestWork.Services;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Mvc;

namespace ApiGeoTestWork.Controllers
{
    // Контроллер для работы со страницей
    public class HomeController : Controller
    {
        private readonly _kmlService _kmlService;
        private List<GeoInformationClass> _geoInfo;

        public HomeController(_kmlService kmlService)
        {
            _kmlService = kmlService;
            _geoInfo = _kmlService.GetGeoAllInfo();
        }

        public IActionResult Index()
        {
            return View(_geoInfo);
        }

        // Получение всех данных (1 функция)
        [HttpGet("api/all")]
        public IActionResult GetAllData()
        {
            return Json(_geoInfo);
        }

        // Получение площади по id (2 функция)
        [HttpGet("api/square/{id}")]
        public IActionResult GetSquare(int id)
        {
            if (id <= _geoInfo.Count && id > 0)
                return Json(_geoInfo[id - 1].Size);
            return BadRequest("Invalid ID");
        }

        // Получение дистанции между данной точкой и центром поля (3 функция)
        [HttpGet("api/distance/{id}&{lat}&{lng}")]
        public IActionResult GetDistance(int id, double lat, double lng)
        {
            if (id <= _geoInfo.Count && id > 0)
            {
                var center = _geoInfo[id - 1].Location.Center;
                var pointCenter = new GeoCoordinate(center.lat, center.lng);
                var pointGiven = new GeoCoordinate(lat, lng);
                return Json(Math.Round(pointCenter.GetDistanceTo(pointGiven), 2));
            }
            return BadRequest("Invalid ID");
        }

        // Находиться ли точка в одном из полигонов (4 функция)
        [HttpGet("api/belonging/{lat}&{lng}")]
        public IActionResult GetBelonging(double lat, double lng)
        {
            var result = PointAffiliationService.CheckPointLocation(_geoInfo,
                new NetTopologySuite.Geometries.Point(lat, lng));

            return Json(result.IsInside
                ? new { Id = result.Id, Name = result.Name }
                : new { IsInside = false });
        }
    }
}
