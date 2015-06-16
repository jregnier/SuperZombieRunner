using UnityEngine;
using System.Collections;

public class TiledBackground : MonoBehaviour
{
    public int textureSize = 32;
    public bool scaleHorizontially = true;
    public bool scaleVertically = true;

    // Use this for initialization
    void Start()
    {
        float newWidth = 1.0f;
        if (scaleHorizontially)
        {
            newWidth = Mathf.Ceil(Screen.width / (textureSize * PixelPerfectCamera.scale));
        }

        float newHeight = 1.0f;
        if (scaleVertically)
        {
            newHeight = Mathf.Ceil(Screen.height / (textureSize * PixelPerfectCamera.scale));
        }

        transform.localScale = new Vector3(newWidth * textureSize, newHeight * textureSize, 1);

        this.GetComponent<Renderer>().material.mainTextureScale = new Vector3(newWidth, newHeight, 1);
    }
}
