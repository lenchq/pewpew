using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PathGizmoScript : MonoBehaviour
{
    public Transform point;
    public float speed = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        HandleMove();
        if (Vector2.Distance(this.transform.position, point.position) < 0.05f)
        {
            Die();
        }
    }

    private void HandleMove()
    {
        var pos = this.transform.position;
        this.transform.position = Vector2.MoveTowards(pos, point.transform.position, speed * Time.deltaTime);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
