using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingTest : Singleton<ObjectPoolingTest>
{ // Dictionary để lưu trữ các đối tượng trong pool
    private Dictionary<GameObject, List<GameObject>> _listObject = new Dictionary<GameObject, List<GameObject>>();

    // Dictionary để lưu trữ các GameObject cha cho mỗi loại đối tượng
    private Dictionary<GameObject, GameObject> _parentObjects = new Dictionary<GameObject, GameObject>();

    public GameObject GetObject(GameObject defaultPrefab)
    {
        if (!_listObject.ContainsKey(defaultPrefab))
        {
            // Nếu khóa không tồn tại, tạo một List mới và thêm vào Dictionary
            _listObject.Add(defaultPrefab, new List<GameObject>());
        }

        foreach (GameObject obj in _listObject[defaultPrefab])
        {
            if (!obj.activeSelf)
            {
                // Nếu tìm thấy một đối tượng không hoạt động, trả về nó
                return obj;
            }
        }

        // Nếu không tìm thấy đối tượng không hoạt động, tạo một đối tượng mới
        GameObject newObj = Instantiate(defaultPrefab);
        _listObject[defaultPrefab].Add(newObj);

        // Kiểm tra và tạo GameObject cha nếu chưa tồn tại
        if (!_parentObjects.ContainsKey(defaultPrefab))
        {
            GameObject parent = new GameObject(defaultPrefab.name + "Pool");
            parent.transform.SetParent(this.transform);
            _parentObjects[defaultPrefab] = parent;
        }

        // Đặt đối tượng mới làm con của GameObject cha
        newObj.transform.SetParent(_parentObjects[defaultPrefab].transform);
        newObj.SetActive(false);
        return newObj;
    }
}
