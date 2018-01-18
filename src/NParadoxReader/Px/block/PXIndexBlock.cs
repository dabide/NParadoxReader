using System;
using System.Collections.Generic;
using Com.Google.Common.IO;
using Com.Khubla.Pdxreader.Api;
using Java.IO;
using Sharpen;

namespace Com.Khubla.Pdxreader.PX.Block
{
	/// <author>tom</author>
	public class PXIndexBlock
	{
		/// <summary>header</summary>
		private PXIndexBlockHeader pxFileBlockHeader;

		/// <summary>Index Records</summary>
		private IList<PXIndexBlockRecord> indexRecords;

		/// <summary>read data.</summary>
		/// <remarks>read data. This assumes that the inputStream is on byte 0 from the start of the block
		/// 	</remarks>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		public virtual void Read(InputStream inputStream)
		{
			try
			{
				/*
				* read the header
				*/
				LittleEndianDataInputStream littleEndianDataInputStream = new LittleEndianDataInputStream
					(inputStream);
				ReadHeader(littleEndianDataInputStream);
				/*
				* read the records
				*/
				indexRecords = new List<PXIndexBlockRecord>();
				PXIndexBlockRecord pxFileIndexRecord = new PXIndexBlockRecord();
				pxFileIndexRecord.Read(littleEndianDataInputStream);
				indexRecords.Add(pxFileIndexRecord);
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in read", e);
			}
		}

		/// <summary>read header</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		private void ReadHeader(LittleEndianDataInputStream littleEndianDataInputStream)
		{
			try
			{
				pxFileBlockHeader = new PXIndexBlockHeader();
				pxFileBlockHeader.Read(littleEndianDataInputStream);
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in readHeader", e);
			}
		}
	}
}
