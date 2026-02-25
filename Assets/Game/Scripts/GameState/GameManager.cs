using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;

    //TODO: make pusable to Reset Game
    [Header("Refrences")]
    [SerializeField] private GameState currentState;
    [SerializeField] private ObjectPool ballPool;
    [SerializeField] private GameObject ShopUi;
    [SerializeField] private GameObject PlayerPlacedItemParant;
    [SerializeField] private GameObject looseUI;

    private int currentRound = 1;
    private int FinalRound = 8;
    private int requredScore;
    private int numberOfBalls = 3;
    private int currentNumberOfBalls;

    private bool IsShopOpen = false;

    public static GameManager GetInstance()
    {
        return Instance;
    }

    public GameState GetCurrentState()
    {
        return currentState;
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }

    public int GetCurrentNumberOfBalls()
    {
        return currentNumberOfBalls;
    }

    public int GetNumberOfBalls()
    {
        return numberOfBalls;
    }

    public int GetRequredScore()
    {
        return requredScore;
    }

    public void SetRequredScore(int value)
    {
        requredScore = value;
    }

    public ObjectPool GetBallPool()
    {
        return ballPool;
    }

    public int GetFinalRound()
    {
        return FinalRound;
    }

    public bool GetIsShopOpen()
    {
        return IsShopOpen;
    }

    public void SetIsShopOpen(bool value)
    {
        IsShopOpen = value;
    }

    public GameObject getShopUi()
    {
        return ShopUi;
    }

    public void SetCurrentNumberOfBalls(int value)
    {
        currentNumberOfBalls = value;
    }

    public void SetNumberOfBalls(int value)
    {
        numberOfBalls = value;
    }

    public void IncrementCurrentRound()
    {
        currentRound++;
    }

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
        ChangeState(new MenuState());
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
            ShopUi.SetActive(false);
        }
    }

    //TODO: make pusable to Reset Game, dosent work right now
    public void ResetGame()
    {
        currentRound = 1;

        Score.GetInstance().ClereScore();
        Gold.GetInstance().ClearGold();
        CameraHandler.GetInstance().MoveCameraSmooth(CameraHandler.GetInstance().Getpinballcamera().transform, 1f);

        foreach (Transform child in PlayerPlacedItemParant.transform)
        {
            Destroy(child.gameObject);
        }

        FlipperScript[] flippers =
           Object.FindObjectsByType<FlipperScript>(FindObjectsSortMode.None);

        foreach (FlipperScript flipper in flippers)
        {
            flipper.SetFlipperStrenght(10000f);
        }

        BumperPointAdder[] bumpers = GameObject.FindObjectsByType<BumperPointAdder>(FindObjectsSortMode.None);

        foreach (BumperPointAdder bumper in bumpers)
        {
            bumper.ResetPoints();
        }

        looseUI.SetActive(false);

        numberOfBalls = 3;
        ChangeState(new RoundState());

        Debug.Log("Game Reset");
    }
}
