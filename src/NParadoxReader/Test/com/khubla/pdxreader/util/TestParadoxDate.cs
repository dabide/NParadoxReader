using System;
using Com.Khubla.Pdxreader.Util;
using Java.Util;
using Org.Testng;
using Sharpen;

namespace Test.Com.Khubla.Pdxreader.Util
{
	public class TestParadoxDate
	{
		public virtual void TestDate1()
		{
			try
			{
				DateTime date = ParadoxDate.GetDateFromParadoxDate(0);
				Calendar calendar = Calendar.GetInstance();
				calendar.SetTime(date);
				Assert.AssertTrue(calendar.Get(Calendar.Date) == 1);
				Assert.AssertTrue(calendar.Get(Calendar.Month) == 1);
				Assert.AssertTrue(calendar.Get(Calendar.Year) == 1);
			}
			catch (Exception e)
			{
				Sharpen.Runtime.PrintStackTrace(e);
				Assert.Fail();
			}
		}

		/*
		* 100 days from January 1, 1 A.D is April 11, 1 A.D.
		*/
		public virtual void TestDate2()
		{
			try
			{
				DateTime date = ParadoxDate.GetDateFromParadoxDate(100);
				Calendar calendar = Calendar.GetInstance();
				calendar.SetTime(date);
				Assert.AssertTrue(calendar.Get(Calendar.Date) == 12);
				Assert.AssertTrue(calendar.Get(Calendar.Month) == 4);
				Assert.AssertTrue(calendar.Get(Calendar.Year) == 1);
			}
			catch (Exception e)
			{
				Sharpen.Runtime.PrintStackTrace(e);
				Assert.Fail();
			}
		}
	}
}
