using UnityEngine;
using System.Collections;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;
    private InputState inputState;

    // Use this for initialization
    void Awake()
    {
        animator = this.GetComponent<Animator>();
        inputState = this.GetComponent<InputState>();
    }

    // Update is called once per frame
    void Update()
    {
        bool running = true;

        if (inputState.absVelocityX > 0 && inputState.absVelocityY < inputState.standingThreshold)
        {
            running = false;
        }

        animator.SetBool("Running", running);
    }
}
