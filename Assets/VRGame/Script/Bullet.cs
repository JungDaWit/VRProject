using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigid;
    [SerializeField] private float firePower;
    private void Start()
    {
        _rigid.AddForce(transform.forward * firePower, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}
