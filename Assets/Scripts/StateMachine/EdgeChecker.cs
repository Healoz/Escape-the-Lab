using UnityEngine;

public class EdgeChecker : MonoBehaviour
{
    public bool deathLedgeDeteched;
    public float distanceFromLedgeCheck;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DrawEdgeChecker(true);
        DrawEdgeChecker(false);
    }

    void DrawEdgeChecker(bool isLeft)
    {
        float newDistanceFromLedgeCheck = distanceFromLedgeCheck;
        if (isLeft) // make negative if for the left side
        {
            newDistanceFromLedgeCheck = distanceFromLedgeCheck * -1;
        }

        Vector3 lineStartPos = new Vector3(transform.position.x + newDistanceFromLedgeCheck, transform.position.y, transform.position.z);
        Vector3 lineEndPos = new Vector3(lineStartPos.x, transform.position.y - 300f, transform.position.z);
        // drawing line between 2 points
        Debug.DrawLine(lineStartPos, lineEndPos, Color.blue);
    }
}