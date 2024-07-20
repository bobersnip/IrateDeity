using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rgbd2d;
    [SerializeField] float moveSpd = 10f;
    public Vector3 moveVec;

    private void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        moveVec = new Vector3();
    }

    private void Update()
    {
        moveVec.x = Input.GetAxisRaw("Horizontal");
        moveVec.y = Input.GetAxisRaw("Vertical");
        moveVec *= moveSpd;
        rgbd2d.velocity = moveVec;
    }
}
