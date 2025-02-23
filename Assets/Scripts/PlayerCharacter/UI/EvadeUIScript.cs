using System;
using System.Text;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class EvadeUIScript : MonoBehaviour
{
    public EvadeLogic evadeLogic;
    public TMP_Text cooldownText;
    public TMP_Text chargesText;

    // Update is called once per frame
    void Update()
    {
        float percentage = (evadeLogic.evadeCooldownTime / evadeLogic.evadeMaxCooldownTime) * 100f;
        cooldownText.text = $"{percentage:F0} / 100";
        RenderCharges();
    }

    public void RenderCharges()
    {
        StringBuilder chargeTextBuilder = new StringBuilder();

        for (int i = 0; i < evadeLogic.evadeCharges; i++)
        {
            chargeTextBuilder.Append("0 ");
        }

        chargesText.text = chargeTextBuilder.ToString();
    }
}
