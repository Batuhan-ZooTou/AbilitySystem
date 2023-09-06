using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    public IObjectPool<Projectile> projectilePool;
    public ProjectileSO projectileSO;
    public Transform projectileOwner;
    private Rigidbody projectileRigidbody;
    public Vector3 spawnPoint { private get; set; }
    public Vector3 travelDirection { private get; set; }
    public Collider playerCollider { private get; set; }

    void Awake()
    {
        projectileRigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (Vector3.Distance(spawnPoint, transform.position) < projectileSO.baseMaxRange)
        {
            projectileRigidbody.velocity = new Vector3(travelDirection.x * projectileSO.baseTravelSpeed, 0, travelDirection.z * projectileSO.baseTravelSpeed);
        }
        else
        {
            projectilePool.Release(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CoreHealth hit))
        {
            hit.TakeDamage(projectileSO.baseDamage);
        }
        projectilePool.Release(this);
    }
    
}
