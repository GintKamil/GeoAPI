using ApiGeoTestWork.Models;
using NetTopologySuite.Geometries; // Используется данная библиотека для определения точки в полигоне

namespace ApiGeoTestWork.Services
{
    // Сервис для определения находиться ли точка в полигоне
    public class PointAffiliationService
    {
        static public PointLocationResponse CheckPointLocation(List<GeoInformationClass> modelGeoInfo, Point searchCoord)
        {
            var polygonArray = CreatePolygon(modelGeoInfo);
            for(int i = 0; i < polygonArray.Length; i++)
                if (polygonArray[i].Contains(searchCoord))
                    return new PointLocationResponse(true, i + 1, modelGeoInfo[i].Name);
            
            return new PointLocationResponse(false);
        }

        static private Polygon[] CreatePolygon(List<GeoInformationClass> modelGeoInfo)
        {
            var polygonArray = new Polygon[modelGeoInfo.Count];
            int count = 0;
            foreach (var GeoInfo in modelGeoInfo) {
                var coordArray = GeoInfo.Location.Polygon;
                NetTopologySuite.Geometries.Coordinate[] coordinates = new NetTopologySuite.Geometries.Coordinate[GeoInfo.Location.Polygon.Count];
                for (int i = 0; i < coordArray.Count; i++)
                {
                    coordinates[i] = new NetTopologySuite.Geometries.Coordinate(coordArray[i].lat, coordArray[i].lng);
                }
                var linearRing = new LinearRing(coordinates);
                polygonArray[count++] = new Polygon(linearRing);
            }
            
            
            return polygonArray;
        }
    }
}
