using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectUtility
{
    public static Dictionary<RecycleGameObject, ObjectPool> pools = new Dictionary<RecycleGameObject, ObjectPool>();

    public static GameObject Instantiate(GameObject prefab, Vector3 position)
    {
        GameObject instance = null;

        var recycledScript = prefab.GetComponent<RecycleGameObject>();
        if (recycledScript != null)
        {
            var pool = GetObjectPool(recycledScript);
            instance = pool.NextObject(position).gameObject;
        }
        else
        {
            instance = GameObject.Instantiate(prefab);
            instance.transform.position = position;
        }

        return instance;
    }

    public static void Destroy(GameObject gameObject)
    {
        var recycledGameObject = gameObject.GetComponent<RecycleGameObject>();

        if (recycledGameObject != null)
        {
            recycledGameObject.ShutDown();
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    private static ObjectPool GetObjectPool(RecycleGameObject reference)
    {
        ObjectPool pool = null;

        if (pools.ContainsKey(reference))
        {
            pool = pools[reference];
        }
        else
        {
            var poolContainer = new GameObject(reference.gameObject.name + "ObjectPool");
            pool = poolContainer.AddComponent<ObjectPool>();
            pool.prefab = reference;
            pools.Add(reference, pool);
        }

        return pool;
    }
}
