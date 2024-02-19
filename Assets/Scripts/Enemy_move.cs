using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_move : MonoBehaviour
{
    // Start is called before the first frame update
    public bool roaming = true;
    public float moveSpeed;
    public float nextWPDistance;
    public bool updateContinuesPath;
    public Seeker seeker;
    Path path;
    Coroutine moveCoroutine;
    bool reachDestination = false;

    //shoot
    public bool isShootable = false;
    public GameObject bullet;
    public float bulletSpeed;
    public float timeBtwFire;
    private float fireCooldown;

    // Update is called once per frame
    private void Start()
    {   
        InvokeRepeating("CalculatePath", 0f, 0.5f);
        reachDestination = true;
    }

    private void Update()
    {
         fireCooldown -= Time.deltaTime;
        if (fireCooldown < 0)
        {
            fireCooldown = timeBtwFire;
            //shoot
            EnemyFireBullet();
        }
    }
    void EnemyFireBullet()
    {
        var bulletTmp = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        Vector3 direction = playerPos - transform.position;
        rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
    }
    void CalculatePath()
    {
        Vector2 target = FindTarget();
        if (seeker.IsDone() && (reachDestination || updateContinuesPath))      
           seeker.StartPath(transform.position, target, OnPathComplete);  
    }

    void OnPathComplete(Path p)
    {
        if (p.error) return;
        path = p;
        //move to target
        MoveToTarget();
    }
    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }
    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;
        reachDestination = false;
        while (currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if(distance < nextWPDistance) 
                currentWP++;

            yield return null;
        }
        reachDestination = false;
    }

    Vector2 FindTarget()
    {
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        if(roaming = true)
        {
            return (Vector2)playerPos + (Random.Range(10f, 50f) * new Vector2 (Random.Range(-1, 1), Random.Range(-1, 1)).normalized);
        }
        else
        {
            return playerPos;
        }
    }
}
