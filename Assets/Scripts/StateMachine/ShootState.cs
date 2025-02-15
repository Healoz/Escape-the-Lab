using UnityEngine;

public class ShootState : State
{

    public GameObject projectileReference;
    public GameObject target;
    public float shootTime;
    public float shootForce;
    public float shotDamage;

    public override void Enter()
    {
        ProjectileScript projectileScript = projectileReference.gameObject.GetComponent<ProjectileScript>();
        projectileScript.damageAmount = shotDamage;
        spriteRenderer.color = Color.red;
        ShootProjectile(); // on enter, shoot projectile
    }
    public override void Do()
    {
        //  end condition here
        if (time > shootTime)
        {
            isComplete = true;
        }

    }

    public void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectileReference, transform.position, Quaternion.identity);
        Rigidbody2D projectileRigidBody = projectile.GetComponent<Rigidbody2D>();

        if (rigidBody == null)
        {
            return;
        }

        projectileRigidBody.AddForce(GetShootDirection() * shootForce, ForceMode2D.Impulse);
    }

    public Vector2 GetShootDirection()
    {
        return (target.transform.position - transform.position).normalized;
    }
    public override void Exit() { }
}
