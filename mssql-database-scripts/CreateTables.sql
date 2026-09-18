USE [PersonenVerwaltung]
GO

BEGIN TRANSACTION
GO
CREATE TABLE dbo.Person
	(
	Id int NOT NULL IDENTITY (1, 1) PRIMARY KEY,
	Name nvarchar(50) NOT NULL,
	Vorname nvarchar(50) NOT NULL,
	Geburtsdatum date NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Person SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO

BEGIN TRANSACTION
GO
CREATE TABLE dbo.Anschrift
	(
	Id int NOT NULL IDENTITY (1, 1) PRIMARY KEY,
	PersonId int NOT NULL,
	Postleitzahl nvarchar(10) NOT NULL,
	Ort nvarchar(50) NOT NULL,
	Straße nvarchar(50) NOT NULL,
	Hausnummer nvarchar(6) NOT NULL
	CONSTRAINT
		FK_Anschrift_Person FOREIGN KEY (PersonId)
		REFERENCES dbo.Person(Id)
		ON DELETE NO ACTION,
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Anschrift SET (LOCK_ESCALATION = TABLE)
GO
CREATE INDEX IX_Anschrift_PersonId
	ON Anschrift(PersonId)
GO
COMMIT
GO

BEGIN TRANSACTION
GO
CREATE TABLE dbo.Telefonverbindung
	(
	Id int NOT NULL IDENTITY (1, 1) PRIMARY KEY,
	PersonId int NOT NULL,
	Nummer nvarchar(20) NOT NULL
	CONSTRAINT
		FK_Telefonverbindung_Person FOREIGN KEY (PersonId)
		REFERENCES dbo.Person(Id)
		ON DELETE NO ACTION,
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Telefonverbindung SET (LOCK_ESCALATION = TABLE)
GO
CREATE INDEX IX_Telefonverbindung_PersonId
	ON Telefonverbindung(PersonId)
GO
COMMIT
