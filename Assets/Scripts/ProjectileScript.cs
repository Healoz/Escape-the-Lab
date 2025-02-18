using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float maxLifeTime;

    public float damageAmount;
    private float currentLifetime = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentLifetime < maxLifeTime)
        {
            currentLifetime += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        StartCoroutine(DeleteSelfAfterDelay());
    }

    public IEnumerator DeleteSelfAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }


}
