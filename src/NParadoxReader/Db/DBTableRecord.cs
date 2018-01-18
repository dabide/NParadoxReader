using System;
using System.Collections.Generic;
using Com.Khubla.Pdxreader.Api;
using Java.IO;
using Sharpen;

namespace Com.Khubla.Pdxreader.DB
{
	/// <author>tom</author>
	public class DBTableRecord
	{
		/// <summary>read one record</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		public virtual void Read(PDXReaderListener pdxReaderListener, IList<DBTableField>
			 fields, InputStream inputStream)
		{
			try
			{
				IList<DBTableValue> values = new List<DBTableValue>();
				foreach (DBTableField pdxTableField in fields)
				{
					DBTableValue pdxTableValue = new DBTableValue();
					pdxTableValue.Read(pdxTableField, inputStream);
					values.Add(pdxTableValue);
				}
				pdxReaderListener.Record(values);
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in read", e);
			}
		}
	}
}
