using System;
using System.Collections.Generic;
using Com.Google.Common.IO;
using Com.Khubla.Pdxreader.Api;
using Com.Khubla.Pdxreader.Util;
using Sharpen;

namespace Com.Khubla.Pdxreader.DB
{
	/// <author>tom</author>
	public class DBTableHeader
	{
		/// <summary>block size</summary>
		[System.Serializable]
		public sealed class BlockSize
		{
			public static readonly DBTableHeader.BlockSize oneK = new DBTableHeader.BlockSize
				(1);

			public static readonly DBTableHeader.BlockSize twoK = new DBTableHeader.BlockSize
				(2);

			public static readonly DBTableHeader.BlockSize threeK = new DBTableHeader.BlockSize
				(3);

			public static readonly DBTableHeader.BlockSize fourK = new DBTableHeader.BlockSize
				(4);

			private int value;

			private BlockSize(int value)
			{
				this.value = value;
			}

			public int GetValue()
			{
				return DBTableHeader.BlockSize.value;
			}

			public void SetValue(int value)
			{
				this.value = value;
			}
		}

		/// <summary>table type</summary>
		[System.Serializable]
		public sealed class TableType
		{
			public static readonly DBTableHeader.TableType keyed = new DBTableHeader.TableType
				(0);

			public static readonly DBTableHeader.TableType unkeyed = new DBTableHeader.TableType
				(2);

			private int value;

			private TableType(int value)
			{
				this.value = value;
			}

			public int GetValue()
			{
				return DBTableHeader.TableType.value;
			}

			public void SetValue(int value)
			{
				this.value = value;
			}
		}

		/// <summary>Block size</summary>
		private DBTableHeader.BlockSize blockSize;

		/// <summary>table type</summary>
		private DBTableHeader.TableType tableType;

		/// <summary>size of single record</summary>
		private int recordBufferSize;

		/// <summary>length of the header block (bytes)</summary>
		private int headerBlockSize;

		/// <summary>total records</summary>
		private long numberRecords;

		/// <summary>number fields</summary>
		private int numberFields;

		/// <summary>number key fields</summary>
		private int numberKeyFields;

		/// <summary>data block size code.</summary>
		/// <remarks>data block size code. 1 - 1k, 2 - 2k, 3 - 3k, 4 - 4k.</remarks>
		private int dataBlockSizeCode;

		private int blocksInUse;

		private int totalBlocksInFile;

		private int firstDataBlock;

		private int lastDataBlock;

		private int firstFreeBlock;

		/// <summary>filename.</summary>
		/// <remarks>filename. There is a filename embedded in the files. I'm not sure what it does, but I do read it.
		/// 	</remarks>
		private string embeddedFilename;

		/// <summary>fields</summary>
		private IList<DBTableField> fields;

		/// <summary>figure out the total records in a block</summary>
		public virtual int CalculateRecordsPerBlock()
		{
			return (blockSize.value * 1024) / recordBufferSize;
		}

		public virtual int GetBlocksInUse()
		{
			return blocksInUse;
		}

		public virtual DBTableHeader.BlockSize GetBlockSize()
		{
			return blockSize;
		}

		public virtual int GetDataBlockSizeCode()
		{
			return dataBlockSizeCode;
		}

		public virtual string GetEmbeddedFilename()
		{
			return embeddedFilename;
		}

		public virtual IList<DBTableField> GetFields()
		{
			return fields;
		}

		public virtual int GetFirstDataBlock()
		{
			return firstDataBlock;
		}

		public virtual int GetFirstFreeBlock()
		{
			return firstFreeBlock;
		}

		public virtual int GetHeaderBlockSize()
		{
			return headerBlockSize;
		}

		public virtual int GetLastDataBlock()
		{
			return lastDataBlock;
		}

		public virtual int GetNumberFields()
		{
			return numberFields;
		}

		public virtual int GetNumberKeyFields()
		{
			return numberKeyFields;
		}

		public virtual long GetNumberRecords()
		{
			return numberRecords;
		}

		public virtual int GetRecordBufferSize()
		{
			return recordBufferSize;
		}

		public virtual DBTableHeader.TableType GetTableType()
		{
			return tableType;
		}

		public virtual int GetTotalBlocksInFile()
		{
			return totalBlocksInFile;
		}

