using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField] GameObject _hair;
    [SerializeField] GameObject _spnie;
    [SerializeField] GameObject _lefpHand;
    [SerializeField] SkinnedMeshRenderer _skinnedPlayer;
    [SerializeField] SkinnedMeshRenderer _pantsPlayer;
    [SerializeField] GameObject _panelShop;

    public List<Image> _listImageBTNShop = new List<Image>();
    
    public GameObject _shopHair;
    public GameObject _shopPants;
    public GameObject _shopLefphand;
    public GameObject _shopSkinnedPlayer;
    // Start is called before the first frame update
    public void ResetActifSetPlay(ItemType _type)
    {
        //foreach (Transform child in _hair.transform)
        //{
        //    child.gameObject.SetActive(false);
        //}

        //// Tắt tất cả các đối tượng con của _spnie
        //foreach (Transform child in _spnie.transform)
        //{
        //    child.gameObject.SetActive(false);
        //}

        //// Kiểm tra nếu _lefpHand.transform có con trước khi truy cập
        //if (_lefpHand.transform.childCount > 0)
        //{
        //    // Tắt tất cả các đối tượng con của _lefpHand
        //    foreach (Transform child in _lefpHand.transform)
        //    {
        //        child.gameObject.SetActive(false);
        //    }
        //}
        switch (_type)
        {
            case ItemType.Hair:
                foreach (Transform child in _hair.transform)
                {
                    child.gameObject.SetActive(false);
                }
                break;

            case ItemType.Spine:
                foreach (Transform child in _spnie.transform)
                {
                    child.gameObject.SetActive(false);
                }
                break;
            case ItemType.Pants:
                foreach (Transform child in _spnie.transform)
                {
                    child.gameObject.SetActive(false);
                }
                break;

            case ItemType.LeftHand:
                if (_lefpHand.transform.childCount > 0)
                {
                    foreach (Transform child in _lefpHand.transform)
                    {
                        child.gameObject.SetActive(false);
                    }
                }
                break;

            // Thêm các case cho các loại item khác cần reset
            case ItemType.Skin:
                // Không cần thực hiện reset nếu itemType là Skin
                break;

            default:
                Debug.LogWarning("Unknown item type.");
                break;
        }
    }
    

    public void resetBTN()
    {
        foreach (Transform child in _panelShop.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Image child in _listImageBTNShop)
        {
            child.color = new Color(0.3660378f, 0.2866145f, 0.2866145f);
        }

    }
    public void setShopHair()
    {
        _listImageBTNShop[0].color=Color.red;
        _shopHair.SetActive(true);
    }
    public void setShopPants()
    {
        _listImageBTNShop[1].color = Color.red;
        _shopPants.SetActive(true);
    }
    public void setShopLefphand()
    {
        _listImageBTNShop[2].color = Color.red;
        _shopLefphand.SetActive(true);
    }
    public void setShopSkin()
    {
        _listImageBTNShop[3].color = Color.red;
        _shopSkinnedPlayer.SetActive(true);
    }
    public void SetItemTest(GameObject itemPrefab, ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Hair:
                GameObject hairInstance = ObjectPooling._instan.GetObjectparent(itemPrefab, _hair.transform);
                hairInstance.SetActive(true);
                break;

            case ItemType.Spine:
                GameObject spineInstance = ObjectPooling._instan.GetObjectparent(itemPrefab, _spnie.transform);
                spineInstance.SetActive(true);
                break;

            case ItemType.LeftHand:
                GameObject leftHandInstance = ObjectPooling._instan.GetObjectparent(itemPrefab, _lefpHand.transform);
                leftHandInstance.SetActive(true);
                break;
           
            case ItemType.Skin:
                // Gán material cho người chơi
                if (itemPrefab.TryGetComponent(out Renderer renderer))
                {
                    _skinnedPlayer.material = renderer.material;
                }
                break;

            default:
                Debug.LogWarning("Unknown item type.");
                break;
        }
    }    

}
