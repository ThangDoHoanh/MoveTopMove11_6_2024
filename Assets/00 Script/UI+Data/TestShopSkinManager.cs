using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TestShopSkinManager : Singleton<TestShopSkinManager>
{
    [SerializeField] Transform _girlayout;
    [SerializeField] List<GameObject> itemSkin = new List<GameObject>();
    public List<GameObject> _itemSkin => itemSkin;

    void Start()
    {
        itemSkin=Resources.LoadAll<GameObject>("ItemSkin").ToList();
        Init();
    }
    void Init()
    {
        foreach (GameObject item in itemSkin)
        {
            GameObject a = ObjectPooling._instan.GetObjectparent(item.gameObject, _girlayout.transform);
        }
    }    
  
}
