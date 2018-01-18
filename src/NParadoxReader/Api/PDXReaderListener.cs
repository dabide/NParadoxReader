using System.Collections.Generic;
using Com.Khubla.Pdxreader.DB;

namespace Com.Khubla.Pdxreader.Api
{
	/// <author>tom</author>
	public interface PDXReaderListener
	{
		void Finish();

		void Header(DBTableHeader pdxTableHeader);

		void Record(IList<DBTableValue> values);

		void Start();
	}
}
