using LunarsOfExiguity.Content.ItemTiers;

namespace LunarsOfExiguity.Content.Items;

public class PurifiedAutoCastEquipmentItem : ItemBase
{
    protected override string Name => "PurifiedAutoCastEquipment";

    protected override CombinedItemTier Tier => PurifiedTier.PurifiedItemTierDef;
}