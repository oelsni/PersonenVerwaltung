namespace PersonenVerwaltung
{
	public readonly struct PersonUpdate
	{
		public DateOnly? Birthdate
		{ get; init; }

		public string? FamilyName
		{ get; init; }

		public string? GivenName
		{ get; init; }
	}
}
