using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShopSkinManager : Singleton<ShopSkinManager>
{
    [SerializeField] Transform _girlayoutSkin;
    [SerializeField] Transform _girlayoutHair;
    [SerializeField] Transform _girlayoutLefpHande;
    [SerializeField] Transform _girlayoutPants;

    [SerializeField] List<GameObject> itemSkin = new List<GameObject>();
    public List<GameObject> _itemSkin => itemSkin;

    [SerializeField] List<GameObject> itemHair = new List<GameObject>();
    public List<GameObject> _itemHair => itemHair;

    [SerializeField] List<GameObject> _itemLefpHande = new List<GameObject>();
    [SerializeField] List<GameObject> _itemPants = new List<GameObject>();

    void Start()// bắt đầu chạy thì add các prefab button vào list
    {
        _itemPants = Resources.LoadAll<GameObject>("ItemPants").ToList();
        _itemLefpHande = Resources.LoadAll<GameObject>("ItemLefpHande").ToList();
        itemSkin =Resources.LoadAll<GameObject>("ItemSkin").ToList();
        itemHair = Resources.LoadAll<GameObject>("ItemHair").ToList();
        Init();
    }
    void Init() // sau khi add vào list thì t thực hiện sinh ra ở các shop
    {
        foreach (GameObject item in itemSkin)
        {
            GameObject a = ObjectPooling._instan.GetObjectparent(item.gameObject, _girlayoutSkin.transform);
            a.SetActive(true);
        }
        foreach (GameObject itemPrefab in itemHair)
        {
            GameObject instance = ObjectPooling._instan.GetObjectparent(itemPrefab, _girlayoutHair.transform);
            instance.SetActive(true);
        }

        foreach (GameObject itemPrefab in _itemLefpHande)
        {
            GameObject instance = ObjectPooling._instan.GetObjectparent(itemPrefab, _girlayoutLefpHande.transform);
            instance.SetActive(true);
        }
        foreach (GameObject itemPrefab in _itemPants)
        {
            GameObject instance = ObjectPooling._instan.GetObjectparent(itemPrefab, _girlayoutPants.transform);
            instance.SetActive(true);
        }
    }    
  
}
