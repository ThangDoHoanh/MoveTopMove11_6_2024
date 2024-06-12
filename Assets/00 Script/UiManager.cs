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
    public Canvas _canvaSkin;
    

    Vector3 _canvaEnemyPosition = new Vector3(0,5,0);
    private void Start()
    {
        _canvaSkin.gameObject.SetActive(false);
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
}
