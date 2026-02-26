using UnityEngine;

public class MakeIceBallMelt : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private GameObject IceBall;

    [Header("Settings")]
    [SerializeField] private float meltTime = 5f;

    private void Update()
    { 
        if (IceBall != null)
        {
            IceBall.transform.localScale = Vector3.Lerp(IceBall.transform.localScale, Vector3.zero, meltTime * Time.deltaTime);

            if (IceBall.transform.localScale.magnitude < 0.1f)
            {
                Destroy(IceBall);
            }
        }
    }
}
