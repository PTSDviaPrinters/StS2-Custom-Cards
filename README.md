# StS2 Custom Cards

Custom card collection for Slay the Spire 2 — C# mod, built against the vanilla loader (no BaseLib dependency).

## Cards

| Card | Cost | Rarity | Pool | Effect |
|---|---|---|---|---|
| **Protect Me Squire!** | 0 | Uncommon | Colorless | Multiplayer-only. Deal 15 (20) damage to a teammate, then gain 15 (20) Block. Drinking-game card. |

## Requirements
- Slay the Spire 2 (tested on v0.111.0)
- .NET 9 SDK

## Build & Install

```bash
dotnet build Sts2CustomCards.csproj -c Release \
  -p:Sts2GameDir="M:/SteamLibrary/steamapps/common/Slay the Spire 2" \
  -p:ModsPath="M:/SteamLibrary/steamapps/common/Slay the Spire 2/mods/"
```

Copies `dll` + `json` + `pck` into `<game>/mods/Sts2CustomCards/`. Enable the mod in-game (Settings → Mods; first launch shows a consent dialog).

## Adding a card
1. Add `Code/Cards/<Name>Card.cs` (extend `CardModel`)
2. Add localization keys in `Sts2CustomCards/localization/eng/cards.json`
3. Register in `Code/ModEntry.cs` with `ModHelper.AddModelToPool<...Pool, NameCard>()`
4. Rebuild — deployment is automatic

## Layout
```
Code/ModEntry.cs                     # [ModInitializer] entry point + registrations
Code/Cards/….cs                      # card implementations
Sts2CustomCards/localization/eng/    # localization JSON (packed into .pck at build)
```
