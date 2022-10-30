using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction<T>
{
    void Attack();
    void SelectTarget(T target);
    void UnSelectTarget(T target);
    void AddTagert(T target);
    void RemoveTarget(T target);
    Transform FindNearestEnemy();
}
