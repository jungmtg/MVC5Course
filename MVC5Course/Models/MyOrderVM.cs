using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
	public enum Status
	{
		P,
		C,
		R
	}
	public class MyOrderVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Status Status { get; set; }
	}
}