using UnityEngine;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    //TODO: Remove when there is no UI overlay
    [Header("References")]
    [SerializeField] private GameObject looseUI;
    [SerializeField] private Button[] buttons;

    private bool firstTime = true;

    private void Update()
    {
        if (GameManager.GetInstance().GetCurrentState() is not MenuState menuState)
        {
            foreach (Button button in buttons)
            {
                button.interactable = false;
            }
        }
        else
        {
            foreach (Button button in buttons)
            {
                button.interactable = true;
            }
        }
    }

    public void StartGame()
    {
        if (firstTime)
        {
            GameManager.GetInstance().ChangeState(new RoundState());
            firstTime = false;
        }
        else if (!firstTime)
        {
            GameManager.GetInstance().ResetGame();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting Game");
    }
 
    public void GoToMenu()
    {
        looseUI.SetActive(false);
        GameManager.GetInstance().ChangeState(new MenuState());
    }
}
