using Microsoft.EntityFrameworkCore;

namespace HotelApi.Models.Db
{
    public class TblRoom : DbContext {
        public int Id { get; set; }
		public int Number { get; set; }
		public string Type { get; set; }
        public int Capacity { get; set; }
        public int HotelId { get; set; }
	}
}
