using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance;
    private ThirdPersonController caster;
    [Header("ProjectileLMB")]
    public IObjectPool<Projectile> projectileLMBPool;
    public Projectile projectileLMBPrefab;
    public Transform projectileLMBHolder;
    [Header("ProjectileRMB")]
    public IObjectPool<Projectile> projectileRMBPool;
    public Projectile projectileRMBPrefab;
    public Transform projectileRMBHolder;
    [Header("DamagePopup")]
    public IObjectPool<DamagePopup> damagePopupPool;
    public DamagePopup damagePopupPrefab;
    public Transform damagePopupHolder;

    private void Awake()
    {
        Instance = this;
        projectileLMBPool = new ObjectPool<Projectile>(CreateProjectileLMB, OnTakeFromPoolProjectileLMB, OnReturnedToPoolProjectileLMB, null, true, 100);
        projectileRMBPool = new ObjectPool<Projectile>(CreateProjectileRMB, OnTakeFromPoolProjectileRMB, OnReturnedToPoolProjectileRMB, null, true, 100);
        damagePopupPool = new ObjectPool<DamagePopup>(CreateDamagePopup, OnTakeFromPoolDamagePopup, OnReturnedToPoolDamagePopup, null, true, 100);
    }
    private void Start()
    {
        caster = ThirdPersonController.Instance;
    }
    Projectile CreateProjectileLMB()
    {
        Projectile projectileLMB = Instantiate(projectileLMBPrefab, transform.position, transform.rotation, projectileLMBHolder);
        projectileLMB.projectilePool =projectileLMBPool;
        projectileLMB.gameObject.SetActive(false);
        return projectileLMB;
    }
    void OnTakeFromPoolProjectileLMB(Projectile projectileLMB)
    {
        projectileLMB.transform.position = caster.abilitySpawnPoint.position;
        projectileLMB.spawnPoint = projectileLMB.transform.position;
        projectileLMB.transform.rotation = Quaternion.LookRotation(caster.transform.forward);
        projectileLMB.travelDirection = caster.transform.forward;
        Physics.IgnoreCollision(caster.playerCollider, projectileLMB.GetComponent<Collider>(), true);
        projectileLMB.gameObject.SetActive(true);
    }
    void OnReturnedToPoolProjectileLMB(Projectile projectileLMB)
    {
        projectileLMB.gameObject.SetActive(false);
    }
    Projectile CreateProjectileRMB()
    {
        Projectile projectileRMB = Instantiate(projectileRMBPrefab, transform.position, transform.rotation, projectileRMBHolder);
        projectileRMB.projectilePool = projectileRMBPool;
        projectileRMB.gameObject.SetActive(false);
        return projectileRMB;
    }
    void OnTakeFromPoolProjectileRMB(Projectile projectileRMB)
    {
        projectileRMB.transform.position = caster.abilitySpawnPoint.position;
        projectileRMB.spawnPoint = projectileRMB.transform.position;
        projectileRMB.transform.rotation = Quaternion.LookRotation(caster.transform.forward);
        projectileRMB.travelDirection = caster.transform.forward;
        Physics.IgnoreCollision(caster.playerCollider, projectileRMB.GetComponent<Collider>(), true);
        projectileRMB.gameObject.SetActive(true);
    }
    void OnReturnedToPoolProjectileRMB(Projectile projectileRMB)
    {
        projectileRMB.gameObject.SetActive(false);
    }
    DamagePopup CreateDamagePopup()
    {
        DamagePopup damagePopup = Instantiate(damagePopupPrefab, transform.position, transform.rotation, damagePopupHolder);
        damagePopup.gameObject.SetActive(false);
        return damagePopup;
    }
    void OnTakeFromPoolDamagePopup(DamagePopup damagePopup)
    {
        damagePopup.gameObject.SetActive(true);
    }
    void OnReturnedToPoolDamagePopup(DamagePopup damagePopup)
    {
        damagePopup.gameObject.SetActive(false);
    }
}
