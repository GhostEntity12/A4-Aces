using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    int score;
    public Spawner s;
    private void OnCollisionEnter(Collision collision)
    {
        Projectile p = collision.gameObject.GetComponent<Projectile>();
        if (p == null) return;

        p.owner.AddScore(score);
        gameObject.SetActive(false);
        s.inactiveBehaviours.Add(this);
    }
}
