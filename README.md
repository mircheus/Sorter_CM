# Sorter_CM

A simple children's game about sorting geometric shapes, made with Unity.

Shapes spawn and travel along moving lines. The player drags each shape into the matching slot before it falls off the screen. Correct matches award score; wrong matches or missed shapes cost health. The round ends when all required shapes have been sorted (win) or health runs out (lose).

## Gameplay

- Drag a shape onto a slot of the same type to score a point.
- Dropping a shape on the wrong slot, or letting it reach the death zone, costs health.
- The level ends in a win when the target number of shapes have been sorted, or in a loss when health hits zero.

## Tech

- **Engine:** Unity 6 (`6000.0.60f1`), Universal Render Pipeline (2D)
- **DI:** [Zenject / Extenject](https://github.com/modesttree/Zenject) for scene-level bindings (`GameInstaller`)
- **Architecture:**
  - Custom lightweight `EventBus` with typed subscriber interfaces (`IUpdateUIEvents`, `IFigureSnapEvent`, `IEndGameEvents`, `IPausable`, `IResettable`, …) for decoupled communication between the model, spawner, slots and UI.
  - Generic `ObjectPool<T>` reused by the `FigureFactory` to recycle shapes.
  - `ScriptableObject` configs: `LevelSettings` (HP, score, target shape count), `SpawnerSettings` (speed / timeout ranges), `FigureType` and `FiguresList`.
  - MVC-ish split: `Model` holds gameplay state, `GameController` orchestrates lifecycle, views (`ScoreView`, `HealthView`, `EndGameView`, …) react to events.

## Project layout

```
Assets/
├── Game/
│   ├── Scenes/        # Game.unity — main playable scene
│   ├── Scripts/
│   │   ├── DragAndDrop/    # Input handling for draggable shapes
│   │   ├── EventBus/       # Typed pub/sub
│   │   ├── Events/         # Event interfaces
│   │   ├── FigureFactory/  # Figure, FigureType, FigureFactory, FiguresList
│   │   ├── Infrastructure/ # GameController, GameInstaller, Model, LevelSettings
│   │   ├── ObjectPool/     # Generic pool + IPoolable
│   │   ├── Slots/          # DropSlot — accepts/rejects figures
│   │   ├── Spawner/        # FigureSpawner, MoveLine, DeathZone
│   │   └── UI/             # MainUIController + views
│   ├── Prefabs/
│   ├── Graphics/
│   └── Settings/
├── ExternalAssets/    # Third-party UI / art
└── Plugins/Zenject/
```

## Getting started

1. Install Unity **6000.0.60f1** (or open with Unity Hub — the version is pinned in `ProjectSettings/ProjectVersion.txt`).
2. Clone the repo and open the project folder in Unity Hub.
3. Open `Assets/Game/Scenes/Game.unity`.
4. Press **Play**.

## Tweaking a level

Level balance lives in ScriptableObject assets — no code changes needed:

- **LevelSettings** — `HealthPoints`, `Score`, `FiguresToSortMin/Max`
- **SpawnerSettings** — min/max spawn timeout, min/max figure speed
- **FiguresList** — the set of `FigureType` assets in rotation

Both settings assets are wired into `GameInstaller` on the scene.
