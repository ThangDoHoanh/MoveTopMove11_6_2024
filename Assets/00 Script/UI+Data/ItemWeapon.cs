using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemWeapon : ItemIvenBase
{
    [SerializeField] Button _BTNbuy;

    [SerializeField] Text _conts;
    
    [SerializeField] Button _buy;
    [SerializeField] Button _isOwnde;
   
    // Start is called before the first frame update
    void Start()
    {
        _buy = UIManager._instan._btnBuyWeapon;
        _isOwnde = UIManager._instan._btnOwndeWeapon;
        _conts = UIManager._instan._txtcontsWeapon;
       
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
    

    // Update is called once per frame
    void checkOwnde()//kiểm tra xem có đang sở hữu k
    {
        if (_info._owned == true)// nếu đang sở hữu thì hiện thị btn Select
        {
            
            _isOwnde.gameObject.SetActive(true);
            _buy.gameObject.SetActive(false);
           
            Debug.Log("123!!");
            if (_isOwnde != null)
            {
                _isOwnde.onClick.AddListener(() => // khi đang sở hữu thì add dữ liệu
                {
                    ShopManager._instan.ResetItemWeapon();
                    ShopManager._instan.SetItemWeapon(_info._id);
                });
            }


        }
        else //nếu không sở hữu thì hiện thị btn Buy
        {
            _buy.gameObject.SetActive(true);
            
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
