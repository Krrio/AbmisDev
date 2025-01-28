# API Tasks - Dokumentacja Testów

## **Opis**

Dokumentacja zawiera przykłady testowych danych i wywołań API dla filtrów i sortowań w systemie zarządzania zadaniami. Wszystkie testy są dostosowane do aktualnych wymagań projektu.

---

## **Dane Testowe**

Przykładowe zadania w bazie danych:

```json
[
  {
    "title": "testowyuzytkownik",
    "description": "test",
    "dueDate": "2025-01-24T15:18:53.227Z",
    "itemStatus": 1,
    "priority": 0
  },
  {
    "title": "testowe zadanie",
    "description": "Opis testowego zadania",
    "dueDate": "2025-01-31T18:45:51.24Z",
    "itemStatus": 2,
    "priority": 1
  },
  {
    "title": "string",
    "description": "string",
    "dueDate": "2025-01-29T19:18:13.479Z",
    "itemStatus": 0,
    "priority": 0
  },
  {
    "title": "Zadanie na dzisiaj",
    "description": "To jest zadanie, które należy wykonać dzisiaj.",
    "dueDate": "2025-01-28T21:00:00Z",
    "itemStatus": 0,
    "priority": 2
  },
  {
    "title": "Zadanie na jutro",
    "description": "Zadanie zaplanowane na jutro.",
    "dueDate": "2025-01-29T14:00:00Z",
    "itemStatus": 1,
    "priority": 1
  },
  {
    "title": "Zadanie na ten tydzień",
    "description": "To zadanie należy ukończyć w ciągu tygodnia.",
    "dueDate": "2025-01-31T10:30:00Z",
    "itemStatus": 0,
    "priority": 0
  },
  {
    "title": "Zadanie na przyszły tydzień",
    "description": "Zadanie zaplanowane na następny tydzień.",
    "dueDate": "2025-02-05T08:00:00Z",
    "itemStatus": 0,
    "priority": 1
  }
]
```
1. Filtry timeRange
   
   GET /api/tasks/all?timeRange=today
   
   GET /api/tasks/all?timeRange=week
   
   GET /api/tasks/all?timeRange=overdue

**Customowy zakres**
GET /api/tasks/all?timeRange=custom
**WAŻNE!!!**
Format daty do customowego zakresu

Przykład: 2025-01-24T15:18:53.227Z

Oznacza to, że w polach startDate, endDate w swagerze wpisujemy datę w dokładnie takim formacie, URL Wygląda tak:

http://localhost:5185/api/Tasks/all?timeRange=custom&startDate=2025-01-24T15%3A18%3A53.227Z&endDate=2025-01-30T15%3A18%3A53.227Z&orderBy=asc

TODO: Przerobić ewentualny format, jeśli będą problemy na froncie

2. Sortowanie SortBy:

duedate,
priority,
status

Do tego pole OrderBy:

asc,
desc

Domyślnie jest zawsze asc
