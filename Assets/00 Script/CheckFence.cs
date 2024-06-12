using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFence : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SphereCollider sphereCollider;
    private void Update()
    {
        //    sphereCollider.radius = PlayerTest._instan.attackRange;
        sphereCollider.radius = PlayerController._instan.attackRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(CONSTANT.FENCE))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if(renderer != null)
            {
                renderer.material = GameManager._instan._material[0];
            }
        }
        if (other.gameObject.CompareTag(CONSTANT.BAN))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = GameManager._instan._material[1];
            }
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(CONSTANT.FENCE))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = GameManager._instan._material[2];
            }
        }
        if (other.gameObject.CompareTag(CONSTANT.BAN))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = GameManager._instan._material[3];
            }
        }
    }
}
