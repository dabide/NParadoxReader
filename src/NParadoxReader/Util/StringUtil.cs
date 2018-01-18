using Com.Google.Common.IO;
using Java.Lang;
using Sharpen;

namespace Com.Khubla.Pdxreader.Util
{
	/// <author>tom</author>
	public class StringUtil
	{
		public static string ByteArrayToString(byte[] bytes)
		{
			StringBuilder builder = new StringBuilder();
			foreach (byte b in bytes)
			{
				builder.Append(string.Format("%02x", b));
			}
			return builder.ToString();
		}

		/// <summary>read a fixed length string from a byte buffer</summary>
		/// <exception cref="System.IO.IOException"/>
		public static string ReadString(byte[] data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while ((data[i] != 0) && (i < (data.Length - 1)))
			{
				stringBuilder.Append((char)data[i++]);
			}
			return stringBuilder.ToString().Trim();
		}

		/// <summary>read a null terminated string from a LittleEndianDataInputStream</summary>
		/// <exception cref="System.IO.IOException"/>
		public static string ReadString(LittleEndianDataInputStream littleEndianDataInputStream
			)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int c = littleEndianDataInputStream.ReadUnsignedByte();
			while (c != 0)
			{
				stringBuilder.Append((char)c);
				c = littleEndianDataInputStream.ReadUnsignedByte();
			}
			return stringBuilder.ToString();
		}
	}
}
