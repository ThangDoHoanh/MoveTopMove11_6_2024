using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{
    [SerializeField] Text _scoreText;
    [SerializeField] Text _liveText;
    int _score = 0;
    public int _live = 50;

    public Image _ImageEnmyVTManager;
    public Text _TxtEnemyVTManager;
    // Start is called before the first frame update
    private void Start()
    {
        updateText();
        setVitriScore();
    }
    private void Update()
    {
        updateText();
        
    }

    public void setScore(int score)
    {
        if (score < 0 || score >= 6)
            return;
        _score += score;
    }
    public void setLive(int score)
    {
        if (_live < 1)
            return;
        _live -= score;
    }
    void updateText()
    {
        _scoreText.text = "Score : "+"\n" + _score.ToString() ;
        _liveText.text = "Live : " + _live.ToString();
    }

    void setVitriScore()
    {
        //_scoreText.transform.position = Camera.main.WorldToScreenPoint(PlayerTest._instan.gameObject.transform.position+ new Vector3(0,5f,0));
        _scoreText.transform.position = Camera.main.WorldToScreenPoint(PlayerController._instan.gameObject.transform.position + new Vector3(0, 5f, 0));
    }
    public void HoaTo()
    {
        _scoreText.rectTransform.anchoredPosition += new Vector2(0, 10f);
    }    
   
}
