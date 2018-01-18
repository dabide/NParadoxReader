using Com.Google.Common.IO;
using Com.Khubla.Pdxreader.MB;
using Sharpen;

namespace Com.Khubla.Pdxreader.MB.Block
{
	public class MBSingleBlock : MBTableBlock
	{
		public MBSingleBlock()
			: base(MBTableBlock.RecordType.single)
		{
		}

		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		public override void Read(LittleEndianDataInputStream littleEndianDataInputStream
			)
		{
		}
		// sizeofBlock = littleEndianDataInputStream.readUnsignedShort();
		// modificationCount = littleEndianDataInputStream.readUnsignedShort();
	}
}
