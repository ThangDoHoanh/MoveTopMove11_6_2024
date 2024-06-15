using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "itemData", menuName = "Item")]
public class ITemDataSO : ScriptableObject // khai báo data ở ScriptableObject (cách lưu dữ liệu của unity)
{
    public int _id;
    public string _name;
    public int _conts;
    public bool _owned;
    public ItemType _itemType;

    public GameObject _hairSkin;
    public GameObject _LefpHandSkin;
    public GameObject _SpineSkin;

}
public enum ItemType
{
     Hair,
     Pants,
    Spine,
    LeftHand,
    Skin,
    Other
}
