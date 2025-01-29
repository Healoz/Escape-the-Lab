using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float currentHealth;
    public float maxHealth;
    public PlayerScript playerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("collision with projectile");
            currentHealth -= playerScript.forcePushState.attackAmount;
            Debug.Log("enemy health " + currentHealth);

        }

    }

    // on collision with player projectile
    // take some damage
    // pushed in opposite direction of projectile (does using physics anyway?)
}
