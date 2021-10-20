using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson
public abstract class State : ScriptableObject
{
    protected StateMachine StateMachine;
    protected object Owner;

    public void Initialize(StateMachine StateMachine, object Owner) {

        this.StateMachine = StateMachine;
        this.Owner = Owner;
        Initialize();
    }

    public virtual void Enter() { }
    public virtual void RunUpdate() { }
    public virtual void Exit() { }
    protected virtual void Initialize() { }
}
