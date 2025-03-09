using UnityEngine;

public class EdgeChecker : MonoBehaviour
{
    public bool isLeftLedge;
    public bool isRightLedge;

    public float maxRaycastDistance;
    public float maxGroundDistance;
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
        // make negative if for the left side
        float newDistanceFromLedgeCheck = isLeft ? distanceFromLedgeCheck * -1 : distanceFromLedgeCheck;

        Vector3 lineStartPos = new Vector3(transform.position.x + newDistanceFromLedgeCheck, transform.position.y, transform.position.z);
        Vector3 lineEndPos = new Vector3(lineStartPos.x, transform.position.y - maxRaycastDistance, transform.position.z);
        // drawing line between 2 points
        Debug.DrawLine(lineStartPos, lineEndPos, Color.blue);

        CheckForLedge(lineStartPos, isLeft);
    }

    (RaycastHit2D[], int) PerformRaycast(Vector3 startPosition)
    {
        // direction pointing straight down
        Vector2 direction = Vector2.down;

        // set up contact filter to include triggers as well.
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;

        // setting up layer mask
        int layerMask = Physics2D.GetLayerCollisionMask(gameObject.layer);
        // don't include enemy itself in the layer mask
        layerMask &= ~(1 << gameObject.layer); // remove balls layer from mask

        filter.SetLayerMask(layerMask); // use the modified layer mask

        // creates array to store results as raycast may hit multiple objects
        RaycastHit2D[] results = new RaycastHit2D[1];

        // perform raycast with filter
        int hitCount = Physics2D.Raycast(startPosition, direction, filter, results, maxRaycastDistance);

        return (results, hitCount); // return both variables we need
    }

    void CheckForLedge(Vector3 startPosition, bool isLeft)
    {

        var (results, hitCount) = PerformRaycast(startPosition);

        if (hitCount <= 0) // no collisions were detected
        {
            return;
        }

        string gameObjectTag = results[0].collider.gameObject.tag;

        bool isDeadlyLedge = gameObjectTag == "OutOfBounds";

        if (isLeft)
        {
            isLeftLedge = isDeadlyLedge;
        }
        else
        {
            isRightLedge = isDeadlyLedge;
        }


        if (isLeft)
        {
            Debug.Log("Left raycast collided with: " + results[0].collider.gameObject.name);

        }
        else
        {
            Debug.Log("Right raycast collided with: " + results[0].collider.gameObject.name);
        }
    }
}