using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhases : MonoBehaviour
{
    public int numOfBullets;
    public float bulletSpeed;
    public GameObject bullet;


    private Vector3 startPoint;
    private const float radius = 1f;
    private float InstantiationTimer = 2f;
    EnemyHealth bossHealth;

    void Update()
    {
        bossHealth = this.GetComponent<EnemyHealth>();
        if (bossHealth.health >= 800)
        {
            numOfBullets = 3;
            bulletSpeed = 10f;
           
        }
        else if (bossHealth.health < 800 && bossHealth.health >= 600)
        {
            numOfBullets = 4;
            bulletSpeed = 16f;
            
        }
        else if (bossHealth.health < 600 && bossHealth.health >= 400)
        {
            numOfBullets = 8;
            bulletSpeed = 24f;
        }
        else
        {
            numOfBullets = 16;
            bulletSpeed = 36f;
            
        }
        startPoint = this.transform.position;
        SpawnBullet(numOfBullets, bulletSpeed);

    }
    private void SpawnBullet(int _numofBullets, float bulletSpeed)
    {
        InstantiationTimer -= Time.deltaTime;
        if (InstantiationTimer <= 0) { 
        
            float angleStep = 360f / _numofBullets;
            float angle = 0f;
            for (int i = 0; i <= _numofBullets - 1; i++)
            {
                float bulletDirX = startPoint.x + Mathf.Sin((angle * Mathf.PI)/ 180) * radius;
                float bulletDirY = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

                Vector3 bulletVector = new Vector3(bulletDirX, bulletDirY,0);
                Vector3 bulletMoveDirection = (bulletVector - startPoint).normalized * bulletSpeed;

                GameObject tmpObj = Instantiate(bullet, startPoint, Quaternion.identity);
                tmpObj.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletMoveDirection.x, bulletMoveDirection.y);
                angle += angleStep;

            }
            InstantiationTimer = 2f;
        }
    }
    

}
