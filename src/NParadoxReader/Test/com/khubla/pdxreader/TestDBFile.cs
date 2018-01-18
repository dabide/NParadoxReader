using System;
using System.Collections.Generic;
using Com.Khubla.Pdxreader;
using Com.Khubla.Pdxreader.Api;
using Com.Khubla.Pdxreader.DB;
using Com.Khubla.Pdxreader.Util;
using Java.IO;
using Org.Testng;
using Sharpen;

namespace Test.Com.Khubla.Pdxreader
{
	public class TestDBFile
	{
		public virtual void TestRead()
		{
			try
			{
				IList<string> files = TestUtil.GetTestFiles("src/test/resources/examples/", new string
					[] { ".DB" });
				foreach (string filename in files)
				{
					File inputFile = new File(filename);
					Assert.AssertTrue(inputFile.Exists());
					System.Console.Out.WriteLine(filename);
					DBTableFile pdxFile = new DBTableFile();
					PDXReaderListener pdxReaderListener = new PDXReaderCSVListenerImpl();
					pdxFile.Read(inputFile, pdxReaderListener);
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
