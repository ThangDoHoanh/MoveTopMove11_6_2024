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
    public bool _owned;
   public ItemType _itemType;
}
public enum ItemType
{
     Hair,
    Spine,
    LeftHand,
    Skin,
    Other
}
