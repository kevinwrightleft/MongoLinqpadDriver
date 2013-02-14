using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoLinqpadDriver
{
	public static class Extensions
	{
		public static string MongoQueryStr(this object o)
		{
			var imq = GetMongoQuery(o);
			var retVal = imq.ToString();
			return retVal;
		}

		private static IMongoQuery GetMongoQuery(object o)
		{
			Type myType = o.GetType();
			// Get the method information for MyFunc(int, int).
			MethodInfo methodInfo = myType.GetMethod("GetMongoQuery");
			var retVal = methodInfo.Invoke(o, null);
			return (IMongoQuery)retVal;
		}
	}
}
