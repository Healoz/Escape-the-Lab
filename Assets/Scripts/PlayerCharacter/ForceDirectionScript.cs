using UnityEngine;
using UnityEngine.UI;

public class ForceDirectionScript : MonoBehaviour
{
    public Vector3 mousePosition;
    public Image mouseReticle;
    public Image forceDirectionArrow;
    public GameObject player;

    public float arrowDistanceFromPlayer;

    public Vector2 playerPosition;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetForceDirection();
    }

    void GetForceDirection()
    {
        // getting mouse position
        mousePosition = Input.mousePosition;
        mouseReticle.rectTransform.position = mousePosition; // moving mouseReticle

        DrawLineFromPlayerToMouse();

    }

    void DrawLineFromPlayerToMouse()
    {

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(
            mousePosition.x,
            mousePosition.y,
            -Camera.main.transform.position.z
        ));

        Vector3 screenPlayerPosition = Camera.main.WorldToScreenPoint(player.transform.position);

        // get direction in screen space
        Vector2 direction = (mousePosition - screenPlayerPosition).normalized;

        // calculate arrow position from player
        Vector2 arrowPosition = (Vector2)screenPlayerPosition + (direction * arrowDistanceFromPlayer);
        forceDirectionArrow.rectTransform.position = arrowPosition;

        playerPosition = player.transform.position;

        // drawing line between 2 points
        Debug.DrawLine(playerPosition, worldMousePosition, Color.red);
    }
}
