using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class PathPointScript : MonoBehaviour
{
    private CreepPathController _parent;
    
    [SerializeField]
    private GameObject pathGizmo;

    [SerializeField]
    private float reloadTime = .5f;

    [SerializeField]
    private float gizmoSpeed;
    [CanBeNull]
    private Transform _nextPoint;

    private bool _canSpawn = true;

    void Start()
    {
        // pathGizmo = Resources.Load<GameObject>("Prefabs/PathGizmo");
        _parent = GetComponentInParent<CreepPathController>();
        _nextPoint = _parent.GetNextPoint(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (_nextPoint is not null && _canSpawn)
        {
            var gizm = Instantiate(pathGizmo, transform);
            var gizmScript = gizm.GetComponent<PathGizmoScript>();
            gizmScript.point = _nextPoint;
            gizmScript.speed = gizmoSpeed;
            StartCoroutine(ReloadSpawn());
        }
    }

    private IEnumerator ReloadSpawn()
    {
        _canSpawn = false;
        yield return new WaitForSeconds(reloadTime);
        _canSpawn = true;
    }
}
