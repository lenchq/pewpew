using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimitGizmo : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D _coll;
    void Start()
    {
        _coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        var coll = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, coll.bounds.size);
    }
}
