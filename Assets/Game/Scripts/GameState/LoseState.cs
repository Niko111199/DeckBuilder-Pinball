using UnityEngine;

public class LoseState : GameState
{
    public LoseState(GameManager manager) : base(manager) { }



    public override void Enter()
    {
        GameObject loseScreen = FindInactiveObjectByName("Loose Panel");
        GameManager.Instance.ResetGame();
        loseScreen.SetActive(true);
        Debug.Log("GAME OVER");
    }

    public static GameObject FindInactiveObjectByName(string name)
    {
        var allObjects = GameObject.FindObjectsByType<Transform>(FindObjectsInactive.Include,FindObjectsSortMode.None);

        foreach (var t in allObjects)
        {
            if (t.name == name)
                return t.gameObject;
        }

        return null;
    }
}

