using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header ("Attributes")]
        [SerializeField]
        Transform Target;

        [SerializeField]
        float range = 5f;

        [SerializeField]
        Transform partTorotate;

        [SerializeField]
        GameObject bullet;

        [SerializeField]
        Transform firePoint;

    float turnSpeed = 10f;
    float fireRate = 1f;
    float fireCountdown = 0f;
    GameObject currenttarget;


    // Start is called before the first frame update
    void Start()
    {
        currenttarget = null;
        InvokeRepeating("UpdateTarget",0f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
            return;
        
        float distance = Vector3.Distance(transform.position, Target.transform.position);

        if (distance < range)
        {
            Vector3 dir = Target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partTorotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partTorotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);


            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        float shortestDitance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if(distance < shortestDitance)
            {
                shortestDitance = distance;
                nearestEnemy = enemy;
                currenttarget = enemy;
            }
        }

        if(nearestEnemy != null && shortestDitance <= range)
        {
            Target = nearestEnemy.transform;

        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bullet, firePoint.position, firePoint.rotation);
        Bullet BULLET = bulletGO.GetComponent<Bullet>();

        if (BULLET != null)
            BULLET.seek(currenttarget);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
