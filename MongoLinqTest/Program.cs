using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoLinqTest.MongoEntity;
using MongoUtils;
using MongoDB.Driver.Builders;
using LINQPad.Extensibility.DataContext;

namespace MongoLinqTest
{

	public static class Extensions
	{
		public static string MongoQueryStr(this object o)
		{
			var mongoQuery = GetMongoQuery(o);
			var retVal = mongoQuery.ToString();
			return retVal;
		}

		private static IMongoQuery GetMongoQuery(object o)
		{
			var myType = o.GetType();
			var methodInfo = myType.GetMethod("GetMongoQuery");
			var retVal = methodInfo.Invoke(o, null);
			return (IMongoQuery)retVal;
		}
	}

	class Program
	{
		private static string _connectionString = "mongodb://localhost:27017/common";

		static void Main(string[] args)
		{
			AddTestData();
			//Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
			//QueryTest();
		}


		static void AddTestData()
		{
			var session = new MongoSession(_connectionString) as IMongoSession;
			var db = session.Server.GetDatabase("common");
			var col = db.GetCollection<TestData>("TestData");

			var t = new TestData
			{
				BoolData = true,
				DecimalData = 3.50m,
				DoubleData = 5.50,
				IntData = 7,
				LongData = 9,
				StringData = "one",
				Timestamp = DateTime.Parse("2012-3-1"),
				InnerData = new InnerData
				{
					IntData = 2,
					StringData = "a",
					BoolList = new List<bool>() { true, false }
				},
                DynamicProps = new BsonDocument().Add("Name", "John").Add("Age", 23)
			};

			col.Insert<TestData>(t);

			t = new TestData
			{
				BoolData = true,
				DecimalData = 4.50m,
				DoubleData = 6.50,
				IntData = 8,
				LongData = 10,
				StringData = "one",
				Timestamp = DateTime.Parse("2012-3-1"),
				InnerData = new InnerData
				{
					IntData = 3,
					StringData = "b",
					BoolList = new List<bool>() { true, true }
				}
			};

			col.Insert<TestData>(t);

			t = new TestData
			{
				BoolData = false,
				DecimalData = 5.50m,
				DoubleData = 7.50,
				IntData = 9,
				LongData = 11,
				StringData = "two",
				Timestamp = DateTime.Parse("2012-3-2"),
				InnerData = new InnerData
				{
					IntData = 4,
					StringData = "c",
					BoolList = new List<bool>() { false, false }
				}
			};

			col.Insert<TestData>(t);

			t = new TestData
			{
				BoolData = false,
				DecimalData = 6.50m,
				DoubleData = 8.50,
				IntData = 10,
				LongData = 12,
				StringData = "two",
				Timestamp = DateTime.Parse("2012-3-2"),
				InnerData = new InnerData
				{
					IntData = 4,
					StringData = "c",
					BoolList = new List<bool>() { true, true }
				}
			};

			col.Insert<TestData>(t);

			t = new TestData
			{
				BoolData = true,
				DecimalData = 13.50m,
				DoubleData = 15.50,
				IntData = 17,
				LongData = 19,
				StringData = "three",
				Timestamp = DateTime.Parse("2012-3-3"),
				InnerData = new InnerData
				{
					IntData = 12,
					StringData = "aa",
					BoolList = new List<bool>() { true, false }
				}
			};

			col.Insert<TestData>(t);

			t = new TestData
			{
				BoolData = true,
				DecimalData = 23.50m,
				DoubleData = 25.50,
				IntData = 27,
				LongData = 29,
				StringData = "three",
				Timestamp = DateTime.Parse("2012-3-4"),
				InnerData = new InnerData
				{
					IntData = 22,
					StringData = "bb",
					BoolList = new List<bool>() { false, false }
				},
                DynamicProps = new BsonDocument().Add("Name", "Jane").Add("Age", 28)

			};

			col.Insert<TestData>(t);


		}
		static void QueryTest()
		{
			var session = new MongoSession(_connectionString) as IMongoSession;
			var col_q = session.AsQuerable<TestData>();
			//var server = MongoServer.Create(_connectionString);
			//var db = server.GetDatabase("common");
			//var col = db.GetCollection<TestData>("TestData", SafeMode.False);
			//var col_q = col.AsQueryable<TestData>();

			//var query = from d in col_q where d.Timestamp > DateTime.Parse("2012-3-1") select d;
			//var query = from d in col_q where d.InnerData.IntData >= 4 select d;
			//var query = from d in col_q where d.InnerData.BoolList[0] == true && d.InnerData.BoolList[1] == false select d;
			//var query = from d in col_q where d.IntData < 10 && d.InnerData.BoolList[0] == true && d.InnerData.BoolList[1] == false select d;
			var query = from d in col_q where d.IntData < 10 && d.InnerData.BoolList[0] == true && Query.EQ("InnerData.BoolList.1", false).Inject() select d;
			//var query = from d in col_q where d.DecimalData > 6.0m select d;  // decimals stored as strings in mongo, so can't query on them
			//var query = from d in col_q where d.InnerData.BoolList.Contains(true) select d;
			//var query = from d in col_q where d.IntData == 7 && d.InnerData.BoolList.Contains(true) select d;

			var mq = (MongoQueryable<TestData>)query;
			var imq = mq.GetMongoQuery();
			var mongoQueryStr = imq.ToString();

			var q2 = query.MongoQueryStr();

			PrintQueryResults(query);
		}

		static void PrintQueryResults(IQueryable<TestData> query)
		{
			var data = query.ToList();
			var num = data.Count;
			Console.WriteLine(String.Format("Num Events = {0}", num));

			foreach (var d in data)
			{
				Console.WriteLine(d);
				Console.WriteLine();
			}

		}


	}
}
