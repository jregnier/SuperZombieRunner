using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    public RecycleGameObject prefab;

    private List<RecycleGameObject> poolInstances = new List<RecycleGameObject>();

    private RecycleGameObject CreateInstance(Vector3 position)
    {
        var clone = GameObject.Instantiate(prefab);
        clone.transform.position = position;
        clone.transform.parent = transform;

        poolInstances.Add(clone);

        return clone;
    }

    public RecycleGameObject NextObject(Vector3 position)
    {
        RecycleGameObject instance = null;

        instance = poolInstances.Where(x => !x.gameObject.activeSelf).FirstOrDefault();

        if (instance == null)
        {
            instance = this.CreateInstance(position);
        }
        else
        {
            instance.transform.position = position;
        }

        instance.Restart();

        return instance;
    }
}
