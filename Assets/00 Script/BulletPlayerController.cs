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

        //if(PlayerTest._instan._Target.position - transform.position==null)
        //{
        //    _rigiBullet.velocity = transform.forward * _speed;
        //}
        //else
        //{
        //    Vector3 direction = (PlayerTest._instan._Target.position - transform.position).normalized;

        //    _rigiBullet.velocity = direction * _speed;
        //}

        // Đặt vận tốc của đạn theo hướng tới mục tiêu với tốc độ _speed
        _rigiBullet.velocity = transform.forward * _speed;

    }

    private void OnEnable()
    {
        StartCoroutine(DeactiveAfterTime());
        if (_rigiBullet != null)
        {
            _rigiBullet.velocity = transform.forward * _speed;
        }
        //StartCoroutine(DeactiveAfterTime());
        //if (_rigiBullet != null)
        //{
        //    Vector3 direction = (PlayerTest._instan._Target.position - transform.position).normalized;

        //    _rigiBullet.velocity = direction * _speed;
        //}
    }
    // Start is called before the first frame update
    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
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
            //PlayerTest._instan._listEnemyTaget = null;
            PlayerController._instan.hoaTo();

            //PlayerTest._instan.hoaTo();
            //PlayerController._instan._Target = null;
            


            UiManager._instan.setScore(UnityEngine.Random.Range(1, 6));
            Debug.Log("+1!!!");
            UiManager._instan.setLive(1);

            other.gameObject.GetComponent<Animator>().SetTrigger(CONSTANT.DEAD);
            other.gameObject.GetComponent<EnemyTest>().dangchet = true;
            
            //PlayerTest._instan.removList(other.transform);
            //GameManager._instan.removeList(other.transform);

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
