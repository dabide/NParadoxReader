using System;
using Java.Util;
using Sharpen;

namespace Com.Khubla.Pdxreader.Util
{
	public class ParadoxDate
	{
		public static DateTime GetDateFromParadoxDate(int d)
		{
			Calendar calendar = Calendar.GetInstance();
			/*
			* Jan 1, 1 A.D.
			*/
			calendar.Set(Calendar.Year, 1);
			calendar.Set(Calendar.Month, 1);
			calendar.Set(Calendar.Date, 1);
			/*
			* add days
			*/
			calendar.Add(Calendar.Date, d);
			/*
			* return date
			*/
			return calendar.GetTime();
		}
	}
}
