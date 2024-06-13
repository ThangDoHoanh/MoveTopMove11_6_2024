using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField] GameObject _hair;
    [SerializeField] GameObject _spnie;
    [SerializeField] GameObject _lefpHand;
    [SerializeField] SkinnedMeshRenderer _skinnedPlayer;
    [SerializeField] SkinnedMeshRenderer _pantsPlayer;
    [SerializeField] GameObject _panelShop;

    public List<Image> _listImageBTNShop = new List<Image>();
    
    public GameObject _shopHair;
    public GameObject _shopPants;
    public GameObject _shopLefphand;
    public GameObject _shopSkinnedPlayer;
    // Start is called before the first frame update
    public void ResetActifSetPlay()
    {
        foreach (Transform child in _hair.transform)
        {
            child.gameObject.SetActive(false);
        }

        // Tắt tất cả các đối tượng con của _spnie
        foreach (Transform child in _spnie.transform)
        {
            child.gameObject.SetActive(false);
        }

        // Kiểm tra nếu _lefpHand.transform có con trước khi truy cập
        if (_lefpHand.transform.childCount > 0)
        {
            // Tắt tất cả các đối tượng con của _lefpHand
            foreach (Transform child in _lefpHand.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    public void SetSkinPlay1()
    {
        GameObject _hairclone = ObjectPooling._instan.GetObjectparent(GameManager._instan._hairAvataPlayer[0], _hair.transform);
        _hairclone.SetActive(true);
        GameObject _spnieclone = ObjectPooling._instan.GetObjectparent(GameManager._instan._spnieAvataPlayer[0], _spnie.transform);
        _spnieclone.SetActive(true);
        _skinnedPlayer.material = GameManager._instan._materialAvataPlayer[0];
    }
    public void SetSkinPlay2()
    {
        GameObject _hairclone = ObjectPooling._instan.GetObjectparent(GameManager._instan._hairAvataPlayer[1], _hair.transform);
        _hairclone.SetActive(true);

        GameObject _spnieclone = ObjectPooling._instan.GetObjectparent(GameManager._instan._spnieAvataPlayer[1], _spnie.transform);
        _spnieclone.SetActive(true);

        GameObject _lefpHandclone = ObjectPooling._instan.GetObjectparent(GameManager._instan._lefpHandAvataPlayer[0], _lefpHand.transform);
        _lefpHandclone.SetActive(true);
        _skinnedPlayer.material = GameManager._instan._materialAvataPlayer[1];
    }

    public void resetBTN()
    {
        foreach (Transform child in _panelShop.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Image child in _listImageBTNShop)
        {
            child.color = new Color(0.3660378f, 0.2866145f, 0.2866145f);
        }

    }
    public void setShopHair()
    {
        _listImageBTNShop[0].color=Color.red;
        _shopHair.SetActive(true);
    }
    public void setShopPants()
    {
        _listImageBTNShop[1].color = Color.red;
        _shopPants.SetActive(true);
    }
    public void setShopLefphand()
    {
        _listImageBTNShop[2].color = Color.red;
        _shopLefphand.SetActive(true);
    }
    public void setShopSkin()
    {
        _listImageBTNShop[3].color = Color.red;
        _shopSkinnedPlayer.SetActive(true);
    }

}
