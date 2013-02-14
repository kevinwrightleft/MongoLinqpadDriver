using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoLinqTest.MongoEntity
{
	public class InnerData
	{
		public int IntData { get; set; }
		public string StringData { get; set; }
		public List<bool> BoolList { get; set; }
	}

	public class TestData
	{
		public ObjectId Id { get; set; }
		public DateTime Timestamp { get; set; }
		public string StringData { get; set; }
		public bool BoolData { get; set; }
		public int IntData { get; set; }
		public long LongData { get; set; }
		public decimal DecimalData { get; set; }
		public double DoubleData { get; set; }
		public InnerData InnerData { get; set; }

		public BsonDocument DynamicProps { get; set; }

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine("----------------------------------------------");
			sb.AppendLine(String.Format("Id : {0}", Id));
			sb.AppendLine(String.Format("Timestamp : {0}", Timestamp));
			sb.AppendLine(String.Format("StringData : {0}", StringData));
			sb.AppendLine(String.Format("BoolData : {0}", BoolData));
			sb.AppendLine(String.Format("IntData : {0}", IntData));
			sb.AppendLine(String.Format("LongData : {0}", LongData));
			sb.AppendLine(String.Format("DecimalData : {0}", DecimalData));
			sb.AppendLine(String.Format("DoubleData : {0}", DoubleData));
			sb.AppendLine(String.Format("InnerData.IntData : {0}", InnerData.IntData));
			sb.AppendLine(String.Format("InnerData.StringData : {0}", InnerData.StringData));
			for (var i = 0; i < InnerData.BoolList.Count; i++)
			{
				sb.AppendLine(String.Format("InnerData.BoolList({0}) : {1}", i, InnerData.BoolList[i]));

			}
            sb.AppendLine(DynamicProps.ToString());
			sb.AppendLine("----------------------------------------------");

			return sb.ToString();
		}
	}

}
