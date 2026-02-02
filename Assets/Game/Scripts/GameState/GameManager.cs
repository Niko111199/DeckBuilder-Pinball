using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //TODO: make pusable to Reset Game
    //TODO: General clean up
    public GameState currentState;
    public Score score;
    public int requredScore;
    public int currentRound = 1;
    public int FinalRound = 8;
    public int numberOfBalls = 3;
    public int currentNumberOfBalls;
    public ObjectPool ballPool;
    //TODO: Abstract out the shop system
    public GameObject Shop;
    public bool IsShopOpen = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ChangeState(new RoundState(this));
    }

    void Update()
    {
        currentState?.Update();  
    }

    public void ChangeState(GameState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState.Enter();
    }

    //TODO: Abstract out the shop system
    public void CloseShop()
    {
        if (IsShopOpen)
        {
            IsShopOpen = false;
        }
    }

    //TODO: make pusable to Reset Game, dosent work right now
    public void ResetGame()
    {
        currentRound = 1;
        score.ClereScore();
        numberOfBalls = 3;
        ChangeState(new RoundState(this));
    }
}
