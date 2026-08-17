using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Sts2CustomCards.Cards;

/// <summary>
/// The Fool — 1-cost Colorless Rare Skill.
/// Base:     Exhaust. Play the last card you played again.
/// Upgraded: Exhaust. Play the last card played by anyone again.
/// Uses CardCmd.AutoPlay (same engine command as vanilla Decisions/Decisions).
/// </summary>
public sealed class TheFoolCard : CardModel
{
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [CardKeyword.Exhaust];

    public TheFoolCard()
        : base(
            canonicalEnergyCost: 1,
            type: CardType.Skill,
            rarity: CardRarity.Rare,
            targetType: TargetType.Self)
    {
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        // Pull the last finished card play, newest first, skipping The Fool itself.
        CardPlayFinishedEntry? last = null;
        foreach (var entry in CombatManager.Instance.History.CardPlaysFinished.Reverse())
        {
            if (entry.CardPlay.Card == this)
                continue;

            if (IsUpgraded || entry.CardPlay.Player == Owner)
            {
                last = entry;
                break;
            }
        }

        if (last == null || last.CardPlay.Card.Keywords.Contains(CardKeyword.Unplayable))
            return;

        await CardCmd.AutoPlay(
            choiceContext,
            last.CardPlay.Card,
            last.CardPlay.Target);
    }

    protected override void OnUpgrade()
    {
        // No stat change — the upgrade extends reach to ANY player's last card.
    }
}
