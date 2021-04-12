using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonMech : MonoBehaviour
{
    public Sounds sounds;
    private bool no_canon;
    private bool show_no_canon;
    private int canon_timer;

    public GameObject p_glow;

    public GameObject projectile;
    private Rigidbody proj_rb;
    public GameObject proj_start;
    public GameObject associated_player;
    private PlayerOnTick player;
    public GameObject UI_obj;
    private UI UI;
    private Camera canon_cam;
    private Transform canon_trans;

    public string seg_break;
    public GameObject opp_wall_obj;
    private WallMech opp_wall;
    private RaycastHit hit;
    private bool wall_hit;

    private float hori;
    private float vert;

    private float yaw;
    private float pitch;

    public bool activated;
    public float sensitivity;
    private bool fired;
    private float fire_var;

    private Vector3 offset;

    public float cooldown; // 75
    private float timer;

    private Vector3 orig;

    // Start is called before the first frame update
    void Start()
    {
        canon_timer = 0;
        no_canon = false;
        show_no_canon = false;

        orig = transform.eulerAngles;
        activated = false;
        proj_rb = projectile.GetComponent<Rigidbody>();
        player = associated_player.GetComponent<PlayerOnTick>();

        UI = UI_obj.GetComponent<UI>();
        opp_wall = opp_wall_obj.GetComponent<WallMech>();

        wall_hit = false;

        canon_cam = GetComponentInChildren<Camera>(true);
        canon_trans = canon_cam.gameObject.transform;

        fired = false;

        timer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (player.pl_num == 1) {
            hori = Input.GetAxis("1hor");
            vert = Input.GetAxis("1ver");
            fire_var = Input.GetAxis("1canon");
        } else {
            hori = Input.GetAxis("2hor");
            vert = -Input.GetAxis("2ver");
            fire_var = Input.GetAxis("2canon");
        }

        if (show_no_canon) {
            canon_timer++;
            if (canon_timer > 10) {
                canon_timer = 0;
                p_glow.SetActive(false);
            }
        }

        if (activated) {
            float distance = 800;
            int layerMask = 1 << 23;

            Debug.DrawRay(canon_trans.position, canon_trans.forward * distance, Color.red);

            if (hori > 0.2f) {
                transform.Rotate(new Vector3(0, (hori - 0.2f) * sensitivity, 0));
            }
            if (hori < -0.2f) {
                transform.Rotate(new Vector3(0, (hori + 0.2f) * sensitivity, 0));
            }
            if (vert > 0.2f) {
                transform.Rotate(new Vector3((vert - 0.2f) * sensitivity, 0, 0));
            }
            if (vert < -0.2f) {
                transform.Rotate(new Vector3((vert + 0.2f) * sensitivity, 0, 0));
            }

            if (!fired && fire_var > 0) {
                fired = true;
                if (player.trash_count > 0) {
                    no_canon = false;
                    if (Physics.Raycast(canon_trans.position, canon_trans.forward, out hit, distance, layerMask)) {
                        Debug.Log(hit.transform.gameObject.name);
                        wall_hit = true;
                    }
                    proj_rb.AddForce(canon_trans.forward * 1200, ForceMode.Impulse);
                    player.trash_count--;
                    UI.set_trash(player.pl_num, player.trash_count);
                    sounds.PlaySound("canon");
                }
                else {
                    show_no_canon = true;
                    p_glow.SetActive(true);
                    no_canon = true;
                    sounds.PlaySound("miss");
                }
            }

        } else {
            transform.eulerAngles = orig;
        }
        if (fired) {
            timer++;
            if (timer > cooldown)
            {
                if (wall_hit) {
                    opp_wall.hit_segment(hit.transform.gameObject.name);
                    wall_hit = false;
                    sounds.PlaySound("wall");
                } else if (!no_canon) {
                    sounds.PlaySound("miss");
                }
                fired = false;
                timer = 0;
                proj_rb.useGravity = false;
                proj_rb.velocity *= 0;
                projectile.transform.position = proj_start.transform.position;

            }
        }

    }

    public void FireCanon(string loc)
    {
        if (!fired)
        {
            fired = true;
        }
    }
}
