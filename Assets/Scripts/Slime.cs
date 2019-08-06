using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slime : MonoBehaviour
{

    // public float respawnDelay = 5f;
    // float timeAlive = 0;
    public float speed = 0.01f;
    public int scoreValue = 100;

    [HideInInspector]
    public DashOnClick dashOnClick;
    [HideInInspector]
    public Spawner spawner;

    public GameObject deathParticle;
    public GameObject floatingTextPrefab;

    [HideInInspector]
    public Transform ftCanvas;
    [HideInInspector]
    public Transform particleGroup;

    // public AudioSource audioSource;

    //public SpriteRenderer spriteRenderer;
    //public Animator animator;
    //public PolygonCollider2D polygonCollider2D;
    //public Rigidbody2D rigidbody2D;

    Vector3 dest;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name != "Player")
        {
            return;
        }


        //spriteRenderer.enabled = false;
        //animator.enabled = false;
        //polygonCollider2D.enabled = false;
        //rigidbody2D.isKinematic = false;

        dashOnClick.enemiesKilled += 1;
        Score.addScore(scoreValue);
        GameObject ft = Instantiate(floatingTextPrefab, transform.position, transform.rotation, ftCanvas);
        ft.GetComponent<TextMeshProUGUI>().text = "+" + scoreValue;
        Instantiate(deathParticle, transform.position, transform.rotation, particleGroup);

        //audioSource.PlayOneShot(audioSource.clip);
        //Destroy(gameObject, audioSource.clip.length);
        Destroy(gameObject);
    }

    private void Start()
    {
        dest = spawner.randomPointInBox();
        // audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //timeAlive += Time.deltaTime;
        //if (timeAlive >= respawnDelay)
        //{
        //    // Immediately respawn this unit
        //    spawner.instantSpawnCount += 1;
        //    Destroy(gameObject);
        //}
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, dest, speed);

        if ((dest - transform.position).magnitude < 1)
        {
            dest = spawner.randomPointInBox();
        }
    }
}
