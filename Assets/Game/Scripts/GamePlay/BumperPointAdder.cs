using System.Collections;
using TMPro;
using UnityEngine;

public class BumperPointAdder : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private int pointsToAdd = 2;

    [Header("Visualiser")]
    [SerializeField] private GameObject textContainer;
    [SerializeField] private TMPro.TMP_Text scoreDisplay;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;
    [SerializeField] private float duration;

    private Vector3 originalLocalPos;
    private int startingPoints;

    private void Start()
    {
        startingPoints = pointsToAdd;
        originalLocalPos = textContainer.transform.localPosition;
        textContainer.SetActive(false);
    }

    public void ResetPoints()
    {
        pointsToAdd = startingPoints;
    }

    public int GetPointsToAdd()
    {
        return pointsToAdd;
    }

    public void SetPointsToAdd(int newPoints)
    {
        pointsToAdd = newPoints;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("IceBall"))
        {
            int currentMultiplier = Score.GetInstance().GetHitMultiplier();

            Score.GetInstance().AddScore(pointsToAdd * currentMultiplier);
            Score.GetInstance().IncrementHitMultiplier();

            StartCoroutine(DisplayPoints(currentMultiplier));
        }
    }

    private IEnumerator DisplayPoints(int multiplierUsed)
    {
        textContainer.SetActive(true);

        float elapsed = 0f;
        Vector3 startPos = originalLocalPos + offset;

        if (multiplierUsed <= 1)
        {
            scoreDisplay.text = pointsToAdd.ToString();
        }
        else
        {
            scoreDisplay.text = multiplierUsed.ToString() + "X " + pointsToAdd.ToString();
        }

        textContainer.transform.localPosition = startPos;

        while (elapsed < duration)
        {
            textContainer.transform.localPosition =
                startPos + Vector3.up * speed * (elapsed / duration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        textContainer.transform.localPosition = originalLocalPos;
        textContainer.SetActive(false);
    }
}
