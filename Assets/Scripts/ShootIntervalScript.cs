using UnityEngine;
using UnityEngine.AI;

public class ShootIntervalScript : MonoBehaviour
{
    public float currentShotTime;
    public float shotIntervalInSeconds;
    public float checkBuffer = 0.1f;
    public bool isShooting = false;

    public float gracePeriodInterval => shotIntervalInSeconds - checkBuffer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentShotTime = 0f;
    }

    void Update()
    {
        if (isShooting)
        {
            if (currentShotTime >= shotIntervalInSeconds)
            {
                currentShotTime = 0f; // reset the timer if it reaches the shot interval
            }
            else
            {
                // if not, increment the timer
                currentShotTime += Time.deltaTime;
            }
        }
    }
}
