using System;
using System.Collections.Generic;
using Com.Khubla.Pdxreader.PX;
using Com.Khubla.Pdxreader.Util;
using Java.IO;
using Org.Testng;
using Sharpen;

namespace Test.Com.Khubla.Pdxreader
{
	public class TestPXFile
	{
		public virtual void TestRead()
		{
			try
			{
				IList<string> files = TestUtil.GetTestFiles("src/test/resources/examples/", new string
					[] { ".PX" });
				foreach (string filename in files)
				{
					File inputFile = new File(filename);
					Assert.AssertTrue(inputFile.Exists());
					System.Console.Out.WriteLine(filename);
					PXFile pxFile = new PXFile();
					pxFile.Read(inputFile);
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
