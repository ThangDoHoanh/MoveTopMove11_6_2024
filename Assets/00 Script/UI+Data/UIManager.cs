using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] Text _scoreText;
    [SerializeField] Text _liveText;
    int _score = 0;
    int _livetime;

    public Canvas _canvaMapPlaying;
    public Canvas _canvaMapHome;
    public Canvas _canvaSkin;
    

    Vector3 _canvaEnemyPosition = new Vector3(0,5,0);
    [Header("---------BTN_BUY---------")]
    public Button _btnBuy;
    public Button _btnOwnde;
    public Text _txtconts;
    public Text _txtselect;
    public GameObject _imagepick;
    public Button _btnAdvertisement;

    [Header("-----Canva player Dead-----")]
    public GameObject _panelBackHomeDead;
    [SerializeField]Text _textBackHomeDead;
    [SerializeField] Button _btnTouch;
    public GameObject _panelPlaying;
    public GameObject _panelSetting;
    [SerializeField] Text _txtAddCoin;
    [Header("-----Canva player ReviveNow-----")]
    public GameObject _panel_ReviveNow;
    [SerializeField] Text _text_ReviveNow;
    [SerializeField] Button _BTNx;
    [SerializeField] Button _BTNBuyPlaying;
    private Coroutine _reviveCoroutine;

    [Header("-----Canva player Home-----")]
    public Text _textCoinPlayerPrefs;
    [Header("-----Canva playerShopeWeapon-----")]
    public Canvas _canvaShopWeapon;
    




    private void Start()
    {
        _textCoinPlayerPrefs.text = PlayerPrefs.GetInt("ContsPlayer").ToString();
        _imagepick.gameObject.SetActive(false);
        _canvaSkin.gameObject.SetActive(false);
        _canvaShopWeapon.gameObject.SetActive(false);
        _canvaMapPlaying.gameObject.SetActive(false);
        _canvaMapHome.gameObject.SetActive(true);
        _livetime = GameManager._instan._live;
        updateText();
       
    }
    private void Update()
    {
        updateText();
        setVitriScore();
    }

    public void setScore(int score)
    {
        if (score < 0 || score >= 6)
            return;
        _score += score;
    }
    public void setLive(int score)
    {
        if (_livetime < 1)
            return;
        _livetime -= score;
    }
    void updateText()
    {
        _scoreText.text = "Score : " + "\n" + _score.ToString();
        _liveText.text = "Live : " + _livetime.ToString();
    }

    void setVitriScore()
    {
        Vector3 enemyScreenPosition = Camera.main.WorldToScreenPoint(PlayerController._instan.transform.position + _canvaEnemyPosition);
        //_scoreText.transform.position = Camera.main.WorldToScreenPoint(PlayerController._instan.gameObject.transform.position + new Vector3(0, 5f, 0));

        _scoreText.transform.position = enemyScreenPosition;
    }
    public void HoaTo()
    {
        _canvaEnemyPosition += new Vector3(0, 1,0);
        Debug.Log("ZZZZ!");
    }
    public void PanelPlayerDead()// khi player dead
    {
        _txtAddCoin.text = GameManager._instan._addingMoney.ToString();
        PlayerPrefs.SetInt("ContsPlayer" , GameManager._instan._addingMoney);
        _panel_ReviveNow.gameObject.SetActive(false);
        _panelBackHomeDead.gameObject.SetActive(true);
        _textBackHomeDead.text = _livetime.ToString();
        if (_btnTouch != null)
        {
            _btnTouch.onClick.AddListener(() =>
            {
                
                SceneManager.LoadScene("SampleScene");
            });
        }
    }   
    
    public void panelReviveNow()
    {
        PlayerController._instan._isPause = true;
        _panelPlaying.gameObject.SetActive(false);
        _panel_ReviveNow.gameObject.SetActive(true);
        _reviveCoroutine = StartCoroutine(CountdownAndRevive());
        
        if (_BTNx != null)
        {
            _BTNx.onClick.AddListener(() =>
            {  
                PanelPlayerDead();
            });
        }
        if (_BTNBuyPlaying != null)
        {
            _BTNBuyPlaying.onClick.AddListener(() =>
            {
                StopCoroutine(_reviveCoroutine);
                PlayerController._instan.continuePlay();
                _panelPlaying.gameObject.SetActive(true);
                _panel_ReviveNow.gameObject.SetActive(false);
            });
        }
    }
    private IEnumerator CountdownAndRevive()
    {
        int countdown = 5;
        while (countdown > 0)
        {
            _text_ReviveNow.text = countdown.ToString();
            yield return new WaitForSeconds(1f); // Đợi 1 giây
            countdown--;
        }

        _text_ReviveNow.text = "0";

        PanelPlayerDead();
    }

    

       
}
