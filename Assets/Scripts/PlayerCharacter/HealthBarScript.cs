using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public PlayerScript playerScript;
    public TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        float percentage = (playerScript.currentHealth / playerScript.maxHealth) * 100f;
        text.text = $"{percentage:F0} / 100";
    }
}
