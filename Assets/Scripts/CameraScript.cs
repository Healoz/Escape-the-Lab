using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public PlayerScript playerScript;

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
        // keep z axis the same
        gameObject.transform.position = new Vector3(playerScript.transform.position.x, playerScript.transform.position.y, transform.position.z);
    }
}
