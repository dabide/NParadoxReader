using System;
using System.Collections.Generic;
using Com.Google.Common.IO;
using Com.Khubla.Pdxreader.Api;
using Com.Khubla.Pdxreader.PX.Block;
using Java.IO;
using Sharpen;

namespace Com.Khubla.Pdxreader.PX
{
	/// <author>tom</author>
	public class PXFile
	{
		/// <summary>max header size</summary>
		private const int MaxHeaderSize = 10240;

		/// <summary>max block size</summary>
		private const int MaxBlockSize = 10240;

		/// <summary>header</summary>
		private PXFileHeader pxFileHeader;

		/// <summary>blocks</summary>
		private IList<PXIndexBlock> blocks;

		/// <summary>read</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		public virtual void Read(File file)
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
					* mark and read the headers
					*/
					bufferedInputStream.Mark(MaxHeaderSize);
					ReadHeaders(littleEndianDataInputStream);
					/*
					* read the block data
					*/
					bufferedInputStream.Reset();
					ReadBlocks(bufferedInputStream);
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

		/// <summary>read block data</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		private void ReadBlocks(BufferedInputStream bufferedInputStream)
		{
			try
			{
				/*
				* init the array
				*/
				blocks = new List<PXIndexBlock>();
				/*
				* skip to the first index block
				*/
				int nSkip = pxFileHeader.GetBlockSize().GetValue() * 1024;
				if (nSkip == bufferedInputStream.Skip(nSkip))
				{
					/*
					* walk index blocks
					*/
					int blocksInUse = pxFileHeader.GetBlocksInUse();
					for (int i = 0; i < blocksInUse; i++)
					{
						/*
						* block
						*/
						PXIndexBlock pxIndexBlock = new PXIndexBlock();
						/*
						* mark at the start of the block
						*/
						bufferedInputStream.Mark(MaxBlockSize);
						/*
						* read the block data
						*/
						pxIndexBlock.Read(bufferedInputStream);
						/*
						* store it. blocks are numbered from 1, not from 0.
						*/
						blocks.Add(pxIndexBlock);
						/*
						* reset to the start of the block
						*/
						bufferedInputStream.Reset();
						/*
						* skip ahead to next block
						*/
						bufferedInputStream.Skip(pxFileHeader.GetBlockSize().GetValue() * 1024);
					}
				}
				else
				{
					throw new PDXReaderException("File format exception");
				}
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in readBlocks", e);
			}
		}

		/// <summary>read</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		private void ReadHeaders(LittleEndianDataInputStream littleEndianDataInputStream)
		{
			try
			{
				/*
				* read header
				*/
				pxFileHeader = new PXFileHeader();
				pxFileHeader.Read(littleEndianDataInputStream);
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in readHeaders", e);
			}
		}
	}
}
