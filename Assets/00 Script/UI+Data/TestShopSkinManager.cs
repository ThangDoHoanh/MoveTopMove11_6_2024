using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TestShopSkinManager : Singleton<TestShopSkinManager>
{
    [SerializeField] Transform _girlayoutSkin;
    [SerializeField] Transform _girlayouiHair;

    [SerializeField] List<GameObject> itemSkin = new List<GameObject>();
    public List<GameObject> _itemSkin => itemSkin;

    [SerializeField] List<GameObject> itemHair = new List<GameObject>();
    public List<GameObject> _itemHair => itemHair;

    void Start()
    {
        itemSkin=Resources.LoadAll<GameObject>("ItemSkin").ToList();
        itemHair = Resources.LoadAll<GameObject>("ItemHair").ToList();
        Init();
    }
    void Init()
    {
        foreach (GameObject item in itemSkin)
        {
            GameObject a = ObjectPooling._instan.GetObjectparent(item.gameObject, _girlayoutSkin.transform);
            a.SetActive(true);
        }
        foreach (GameObject itemPrefab in itemHair)
        {
            GameObject instance = ObjectPooling._instan.GetObjectparent(itemPrefab, _girlayouiHair.transform);
            instance.SetActive(true);
        }


    }    
  
}
