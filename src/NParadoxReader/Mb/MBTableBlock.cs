using System;
using Com.Google.Common.IO;
using Sharpen;

namespace Com.Khubla.Pdxreader.MB
{
	public abstract class MBTableBlock
	{
		/// <summary>record type</summary>
		[System.Serializable]
		public sealed class RecordType
		{
			public static readonly MBTableBlock.RecordType header = new MBTableBlock.RecordType
				(0);

			public static readonly MBTableBlock.RecordType single = new MBTableBlock.RecordType
				(2);

			public static readonly MBTableBlock.RecordType suballocated = new MBTableBlock.RecordType
				(3);

			public static readonly MBTableBlock.RecordType free = new MBTableBlock.RecordType
				(4);

			private int value;

			private RecordType(int value)
			{
				this.value = value;
			}

			public int GetValue()
			{
				return MBTableBlock.RecordType.value;
			}

			public void SetValue(int value)
			{
				this.value = value;
			}
		}

		protected internal const int BlockSizeMultiplier = 4096;

		/// <exception cref="System.Exception"/>
		public static MBTableBlock.RecordType GetRecordType(int type)
		{
			switch (type)
			{
				case -1:
				{
					// no idea what a -1 record type is...
					return null;
				}

				case 0:
				{
					return MBTableBlock.RecordType.header;
				}

				case 2:
				{
					return MBTableBlock.RecordType.single;
				}

				case 3:
				{
					return MBTableBlock.RecordType.suballocated;
				}

				case 4:
				{
					return MBTableBlock.RecordType.free;
				}

				default:
				{
					throw new Exception("Unknown record type '" + type + "'");
				}
			}
		}

		/// <summary>offset of this block from the start of the file</summary>
		protected internal int fileOffset;

		/// <summary>record type</summary>
		protected internal readonly MBTableBlock.RecordType recordType;

		/// <summary>sizeofBlock</summary>
		protected internal int sizeofBlock;

		public MBTableBlock(MBTableBlock.RecordType recordType)
		{
			// Size of block divided by 4k (1 because the header is 4k)
			this.recordType = recordType;
		}

		public virtual int GetFileOffset()
		{
			return fileOffset;
		}

		public virtual MBTableBlock.RecordType GetRecordType()
		{
			return recordType;
		}

		public virtual int GetSizeofBlock()
		{
			return sizeofBlock;
		}

		/// <exception cref="System.IO.IOException"/>
		public virtual int PreRead(LittleEndianDataInputStream littleEndianDataInputStream
			)
		{
			sizeofBlock = BlockSizeMultiplier * littleEndianDataInputStream.ReadUnsignedShort
				();
			return 2;
		}

		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		public abstract void Read(LittleEndianDataInputStream littleEndianDataInputStream
			);

		public virtual void SetFileOffset(int fileOffset)
		{
			this.fileOffset = fileOffset;
		}

		public virtual void SetSizeofBlock(int sizeofBlock)
		{
			this.sizeofBlock = sizeofBlock;
		}
	}
}
