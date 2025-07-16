using Microsoft.Extensions.Caching.Memory;

namespace ApiGeoTestWork.Models
{
    // Общий класс для всей необходимой информации
    public class GeoInformationClass
    {
        private int _id;
        public int Id
        {
            get => _id;
            set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; }
        }

        private int _size;
        public int Size
        {
            get => _size;
            set { _size = value; }
        }

        private LocationClass _location = new LocationClass();
        public LocationClass Location
        {
            get => _location;
            set { _location = value; }
        }

        public GeoInformationClass(int id, string name, int size, Coordinate center, List<Coordinate> polygon)
        {
            _id = id;
            _name = name;
            _size = size;
            _location.Center = center;
            _location.Polygon = polygon;
        }
    }


}
