namespace ApiGeoTestWork.Models
{
    // Класс для хранения координат, используется для центральной точки и точек полигона
    public class Coordinate
    {
        public double lat { get; set; }
        public double lng { get; set; }


        public override string ToString() => $"{lat}, {lng}";
    }
}
