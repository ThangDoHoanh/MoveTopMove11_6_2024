using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class ItemTest : ItemIvenBaseTeset
{
    [SerializeField] Button _BTNbuy;
    
    [SerializeField] Text _conts;
    [SerializeField] Text _select;
    [SerializeField] Button _buy;
    [SerializeField] Button _isOwnde;


    private void Awake()
    {
        _buy = UIManager._instan._btnBuy;
        _isOwnde = UIManager._instan._btnOwnde;
        _conts = UIManager._instan._txtconts;
        _select = UIManager._instan._txtselect;
        _buy.gameObject.SetActive(false);
        _isOwnde.gameObject.SetActive(false);
        if (_BTNbuy != null)
        {
            _BTNbuy.onClick.AddListener(() =>
            {
                UIManager._instan._imagepick.transform.SetParent(_BTNbuy.transform, false);
                UIManager._instan._imagepick.gameObject.SetActive(true);
                checkOwnde();
            });
        }
    }
    
    void checkOwnde ()//kiểm tra xem có đang sở hữu k
    {
        if (_info._owned == true)
        {
            _isOwnde.gameObject.SetActive(true);
            _buy.gameObject.SetActive(false);
            _select.text = "Select";
            Debug.Log("123!!");
            if (_isOwnde != null)
            {
                _isOwnde.onClick.AddListener(() =>
                {
                    ShopManager._instan.ResetActifSetPlay(_info._itemType);
                    switch (_info._itemType)
                    {
                        case ItemType.Hair:
                            ShopManager._instan.SetItemTest(GameManager._instan._hairAvataPlayer[_info._id], ItemType.Hair);
                            break;
                        case ItemType.Spine:
                            ShopManager._instan.SetItemTest(GameManager._instan._spnieAvataPlayer[_info._id], ItemType.Spine);
                            break;
                        case ItemType.LeftHand:
                            ShopManager._instan.SetItemTest(GameManager._instan._lefpHandAvataPlayer[_info._id], ItemType.LeftHand);
                            break;
                        case ItemType.Skin:
                            ShopManager._instan.SetItemTest(GameManager._instan._skinPlayer[_info._id], ItemType.Skin);
                            break;
                        default:
                            Debug.LogWarning("Unknown item type.");
                            break;
                    }
                });
            }

        }
        else
        {
            _buy.gameObject.SetActive(true);
            _isOwnde.gameObject.SetActive(false);
            _conts.text = _info._conts.ToString();
            if (_buy != null)
            {
                _buy.onClick.AddListener(() =>
                {
                    OnPurchase();
                });
            }
        }
    }   
    void OnPurchase()//kiểm tra xem đủ conts đề mua k
    {
        int playerMoney = GameManager._instan._contsPlayer;
        if (playerMoney >= _info._conts)
        {
            // Người chơi đủ tiền để mua
            GameManager._instan._contsPlayer -= _info._conts; // Trừ tiền
            _info._owned = true; // Đánh dấu là đã sở hữu
            checkOwnde(); // Cập nhật giao diện
            Debug.Log("Item purchased successfully!");
        }
        else
        {
            // Người chơi không đủ tiền để mua
            Debug.Log("bạn nghèo !");
        }
    }    
}
