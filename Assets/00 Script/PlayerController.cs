﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] float _speed, _speedAgle, _countDownAtkPlayer;
    public float attackRange;
    Vector2 _move;
    public Transform _Target;
    [SerializeField] Transform _tfplayer;
    [SerializeField] GameObject _bulletPrefab;
    float countDownAtk = 0;
    [SerializeField] bool _isMoving = true;
    [SerializeField] Animator _animator;
    public List<GameObject> _listEnemyTaget = new List<GameObject>();
    public Transform _scalePlayer;
    [SerializeField] CameraPlayer _camera;

    public SpriteRenderer _spriteRenderer;//RangeATKRenderer
    [SerializeField] GameObject _AimSpriteRenderer;//Aim

    [SerializeField] LayerMask _layerMask;
    public bool chuyenhuong=true;
    public bool _idDead =false;
    public bool _isPause=true;
    
    public GameObject _bulletTrenNguoi;
    [Header("-----body-----")]
    [SerializeField] GameObject _hair;
    [SerializeField] GameObject _spnie;
    [SerializeField] GameObject _lefpHand;
    [SerializeField] SkinnedMeshRenderer _skinnedPlayer;
    
    public DataPlayer _dataSOPlayer;

   
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
    }
    void Start()
    {
        _listEnemyTaget = GameManager._instan._listTarget;
        if (_AimSpriteRenderer.activeSelf == true)
        {
            _AimSpriteRenderer.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPause==false)
        {
            countDownAtk -= Time.deltaTime;
            rangAtk();
            movePlayer();
        }
        
       
    }

    void movePlayer()
    {
        Vector3 movement = new Vector3(_move.x, 0f, _move.y);

        float movementThreshold = 0.1f; // Giá trị ngưỡng để kiểm tra xem movement có quá nhỏ hay không

        if (movement.magnitude < movementThreshold)
        {
            _isMoving = false;
            _animator.SetBool(CONSTANT.RUN, false);
            
            if(chuyenhuong == true)
            {
                checkDistance();
                if (_Target != null)
                {
                    addAnim();
                    animoPlayerATk();
                   
                }
                else
                {

                    _AimSpriteRenderer.SetActive(false);
                }
            }
            
                quayhuong();

            return; // Không thực hiện bất kỳ hành động nào nếu movement quá nhỏ
        }
        
        _isMoving = true;
        //chuyenhuong = true;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);


        transform.Translate(movement.normalized * _speed * Time.deltaTime, Space.World);
        _animator.SetBool(CONSTANT.RUN, true);
    }

    public void checkDistance()
    {
        
        for (int i = 0; i < _listEnemyTaget.Count; i++)
        {
            GameObject z = _listEnemyTaget[i];

            float distance = Vector3.Distance(z.transform.position, this.transform.position);

          
            if (z.gameObject.activeSelf==true)
            {
                
                if (distance < attackRange && distance != 0)
                {
                       _Target = z.transform;
                   
                        break;
                    
                     // Thoát khỏi vòng lặp sau khi tìm thấy mục phù hợp
                }
            }
            else
            {
                
                _Target = null;
            }
        }

    }
    void quayhuong()
    {
        if (_Target != null)
        {
            Vector3 directionToEnemy = (_Target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
            Quaternion fixedLookRotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, fixedLookRotation, 1f);
        }
    }
    void checkMove()// event playerATK
    {
        if (_isMoving == false)
        {
            
            playerATK();
           
            return;
        }
        chuyenhuong = true;
        _animator.SetBool(CONSTANT.RUN, true);
    }
    void animoPlayerATk()
    {
        if (countDownAtk > 0 || _isMoving == true)
            return;
        _animator.SetTrigger(CONSTANT.ATK);

        countDownAtk = _countDownAtkPlayer;
    }
    void playerATK()
    {
        chuyenhuong = false;
        _bulletTrenNguoi.gameObject.SetActive(false);
        _Target = null;
        //GameObject bulletPlayer = ObjectPooling._instan.GetObject(_bulletPrefab.gameObject);
        GameObject bulletPlayer = ObjectPooling._instan.GetObject(_bulletPrefab.gameObject);
        Vector3 bulletPosition = this.transform.position + this.transform.forward + new Vector3(0, 1, 0);
        bulletPlayer.transform.position = bulletPosition;
        bulletPlayer.transform.rotation = this.transform.rotation;
        bulletPlayer.transform.localScale = _scalePlayer.transform.localScale;
        bulletPlayer.SetActive(true);
        
    }
    public void hoaTo()
    {
        transform.position += new Vector3(0, 0.05f, 0);
        _scalePlayer.gameObject.transform.localScale += Vector3.one * 0.5f;
        _camera.HoaTo();
        //_scalerRangAtk.gameObject.transform.localScale += Vector3.one * 0.5f;
        attackRange += 0.5f;
        _speed += 0.5f;
        
        UIManager._instan.HoaTo();
    }
    private void OnDrawGizmos()
    {
        // Chọn màu của Gizmo
        Gizmos.color = Color.red;

        // Vẽ vòng tròn xung quanh đối tượng này
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    void rangAtk()
    {
        float spriteDiameter = _spriteRenderer.sprite.bounds.size.x;

        // Tính tỷ lệ phóng đại để mở rộng sprite đến đường kính mong muốn (2 lần attackRange)
        float scaleFactor = (attackRange * 2) / spriteDiameter;

        // Đặt scale cho vòng tròn sprite
        _spriteRenderer.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
    }
    void addAnim()// bật tắt aimo enemy
    {
        _AimSpriteRenderer.transform.localPosition = _Target.transform.position;
        _AimSpriteRenderer.SetActive(true);
    }
    
    public void SetMapSkin(bool _setAnim)
    {
        _animator.SetBool(CONSTANT.DANCE, _setAnim);
    }

     public void PlayerDead()//khi play dính đạn của enemy
        {
            Renderer renderer = this.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false; // Disable the renderer
            }
            _idDead = true;
            _animator.SetTrigger(CONSTANT.DEAD);
             UIManager._instan.panelReviveNow();

      }    
    
    public void continuePlay()
    {
        _animator.SetTrigger(CONSTANT.IDLE);

        // Tìm vị trí an toàn trên nền tảng
        Vector3 safePosition = GetSafePositionOnPlatform();

        // Đặt người chơi tại vị trí đó
        this.transform.position = safePosition;

        _Target = null;

        // Khôi phục các trạng thái cần thiết
        _idDead = false;
        _isPause = false;

        // Bật lại renderer nếu nó bị tắt khi người chơi chết
        Renderer renderer = this.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }
    }

    private Vector3 GetSafePositionOnPlatform()
    {
        // Kích thước nền tảng
        Vector3 platformSize = GameManager._instan.platformCollider.bounds.size;

        Vector3 playerPosition = new Vector3(0,1,0);
        float minSafeDistance = attackRange; // Khoảng cách an toàn tối thiểu từ enemy

        // Lặp lại cho đến khi tìm được một vị trí an toàn
        bool isSafe;
        do
        {
            // Tạo vị trí ngẫu nhiên trong phạm vi nền tảng
            playerPosition.x = Random.Range(-platformSize.x / 2, platformSize.x / 2);
            playerPosition.z = Random.Range(-platformSize.z / 2, platformSize.z / 2);
            playerPosition.y = GameManager._instan.platformCollider.bounds.center.y;

            isSafe = true;

            // Kiểm tra khoảng cách đến tất cả các enemy
            foreach (GameObject enemy in GameManager._instan._listTarget)
            {
                if (enemy.activeSelf)
                {
                    float distanceToEnemy = Vector3.Distance(playerPosition, enemy.transform.position);
                    if (distanceToEnemy < minSafeDistance)
                    {
                        isSafe = false;
                        break;
                    }
                }
            }

        } while (!isSafe);

        return playerPosition;
    }
    public void SetDataPlayer(int _id ,ItemType _type)
    {
        switch (_type)
        {
            case ItemType.Hair:
                foreach (Transform _cloneHair in ShopManager._instan._hair.transform)
                {
                    if (_cloneHair.gameObject.activeSelf == true)
                    {
                        _dataSOPlayer._info._hairSkin = null;
                        _dataSOPlayer._info._hairSkin = GameManager._instan._hairAvataPlayer[_id];
                    }
                }
                break;

            case ItemType.Spine:
                foreach (Transform _cloneSpnie in ShopManager._instan._spnie.transform)
                {
                    if (_cloneSpnie.gameObject.activeSelf == true)
                    {
                        _dataSOPlayer._info._SpineSkin = null;
                        _dataSOPlayer._info._SpineSkin = GameManager._instan._spnieAvataPlayer[_id];
                    }
                }
                break;

            case ItemType.LeftHand:
                foreach (Transform _cloneLefpHand in ShopManager._instan._lefpHand.transform)
                {
                    if (_cloneLefpHand.gameObject.activeSelf == true)
                    {
                        _dataSOPlayer._info._LefpHandSkin = null;
                        _dataSOPlayer._info._LefpHandSkin = GameManager._instan._lefpHandAvataPlayer[_id];
                    }
                }
                break;

            case ItemType.Pants:
                _dataSOPlayer._info._pantSkin = GameManager._instan._PantsPlayer[_id];
                break;

            case ItemType.Skin:
                _dataSOPlayer._info._materialSkin = GameManager._instan._materialAvataPlayer[_id];
                break;

            default:
                Debug.LogWarning("Unhandled item type in SetDataPlayer: " + _type);
                break;
        }

    }
    public void GetTingDataPlayer()
    {
        if (_dataSOPlayer._info._hairSkin != null)
        {
            GameObject hairClone = ObjectPooling._instan.GetObjectparent(_dataSOPlayer._info._hairSkin, ShopManager._instan._hair.transform);
            hairClone.SetActive(true);
        }
        if (_dataSOPlayer._info._LefpHandSkin != null)
        {
            GameObject _lefpHandClone = ObjectPooling._instan.GetObjectparent(_dataSOPlayer._info._LefpHandSkin, ShopManager._instan._lefpHand.transform);
            _lefpHandClone.SetActive(true);
        }
        if (_dataSOPlayer._info._SpineSkin != null)
        {
            GameObject _lefpHandClone = ObjectPooling._instan.GetObjectparent(_dataSOPlayer._info._SpineSkin, ShopManager._instan._spnie.transform);
            _lefpHandClone.SetActive(true);
        }
        if (_dataSOPlayer._info._materialSkin != null)
        {
            ShopManager._instan._skinnedPlayer.material = _dataSOPlayer._info._materialSkin;
        }
        if (_dataSOPlayer._info._pantSkin != null)
        {
            ShopManager._instan._pantsPlayer.material = _dataSOPlayer._info._pantSkin;
        }
        if (_dataSOPlayer._info._Right != null)
        {
            GameObject _rightClone = ObjectPooling._instan.GetObjectparent(_dataSOPlayer._info._Right, ShopManager._instan._RightHand.transform);
            _rightClone.SetActive(true);
        }
    }
    public void GetWeapon()
    {
        if (_dataSOPlayer._info._Right != null)
        {
            GameObject _RightHandClone = ObjectPooling._instan.GetObjectparent(_dataSOPlayer._info._Right, ShopManager._instan._RightHand.transform);
            _RightHandClone.SetActive(true);
        }
    }
    public void SetWeapon(int _id)
    {
        foreach (Transform _cloneright in ShopManager._instan._RightHand.transform)
        {
            if (_cloneright.gameObject.activeSelf == true)
            {
                _dataSOPlayer._info._Right = null;
                _dataSOPlayer._info._Right = GameManager._instan._weapon[_id];
                
            }
        }
    }
    
    void testGit()
    {
        //abcd
    }
    

}
