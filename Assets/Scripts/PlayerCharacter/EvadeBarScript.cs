using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class EvadeBarScript : MonoBehaviour
{
    public PlayerScript playerScript;
    public TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        float percentage = (playerScript.evadeCooldownTime / playerScript.evadeState.evadeMaxCooldownTime) * 100f;
        text.text = $"{percentage:F0} / 100";
    }
}
