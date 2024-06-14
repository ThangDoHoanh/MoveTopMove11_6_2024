using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    [Header("-----SetBody------")]
    public GameObject _hair;
    public GameObject _spnie;
    public GameObject _lefpHand;
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
                // Không cần thực hiện reset cho pants trong trường hợp này vì không phải là skin
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

            case ItemType.Skin:
                // Reset skin cho _skinnedPlayer (body skin)
                _skinnedPlayer.material = null;

                // Đặt lại các prefab cho skin của từng phần (hair, spine, left hand)
                // Tắt các prefab cũ trước khi đặt lại
                foreach (Transform child in _hair.transform)
                {
                    child.gameObject.SetActive(false);
                }
                foreach (Transform child in _spnie.transform)
                {
                    child.gameObject.SetActive(false);
                }
                if (_lefpHand.transform.childCount > 0)
                {
                    foreach (Transform child in _lefpHand.transform)
                    {
                        child.gameObject.SetActive(false);
                    }
                }
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
    public void SetItemTest(int _id, ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Hair:
                GameObject hairInstance = ObjectPooling._instan.GetObjectparent(GameManager._instan._hairAvataPlayer[_id], _hair.transform);
                hairInstance.SetActive(true);
                break;

            case ItemType.Spine:
                GameObject spineInstance = ObjectPooling._instan.GetObjectparent(GameManager._instan._spnieAvataPlayer[_id], _spnie.transform);
                spineInstance.SetActive(true);
                break;

            case ItemType.LeftHand:
                GameObject leftHandInstance = ObjectPooling._instan.GetObjectparent(GameManager._instan._lefpHandAvataPlayer[_id], _lefpHand.transform);
                leftHandInstance.SetActive(true);
                break;
            case ItemType.Pants:
                _pantsPlayer.material = GameManager._instan._PantsPlayer[_id];
                break;
            case ItemType.Skin:
                
                _skinnedPlayer.material = GameManager._instan._materialAvataPlayer[_id];

                break;

            default:
                Debug.LogWarning("Unknown item type.");
                break;
        }
    }    

}
