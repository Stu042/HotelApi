using Microsoft.EntityFrameworkCore;

namespace HotelApi.Models.Db {
	public class TblHotel : DbContext {
		public int Id { get; set; }
		public string Name { get; set; }
	}
}

