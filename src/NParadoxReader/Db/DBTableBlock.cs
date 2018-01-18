using System;
using System.Collections.Generic;
using System.IO;
using Com.Khubla.Pdxreader.Api;

namespace Com.Khubla.Pdxreader.DB
{
	/// <author>tom</author>
	public class DBTableBlock
	{
		/// <summary>header</summary>
		private DBTableBlockHeader dbTableBlockHeader;

		/// <summary>block number</summary>
		private readonly int blockNumber;

		/// <summary>fields</summary>
		private readonly IList<DBTableField> fields;

		/// <summary>records</summary>
		private IList<DBTableRecord> records;

		/// <summary>records per block</summary>
		private readonly int recordsPerBlock;

		public DBTableBlock(int blockNumber, int recordsPerBlock, IList<DBTableField> fields
			)
		{
			this.blockNumber = blockNumber;
			this.fields = fields;
			this.recordsPerBlock = recordsPerBlock;
		}

		public virtual int GetBlockNumber()
		{
			return blockNumber;
		}

		public virtual DBTableBlockHeader GetDbTableBlockHeader()
		{
			return dbTableBlockHeader;
		}

		public virtual IList<DBTableField> GetFields()
		{
			return fields;
		}

		public virtual DBTableBlockHeader GetPdxFileBlockHeader()
		{
			return dbTableBlockHeader;
		}

		public virtual IList<DBTableRecord> GetRecords()
		{
			return records;
		}

		public virtual int GetRecordsPerBlock()
		{
			return recordsPerBlock;
		}

		/// <summary>read data.</summary>
		/// <remarks>read data. This assumes that the inputStream is on byte 0 from the start of the block
		/// 	</remarks>
		/// <exception cref="Com.Khubla.Pdxreader.Api.PDXReaderException"/>
		public virtual void Read(PDXReaderListener pdxReaderListener, Stream inputStream
			)
		{
			try
			{
				records = new List<DBTableRecord>();
				/*
				* read the header
				*/
				LittleEndianDataInputStream littleEndianDataInputStream = new LittleEndianDataInputStream
					(inputStream);
				ReadHeader(littleEndianDataInputStream);
				/*
				* read the records
				*/
				for (int i = 0; i < recordsPerBlock; i++)
				{
					DBTableRecord pdxTableRecord = new DBTableRecord();
					pdxTableRecord.Read(pdxReaderListener, fields, inputStream);
					records.Add(pdxTableRecord);
				}
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
				dbTableBlockHeader = new DBTableBlockHeader();
				dbTableBlockHeader.Read(littleEndianDataInputStream);
			}
			catch (Exception e)
			{
				throw new PDXReaderException("Exception in readHeader", e);
			}
		}

		public virtual void SetDbTableBlockHeader(DBTableBlockHeader dbTableBlockHeader)
		{
			this.dbTableBlockHeader = dbTableBlockHeader;
		}

		public virtual void SetPdxFileBlockHeader(DBTableBlockHeader pdxFileBlockHeader)
		{
			dbTableBlockHeader = pdxFileBlockHeader;
		}

		public virtual void SetRecords(IList<DBTableRecord> records)
		{
			this.records = records;
		}
	}
}
