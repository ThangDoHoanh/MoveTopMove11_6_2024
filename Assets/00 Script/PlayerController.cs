using System.Collections;
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

    [SerializeField] SpriteRenderer _spriteRenderer;//RangeATKRenderer
    [SerializeField] GameObject _AimSpriteRenderer;//Aim

    [SerializeField] LayerMask _layerMask;
    public bool chuyenhuong=true;

    public bool _isPause=true;

    [SerializeField] GameObject _bulletTrenNguoi;

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

        //chuyenHuongTarget();

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
    public void chuyenHuongTarget()
    {
        StartCoroutine(resetChuyenHuong());
    }    
    public void hoaTo()
    {
        transform.position += new Vector3(0, 0.05f, 0);
        _scalePlayer.gameObject.transform.localScale += Vector3.one * 0.5f;
        _camera.HoaTo();
        //_scalerRangAtk.gameObject.transform.localScale += Vector3.one * 0.5f;
        attackRange += 0.5f;
        _speed += 0.5f;
        //UiManager._instan.HoaTo
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
    void addAnim()
    {
        _AimSpriteRenderer.transform.localPosition = _Target.transform.position;
        _AimSpriteRenderer.SetActive(true);
    }
    IEnumerator resetChuyenHuong()
    {
       
        yield return new WaitForSeconds(4f);
        chuyenhuong = true;
        _bulletTrenNguoi.SetActive(true);
        
    }    

}
