using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopElement : MonoBehaviour
{
    public int id;
    public int cost;

    public Text costTxt;
    public Button purchaseBTn;
    private void Awake()
    {
        purchaseBTn.onClick.AddListener(OnPurchase);
        
    }
    void UpdateView()
    {
        var isOwned = DataPlayer.IsOwnedHairWithid(id);
        if(isOwned)
        {
            purchaseBTn.enabled = false;
            costTxt.text ="Owned";
        }
        else
        {
            purchaseBTn.enabled = true;
            costTxt.text = cost.ToString();
        }
     
    }

    void OnPurchase()
    {

    }
    public void SetData(int _id )
    {
        this.id = _id;
        this.cost = _id*100;
        UpdateView();
    }



}
