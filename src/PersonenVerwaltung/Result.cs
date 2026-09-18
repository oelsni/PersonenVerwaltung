namespace PersonenVerwaltung
{
	public readonly union Result<T>(T, Exception) where T : notnull;
}
