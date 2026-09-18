namespace PersonenVerwaltung.FormsClient
{
	partial class PersonDialog
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
			if ( disposing && (components != null) )
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
			_lbl_FamilyName = new Label();
			_lbl_GivenName = new Label();
			_lbl_Birthdate = new Label();
			_lbl_Addresses = new Label();
			_lbl_Phones = new Label();
			_txb_FamilyName = new TextBox();
			_txb_GivenName = new TextBox();
			_txb_Birthdate = new TextBox();
			_lsv_Addresses = new ListView();
			_lvc_City = new ColumnHeader();
			_lvc_Zipcode = new ColumnHeader();
			_lvc_Street = new ColumnHeader();
			_lvc_StreetNumber = new ColumnHeader();
			_lsb_Phones = new ListBox();
			_btn_Cancel = new Button();
			_btn_Save = new Button();
			SuspendLayout();
			// 
			// _lbl_FamilyName
			// 
			_lbl_FamilyName.AutoSize = true;
			_lbl_FamilyName.Location = new Point(12, 9);
			_lbl_FamilyName.Name = "_lbl_FamilyName";
			_lbl_FamilyName.Size = new Size(39, 15);
			_lbl_FamilyName.TabIndex = 0;
			_lbl_FamilyName.Text = "Name";
			// 
			// _lbl_GivenName
			// 
			_lbl_GivenName.AutoSize = true;
			_lbl_GivenName.Location = new Point(194, 9);
			_lbl_GivenName.Name = "_lbl_GivenName";
			_lbl_GivenName.Size = new Size(54, 15);
			_lbl_GivenName.TabIndex = 1;
			_lbl_GivenName.Text = "Vorname";
			// 
			// _lbl_Birthdate
			// 
			_lbl_Birthdate.AutoSize = true;
			_lbl_Birthdate.Location = new Point(12, 69);
			_lbl_Birthdate.Name = "_lbl_Birthdate";
			_lbl_Birthdate.Size = new Size(83, 15);
			_lbl_Birthdate.TabIndex = 2;
			_lbl_Birthdate.Text = "Geburtsdatum";
			// 
			// _lbl_Addresses
			// 
			_lbl_Addresses.AutoSize = true;
			_lbl_Addresses.Location = new Point(12, 151);
			_lbl_Addresses.Name = "_lbl_Addresses";
			_lbl_Addresses.Size = new Size(68, 15);
			_lbl_Addresses.TabIndex = 3;
			_lbl_Addresses.Text = "Anschriften";
			// 
			// _lbl_Phones
			// 
			_lbl_Phones.Anchor =  AnchorStyles.Top | AnchorStyles.Right;
			_lbl_Phones.AutoSize = true;
			_lbl_Phones.Location = new Point(410, 9);
			_lbl_Phones.Name = "_lbl_Phones";
			_lbl_Phones.Size = new Size(119, 15);
			_lbl_Phones.TabIndex = 4;
			_lbl_Phones.Text = "Telefonverbindungen";
			// 
			// _txb_FamilyName
			// 
			_txb_FamilyName.BorderStyle = BorderStyle.FixedSingle;
			_txb_FamilyName.Location = new Point(12, 27);
			_txb_FamilyName.Name = "_txb_FamilyName";
			_txb_FamilyName.Size = new Size(143, 23);
			_txb_FamilyName.TabIndex = 5;
			// 
			// _txb_GivenName
			// 
			_txb_GivenName.BorderStyle = BorderStyle.FixedSingle;
			_txb_GivenName.Location = new Point(194, 27);
			_txb_GivenName.Name = "_txb_GivenName";
			_txb_GivenName.Size = new Size(143, 23);
			_txb_GivenName.TabIndex = 6;
			// 
			// _txb_Birthdate
			// 
			_txb_Birthdate.BorderStyle = BorderStyle.FixedSingle;
			_txb_Birthdate.Location = new Point(12, 87);
			_txb_Birthdate.Name = "_txb_Birthdate";
			_txb_Birthdate.ReadOnly = true;
			_txb_Birthdate.Size = new Size(143, 23);
			_txb_Birthdate.TabIndex = 7;
			// 
			// _lsv_Addresses
			// 
			_lsv_Addresses.Anchor =  AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			_lsv_Addresses.Columns.AddRange(new ColumnHeader[] { _lvc_City, _lvc_Zipcode, _lvc_Street, _lvc_StreetNumber });
			_lsv_Addresses.FullRowSelect = true;
			_lsv_Addresses.Location = new Point(12, 169);
			_lsv_Addresses.Name = "_lsv_Addresses";
			_lsv_Addresses.Size = new Size(631, 192);
			_lsv_Addresses.TabIndex = 8;
			_lsv_Addresses.UseCompatibleStateImageBehavior = false;
			_lsv_Addresses.View = View.Details;
			// 
			// _lvc_City
			// 
			_lvc_City.Text = "Ort";
			_lvc_City.Width = 100;
			// 
			// _lvc_Zipcode
			// 
			_lvc_Zipcode.Text = "Postleitzahl";
			_lvc_Zipcode.Width = 90;
			// 
			// _lvc_Street
			// 
			_lvc_Street.Text = "Straße";
			_lvc_Street.Width = 150;
			// 
			// _lvc_StreetNumber
			// 
			_lvc_StreetNumber.Text = "Hausnummer";
			_lvc_StreetNumber.TextAlign = HorizontalAlignment.Right;
			_lvc_StreetNumber.Width = 90;
			// 
			// _lsb_Phones
			// 
			_lsb_Phones.Anchor =  AnchorStyles.Top | AnchorStyles.Right;
			_lsb_Phones.FormattingEnabled = true;
			_lsb_Phones.Location = new Point(410, 27);
			_lsb_Phones.Name = "_lsb_Phones";
			_lsb_Phones.Size = new Size(233, 124);
			_lsb_Phones.TabIndex = 9;
			// 
			// _btn_Cancel
			// 
			_btn_Cancel.Location = new Point(12, 367);
			_btn_Cancel.Name = "_btn_Cancel";
			_btn_Cancel.Size = new Size(75, 23);
			_btn_Cancel.TabIndex = 10;
			_btn_Cancel.Text = "Abbrechen";
			_btn_Cancel.UseVisualStyleBackColor = true;
			_btn_Cancel.Click += _btn_Cancel_Click;
			// 
			// _btn_Save
			// 
			_btn_Save.Location = new Point(568, 367);
			_btn_Save.Name = "_btn_Save";
			_btn_Save.Size = new Size(75, 23);
			_btn_Save.TabIndex = 11;
			_btn_Save.Text = "Speichern";
			_btn_Save.UseVisualStyleBackColor = true;
			_btn_Save.Click += _btn_Save_Click;
			// 
			// PersonDialog
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(658, 397);
			Controls.Add(_btn_Save);
			Controls.Add(_btn_Cancel);
			Controls.Add(_lsb_Phones);
			Controls.Add(_lsv_Addresses);
			Controls.Add(_txb_Birthdate);
			Controls.Add(_txb_GivenName);
			Controls.Add(_txb_FamilyName);
			Controls.Add(_lbl_Phones);
			Controls.Add(_lbl_Addresses);
			Controls.Add(_lbl_Birthdate);
			Controls.Add(_lbl_GivenName);
			Controls.Add(_lbl_FamilyName);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "PersonDialog";
			Text = "Person";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label _lbl_FamilyName;
		private Label _lbl_GivenName;
		private Label _lbl_Birthdate;
		private Label _lbl_Addresses;
		private Label _lbl_Phones;
		private TextBox _txb_FamilyName;
		private TextBox _txb_GivenName;
		private TextBox _txb_Birthdate;
		private ListView _lsv_Addresses;
		private ColumnHeader _lvc_City;
		private ColumnHeader _lvc_Zipcode;
		private ColumnHeader _lvc_Street;
		private ColumnHeader _lvc_StreetNumber;
		private ListBox _lsb_Phones;
		private Button _btn_Cancel;
		private Button _btn_Save;
	}
}