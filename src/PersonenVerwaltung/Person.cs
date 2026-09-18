namespace PersonenVerwaltung
{
	public readonly struct Person
	{
		public required int Id
		{ get; init; }

		public IReadOnlyList<Address> Addresses
		{
			get => field ?? Array.Empty<Address>();
			init;
		}

		public required DateOnly Birthdate
		{ get; init; }

		public required string FamilyName
		{
			get => field ?? "";
			init;
		}

		public required string GivenName
		{
			get => field ?? "";
			init;
		}

		public IReadOnlyList<Phone> Phones
		{
			get => field ?? Array.Empty<Phone>();
			init;
		}
	}

	public readonly struct Address
	{
		public required string City
		{
			get => field ?? "";
			init;
		}

		public required string StreetNumber
		{
			get => field ?? "";
			init;
		}

		public required string Street
		{
			get => field ?? "";
			init;
		}

		public required string Zipcode
		{
			get => field ?? "";
			init;
		}
	}

	public readonly struct Phone
	{
		public required string Number
		{
			get => field ?? "";
			init;
		}
	}
}
