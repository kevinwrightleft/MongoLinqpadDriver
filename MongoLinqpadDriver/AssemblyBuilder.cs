using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using LINQPad.Extensibility.DataContext;
using Microsoft.CSharp;
using MongoUtils;

namespace MongoLinqpadDriver
{
	class AssemblyBuilder
	{
		protected IConnectionInfo _cxInfo;
		protected string _driverFolder;
		protected AssemblyName _assemblyToBuild;
		protected string _nameSpace;
		protected string _typeName;
		protected MongoLinqpadProperties _mongoProps;
		protected IEnumerable<string> _collectionNames;
		protected Assembly _entityAssembly;
		protected List<Tuple<Type, string>> _entityTypes;
		protected string[] _entityAssemblyNamespaces;

		public AssemblyBuilder(IConnectionInfo cxInfo, string driverFolder, AssemblyName assemblyToBuild, string nameSpace, string typeName)
		{
			_cxInfo = cxInfo;
			_driverFolder = driverFolder;
			_assemblyToBuild = assemblyToBuild;
			_nameSpace = nameSpace;
			_typeName = typeName;
			_mongoProps = new MongoLinqpadProperties(_cxInfo);
			_entityAssembly = Assembly.LoadFrom(_mongoProps.EntityAssemblyPath);
			_entityTypes = new List<Tuple<Type, string>>();
			_collectionNames = GetCollectionNames(_mongoProps);
			_entityAssemblyNamespaces = _mongoProps.EntityAssemblyNamespaces.Split(';');
		}

		public List<ExplorerItem> Build()
		{
			GetSameNamedCollections();
			GetDifferentNamedCollections();
			var src = CreateSourceCode();
			CreateBinary(src);
			return BuildExplorerItems();
		}

		private void GetDifferentNamedCollections()
		{
			// handle collections that have different names than the collection name
			for (int i = 0; i < _entityAssemblyNamespaces.Length; i++)
			{
				// skip explictity named types
				if (!_entityAssemblyNamespaces[i].Contains(":"))
					continue;

				var typeAndCollectionName = _entityAssemblyNamespaces[i].Split(':');
				var entityType = Tuple.Create<Type, string>(_entityAssembly.GetType(typeAndCollectionName[0]), typeAndCollectionName[1]); 
				_entityTypes.Add(entityType);
			}

		}

		private void GetSameNamedCollections()
		{
			for (int i = 0; i < _entityAssemblyNamespaces.Length; i++)
			{
				// skip explictity named collections
				if (_entityAssemblyNamespaces[i].Contains(":"))
					continue;

				// add types in the specifiled namespaces that have names that are in the mongo collection names
				var filteredTypes = _entityAssembly.GetTypes().
					Where(x => x.Namespace != null &&
						x.Namespace.StartsWith(_entityAssemblyNamespaces[i].Trim()) &&
						_collectionNames.Contains(x.Name)).
					Select(x => Tuple.Create<Type, string>(x, x.Name)).
					ToList();

				_entityTypes.AddRange(filteredTypes);
			}

		}

		private IEnumerable<string> GetCollectionNames(MongoLinqpadProperties props)
		{
			var session = new MongoSession(props.ConnectionString);
			return session.GetCollectionNames();
		}

		private void GetChildProperties(Type t, List<ExplorerItem> childItems)
		{
			var publicProperties = t.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
			for (int j = 0; j < publicProperties.Length; j++)
			{
				PropertyInfo info = publicProperties[j];

				if (info.PropertyType.IsClass)
				{
					var item = new ExplorerItem(info.Name, ExplorerItemKind.Property, ExplorerIcon.Column);
					item.ToolTipText = info.PropertyType.Name;
					childItems.Add(item);

					if (info.PropertyType.Name.StartsWith("Bson")) // don't add properties of bson documents
					{
						continue;
					}

					var ienunerable = info.PropertyType.GetInterface("IEnumerable");
					if (ienunerable != null)
					{
						item.IsEnumerable = true;
					}

					item.Children = new List<ExplorerItem>();
					GetChildProperties(info.PropertyType, item.Children);
				}
				else
				{
					childItems.Add(new ExplorerItem(info.Name, ExplorerItemKind.Property, ExplorerIcon.Column));
				}
			}

		}


		//private void GetChildProperties(Type t, List<ExplorerItem> childItems)
		//{
		//	var publicProperties = t.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
		//	for (int j = 0; j < publicProperties.Length; j++)
		//	{
		//		PropertyInfo info = publicProperties[j];
		//		if (info.PropertyType.Name.StartsWith("Bson")) // ignore bson documents
		//			continue;

