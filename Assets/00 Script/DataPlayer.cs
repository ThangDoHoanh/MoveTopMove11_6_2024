using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlayer : ItemIvenBase
{
    private void Start()
    {
        //GetTingDataPlayer();
    }

    void GetTingDataPlayer()
    {
        if (_info._hairSkin != null)
        {
            GameObject hairClone = ObjectPooling._instan.GetObjectparent(_info._hairSkin, ShopManager._instan._hair.transform);
            hairClone.SetActive(true);
        }
        if (_info._LefpHandSkin != null)
        {
            GameObject _lefpHandClone = ObjectPooling._instan.GetObjectparent(_info._LefpHandSkin, ShopManager._instan._lefpHand.transform);
            _lefpHandClone.SetActive(true);
        }
        if (_info._SpineSkin != null)
        {
            GameObject _lefpHandClone = ObjectPooling._instan.GetObjectparent(_info._SpineSkin, ShopManager._instan._spnie.transform);
            _lefpHandClone.SetActive(true);
        }
        if (_info._materialSkin != null)
        {
            ShopManager._instan._skinnedPlayer.material = _info._materialSkin;
        }
        if (_info._pantSkin != null)
        {
            ShopManager._instan._pantsPlayer.material = _info._pantSkin;
        }
        if (_info._Right != null)
        {
            GameObject _rightClone = ObjectPooling._instan.GetObjectparent(_info._Right, ShopManager._instan._RightHand.transform);
            _rightClone.SetActive(true);
        }
    }
     

}
