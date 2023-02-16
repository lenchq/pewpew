using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTriggerScript : MonoBehaviour
{
    private TowerController _controller;
    void Start()
    {
        _controller = GetComponentInParent<TowerController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        _controller.TriggerEnter2D(col);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _controller.TriggerExit2D(other);
    }
}
