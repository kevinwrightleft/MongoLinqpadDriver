using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoLinqpadDriver
{
	public partial class MongoContext
	{
		protected string _nameSpace;
		protected string _typeName;
		protected List<Tuple<Type, string>> _entityTypes;

		public MongoContext(string nameSpace, string typeName, List<Tuple<Type, string>> entityTypes)
		{
			_nameSpace = nameSpace;
			_typeName = typeName;
			_entityTypes = entityTypes;
		}
	}
}
