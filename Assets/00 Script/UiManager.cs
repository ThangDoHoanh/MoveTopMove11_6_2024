using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] Text _scoreText;
    [SerializeField] Text _liveText;
    int _score = 0;
    int _livetime;

    public Canvas _canvaMapPlaying;
    public Canvas _canvaMapHome;
    private void Start()
    {
        _canvaMapPlaying.gameObject.SetActive(false);
        _canvaMapHome.gameObject.SetActive(true);
        _livetime = GameManager._instan._live;
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
        //_scoreText.transform.position = Camera.main.WorldToScreenPoint(PlayerTest._instan.gameObject.transform.position+ new Vector3(0,5f,0));
        _scoreText.transform.position = Camera.main.WorldToScreenPoint(PlayerController._instan.gameObject.transform.position + new Vector3(0, 7f, 0));
    }
    public void HoaTo()
    {
        _scoreText.rectTransform.anchoredPosition += new Vector2(0, 10f);
    }
}
