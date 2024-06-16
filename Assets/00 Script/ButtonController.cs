using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Button _BTLPlay;
    [SerializeField] Button _BTLSkin;
    [SerializeField] Button _BTLBackHome;
    [Header("---------Shop-----")]
    [SerializeField] Button _BTNShopWeapon;
    [SerializeField] Button _BTNShopWeaponBack;
    [SerializeField] Button _BTLShopHair;
    [SerializeField] Button _BTLShopPants;
    [SerializeField] Button _BTLShopLefpHande;
    [SerializeField] Button _BTLShopSkin;
    [SerializeField] Button _BTLSetting;
    [SerializeField] Button _BTNHome;
    [SerializeField] Button _BTNContinue;

    // Start is called before the first frame update
    private void Awake()
    {
        if (_BTLPlay != null)
        {
            _BTLPlay.onClick.AddListener(() =>
            {
                UIManager._instan._canvaMapPlaying.gameObject.SetActive(true);//bật canva playing
                UIManager._instan._canvaMapHome.gameObject.SetActive(false);//tắt canva home
                GameManager._instan._cameraPlayer.enabled = true;//set cam playing
                GameManager._instan._startAddEnemy();//bắt đầu hàm add enemy
                PlayerController._instan._isPause=false;
                PlayerController._instan._spriteRenderer.gameObject.SetActive(true);
            });
        }
        if (_BTLSkin != null)
        {
            _BTLSkin.onClick.AddListener(() =>
            {
               
                ShopManager._instan.resetBTN();
                ShopManager._instan._listImageBTNShop[0].color= Color.red;// đổi màu đỏ cho button
                UIManager._instan._canvaSkin.gameObject.SetActive(true);
                UIManager._instan._canvaMapHome.gameObject.SetActive(false);
                PlayerController._instan.SetMapSkin(true);
                ShopManager._instan.setShopHair();
            });
        }
        if (_BTLBackHome != null)
        {
            _BTLBackHome.onClick.AddListener(() =>
            {
                PlayerController._instan.SetMapSkin(false);
                UIManager._instan._canvaSkin.gameObject.SetActive(false);
                UIManager._instan._canvaMapHome.gameObject.SetActive(true);
                UIManager._instan._textCoinPlayerPrefs.text = PlayerPrefs.GetInt("ContsPlayer").ToString();
            });
        }
        
        if (_BTLShopHair != null)
        {
            _BTLShopHair.onClick.AddListener(() =>
            {
                ShopManager._instan.resetBTN();
                ShopManager._instan.setShopHair();
            });
        }
        if (_BTLShopPants != null)
        {
            _BTLShopPants.onClick.AddListener(() =>
            {
                ShopManager._instan.resetBTN();
                ShopManager._instan.setShopPants();
            });
        }
        if (_BTLShopLefpHande != null)
        {
            _BTLShopLefpHande.onClick.AddListener(() =>
            {
                ShopManager._instan.resetBTN();
                ShopManager._instan.setShopLefphand();

            });
        }
        if (_BTLShopSkin != null)
        {
            _BTLShopSkin.onClick.AddListener(() =>
            {
                ShopManager._instan.resetBTN();
                ShopManager._instan.setShopSkin();

            });
        }
        if (_BTLSetting != null)
        {
            _BTLSetting.onClick.AddListener(() =>
            {   PlayerController._instan._isPause = true;
                UIManager._instan._panelSetting.SetActive(true);
                UIManager._instan._panelPlaying.SetActive(false);

            });
        }
        if (_BTNHome != null)
        {
            _BTNHome.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("SampleScene");

            });
        }
        if (_BTNContinue != null)
        {
            _BTNContinue.onClick.AddListener(() =>
            {
                PlayerController._instan._isPause = false;
                UIManager._instan._panelSetting.SetActive(false);
                UIManager._instan._panelPlaying.SetActive(true);

            });
        }
        if (_BTNShopWeapon != null)
        {
            _BTNShopWeapon.onClick.AddListener(() =>
            {
                UIManager._instan._canvaShopWeapon.gameObject.SetActive(true);
                UIManager._instan._canvaMapHome.gameObject.SetActive(false);
                

            });
        }
        if (_BTNShopWeaponBack != null)
        {
            _BTNShopWeaponBack.onClick.AddListener(() =>
            {
                UIManager._instan._canvaShopWeapon.gameObject.SetActive(false);
                UIManager._instan._canvaMapHome.gameObject.SetActive(true);


            });
        }
    }
}
