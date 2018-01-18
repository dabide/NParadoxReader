using System;
using System.Collections.Generic;
using Com.Khubla.Pdxreader.Api;
using Com.Khubla.Pdxreader.DB;
using Sharpen;

namespace Com.Khubla.Pdxreader
{
	/// <author>tom</author>
	public class PDXReaderCSVListenerImpl : PDXReaderListener
	{
		/// <summary>total records</summary>
		private int totalRecords = 0;

		public virtual void Finish()
		{
			System.Console.Out.WriteLine("# total records " + totalRecords);
		}

		public virtual void Header(DBTableHeader pdxTableHeader)
		{
			bool first = true;
			foreach (DBTableField pdxTableField in pdxTableHeader.GetFields())
			{
				if (first)
				{
					first = false;
				}
				else
				{
					System.Console.Out.Write(",");
				}
				System.Console.Out.Write(pdxTableField.GetName());
			}
			System.Console.Out.WriteLine();
		}

		public virtual void Record(IList<DBTableValue> values)
		{
			/*
			* count the record
			*/
			totalRecords++;
			/*
			* dump the record
			*/
			bool first = true;
			foreach (DBTableValue pdxTableValue in values)
			{
				if (first)
				{
					first = false;
				}
				else
				{
					System.Console.Out.Write(",");
				}
				System.Console.Out.Write(pdxTableValue.GetValue());
			}
			System.Console.Out.WriteLine();
		}

		public virtual void Start()
		{
			System.Console.Out.WriteLine("# generated " + new DateTime().ToString());
		}
	}
}
