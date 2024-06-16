using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletEnemyController : MonoBehaviour
{
    [SerializeField] float _speed, _speedAgle, _lifeTime;
    Coroutine _coroutine = null;
    [SerializeField] Rigidbody _rigiBullet;
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
   
  
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag(CONSTANT.PLAYER))
        //{
        //    Debug.Log("ga!!!");
        //    this.gameObject.SetActive(false);
        //}
        //if (!collision.gameObject.CompareTag(CONSTANT.HIDDEN_WALL))
        //{
        //    this.gameObject.SetActive(false);
        //}
        //if (!collision.gameObject.CompareTag(CONSTANT.FENCE))
        //{
        //    this.gameObject.SetActive(false);
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(CONSTANT.PLAYER))
        {
            PlayerController._instan.PlayerDead();
           
            Debug.Log("ga!!!");
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

        if (other.gameObject.CompareTag(CONSTANT.ENEMY))
        {
            other.gameObject.GetComponent<Animator>().SetTrigger(CONSTANT.DEAD);

            //other.gameObject.GetComponent<EnemyController>()._isDaed = true;

            UIManager._instan.setLive(1);
            //UiManager._instan.setLive(1);
            //GameManager._instan.removeList(other.transform);

            Debug.Log("-1!!");
            this.gameObject.SetActive(false);
        }
        
    }
}
