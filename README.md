# Narutodle 🍥

> A full-stack daily guessing game inspired by Wordle and Loldle, created to explore and learn the core fundamentals of **ASP.NET Core (.NET 10)**, **Entity Framework Core**, and decoupled frontend integration.

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-10-239120?style=flat&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![SQLite](https://img.shields.io/badge/SQLite-EF%20Core-003B57?style=flat&logo=sqlite&logoColor=white)](https://www.sqlite.org/)
[![JavaScript](https://img.shields.io/badge/JavaScript-ES6+-F7DF1E?style=flat&logo=javascript&logoColor=black)](https://developer.mozilla.org/)
[![CSS3](https://img.shields.io/badge/CSS3-Themed%20UI-1572B6?style=flat&logo=css3&logoColor=white)](https://www.w3.org/Style/CSS/)

---

## 📌 Project Purpose & Learning Goals

**Narutodle** was built as a practical, hands-on project to learn the foundations of the **ASP.NET Core** framework and C# backend development. The goal was to step through the complete lifecycle of creating a full-stack application from scratch:

- **ASP.NET Core Web API**: Routing, controllers, attribute routing, and action results.
- **Dependency Injection (DI)**: Registering and understanding service lifetimes (`AddScoped` for database-dependent game logic vs. `AddSingleton` for daily target caching).
- **Entity Framework Core (EF Core)**: Code-first modeling, SQLite integration, schema migrations, and LINQ queries.
- **Data Ingestion & Normalization**: Building a console scraper tool and seeder to fetch raw anime data from a remote REST API (`dattebayo-api.onrender.com`) and normalize irregular character fields into strongly-typed C# enums.
- **Cross-Origin Resource Sharing (CORS)**: Configuring secure policies to enable communication between a standalone frontend and the API server.

---

## 🎮 Gameplay Mechanics

Each day, a single shinobi is selected as the daily mystery character. Players type and select a character from an autocomplete dropdown list. Upon submitting a guess, the backend evaluates it across 7 distinct attributes:

| Attribute | Comparison Mechanism | Feedback States |
| :--- | :--- | :--- |
| **Gender** | Exact Match | 🟩 Correct / 🟥 Incorrect |
| **Affiliation(s)** | Set Intersection | 🟩 Exact Match / 🟨 Partial Match / 🟥 No Match |
| **Jutsu Type(s)** | Set Intersection | 🟩 Exact Match / 🟨 Partial Match / 🟥 No Match |
| **Kekkei Genkai** | Categorized Grouping | 🟩 Exact Match / 🟨 Partial Match / 🟥 No Match |
| **Nature Type(s)** | Elemental Set Match | 🟩 Exact Match / 🟨 Partial Match / 🟥 No Match |
| **Classification(s)** | Role / Title Intersection | 🟩 Exact Match / 🟨 Partial Match / 🟥 No Match |
| **Debut Arc** | Chronological Comparison | 🟩 Correct / ⬆️ Earlier Arc / ⬇️ Later Arc |

Tiles animate sequentially using CSS 3D flip-reveal animations.

---

## 🏗️ Architecture & How It Works

```
narutodle/
├── backend/narutodleAPI/        # ASP.NET Core Web API (.NET 10)
│   ├── controllers/             # REST API Controllers (GameController)
│   ├── data/                    # AppDbContext & DataBaseSeeder
│   ├── dtos/                    # Request/Response DTOs (GuessResultDto, MatchStatus)
│   ├── enums/                   # Strongly-typed Domain Enums
│   ├── models/                  # Entity Models (Ninja)
│   ├── services/                # GameService & DailyNinjaService
│   └── utils/                   # Keyword parsers, JSON helpers, Enum extensions
├── frontend/                    # Lightweight Vanilla Client
│   ├── assets/                  # Naruto font & elemental nature icons
│   ├── styles/                  # Parchment styling & keyframe animations
│   ├── index.html               # Semantic HTML with <template> elements
│   └── index.js                 # Autocomplete search & API fetch handling
└── scrape/ScraperTool/          # Auxiliary console scraper for API discovery
```

### Key Technical Concepts

1. **Deterministic Daily Target**:
   The daily character is calculated algorithmically using the UTC date seed: `(Year * 10000) + (Month * 100) + Day`. Every player receives the same mystery character without needing manual database resets or scheduled cron jobs. `DailyNinjaService` caches this in memory for the rest of the day.

2. **Custom Enum Keyword Parser**:
   Raw anime data from third-party APIs often contains non-standard text (e.g., Japanese macrons like *ō* vs *o*, irregular spellings). `NinjaDataParser` and `EnumKeywordParser` use keyword dictionaries to reliably map strings to typed enums (`Dojutsu`, `Jinchuriki`, `Sage`, etc.).

3. **Chronological Arc Comparison**:
   `StoryArc` enum values are ordered chronologically (from *Land of Waves* to *Blank Period*). The `CompareArcs` method uses ordinal integer values to determine whether the target debuted *earlier* or *later* in the timeline.

4. **Configurable Client Port**:
   The frontend communicates with the backend via `API_BASE_URL` defined at the top of `frontend/index.js` (pointing to `http://localhost:5256`).

---

## 🔌 API Reference

### `GET /api/game/names`
Retrieves character names and image thumbnails for the autocomplete list.

**Response `200 OK`:**
```json
[
  {
    "name": "Naruto Uzumaki",
    "imageUrl": "https://dattebayo-api.onrender.com/images/naruto.png"
  }
]
```

### `POST /api/game/guess/{guessedName}`
Submits a character guess and returns comparison feedback against today's target.

**Response `200 OK`:**
```json
{
  "name": "Kakashi Hatake",
  "imageUrl": "...",
  "isVictory": false,
  "gender": { "value": "Male", "status": 2 },
  "affiliations": { "value": ["Konohagakure"], "status": 2 },
  "jutsuTypes": { "value": ["Ninjutsu", "Taijutsu", "Genjutsu"], "status": 2 },
  "kekkeiGenkais": { "value": ["Dōjutsu"], "status": 1 },
  "natureTypes": { "value": ["Lightning", "Earth", "Water", "Fire", "Wind"], "status": 1 },
  "classifications": { "value": [], "status": 0 },
  "debutArc": { "value": "Land of Waves", "status": 2 }
}
```

*Status Codes: `0: Incorrect`, `1: Partial`, `2: Correct`, `3: Earlier`, `4: Later`*

---

## 🚀 Getting Started

### Prerequisites
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) (or .NET 8.0+)
- Any modern web browser

### 1. Run the Backend API
The backend is configured in `Properties/launchSettings.json` to listen on **port `5256`** (`http://localhost:5256`).

```bash
cd backend/narutodleAPI
dotnet run
```

> **Note on Port**: The frontend expects the backend at `http://localhost:5256`. If your system launches on a different port, update the `API_BASE_URL` variable at the top of `frontend/index.js`.

### 2. Run the Frontend
Open `frontend/index.html` directly in your browser or run a simple local static server:

```bash
cd frontend
python3 -m http.server 8000
# Navigate to http://localhost:8000 in your browser
```

---

## ⚠️ Known Limitations & Learning Takeaways

Because this project was developed as a learning playground for ASP.NET Core, there are several architectural constraints and areas for future growth:

- **Startup Seeding & External Dependency**: Seeding currently runs directly on application startup inside `Program.cs`. If the pre-seeded SQLite database is removed, the seeder fetches 100+ pages from the third-party Dattebayo API. Because Render's free tier spins down when idle, initial cold-start seeding may take 30–60 seconds or time out. A production design would use an asynchronous background service (`IHostedService`) or an offline migration script.
- **Client-Side Session State**: Game guesses and victory states exist solely in browser DOM memory. Reloading the page clears your current guesses. Persisting progress to `localStorage` or managing player sessions on the backend would improve the user experience.
- **Global UTC Reset vs. Local Time**: Daily ninja selection resets at 00:00 UTC globally. Players in other time zones experience the rollover during the day rather than at their local midnight, and there is currently no visual countdown timer.
- **Exact-String Matching**: Character queries match via exact case-insensitive strings (`Name.ToLower() == guessedName.ToLower()`). Minor spelling mistakes or special accents (e.g., *Chōji* vs *Choji*) won't match unless selected via the autocomplete dropdown. Implementing Levenshtein distance matching would add typo tolerance.
- **Automated Testing**: Game comparison logic and parsing rules were manually verified during development; writing an automated xUnit / Moq test suite for `GameService` and `NinjaDataParser` is the planned next step.

---

## 📄 License

This project is open-source for personal and educational learning.
Naruto and all related characters and trademarks belong to Masashi Kishimoto / Shueisha.
