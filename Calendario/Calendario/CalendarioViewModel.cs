using System;
using System.Collections.Generic;
using System.Linq;

namespace Calendario
{
	public class CalendarioViewModel
	{
		public IEnumerable<DateTime> Datas { get; }

		public CalendarioViewModel()
		{
			var data = new DateTime(2018, 12, 16);

			var weeks = 4;

			var days = weeks * 7;

			var datas = new List<DateTime>();

			for (int n = 0; n < days; n++)
			{
				datas.Add(data);
				data = data.AddDays(1);
			}

			Datas = datas;
		}
	}
}