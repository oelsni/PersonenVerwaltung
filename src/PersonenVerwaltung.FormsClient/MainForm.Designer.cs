namespace PersonenVerwaltung.FormsClient
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if ( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			_lsv_Persons = new ListView();
			_lvc_Id = new ColumnHeader();
			_lvc_FamilyName = new ColumnHeader();
			_lvc_GivenName = new ColumnHeader();
			_lvc_Birthdate = new ColumnHeader();
			_lbl_Persons = new Label();
			_txb_Filter = new TextBox();
			_btn_Load = new Button();
			_lbl_Filter = new Label();
			_cmb_Pagesize = new ComboBox();
			_lbl_Page = new Label();
			_lbl_Next = new Label();
			_lbl_Previous = new Label();
			SuspendLayout();
			// 
			// _lsv_Persons
			// 
			_lsv_Persons.Anchor =  AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			_lsv_Persons.Columns.AddRange(new ColumnHeader[] { _lvc_Id, _lvc_FamilyName, _lvc_GivenName, _lvc_Birthdate });
			_lsv_Persons.FullRowSelect = true;
			_lsv_Persons.Location = new Point(11, 38);
			_lsv_Persons.Name = "_lsv_Persons";
			_lsv_Persons.Size = new Size(1042, 535);
			_lsv_Persons.TabIndex = 0;
			_lsv_Persons.UseCompatibleStateImageBehavior = false;
			_lsv_Persons.View = View.Details;
			// 
			// _lvc_Id
			// 
			_lvc_Id.Text = "Id";
			// 
			// _lvc_FamilyName
			// 
			_lvc_FamilyName.Text = "Name";
			_lvc_FamilyName.Width = 100;
			// 
			// _lvc_GivenName
			// 
			_lvc_GivenName.Text = "Vorname";
			_lvc_GivenName.Width = 100;
			// 
			// _lvc_Birthdate
			// 
			_lvc_Birthdate.Text = "Geburtsdatum";
			_lvc_Birthdate.TextAlign = HorizontalAlignment.Right;
			_lvc_Birthdate.Width = 90;
			// 
			// _lbl_Persons
			// 
			_lbl_Persons.AutoSize = true;
			_lbl_Persons.BorderStyle = BorderStyle.FixedSingle;
			_lbl_Persons.Location = new Point(11, 22);
			_lbl_Persons.Name = "_lbl_Persons";
			_lbl_Persons.Size = new Size(58, 17);
			_lbl_Persons.TabIndex = 1;
			_lbl_Persons.Text = "Personen";
			// 
			// _txb_Filter
			// 
			_txb_Filter.Anchor =  AnchorStyles.Top | AnchorStyles.Right;
			_txb_Filter.Location = new Point(819, 9);
			_txb_Filter.Name = "_txb_Filter";
			_txb_Filter.Size = new Size(153, 23);
			_txb_Filter.TabIndex = 2;
			// 
			// _btn_Load
			// 
			_btn_Load.Anchor =  AnchorStyles.Top | AnchorStyles.Right;
			_btn_Load.Location = new Point(978, 9);
			_btn_Load.Name = "_btn_Load";
			_btn_Load.Size = new Size(75, 23);
			_btn_Load.TabIndex = 3;
			_btn_Load.Text = "Laden";
			_btn_Load.UseVisualStyleBackColor = true;
			_btn_Load.Click += _btn_Load_Click;
			// 
			// _lbl_Filter
			// 
			_lbl_Filter.Anchor =  AnchorStyles.Top | AnchorStyles.Right;
			_lbl_Filter.AutoSize = true;
			_lbl_Filter.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
			_lbl_Filter.Location = new Point(738, 13);
			_lbl_Filter.Name = "_lbl_Filter";
			_lbl_Filter.Size = new Size(75, 15);
			_lbl_Filter.TabIndex = 4;
			_lbl_Filter.Text = "Namensfilter";
			// 
			// _cmb_Pagesize
			// 
			_cmb_Pagesize.Anchor =  AnchorStyles.Bottom | AnchorStyles.Right;
			_cmb_Pagesize.DropDownStyle = ComboBoxStyle.DropDownList;
			_cmb_Pagesize.FormattingEnabled = true;
			_cmb_Pagesize.Items.AddRange(new object[] { "10", "15", "20", "50", "100" });
			_cmb_Pagesize.Location = new Point(956, 579);
			_cmb_Pagesize.Name = "_cmb_Pagesize";
			_cmb_Pagesize.Size = new Size(97, 23);
			_cmb_Pagesize.TabIndex = 5;
			_cmb_Pagesize.SelectedIndexChanged += _cmb_Pagesize_SelectedIndexChanged;
			// 
			// _lbl_Page
			// 
			_lbl_Page.Anchor =  AnchorStyles.Bottom | AnchorStyles.Left;
			_lbl_Page.AutoSize = true;
			_lbl_Page.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
			_lbl_Page.Location = new Point(523, 587);
			_lbl_Page.Name = "_lbl_Page";
			_lbl_Page.Size = new Size(13, 15);
			_lbl_Page.TabIndex = 6;
			_lbl_Page.Text = "1";
			// 
			// _lbl_Next
			// 
			_lbl_Next.Anchor =  AnchorStyles.Bottom | AnchorStyles.Left;
			_lbl_Next.AutoSize = true;
			_lbl_Next.Enabled = false;
			_lbl_Next.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
			_lbl_Next.ForeColor = Color.Gray;
			_lbl_Next.Location = new Point(576, 587);
			_lbl_Next.Name = "_lbl_Next";
			_lbl_Next.Size = new Size(32, 15);
			_lbl_Next.TabIndex = 7;
			_lbl_Next.Text = "Next";
			_lbl_Next.Click += _lbl_Next_Click;
			// 
			// _lbl_Previous
			// 
			_lbl_Previous.Anchor =  AnchorStyles.Bottom | AnchorStyles.Left;
			_lbl_Previous.AutoSize = true;
			_lbl_Previous.Enabled = false;
			_lbl_Previous.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
			_lbl_Previous.ForeColor = Color.Gray;
			_lbl_Previous.Location = new Point(438, 587);
			_lbl_Previous.Name = "_lbl_Previous";
			_lbl_Previous.Size = new Size(52, 15);
			_lbl_Previous.TabIndex = 8;
			_lbl_Previous.Text = "Previous";
			_lbl_Previous.Click += _lbl_Previuos_Click;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1064, 614);
			Controls.Add(_lbl_Previous);
			Controls.Add(_lbl_Next);
			Controls.Add(_lbl_Page);
			Controls.Add(_cmb_Pagesize);
			Controls.Add(_lbl_Filter);
			Controls.Add(_btn_Load);
			Controls.Add(_txb_Filter);
			Controls.Add(_lbl_Persons);
			Controls.Add(_lsv_Persons);
			Name = "MainForm";
			Text = "Form1";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ListView _lsv_Persons;
		private Label _lbl_Persons;
		private TextBox _txb_Filter;
		private Button _btn_Load;
		private Label _lbl_Filter;
		private ColumnHeader _lvc_Id;
		private ColumnHeader _lvc_FamilyName;
		private ColumnHeader _lvc_GivenName;
		private ColumnHeader _lvc_Birthdate;
		private ComboBox _cmb_Pagesize;
		private Label _lbl_Page;
		private Label _lbl_Next;
		private Label _lbl_Previous;
	}
}