		/// <summary>read (little endian)</summary>
		/// <exception cref="System.Exception"/>
		public virtual void Read(LittleEndianDataInputStream littleEndianDataInputStream)
		{
			try
			{
				recordBufferSize = littleEndianDataInputStream.ReadUnsignedShort();
				headerBlockSize = littleEndianDataInputStream.ReadUnsignedShort();
				int tableType = littleEndianDataInputStream.ReadUnsignedByte();
				if (0 == tableType)
				{
					this.tableType = DBTableHeader.TableType.keyed;
				}
				else
				{
					if (2 == tableType)
					{
						this.tableType = DBTableHeader.TableType.unkeyed;
					}
					else
					{
						throw new Exception("Unknown table type '" + tableType + "'");
					}
				}
				dataBlockSizeCode = littleEndianDataInputStream.ReadUnsignedByte();
				if (1 == dataBlockSizeCode)
				{
					blockSize = DBTableHeader.BlockSize.oneK;
				}
				else
				{
					if (2 == dataBlockSizeCode)
					{
						blockSize = DBTableHeader.BlockSize.twoK;
					}
					else
					{
						if (3 == dataBlockSizeCode)
						{
							blockSize = DBTableHeader.BlockSize.threeK;
						}
						else
						{
							if (4 == dataBlockSizeCode)
							{
								blockSize = DBTableHeader.BlockSize.fourK;
							}
							else
							{
								throw new Exception("Unknown block size code '" + dataBlockSizeCode + "'");
							}
						}
					}
				}
				numberRecords = littleEndianDataInputStream.ReadInt();
				blocksInUse = littleEndianDataInputStream.ReadUnsignedShort();
				totalBlocksInFile = littleEndianDataInputStream.ReadUnsignedShort();
				firstDataBlock = littleEndianDataInputStream.ReadUnsignedShort();
				lastDataBlock = littleEndianDataInputStream.ReadUnsignedShort();
				littleEndianDataInputStream.SkipBytes(15);
				// byte 0x21
				numberFields = littleEndianDataInputStream.ReadUnsignedByte();
				// byte 0x22
				littleEndianDataInputStream.SkipBytes(1);
				// byte 0x23
				numberKeyFields = littleEndianDataInputStream.ReadUnsignedByte();
				littleEndianDataInputStream.SkipBytes(unchecked((int)(0x54)));
				// byte 0x78
				ReadFieldTypesAndSizes(littleEndianDataInputStream);
				// name
				littleEndianDataInputStream.SkipBytes(20);
				embeddedFilename = StringUtil.ReadString(littleEndianDataInputStream);
				/*
				* skip forward 250 bytes
				*/
				int skipBytes = 250;
				// dumpBytes(skipBytes, littleEndianDataInputStream);
				littleEndianDataInputStream.SkipBytes(skipBytes);
				ReadFieldNames(littleEndianDataInputStream);
			}
			catch (Exception e)
			{
				throw new Exception("Exception in read", e);
			}
		}

		/// <summary>read the field descriptions</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		private void ReadFieldNames(LittleEndianDataInputStream littleEndianDataInputStream
			)
		{
			try
			{
				foreach (DBTableField pdxTableField in fields)
				{
					pdxTableField.ReadFieldName(littleEndianDataInputStream);
				}
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in readFields", e);
			}
		}

		/// <summary>read the field descriptions</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		private void ReadFieldTypesAndSizes(LittleEndianDataInputStream littleEndianDataInputStream
			)
		{
			try
			{
				fields = new List<DBTableField>();
				for (int i = 0; i < numberFields; i++)
				{
					DBTableField pdxTableField = new DBTableField();
					if (pdxTableField.ReadFieldTypeAndSize(littleEndianDataInputStream))
					{
						fields.Add(pdxTableField);
					}
				}
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in readFields", e);
			}
		}

		public virtual void Report()
		{
			foreach (DBTableField pdxTableField in fields)
			{
				System.Console.Out.WriteLine("Field '" + pdxTableField.GetName() + "' type '" + pdxTableField
					.GetFieldType().ToString() + "'");
			}
		}

		public virtual void SetBlocksInUse(int blocksInUse)
		{
			this.blocksInUse = blocksInUse;
		}

		public virtual void SetBlockSize(DBTableHeader.BlockSize blockSize)
		{
			this.blockSize = blockSize;
		}

		public virtual void SetDataBlockSizeCode(int dataBlockSizeCode)
		{
			this.dataBlockSizeCode = dataBlockSizeCode;
		}

		public virtual void SetEmbeddedFilename(string embeddedFilename)
		{
			this.embeddedFilename = embeddedFilename;
		}

		public virtual void SetFields(IList<DBTableField> fields)
		{
			this.fields = fields;
		}

		public virtual void SetFirstDataBlock(int firstDataBlock)
		{
			this.firstDataBlock = firstDataBlock;
		}

		public virtual void SetFirstFreeBlock(int firstFreeBlock)
		{
			this.firstFreeBlock = firstFreeBlock;
		}

		public virtual void SetHeaderBlockSize(int headerBlockSize)
		{
			this.headerBlockSize = headerBlockSize;
		}

		public virtual void SetLastDataBlock(int lastDataBlock)
		{
			this.lastDataBlock = lastDataBlock;
		}

		public virtual void SetNumberFields(int numberFields)
		{
			this.numberFields = numberFields;
		}

		public virtual void SetNumberKeyFields(int numberKeyFields)
		{
			this.numberKeyFields = numberKeyFields;
		}

		public virtual void SetNumberRecords(long numberRecords)
		{
			this.numberRecords = numberRecords;
		}

		public virtual void SetRecordBufferSize(int recordBufferSize)
		{
			this.recordBufferSize = recordBufferSize;
		}

		public virtual void SetTableType(DBTableHeader.TableType tableType)
		{
			this.tableType = tableType;
		}

		public virtual void SetTotalBlocksInFile(int totalBlocksInFile)
		{
			this.totalBlocksInFile = totalBlocksInFile;
		}
	}
}
