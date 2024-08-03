using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamageable, INoncollidable
{
    [SerializeField] int hp;
    [SerializeField] float speed;
    [SerializeField] projectileBase projectile;
    public GameObject targetGameObject;

    public void Start()
    {
        IgnoreCollision();
    }

    public void IgnoreCollision()
    {
        INoncollidable[] objToIgnore = FindObjectsOfType<MonoBehaviour>().OfType<INoncollidable>().ToArray();
        for (int i = 0; i < objToIgnore.Length; i++)
        {
            GameObject objToIgnoreGO = ((MonoBehaviour)objToIgnore[i]).gameObject;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), objToIgnoreGO.GetComponent<Collider2D>());
        }
        
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 1)
        {
            targetGameObject.GetComponent<Level>().AddExperience(experience_reward);
            Destroy(gameObject);
            //GetComponent<DropOnDestroy>().CheckDrop();
        }
    }
    public abstract void Attack();
}
