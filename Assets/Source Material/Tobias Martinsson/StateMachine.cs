using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Main Author: Tobias Martinsson
public class StateMachine 
{
    private Dictionary<Type, State> InstantiatedStates = new Dictionary<Type, State>();

    private State CurrentState;

    public StateMachine(object Owner, State[] States)
    {
        Debug.Assert(States.Length > 0);
        foreach (State state in States)
        {
            State instantiated = UnityEngine.Object.Instantiate(state);
            instantiated.Initialize(this, Owner);
            InstantiatedStates.Add(state.GetType(), instantiated);
            if (!CurrentState)
                CurrentState = instantiated;

        }
        CurrentState?.Enter();
    }

    public void RunUpdate()
    {
        CurrentState?.RunUpdate();
    }

    public void ChangeState<T>() where T : State
    {
        if (InstantiatedStates.ContainsKey(typeof(T)))
        {
            State Instance = InstantiatedStates[typeof(T)];
            CurrentState?.Exit();
            CurrentState = Instance;
            CurrentState.Enter();
        }
    }
}
