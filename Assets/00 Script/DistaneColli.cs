using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistaneColli : IComparer
{
    private Transform _colliTransform;
    public DistaneColli(Transform _collitf) 
    {
        _colliTransform = _collitf;
    }
    public int Compare(object x, object y)
    {
        Collider xCollider = x as Collider;
        Collider yCollider = y as Collider;
        Vector3 offset = xCollider.transform.position - _colliTransform.transform.position;
        float xDistance = offset.sqrMagnitude;

        offset = yCollider.transform.position-xCollider.transform.position;
        float yDistance = offset.sqrMagnitude;
        return xDistance.CompareTo(yDistance);
    }
}
