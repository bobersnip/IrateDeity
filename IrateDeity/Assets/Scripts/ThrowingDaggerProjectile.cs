using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingDaggerProjectile : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] public int damage = 5;

    [SerializeField] public float duration = 5f;
    private float timeAlive;

    private bool hitDetected = false;

    private DaggerWeapon baseDagger;

    private void Awake()
    {
        baseDagger = GetComponentInParent<DaggerWeapon>();
        this.transform.parent = null;
        timeAlive = 0f;
    }

    public void SetDirection(Vector3 direction, float currentDegrees, float playerHorizontalVectorComponent)
    {
        this.direction = direction;
        Debug.Log(currentDegrees);
        if (playerHorizontalVectorComponent > 0)
        {
            currentDegrees += 180;
        }

        transform.localRotation *= Quaternion.AngleAxis(currentDegrees, Vector3.forward);
    }

    private void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
        timeAlive += Time.deltaTime;

        if (Time.frameCount % 6 == 0)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.4f);
            foreach (Collider2D c in hit)
            {
                IDamageable damageableEntity = c.GetComponent<IDamageable>();
                if (damageableEntity != null)
                {
                    baseDagger.PostMessage(damage, c.transform.position);
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