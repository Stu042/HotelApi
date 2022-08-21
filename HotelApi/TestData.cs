using HotelApi.Repositories;

using Microsoft.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApi {
	public class TestData {
		private readonly HotelRepo _hotelRepo;
		public TestData(HotelRepo hotelRepo) {
			_hotelRepo = hotelRepo;
		}


		public void AddHotel(int id, string name) {
			//_hotelRepo.
			//using (SqlConnection con = new SqlConnection("Data Source = (localdb)\\mssqllocaldb; Initial Catalog = myTestDB; Integrated Security = True; Pooling = False")) {
			//	con.Open();

			//	SqlCommand cmd = new SqlCommand("INSER INTO [tesTable] VALUES (@fname, @lname)", con);

			//	cmd.Parameters.AddWithValue("@fname", textBox1.Text);
			//	cmd.Parameters.AddWithValue("@lname", textBox2.Text);
			//}
		}
	}
}
