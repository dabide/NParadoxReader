using System;
using Com.Khubla.Pdxreader.Api;
using Com.Khubla.Pdxreader.Util;
using Java.IO;
using Java.Nio;
using Sharpen;

namespace Com.Khubla.Pdxreader.DB
{
	/// <author>tom</author>
	public class DBTableValue
	{
		/// <summary>the value</summary>
		private string value;

		/// <summary>the type</summary>
		private DBTableField.FieldType type;

		public virtual DBTableField.FieldType GetType()
		{
			return type;
		}

		public virtual string GetValue()
		{
			return value;
		}

		/// <summary>Read a table field</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		public virtual void Read(DBTableField pdxTableField, InputStream inputStream)
		{
			try
			{
				/*
				* get the data
				*/
				byte[] data = new byte[pdxTableField.GetLength()];
				int bytesRead = inputStream.Read(data);
				if (bytesRead > 0)
				{
					/*
					* convert to type
					*/
					DBTableField.FieldType fieldType = pdxTableField.GetFieldType();
					switch (fieldType)
					{
						case DBTableField.FieldType.A:
						{
							value = StringUtil.ReadString(data);
							break;
						}

						case DBTableField.FieldType.D:
						{
							// date
							long d = ByteBuffer.Wrap(data).Order(ByteOrder.LittleEndian).GetShort();
							value = System.Convert.ToString(d);
							break;
						}

						case DBTableField.FieldType.S:
						{
							long s = ByteBuffer.Wrap(data).Order(ByteOrder.LittleEndian).GetShort();
							value = System.Convert.ToString(s);
							break;
						}

						case DBTableField.FieldType.I:
						{
							data[0] = unchecked((byte)(data[0] & unchecked((int)(0x7f))));
							// handle unsigned integers
							long i = ByteBuffer.Wrap(data).Order(ByteOrder.BigEndian).GetInt();
							value = System.Convert.ToString(i);
							break;
						}

						case DBTableField.FieldType.C:
						{
							// currency
							double dollars = ByteBuffer.Wrap(data).Order(ByteOrder.BigEndian).GetDouble();
							value = double.ToString(dollars);
							break;
						}

						case DBTableField.FieldType.M:
						{
							// Memo
							value = StringUtil.ByteArrayToString(data);
							break;
						}

						case DBTableField.FieldType.N:
						{
							long n = ByteBuffer.Wrap(data).Order(ByteOrder.LittleEndian).GetLong();
							value = double.ToString(n);
							break;
						}

						case DBTableField.FieldType.L:
						{
							// Logical
							value = StringUtil.ByteArrayToString(data);
							break;
						}

						case DBTableField.FieldType.B:
						{
							// Binary
							value = StringUtil.ByteArrayToString(data);
							break;
						}

						case DBTableField.FieldType.O:
						{
							// OLE
							value = StringUtil.ByteArrayToString(data);
							break;
						}

						case DBTableField.FieldType.E:
						{
							// formatted memo
							value = StringUtil.ByteArrayToString(data);
							break;
						}

						case DBTableField.FieldType.G:
						{
							// Graphic
							value = StringUtil.ByteArrayToString(data);
							break;
						}

						case DBTableField.FieldType.Bcd:
						{
							// BCD
							value = StringUtil.ByteArrayToString(data);
							break;
						}

						case DBTableField.FieldType.Bytes:
						{
							// Bytes
							value = StringUtil.ByteArrayToString(data);
							break;
						}

						case DBTableField.FieldType.Ts:
						{
							// milliseconds since Jan 1, 1 AD, convert to UTC time
							data[0] = unchecked((byte)(data[0] & unchecked((int)(0x7f))));
							// handle unsigned number
							double dt = ByteBuffer.Wrap(data).Order(ByteOrder.BigEndian).GetDouble();
							long dateTime = (long)dt;
							if (dateTime == 0)
							{
								value = null;
							}
							else
							{
								dateTime -= 86400000;
								// millis in 1 day
								dateTime -= 62135607600000l;
								// millis from 01.01.1970
								value = System.Convert.ToString(dateTime);
							}
							break;
						}

						case DBTableField.FieldType.Auto:
						{
							short auto = ByteBuffer.Wrap(data).Order(ByteOrder.LittleEndian).GetShort();
							value = short.ToString(auto);
							break;
						}

						default:
						{
							throw new Exception("Unknown field type '" + fieldType.ToString() + "'");
						}
					}
				}
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in read", e);
			}
		}

		public virtual void SetType(DBTableField.FieldType type)
		{
			this.type = type;
		}

		public virtual void SetValue(string value)
		{
			this.value = value;
		}
	}
}
