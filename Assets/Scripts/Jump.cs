using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
    public float jumpSpeed = 240f;
    public float forwardSpeed = 50f;

    private Rigidbody2D body2;
    private InputState inputState;

    public void Awake()
    {
        body2 = this.GetComponent<Rigidbody2D>();
        inputState = this.GetComponent<InputState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputState.standing)
        {
            if (inputState.actionButton)
            {
                body2.velocity = new Vector2(transform.position.x < 0 ? forwardSpeed : 0, jumpSpeed);
            }
        }
    }
}   
