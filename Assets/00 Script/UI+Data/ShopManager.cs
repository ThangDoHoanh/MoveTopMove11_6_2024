using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    public enum ShopType
    {
        None,
        Hair,
        Pants,
        LeftHand,
        Skin
    }

    public ShopType currentShop = ShopType.None;

    [Header("-----SetBody------")]
    public GameObject _hair;
    public GameObject _spnie;
    public GameObject _lefpHand;
    public GameObject _RightHand;
    public SkinnedMeshRenderer _skinnedPlayer;
    public SkinnedMeshRenderer _pantsPlayer;
    [SerializeField] GameObject _panelShop;

    public List<Image> _listImageBTNShop = new List<Image>();
    
    public GameObject _shopHair;
    public GameObject _shopPants;
    public GameObject _shopLefphand;
    public GameObject _shopSkinnedPlayer;

    public void ResetActifSetPlay(ItemType _type)
    {
        switch (currentShop)
        {
            case ShopType.Hair:
                if (_type == ItemType.Hair)
                {
                    foreach (Transform child in _hair.transform)
                    {
                        child.gameObject.SetActive(false);
                    }
                }
                break;

            case ShopType.Pants:
                if (_type == ItemType.Pants)
                {
                    // Không cần thực hiện reset cho pants trong trường hợp này vì không phải là skin
                }
                break;

            case ShopType.LeftHand:
                if (_type == ItemType.LeftHand)
                {
                    if (_lefpHand.transform.childCount > 0)
                    {
                        foreach (Transform child in _lefpHand.transform)
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
                break;

            case ShopType.Skin:
                if (_type == ItemType.Skin)
                {
                    // Reset skin cho _skinnedPlayer (body skin)
                    _skinnedPlayer.material = null;

                    // Đặt lại các prefab cho skin của từng phần (hair, spine, left hand)
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
                }
                break;

            default:
                Debug.LogWarning("Unknown or irrelevant item type for the current shop.");
                break;
        }
    }
    public void ResetSkin()
    {
        foreach (Transform child in _hair.transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in _lefpHand.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in _spnie.transform)
        {
            child.gameObject.SetActive(false);
        }
        _skinnedPlayer.material = GameManager._instan._materialAvataPlayer[3];
        _pantsPlayer.material = null;
    }

    public void resetBTN()// mỗi lần  uesr ấn chuyển shop thì reset lại btn
    {
        foreach (Transform child in _panelShop.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Image child in _listImageBTNShop)
        {
            child.color = new Color(0.3660378f, 0.2866145f, 0.2866145f);// đổi màu lại cho giống màu btn shop ban đầu
        }

    }

    public void SetCurrentShop(ShopType shopType)// Đặt trạng thái cho shop
    {
        currentShop = shopType;
    }
    public void setShopHair()//KHi uesr bấm vào btn shop Skin thì chạy đến hàm này và khi chuyển từ home sang canva skin
    {
        SetCurrentShop(ShopType.Hair);
        _listImageBTNShop[0].color=Color.red; //đổi màu đỏ cho shop đang được chọn
        _shopHair.SetActive(true);
    }
    public void setShopPants()
    {
        SetCurrentShop(ShopType.Pants);
        _listImageBTNShop[1].color = Color.red; //đổi màu đỏ cho shop đang được chọn
        _shopPants.SetActive(true);
    }
    public void setShopLefphand()
    {
        SetCurrentShop(ShopType.LeftHand);
        _listImageBTNShop[2].color = Color.red;//đổi màu đỏ cho shop đang được chọn
        _shopLefphand.SetActive(true);
    }
    public void setShopSkin()//KHi uesr bấm vào btn shop Skin thì chạy đến hàm này
    {
        SetCurrentShop(ShopType.Skin);
        _listImageBTNShop[3].color = Color.red;//đổi màu đỏ cho shop đang được chọn
        _shopSkinnedPlayer.SetActive(true);
    }
    public void SetItem(int _id, ItemType itemType)// uesr ấn Button Select thì khi ở shop nào thì lấy các type item shop đó và sinh ra
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
    public void SetItemWeapon(int _id)// uesr ấn Button Select thì khi ở shop nào thì lấy các type item shop đó và sinh ra
    {
        GameObject a = ObjectPooling._instan.GetObjectparent(GameManager._instan._weapon[_id], _RightHand.transform);
        a.SetActive(true);
    }

    public void ResetItemWeapon()
    {
        foreach (Transform child in _RightHand.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    

}
