using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float bell = 100f;
    public float bulletSpeed = 8f;
    public float hitPerSecond = 0.6f;


    public GameObject bullet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletControl bulletHit = collision.gameObject.GetComponent<BulletControl>();
        if (bulletHit)
        {
            bulletHit.DestroyWhenHit();
            bell -= bulletHit.Harm();
            if(bell <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float probability = Time.deltaTime * hitPerSecond;
        if (Random.value < probability)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -0.8f, 0);
        GameObject enemyBullet = Instantiate(bullet, startPosition, Quaternion.identity) as GameObject;
        enemyBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeed);
    }
}
