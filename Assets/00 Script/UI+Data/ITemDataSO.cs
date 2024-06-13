using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "itemData", menuName = "Item")]
public class ITemDataSO : ScriptableObject
{
    public int _id;
    public string _name;
    public Sprite _sprite;
    public int _conts;
   
}
