using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;


public class PositionEnemy : MonoBehaviour
{
    [SerializeField] Image _ImageEnmyVT; // Hình ảnh hiển thị phía trên kẻ địch
    [SerializeField] Text _TxtEnemyVT;

    // Update is called once per frame
    void Update()
    {
        positonEnemy();

    }
     void positonEnemy()//set vị trí canva trên đầu enemy;
     {

        Vector3 enemyScreenPosition = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 2, 0));
        if (enemyScreenPosition.z < 0)
        {
            // Nếu kẻ địch nằm phía sau camera, thực hiện phép biến đổi để đảo ngược hướng màn hình
            enemyScreenPosition *= -1;
        }
        float minX = _ImageEnmyVT.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = _ImageEnmyVT.GetPixelAdjustedRect().height/2;
        float maxY = Screen.height - minY;

        enemyScreenPosition.x = Mathf.Clamp(enemyScreenPosition.x, minX, maxX);
        enemyScreenPosition.y = Mathf.Clamp(enemyScreenPosition.y, minY, maxY);

        _ImageEnmyVT.transform.position = enemyScreenPosition;

        _TxtEnemyVT.transform.position = enemyScreenPosition + new Vector3(0, _ImageEnmyVT.GetPixelAdjustedRect().height / 2 + 20, 0);

        float distanceToEnemy = Vector3.Distance(this.transform.position, PlayerController._instan.gameObject.transform.position);
        _TxtEnemyVT.text = distanceToEnemy.ToString("F1") + "m"; // Định dạng để hiển thị một chữ số thập
        

    }
}
