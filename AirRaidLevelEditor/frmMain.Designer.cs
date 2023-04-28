namespace AirRaidLevelEditor
{
	partial class frmMain
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
			this.uiTabControlMenu1 = new Sunny.UI.UITabControlMenu();
			this.tbLevelEdit = new System.Windows.Forms.TabPage();
			this.tbAssetEdit = new System.Windows.Forms.TabPage();
			this.uiTableLayoutPanel1 = new Sunny.UI.UITableLayoutPanel();
			this.uiTableLayoutPanel2 = new Sunny.UI.UITableLayoutPanel();
			this.uiListBox1 = new Sunny.UI.UIListBox();
			this.uiPanel1 = new Sunny.UI.UIPanel();
			this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
			this.uiSymbolButton2 = new Sunny.UI.UISymbolButton();
			this.uiTabControlMenu1.SuspendLayout();
			this.tbLevelEdit.SuspendLayout();
			this.uiTableLayoutPanel1.SuspendLayout();
			this.uiTableLayoutPanel2.SuspendLayout();
			this.uiPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// uiTabControlMenu1
			// 
			this.uiTabControlMenu1.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.uiTabControlMenu1.Controls.Add(this.tbLevelEdit);
			this.uiTabControlMenu1.Controls.Add(this.tbAssetEdit);
			this.uiTabControlMenu1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiTabControlMenu1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.uiTabControlMenu1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.uiTabControlMenu1.Location = new System.Drawing.Point(0, 35);
			this.uiTabControlMenu1.Multiline = true;
			this.uiTabControlMenu1.Name = "uiTabControlMenu1";
			this.uiTabControlMenu1.SelectedIndex = 0;
			this.uiTabControlMenu1.Size = new System.Drawing.Size(867, 523);
			this.uiTabControlMenu1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.uiTabControlMenu1.TabIndex = 0;
			this.uiTabControlMenu1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
			// 
			// tbLevelEdit
			// 
			this.tbLevelEdit.Controls.Add(this.uiTableLayoutPanel1);
			this.tbLevelEdit.Location = new System.Drawing.Point(201, 0);
			this.tbLevelEdit.Name = "tbLevelEdit";
			this.tbLevelEdit.Size = new System.Drawing.Size(666, 523);
			this.tbLevelEdit.TabIndex = 0;
			this.tbLevelEdit.Text = "Levels";
			this.tbLevelEdit.UseVisualStyleBackColor = true;
			// 
			// tbAssetEdit
			// 
			this.tbAssetEdit.Location = new System.Drawing.Point(201, 0);
			this.tbAssetEdit.Name = "tbAssetEdit";
			this.tbAssetEdit.Size = new System.Drawing.Size(666, 523);
			this.tbAssetEdit.TabIndex = 1;
			this.tbAssetEdit.Text = "Assets";
			this.tbAssetEdit.UseVisualStyleBackColor = true;
			// 
			// uiTableLayoutPanel1
			// 
			this.uiTableLayoutPanel1.ColumnCount = 2;
			this.uiTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
			this.uiTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.uiTableLayoutPanel1.Controls.Add(this.uiTableLayoutPanel2, 0, 0);
			this.uiTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.uiTableLayoutPanel1.Name = "uiTableLayoutPanel1";
			this.uiTableLayoutPanel1.RowCount = 1;
			this.uiTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.uiTableLayoutPanel1.Size = new System.Drawing.Size(666, 523);
			this.uiTableLayoutPanel1.TabIndex = 1;
			this.uiTableLayoutPanel1.TagString = null;
			// 
			// uiTableLayoutPanel2
			// 
			this.uiTableLayoutPanel2.ColumnCount = 1;
			this.uiTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.uiTableLayoutPanel2.Controls.Add(this.uiListBox1, 0, 0);
			this.uiTableLayoutPanel2.Controls.Add(this.uiPanel1, 0, 1);
			this.uiTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiTableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.uiTableLayoutPanel2.Name = "uiTableLayoutPanel2";
			this.uiTableLayoutPanel2.RowCount = 2;
			this.uiTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.uiTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.uiTableLayoutPanel2.Size = new System.Drawing.Size(294, 517);
			this.uiTableLayoutPanel2.TabIndex = 0;
			this.uiTableLayoutPanel2.TagString = null;
			// 
			// uiListBox1
			// 
			this.uiListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiListBox1.FillColor = System.Drawing.Color.White;
			this.uiListBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.uiListBox1.Location = new System.Drawing.Point(4, 5);
			this.uiListBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.uiListBox1.MinimumSize = new System.Drawing.Size(1, 1);
			this.uiListBox1.Name = "uiListBox1";
			this.uiListBox1.Padding = new System.Windows.Forms.Padding(2);
			this.uiListBox1.ShowText = false;
			this.uiListBox1.Size = new System.Drawing.Size(286, 427);
			this.uiListBox1.TabIndex = 0;
			this.uiListBox1.Text = "uiListBox1";
			this.uiListBox1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
			// 
			// uiPanel1
			// 
			this.uiPanel1.Controls.Add(this.uiSymbolButton2);
			this.uiPanel1.Controls.Add(this.uiSymbolButton1);
			this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.uiPanel1.Location = new System.Drawing.Point(4, 442);
			this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
			this.uiPanel1.Name = "uiPanel1";
			this.uiPanel1.Size = new System.Drawing.Size(286, 70);
			this.uiPanel1.TabIndex = 1;
			this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.uiPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
			// 
			// uiSymbolButton1
			// 
			this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.uiSymbolButton1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.uiSymbolButton1.Location = new System.Drawing.Point(13, 10);
			this.uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
			this.uiSymbolButton1.Name = "uiSymbolButton1";
			this.uiSymbolButton1.Size = new System.Drawing.Size(48, 48);
			this.uiSymbolButton1.Symbol = 61543;
			this.uiSymbolButton1.TabIndex = 0;
			this.uiSymbolButton1.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.uiSymbolButton1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
			// 
			// uiSymbolButton2
			// 
			this.uiSymbolButton2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.uiSymbolButton2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.uiSymbolButton2.Location = new System.Drawing.Point(77, 10);
			this.uiSymbolButton2.MinimumSize = new System.Drawing.Size(1, 1);
			this.uiSymbolButton2.Name = "uiSymbolButton2";
			this.uiSymbolButton2.Size = new System.Drawing.Size(48, 48);
			this.uiSymbolButton2.Symbol = 61544;
			this.uiSymbolButton2.TabIndex = 1;
			this.uiSymbolButton2.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.uiSymbolButton2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
			// 
			// frmMain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(867, 558);
			this.Controls.Add(this.uiTabControlMenu1);
			this.Name = "frmMain";
			this.Text = "Air Raid : Red Sea Level Editor";
			this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 867, 558);
			this.uiTabControlMenu1.ResumeLayout(false);
			this.tbLevelEdit.ResumeLayout(false);
			this.uiTableLayoutPanel1.ResumeLayout(false);
			this.uiTableLayoutPanel2.ResumeLayout(false);
			this.uiPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Sunny.UI.UITabControlMenu uiTabControlMenu1;
		private System.Windows.Forms.TabPage tbLevelEdit;
		private System.Windows.Forms.TabPage tbAssetEdit;
		private Sunny.UI.UITableLayoutPanel uiTableLayoutPanel1;
		private Sunny.UI.UITableLayoutPanel uiTableLayoutPanel2;
		private Sunny.UI.UIListBox uiListBox1;
		private Sunny.UI.UIPanel uiPanel1;
		private Sunny.UI.UISymbolButton uiSymbolButton2;
		private Sunny.UI.UISymbolButton uiSymbolButton1;
	}
}