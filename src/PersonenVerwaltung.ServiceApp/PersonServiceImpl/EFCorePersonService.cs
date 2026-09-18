using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace PersonenVerwaltung.App.PersonServiceImpl
{
	public class EFCorePersonService : IPersonService
	{
		public EFCorePersonService(PersonalVerwaltungContext context) => _context = context;


		public async ValueTask<Result<PersonPage>> FilterPersonsAsync(Expression<Func<Person, bool>> predicate, int page = 0, int pagesize = 20, CancellationToken cancellationToken = default)
		{
			try
			{
				var newPredicate = RewritePredicate(predicate);
				int totalCount = await _context.Persons.Where(newPredicate).CountAsync();
				IReadOnlyList<Person> persons = await _context.Persons
					.Where(newPredicate)
					.Skip(page * pagesize)
					.Take(pagesize)
					.Include(e => e.Addresses)
					.Include(e => e.Phones)
					.Select(_personRecordMapping)
					.ToListAsync();

				return new PersonPage
				{
					Persons = persons,
					TotalCount = totalCount,
				};
			}
			catch ( Exception ex )
			{
				return ex;
			}
		}

		public async ValueTask<Result<Person>> GetPersonAsync(int id, CancellationToken cancellationToken = default)
		{
			try
			{
				Person person = await _context.Persons
					.Where(e => e.Id == id)
					.Include(e => e.Addresses)
					.Include(e => e.Phones)
					.Select(_personRecordMapping)
					.SingleOrDefaultAsync();

				if ( person.Id == 0 )
					return new KeyNotFoundException($"Person with id '{id}' not found");

				return person;
			}
			catch ( Exception ex )
			{
				return ex;
			}
		}

		public async ValueTask<Result<PersonPage>> ListPersonsAsync(int page = 0, int pagesize = 20, CancellationToken cancellationToken = default)
		{
			try
			{
				int totalCount = await _context.Persons.CountAsync();
				IReadOnlyList<Person> persons = await _context.Persons
					.Skip(page * pagesize)
					.Take(pagesize)
					.Include(e => e.Addresses)
					.Include(e => e.Phones)
					.Select(_personRecordMapping)
					.ToListAsync();

				return new PersonPage
				{
					Persons = persons,
					TotalCount = totalCount,
				};
			}
			catch ( Exception ex )
			{
				return ex;
			}
		}

		public async ValueTask<Result<Person>> UpdatePersonAsync(int id, PersonUpdate update, CancellationToken cancellationToken = default)
		{
			try
			{
				if ( update.Birthdate == null && update.FamilyName == null && update.GivenName == null )
					return await GetPersonAsync(id, cancellationToken);

				PersonRecord? record = await _context.Persons.Where(e => e.Id == id).Include(e => e.Addresses).Include(e => e.Phones).SingleOrDefaultAsync();

				if ( record == null )
					return new KeyNotFoundException($"Person with id '{id}' not found");

				record.Birthdate = update.Birthdate ?? record.Birthdate;
				record.FamilyName = update.FamilyName ?? record.FamilyName;
				record.GivenName = update.GivenName ?? record.GivenName;

				_context.SaveChanges();

				return await GetPersonAsync(id, cancellationToken);
			}
			catch ( Exception ex )
			{
				return ex;
			}
		}


		private static Expression<Func<PersonRecord, bool>> RewritePredicate(Expression<Func<Person, bool>> origin)
		{
			//Preparing new ParameterExpression for type 'PersonRecord' instead of 'Person'
			ParameterExpression newParameter = Expression.Parameter(typeof(PersonRecord), origin.Parameters[0].Name);

			//Rewrite LambdaExpression-Body so that all occurrences of the original ParameterExpression gets replaced by the new one
			return Expression.Lambda<Func<PersonRecord, bool>>(RewriteExpression(origin.Body, newParameter), newParameter);
		}

		private static Expression RewriteExpression(Expression expression, ParameterExpression newParameter)
		{
			// This is just a snippet to ensure that most predicates can be rewritten. For full functionality the evaluation of Expressions needs to be more diligent
			return expression switch
			{
				UnaryExpression ue => Expression.MakeUnary(ue.NodeType, RewriteExpression(ue.Operand, newParameter), (ue.Type == typeof(Person)) ? typeof(PersonRecord) : ue.Type),
				BinaryExpression be => Expression.MakeBinary(be.NodeType, RewriteExpression(be.Left, newParameter), RewriteExpression(be.Right, newParameter)),
				MemberExpression me => CreateMemberAccess(me, newParameter),
				MethodCallExpression mce => Expression.Call(RewriteExpression(mce.Object!, newParameter), mce.Method, mce.Arguments.Select(e => RewriteExpression(e, newParameter))),
				ParameterExpression => newParameter,
				_ => expression,
			};
		}

		private static MemberExpression CreateMemberAccess(MemberExpression me, ParameterExpression newParameter)
		{
			//Check if the current MemberAccess is for the original ParameterExpression. If so then remap the MemberAccess to 'PersonRecord'
			if ( me.Expression!.NodeType == ExpressionType.Parameter && me.Member.MemberType == System.Reflection.MemberTypes.Property && me.Member.DeclaringType == typeof(Person) )
				return Expression.MakeMemberAccess(newParameter, typeof(PersonRecord).GetProperty(me.Member.Name)!);

			return Expression.MakeMemberAccess(RewriteExpression(me.Expression!, newParameter), me.Member);
		}


		readonly static Expression<Func<PersonRecord, Person>>
			_personRecordMapping = record => new Person
			{
				Id = record.Id,
				Birthdate = record.Birthdate,
				FamilyName = record.FamilyName,
				GivenName = record.GivenName,
				Phones = record.Phones.Select(static e => new Phone
				{
					Number = e.Number,
				}).ToArray(),
				Addresses = record.Addresses.Select(static e => new Address
				{
					City = e.City,
					Street = e.Street,
					StreetNumber = e.StreetNumber,
					Zipcode = e.Zipcode,
				}).ToArray(),
			};


		readonly PersonalVerwaltungContext
			_context;
	}
}
