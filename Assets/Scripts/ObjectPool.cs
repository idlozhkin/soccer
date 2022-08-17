using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected Transform startPoint;
    [SerializeField] protected int capacity;
    [Inject] private DiContainer diContainer;

    private List<GameObject> pool = new List<GameObject>();

    protected void Initialize(GameObject prefab)
    {
        GameObject spawned = diContainer.InstantiatePrefab(prefab, startPoint);
        spawned.SetActive(false);

        pool.Add(spawned);
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = pool.Where(x => x.activeSelf == false)
            .OrderBy(x => Random.value)
            .FirstOrDefault(x => x.activeSelf == false);
        return result != null;
    }
}