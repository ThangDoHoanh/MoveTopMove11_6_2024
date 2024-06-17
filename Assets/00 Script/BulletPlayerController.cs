using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletPlayerController : MonoBehaviour
{
    [SerializeField] float _speed,_speedAgle, _lifeTime;
    Coroutine _coroutine = null;
    [SerializeField]Rigidbody _rigiBullet;
    [SerializeField] Transform _quaydeu;
   
    void Start()
    {

        _rigiBullet.velocity = transform.forward * _speed;

    }

    private void OnEnable()
    {
        StartCoroutine(DeactiveAfterTime());
        if (_rigiBullet != null)
        {
            _rigiBullet.velocity = transform.forward * _speed;
        }
        
    }
    // Start is called before the first frame update
    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        PlayerController._instan._bulletTrenNguoi.SetActive(true);
        PlayerController._instan.chuyenhuong = true;
    }

    // Update is called once per frame
    void Update()
    {
       
        rotateBullet();
    }

    void rotateBullet()
    {
        _quaydeu.transform.Rotate(0f, 0f, _speedAgle * Time.deltaTime);
    }

    IEnumerator DeactiveAfterTime()
    {
        yield return new WaitForSeconds(_lifeTime);
        _rigiBullet.velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }
    
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(CONSTANT.ENEMY))
        {
            GameManager._instan.AddMoney(UnityEngine.Random.Range(1, 6));

            PlayerController._instan.hoaTo();

            UIManager._instan.setScore(UnityEngine.Random.Range(1, 6));
            Debug.Log("+1!!!");
            UIManager._instan.setLive(1);

            this.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag(CONSTANT.FENCE))
        {
            this.gameObject.SetActive(false);
            Debug.Log("FENCE!");
        }
        if (other.gameObject.CompareTag(CONSTANT.BAN))
        {
            this.gameObject.SetActive(false);
            Debug.Log("BAN!");
        }

    }
}
