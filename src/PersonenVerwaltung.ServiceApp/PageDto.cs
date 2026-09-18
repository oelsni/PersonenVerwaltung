namespace PersonenVerwaltung.App
{
	public readonly struct PageDto<T> where T : notnull
	{
		public required IEnumerable<T> Items
		{ get; init; }

		public required int TotalCount
		{ get; init; }
	}
}
