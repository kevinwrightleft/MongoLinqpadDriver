namespace MongoLinqpadDriver
{
    partial class MongoLinqpadPropsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtConnectionString = new System.Windows.Forms.TextBox();
			this.txtEntityAssembly = new System.Windows.Forms.TextBox();
			this.txtEntityNamespace = new System.Windows.Forms.TextBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnBrowseAssembly = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(127, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Mongo Connection String";
			this.toolTip1.SetToolTip(this.label1, "MongoDb Connection String");
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Entity Assembly";
			this.toolTip1.SetToolTip(this.label2, "Path to the assembly containing the actual entities");
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 68);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Entity Namespace(s)";
			this.toolTip1.SetToolTip(this.label3, "Namespace(s) within the EntityAssembly that contain the Entity Classes. Separate " +
        "multiple namespaces with semicolons.");
			// 
			// txtConnectionString
			// 
			this.txtConnectionString.Location = new System.Drawing.Point(143, 6);
			this.txtConnectionString.Name = "txtConnectionString";
			this.txtConnectionString.Size = new System.Drawing.Size(342, 20);
			this.txtConnectionString.TabIndex = 1;
			this.toolTip1.SetToolTip(this.txtConnectionString, "MongoDb Connection String");
			// 
			// txtEntityAssembly
			// 
			this.txtEntityAssembly.Location = new System.Drawing.Point(143, 36);
			this.txtEntityAssembly.Name = "txtEntityAssembly";
			this.txtEntityAssembly.ReadOnly = true;
			this.txtEntityAssembly.Size = new System.Drawing.Size(308, 20);
			this.txtEntityAssembly.TabIndex = 4;
			this.toolTip1.SetToolTip(this.txtEntityAssembly, "Path to the assembly containing the actual entities");
			// 
			// txtEntityNamespace
			// 
			this.txtEntityNamespace.Location = new System.Drawing.Point(143, 65);
			this.txtEntityNamespace.Name = "txtEntityNamespace";
			this.txtEntityNamespace.Size = new System.Drawing.Size(342, 20);
			this.txtEntityNamespace.TabIndex = 3;
			this.toolTip1.SetToolTip(this.txtEntityNamespace, "Namespace(s) within the EntityAssembly that contain the Entity Classes. Separate " +
        "multiple namespaces with semicolons.");
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(15, 95);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(471, 1);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			// 
			// btnBrowseAssembly
			// 
			this.btnBrowseAssembly.Location = new System.Drawing.Point(457, 35);
			this.btnBrowseAssembly.Name = "btnBrowseAssembly";
			this.btnBrowseAssembly.Size = new System.Drawing.Size(28, 20);
			this.btnBrowseAssembly.TabIndex = 2;
			this.btnBrowseAssembly.Text = "...";
			this.btnBrowseAssembly.UseVisualStyleBackColor = true;
			this.btnBrowseAssembly.Click += new System.EventHandler(this.OnBrowseAssembly);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(329, 107);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(410, 107);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 11;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.OnOk);
			// 
			// DriverConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(499, 136);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnBrowseAssembly);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.txtEntityNamespace);
			this.Controls.Add(this.txtEntityAssembly);
			this.Controls.Add(this.txtConnectionString);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "DriverConfigForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MongoDb Connection Configuration";
			this.Load += new System.EventHandler(this.OnLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.TextBox txtEntityAssembly;
        private System.Windows.Forms.TextBox txtEntityNamespace;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrowseAssembly;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}