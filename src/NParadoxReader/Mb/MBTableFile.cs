using System;
using System.Collections.Generic;
using Com.Google.Common.IO;
using Com.Khubla.Pdxreader.Api;
using Java.IO;
using Sharpen;

namespace Com.Khubla.Pdxreader.MB
{
	/// <author>tom</author>
	public class MBTableFile
	{
		/// <summary>blocks</summary>
		private readonly IList<MBTableBlock> blocks = new List<MBTableBlock>();

		/// <summary>This method finds all the blocks with their types and offsets</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		private void PreReadBlocks(File file)
		{
			try
			{
				/*
				* set up streams
				*/
				BufferedInputStream bufferedInputStream = new BufferedInputStream(new FileInputStream
					(file));
				LittleEndianDataInputStream littleEndianDataInputStream = new LittleEndianDataInputStream
					(bufferedInputStream);
				try
				{
					/*
					* offset tracker
					*/
					int fileOffset = 0;
					/*
					* loop
					*/
					while (littleEndianDataInputStream.Available() > 1)
					{
						/*
						* read type
						*/
						int type = littleEndianDataInputStream.ReadByte();
						/*
						* get an appropriate Block type
						*/
						MBTableBlock.RecordType recordType = MBTableBlock.GetRecordType(type);
						/*
						* ok?
						*/
						if (null != recordType)
						{
							MBTableBlock mbTableBlock = MBTableBlockFactory.GetMBTableBlock(recordType);
							/*
							* set the offset
							*/
							mbTableBlock.SetFileOffset(fileOffset);
							/*
							* pre-read
							*/
							int bytesPreRead = mbTableBlock.PreRead(littleEndianDataInputStream);
							/*
							* add to list
							*/
							blocks.Add(mbTableBlock);
							/*
							* skip forward to next one
							*/
							int bytesToSkip = mbTableBlock.GetSizeofBlock() - (1 + bytesPreRead);
							long skipped = littleEndianDataInputStream.Skip(bytesToSkip);
							if (0 != skipped)
							{
								/*
								* update the offset
								*/
								fileOffset += mbTableBlock.GetSizeofBlock();
							}
						}
					}
				}
				finally
				{
					littleEndianDataInputStream.Close();
					bufferedInputStream.Close();
				}
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in read", e);
			}
		}

		/// <summary>read</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		public virtual void Read(File file)
		{
			try
			{
				/*
				* pre-read
				*/
				PreReadBlocks(file);
				/*
				* read
				*/
				ReadBlocks(file);
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in read", e);
			}
		}

		/// <summary>Read all blocks</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		private void ReadBlocks(File file)
		{
			try
			{
				foreach (MBTableBlock mbTableBlock in blocks)
				{
					/*
					* set up streams
					*/
					BufferedInputStream bufferedInputStream = new BufferedInputStream(new FileInputStream
						(file));
					LittleEndianDataInputStream littleEndianDataInputStream = new LittleEndianDataInputStream
						(bufferedInputStream);
					try
					{
						/*
						* ffd
						*/
						long skipped = littleEndianDataInputStream.Skip(mbTableBlock.GetFileOffset());
						if (0 != skipped)
						{
							/*
							* read
							*/
							mbTableBlock.Read(littleEndianDataInputStream);
						}
					}
					finally
					{
						littleEndianDataInputStream.Close();
						bufferedInputStream.Close();
					}
				}
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in read", e);
			}
		}
	}
}
