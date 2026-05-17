# ⚔️ FateArena

A turn-based fantasy battle game built in C# demonstrating core OOP concepts through an interactive dice-combat system.

---

## 🎮 Gameplay

Players choose 2–4 characters to battle in sequence. Each turn, the attacker rolls a dice that determines the outcome:

| Roll | Result | Effect |
|------|--------|--------|
| 1–3  | MISS | Attack fails |
| 4–7  | HIT | Defender can attempt to block |
| 8–10 | CRITICAL | ×1.5 damage, unblockable |

On a HIT, the defender rolls to block — a result of 5 or higher deflects the attack entirely.

---

## 🧙 Characters

| Class | HP | Power | Defense | Special |
|-------|----|-------|---------|---------|
| ⚔️ Warrior | 120 | 22 | 12 | Shield bonus (+8 defense) |
| 🔮 Mage | 75 | 35 | 4 | Mana system (100 MP, costs 15/cast) |
| 🏹 Archer | 95 | 28 | 8 | 12 arrows (high dice scaling) |

---

## 🧱 OOP Concepts Demonstrated

- **Inheritance** — `Warrior`, `Mage`, `Archer` all inherit from abstract base class `Character`
- **Polymorphism** — `DamageCalculate()` is abstract and overridden in each subclass; `List<Character>` holds mixed types
- **Encapsulation** — Properties with validated setters (HP clamped to `[0, maxHP]`, etc.)
- **Constructors** — Base constructor chaining via `: base(...)` for shared initialization
- **Static Members** — `Character.TotalCharacters` and `Arena.TotalBattles` as class-level counters
- **Name Hiding** — `Info()` uses `override` + `base.Info()` for polymorphic display with extension
- **Memory Management** — `using` blocks for file streams (auto-dispose via `IDisposable`)

---

## 📁 Project Structure

```
FateArena/
├── Character.cs   # Abstract base class (shared stats, HP logic, logging)
├── Warrior.cs     # Tank — high HP and defense
├── Mage.cs        # Glass cannon — mana-powered spells
├── Archer.cs      # Balanced — arrow resource management
├── Arena.cs       # Battle engine — turn loop, dice resolution
├── Dice.cs        # Static utility — dice rolls + ZarSonucu wrapper
└── Program.cs     # Entry point — character selection, game loop
```

---

## 🚀 Running the Project

```bash
# Clone the repository
git clone https://github.com/yourusername/FateArena.git

# Open in Visual Studio and run, or use .NET CLI:
dotnet run
```

**Requirements:** .NET 6.0 or later

---

## 📋 Features

- Turn-based combat with dice randomness
- Interactive block mechanic on HIT rolls
- Per-character resource systems (mana / arrows)
- HP bar visualization in console output
- Battle logging to `FateArena_Log.txt`
- Multi-battle session with cumulative stats
- 50-turn draw condition

---

*Built for a C# OOP course project.*
