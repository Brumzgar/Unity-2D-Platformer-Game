using UnityEngine;

public class Stomper : MonoBehaviour
{
    public int damageToDealOnStomp;
    private Rigidbody2D theRigidBody2D;
    public float bounceForce;

    void Start()
    {
        theRigidBody2D = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hurtbox")
        {
            other.gameObject.GetComponent<EnemyHP>().TakeDamage(damageToDealOnStomp);
            theRigidBody2D.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
        }
    }
}
