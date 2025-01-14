using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    public float jumpStrength;
    public bool isJumping = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            isJumping = true;
            rigidBody.linearVelocityY = jumpStrength;
        }
    }


}
