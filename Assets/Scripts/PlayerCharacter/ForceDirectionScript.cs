using UnityEngine;
using UnityEngine.UI;

public class ForceDirectionScript : MonoBehaviour
{
    public Vector3 mousePosition;
    public Image mouseReticle;
    public GameObject forceDirectionArrow;


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
        mouseReticle.rectTransform.position = mousePosition;


        // mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // // z value should be 0, not -10
        // mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
        // mouseReticle.transform.position = mousePosition;

        forceDirectionArrow.transform.position = transform.position;
    }
}
