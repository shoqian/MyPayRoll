using System.Text.Json;

using PayRollProject.DataModel.Services.Interface;
using PayRollProject.DataModel.Services.Repository;
using PayRollProject.Entities.Entities;

namespace PayRollProject.DataModel.Seeder
{
	public class GeoSeeder
	{
		private readonly IUnitOfWork _uow;
		private readonly IAuditService _audit;

		public GeoSeeder(IUnitOfWork unitOfWork, IAuditService audit)
		{
			_uow = unitOfWork;
			_audit = audit;
		}

		public async Task SeedFromJsonAsync(string filePath, string userId = "ea29e265-e18d-4998-98ee-62db9344c735")
		{
			if (!File.Exists(filePath))
			{
				Console.WriteLine($"⚠️ فایل '{filePath}' پیدا نشد.");
				return;
			}

			var jsonData = await File.ReadAllTextAsync(filePath);
			var data = JsonSerializer.Deserialize<List<GeoDataModel>>(jsonData);

			using (var transaction = _uow.BeginTransaction())
			{
				try
				{
					foreach (var item in data)
					{
						if (string.IsNullOrWhiteSpace(item.Province))
							continue;

						// -------- Province --------
						var province = _uow.GeoProvincesUw
							.Get(p => p.Province_Name == item.Province)
							.FirstOrDefault();

						if (province == null)
						{
							var newProvince = new GeoProvinces
							{
								Province_Name = item.Province,
								UserID = userId,
								CreateDateTime = DateTime.Now
							};
							_uow.GeoProvincesUw.Create(newProvince);
							_audit.LogChange<GeoProvinces>(null, newProvince, "Create", userId);
							_uow.Save();
							province = newProvince;
						}

						// -------- County --------
						var county = _uow.GeoCountiesUw
							.Get(c => c.County_Name == item.County && c.GeoProvince_ID == province.GeoProvince_ID)
							.FirstOrDefault();

						if (county == null)
						{
							var newCounty = new GeoCounties
							{
								County_Name = item.County,
								GeoProvince_ID = province.GeoProvince_ID,
								UserID = userId,
								CreateDateTime = DateTime.Now
							};
							_uow.GeoCountiesUw.Create(newCounty);
							_audit.LogChange<GeoCounties>(null, newCounty, "Create", userId);
							_uow.Save();
							county = newCounty;
						}

						// -------- District --------
						var district = _uow.GeoDistrictsUw
							.Get(d => d.Districts_Name == item.District && d.GeoCounty_ID == county.GeoCounty_ID)
							.FirstOrDefault();

						if (district == null)
						{
							var newDistrict = new GeoDistricts
							{
								Districts_Name  = item.District,
								GeoProvince_ID = province.GeoProvince_ID,
								GeoCounty_ID = county.GeoCounty_ID,
								UserID = userId,
								CreateDateTime = DateTime.Now
							};
							_uow.GeoDistrictsUw.Create(newDistrict);
							_audit.LogChange<GeoDistricts>(null, newDistrict, "Create", userId);
							_uow.Save();
							district = newDistrict;
						}

						// -------- RuralDistrict --------
						var rural = _uow.GeoRuralDistrictsUw
							.Get(r => r.RuralDistricts_Name == item.RuralDistrict && r.GeoDistricts_ID == district.GeoDistricts_ID)
							.FirstOrDefault();

						if (rural == null)
						{
							var newRural = new GeoRuralDistricts
							{
								RuralDistricts_Name = item.RuralDistrict,
								GeoProvince_ID = province.GeoProvince_ID,
								GeoCounty_ID = county.GeoCounty_ID,
								GeoDistricts_ID = district.GeoDistricts_ID,
								UserID = userId,
								CreateDateTime = DateTime.Now
							};
							_uow.GeoRuralDistrictsUw.Create(newRural);
							_audit.LogChange<GeoRuralDistricts>(null, newRural, "Create", userId);
							_uow.Save();
							rural = newRural;
						}

						// -------- Neighborhood --------
						if (!string.IsNullOrWhiteSpace(item.Neighborhood))
						{
							var neigh = _uow.GeoNeighborhoodsUw
								.Get(n => n.Neighborhoods_Name == item.Neighborhood && n.City_Name == item.City)
								.FirstOrDefault();

							if (neigh == null)
							{
								var newNeigh = new GeoNeighborhoods
								{
									Neighborhoods_Name = item.Neighborhood,
									City_Name = item.City,
									GeoProvince_ID = province.GeoProvince_ID,
									GeoCounty_ID = county.GeoCounty_ID,
									GeoDistricts_ID = district.GeoDistricts_ID,
									GeoRuralDistricts_ID = rural.GeoRuralDistricts_ID,
									UserID = userId,
									CreateDateTime = DateTime.Now
								};
								_uow.GeoNeighborhoodsUw.Create(newNeigh);
								_audit.LogChange<GeoNeighborhoods>(null, newNeigh, "Create", userId);
								_uow.Save();
							}
						}
					}

					transaction.Commit();
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("✅ GeoSeeder با موفقیت اجرا شد.");
					Console.ResetColor();
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"❌ خطا در اجرای Seeder: {ex.Message}");
					Console.ResetColor();
				}
			}
		}
	}

	public class GeoDataModel
	{
		public string Province { get; set; }
		public string County { get; set; }
		public string District { get; set; }
		public string RuralDistrict { get; set; }
		public string City { get; set; }
		public string Neighborhood { get; set; }
	}
}
