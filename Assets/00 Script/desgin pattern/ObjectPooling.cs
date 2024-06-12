using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    Dictionary<GameObject, List<GameObject>> _listObject = new Dictionary<GameObject, List<GameObject>>();
    
    public GameObject GetObject(GameObject defaultPrefab)
    {

        if (!_listObject.ContainsKey(defaultPrefab))
        {
            // Nếu khóa không tồn tại, tạo một List mới và thêm vào Dictionary
            _listObject.Add(defaultPrefab, new List<GameObject>());
        }

        foreach (GameObject a in _listObject[defaultPrefab])
        {
            if (!a.activeSelf)
            {
                // Nếu tìm thấy một đối tượng không hoạt động, trả về nó
                return a;
            }
        }

        // Nếu không tìm thấy đối tượng không hoạt động, tạo một đối tượng mới
        GameObject b = Instantiate(defaultPrefab, this.transform.position, Quaternion.identity, this.transform.parent = this.transform);
        _listObject[defaultPrefab].Add(b);
        b.SetActive(false);
        return b;
    }
    public GameObject GetObjectparent(GameObject defaultPrefab, Transform parent)
    {
        // Kiểm tra xem có danh sách nào cho prefab này chưa, nếu chưa thì tạo mới
        if (!_listObject.ContainsKey(defaultPrefab))
        {
            _listObject.Add(defaultPrefab, new List<GameObject>());
        }

        // Tìm đối tượng không hoạt động trong danh sách
        foreach (GameObject a in _listObject[defaultPrefab])
        {
            if (!a.activeSelf)
            {
                // Đặt lại parent của đối tượng nếu nó đã được tạo trước đó
                a.transform.SetParent(parent);
                // Giữ lại vị trí và rotation hiện tại của đối tượng
                Vector3 previousPosition = a.transform.position;
                Quaternion previousRotation = a.transform.rotation;

                // Gán lại vị trí và rotation sau khi thay đổi parent
                a.transform.position = previousPosition;
                a.transform.rotation = previousRotation;

                return a;
            }
        }

        // Nếu không tìm thấy đối tượng không hoạt động, tạo đối tượng mới
        GameObject b = Instantiate(defaultPrefab, parent);
        _listObject[defaultPrefab].Add(b);

        // Thiết lập vị trí cục bộ nếu cần
        // b.transform.localPosition = Vector3.zero; // Nếu muốn đặt vị trí tương đối của đối tượng mới tại (0,0,0) so với parent

        b.SetActive(false);
        return b;
    }

    // Phương thức trả lại đối tượng vào pool
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }


}
