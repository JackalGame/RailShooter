using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    Transform parent;
    [SerializeField] int hp = 3;
    
    private void Awake()
    {
        AddNonTriggerBoxCollider();
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        hp -= 1;
        if(hp <= 0)
        {
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
        Destroy(fx, 2f);
    }
}
