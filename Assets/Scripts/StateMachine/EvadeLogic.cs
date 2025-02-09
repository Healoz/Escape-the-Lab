using UnityEngine;

public class EvadeLogic : MonoBehaviour
{
    public bool isEvading;
    public float evadeCooldownTime;
    public float evadeMaxCooldownTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // initialise coolDown on start
        evadeCooldownTime = evadeMaxCooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        IncrementEvadeCooldown();
    }

    public void IncrementEvadeCooldown()
    {
        if (evadeCooldownTime < evadeMaxCooldownTime)
        {
            evadeCooldownTime += Time.deltaTime; // increment cooldown if cooldowntime is less than the max
        }
    }

}
