using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStates
{
    void Enter(Enemy parent);
    void Update();
    void Exit();
}
