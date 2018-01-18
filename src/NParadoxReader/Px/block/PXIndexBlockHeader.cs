using System;
using Com.Google.Common.IO;
using Com.Khubla.Pdxreader.Api;
using Sharpen;

namespace Com.Khubla.Pdxreader.PX.Block
{
	/// <author>tom</author>
	public class PXIndexBlockHeader
	{
		private int nextBlockNumber;

		private int previousBlockNumber;

		private int offsetLastRecord;

		public virtual int GetNextBlockNumber()
		{
			return nextBlockNumber;
		}

		public virtual int GetOffsetLastRecord()
		{
			return offsetLastRecord;
		}

		public virtual int GetPreviousBlockNumber()
		{
			return previousBlockNumber;
		}

		/// <summary>block header, 6 bytes</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		public virtual void Read(LittleEndianDataInputStream littleEndianDataInputStream)
		{
			try
			{
				nextBlockNumber = littleEndianDataInputStream.ReadUnsignedShort();
				previousBlockNumber = littleEndianDataInputStream.ReadUnsignedShort();
				offsetLastRecord = littleEndianDataInputStream.ReadUnsignedShort();
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in read", e);
			}
		}

		public virtual void SetNextBlockNumber(int nextBlockNumber)
		{
			this.nextBlockNumber = nextBlockNumber;
		}

		public virtual void SetOffsetLastRecord(int offsetLastRecord)
		{
			this.offsetLastRecord = offsetLastRecord;
		}

		public virtual void SetPreviousBlockNumber(int previousBlockNumber)
		{
			this.previousBlockNumber = previousBlockNumber;
		}
	}
}
