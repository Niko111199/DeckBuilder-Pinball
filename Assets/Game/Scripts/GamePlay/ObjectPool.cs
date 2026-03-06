using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private Transform SpawnPoint;

    private Queue<GameObject> pool = new Queue<GameObject>();

    //TODO: make this inharited insted, to keep code cleaner
    [Header("Spawn Everthing at start")]
    [SerializeField] private bool SpawnEverythingAtStart = false;
    [SerializeField] private float spawnRadius = 0f; 


    private void Awake()
    {
        InitializePool();

        if (SpawnEverythingAtStart)
        {
            SpawnEverthingInPool();
        }
    }

    private void InitializePool()
    {
        pool.Clear();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetFromPool()
    {
        GameObject obj = pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab, transform);
        obj.SetActive(true);
        obj.transform.position = SpawnPoint.position;
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        pool.Enqueue(obj);
    }

    public void SpawnEverthingInPool()
    {
        int count = pool.Count;

        for (int i = 0; i < count; i++)
        {
            GameObject obj = GetFromPool();

            Vector3 randomPos = SpawnPoint.position + Random.insideUnitSphere * spawnRadius;
            randomPos.y = SpawnPoint.position.y;

            obj.transform.position = randomPos;
        }
    }

    public void ReturnAndRespawn(GameObject obj)
    {

        obj.SetActive(false);
        obj.transform.SetParent(transform);
        pool.Enqueue(obj);

        GameObject reusedObj = pool.Dequeue();
        reusedObj.SetActive(true);

        Vector3 randomPos = SpawnPoint.position + Random.insideUnitSphere * spawnRadius;
        randomPos.y = SpawnPoint.position.y;
        reusedObj.transform.position = randomPos;
    }

    public void ReturnAndRespawnNewItem(GameObject oldObj)
    {
        Destroy(oldObj);

        GameObject newObj = Instantiate(prefab, transform);

        newObj.SetActive(false);
        pool.Enqueue(newObj);

        newObj.SetActive(true);
        Vector3 randomPos = SpawnPoint.position + Random.insideUnitSphere * spawnRadius;
        randomPos.y = SpawnPoint.position.y;
        newObj.transform.position = randomPos;
    }

    public void ReturnEverthingToPool()
    {
        foreach (Transform child in transform)
        {
            ReturnToPool(child.gameObject);
        }
    }

    public void RestockPool()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        InitializePool();

        SpawnEverthingInPool();
    }

    private void OnDrawGizmos()
    {
        if (SpawnPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(SpawnPoint.position, spawnRadius);
        }
    }
}
