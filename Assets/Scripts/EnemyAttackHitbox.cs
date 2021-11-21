using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    [SerializeField] float damage = 50f;
    [SerializeField] Collider2D attackCollider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<HealthSystem>().TakeDamage(damage);
            attackCollider.enabled = false;
        }
    }
}
