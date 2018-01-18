using System;
using Com.Google.Common.IO;
using Com.Khubla.Pdxreader.Api;
using Sharpen;

namespace Com.Khubla.Pdxreader.PX
{
	/// <author>tom</author>
	public class PXFileHeader
	{
		/// <summary>block size</summary>
		[System.Serializable]
		public sealed class BlockSize
		{
			public static readonly PXFileHeader.BlockSize oneK = new PXFileHeader.BlockSize(1
				);

			public static readonly PXFileHeader.BlockSize twoK = new PXFileHeader.BlockSize(2
				);

			public static readonly PXFileHeader.BlockSize threeK = new PXFileHeader.BlockSize
				(3);

			public static readonly PXFileHeader.BlockSize fourK = new PXFileHeader.BlockSize(
				4);

			private int value;

			private BlockSize(int value)
			{
				this.value = value;
			}

			public int GetValue()
			{
				return PXFileHeader.BlockSize.value;
			}

			public void SetValue(int value)
			{
				this.value = value;
			}
		}

		/// <summary>Block size</summary>
		private PXFileHeader.BlockSize blockSize;

		/// <summary>length of the header block (bytes)</summary>
		private int headerBlockSize;

		/// <summary>length of index record (-6)</summary>
		private int indexRecordLength;

		/// <summary>total records</summary>
		private long numberRecords;

		/// <summary>data block size code.</summary>
		/// <remarks>data block size code. 1 - 1k, 2 - 2k, 3 - 3k, 4 - 4k.</remarks>
		private int dataBlockSizeCode;

		private int blocksInUse;

		private int totalBlocksInFile;

		private int firstDataBlock;

		private int lastDataBlock;

		private int indexRootBlock;

		private int numberIndexLevels;

		private int numberIndexFields;

		public virtual int GetBlocksInUse()
		{
			return blocksInUse;
		}

		public virtual PXFileHeader.BlockSize GetBlockSize()
		{
			return blockSize;
		}

		public virtual int GetDataBlockSizeCode()
		{
			return dataBlockSizeCode;
		}

		public virtual int GetFirstDataBlock()
		{
			return firstDataBlock;
		}

		public virtual int GetHeaderBlockSize()
		{
			return headerBlockSize;
		}

		public virtual int GetIndexRecordLength()
		{
			return indexRecordLength;
		}

		public virtual int GetIndexRootBlock()
		{
			return indexRootBlock;
		}

		public virtual int GetLastDataBlock()
		{
			return lastDataBlock;
		}

		public virtual int GetNumberIndexFields()
		{
			return numberIndexFields;
		}

		public virtual int GetNumberIndexLevels()
		{
			return numberIndexLevels;
		}

		public virtual long GetNumberRecords()
		{
			return numberRecords;
		}

		public virtual int GetTotalBlocksInFile()
		{
			return totalBlocksInFile;
		}

		/// <summary>read (little endian)</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		public virtual void Read(LittleEndianDataInputStream littleEndianDataInputStream)
		{
			try
			{
				/*
				* The index record length is six greater than the sum of the lengths of the key fields.
				*/
				indexRecordLength = littleEndianDataInputStream.ReadUnsignedShort();
				headerBlockSize = littleEndianDataInputStream.ReadUnsignedShort();
				if (2048 != headerBlockSize)
				{
					throw new Exception("headerBlockSize was expected to be 2048, but '" + headerBlockSize
						 + "' was found");
				}
				int fileType = littleEndianDataInputStream.ReadUnsignedByte();
				if (1 != fileType)
				{
					throw new Exception("PX file type was expected to be 1, but '" + fileType + "' was found"
						);
				}
				dataBlockSizeCode = littleEndianDataInputStream.ReadUnsignedByte();
				if (1 == dataBlockSizeCode)
				{
					blockSize = PXFileHeader.BlockSize.oneK;
				}
				else
				{
					if (2 == dataBlockSizeCode)
					{
						blockSize = PXFileHeader.BlockSize.twoK;
					}
					else
					{
						if (3 == dataBlockSizeCode)
						{
							blockSize = PXFileHeader.BlockSize.threeK;
						}
						else
						{
							if (4 == dataBlockSizeCode)
							{
								blockSize = PXFileHeader.BlockSize.fourK;
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
				if (1 != firstDataBlock)
				{
					throw new Exception("firstDataBlock was expected to be 1, but '" + firstDataBlock
						 + "' was found");
				}
				lastDataBlock = littleEndianDataInputStream.ReadUnsignedShort();
				indexRootBlock = littleEndianDataInputStream.ReadUnsignedShort();
				numberIndexLevels = littleEndianDataInputStream.ReadUnsignedByte();
				/*
				* The number of fields in the index is the same as the number of key fields for the table.
				*/
				numberIndexFields = littleEndianDataInputStream.ReadUnsignedByte();
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in read", e);
			}
		}

		public virtual void SetBlocksInUse(int blocksInUse)
		{
			this.blocksInUse = blocksInUse;
		}

		public virtual void SetBlockSize(PXFileHeader.BlockSize blockSize)
		{
			this.blockSize = blockSize;
		}

		public virtual void SetDataBlockSizeCode(int dataBlockSizeCode)
		{
			this.dataBlockSizeCode = dataBlockSizeCode;
		}

		public virtual void SetFirstDataBlock(int firstDataBlock)
		{
			this.firstDataBlock = firstDataBlock;
		}

		public virtual void SetHeaderBlockSize(int headerBlockSize)
		{
			this.headerBlockSize = headerBlockSize;
		}

		public virtual void SetIndexRecordLength(int indexRecordLength)
		{
			this.indexRecordLength = indexRecordLength;
		}

		public virtual void SetIndexRootBlock(int indexRootBlock)
		{
			this.indexRootBlock = indexRootBlock;
		}

		public virtual void SetLastDataBlock(int lastDataBlock)
		{
			this.lastDataBlock = lastDataBlock;
		}

		public virtual void SetNumberIndexFields(int numberIndexFields)
		{
			this.numberIndexFields = numberIndexFields;
		}

		public virtual void SetNumberIndexLevels(int numberIndexLevels)
		{
			this.numberIndexLevels = numberIndexLevels;
		}

		public virtual void SetNumberRecords(long numberRecords)
		{
			this.numberRecords = numberRecords;
		}

		public virtual void SetTotalBlocksInFile(int totalBlocksInFile)
		{
			this.totalBlocksInFile = totalBlocksInFile;
		}
	}
}
