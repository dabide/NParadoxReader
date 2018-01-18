using System.IO;
using Com.Google.Common.IO;
using Com.Khubla.Pdxreader.Api;
using Com.Khubla.Pdxreader.MB;
using Sharpen;

namespace Com.Khubla.Pdxreader.MB.Block
{
	public class MBSuballocatedBlock : MBTableBlock
	{
		public MBSuballocatedBlock()
			: base(MBTableBlock.RecordType.suballocated)
		{
		}

		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		public override void Read(LittleEndianDataInputStream littleEndianDataInputStream
			)
		{
			try
			{
				int blockType = littleEndianDataInputStream.ReadByte();
				if (blockType != base.recordType.GetValue())
				{
					throw new PDXReaderException("Block type mismatch");
				}
				int sizeofBlock = littleEndianDataInputStream.ReadUnsignedShort();
				if ((sizeofBlock * BlockSizeMultiplier) != base.sizeofBlock)
				{
					throw new PDXReaderException("Block type mismatch");
				}
			}
			catch (IOException e)
			{
				throw new PDXReaderException("Exception reading inputStream", e);
			}
		}
	}
}
