# Narutodle 🍥

> A full-stack daily guessing game inspired by Wordle and Loldle, built with **ASP.NET Core (.NET 10)**, **Entity Framework Core**, and modern **Vanilla JavaScript**.

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-10-239120?style=flat&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![SQLite](https://img.shields.io/badge/SQLite-EF%20Core-003B57?style=flat&logo=sqlite&logoColor=white)](https://www.sqlite.org/)
[![JavaScript](https://img.shields.io/badge/JavaScript-ES6+-F7DF1E?style=flat&logo=javascript&logoColor=black)](https://developer.mozilla.org/)
[![CSS3](https://img.shields.io/badge/CSS3-Themed%20UI-1572B6?style=flat&logo=css3&logoColor=white)](https://www.w3.org/Style/CSS/)

---

## 📌 Overview

**Narutodle** is a web-based trivia game where players test their knowledge of the *Naruto* and *Naruto Shippūden* universe. Each day, a single shinobi is selected as the "Daily Ninja." Players submit guesses and receive color-coded feedback across multiple character attributes to deduce the mystery character.

Designed as a portfolio demonstration of full-stack engineering practices, showcasing clean architecture, deterministic backend logic, automated data ETL pipelines, and dependency-free frontend design.

---

## 🎮 Gameplay Mechanics

Players enter character names via a debounced search bar. Upon submitting a guess, the backend evaluates the guess against the daily target across 7 distinct criteria:

| Attribute | Comparison Mechanism | Feedback States |
| :--- | :--- | :--- |
| **Gender** | Exact Match | 🟩 Correct / 🟥 Incorrect |
| **Affiliation(s)** | Set Intersection | 🟩 Exact Match / 🟨 Partial Match / 🟥 No Match |
| **Jutsu Type(s)** | Set Intersection | 🟩 Exact Match / 🟨 Partial Match / 🟥 No Match |
| **Kekkei Genkai** | Categorized Grouping | 🟩 Exact Match / 🟨 Partial Match / 🟥 No Match |
| **Nature Type(s)** | Elemental Set Match | 🟩 Exact Match / 🟨 Partial Match / 🟥 No Match |
| **Classification(s)** | Role / Title Intersection | 🟩 Exact Match / 🟨 Partial Match / 🟥 No Match |
| **Debut Arc** | Chronological Comparison | 🟩 Correct / ⬆️ Earlier Arc / ⬇️ Later Arc |

Tiles animate sequentially using CSS 3D flip-reveal animations, providing an intuitive, polished user experience.

---

## 🏗️ Architecture & Technical Highlights

```
narutodle/
├── backend/narutodleAPI/        # ASP.NET Core Web API (.NET 10)
│   ├── controllers/             # REST API Controllers (GameController)
│   ├── data/                    # AppDbContext & DataBaseSeeder
│   ├── dtos/                    # Strongly-typed Request/Response DTOs
│   ├── enums/                   # Rich Domain Enums & metadata
│   ├── models/                  # Core Entity Models (Ninja)
│   ├── services/                # GameService & DailyNinjaService
│   └── utils/                   # Keyword parsers, JSON helpers, Enum extensions
├── frontend/                    # Responsive Single-Page Application
│   ├── assets/                  # Typography & elemental chakra SVG/PNG icons
│   ├── styles/                  # Anime parchment styling & keyframe animations
│   ├── index.html               # Semantic HTML with <template> elements
│   └── index.js                 # Client-side state, debounced search & fetch API
└── scrape/ScraperTool/          # CLI utility for automated API data extraction
```

### 1. Deterministic Daily Selection (Pseudo-Random Seeding)
- The daily character is chosen deterministically using a date seed: `(Year * 10000) + (Month * 100) + Day`.
- All players worldwide receive the identical character each day without requiring background cron jobs, scheduled tasks, or database mutations.
- Uses an in-memory caching layer (`DailyNinjaService`) to minimize database queries.

### 2. Automated ETL & Data Sanitization Pipeline
- **Scraper Utility** (`scrape/ScraperTool`): An isolated console tool that extracted, mapped, and audited raw character attributes from the external Dattebayo API.
- **Automated Seeder** (`DataBaseSeeder`): On initial launch, the application checks the SQLite database, automatically applies EF Core migrations, and ingests character data across paginated API endpoints.
- **Fuzzy Keyword Parser** (`NinjaDataParser`): Normalizes irregular anime classifications, nature releases, and Japanese transliterations (e.g., macrons like *ō* vs *o*) into strongly-typed C# enums.

### 3. Chronological Arc Traversal
- `StoryArc` enums are ordinally mapped according to anime chronology (from *Land of Waves* to *Blank Period*).
- Allows the comparison engine to return directional feedback (`Earlier` / `Later`), giving players strategic clues.

### 4. Zero-Dependency Client
- The frontend is engineered with vanilla JavaScript and CSS3—no heavy frameworks or node bundles required.
- Uses HTML5 `<template>` tags for clean DOM cloning and minimal layout thrashing.
- Implements client-side debouncing (300ms) to throttle autocomplete queries against the character roster.

---

## 🔌 API Reference

### `GET /api/game/names`
Retrieves character names and thumbnail URLs for autocomplete suggestions.

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
Submits a guess and evaluates character attributes against the daily mystery ninja.

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
- Any modern web browser (Chrome, Firefox, Safari, Edge)

### Installation & Run

1. **Clone the repository:**
   ```bash
   git clone https://github.com/pemcauwilla/narutodle-clone.git
   cd narutodle-clone
   ```

2. **Run the Backend API:**
   ```bash
   cd backend/narutodleAPI
   dotnet run
   ```
   > The API will start on `http://localhost:5256`. On first run, EF Core will automatically apply migrations and seed the database if needed.

3. **Launch the Frontend:**
   Open `frontend/index.html` directly in your browser or serve it using any local static file server (e.g. Live Server in VS Code, or `python3 -m http.server`):
   ```bash
   cd ../../frontend
   python3 -m http.server 8000
   # Open http://localhost:8000 in your browser
   ```

---

## 🛠️ Built With

- **Backend**: C#, ASP.NET Core Web API, Entity Framework Core, SQLite
- **Frontend**: Vanilla JavaScript (ES6+), HTML5 Templates, CSS3 Keyframes
- **Tools**: Git, .NET CLI, REST Client

---

## 📄 License

This project is licensed under the [MIT License](LICENSE) — free for educational and personal use.
Naruto and all associated characters are trademarks and copyright of Masashi Kishimoto / Shueisha.
