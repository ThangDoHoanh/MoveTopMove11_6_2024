using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Item : ItemIvenBase
{
    [SerializeField] Button _BTNbuy;
    
    [SerializeField] Text _conts;
    [SerializeField] Text _select;
    [SerializeField] Button _buy;
    [SerializeField] Button _isOwnde;
    [SerializeField] Button _Adver;


    private void Start()
    {
        _Adver = UIManager._instan._btnAdvertisement;
         _buy = UIManager._instan._btnBuy;
        _isOwnde = UIManager._instan._btnOwnde;
        _conts = UIManager._instan._txtconts;
        _select = UIManager._instan._txtselect;
        _buy.gameObject.SetActive(false);
        _isOwnde.gameObject.SetActive(false);
        if (_BTNbuy != null)
        {
            _BTNbuy.onClick.AddListener(() =>// nó sẽ tự add chính nó để  bắt điều kiện hiện thị button buy or seclect
            {
                UIManager._instan._imagepick.transform.SetParent(_BTNbuy.transform, false);
                UIManager._instan._imagepick.gameObject.SetActive(true);
                checkOwnde();
            });
        }
    }
    
    void checkOwnde ()//kiểm tra xem có đang sở hữu k
    {
        if (_info._owned == true)// nếu đang sở hữu thì hiện thị btn Select
        {
            _Adver.gameObject.SetActive(false);
            _isOwnde.gameObject.SetActive(true);
            _buy.gameObject.SetActive(false);
            _select.text = "Select";
            Debug.Log("123!!");
            if (_isOwnde != null)
            {
                _isOwnde.onClick.AddListener(() => // khi đang sở hữu thì add dữ liệu
                {
                    var currentShop = ShopManager._instan.currentShop;
                   
                    // Use the current shop type to determine the action

                    if (currentShop == ShopManager.ShopType.None)
                {
                    Debug.LogWarning("No shop is currently selected.");
                    return;
                }

                    // Check the item type and the current shop to perform the appropriate action
                    ShopManager._instan.ResetActifSetPlay(_info._itemType);

                    switch (currentShop)// ở shop nào chỉ nhận _info.id và type ở shop đó
                    {
                        case ShopManager.ShopType.Hair:
                            if (_info._itemType == ItemType.Hair)
                            {

                                ShopManager._instan.SetItem(_info._id, ItemType.Hair);
                                PlayerController._instan.SetDataPlayer(_info._id, _info._itemType);
                                PlayerController._instan.GetTingDataPlayer();
                            }

                            break;
                        case ShopManager.ShopType.Pants:
                            if (_info._itemType == ItemType.Pants)
                            {


                                ShopManager._instan.SetItem(_info._id, ItemType.Pants);
                                PlayerController._instan.SetDataPlayer(_info._id, _info._itemType);
                                PlayerController._instan.GetTingDataPlayer();
                            }

                            break;
                        case ShopManager.ShopType.LeftHand:
                            if (_info._itemType == ItemType.LeftHand)
                            {

                                ShopManager._instan.SetItem(_info._id, ItemType.LeftHand);
                                PlayerController._instan.SetDataPlayer(_info._id, _info._itemType);
                                PlayerController._instan.GetTingDataPlayer();
                            }

                            break;
                        case ShopManager.ShopType.Skin:
                            if (_info._itemType == ItemType.Skin)
                            {

                                ShopManager._instan.SetItem(_info._id, ItemType.Skin);

                                if (_info._hairSkin != null)
                                {
                                    GameObject SkinhairInstance = ObjectPooling._instan.GetObjectparent(_info._hairSkin, ShopManager._instan._hair.transform);
                                    SkinhairInstance.SetActive(true);
                                }

                                if (_info._SpineSkin != null)
                                {
                                    GameObject SkinspineInstance = ObjectPooling._instan.GetObjectparent(_info._SpineSkin, ShopManager._instan._spnie.transform);
                                    SkinspineInstance.SetActive(true);
                                }

                                if (_info._LefpHandSkin != null)
                                {
                                    GameObject SkinleftHandInstance = ObjectPooling._instan.GetObjectparent(_info._LefpHandSkin, ShopManager._instan._lefpHand.transform);
                                    SkinleftHandInstance.SetActive(true);
                                }
                                if (_info._pantSkin != null)
                                {
                                    ShopManager._instan._pantsPlayer.material = _info._pantSkin;

                                }
                                PlayerController._instan.SetDataPlayer(_info._id, _info._itemType);
                                
                            }
                     
                            break;
                    default:
                        Debug.LogWarning("Unknown shop type.");
                        break;
                    }
                   
                });
            }


        }
        else //nếu không sở hữu thì hiện thị btn Buy
        {
            _buy.gameObject.SetActive(true);
            _Adver.gameObject.SetActive(true);
            _isOwnde.gameObject.SetActive(false);
            _conts.text = _info._conts.ToString();
            _buy.onClick.RemoveAllListeners();

            if (_buy != null)
            {
                _buy.onClick.AddListener(() =>
                {
                    OnPurchase();
                });
            }
            //if (_Adver != null)
            //{
            //    _Adver.onClick.AddListener(() =>
            //    {
            //        // chạy quảng cáo
            //    });
            //}
        }
    }   

   
    void OnPurchase()//kiểm tra xem đủ conts đề mua k
    {
        int playerMoney = PlayerPrefs.GetInt("ContsPlayer");
        if (playerMoney >= _info._conts)
        {
            // Người chơi đủ tiền để mua
            playerMoney -= _info._conts; // Trừ tiền
            _info._owned = true; // Đánh dấu là đã sở hữu
            checkOwnde(); // Cập nhật giao diện
            PlayerPrefs.SetInt("ContsPlayer", playerMoney);
            Debug.Log("Item purchased successfully!");
        }
        else
        {
            // Người chơi không đủ tiền để mua
            Debug.Log("bạn nghèo !");
        }
    }
    
    

}
