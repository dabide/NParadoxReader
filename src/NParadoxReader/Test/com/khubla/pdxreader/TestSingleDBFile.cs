using System;
using Com.Khubla.Pdxreader;
using Com.Khubla.Pdxreader.Api;
using Com.Khubla.Pdxreader.DB;
using Java.IO;
using Org.Testng;
using Sharpen;

namespace Test.Com.Khubla.Pdxreader
{
	public class TestSingleDBFile
	{
		public virtual void TestRead()
		{
			try
			{
				// File inputFile = new File("src/test/resources/examples/PCLDATA/LPI.DB");
				File inputFile = new File("src/test/resources/examples/PCLDATA/BOLDNESS.DB");
				DBTableFile pdxFile = new DBTableFile();
				PDXReaderListener pdxReaderListener = new PDXReaderCSVListenerImpl();
				pdxFile.Read(inputFile, pdxReaderListener);
			}
			catch (Exception e)
			{
				Sharpen.Runtime.PrintStackTrace(e);
				Assert.Fail();
			}
		}
	}
}
