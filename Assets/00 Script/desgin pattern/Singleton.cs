using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Start is called before the first frame update
    private static T instant = null;
    public static T _instan => instant;
    private void Awake()
    {
        if (instant == null)
        {
            instant = GetComponent<T>();
            return;
        }
        if (instant.gameObject.GetInstanceID()
            != this.gameObject.GetInstanceID())
        {
            Destroy(this.gameObject);
        }
    }
}
