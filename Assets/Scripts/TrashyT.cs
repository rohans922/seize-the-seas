using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashyT : MonoBehaviour
{
    public float speed;
    private float def_speed;
    public GameObject player1;
    public GameObject player2;
    public float detect_radius;
    public Rigidbody rb;
    private int after;
    private bool swimming;

    public GameObject path;
    private Transform[] path_points;
    private GameObject end_obj;
    private Vector3 end;

    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        def_speed = speed;
        after = 0;
        rb = GetComponent<Rigidbody>();
        swimming = false;
        path_points = path.GetComponentsInChildren<Transform>();
        randomize_path();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 84) {
            swimming = true;
            rb.drag = 15;
        }
        else {
            swimming = false;
        }
        if (swimming) {
            float p1_y = player1.transform.position.y;
            float p2_y = player2.transform.position.y;
            if (after == 0 && p1_y < 84f && Vector3.Distance(transform.position, player1.transform.position) <= detect_radius) {
                after = 1;
                Debug.Log("in P1 radius");
                speed = def_speed + 6;
            }
            if (after == 0 && p2_y < 84f && Vector3.Distance(transform.position, player2.transform.position) <= detect_radius) {
                after = 2;
                Debug.Log("in P2 radius");
                speed = def_speed + 6;
            }
            if (after == 1) {
                if (p1_y > 84f || Vector3.Distance(transform.position, player1.transform.position) > detect_radius)
                {
                    Debug.Log("left P1 radius");
                    after = 0;
                    randomize_path();
                }
                if (Vector3.Distance(transform.position, player2.transform.position) <= detect_radius)
                {
                    float range = Random.Range(0, 150);
                    if (range >= 10 && range <= 14)
                    {
                        after = 2;
                    }
                }
            }
            if (after == 2) {
                if (p2_y > 84f || Vector3.Distance(transform.position, player2.transform.position) > detect_radius)
                {
                    Debug.Log("left P1 radius");
                    after = 0;
                    randomize_path();
                }
                if (Vector3.Distance(transform.position, player1.transform.position) <= detect_radius)
                {
                    float range = Random.Range(0, 150);
                    if (range >= 10 && range <= 14)
                    {
                        after = 1;
                    }
                }

            }
            float step = speed * Time.deltaTime; // calculate distance to move
            if (after == 1) {
                transform.position = Vector3.MoveTowards(transform.position, player1.transform.position, step);
                transform.LookAt(player1.transform);
            }
            else if (after == 2) {
                transform.position = Vector3.MoveTowards(transform.position, player2.transform.position, step);
                transform.LookAt(player2.transform);
            }
            else if (after == 0)
            {
                float sw = Random.Range(0, 300);
                if (sw >= 10 && sw <= 10) {
                    Debug.Log("randomized!");
                    randomize_path();
                }
                transform.position = Vector3.MoveTowards(transform.position, end, step);
                transform.LookAt(end_obj.transform);
                if (Vector3.Distance(transform.position, end) < 4f) {
                    randomize_path();
                }
            }
        }
    }

    public void player_caught() {
        after = 0;
    }

    private void randomize_path()
    {
        int num1 = Random.Range(1, 12);
        end_obj = path_points[num1].gameObject;
        end = end_obj.transform.position;
    }

}
