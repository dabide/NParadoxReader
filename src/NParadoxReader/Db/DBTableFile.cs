using System;
using Com.Google.Common.IO;
using Com.Khubla.Pdxreader.Api;
using Java.IO;
using Java.Util;
using Sharpen;

namespace Com.Khubla.Pdxreader.DB
{
	/// <author>tom</author>
	public class DBTableFile
	{
		/// <summary>max header size</summary>
		private const int MaxHeaderSize = 10240;

		/// <summary>max block size</summary>
		private const int MaxBlockSize = 10240;

		/// <summary>header</summary>
		private DBTableHeader dbTableHeader;

		/// <summary>blocks</summary>
		private Hashtable<int, DBTableBlock> blocks;

		public virtual Hashtable<int, DBTableBlock> GetBlocks()
		{
			return blocks;
		}

		public virtual DBTableHeader GetDbTableHeader()
		{
			return dbTableHeader;
		}

		/// <summary>read</summary>
		/// <exception cref="System.Exception"/>
		public virtual void Read(File file, PDXReaderListener pdxReaderListener)
		{
			try
			{
				/*
				* start
				*/
				pdxReaderListener.Start();
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
					* call the api
					*/
					pdxReaderListener.Header(dbTableHeader);
					/*
					* read the block data
					*/
					bufferedInputStream.Reset();
					ReadBlocks(bufferedInputStream, pdxReaderListener);
					/*
					* done
					*/
					pdxReaderListener.Finish();
				}
				finally
				{
					littleEndianDataInputStream.Close();
					bufferedInputStream.Close();
				}
			}
			catch (Exception e)
			{
				throw new Exception("Exception in read", e);
			}
		}

		/// <summary>read block data</summary>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		private void ReadBlocks(BufferedInputStream bufferedInputStream, PDXReaderListener
			 pdxReaderListener)
		{
			try
			{
				/*
				* init the array
				*/
				blocks = new Hashtable<int, DBTableBlock>();
				/*
				* skip to the first block
				*/
				int nSkip = dbTableHeader.GetBlockSize().GetValue() * 1024;
				if (nSkip == bufferedInputStream.Skip(nSkip))
				{
					/*
					* walk blocks
					*/
					int blocksInUse = dbTableHeader.GetBlocksInUse();
					for (int i = 0; i < blocksInUse; i++)
					{
						/*
						* block
						*/
						DBTableBlock pdxTableBlock = new DBTableBlock(i + 1, dbTableHeader.CalculateRecordsPerBlock
							(), dbTableHeader.GetFields());
						/*
						* mark at the start of the block
						*/
						bufferedInputStream.Mark(MaxBlockSize);
						/*
						* read the block data
						*/
						pdxTableBlock.Read(pdxReaderListener, bufferedInputStream);
						/*
						* store it. blocks are numbered from 1, not from 0.
						*/
						blocks[pdxTableBlock.GetBlockNumber()] = pdxTableBlock;
						/*
						* reset to the start of the block
						*/
						bufferedInputStream.Reset();
						/*
						* skip ahead to next block
						*/
						bufferedInputStream.Skip(dbTableHeader.GetBlockSize().GetValue() * 1024);
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
				dbTableHeader = new DBTableHeader();
				dbTableHeader.Read(littleEndianDataInputStream);
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in readHeaders", e);
			}
		}

		public virtual void SetBlocks(Hashtable<int, DBTableBlock> blocks)
		{
			this.blocks = blocks;
		}

		public virtual void SetDbTableHeader(DBTableHeader dbTableHeader)
		{
			this.dbTableHeader = dbTableHeader;
		}
	}
}
