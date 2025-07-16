namespace ApiGeoTestWork.Models
{
    // Класс для парсинга из файла fields
    public class GeoInformationFieldsClass
    {
        private int _id;
        public int Id { 
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

        private List<Coordinate> _coordinates;
        public List<Coordinate> Coordinates
        {
            get => _coordinates;
            set { _coordinates = value; }
        }

        public GeoInformationFieldsClass(int id, string name, int size, List<Coordinate> coordinates)
        {
            _id = id;
            _name = name;
            _size = size;
            _coordinates = coordinates;
        }
    }
}
