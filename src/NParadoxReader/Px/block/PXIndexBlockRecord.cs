using System;
using Com.Google.Common.IO;
using Com.Khubla.Pdxreader.Api;
using Sharpen;

namespace Com.Khubla.Pdxreader.PX.Block
{
	public class PXIndexBlockRecord
	{
		private int blockNumberForKey;

		private int statistics;

		private int unknown;

		public virtual int GetBlockNumberForKey()
		{
			return blockNumberForKey;
		}

		public virtual int GetStatistics()
		{
			return statistics;
		}

		public virtual int GetUnknown()
		{
			return unknown;
		}

		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		public virtual void Read(LittleEndianDataInputStream littleEndianDataInputStream)
		{
			try
			{
				blockNumberForKey = littleEndianDataInputStream.ReadInt();
				statistics = littleEndianDataInputStream.ReadInt();
				unknown = littleEndianDataInputStream.ReadInt();
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in read", e);
			}
		}

		public virtual void SetBlockNumberForKey(int blockNumberForKey)
		{
			this.blockNumberForKey = blockNumberForKey;
		}

		public virtual void SetStatistics(int statistics)
		{
			this.statistics = statistics;
		}

		public virtual void SetUnknown(int unknown)
		{
			this.unknown = unknown;
		}
	}
}
