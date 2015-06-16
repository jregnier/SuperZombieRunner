using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public interface IRecycle
{
    void Restart();
    void ShutDown();
}

public class RecycleGameObject : MonoBehaviour
{
    private List<IRecycle> recycleComponents;

    public void Awake()
    {
        var components = this.GetComponents<MonoBehaviour>();
        recycleComponents = components
                                .Where(x => x is IRecycle)
                                .Select(x => (IRecycle)x)
                                .ToList();
    }

    public void Restart()
    {
        gameObject.SetActive(true);

        foreach(var component in recycleComponents)
        {
            component.Restart();
        }
    }

    public void ShutDown()
    {
        gameObject.SetActive(false);

        foreach (var component in recycleComponents)
        {
            component.ShutDown();
        }
    }
}
