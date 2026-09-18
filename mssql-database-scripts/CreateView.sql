USE [PersonenVerwaltung]
GO

-- Erstelle View mit allen Informationen bezüglich zu Personen
BEGIN TRANSACTION
GO
CREATE VIEW PersonDetailAnsicht AS
SELECT
    person.Id,
    person.Name,
    person.Vorname,
    person.Geburtsdatum,
    address.Ort,
    address.Postleitzahl,
    address.Strasse,
    address.Hausnummer,
    phone.Nummer AS Telefonnummer
FROM dbo.Person person
LEFT JOIN dbo.Anschrift address ON person.Id = address.PersonId
LEFT JOIN dbo.Telefonverbindung phone ON person.Id = phone.PersonId;
GO
COMMIT
