Opis projektu



FitTrackPro to aplikacja internetowa służąca do kompleksowego zarządzania planami treningowymi fitness. System umożliwia przeglądanie bazy ćwiczeń, tworzenie własnych zestawów treningowych oraz zarządzanie treściami. Aplikacja posiada wbudowany system ról, który dzieli użytkowników na administratorów (zarządzających bazą ćwiczeń) oraz standardowych użytkowników.



Użyte technologie



Projekt został zrealizowany z wykorzystaniem następujących technologii i narzędzi:

\- Język: C# 

\- Framework: ASP.NET Core MVC (.NET 8.0)

\- ORM: Entity Framework Core

\- Baza danych: Microsoft SQL Server (LocalDB)

\- Autoryzacja i autentykacja: ASP.NET Core Identity

\- Frontend: HTML5, CSS, Bootstrap



Instrukcja uruchomienia (środowisko lokalne)



Aby uruchomić aplikację na lokalnym komputerze, postępuj zgodnie z poniższymi krokami:



1\. Sklonuj repozytorium:



git clone https://github.com/DanielG224/FitTrackPro.git



2\. Przejdź do katalogu z projektem:



cd FitTrackPro-main/FitTrackPro



3\. Zaktualizuj bazę danych (Entity Framework Core Tools):



Aplikacja korzysta z podejścia Code-First. Przed pierwszym uruchomieniem należy nałożyć migracje na lokalną bazę danych. W terminalu uruchom: dotnet ef database update



Alternatywnie można użyć polecenia Update-Database w konsoli Menedżera pakietów w Visual Studio.



4\. Uruchom aplikację:



dotnet run



Aplikacja będzie dostępna w przeglądarce pod adresem wskazanym w konsoli.



5\. Dostęp do konta Administratora:



Aby przetestować funkcje administracyjne, użyj poniższych danych logowania:

\- Email: admin@fittrack.com

\- Hasło: Admin123!

