namespace PersonenVerwaltung.App
{
	public readonly struct PersonDto
	{
		public required int Id
		{ get; init; }

		public IEnumerable<AddressDto> Addresses
		{ get; init; }

		public required DateOnly Birthdate
		{ get; init; }

		public required string FamilyName
		{ get; init; }

		public required string GivenName
		{ get; init; }

		public IEnumerable<PhoneDto> Phones
		{ get; init; }


		public static PersonDto FromPerson(Person person)
		{
			return new PersonDto
			{
				Addresses = (person.Addresses.Count > 0) ? person.Addresses.Select(AddressDto.FromAddress) : Array.Empty<AddressDto>(),
				Birthdate = person.Birthdate,
				FamilyName = person.FamilyName,
				GivenName = person.GivenName,
				Id = person.Id,
				Phones = (person.Phones.Count > 0) ? person.Phones.Select(PhoneDto.FromPhone) : Array.Empty<PhoneDto>(),
			};
		}
	}
}
