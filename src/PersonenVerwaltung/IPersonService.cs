using System.Linq.Expressions;

namespace PersonenVerwaltung
{
	public readonly struct PersonPage
	{
		public required IReadOnlyList<Person> Persons
		{ get; init; }

		public required int TotalCount
		{ get; init; }
	}

	public interface IPersonService
	{
		public ValueTask<Result<Person>> GetPersonAsync(int id, CancellationToken cancellationToken = default);
		public ValueTask<Result<PersonPage>> FilterPersonsAsync(Expression<Func<Person, bool>> predicate, int page = 0, int pagesize = 20, CancellationToken cancellationToken = default);
		public ValueTask<Result<PersonPage>> ListPersonsAsync(int page = 0, int pagesize = 20, CancellationToken cancellationToken = default);
		public ValueTask<Result<Person>> UpdatePersonAsync(int id, PersonUpdate update, CancellationToken cancellationToken = default);
	}
}
