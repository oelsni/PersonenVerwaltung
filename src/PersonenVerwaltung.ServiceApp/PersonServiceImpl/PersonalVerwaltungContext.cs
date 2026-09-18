using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace PersonenVerwaltung.App.PersonServiceImpl
{
	public class PersonalVerwaltungContext : DbContext
	{
		public DbSet<AddressRecord> Addresses { get; set; }
		public DbSet<PersonRecord> Persons { get; set; }
		public DbSet<PhoneRecord> Phones { get; set; }


		public PersonalVerwaltungContext(DbContextOptions<PersonalVerwaltungContext> options) : base(options)
		{ }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PhoneRecord>(builder =>
			{
				builder
					.ToTable("Telefonverbindung")
					.HasKey(e => e.Id);

				builder
					.Property(e => e.Number)
					.IsRequired();

				builder
					.Property(e => e.PersonId)
					.IsRequired();

				builder
					.HasOne(e => e.Person)
					.WithMany(e => e.Phones)
					.HasForeignKey(e => e.PersonId);
			});

			modelBuilder.Entity<AddressRecord>(builder =>
			{
				builder
					.ToTable("Anschrift")
					.HasKey(e => e.Id);

				builder
					.Property(e => e.City)
					.IsRequired();

				builder
					.Property(e => e.Street)
					.IsRequired();

				builder
					.Property(e => e.StreetNumber)
					.IsRequired();

				builder
					.Property(e => e.Zipcode)
					.IsRequired();

				builder
					.HasOne(e => e.Person)
					.WithMany(e => e.Addresses)
					.HasForeignKey(e => e.PersonId);
			});

			modelBuilder.Entity<PersonRecord>(builder =>
			{
				builder
					.ToTable("Person")
					.HasKey(e => e.Id);

				builder
					.Property(e => e.Birthdate)
					.IsRequired();

				builder
					.Property(e => e.FamilyName)
					.IsRequired();

				builder
					.Property(e => e.GivenName)
					.IsRequired();

				builder
					.HasMany(e => e.Addresses)
					.WithOne(e => e.Person)
					.HasForeignKey(e => e.PersonId);

				builder
					.HasMany(e => e.Phones)
					.WithOne(e => e.Person)
					.HasForeignKey(e => e.PersonId);
			});
		}
	}

	public class PersonRecord
	{
		public int Id
		{ get; set; }

		public ICollection<AddressRecord> Addresses
		{
			get => field ?? Array.Empty<AddressRecord>();
			set;
		}

		[Column("Geburtsdatum")]
		public DateOnly Birthdate
		{ get; set; }

		[Column("Name")]
		public string FamilyName
		{
			get => field ?? "";
			set;
		}

		[Column("Vorname")]
		public string GivenName
		{
			get => field ?? "";
			set;
		}

		public List<PhoneRecord> Phones { get; } = new();
	}

	public class AddressRecord
	{
		public int Id
		{ get; set; }

		[Column("Ort")]
		public string City
		{
			get => field ?? "";
			set;
		}

		public int PersonId
		{ get; set; }

		[Column("Hausnummer")]
		public string StreetNumber
		{
			get => field ?? "";
			set;
		}

		[Column("Straße")]
		public string Street
		{
			get => field ?? "";
			set;
		}

		[Column("Postleitzahl")]
		public string Zipcode
		{
			get => field ?? "";
			set;
		}

		public PersonRecord? Person
		{ get; set; }
	}

	public class PhoneRecord
	{
		public int Id
		{ get; set; }

		[Column("Nummer")]
		public string Number
		{
			get => field ?? "";
			set;
		}

		public int PersonId
		{ get; set; }

		public PersonRecord? Person
		{ get; set; }
	}
}
