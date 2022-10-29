public enum EquipmentSlotType
{
    BELT_SLOT,
    CLOTH_SLOT,
    GLOVE_SLOT,
    HAT_SLOT,
    SHOE_SLOT,
    SHOULDER_PAD_SLOT,
    BACK_PACK_SLOT,
    WEAPON_SLOT,
    SHIELD_SLOT
}

public class PlayerData
{
    public EquipmentSlot[] equipmentSlot = new EquipmentSlot[9];
    public int gold;
    public int currentDungeon;
}

public class EquipmentSlot
{
    public EquipmentType equipment_type;
    public string equipment_object_name;
    public int equipment_material;
}
