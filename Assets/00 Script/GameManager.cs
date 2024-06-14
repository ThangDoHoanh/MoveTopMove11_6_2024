using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;

public class GameManager : Singleton<GameManager>
{
    //[SerializeField] EnemyController _prefabEnemy;
    public CameraPlayer _cameraPlayer;
    [SerializeField] EnemyTest _prefabEnemyTest;
    [SerializeField] GameObject _gobjPlayer;
    public Transform _Player;
    public Collider platformCollider;
    public int _live;
    int _enemyCount=1;
    

    public List<Material> _materialsEnemy= new List<Material>();
    public List<Material> _materialsPant = new List<Material>();
    public List<Material> _material_Fence = new List<Material>();
    
    public List<GameObject> _listTarget = new List<GameObject>();
   
    public List<Material> _materialAvataPlayer = new List<Material>();
    [Header("----ListPlayer-----")]
    public List<GameObject> _skinPlayer = new List<GameObject>();
    public List<GameObject> _hairAvataPlayer = new List<GameObject>();
    public List<GameObject> _spnieAvataPlayer = new List<GameObject>();
    public List<GameObject> _lefpHandAvataPlayer = new List<GameObject>();
    [Header("----Conts-----")]
    public int _contsPlayer;
   
    private void Start()
    {
        _listTarget.Add(_gobjPlayer);

        _cameraPlayer.enabled = false;
        if (_hairAvataPlayer != null)
        {
            _hairAvataPlayer.Clear();
        }
        _hairAvataPlayer = Resources.LoadAll<GameObject>("prefabBodyHair").ToList();
        if (_lefpHandAvataPlayer != null)
        {
            _lefpHandAvataPlayer.Clear();
        }
        _lefpHandAvataPlayer = Resources.LoadAll<GameObject>("prefabBodyLefhand").ToList();
        if (_lefpHandAvataPlayer != null)
        {
            _lefpHandAvataPlayer.Clear();
        }
        _lefpHandAvataPlayer = Resources.LoadAll<GameObject>("prefabBodyLefhand").ToList();

    }
    //public List<Transform> GetItems(Transform banthan)
    //{
    //    List<Transform> tempList = new List<Transform>(_listtarget); // Tạo một bản sao của _listtarget
    //    tempList.Remove(banthan); // Xóa bản thân khỏi bản sao
    //    tempList.RemoveAll(item => item == null || !item.gameObject.activeSelf); // Loại bỏ các mục không hợp lệ



    //    // Trả về bản sao đã chỉnh sửa
    //    return tempList;
    //}
    //public void addList(Transform aaa)
    //{
    //    _listtarget.Add(aaa);
    //}    
    //public void removeList(Transform aaa)
    //{
    //    _listtarget.Remove(aaa);
    //}
    public void _startAddEnemy()
    {
        StartCoroutine(InvokeAfterTime());
    }
        
    void addEnemy()
    {
        //if (_enemyCount < _live)
        //{
            Vector3 randomPosition = GetRandomPositionOnPlatform();
            GameObject eneymy = ObjectPooling._instan.GetObject(_prefabEnemyTest.gameObject);
            //_listTest.Add(eneymy);
            eneymy.transform.position = randomPosition;
            eneymy.transform.localScale = PlayerController._instan._scalePlayer.localScale;

            eneymy.SetActive(true);
            _enemyCount++;

        //}

        //if (_enemyCount < _live)
        //{
        //    Vector3 randomPosition = GetRandomPositionOnPlatform();
        //    GameObject eneymy = ObjectPooling._instan.GetObject(_prefabEnemyTest.gameObject);

        //    eneymy.transform.position = randomPosition;
        //    eneymy.transform.localScale = PlayerTest._instan._scalePlayer.localScale;

        //    eneymy.SetActive(true);
        //    _enemyCount++;

        //}

    }


    Vector3 GetRandomPositionOnPlatform()
    {

        Vector3 platformSize = platformCollider.bounds.size;

        Vector3 playerPosition = _Player.transform.position;

        Vector3 newPos = Vector3.zero;
        float distanceToPlayer; // Di chuyển biến này ra ngoài vòng lặp

        do
        {
            newPos.x = Random.Range(-platformSize.x / 2, platformSize.x / 2);
            newPos.z = Random.Range(-platformSize.z / 2, platformSize.z / 2);

            // Tính toán khoảng cách giữa vị trí mới và vị trí của player
            distanceToPlayer = Vector3.Distance(playerPosition, newPos);

        } while (distanceToPlayer < 7);

        return new Vector3(newPos.x, newPos.y + 0.6f, newPos.z);

    }

    IEnumerator InvokeAfterTime()
    {
        while (_enemyCount < _live)
        {
            yield return new WaitForSeconds(
               Random.Range(2, 7));

            addEnemy();

        }
    }
}
