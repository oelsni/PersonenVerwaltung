USE [PersonenVerwaltung]
GO

-- Lösche alle Telefonverbindungen die weder mit '0' noch mit '+' beginnen
BEGIN TRANSACTION
GO
DELETE FROM dbo.Telefonverbindung
WHERE Nummer NOT LIKE '0%' AND Nummer NOT LIKE '+%'
GO
COMMIT
