using System;

namespace Com.Khubla.Pdxreader.Api
{
	[System.Serializable]
	public class PDXReaderException : Exception
	{
		private const long serialVersionUID = 1L;

		public PDXReaderException(string message)
			: base(message)
		{
		}

		public PDXReaderException(string message, Exception e)
			: base(message, e)
		{
		}
	}
}
