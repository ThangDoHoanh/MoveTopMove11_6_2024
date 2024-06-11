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
    
    private void Update()
    {
        if (_taget != null)
        {
            Vector3 tagetrPosition = _taget.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, tagetrPosition, ref _velocity, smoothTime);
        }
        
    }
   
        
    public void HoaTo()
    {
        _offset.y += 5f;
        _offset.z -= 5f;
    }
    

}
