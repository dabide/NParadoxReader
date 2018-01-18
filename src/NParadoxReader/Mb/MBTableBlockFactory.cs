using Com.Khubla.Pdxreader.Api;
using Com.Khubla.Pdxreader.MB.Block;
using Sharpen;

namespace Com.Khubla.Pdxreader.MB
{
	public class MBTableBlockFactory
	{
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		public static MBTableBlock GetMBTableBlock(MBTableBlock.RecordType recordType)
		{
			switch (recordType)
			{
				case MBTableBlock.RecordType.header:
				{
					return new MBTableHeaderBlock();
				}

				case MBTableBlock.RecordType.single:
				{
					return new MBSingleBlock();
				}

				case MBTableBlock.RecordType.suballocated:
				{
					return new MBSuballocatedBlock();
				}

				case MBTableBlock.RecordType.free:
				{
					return new MBFreeBlock();
				}

				default:
				{
					throw new PDXReaderException("Unknown record type '" + recordType + "'");
				}
			}
		}
	}
}
