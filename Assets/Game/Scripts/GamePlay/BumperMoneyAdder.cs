using UnityEngine;

public class BumperMoneyAdder : MonoBehaviour
{
    public int MoneyToAdd = 2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Gold.Instance.AddGold(MoneyToAdd);
        }
    }
}
