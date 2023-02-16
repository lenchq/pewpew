using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class CreepPathController : MonoBehaviour
{
    [SerializeField]
    public List<Transform> Points { get; private set; }
    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    [CanBeNull]
    public Transform GetNextPoint(Transform child)
    {
        for (var i = 0; i < Points.Count; i++)
        {
            var point = Points[i];
            if (point == child)
            {
                if (i + 1 < Points.Count)
                    return Points[i + 1];
            }
        }
        return null;
    }
    private void Awake()
    {
        //Points = new List<Transform>();
        var children = GetComponentsInChildren<Transform>();
        Points = children.ToList();

        // foreach (var point in children)
        // {
        //     Points.Add(point.transform);
        // }
    }

    private void OnDrawGizmos()
    {
        var children = GetComponentsInChildren<Transform>();
        children = children[1..];
        Transform prevel = null;
        foreach (var el in children)
        {
            if (prevel is null)
            {
                prevel = el;
                continue;
            }
            Gizmos.DrawLine(prevel.transform.position, el.transform.position);
            prevel = el;
        }
    }

    public List<Transform> GetPoints()
    { //todo delete
        var children = GetComponentsInChildren<Transform>();

        //return transforms of children
        return children[1..].ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
