using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMechanics : MonoBehaviour
{
    public GameObject path;
    private GameObject player_obj;
    private Transform[] path_points;
    private Vector3 start;
    private Vector3 end;
    private float speed;
    public bool picked;
    private Vector3 offset;
    private Vector3 delta_scale;
    private Vector3 orig_delta_scale;
    private Vector3 def_scale;
    private Vector3 def_angle;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;


    // Start is called before the first frame update
    void Start()
    {
        def_scale = transform.localScale;
        def_angle = transform.eulerAngles;
        offset = new Vector3(0f, 1f, 0f);
        delta_scale = new Vector3(0.1f, 0.1f, 0.1f);
        orig_delta_scale = delta_scale;
        picked = false;
        speed = 7;
        path_points = path.GetComponentsInChildren<Transform>();
        randomize_path(true);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * 3 * Time.deltaTime; // calculate distance to move
        if (picked) {
            transform.position = Vector3.MoveTowards(transform.position, player_obj.transform.position, step * 2);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 0.8f, 0);
            transform.localScale = transform.localScale *= 0.4f;
            if (Vector3.Distance(transform.position, player_obj.transform.position + offset) < 5.5f)
            {
                picked = false;
                delta_scale = orig_delta_scale;
                transform.eulerAngles = def_angle;
                transform.localScale = def_scale;
                randomize_path(true);
            }
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, end, step);
            if (Vector3.Distance(transform.position, end) < 5f) {
                randomize_path(false);
            }
        }
    }

    public void randomize_path(bool change_start)
    {
        int length = path_points.Length;
        int num1 = Random.Range(1, 12);
        int num2;
        do {
            num2 = Random.Range(1, 12);
        } while (num2 == num1);
        if (change_start)
            start = path_points[num1].gameObject.transform.position;
        else
            start = end;
        end = path_points[num2].gameObject.transform.position;
        transform.position = start;
        speed = Random.Range(7, 9);
    }

    public void picked_up(PlayerOnTick player)
    {
        picked = true;
        player_obj = player.gameObject;

    }
}
