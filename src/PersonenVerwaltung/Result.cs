namespace PersonenVerwaltung
{
	public readonly struct NoOpResult;
	public readonly union Result<T>(NoOpResult, T, Exception) where T : notnull;
}
