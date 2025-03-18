using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public PlayerScript playerScript;
    public float cameraYOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveCameraToPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCameraToPlayer();
    }

    public void MoveCameraToPlayer()
    {
        float cameraYPosition = playerScript.transform.position.y + cameraYOffset;
        // keep z axis the same
        gameObject.transform.position = new Vector3(playerScript.transform.position.x, cameraYPosition, transform.position.z);
    }
}
