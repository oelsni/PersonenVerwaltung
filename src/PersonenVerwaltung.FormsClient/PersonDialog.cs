using System.ComponentModel;

using PersonenVerwaltung.ServiceClient;

namespace PersonenVerwaltung.FormsClient
{
	public partial class PersonDialog : Form
	{
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PersonDto Person
		{
			get;
			set
			{
				field = value;
				ApplyValues();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public NameUpdateDto NameUpdate
		{ get; set; }


		public PersonDialog()
		{
			InitializeComponent();

			DialogResult = DialogResult.None;
		}


		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			DialogResult = DialogResult.None;
		}

		private void ApplyValues()
		{
			Text = $"{Person.FamilyName} {Person.GivenName} ({Person.Id})";

			_txb_Birthdate.Text = $"{Person.Birthdate}";
			_txb_FamilyName.Text = Person.FamilyName;
			_txb_GivenName.Text = Person.GivenName;

			_lsv_Addresses.Items.Clear();
			_lsb_Phones.Items.Clear();

			foreach ( AddressDto address in Person.Addresses )
				_lsv_Addresses.Items.Add(new ListViewItem([address.City, address.Zipcode, address.Street, address.StreetNumber]));

			foreach ( PhoneDto phone in Person.Phones )
				_lsb_Phones.Items.Add(phone.Number);

			NameUpdate = default;
		}

		private void _btn_Save_Click(object? sender, EventArgs e)
		{
			string? familyName = null;
			string? givenName = null;

			if ( _txb_FamilyName.Text != Person.FamilyName )
				familyName = _txb_FamilyName.Text;

			if ( _txb_GivenName.Text != Person.GivenName )
				givenName = _txb_GivenName.Text;

			NameUpdate = new NameUpdateDto
			{
				FamilyName = familyName,
				GivenName = givenName,
			};

			DialogResult = DialogResult.Continue;
			Close();
		}

		private void _btn_Cancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
