﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class PositionEnemy : MonoBehaviour
{
    [SerializeField] Image _ImageEnmyVT; // Hình ảnh hiển thị phía trên kẻ địch
    [SerializeField] Text _TxtEnemyVT;
    private string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";


    void Start()
    {
        string randomName = GenerateRandomName(6); // Tạo tên với độ dài 6 ký tự
        _TxtEnemyVT.text = randomName;
    }
    // Update is called once per frame
    void Update()
    {
        positonEnemy();

    }
    
    void positonEnemy()
    {
        
        // Lấy vị trí của kẻ địch trong không gian màn hình
        Vector3 enemyScreenPosition = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 2, 0));

        // Kiểm tra xem kẻ địch có nằm trong tầm nhìn của camera không
        bool isInScreen = enemyScreenPosition.x > 0 && enemyScreenPosition.x < Screen.width &&
                          enemyScreenPosition.y > 0 && enemyScreenPosition.y < Screen.height &&
                          enemyScreenPosition.z > 0;

            
          
        float minX = _ImageEnmyVT.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = _ImageEnmyVT.GetPixelAdjustedRect().height/2;
        float maxY = Screen.height - minY;
        // Nếu kẻ địch nằm trong tầm nhìn, hiển thị hình ảnh và văn bản
        if (isInScreen)
        {
            _ImageEnmyVT.enabled = false;
            _TxtEnemyVT.enabled = true;

            _TxtEnemyVT.transform.position = enemyScreenPosition + new Vector3(0, _ImageEnmyVT.GetPixelAdjustedRect().height / 2 + 20, 0);
            //float distanceToEnemy = Vector3.Distance(this.transform.position, PlayerController._instan.gameObject.transform.position);
            //_TxtEnemyVT.text = distanceToEnemy.ToString("F1") + "m"; // Hiển thị khoảng cách với 1 số thập phân

            // Đặt hướng của hình ảnh
            _ImageEnmyVT.transform.rotation = Quaternion.Euler(0f, 0f, 180);
        }
        else
        {
            if (enemyScreenPosition.z < 0)
            {

                // Nếu kẻ địch nằm phía sau camera, thực hiện phép biến đổi để đảo ngược hướng màn hình
                enemyScreenPosition *= -1;

            }
            _TxtEnemyVT.enabled = false;
            _ImageEnmyVT.enabled = true;
            enemyScreenPosition.x = Mathf.Clamp(enemyScreenPosition.x, minX, maxX);
            enemyScreenPosition.y = Mathf.Clamp(enemyScreenPosition.y, minY, maxY);

            _ImageEnmyVT.transform.position = enemyScreenPosition + new Vector3(0, -_ImageEnmyVT.GetPixelAdjustedRect().height / 2 + 20, 0);
            //_TxtEnemyVT.transform.position = enemyScreenPosition + new Vector3(0, -_ImageEnmyVT.GetPixelAdjustedRect().height / 2 + 20, 0);


            // Tính khoảng cách đến người chơi
            Vector3 directionToEnemy = _ImageEnmyVT.transform.position - Camera.main.WorldToScreenPoint(PlayerController._instan.gameObject.transform.position);
            directionToEnemy.Normalize(); // Chuẩn hóa vector hướng

            // Tính góc quay để trục Z của _ImageEnmyVT hướng về _ImageEnmyVT
            float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
            // Áp dụng quay trục Z
            _ImageEnmyVT.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        }
    }

    string GenerateRandomName(int length)
    {
        char[] nameArray = new char[length];
        for (int i = 0; i < length; i++)
        {
            nameArray[i] = characters[Random.Range(0, characters.Length)];
        }
        return new string(nameArray);
    }
}    
    


