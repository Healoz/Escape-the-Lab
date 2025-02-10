using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float maxLifeTime;
    public float currentLifetime = 0;
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
}
