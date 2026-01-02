using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float fireRate = 2f;        // colpi al secondo
    [SerializeField] float fireRange = 10f;      // raggio massimo
    [SerializeField] Bullet bulletPrefab;        // PREFAB del bullet
    [SerializeField] float bulletSpeed = 10f;    // velocità del proiettile
    [SerializeField] int bulletDamage = 1;

    float nextFireTime;

    void Update()
    {
        ShootNearestEnemy();
    }

    void ShootNearestEnemy()
    {
        // rispetta il fire rate
        if (Time.time < nextFireTime)
            return;

        GameObject target = FindNearestEnemy();
        if (target == null)
            return;

        nextFireTime = Time.time + 1f / fireRate;

        Vector2 myPos = transform.position;
        Vector2 targetPos = target.transform.position;
        Vector2 dir = (targetPos - myPos).normalized;

        // piccolo offset per non spawnare DENTRO il player
        Vector2 spawnPos = myPos + dir * 0.3f;

        Bullet b = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        b.SetDamage(bulletDamage);
        b.Init(dir, bulletSpeed);

        // ✅ sorting del bullet in base al livello del player
        var ownerGroup = GetComponentInParent<UnityEngine.Rendering.SortingGroup>();
        var bulletSR = b.GetComponent<SpriteRenderer>();

        if (ownerGroup != null && bulletSR != null)
        {
            bulletSR.sortingLayerID = ownerGroup.sortingLayerID;
            bulletSR.sortingOrder = ownerGroup.sortingOrder + 1; // davanti al player
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject nearest = null;
        float minDistSq = fireRange * fireRange;


        Vector2 myPos = transform.position;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null || !enemy.activeInHierarchy)
                continue;

            float distSq = ((Vector2)enemy.transform.position - myPos).sqrMagnitude;

            if (distSq < minDistSq)
            {
                minDistSq = distSq;
                nearest = enemy;
            }
        }

        return nearest;
    }
}
