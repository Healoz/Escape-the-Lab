using UnityEngine;

public class DetectionRadiusScript : MonoBehaviour
{
    public float detectionRadius;
    public bool playerDetected;
    public CircleCollider2D circleCollider2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circleCollider2D.radius = detectionRadius;
        playerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("trigger entered");
            playerDetected = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("trigger exited");
            playerDetected = false;
        }
    }
}
