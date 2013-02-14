using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;

namespace MongoLinqpadDriver
{
	public interface IMongoSession
	{
		MongoDatabase DB { get; }
		MongoCollection<T> GetCollection<T>();
		MongoCollection<T> GetCollection<T>(string collectionName);

		IQueryable<T> AsQuerable<T>()
			where T : class, new();

		IQueryable<T> AsQuerable<T>(string collectionName)
			where T : class, new();

	}

	public class MongoSession : IMongoSession
	{
		//private MongoClient _client;
		private MongoServer _server;
		private MongoDatabase _db;

		public MongoDatabase DB { get { return _db; } }

		public MongoSession(string connectionStr)
		{

			if (string.IsNullOrEmpty(connectionStr))
			{
				throw new MongoException("Mongo connection string unspecified.");
			}

			var lastSlash = connectionStr.LastIndexOf('/');
			if (lastSlash == -1)
			{
				throw new MongoException("Mongo connection string not formatted correctly");
			}

			var server = connectionStr.Substring(0, lastSlash);
			var database = connectionStr.Substring(lastSlash + 1, (connectionStr.Length - 1) - lastSlash);

			_server = MongoServer.Create(server);
			//_client = new MongoClient(server);
			//_server = _client.GetServer();
			_db = _server.GetDatabase(database);

		}

		public IEnumerable<string> GetCollectionNames()
		{
			return DB.GetCollectionNames();
		}

		public MongoCollection<T> GetCollection<T>()
		{
			var collectionName = typeof(T).Name;
			return _db.GetCollection<T>(collectionName, SafeMode.False);
		}

		public MongoCollection<T> GetCollection<T>(string collectionName)
		{
			return _db.GetCollection<T>(collectionName, SafeMode.False);
		}

		public IQueryable<T> AsQuerable<T>()
			 where T : class, new()
		{
			return GetCollection<T>().AsQueryable<T>();
		}

		public IQueryable<T> AsQuerable<T>(string collectionName)
			 where T : class, new()
		{
			return GetCollection<T>(collectionName).AsQueryable<T>();
		}

	}
}

