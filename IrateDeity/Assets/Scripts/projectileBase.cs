using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class projectileBase : MonoBehaviour
{
    [SerializeField] public GameObject spritePrefab;
    [SerializeField] public float velocity;
    //Movement modifier will be some routine to modify the way the projectile travels through the game
    //[SerializeField] string movementModifier


    //private Vector3 direction;
    [SerializeField] public Vector3 direction;
    [SerializeField] public float speed;
    [SerializeField] public int damage;

    [SerializeField] public float duration;

    public float timeAlive;
    public bool hitDetected = false;

    //public void SetDirection(Vector3 direction, float currentDegrees, float playerHorizontalVectorComponent)
    //{
    //    this.direction = direction;
    //    Debug.Log(currentDegrees);
    //    if (playerHorizontalVectorComponent > 0)
    //    {
    //        currentDegrees += 180;
    //    }

    //    transform.localRotation *= Quaternion.AngleAxis(currentDegrees, Vector3.forward);
    //}

    private void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
        timeAlive += Time.deltaTime;

        //collision detection every 6 frames
        if (Time.frameCount % 6 == 0)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.4f);
            foreach (Collider2D c in hit)
            {
                IDamageable damageableEntity = c.GetComponent<IDamageable>();
                if (damageableEntity != null)
                {
                    damageableEntity.TakeDamage(damage);
                    hitDetected = true;
                    break;
                }
            }

            if (hitDetected == true || timeAlive > duration)
            {
                Destroy(gameObject);
            }
        }
    }
}
