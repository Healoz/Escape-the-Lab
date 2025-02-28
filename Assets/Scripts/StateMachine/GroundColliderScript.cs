using UnityEngine;

public class GroundColliderScript : MonoBehaviour
{
    [SerializeField]
    private bool _isGrounded;

    public bool isGrounded
    {
        get { return _isGrounded; }
        private set { _isGrounded = value; }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("is Collided with ground");
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("left ground");
            isGrounded = false;
        }
    }
}
