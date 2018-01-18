using System;
using Com.Google.Common.IO;
using Com.Khubla.Pdxreader.Api;
using Com.Khubla.Pdxreader.Util;
using Sharpen;

namespace Com.Khubla.Pdxreader.DB
{
	/// <author>tom</author>
	public class DBTableField
	{
		/// <summary>field type</summary>
		[System.Serializable]
		public sealed class FieldType
		{
			/// <summary>
			/// <pre>
			/// A - Alpha, length 255
			/// D - Date, length 4
			/// S - Short Integer, length 2
			/// I - Long Integer, length 4
			/// C - Currency, length 8
			/// N - Number, length 8
			/// L - Logical, length 1
			/// M - Memo, variable length
			/// B - Binary, variable length
			/// E - Formatting memo, variable length
			/// O - OLE, variable length
			/// G - Graphic Blob, variable length
			/// T - Time, length 4
			/// TS - TimeStamp, length 8
			/// Auto - AutoIncrement
			/// BCD - BCD, length 17
			/// Bytes - Bytes, variable length
			/// </pre>
			/// </summary>
			public static readonly DBTableField.FieldType A = new DBTableField.FieldType(1);

			public static readonly DBTableField.FieldType D = new DBTableField.FieldType(2);

			public static readonly DBTableField.FieldType S = new DBTableField.FieldType(3);

			public static readonly DBTableField.FieldType I = new DBTableField.FieldType(4);

			public static readonly DBTableField.FieldType C = new DBTableField.FieldType(5);

			public static readonly DBTableField.FieldType N = new DBTableField.FieldType(6);

			public static readonly DBTableField.FieldType L = new DBTableField.FieldType(9);

			public static readonly DBTableField.FieldType M = new DBTableField.FieldType(unchecked(
				(int)(0x0c)));

			public static readonly DBTableField.FieldType B = new DBTableField.FieldType(unchecked(
				(int)(0x0d)));

			public static readonly DBTableField.FieldType E = new DBTableField.FieldType(unchecked(
				(int)(0x0e)));

			public static readonly DBTableField.FieldType O = new DBTableField.FieldType(unchecked(
				(int)(0x0f)));

			public static readonly DBTableField.FieldType G = new DBTableField.FieldType(unchecked(
				(int)(0x10)));

			public static readonly DBTableField.FieldType T = new DBTableField.FieldType(unchecked(
				(int)(0x14)));

			public static readonly DBTableField.FieldType Ts = new DBTableField.FieldType(unchecked(
				(int)(0x15)));

			public static readonly DBTableField.FieldType Auto = new DBTableField.FieldType(unchecked(
				(int)(0x16)));

			public static readonly DBTableField.FieldType Bcd = new DBTableField.FieldType(unchecked(
				(int)(0x17)));

			public static readonly DBTableField.FieldType Bytes = new DBTableField.FieldType(
				unchecked((int)(0x18)));

			private int value;

			private FieldType(int value)
			{
				this.value = value;
			}

			public int GetValue()
			{
				return DBTableField.FieldType.value;
			}

			public void SetValue(int value)
			{
				this.value = value;
			}
		}

		/// <summary>field type</summary>
		private DBTableField.FieldType fieldType;

		/// <summary>field type</summary>
		private int type;

		/// <summary>field length</summary>
		private int length;

		/// <summary>name</summary>
		private string name;

		public virtual DBTableField.FieldType GetFieldType()
		{
			return fieldType;
		}

		public virtual int GetLength()
		{
			return length;
		}

		public virtual string GetName()
		{
			return name;
		}

		public virtual int GetType()
		{
			return type;
		}

		/// <summary>names</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		public virtual void ReadFieldName(LittleEndianDataInputStream littleEndianDataInputStream
			)
		{
			try
			{
				name = StringUtil.ReadString(littleEndianDataInputStream);
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in read", e);
			}
		}

