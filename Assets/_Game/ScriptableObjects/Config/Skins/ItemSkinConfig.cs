using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkinType
{
    MOUTH,
    CROWN,
    HEADPHONE,
    HAT,
    PANTS,
    SHIELD
}

public class ItemSkinConfig : ItemConfig
{
    public Vector3 positionOnHand;
}