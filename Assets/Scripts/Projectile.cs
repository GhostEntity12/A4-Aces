using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public Player owner;
    [Min(2), Tooltip("Speed of the projectile. Should be larger than the playr's movement speed")]
    public float speed = 2;
    public float damage;
    public float lifetime = 40f;
    float life = 0f;
    public Rigidbody rb;
    bool stuck;

    private void Awake()
    {
        // Setting variables
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // If the projectile is not yet stuck, move the projectile forwards
        if (!stuck)
        {
            rb.velocity = transform.forward * speed;
        }

        // Increments death counter and destroys object if it's existed for more than lifetime seconds
        life += Time.deltaTime;
        if (life >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the projectile is already stuck, ignore
        if (stuck) return;

        // Check whether the other object hit is a player
        Player hitPlayer = collision.transform.GetComponentInParent<Player>();

        // Not a player
        if (hitPlayer == null)
        {
            // Freeze the projectile and exit
            stuck = true;
            rb.isKinematic = true;
            return;
        }
        // Is a player
        else
        {
            // subtract health from the other player
            hitPlayer.currentHealth -= damage;
            // Destroys the object next frame (see Update)
            life = lifetime;
        }
    }
}
