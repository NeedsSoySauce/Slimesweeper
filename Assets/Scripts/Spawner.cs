using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float spawnTime = 0.7f;
    public float width = 1f;
    public float height = 1f;
    public int initSpawnCount = 10;

    public float minScaleSize = 0.3f;
    public float maxScaleSize = 1.7f;

    [HideInInspector]
    public int instantSpawnCount = 0;

    float timeCount = 0f;
    DashOnClick dashOnClick;

    Transform player;

    public Transform ftCanvas;
    public Transform particleGroup;

    private void Start()
    {
        instantSpawnCount += initSpawnCount;
        dashOnClick = GameObject.FindObjectOfType<DashOnClick>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ftCanvas = GameObject.FindGameObjectWithTag("ftCanvas").transform;
        particleGroup = GameObject.FindGameObjectWithTag("ParticleGroup").transform;
    }

    // Update is called once per frame
    void Update()
    {

        timeCount += Time.deltaTime;

        if (timeCount >= spawnTime)
        {
            spawnEnemy();
            timeCount = 0;
        }

        while (instantSpawnCount > 0)
        {
            spawnEnemy();
            instantSpawnCount--;
        }

    }

    private void spawnEnemy()
    {
        Vector2 spawnPos = randomPointInBox();

        while (Vector2.Distance(spawnPos, player.position) < 1)
        {
            spawnPos = randomPointInBox();
        }

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity, transform);
        float scaleFactor = Random.Range(minScaleSize, maxScaleSize);
        enemy.transform.localScale *= scaleFactor;

        Slime slime = enemy.GetComponent<Slime>();
        slime.dashOnClick = dashOnClick;
        slime.spawner = this;
        slime.ftCanvas = ftCanvas;
        slime.particleGroup = particleGroup;
        slime.scoreValue = (int)(slime.scoreValue / scaleFactor);

        enemy.SetActive(true);
    }

    public Vector2 randomPointInBox()
    {
        float x = Random.Range(-width / 2, width / 2);
        float y = Random.Range(-height / 2, height / 2);
        return new Vector2(x, y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position + new Vector3(-width / 2, height / 2), transform.position + new Vector3(width / 2, height / 2));
        Gizmos.DrawLine(transform.position + new Vector3(-width / 2, -height / 2), transform.position + new Vector3(width / 2, -height / 2));


        Gizmos.DrawLine(transform.position + new Vector3(-width / 2, height / 2), transform.position + new Vector3(-width / 2, -height / 2));
        Gizmos.DrawLine(transform.position + new Vector3(width / 2, height / 2), transform.position + new Vector3(width / 2, -height / 2));
    }

}
