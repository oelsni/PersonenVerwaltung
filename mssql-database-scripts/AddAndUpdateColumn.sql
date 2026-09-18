USE [PersonenVerwaltung]
GO

-- Neue Spalte zu Person hinzufügen welche normalisierten Namen beinhaltet (Name in Großbuchstaben)
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Person
ADD NameNormalisiert NVARCHAR(100);
GO
COMMIT
GO

-- Befülle Spalte 'NameNormalisiert' mit Name in Großbuchstaben für alle Personen
BEGIN TRANSACTION
GO
UPDATE dbo.Person
SET NameNormalisiert = UPPER(Name);
GO
COMMIT
