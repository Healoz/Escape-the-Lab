using UnityEngine;

public class EvadeLogic : MonoBehaviour
{
    public bool isEvading;
    public float evadeCooldownTime;
    public float evadeMaxCooldownTime;
    public int evadeCharges;
    public int maxEvadeCharges;
    public float currentRechargeTime;
    public float rechargeTime;

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

        if (evadeCharges >= maxEvadeCharges) // don't do recharge if max evade charges has been reached
        {
            currentRechargeTime = 0;
            return;
        }

        if (currentRechargeTime < rechargeTime)
        { // charge mechanic, seperate to cooldown time. stops from being able to constantly evade
            currentRechargeTime += Time.deltaTime;
        }
        else
        {
            currentRechargeTime = 0f;

            evadeCharges += 1; // once recharge time passes, add a charge


        }
    }

}
