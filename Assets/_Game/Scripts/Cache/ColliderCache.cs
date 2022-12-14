using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColliderCache
{
    static Dictionary<Collider, IHit> Collider_IHit_Dic = new Dictionary<Collider, IHit>();
    static Dictionary<Collider, Character> Collider_Character_Dic = new Dictionary<Collider, Character>();
    static Dictionary<Collider, Transform> Collider_Transform_Dic = new Dictionary<Collider, Transform>();
    static Dictionary<Collider, Transform> Collider_TransformAnim_Dic = new Dictionary<Collider, Transform>();

    public static IHit GetHit(Collider collider)
    {
        if (!Collider_IHit_Dic.ContainsKey(collider))
        {
            Collider_IHit_Dic.Add(collider, collider.GetComponent<CharacterCollider>().character.GetComponent<IHit>());
        }
        return Collider_IHit_Dic[collider];
    }

    public static Character GetCharacter(Collider collider)
    {
        if (!Collider_Character_Dic.ContainsKey(collider))
        {
            Collider_Character_Dic.Add(collider, collider.GetComponent<CharacterCollider>().character);
        }
        return Collider_Character_Dic[collider];
    }
    public static Transform GetTransform(Collider collider)
    {
        if (!Collider_Transform_Dic.ContainsKey(collider))
        {
            Collider_Transform_Dic.Add(collider, collider.GetComponent<CharacterCollider>().character.transform);
        }
        return Collider_Transform_Dic[collider];
    }
}
