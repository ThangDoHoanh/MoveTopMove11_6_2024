using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraPlayer : Singleton<CameraPlayer>
{
    [SerializeField] Transform _taget;
    float smoothTime = 0.01f;
    [SerializeField] Vector3 _offset;
    Vector3 _velocity = Vector3.zero;
    public bool _isPlaying =false;


    private void Awake()
    {
        
        setMapHome();
    }
   
    private void FixedUpdate()
    {
        
            setMap1();
    }
    void setMap1()
    {
        if (_taget != null)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            Vector3 tagetrPosition = _taget.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, tagetrPosition, ref _velocity, smoothTime);
          

        }

    }
    void setMapHome()
    {
        this.transform.rotation = Quaternion.Euler(-30, 0, 0);
    }
    public void HoaTo()
    {
        _offset.y += 5f;
        _offset.z -= 5f;
    }
    

}
