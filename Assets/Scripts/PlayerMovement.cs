using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask mask = -1;
    public Camera mainCamera { get; private set; }
    private uint tap = 0;
    private float lastTapTime;
    private bool singleTap = true;
    private Vector3 dir;

    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        dir = transform.position;
        StayOnGround();
    }
    void Update()
    {
        if (Input.touchCount < 1)
        {
            singleTap = true;
            return;
        }
        tap = singleTap ? GetTaps() : tap;

        singleTap = false;

        if (tap == 2)
        {
            //TODO:               
            return;
        }

        else if (tap == 1)
        {
            RaycastHit hit;
            // note that the ray starts at 100 units
            Ray ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                if (hit.collider.tag == "Ground")
                {
                    // this is where the gameobject is actually put on the ground
                    Vector3 dir = Vector3.zero;
                    dir.z = 0.1f;
                    transform.Translate(dir * SPEED * Time.deltaTime);
                    transform.position = new Vector3(transform.position.x, hit.point.y + RADIUS_HEIGHT, transform.position.z);
                }
            }
        }
    }

    void StayOnGround()
    {
        RaycastHit hit;
        // note that the ray starts at 100 units
        Ray ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            if (hit.collider.name == "Terrain")
            {
                // this is where the gameobject is actually put on the ground
                Vector3 dir = Vector3.zero;
                dir.z = 0.1f;
                transform.Translate(dir * SPEED * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, hit.point.y + RADIUS_HEIGHT, transform.position.z);

            }
        }
    }


    public uint GetTaps()
    {
        float sinceLastTap = Time.time - lastTapTime;
        uint tap = 0;
        if (sinceLastTap <= DOUBLE_TAP_TIME)
        {
            tap = 2;
        }
        else
        {
            tap = 1;
        }
        lastTapTime = Time.time;
        return tap;
    }
    private const float SPEED = 50.0f;
    private const float DOUBLE_TAP_TIME = 0.2f;
    private const float RADIUS_HEIGHT = 1.5f;
}