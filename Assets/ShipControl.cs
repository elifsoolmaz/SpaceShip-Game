using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    private float speed=10f;
    public float fineTune = 0.7f;
    float xMin, xMax;
    public GameObject bullet;
    public float bulletSpeed = 1f;
    public float shootRange = 2f;
    public float bell = 400f;


    // Start is called before the first frame update
    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftEdge.x+ fineTune;
        xMax = rightEdge.x- fineTune;


    }
    void Shoot()
    {
        GameObject shipBullet = Instantiate(bullet, transform.position + new Vector3(0,1f,0), Quaternion.identity) as GameObject;
        shipBullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, bulletSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            InvokeRepeating("Shoot", 0.000001f, shootRange);
        }
        if (Input.GetKeyUp(KeyCode.Space)){
            CancelInvoke("Shoot");
        }

        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // transform.position += new Vector3(-speed*Time.deltaTime, 0, 0);
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletControl bulletHit = collision.gameObject.GetComponent<BulletControl>();
        if (bulletHit)
        {
            bulletHit.DestroyWhenHit();
            bell -= bulletHit.Harm();
            if (bell <= 0)
            {
                Destroy(gameObject);
            }
        }
    }


}
