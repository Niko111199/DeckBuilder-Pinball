using UnityEngine;

public class BumperMoneyAdder : MonoBehaviour
{
    [Header("Money")]
    [SerializeField] private int MoneyToAdd = 2;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("IceBall"))
        {
            Gold.GetInstance().AddGold(MoneyToAdd);
        }
    }
}
