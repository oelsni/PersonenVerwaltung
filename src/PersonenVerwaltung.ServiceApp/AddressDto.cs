namespace PersonenVerwaltung.App
{
	public readonly struct AddressDto
	{
		public required string City
		{ get; init; }

		public required string StreetNumber
		{ get; init; }

		public required string Street
		{ get; init; }

		public required string Zipcode
		{ get; init; }


		public static AddressDto FromAddress(Address address)
		{
			return new AddressDto
			{
				City = address.City,
				Street = address.Street,
				StreetNumber = address.StreetNumber,
				Zipcode = address.Zipcode,
			};
		}
	}
}
