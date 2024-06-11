using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoollingX : Singleton<ObjPoollingX>
{

    // Start is called before the first frame update

    Dictionary<GameObject, List<GameObject>> _listObject = new Dictionary<GameObject, List<GameObject>>();
    public GameObject GetObject(GameObject defaultPrefab)
    {
        if (!_listObject.ContainsKey(defaultPrefab))
        {
            _listObject.Add(defaultPrefab, new List<GameObject>());
        }

        foreach (GameObject obj in _listObject[defaultPrefab])
        {
            if (!obj.activeSelf)
            {
                
                return obj;
            }
        }

        GameObject newObj = Instantiate(defaultPrefab);
        _listObject[defaultPrefab].Add(newObj);
        newObj.SetActive(false);
        return newObj;
    }
}
