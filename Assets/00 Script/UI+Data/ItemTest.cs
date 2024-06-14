﻿using System;
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
                            ShopManager._instan.SetItemTest(_info._id, ItemType.Hair);
                            break;
                        case ItemType.Spine:
                            ShopManager._instan.SetItemTest(_info._id, ItemType.Spine);
                            break;
                        case ItemType.LeftHand:
                            ShopManager._instan.SetItemTest(_info._id, ItemType.LeftHand);
                            break;
                        case ItemType.Pants:
                            ShopManager._instan.SetItemTest(_info._id, ItemType.Pants);
                            break;
                        case ItemType.Skin:
                            ShopManager._instan.SetItemTest(_info._id, ItemType.Skin);

                            if (_info._hairSkin != null)
                            {
                                GameObject SkinhairInstance = ObjectPooling._instan.GetObjectparent(_info._hairSkin, ShopManager._instan._hair.transform);
                                SkinhairInstance.SetActive(true);
                            }
                            else
                            {
                                Debug.LogWarning("Hair skin prefab is not assigned in ItemDataSO.");
                            }

                            if (_info._SpineSkin != null)
                            {
                                GameObject SkinspineInstance = ObjectPooling._instan.GetObjectparent(_info._SpineSkin, ShopManager._instan._spnie.transform);
                                SkinspineInstance.SetActive(true);
                            }
                            else
                            {
                                Debug.LogWarning("Spine skin prefab is not assigned in ItemDataSO.");
                            }

                            if (_info._LefpHandSkin != null)
                            {
                                GameObject SkinleftHandInstance = ObjectPooling._instan.GetObjectparent(_info._LefpHandSkin, ShopManager._instan._lefpHand.transform);
                                SkinleftHandInstance.SetActive(true);
                            }
                            else
                            {
                                Debug.LogWarning("Left hand skin prefab is not assigned in ItemDataSO.");
                            }


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

    void CheckSkin()
    {

    }    
}
