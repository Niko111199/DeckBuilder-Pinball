using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]private List<GameObject> Prefabs = new List<GameObject>();

    public GameObject GetPrefab(int index)
    {
        return Prefabs[index];
    }
}
