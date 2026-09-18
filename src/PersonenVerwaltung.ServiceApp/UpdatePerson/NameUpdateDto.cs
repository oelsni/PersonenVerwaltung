namespace PersonenVerwaltung.App.UpdatePerson
{
	public readonly struct NameUpdateDto
	{
		public string? FamilyName
		{ get; init; }

		public string? GivenName
		{ get; init; }
	}
}
