# Allgemein 
Ich habe mich dazu entschieden diese Aufgabenstellung in .net11 mit C#-Preview um zu setzen, da nun Unions zur Verfügung stehen.

SumTypes/Unions sind extrem mächtig in Hinsicht der Erzwingung eines geregelten Kontrollflusses.

Original hatte in das Projekt in .net10 mit Quellcode-Generatoren begonnen welche mir Typen generieren die das verhalten von Unions anbieten, allerdings ist es nicht möglich diese mit Switch-Expressions einzusetzen.

In .net11 ist dies mit den Spracheigenen Unions möglich wodurch sich die Leserlichkeit sowie der Komfort extrem verbessert hat

Ich nutze Unions um Result-Typen zu definieren, welche verschiedene Ergebnisse darstellen können und somit mir erlauben Fehler(Exception) besser handhaben zu können.


# Voraussetzungen
Zur Ausführung des Projektes 'PersonenVerwaltung.FormsAppHost' (Aspire) muss sicher gestellt sein das die Aspire-CLI installiert ist.

Da der Client eine Windows-Forms Anwendung ist kann das Projekt nur unter Windows ausgeführt werden.

Um die Aspire-CLI zu installieren reicht es folgendes Power-Script auszuführen:
```
irm https://aspire.dev/install.ps1 | iex
```

Für das Test-Projekt ist Docker-Desktop oder vergleichbarem von Nöten da dieses TestContainers verwendet um Integrationtests zu ermöglichen.

# Datenbank-Aufgaben
Die Aufgaben hinsichtlich der Datenbank habe ich in MsSql umgesetzt und sind im Ordner 'mssql-database-scripts' zu finden.

Die Scripts sind nach Teilaufgaben Unterteilt:
- Datenbank erstellt:
  - CreateDatabase.sql
- Tabellen erstellen mit relationalen Einschränkungen (Person / Anschrift / Telefonverbindung)
  - CreateTables.sql
- Tabellen mit Daten füllen wobei gilt, jede Person hat eine Anschrift und keine bis mehrere Telefonverbindungen
  - SeedTables.sql (168 Nummern mit + beginnend / 152 Nummern mit 0 beginnend / 40 Nummern ohne 0 oder + am Anfang)
- Anzahl aller Personen sowie die Anzahl von Personen mit mehr als einer Telefonverbindung als auch Anzahl der Personen wie in Dresden ihre Anschrift haben
  - FilterPersons.sql
- Telefonverbindungen löschen wo die Nummer weder mit 0 noch + beginnen
  - DeletePhoneNumbers.sql
- Ansicht erstellen für Person einschließlich der Anschrift sowie Telefonnummer
  - CreateView.sql
- Neue Spalte zu Person-Entität hinzufügen and diese mit dem Namen in Großbuchstaben befüllen der jeweiligen Person befüllen
  - AddAndUpdateColumn.sql

# Projektmappen-Aufbau
Die Projektmappe beinhaltet folgende Projekte:
- PersonenVerwaltung
  - Abstraktion-Elemente + Daten-Typen für den WebService
- PersonenVerwaltung.ServiceApp
  - ASP.Net-Anwendung mit Minimal-Api-Endpunkten
  - Beinhalten zudem die Implementierung der Abstraktion-Elemente (IPersonService) unter Verwendung von EFCore
- PersonenVerwaltung.ServiceClient:
  - Api-Client für den WebService (PersonenVerwaltungClient)
- PersonenVerwaltung.FormsClient
  - Windows-Forms-Anwendung welche den Client aus dem Projekt 'PersonenVerwaltung.ServiceClient' verwendet
  - Die FormsApp beinhaltet alle pflicht sowie optionale Anforderungen
- PersonenVerwaltung.FormsAppHost
  - .Net-Aspire-Projekt welches den WebService sowie die FormsApp ausführen und einfache Telemetrie über ein Dashboard im Browser zur Verfügung stellt
- PersonenVerwaltung.FormsApp.Tests
  - Test-Projekt welches TestContainers verwendet um einen vollen Integrationstest durch zu führen
  - Ähnlich wie das Aspire-Projekt greift dasd Test-projekt auf den WebService sowie auf den ServiceClient zu um die Integration von ServiceClient bis Datenbank zu testen 