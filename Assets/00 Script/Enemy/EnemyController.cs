﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _speedMove, attackRange, _countDownAtkEnemy, _timeMove;

    Vector3 movementVector = Vector3.zero;
    [SerializeField]List<GameObject> _listTaget = new List<GameObject>();
    [SerializeField] Transform _target;
    [SerializeField] Transform _enemy;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Animator _animatorEnemy;
     
    public bool _isMoving = false;
    public float _stopTime;
    float countDownAtk = 0;

    [SerializeField] Transform _diemban;
    [SerializeField]CapsuleCollider _capsuleCollider;

    [SerializeField] bool chuyenhuong = true;
    public bool dangchet=false;
    List<Material> _PantEnemy = new List<Material>();
    [SerializeField] SkinnedMeshRenderer _skinnedPant;
    List<Material> _skinEnemy = new List<Material>();
    [SerializeField] SkinnedMeshRenderer _skinnedEnemy;

    public bool HasCollided { get; private set; } = false;

    public bool _isDaed = false;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _isDaed = false;
        _skinnedEnemy.material = _skinEnemy[UnityEngine.Random.Range(0, 5)];
        _skinnedPant.material = _PantEnemy[UnityEngine.Random.Range(0,5)];
        if (dangchet == true)
        {
            dangchet = false;
        }

        if (_capsuleCollider.enabled == false)
        {
            _capsuleCollider.enabled = true;
        }
        chuyenhuong = true;

        if (PlayerController._instan != null)
        {
            attackRange = PlayerController._instan.attackRange;
        }
        //_listTaget = GameManager._instan.GetItems(this.transform);
    }
    private void OnDisable()
    {
        //_listTaget = GameManager._instan.GetItems(this.transform);

    }
    private void Awake()
    {
        GameManager._instan._listTarget.Add(this.gameObject);
        _PantEnemy = GameManager._instan._PantsPlayer;
        _skinEnemy = GameManager._instan._materialsEnemy;
        //GameManager._instan.addList(this.transform);
    }
    private void Start()
    {
        _listTaget = GameManager._instan._listTarget;

    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (chuyenhuong == true)
        {
            Testdistance();
        }
        if (dangchet == false)
        {
            EnemyMove();
        }


        if (_target != null)
        {
            animoEnemyATk();
        }
    }
    
   
    public void Testdistance()
    {
       
        for (int i = 0; i < _listTaget.Count; i++)
        {
            GameObject z = _listTaget[i];
            float distance = Vector3.Distance(z.transform.position, this.transform.position);

            if (z.activeSelf && distance < attackRange && distance != 0)
            {
                if (!PlayerController._instan._idDead || z != PlayerController._instan.gameObject)
                {
                    _isMoving = false; // Dừng di chuyển
                    _timeMove = 2f;

                    _target = z.transform;
                    break; // Thoát khỏi vòng lặp sau khi tìm thấy mục phù hợp
                }
            }
            else
            {
                _target = null;
            }
        }
    }


    void EnemyMove()
    {
        if (!_isMoving)
        {
            _stopTime -= Time.deltaTime; // Giảm thời gian đứng yên
            if (_stopTime <= 0f)
            {
                _isMoving = true; // Bắt đầu di chuyển
                _stopTime = 2f; // Reset lại thời gian đứng yên
                GenerateMovementVector(); // Tạo vector di chuyển mới
                
            }
            else
            {
                chuyenhuong = true;
                quayhuongEnemy();
                _animatorEnemy.SetBool(CONSTANT.RUN, false);
            }
        }
        else
        {
            // Đặt animation chạy
            _timeMove -= Time.deltaTime; // Giảm thời gian di chuyển
            if (_timeMove <= 0f)
            {
                _isMoving = false; // Dừng di chuyển
                _timeMove = 2f; // Reset lại thời gian di chuyển

            }
            else
            {
                _animatorEnemy.SetBool(CONSTANT.RUN, true);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementVector), 0.15f);
                transform.Translate(movementVector * _speedMove * Time.deltaTime, Space.World);
            }


        }
    }
    void GenerateMovementVector()
    {
        float moveX = UnityEngine.Random.Range(-1f, 1f);
        float moveZ = UnityEngine.Random.Range(-1f, 1f);

        // Tạo vector di chuyển mới
        movementVector = new Vector3(moveX, 0, moveZ).normalized;
    }
    void quayhuongEnemy()
    {
        if (_target != null)
        {
            Vector3 directionToEnemy = (_target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
            Quaternion fixedLookRotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, fixedLookRotation, 1f);

        }
    }

    void animoEnemyATk()
    {
        countDownAtk -= Time.deltaTime;
        if (countDownAtk > 0)
            return;
       
            _animatorEnemy.SetBool(CONSTANT.ATK, true);

            countDownAtk = _countDownAtkEnemy;
            _isMoving = false; // Dừng di chuyển
            _timeMove = 2f;

        chuyenhuong = false;

    }
    
    void enemyATK()// được gọi ở giữa animo atk để sinh ra bullet
    {   _target = null;
        GameObject bulletEnemy = ObjectPooling._instan.GetObject(_bulletPrefab.gameObject);
        bulletEnemy.transform.position = _diemban.transform.position;
        bulletEnemy.transform.rotation = _diemban.transform.rotation;
        bulletEnemy.transform.localScale= this.transform.localScale;
        bulletEnemy.SetActive(true);
        _animatorEnemy.SetBool(CONSTANT.ATK, false);

        _isMoving = true; // Bắt đầu di chuyển
        _stopTime = 2f; // Reset lại thời gian đứng yên
        GenerateMovementVector(); // Tạo vector di chuyển mới
        chuyenhuong = false;
        //StartCoroutine(resetChuyenHuong());
    }

   
    void Deading() //KHi enemy đang chết bời player giết
    {
        Renderer renderer = this.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false; // Disable the renderer
        }
        PlayerController._instan.chuyenhuong = false;
        _capsuleCollider.enabled = false;
        _isMoving = false; // Dừng di chuyển
        _timeMove = 2f;
    }   
    void Dead()//khi enemy chết bởi player
    {
        
        PlayerController._instan.chuyenhuong = true;
        this.gameObject.SetActive(false);
    }

  
    //IEnumerator resetChuyenHuong()
    //{
    //    yield return new WaitForSeconds(3f);
    //    chuyenhuong = true;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(CONSTANT.BULLET))
        {
            _animatorEnemy.SetTrigger(CONSTANT.DEAD);
            dangchet = true;
        }
       

    }


}
