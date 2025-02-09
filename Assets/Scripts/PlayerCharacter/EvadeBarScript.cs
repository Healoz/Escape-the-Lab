using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class EvadeBarScript : MonoBehaviour
{
    public EvadeLogic evadeLogic;
    public TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        float percentage = (evadeLogic.evadeCooldownTime / evadeLogic.evadeMaxCooldownTime) * 100f;
        text.text = $"{percentage:F0} / 100";
    }
}
