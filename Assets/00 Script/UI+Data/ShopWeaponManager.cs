using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopWeaponManager : Singleton<ShopWeaponManager>
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> _itemWeapon = new List<GameObject>();
    [SerializeField] Transform _panleWeapon;

    public List<GameObject> _instantiatedWeapons = new List<GameObject>();
    void Start()
    {
        _itemWeapon = Resources.LoadAll<GameObject>("ItemWeapon").ToList();
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Init() // sau khi add vào list thì t thực hiện sinh ra ở các shop
    {
        foreach (GameObject itemPrefab in _itemWeapon)
        {
            // Lấy một instance từ hệ thống pooling
            GameObject instance = ObjectPooling._instan.GetObjectparent(itemPrefab, _panleWeapon.transform);

            // Thêm instance vào danh sách các vũ khí đã được sinh ra
            _instantiatedWeapons.Add(instance);

           
        }

        // Kích hoạt vũ khí đầu tiên nếu có trong danh sách
        if (_instantiatedWeapons.Count > 0)
        {
            _instantiatedWeapons[0].SetActive(true);
        }
    }
    
}
