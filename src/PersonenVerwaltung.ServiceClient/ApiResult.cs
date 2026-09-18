using System.Net;

namespace PersonenVerwaltung.ServiceClient
{
	public readonly struct Page<T>
	{
		public IReadOnlyList<T> Items
		{
			get => field ?? Array.Empty<T>();
			init;
		}

		public required int TotalCount
		{ get; init; }
	}

	public readonly union ApiResult(NoContent, ProblemDetails, Exception);
	public readonly union ApiResult<T>(T, ProblemDetails, Exception) where T : notnull;

	public readonly struct NoContent
	{
		public HttpStatusCode Status
		{ get; init; }
	}

	public readonly struct ProblemDetails
	{
		public HttpStatusCode Status
		{ get; init; }

		public required string Detail
		{ get; init; }

		public required string Instance
		{ get; init; }

		public required string Title
		{ get; init; }

		public required string Type
		{ get; init; }
	}
}
