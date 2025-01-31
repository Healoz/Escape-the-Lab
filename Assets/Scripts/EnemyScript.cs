using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float currentHealth;
    public Rigidbody2D rigidBody;
    public float maxHealth;

    public float damageAmount;
    public PlayerScript playerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemyDead();
    }

    public void CheckEnemyDead()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            rigidBody.freezeRotation = false;

            currentHealth -= playerScript.forcePushState.attackAmount;
        }
    }

    // on collision with player projectile
    // take some damage
    // pushed in opposite direction of projectile (does using physics anyway?)
}
