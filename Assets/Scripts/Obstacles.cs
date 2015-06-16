using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour, IRecycle
{
    public Sprite[] sprites;
    public Vector2 offset = Vector2.zero;

    public void Restart()
    {
        var renderer = this.GetComponent<SpriteRenderer>();
        renderer.sprite = sprites[Random.Range(0, sprites.Length)];

        var collider = this.GetComponent<BoxCollider2D>();
        var size = renderer.bounds.size;
        size.y += offset.y;
        collider.size = size;
        collider.offset = new Vector2(-offset.x, collider.size.y / 2 - offset.y);
    }

    public void ShutDown()
    {

    }
}
