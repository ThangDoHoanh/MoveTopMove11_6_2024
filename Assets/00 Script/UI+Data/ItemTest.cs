using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class ItemTest : ItemIvenBaseTeset
{
    [SerializeField] Button _BTNbuy;
    [SerializeField] Image _imageItem;
    [SerializeField] Text _conts;
 
    private void OnDrawGizmosSelected()
    {
        _imageItem.sprite = _info._sprite;
        _conts.text = _info._conts.ToString();
    }
}
