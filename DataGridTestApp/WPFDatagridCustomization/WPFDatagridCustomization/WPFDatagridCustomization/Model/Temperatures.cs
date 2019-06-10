using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFDatagridCustomization.Model
{
	public class Temperatures
	{
		public Temperatures(string state,string city,double max,double min)
		{
			State = state;
			City = city;
			MaxTemperature = max;
			MinTemperature = min;
		}
		public string State { get; set; }
		public string City { get; set; }
		public double MaxTemperature { get; set; }
		public double MinTemperature { get; set; }
		public string Description { get; set; }
	}
}
