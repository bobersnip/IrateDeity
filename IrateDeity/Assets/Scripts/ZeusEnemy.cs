using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusEnemy : EnemyBase
{
    [SerializeField] public Transform targetDestination;
    Character targetCharacter;
    [SerializeField] float speed;

    //We will not be colliding with enemies.
    Rigidbody2D rgdbd2d;

    [SerializeField] int hp = 10;
    [SerializeField] int damage = 1;
    [SerializeField] int experience_reward = 400;

    private void Awake()
    {
        rgdbd2d = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rgdbd2d.velocity = direction * speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        if (targetCharacter == null)
        {
            targetCharacter = targetGameObject.GetComponent<Character>();
        }

        targetCharacter.TakeDamage(damage);
    }
}
