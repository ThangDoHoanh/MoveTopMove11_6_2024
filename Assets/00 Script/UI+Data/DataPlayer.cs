using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataPlayer
{   const string ALL_DATA = "all_data";
    static AllData _allData;
    static DataPlayer()
    {
        _allData = JsonUtility.FromJson<AllData>(PlayerPrefs.GetString(ALL_DATA));
    }
    static void SaveData()
    {
        var data = JsonUtility.ToJson(ALL_DATA);
        PlayerPrefs.SetString(ALL_DATA, data);
    }
     public static bool IsOwnedHairWithid(int _id)
    {
        return _allData.IsOwnedHairWithid(_id);
    }
}

public class AllData
{
    public List<int> _listhair;

    public bool IsOwnedHairWithid(int _id)
    {
        return _listhair.Contains(_id);
    }
}
