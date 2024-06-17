using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeaponManager : Singleton<ShopWeaponManager>
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> _itemWeapon = new List<GameObject>();
    [SerializeField] Transform _panleWeapon;

    public List<GameObject> _instantiatedWeapons = new List<GameObject>();

    [SerializeField] Button _lefp;
    [SerializeField] Button _Right;

    [SerializeField]private int _currentActiveIndex = 0;
    void Start()
    {
        _currentActiveIndex = 0;
        if (_lefp != null)
        {
            _lefp.onClick.AddListener(() =>
            {
                OnLeftButtonClicked();

            });
        }
        if (_Right != null)
        {
            _Right.onClick.AddListener(() =>
            {
                OnRightButtonClicked();
            });
        }

        _itemWeapon = Resources.LoadAll<GameObject>("ItemWeapon").ToList();
        Init();
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

            _instantiatedWeapons[_currentActiveIndex].SetActive(true);
        }
    }
    void OnLeftButtonClicked()
    {
        if(_currentActiveIndex > 0)
        {
            _instantiatedWeapons[_currentActiveIndex].SetActive(false);
            _currentActiveIndex -= 1;
        }    
        else if(_currentActiveIndex == 0)
        {
            _instantiatedWeapons[_currentActiveIndex].SetActive(false);
        }    
        if(_currentActiveIndex<0)
        {
            return;
        }
        _instantiatedWeapons[_currentActiveIndex].SetActive(true);

    }
    void OnRightButtonClicked()
    {
        if(_currentActiveIndex+1< _instantiatedWeapons.Count)
        {
            _instantiatedWeapons[_currentActiveIndex].SetActive(false);
            _currentActiveIndex += 1;
        }
        else if(_currentActiveIndex+1 == _instantiatedWeapons.Count)
        {
            _instantiatedWeapons[_currentActiveIndex].SetActive(false);

        }
        
        if (_currentActiveIndex+1 > _instantiatedWeapons.Count)
        {
            return;
        }
        _instantiatedWeapons[_currentActiveIndex].SetActive(true);

    }


}
