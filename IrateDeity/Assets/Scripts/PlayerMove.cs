using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMove : MonoBehaviour, INoncollidable
{
    Rigidbody2D rgbd2d;
    [HideInInspector] public Vector3 movementVector;
    [HideInInspector] public float lastHorizontalVector;
    [HideInInspector] public float lastVerticalVector;

    float speed = 3f;

    //Animate animate;

    // Awake is called before the first frame update
    private void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        speed = GetComponent<Character>().GetMovementSpeed();
        //animate = GetComponent<Animate>();
    }

    private void Start()
    {
        lastHorizontalVector = 1f;
        lastVerticalVector = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        if (movementVector.x != 0)
        {
            lastHorizontalVector = movementVector.x;
        }
        if (movementVector.y != 0)
        {
            lastVerticalVector = movementVector.y;
        }

        //animate.horizontal = movementVector.x;
        //animate.vertical = movementVector.y;

        movementVector = movementVector.normalized;
        movementVector *= speed;
        
        rgbd2d.velocity = movementVector;
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

}
