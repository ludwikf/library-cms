Dokumentacja projektu

Instalacja

1. **Klonowanie repozytorium**:

   git clone https://github.com/ludwikf/library-cms

- Aplikacja umożliwia:
  - Rejestrację użytkowników.
  - Logowanie i autoryzację.
  - Zarządzanie książkami (dodawanie, edytowanie, usuwanie).
  - Rezerwację książek przez użytkowników.
  - Przeglądanie zarezerwowanych książek.

4. Testy użytkowników

- Użytkownicy powinni być w stanie:

  - Zarejestrować się, podając unikalną nazwę użytkownika i hasło.
  - Zalogować się przy użyciu zarejestrowanych danych.
  - Dodawać nowe książki (tylko administratorzy).
  - Rezerwować książki.
  - Przeglądać swoje zarezerwowane książki.

  Konta testowe:

  - admin, 123
  - guest, 123

5. Opis działania aplikacji z punktu widzenia użytkownika

Rejestracja

- Użytkownik wprowadza nazwę użytkownika, hasło i potwierdza hasło.
- Po pomyślnej rejestracji użytkownik jest przekierowywany na stronę logowania.

Logowanie

- Użytkownik wprowadza nazwę użytkownika i hasło.
- Po pomyślnym zalogowaniu użytkownik jest przekierowywany na stronę główną.

Zarządzanie książkami

- Administratorzy mogą dodawać nowe książki, edytować istniejące i usuwać książki.
- Użytkownicy mogą przeglądać dostępne książki i rezerwować je.

Rezerwacja książek

- Użytkownik klika przycisk rezerwacji obok książki.
- Po pomyślnej rezerwacji użytkownik otrzymuje komunikat o sukcesie.

Przeglądanie zarezerwowanych książek

- Użytkownik może przeglądać listę swoich zarezerwowanych książek na dedykowanej stronie.
