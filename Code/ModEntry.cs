using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace Sts2CustomCards;

/// <summary>
/// Mod entry point. Registers every custom card into its card pool.
/// Add new cards in Code/Cards/ and register them here.
/// </summary>
[ModInitializer("Initialize")]
public static class ModEntry
{
    public static void Initialize()
    {
        ModHelper.AddModelToPool<ColorlessCardPool, Cards.ProtectMeSquireCard>();
        ModHelper.AddModelToPool<ColorlessCardPool, Cards.TheFoolCard>();
    }
}
