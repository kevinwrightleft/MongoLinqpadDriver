using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LINQPad.Extensibility.DataContext;

namespace MongoLinqpadDriver
{
    public partial class MongoLinqpadPropsForm : Form
    {
        public MongoLinqpadPropsForm(IConnectionInfo cxInfo, bool isNewConnection)
        {
            this.cxInfo = cxInfo;
            this.isNewConnection = isNewConnection;
            Connection = new MongoLinqpadProperties(cxInfo);

            InitializeComponent();
        }

        #region Properties 
        MongoLinqpadProperties Connection;
        private readonly IConnectionInfo cxInfo;
        private bool isNewConnection;
        #endregion

        private void OnLoad(object sender, EventArgs e)
        {
            this.txtConnectionString.Text = Connection.ConnectionString;
            this.txtEntityAssembly.Text = Connection.EntityAssemblyPath;
            this.txtEntityNamespace.Text = Connection.EntityAssemblyNamespaces;

            txtConnectionString.Focus();
        }

        private void OnBrowseAssembly(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Filter = "Assembly files (*.dll;*.exe)|*.dll;*.exe|All files (*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
                txtEntityAssembly.Text = dlg.FileName;
        }

        private void OnOk(object sender, EventArgs e)
        {
            Connection.ConnectionString = this.txtConnectionString.Text;
            Connection.EntityAssemblyPath = this.txtEntityAssembly.Text;
            Connection.EntityAssemblyNamespaces = this.txtEntityNamespace.Text;

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