		//		if (info.PropertyType.IsClass)
		//		{
		//			var item = new ExplorerItem(info.Name, ExplorerItemKind.Property, ExplorerIcon.Column);
		//			var ienunerable = info.PropertyType.GetInterface("IEnumerable");
		//			if (ienunerable != null)
		//			{
		//				item.IsEnumerable = true;
		//			}

		//			item.Children = new List<ExplorerItem>();
		//			childItems.Add(item);
		//			GetChildProperties(info.PropertyType, item.Children);
		//		}
		//		else
		//		{
		//			childItems.Add(new ExplorerItem(info.Name, ExplorerItemKind.Property, ExplorerIcon.Column));
		//		}
		//	}

		//}

		private List<ExplorerItem> BuildExplorerItems()
		{
			var result = new List<ExplorerItem>();
			var item = new ExplorerItem("Entities", ExplorerItemKind.Category, ExplorerIcon.Schema);
			item.Children = new List<ExplorerItem>();
			result.Add(item);

			for (int i = 0; i < _entityTypes.Count; i++)
			{
				Type t = _entityTypes[i].Item1;
				string collectionName = _entityTypes[i].Item2;
				var typeItem = new ExplorerItem(collectionName, ExplorerItemKind.QueryableObject, ExplorerIcon.Table);
				item.Children.Add(typeItem);
				typeItem.IsEnumerable = true;
				typeItem.ToolTipText = string.Format("{0} Entity", collectionName);

				typeItem.Children = new List<ExplorerItem>();
				GetChildProperties(t, typeItem.Children);
			}

			return result;
		}

		//private List<ExplorerItem> BuildExplorerItems()
		//{
		//	var result = new List<ExplorerItem>();
		//	var item = new ExplorerItem("Entities", ExplorerItemKind.Category, ExplorerIcon.Schema);
		//	item.Children = new List<ExplorerItem>();

		//	for (int i = 0; i < _entityTypes.Count; i++)
		//	{
		//		Type t = _entityTypes[i].Item1;
		//		string collectionName = _entityTypes[i].Item2;
		//		var typeItem = new ExplorerItem(collectionName, ExplorerItemKind.QueryableObject, ExplorerIcon.Table);
		//		typeItem.IsEnumerable = true;
		//		typeItem.ToolTipText = string.Format("{0} Entity", collectionName);

		//		var publicProperties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
		//		typeItem.Children = new List<ExplorerItem>();

		//		for (int j = 0; j < publicProperties.Length; j++)
		//		{
		//			PropertyInfo info = publicProperties[j];
		//			typeItem.Children.Add(new ExplorerItem(info.Name, ExplorerItemKind.Property, ExplorerIcon.Column));
		//		}

		//		item.Children.Add(typeItem);
		//	}

		//	result.Add(item);
		//	return result;
		//}

		private string CreateSourceCode()
		{
			var template = new MongoContext(_nameSpace, _typeName, _entityTypes);
			var source = template.TransformText();
			return source;
		}

		private void CreateBinary(string src)
		{
			CompilerResults results;

			var assemblyNames = new List<string>();
			assemblyNames.Add("System.dll");
			assemblyNames.Add("System.Core.dll");
			assemblyNames.Add(_mongoProps.EntityAssemblyPath);
			assemblyNames.Add(Path.Combine(_driverFolder, "MongoUtils.dll"));   /// TODO - uncomment this and see errors
			assemblyNames.Add(Path.Combine(_driverFolder, "MongoLinqpadDriver.dll"));

			var providerOptions = new Dictionary<string, string> { { "CompilerVersion", "v4.0" } };
			using (var provider = new CSharpCodeProvider(providerOptions))
			{
				var compilerParams = new CompilerParameters(assemblyNames.ToArray(), _assemblyToBuild.CodeBase, true);
				results = provider.CompileAssemblyFromSource(compilerParams, src);
			}

			if (results.Errors.Count > 0)
			{
				throw new Exception(GetCompileErrorsAsString(results.Errors));
			}
		}

		private string GetCompileErrorsAsString(CompilerErrorCollection errors)
		{
			var sb = new StringBuilder();
			sb.AppendLine("MongoDbLinqpadDriver - Error compiling dynamic driver: ");
			for (var i = 0; i < errors.Count; i++)
			{
				sb.AppendLine(String.Format("line {0} - {1}", errors[i].Line, errors[i].ErrorText));
			}

			return sb.ToString();
		}
	}
}
