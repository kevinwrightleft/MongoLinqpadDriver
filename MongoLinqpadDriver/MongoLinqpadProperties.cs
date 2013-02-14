using System;
using System.Xml.Linq;
using LINQPad.Extensibility.DataContext;

namespace MongoLinqpadDriver
{
    public class MongoLinqpadProperties
    {
        private readonly IConnectionInfo cxInfo;
        readonly XElement driverData;
        
        public MongoLinqpadProperties(IConnectionInfo cxInfo)
        {
            this.cxInfo = cxInfo;
            driverData = cxInfo.DriverData;
        }

        public bool Persist
        {
            get { return CxInfo.Persist; }
            set { CxInfo.Persist = value; }
        }

        public string ConnectionString
        {
            get { return (string)driverData.Element("ConnectionString") ?? string.Empty; }
            set { driverData.SetElementValue("ConnectionString", value); }
        }

        public string EntityAssemblyPath
        {
            get { return (string)driverData.Element("EntityAssemblyPath") ?? string.Empty; }
            set { driverData.SetElementValue("EntityAssemblyPath", value); }
        }

        public string EntityAssemblyNamespaces
        {
            get { return (string)driverData.Element("EntityAssemblyNamespaces") ?? string.Empty; }
            set { driverData.SetElementValue("EntityAssemblyNamespaces", value); }
        }

        public IConnectionInfo CxInfo
        {
            get { return cxInfo; }
        }
    }
}
