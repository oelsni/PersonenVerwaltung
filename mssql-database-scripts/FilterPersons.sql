USE [PersonenVerwaltung]
GO

-- Anzahl aller Personen in Datenbank
SELECT COUNT(Id) AS AnzahlPersonen FROM dbo.Person
GO

-- Anzahl aller Personen die in Dresden leben
SELECT COUNT(dbo.Person.Id) AS AnzahlPersonenInDresden FROM dbo.Person
JOIN dbo.Anschrift ON PersonId = dbo.Person.Id
WHERE Ort = 'Dresden'
GO

-- Alle Personen die mehr als eine Telefonverbindung besitzen. (Vorbereitung um Anzahl zu ermitteln)
WITH PersonenMitMehrerenNummern AS (
	SELECT PersonId FROM dbo.Telefonverbindung
	GROUP BY PersonId
	HAVING COUNT(Id) > 1
)

-- Anzahl aller Personen mit mehr als einer Telefonverbindung
SELECT COUNT(*) AS AnzahlPersonenMitMehrerenNummern FROM PersonenMitMehrerenNummern
