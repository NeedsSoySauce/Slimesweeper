using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DashOnClick : MonoBehaviour
{

    public float speed = 1f;

    bool isDashing = false;
    [HideInInspector]
    public int enemiesKilled = 0;
    int singleKillStreak = 1;
    Vector3 dest;
    Spawner spawner;
    public GameObject floatingTextPrefab;
    Transform ftCanvas;

    public LayerMask wallLayers;

    AudioSource audioSource;
    public AudioClip singleKillAudioClip;
    public AudioClip multieKillAudioClip;

    private void OnCollisionStay2D(Collision2D collision)
    {
        isDashing = false;
        return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only one unit is spawned at the end of each dash (if anything was killed
        // So killing a bunch of stuff in one go means you have less stuff to kill!

        // Also, if only one enemy is killed, two enemies are spawned!
        int respawnCount = 0;
        if (enemiesKilled == 1)
        {
            singleKillStreak += 2;
            respawnCount = singleKillStreak;
            audioSource.PlayOneShot(singleKillAudioClip);
        }
        else if (enemiesKilled > 0)
        {
            respawnCount = 1;
            singleKillStreak = 0;
            audioSource.PlayOneShot(multieKillAudioClip);
            // GameObject ft = Instantiate(floatingTextPrefab, transform.position, transform.rotation, ftCanvas);
            //TextMeshProUGUI text = ft.GetComponent<TextMeshProUGUI>();
            //text.text = "x" + enemiesKilled;
            //ft.transform.localScale *= enemiesKilled;
        }


        spawner.instantSpawnCount += respawnCount > 64 ? 64 : respawnCount;

        isDashing = false;
        enemiesKilled = 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spawner = GameObject.FindObjectOfType<Spawner>();
        ftCanvas = GameObject.FindGameObjectWithTag("ftCanvas").transform;
    }

    private void FixedUpdate()
    {
        if (!isDashing && Input.GetMouseButton(0))
        {
            dest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dest.z = transform.position.z;
            if ((dest - transform.position).magnitude > 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dest - transform.position, 1000f, wallLayers);
                dest = hit.point;
                isDashing = true;
            }
        }

        if (isDashing)
        {
            transform.position = Vector2.MoveTowards(transform.position, dest, speed);
        }
    }
}
