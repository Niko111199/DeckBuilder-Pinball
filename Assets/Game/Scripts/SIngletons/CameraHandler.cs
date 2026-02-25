using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraHandler : MonoBehaviour
{
    private static CameraHandler instance;

    [Header("Refrences")]
    [SerializeField] private GameObject pinballCamera;
    [SerializeField] private GameObject shopCamera;
    [SerializeField] private GameObject MenuCamera;

    public GameObject Getpinballcamera()
    {
        return pinballCamera;
    }

    public GameObject GetShopCamera()
    {
        return shopCamera;
    }

    public GameObject GetMenuCamera()
    {
        return MenuCamera;
    }

    public static CameraHandler GetInstance()
    {
        return instance;
    }

    public void ResetCamera()
    {
        Camera cam = Camera.main;

        cam.transform.position = pinballCamera.transform.position;
        cam.transform.rotation = pinballCamera.transform.rotation;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

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
