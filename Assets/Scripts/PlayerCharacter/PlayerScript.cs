using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    public SpriteRenderer spriteRenderer;

    public float jumpStrength;
    public float runStrength;
    public bool isGrounded = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rigidBody.linearVelocityY = jumpStrength;
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveHorizontal(true);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            rigidBody.linearVelocityX = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            MoveHorizontal(false);
        }
    }

    public void MoveHorizontal(bool isGoingRight)
    {
        Debug.Log("function triggered");
        float runForceValue = runStrength;
        if (!isGoingRight)
        {
            // make negative if going left
            runForceValue = runForceValue * -1;
        }

        rigidBody.linearVelocityX = runForceValue;
    }

}
