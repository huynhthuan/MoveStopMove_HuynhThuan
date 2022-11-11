using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateBot
{
    // Enter state
    void OnEnter(Bot bot);

    // Stay state
    void OnExecute(Bot bot);

    // Exit state
    void OnExit(Bot bot);
}