using System.Collections;
using UnityEngine;

public class BumperCreateIceBall : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject IceBall;
    [SerializeField] private Transform SpawnLocation;
    private bool canSpawn = true;

    [Header("Settings")]
    [SerializeField] private float spawnCooldown = 1f;
    [SerializeField] private Vector3 offSetSpawnLocation;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && canSpawn || collision.gameObject.CompareTag("IceBall") && canSpawn)
        {
            Instantiate(IceBall, SpawnLocation.position + offSetSpawnLocation, SpawnLocation.rotation);
            StartCoroutine(SpawnCooldown());
        }
    }

    private IEnumerator SpawnCooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }
}
