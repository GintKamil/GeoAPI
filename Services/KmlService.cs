using ApiGeoTestWork.Models;
using Aspose.Gis;
using Aspose.Gis.Geometries;
namespace ApiGeoTestWork.Services
{
    // Сервис для парсинга KML файла через библиотеку Aspose.Gis
    public class _kmlService
    {
        // Получение из файла fields
        static public List<GeoInformationFieldsClass> GetInfoFields()
        {
            var listInfo = new List<GeoInformationFieldsClass>();
            using (var layer = Drivers.Kml.OpenLayer("wwwroot/kml-files/fields.kml"))
            {
                foreach(var feature in layer) {
                    var listInfoCoord = new List<Coordinate>();
                    if (feature.Geometry is IPolygon polygon)
                    {
                        var ring = polygon.ExteriorRing;
                        for (int i = 0; i < ring.Count; i++)
                        {
                            listInfoCoord.Add(new Coordinate
                            {
                                lat = ring[i].X,
                                lng = ring[i].Y
                            });
                        }
                    }
                    var features = new GeoInformationFieldsClass(feature.GetValue<int>("fid"), feature.GetValue<string>("name"), feature.GetValue<int>("size"), listInfoCoord);
                    listInfo.Add(features);
                }
            }
            return listInfo;
        }

        // Получение из файла centroids
        static public List<GeoInformationCentroidsClass> GetInfoCentroids()
        {
            var listInfo = new List<GeoInformationCentroidsClass>();
            using (var layer = Drivers.Kml.OpenLayer("wwwroot/kml-files/centroids.kml"))
            {
                foreach(var feature in layer)
                {
                    Coordinate coord = new Coordinate();
                    if (feature.Geometry is IPoint point)
                    {
                        coord.lat = point.X;
                        coord.lng = point.Y;
                    }
                    var features = new GeoInformationCentroidsClass(feature.GetValue<int>("fid"), coord);
                    listInfo.Add(features);
                    
                }
            }
            return listInfo;
        }

        // Объединение информации из файла fields и centroids в один общий класс
        static public List<GeoInformationClass> GetGeoAllInfo()
        {
            var listInfo = new List<GeoInformationClass>();

            var fields = GetInfoFields();
            var centoroids = GetInfoCentroids();

            for (int i = 0; i < centoroids.Count; i++)
            {
                listInfo.Add(new GeoInformationClass(fields[i].Id, fields[i].Name, fields[i].Size, centoroids[i].Coordinates, fields[i].Coordinates));
            }
            return listInfo;
        }
    }

}
