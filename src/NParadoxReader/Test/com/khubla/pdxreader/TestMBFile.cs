using System;
using System.Collections.Generic;
using Com.Khubla.Pdxreader.MB;
using Com.Khubla.Pdxreader.Util;
using Java.IO;
using Org.Testng;
using Sharpen;

namespace Test.Com.Khubla.Pdxreader
{
	/// <author>tom</author>
	public class TestMBFile
	{
		public virtual void TestRead()
		{
			try
			{
				IList<string> files = TestUtil.GetTestFiles("src/test/resources/examples/", new string
					[] { ".MB" });
				foreach (string filename in files)
				{
					File inputFile = new File(filename);
					Assert.AssertTrue(inputFile.Exists());
					System.Console.Out.WriteLine(filename);
					MBTableFile mbTableFile = new MBTableFile();
					mbTableFile.Read(inputFile);
				}
			}
			catch (Exception e)
			{
				Sharpen.Runtime.PrintStackTrace(e);
				Assert.Fail();
			}
		}
	}
}
