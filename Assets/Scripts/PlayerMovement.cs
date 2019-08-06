using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 0.1f;


    void Start()
    {
    }

    void FixedUpdate()
    {
        /*
         transform.Translate(Vector2.right * Input.GetAxisRaw("Horizontal") * speed, Space.World);
         transform.Translate(Vector2.up * Input.GetAxisRaw("Vertical") * speed, Space.World);
         */
    }

    void LateUpdate()
    {
        if (GameManager.isGamePaused)
        {
            return;
        }

        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos -= transform.position;
        float angle = Mathf.Atan2(targetPos.x, targetPos.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = rotation;
    }

    private void OnDrawGizmosSelected()
    {
        // Get mouse direction
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = transform.position.z;

        Gizmos.DrawLine(transform.position, targetPos);
    }
}
