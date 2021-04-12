using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;

    public GameObject p1_t_obj;
    public GameObject p2_t_obj;

    private Text p1_text;
    private Text p2_text;

    private bool p1_caught;
    private bool p2_caught;
    private bool p1_trash;
    private bool p2_trash;

    private int p1_timer;
    private int p2_timer;
    private int p1_trash_timer;
    private int p2_trash_timer;

    public GameObject p1_timmy;
    public GameObject p2_timmy;
    public GameObject p1_glow;
    public GameObject p2_glow;

    // Start is called before the first frame update
    void Start()
    {
        p1_caught = false;
        p1_timer = 0;
        p2_caught = false;
        p2_timer = 0;
        p1_trash = false;
        p1_trash_timer = 0;
        p2_trash = false;
        p2_trash_timer = 0;
        p1_text = p1_t_obj.GetComponent<Text>();
        p2_text = p2_t_obj.GetComponent<Text>();
        p1_text.text = "0";
        p2_text.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (p1_caught) {
            p1_timer++;
            p1_timmy.SetActive(true);
            if (p1_timer > 25) {
                p1_caught = false;
                p1_timer = 0;
                p1_timmy.SetActive(false);
            }
        }
        if (p2_caught) {
            p2_timer++;
            p2_timmy.SetActive(true);
            if (p2_timer > 25) {
                p2_caught = false;
                p2_timer = 0;
                p2_timmy.SetActive(false);
            }
        }
        if (p1_trash) {
            p1_trash_timer++;
            p1_glow.SetActive(true);
            if (p1_trash_timer > 25) {
                p1_trash = false;
                p1_trash_timer = 0;
                p1_glow.SetActive(false);
            }
        }
        if (p2_trash) {
            p2_trash_timer++;
            p2_glow.SetActive(true);
            if (p2_trash_timer > 25) {
                p2_trash = false;
                p2_trash_timer = 0;
                p2_glow.SetActive(false);
            }
        }
    }

    public void set_trash(int pnum, float count)
    {
        if (pnum == 1)
        {
            p1_trash = true;
            p1_text.text = "" + count;
        }

        else if (pnum == 2)
        {
            p2_trash = true;
            p2_text.text = "" + count;
        }

    }
    public void set_caught(int pnum)
    {
        if (pnum == 1)
            p1_caught = true;
        else if (pnum == 2)
            p2_caught = true;
    }

}
