using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    public SpriteRenderer spriteRenderer;

    public float jumpStrength;
    public bool isGrounded = false;

    public State airState;
    public State groundState;

    State state;

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
    }

    void SelectState()
    {

    }

    void UpdateState()
    {

    }


}
