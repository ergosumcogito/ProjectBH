using System.Collections.Generic;
using UnityEngine;

public enum GameStateType
{
    MainMenu,
    Gameplay,
    Shop
}

public class GameStateMachine : MonoBehaviour
{
    public static GameStateMachine Instance;

    private Dictionary<GameStateType, GameState> _states;
    private GameState _current;

    [Header("Scene References")]
    public RoundSystem roundSystem;

    private void Awake()
    {
        Instance = this;

        _states = new Dictionary<GameStateType, GameState>()
        {
            { GameStateType.MainMenu, new MainMenuState(this) },
            { GameStateType.Gameplay, new GameplayState(this, roundSystem) },
            { GameStateType.Shop,     new ShopState(this) }
        };
    }

    private void Start()
    {
        ChangeState(GameStateType.MainMenu);
    }

    public void ChangeState(GameStateType type)
    {
        _current?.Exit();
        _current = _states[type];
        _current.Enter();
    }

    private void Update()
    {
        _current?.Update();
    }
}