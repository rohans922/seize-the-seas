using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnTick : MonoBehaviour
{
    public Sounds sounds;
    private bool play_foot;
    private bool prev_foot;

    private Vector3 spawn_pos;
    private Vector3 spawn_angle;
    private Vector3 spawn_cam_angle;

    public bool swim_first_time;
    public int pl_num;
    public GameObject UI_obj;
    public UI UI;
    private RectTransform[] UI_components;
    public GameObject other_player_obj;
    private PlayerOnTick other_player;
    private float x_axis, y_axis, hori, ver, fire, exit, sprint;
    public bool playing;
    public bool swimming;
    private bool prev_state;
    private bool at_canon;
    private bool show_canon;
    private bool exited;
    private bool entered;
    private bool jumpable;
    private bool boosting;
    private float boost_timer;
    public float trash_count;

    public float jump_speed;
    public float walk_default;
    public float sprint_default;
    public float swim_default;
    public float boost_default;
    public float walk_sensitivity;
    public float swim_sensitivity;
    private float land_speed;
    private float water_speed;

    private GameObject canon_cam;
    private Camera canon_cam_cam;
    private ControlAnim anim;
    private CameraMech cam;
    private Camera cam_cam;
    private float default_fov;
    private Transform cam_trans;
    private Transform last_angle;
    private float dir1;
    private float dir2;
    private float dir3;
    private float cam_pitch;
    private float player_pitch;
    private Rigidbody body_rb;
    private Grounded feet;
    private Transform start_pos;

    public GameObject canon_obj;
    private CanonMech canon;
    private Transform[] canon_trans;

    public GameObject Water_Layer;
    private float water_level;

    // Use this for initialization
    void Start()
    {
        play_foot = false;
        prev_foot = play_foot;
        sounds.PlaySound("wind");
        spawn_pos = transform.position + new Vector3(0, 10, 0);
        spawn_angle = transform.eulerAngles;

        last_angle = transform;

        UI = UI_obj.GetComponent<UI>();
        UI_components = UI_obj.GetComponentsInChildren<RectTransform>(true);

        other_player = other_player_obj.GetComponent<PlayerOnTick>();
        swimming = false;
        swim_first_time = false;
        playing = false;
        at_canon = false;
        show_canon = false;
        exited = true;
        entered = false;
        jumpable = true;
        boosting = false;
        trash_count = 0;
        boost_timer = 40;

        land_speed = walk_default;
        water_speed = swim_default;

        cam_cam = GetComponentInChildren<Camera>();
        default_fov = cam_cam.fieldOfView;

        anim = GetComponentInChildren<ControlAnim>();
        cam = GetComponentInChildren<CameraMech>();
        cam_trans = cam.gameObject.transform;
        body_rb = GetComponent<Rigidbody>();
        feet = GetComponentInChildren<Grounded>();
        start_pos = transform;

        canon = canon_obj.GetComponent<CanonMech>();
        canon_trans = canon_obj.GetComponentsInChildren<Transform>(true);

        canon_cam = canon_trans[6].gameObject;
        canon_cam_cam = canon_cam.GetComponent<Camera>();

        spawn_cam_angle = cam_cam.gameObject.transform.eulerAngles;

        water_level = Water_Layer.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (other_player.swimming) {
            if (pl_num == 1) {
                cam_cam.cullingMask |= 1 << LayerMask.NameToLayer("Model-Treading");
                cam_cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Model-Idle"));
                canon_cam_cam.cullingMask |= 1 << LayerMask.NameToLayer("Model-Treading");
                canon_cam_cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Model-Idle"));
            }
            else {
                cam_cam.cullingMask |= 1 << LayerMask.NameToLayer("Model1-Treading");
                cam_cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Model1-Idle"));
                canon_cam_cam.cullingMask |= 1 << LayerMask.NameToLayer("Model1-Treading");
                canon_cam_cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Model1-Idle"));
            }
        }
        else {
            if (pl_num == 1) {
                cam_cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Model-Treading"));
                cam_cam.cullingMask |= 1 << LayerMask.NameToLayer("Model-Idle");
                canon_cam_cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Model-Treading"));
                canon_cam_cam.cullingMask |= 1 << LayerMask.NameToLayer("Model-Idle");
            }
            else {
                cam_cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Model1-Treading"));
                cam_cam.cullingMask |= 1 << LayerMask.NameToLayer("Model1-Idle");
                canon_cam_cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Model1-Treading"));
                canon_cam_cam.cullingMask |= 1 << LayerMask.NameToLayer("Model1-Idle");
            }
        }
        if (show_canon)
        {
            if (fire > 0 && !entered) {
                entered = true;
                exited = false;
                if (pl_num == 1) {
                    UI_components[1].gameObject.SetActive(true);
                } else {
                    UI_components[2].gameObject.SetActive(true);
                }
                sounds.PlaySound("mount");
                last_angle = transform;
                at_canon = true;
                canon.activated = true;
                canon_cam.SetActive(true);
                cam_cam.gameObject.SetActive(false);
                transform.position = canon_trans[1].position;
                transform.eulerAngles = canon_trans[1].eulerAngles;
                body_rb.velocity *= 0;
            }
        }
        if (pl_num == 1)
        {
            x_axis = Input.GetAxis("1x");
            y_axis = Input.GetAxis("1y");
            hori = Input.GetAxis("1hor");
            ver = Input.GetAxis("1ver");
            fire = Input.GetAxis("1fire");
            exit = Input.GetAxis("1exit");
            sprint = Input.GetAxis("1sprint");
        }

        else if (pl_num == 2)
        {
            x_axis = Input.GetAxis("2x");
            y_axis = Input.GetAxis("2y");
            hori = Input.GetAxis("2hor");
            ver = Input.GetAxis("2ver");
            fire = Input.GetAxis("2fire");
            exit = Input.GetAxis("2exit");
            sprint = Input.GetAxis("2sprint");
        }

        prev_state = swimming;
        swimming = transform.position.y < (water_level - 5.5f);
        if (prev_state != swimming)
        {
            prev_state = swimming;
            if (swimming) {
                sounds.StopSound("footsteps");
                sounds.PlaySound("dunk");
                sounds.PlaySound("swim");
                sounds.StopSound("wind");
                if (!swim_first_time) {
                    swim_first_time = true;
                    cam_cam.cullingMask |= 1 << LayerMask.NameToLayer("Titles");
                }
            } else {
                sounds.PlaySound("wind");
                sounds.StopSound("swim");
                //Debug.Log("Switched to other");
                body_rb.velocity = new Vector3(body_rb.velocity.x, 0, body_rb.velocity.z);
            }
        }
        if (playing)
        {

            if (transform.position.y < (water_level - 7f)) {
                cam.render_swim(pl_num);
            } else {
                cam.render_walk(pl_num);
            }

            if (!swimming) {
                anim.Stop_Swimming();
                jumpable = true;
                cam.render_walk(pl_num);
                if (!at_canon) {
                    control_walk();
                    if (play_foot != prev_foot) {
                        Debug.Log(pl_num);
                        prev_foot = play_foot;
                        if (play_foot) {
                            sounds.PlaySound("footsteps");
                        } else {
                            sounds.StopSound("footsteps");
                        }
                    }
                }
                else {
                    jumpable = false;
                    if (exit > 0 && !exited)
                    {
                        exited = true;
                        entered = false;
                        if (pl_num == 1) {
                            cam_cam.cullingMask |= 1 << LayerMask.NameToLayer("CanonMount1");
                            UI_components[1].gameObject.SetActive(false);
                        } else {
                            cam_cam.cullingMask |= 1 << LayerMask.NameToLayer("CanonMount2");
                            UI_components[2].gameObject.SetActive(false);
                        }
                        sounds.PlaySound("dismount");
                        canon_cam.SetActive(false);
                        cam_cam.gameObject.SetActive(true);
                        canon.activated = false;
                        at_canon = false;
                        transform.eulerAngles = last_angle.eulerAngles;
                        transform.position = last_angle.position;
                    }
                }
            } else {
                anim.Start_Swimming();
                jumpable = false;
                control_swim();
            }
        }
    }

    void FixedUpdate()
    {
        if (fire > 0 && jumpable && feet.grounded) {
            feet.grounded = false;
            body_rb.AddForce(new Vector3(0.0f, jump_speed, 0.0f), ForceMode.VelocityChange);
        }
    }


    private void control_walk()
    {
        body_rb.drag = 0;
        Vector3 curr_y = new Vector3(0f, body_rb.velocity.y, 0f);
        Vector3 right = -Vector3.Cross(transform.forward.normalized, Vector3.up.normalized);
        Vector3 hor = right * hori;
        Vector3 vert = transform.forward * ver;
        Vector3 avg = (hor + vert).normalized * land_speed;

        dir1 = 0;
        if (x_axis > 0.07)
            dir1 = x_axis - 0.05f;
        else if (x_axis < -0.07)
            dir1 = x_axis + 0.05f;

        transform.Rotate(0, walk_sensitivity * dir1, 0);
        dir2 = 0;
        if (y_axis > 0.07) {
            dir2 = y_axis - 0.05f;
        }
        else if (y_axis < -0.07) {
            dir2 = y_axis + 0.05f;
        }

        cam_pitch += walk_sensitivity * dir2;
        cam_trans.eulerAngles = new Vector3(Mathf.Clamp(cam_pitch, -60f, 60f), cam_trans.eulerAngles.y, cam_trans.eulerAngles.z);
        if (cam_pitch > 60f)
            cam_pitch = 60f;
        if (cam_pitch < -60f)
            cam_pitch = -60f;

        if (sprint > 0)
            land_speed = sprint_default;
        else
            land_speed = walk_default;
        avg += curr_y;
        body_rb.velocity = avg;
        if (body_rb.velocity.x > 0 || body_rb.velocity.x < 0 || body_rb.velocity.z > 0 || body_rb.velocity.z < 0) {
            play_foot = true;
        }
        else {
            play_foot = false;
        }

    }
    private void control_swim()
    {
        body_rb.drag = 15;

        dir1 = 0;
        if (x_axis > 0.03)
            dir1 = x_axis - 0.03f;
        else if (x_axis < -0.03)
            dir1 = x_axis + 0.03f;
        transform.Rotate(0, swim_sensitivity * dir1, 0);

        dir2 = 0;
        if (y_axis > 0.05)
            dir2 = y_axis - 0.05f;
        else if (y_axis < -0.05)
            dir2 = y_axis + 0.05f;
        cam_pitch += swim_sensitivity * dir2;
        cam_trans.eulerAngles = new Vector3(Mathf.Clamp(cam_pitch, -60f, 60f), cam_trans.eulerAngles.y, cam_trans.eulerAngles.z);
        if (cam_pitch > 60f)
            cam_pitch = 60f;
        if (cam_pitch < -60f)
            cam_pitch = -60f;

        if (sprint > 0)
            water_speed = boost_default;
        else
            water_speed = swim_default;

        dir3 = 0;
        if (ver > 0.05)
            dir3 = ver - 0.05f;
        if (ver < 0.05)
            dir3 = ver + 0.05f;

        body_rb.AddForce(cam_trans.forward * dir3 * water_speed, ForceMode.Impulse);


        if (!boosting && (fire > 0)) {
            boosting = true;
            body_rb.AddForce(cam_trans.forward * dir3 * boost_default, ForceMode.Impulse);
            Debug.Log("boost!");
        }

        if (boosting)
        {
            boost_timer--;
            if (boost_timer < 0) {
                Debug.Log("done!");
                boosting = false;
                boost_timer = 40;
            }
        }
    }

    public void reset_pos()
    {
        transform.position = spawn_pos;
        transform.eulerAngles = spawn_angle;
        cam_trans.eulerAngles = spawn_cam_angle;
        cam_pitch = spawn_cam_angle.x;
        body_rb.velocity *= 0;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Detector") && !at_canon)
        {
            if (pl_num == 1 && other.gameObject.name == "Canon1Detect") {
                cam_cam.cullingMask |= 1 << LayerMask.NameToLayer("CanonMount1");
                show_canon = true;
            }
            if (pl_num == 2 && other.gameObject.name == "Canon2Detect") {
                cam_cam.cullingMask |= 1 << LayerMask.NameToLayer("CanonMount2");
                show_canon = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Detector"))
        {
            if (pl_num == 1 && other.gameObject.name == "Canon1Detect")
            {
                cam_cam.cullingMask &= ~(1 << LayerMask.NameToLayer("CanonMount1"));
                show_canon = false;
            }
            if (pl_num == 2 && other.gameObject.name == "Canon2Detect")
            {
                cam_cam.cullingMask &= ~(1 << LayerMask.NameToLayer("CanonMount2"));
                show_canon = false;
            }
        }
    }
}