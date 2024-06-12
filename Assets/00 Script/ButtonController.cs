using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Button _BTLPlay;
    [SerializeField] Button _BTLPlayAvatar1;
    [SerializeField] Button _BTLPlayAvatar2;
    [SerializeField] Button _BTLSkin;
    [SerializeField] Button _BTLBackHome;

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
            });
        }
        if (_BTLSkin != null)
        {
            _BTLSkin.onClick.AddListener(() =>
            {
                UIManager._instan._canvaSkin.gameObject.SetActive(true);
                UIManager._instan._canvaMapHome.gameObject.SetActive(false);
                PlayerController._instan.SetMapSkin(true);
            });
        }
        if (_BTLBackHome != null)
        {
            _BTLBackHome.onClick.AddListener(() =>
            {
                PlayerController._instan.SetMapSkin(false);
                UIManager._instan._canvaSkin.gameObject.SetActive(false);
                UIManager._instan._canvaMapHome.gameObject.SetActive(true);

            });
        }
        if (_BTLPlayAvatar1 != null)
        {
            _BTLPlayAvatar1.onClick.AddListener(() =>
            {
                PlayerController._instan.SetActifSetPlay();
                PlayerController._instan.SetPlay1();

            });
        }
        if (_BTLPlayAvatar2 != null)
        {
            _BTLPlayAvatar2.onClick.AddListener(() =>
            {
                PlayerController._instan.SetActifSetPlay();
                PlayerController._instan.SetPlay2();

            });
        }
    }
}
