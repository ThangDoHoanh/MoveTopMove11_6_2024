using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [Header("--------ItemSkin--------")]
    [SerializeField] List<ITemDataSO>listDataSkin = new List<ITemDataSO>();
    public List<ITemDataSO> _listDataSkin => listDataSkin;

    //[SerializeField] Transform _gameObject;
    void Start()
    {
        listDataSkin= Resources.LoadAll<ITemDataSO>("dataSkin").ToList();

    }

    private void Init()
    {
        foreach (var item in listDataSkin)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
