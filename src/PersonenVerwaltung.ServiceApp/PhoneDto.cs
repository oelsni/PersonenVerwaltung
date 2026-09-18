namespace PersonenVerwaltung.App
{
	public readonly struct PhoneDto
	{
		public required string Number
		{ get; init; }


		public static PhoneDto FromPhone(Phone phone)
		{
			return new PhoneDto
			{
				Number = phone.Number,
			};
		}
	}
}
