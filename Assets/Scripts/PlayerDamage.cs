using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class PlayerDamage : MonoBehaviour
{
    Player p;

    [SerializeField]
    float damageGracePeriod = 2.5f;

    float damageTimer;

    bool recentlyTookDamage;
    // Start is called before the first frame update
    void Awake()
    {
        p = GetComponentInParent<Player>();
    }

    private void Update()
    {
        if (recentlyTookDamage)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer > damageGracePeriod)
            {
                recentlyTookDamage = false;
                damageTimer = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.transform.name);
        if (!recentlyTookDamage)
        {
            float damageAmount;

            if (collision.transform.CompareTag("Border"))
            {
                damageAmount = 100;
            }
            else
            {
                damageAmount = 10;
            }

            //if (collision.transform.CompareTag()
            //{

            //}

            p.TakeDamage(damageAmount);
            recentlyTookDamage = true;
        }
    }
}
