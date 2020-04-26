using System;
// using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;
// using UnityEngine.UI;

public class GameStateAttribute : Attribute
{

}

public class GameStateCtrl
{
    private StateMachine gameState = new StateMachine();
    public void Initialize()
    {
        gameState.RegisterStateByAttributes<GameStateAttribute>(typeof(GameStateAttribute).Assembly);

    }

    public void GotoState(string name, StateSwitchCtx inSwitchCtx = null)
    {
        IState tempState = new LoginState();
        gameState.ChangeState(tempState, inSwitchCtx);
    }

    public IState GetState()
    {
        return gameState.TopState();
    }

    public bool isLoginState
    {
        get { return (gameState.TopState() as LoginState) != null; }
    }
}

public class StateMachine
{
    private Stack<IState> _stateStack = new Stack<IState>();
    //private IState _registedState;
    public ClassEnumerator RegisterStateByAttributes<TAttributeType>(Assembly InAsswmbly)
    where TAttributeType : GameStateAttribute
    {
        var Classes = new ClassEnumerator(
            typeof(TAttributeType),
            typeof(IState),
            InAsswmbly);
        var Iter = Classes.results.GetEnumerator();
        while (Iter.MoveNext())
        {
            var StateType = Iter.Current;
            IState StateObj = (IState)System.Activator.CreateInstance(StateType);

            // if (!_registedState.ContainsKey(StateObj.name))
            // {
            //     _registedState.Add(StateObj.name, StateObj);
            // }
        }
        return Classes;
    }

    public IState ChangeState(IState state, StateSwitchCtx inSwithcCtx)
    {
        if (state == null)
        {
            return default;
        }

        IState oldState = default;

        if (_stateStack.Count > 0)
        {
            oldState = _stateStack.Pop();
            oldState.OnStateLeave();
        }

        _stateStack.Push(state);
        state.OnStateEnter(inSwithcCtx);

        return oldState;
    }
    public IState TopState()
    {
        return _stateStack.Pop();
    }
}

public class ClassEnumerator
{
    protected List<Type> Results = new List<Type>();

    public List<Type> results { get { return Results; } }
    public ClassEnumerator(Type InAttributeType, Type InInterfaceType, Assembly InAssembly)
    {
        Type[] Types = InAssembly.GetTypes();
        if (Types != null)
        {
            for (int i = 0; i < Types.Length; ++i)
            {
                var t = Types[i];
                if (InInterfaceType.IsAssignableFrom(t))
                {
                    if (!t.IsAbstract)
                    {
                        if (t.GetCustomAttributes(InAttributeType, false).Length > 0)
                        {
                            Results.Add(t);
                        }
                    }
                }
            }
        }
    }

}

public class StateSwitchCtx
{

}

public interface IState
{
    void OnStateEnter(StateSwitchCtx inSwithcCtx);

    void OnStateLeave();
    void OnStateOverride();
    void OnStateResume();
    string name { get; }
}

public abstract class BaseState : IState
{
    public virtual void OnStateEnter(StateSwitchCtx inSwitchCtx) { }
    public virtual void OnStateLeave() { }
    public virtual void OnStateOverride() { }

    public virtual void OnStateResume() { }
    public virtual string name { get { return GetType().Name; } }

}

[GameState]
public class LoginState : BaseState
{
    public override void OnStateEnter(StateSwitchCtx inSwitchCtx)
    {
        Debug.Log("LoginSceneOnStateEnger");
    }

    public override void OnStateLeave()
    {
        Debug.Log("LoginScene OnStateLeave");
    }
}