		/// <summary>types and sizes, 2 bytes per field</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		public virtual bool ReadFieldTypeAndSize(LittleEndianDataInputStream littleEndianDataInputStream
			)
		{
			try
			{
				type = littleEndianDataInputStream.ReadUnsignedByte();
				length = littleEndianDataInputStream.ReadUnsignedByte();
				switch (type)
				{
					case unchecked((int)(0x00)):
					{
						return false;
					}

					case unchecked((int)(0x01)):
					{
						fieldType = DBTableField.FieldType.A;
						break;
					}

					case unchecked((int)(0x02)):
					{
						fieldType = DBTableField.FieldType.D;
						if (length != 4)
						{
							throw new Exception("Invalid field length '" + length + "' for type '" + type + "'"
								);
						}
						break;
					}

					case unchecked((int)(0x03)):
					{
						fieldType = DBTableField.FieldType.S;
						if (length != 2)
						{
							throw new Exception("Invalid field length '" + length + "' for type '" + type + "'"
								);
						}
						break;
					}

					case unchecked((int)(0x04)):
					{
						fieldType = DBTableField.FieldType.I;
						if (length != 4)
						{
							throw new Exception("Invalid field length '" + length + "' for type '" + type + "'"
								);
						}
						break;
					}

					case unchecked((int)(0x05)):
					{
						fieldType = DBTableField.FieldType.C;
						if (length != 8)
						{
							throw new Exception("Invalid field length '" + length + "' for type '" + type + "'"
								);
						}
						break;
					}

					case unchecked((int)(0x06)):
					{
						fieldType = DBTableField.FieldType.N;
						if (length != 8)
						{
							throw new Exception("Invalid field length '" + length + "' for type '" + type + "'"
								);
						}
						break;
					}

					case unchecked((int)(0x09)):
					{
						fieldType = DBTableField.FieldType.L;
						if (length != 1)
						{
							throw new Exception("Invalid field length '" + length + "' for type '" + type + "'"
								);
						}
						break;
					}

					case unchecked((int)(0xc)):
					{
						fieldType = DBTableField.FieldType.M;
						break;
					}

					case unchecked((int)(0xd)):
					{
						fieldType = DBTableField.FieldType.B;
						break;
					}

					case unchecked((int)(0xe)):
					{
						fieldType = DBTableField.FieldType.E;
						break;
					}

					case unchecked((int)(0xf)):
					{
						fieldType = DBTableField.FieldType.O;
						break;
					}

					case unchecked((int)(0x10)):
					{
						fieldType = DBTableField.FieldType.G;
						break;
					}

					case unchecked((int)(0x14)):
					{
						fieldType = DBTableField.FieldType.T;
						if (length != 4)
						{
							throw new Exception("Invalid field length '" + length + "' for type '" + type + "'"
								);
						}
						break;
					}

					case unchecked((int)(0x15)):
					{
						fieldType = DBTableField.FieldType.Ts;
						if (length != 8)
						{
							throw new Exception("Invalid field length '" + length + "' for type '" + type + "'"
								);
						}
						break;
					}

					case unchecked((int)(0x16)):
					{
						fieldType = DBTableField.FieldType.Auto;
						break;
					}

					case unchecked((int)(0x17)):
					{
						fieldType = DBTableField.FieldType.Bcd;
						if (length != 17)
						{
							throw new Exception("Invalid field length '" + length + "' for type '" + type + "'"
								);
						}
						break;
					}

					case unchecked((int)(0x18)):
					{
						fieldType = DBTableField.FieldType.Bytes;
						break;
					}

					default:
					{
						throw new PDXReaderException("Unknown field type '" + type + "'");
					}
				}
				return true;
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in read", e);
			}
		}

		public virtual void SetFieldType(DBTableField.FieldType fieldType)
		{
			this.fieldType = fieldType;
		}

		public virtual void SetLength(int length)
		{
			this.length = length;
		}

		public virtual void SetName(string name)
		{
			this.name = name;
		}

		public virtual void SetType(int type)
		{
			this.type = type;
		}
	}
}
