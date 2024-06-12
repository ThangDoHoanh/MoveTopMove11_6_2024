using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Button _BTLPlay;
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
    }
}
