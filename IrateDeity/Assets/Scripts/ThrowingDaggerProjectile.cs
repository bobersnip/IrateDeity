using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingDaggerProjectile : projectileBase
{
    private void Awake()
    {
        DaggerWeapon baseDagger = GetComponentInParent<DaggerWeapon>();
        this.transform.parent = null;
        
        damage = 5;
        duration = 3f;
        speed = 4f;
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
}