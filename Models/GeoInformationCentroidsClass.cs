namespace ApiGeoTestWork.Models
{
    // Класс для парсинга из файла centroids
    public class GeoInformationCentroidsClass
    {
        private int _id;
        public int ID { 
            get => _id;
            set { _id = value; }
        }

        private Coordinate _coordinates;
        public Coordinate Coordinates
        {
            get => _coordinates;
            set { _coordinates = value; }
        }

        public GeoInformationCentroidsClass(int id, Coordinate coordinates)
        {
            _id = id;
            _coordinates = coordinates;
        }
    }
}
