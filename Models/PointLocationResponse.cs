namespace ApiGeoTestWork.Models
{
    // Класс для вывода в 4 функции с определением точки в полигоне
    public class PointLocationResponse
    {
        public bool IsInside { get; set; }
        public int? Id { get; set; }
        public string? Name { get; set; }

        public PointLocationResponse(bool isInside, int id, string name) {
            IsInside = isInside;
            Id = id;
            Name = name;
        }
        public PointLocationResponse(bool isInside)
        {
            IsInside = isInside;
        }
    }
}
