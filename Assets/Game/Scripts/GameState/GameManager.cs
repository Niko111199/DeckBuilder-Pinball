using System.Collections;
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
    public bool IsShopOpen = false;
    public GameObject pinballcamera;
    public GameObject ShopCamera;

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

    //TODO: Abstract out the camera system, this is just a quick way to move the camera to the shop and back, but it should be more flexible and reusable
    public void MoveCameraSmooth(Transform target, float duration)
    {
        StartCoroutine(MoveCameraCoroutine(target, duration));
    }

    private IEnumerator MoveCameraCoroutine(Transform target, float duration)
    {
        Camera cam = Camera.main;

        Vector3 startPos = cam.transform.position;
        Quaternion startRot = cam.transform.rotation;

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            cam.transform.position = Vector3.Lerp(startPos, target.position, t);
            cam.transform.rotation = Quaternion.Slerp(startRot, target.rotation, t);

            yield return null;
        }

        cam.transform.position = target.position;
        cam.transform.rotation = target.rotation;
    }
}
