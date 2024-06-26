﻿using Microsoft.AspNetCore.Identity;

namespace PhanThiHoaiAnh_223DATN_DVTC.Models
{
	public class AppUserModel : IdentityUser
	{
		public string FName { get; set; }
		public string LName {  get; set; }
		public DateOnly Birthday { get; set; }
		public string Address { get; set; }
		public string Gender { get; set; }
		public string Position { get; set; }
		public string IRole { get; set; }
		public PositionModel Positions { get; set; }
		public IdentityRole Role { get; set; }
	}
}
