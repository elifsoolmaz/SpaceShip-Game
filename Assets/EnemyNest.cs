using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNest : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float width, height;
    private bool toRight = true;
    private float speed = 10f;
    private float xMax, xMin;
    private float delayTime = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        float distanceOfCameraZAndObject = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftCamera = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceOfCameraZAndObject));
        Vector3 rightCamera = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceOfCameraZAndObject));
        xMax = rightCamera.x;
        xMin = leftCamera.x;
        CreateEnemyOneByOne();
    }

    void CreateEnemy()
    {
        foreach (Transform child in transform)
        {
            //Prefabı gameobject gibi yarattım.
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    void CreateEnemyOneByOne()
    {
        Transform avaliablePosition = NextAvailablePosition();
        if (avaliablePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, avaliablePosition.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = avaliablePosition;
        }

        if (NextAvailablePosition())
        {
            Invoke("CreateEnemyOneByOne", delayTime);
        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update()
    {
        if (toRight) {
            //  transform.position += new Vector3(speed * Time.deltaTime,0);
            transform.position += speed* Vector3.right * Time.deltaTime;
        }
        else
        {
            transform.position += speed * Vector3.left * Time.deltaTime;

        }

        float rightBorder = transform.position.x + width * 0.5f;
        float leftBorder = transform.position.x - width * 0.5f;

        if(rightBorder> xMax)
        {
            toRight = false;
        }
        else if (leftBorder < xMin)
        {
            toRight = true;
        }

        if (AllEnemiesDead())
        {
            CreateEnemyOneByOne();
        }

    }

    //enemies positions
    Transform NextAvailablePosition()
    {
        foreach (Transform ChildPosition in transform)
        {
            if (ChildPosition.childCount == 0)
            {
                return ChildPosition;
            }
        }
        return null;
    }

    bool AllEnemiesDead()
    {
        foreach(Transform ChildPosition in transform)
        {
            if(ChildPosition.childCount > 0)
            {
                return false;
            }        
        }
        return true;
        
    }



}
