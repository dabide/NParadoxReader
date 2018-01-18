using System;
using Com.Khubla.Pdxreader.Api;
using Com.Khubla.Pdxreader.DB;
using Java.IO;
using Org.Apache.Commons.Cli;
using Sharpen;

namespace Com.Khubla.Pdxreader
{
	/// <author>tom</author>
	public class PDXReader
	{
		/// <summary>file option</summary>
		private const string FileOption = "file";

		public static void Main(string[] args)
		{
			try
			{
				System.Console.Out.WriteLine("khubla.com Paradox DB reader");
				/*
				* options
				*/
				Options options = new Options();
				Option oo = Option.Builder().ArgName(FileOption).LongOpt(FileOption).Type(Sharpen.Runtime.GetClassForType
					(typeof(string))).HasArg().Required(true).Desc("file to read").Build();
				options.AddOption(oo);
				/*
				* parse
				*/
				CommandLineParser parser = new DefaultParser();
				CommandLine cmd = null;
				try
				{
					cmd = parser.Parse(options, args);
				}
				catch (Exception e)
				{
					Sharpen.Runtime.PrintStackTrace(e);
					HelpFormatter formatter = new HelpFormatter();
					formatter.PrintHelp("posix", options);
					System.Environment.Exit(0);
				}
				/*
				* get file
				*/
				string filename = cmd.GetOptionValue(FileOption);
				if (null != filename)
				{
					File inputFile = new File(filename);
					if (inputFile.Exists())
					{
						DBTableFile pdxFile = new DBTableFile();
						PDXReaderListener pdxReaderListener = new PDXReaderCSVListenerImpl();
						pdxFile.Read(inputFile, pdxReaderListener);
						System.Console.Out.WriteLine("done");
					}
				}
			}
			catch (Exception e)
			{
				Sharpen.Runtime.PrintStackTrace(e);
			}
		}
	}
}
