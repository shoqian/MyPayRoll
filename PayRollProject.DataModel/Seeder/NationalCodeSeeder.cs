using Microsoft.AspNetCore.Mvc;

namespace PayRollProject.DataModel.Seeder
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text.Json;
	using DataModel;
	using Entities.Entities;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;

	[Authorize]
	public class NationalCodeSeeder
	{
		private readonly PayRollDbContext _context;


		public NationalCodeSeeder(PayRollDbContext context)
		{
			this._context = context;
		}

		public void SeedFromJson(string jsonFilePath)
		{
			if (!File.Exists(jsonFilePath))
			{
				throw new FileNotFoundException($"فایل Json در مسیر {jsonFilePath} پیدا نشد.");
			}


			var jsonData = File.ReadAllText(jsonFilePath);
			var prefixData = JsonSerializer.Deserialize<List<PrefixData>>(jsonData);


			foreach (var item in prefixData)
			{
				// بررسی وجود استان در دیتابیس
				var province = this._context.Set<Provinces>()
					.FirstOrDefault((Provinces p) => p.Province_Name == item.province);

				if (province == null)
				{
					province = new() { Province_Name = item.province };
					this._context.Set<Provinces>().Add(province);
					this._context.SaveChanges();
				}


				// بررسی وجود شهر در دیتابیس
				var existingCity = this._context.Set<Cities>()
					.FirstOrDefault((Cities c) => c.City_Name == item.city && c.Prefix == item.prefix);

				if (existingCity == null)
				{
					var city = new Cities
					{
						Prefix = item.prefix,
						City_Name = item.city,
						ID_Province = province.ID_Province,
						UserID = "ea29e265-e18d-4998-98ee-62db9344c735",
						CreateDateTime = DateTime.Now
					};
					this._context.Set<Cities>().Add(city);
				}
			}

			this._context.SaveChanges();
		}

		private class PrefixData
		{
			public string prefix { get; set; }

			public string province { get; set; }

			public string city { get; set; }
		}
	}
}