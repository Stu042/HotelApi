using Microsoft.EntityFrameworkCore;

namespace HotelApi.Models.Db
{
    public class RoomModel : DbContext {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
        public int Hotel { get; set; }
	}
}
