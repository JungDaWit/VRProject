using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRange : MonoBehaviour
{

    [SerializeField] private float detectRadius;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform muzzlePos;
    [SerializeField] private float bulletTime;

    private Coroutine fireCorotine;
    private void Update()
    {
        DetectPlayer();
    }
    private void DetectPlayer()
    {
        if (Physics.OverlapSphere(transform.position,detectRadius,PlayerLayer).Length>0)
        {
           // Debug.Log("플레이어가 감지됨");
            if (fireCorotine == null)
            {
                fireCorotine = StartCoroutine(Fire());
            }
        }
        else
        {
           // Debug.Log("감지X");
            if(fireCorotine != null)
            {
                StopCoroutine(fireCorotine);
                fireCorotine = null;
            }
        }
       
    }
    private IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject bullet = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
            Destroy(bullet, bulletTime);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}

