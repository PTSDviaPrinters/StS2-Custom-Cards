using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Sts2CustomCards.Cards;

/// <summary>
/// Protect Me Squire! — 0-cost Colorless multiplayer Skill.
/// Deal 15 damage to a teammate, then gain 15 Block.
/// Upgrade: gains Innate (starts in your opening hand).
/// </summary>
public sealed class ProtectMeSquireCard : CardModel
{
    public const decimal TakeDamage = 15m;

    public override CardMultiplayerConstraint MultiplayerConstraint =>
        CardMultiplayerConstraint.MultiplayerOnly;

    public override string PortraitPath =>
        "res://Sts2CustomCards/images/cards/protect_me_squire.png";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(TakeDamage, ValueProp.Unpowered),
        new BlockVar(TakeDamage, ValueProp.Move)
    ];

    public ProtectMeSquireCard()
        : base(
            canonicalEnergyCost: 0,
            type: CardType.Skill,
            rarity: CardRarity.Uncommon,
            targetType: TargetType.AnyAlly)
    {
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");

        // Hurt the chosen teammate (unpowered: ignores Strength/Dex so the self-damage
        // never scales out of control).
        await CreatureCmd.Damage(
            choiceContext,
            cardPlay.Target,
            DynamicVars.Damage.BaseValue,
            ValueProp.Unpowered,
            Owner.Creature);

        // Gain Block regardless of how much damage the teammate took.
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}
