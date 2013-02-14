using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using LINQPad.Extensibility.DataContext;
using Microsoft.CSharp;
using MongoUtils;

namespace MongoLinqpadDriver
{
    public class MongoDriver : DynamicDataContextDriver
    {
        private IConnectionInfo _connectionInfo;

        public MongoDriver()
        {
        }

        public override string Name
        {
            get { return "MongoDb"; }
        }

        public override string Author
        {
            get { return "Volusion"; }
        }

        public override string GetConnectionDescription(IConnectionInfo connectionInfo)
        {
            _connectionInfo = connectionInfo;
            var props = new MongoLinqpadProperties(connectionInfo);
            return "MongoDb - " + props.ConnectionString;
        }

        public override bool ShowConnectionDialog(IConnectionInfo connectionInfo, bool isNewConnection)
        {
            WindowsFormsSynchronizationContext.AutoInstall = true;

            using (var cd = new MongoLinqpadPropsForm(connectionInfo, isNewConnection))
            {
                try
                {
                    Application.Run(cd);
                }
                catch (InvalidOperationException)
                {
                    cd.ShowDialog();
                }

                Application.Exit();
                return cd.DialogResult == DialogResult.OK;
            }
        }

        public override IEnumerable<string> GetNamespacesToAdd()
        {
			var retVal = new List<string>
			{
				"MongoUtils",
				"MongoLinqpadDriver",
				"MongoDB.Bson",
				"MongoDB.Driver",
				"MongoDB.Driver.Builders",
				"MongoDB.Driver.Linq"
			};
			return retVal;
        }

        public override IEnumerable<string> GetAssembliesToAdd()
        {
            var props = new MongoLinqpadProperties(_connectionInfo);
			var retVal = new List<string>()
			{
				Path.Combine(GetDriverFolder(), "MongoDB.Bson.dll"),
				Path.Combine(GetDriverFolder(), "MongoDB.Driver.dll"),
				Path.Combine(GetDriverFolder(), "MongoUtils.dll"),
				Path.Combine(GetDriverFolder(), "MongoLinqpadDriver.dll"),
				props.EntityAssemblyPath
			};
			return retVal;
        }

        public override object[] GetContextConstructorArguments(IConnectionInfo cxInfo)
        {
            var mongoProps = new MongoLinqpadProperties(cxInfo);
			var mongoSessionType = typeof(MongoSession);
			var ctor = mongoSessionType.GetConstructor(new[] { typeof(string) });
			var session = ctor.Invoke(new object[] { mongoProps.ConnectionString });
            return new[] {session};
        }

        public override ParameterDescriptor[] GetContextConstructorParameters(IConnectionInfo connectionInfo)
        {
			var sessionType = typeof(MongoSession);
			return new[] {new ParameterDescriptor("MongoSession", sessionType.FullName),};
        }


        public override List<ExplorerItem> GetSchemaAndBuildAssembly(IConnectionInfo cxInfo, AssemblyName assemblyToBuild, ref string nameSpace, ref string typeName)
        {
			var builder = new AssemblyBuilder(cxInfo, GetDriverFolder(), assemblyToBuild, nameSpace, typeName);
			return builder.Build();
        }

    }
}