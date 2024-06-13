using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopPopup : MonoBehaviour
{
    public UIShopElement[] _uIShopElements;

    private void OnValidate()
    {
        if(_uIShopElements ==null|| _uIShopElements.Length==0)
        {
            _uIShopElements = GetComponentsInChildren<UIShopElement>();//Cập nhật lại tất cả các uishopelement
        }
    }
    private void SetData()
    {
        for(int i = 0; i <_uIShopElements.Length; i ++)
        {
            _uIShopElements[i].SetData(i+1);
        }
    }
    private void Awake()
    {
        SetData();
    }

}
