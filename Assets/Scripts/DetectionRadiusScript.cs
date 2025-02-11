using UnityEngine;

public class DetectionRadiusScript : MonoBehaviour
{
    public float detectionRadius;
    public CircleCollider2D circleCollider2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circleCollider2D.radius = detectionRadius;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
