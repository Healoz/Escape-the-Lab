using UnityEngine;

public class DetectionRadiusScript : MonoBehaviour
{
    public float detectionRadius;
    public bool playerCurrentlyDetected;
    public bool playerHasBeenDetected;
    public CircleCollider2D circleCollider2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circleCollider2D.radius = detectionRadius;
        playerCurrentlyDetected = false;
        playerHasBeenDetected = false;
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
            playerCurrentlyDetected = true;
            playerHasBeenDetected = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("trigger exited");
            playerCurrentlyDetected = false;
        }
    }
}
