# Escape the Lab

A 2D action platformer built in Unity, featuring a custom hierarchical state machine architecture driving both player and enemy behaviour, telekinetic "force" combat mechanics, and reactive AI enemies.

> 🚧 **Work in progress** — this is a small solo project built to explore state machine architecture and 2D combat systems in Unity. Expect placeholder art and rough edges!

---

## Overview

You play as a character with telekinetic force powers, navigating a 2D platformer level while evading and fighting off enemies. Core mechanics include:

- **Mouse-aimed force powers** — push enemies away or dash (evade) in any direction relative to your cursor
- **Charge-based evade system** — a limited-charge dodge with cooldown and regeneration, encouraging tactical positioning rather than spam-dodging
- **Reactive enemy AI** — a "Scientist" enemy type that idles, chases, retreats, and shoots based on line-of-sight detection and distance to the player
- **Ledge-aware movement** — enemies use raycasts to detect platform edges and avoid walking off into out-of-bounds zones
- **Ragdoll/hit-reaction state** — enemies get knocked back and flash red when hit by a projectile
- **Win/lose loop** — reach the goal to win, fall out of bounds or die to restart the level

---

## Architecture

The core of this project is a **hierarchical finite state machine (FSM)** framework, shared between the player and all enemy types. Rather than hardcoding behaviour per-entity, both `PlayerScript` and enemy scripts (`ScientistScript`, `NPCScript`) extend a common `StateMachineCore`, and delegate all behaviour to swappable `State` objects.

### Why this matters

This isn't just `if (isRunning) { ... }` spaghetti — it's a proper state pattern implementation with:

- **Shared "blackboard" data** — states read shared context (rigidbody, health, hit status, ground checks) from the owning `StateMachineCore` without needing direct references passed around
- **Nested/hierarchical states** — a state can itself own a child `StateMachine` (e.g. `ShootingState` manages its own sub-states like `IdleState`/`ShootState`/`BackAwayState`), so complex behaviours are composed from simpler ones instead of one giant state
- **Consistent lifecycle** — every state implements `Enter()` / `Do()` / `FixedDo()` / `Exit()`, so transitions always clean up after themselves (e.g. resetting velocity, restoring sprite colour)
- **Reusable across entity types** — the player and enemies use completely different state sets, but run through the exact same machine

### Core framework files

| File                  | Responsibility                                                                                                                                                                                                                        |
| --------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `StateMachineCore.cs` | Abstract base for any entity driven by the FSM (player, enemies). Holds shared blackboard data (health, hit state, rigidbody, sprite renderer), handles taking damage from projectile collisions, and manages the hit-reaction flash. |
| `StateMachine.cs`     | Minimal state container — handles transitioning between states and can report the full active state chain (used for on-screen debug labels via gizmos).                                                                               |
| `State.cs`            | Abstract base for all states. Exposes blackboard data as protected properties, tracks time-in-state, and supports nesting via child `StateMachine` instances.                                                                         |

### Player states

Driven by `PlayerScript`, which reads input, resolves the active state each frame, and delegates physics/animation logic to:

- `IdleState`, `RunState`, `AirState` — grounded/airborne movement states, exit conditions based on ground detection
- `EvadeState` — directional dash using the mouse-aim vector, consumes an evade charge and resets cooldown
- `ForcePushState` — spawns and launches a projectile toward the cursor
- `DeadState` — freezes movement, triggers a level restart after a short delay

### Enemy states (Scientist)

Driven by `ScientistScript`, a ranged enemy with detection-radius-based awareness:

- `IdleState` — default, until the player is detected
- `ChaseState` — moves toward the player once detected but out of shooting range, respects ledge boundaries via `EdgeChecker`
- `ShootingState` (with nested `ShootState` / `BackAwayState`) — maintains distance from the player, repositions semi-randomly, retreats if the player gets too close, and fires on a timed interval
- `RagdollState` — triggered on taking damage; applies knockback and briefly disables normal control
- `DeadState` — enemy death/cleanup

### Enemy states (NPC / Patrol)

A simpler enemy/NPC type (`NPCScript`) that just wanders between two anchor points:

- `PatrolState` → `NavigateState` — picks a random point between two anchors, walks to it, idles, repeats

---

## Combat & Projectiles

- `ProjectileScript` — self-destructs on collision or after a max lifetime, carries a configurable `damageAmount`
- Damage is applied via collision detection in `StateMachineCore`, which also determines knockback direction from the projectile's origin and triggers the hit-reaction coroutine (red flash + temporary ragdoll)
- `ForceDirectionScript` — converts mouse position into a world-space aim direction, drives both the force-push throw direction and the evade dash direction, with a UI reticle and directional arrow for visual feedback

---

## Support Systems

| File                    | Purpose                                                                                                                            |
| ----------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| `GroundColliderScript`  | Trigger-based ground detection feeding into movement state exit conditions                                                         |
| `EdgeChecker`           | Raycasts down from both sides of an enemy to detect platform edges/out-of-bounds zones, preventing enemies from walking off ledges |
| `DetectionRadiusScript` | Trigger-based player detection radius, tracks both current and "ever seen" detection state                                         |
| `ShootIntervalScript`   | Simple cooldown timer gating how often an enemy can fire                                                                           |
| `CameraScript`          | Follows the player on X/Y with a configurable vertical offset                                                                      |
| `OutOfBoundsScript`     | Restarts the level if the player falls out of bounds; destroys enemies that do                                                     |
| `GoalScript`            | Triggers a win state and restarts the level after a delay                                                                          |

## UI

- `HealthBarScript` — renders player health as a percentage
- `EvadeUIScript` — renders evade cooldown percentage and remaining charge count

---

## Tech Stack

- **Engine:** Unity (2D, URP-ready physics via `Physics2D`)
- **Language:** C#
- **UI:** TextMeshPro
- Custom hierarchical FSM (no external state machine asset/plugin used)

---

## Known Limitations / Roadmap

This is an active learning project, so a few things are intentionally rough or unfinished:

- `EnemyScript` has an older, mostly-stubbed damage path that's since been superseded by the `StateMachineCore` collision handling
- `RetreatState` is currently an empty stub
- `ForcePushState` has no defined end condition yet
- Patrol/NPC enemies don't yet respond to combat (no ragdoll/dead states wired in)
- Player scaling on crouch (`Scale()`) is a placeholder squash effect rather than a proper crouch animation

---

## Running the Project

1. Clone the repo
2. Open with Unity Hub (see `ProjectSettings` for the exact editor version)
3. Open the main scene and hit Play

**Controls:**

- `A` / `D` or arrow keys — move
- `S` / Down — crouch (scale)
- `Space` — jump
- `Left Click` — force push (aimed at cursor)
- `Right Click` — evade dash (aimed at cursor, limited charges)
