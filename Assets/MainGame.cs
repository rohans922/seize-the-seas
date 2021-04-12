using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainGame : MonoBehaviour
{
    public VideoPlayer video;
    public Sounds sounds;
    public Sounds other_sound;

    private int clock;
    public GameObject p1_obj;
    public GameObject p2_obj;
    public GameObject trashy_obj;
    public GameObject wall1_obj;
    public GameObject wall2_obj;
    private PlayerOnTick p1;
    private PlayerOnTick p2;
    private TrashyT trashy;
    private WallMech wall1;
    private WallMech wall2;
    public GameObject canon1_cam;
    public GameObject canon2_cam;
    private int nextUpdate;
    private bool game_over;
    private int end_clock;
    private int p1_swim_clock;
    private int p2_swim_clock;

    public GameObject ready;
    public GameObject three;
    public GameObject two;
    public GameObject one;
    public GameObject go;
    public GameObject p1_walk;
    public GameObject p2_walk;
    public GameObject p1_swim;
    public GameObject p2_swim;
    public GameObject p1_won;
    public GameObject p2_won;

    public GameObject menu;

    private bool end_game;

    // Start is called before the first frame update
    void Start()
    {
        end_game = false;
        nextUpdate = 1;
        end_clock = 0;
        p1_swim_clock = 0;
        p2_swim_clock = 0;
        clock = 0;
        p1 = p1_obj.GetComponent<PlayerOnTick>();
        p2 = p2_obj.GetComponent<PlayerOnTick>();
        trashy = trashy_obj.GetComponent<TrashyT>();
        wall1 = wall1_obj.GetComponent<WallMech>();
        wall2 = wall2_obj.GetComponent<WallMech>();
        game_over = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextUpdate)
        {
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            UpdateEverySecond();
        }
        if (!game_over)
        {
            if (wall1.broken)
            {
                p2_won.SetActive(true);
                game_over = true;
                end_game_tasks();
            }
            else if (wall2.broken)
            {
                p1_won.SetActive(true);
                game_over = true;
                end_game_tasks();
            }
        }
        if (end_game) {
            if (!(video.isPlaying)) {
                SceneManager.LoadScene("ExitScreen");
            }
        }

    }

    void end_game_tasks() {
        sounds.PlaySound("win");
        sounds.StopSound("footsteps");
        other_sound.StopSound("footsteps");
        sounds.StopSound("swim");
        other_sound.StopSound("swim");
        sounds.PlaySound("wind");
        other_sound.PlaySound("wind");
        canon1_cam.SetActive(false);
        canon2_cam.SetActive(false);
        p1.reset_pos();
        p2.reset_pos();
        p1.playing = false;
        p2.playing = false;
    }

    void UpdateEverySecond()
    {
        clock++;
        if (clock > 0 && clock < 5) {
            if (clock < 2)  {
                sounds.PlaySound("ready");
            }
            ready.SetActive(true);
        }
        if (clock > 4 && clock < 6) { 
            sounds.PlaySound("count");
            ready.SetActive(false);
            three.SetActive(true);
        }
        if (clock > 5 && clock < 7) {
            sounds.PlaySound("count");
            three.SetActive(false);
            two.SetActive(true);
        }
        if (clock > 6 && clock < 8) {
            sounds.PlaySound("count");
            two.SetActive(false);
            one.SetActive(true);
        }
        if (clock > 7 && clock < 9) { 
            sounds.PlaySound("go");
            one.SetActive(false);
            go.SetActive(true);
            p1.playing = true;
            p2.playing = true;
            trashy.rb.useGravity = true;
        }
        if (clock > 10 && clock < 14) {
            go.SetActive(false);
            if (clock < 12) {
                p1_walk.SetActive(true);
                p2_walk.SetActive(true);
            }
        }
        else if (clock > 13 && clock < 19)
        {
            p1_walk.SetActive(false);
            p2_walk.SetActive(false);
        }
        if (p1.swim_first_time) {
            p1_swim_clock++;
        }
        if (p2.swim_first_time) {
            p2_swim_clock++;
        }
        if (p1_swim_clock > 0 && p1_swim_clock < 2)  {
            p1_walk.SetActive(false);
            p1_swim.SetActive(true);
        }
        else if (p1_swim_clock > 3 && p1_swim_clock < 5)
        {
            p1_swim.SetActive(false);
        }
        if (p2_swim_clock > 0 && p2_swim_clock < 2)
        {
            p2_walk.SetActive(false);
            p2_swim.SetActive(true);
        }
        else if (p2_swim_clock > 3 && p2_swim_clock < 5)
        {
            p2_swim.SetActive(false);
        }

        if (game_over) {
            end_clock++;
        }
        if (end_clock > 4 && end_clock < 6) {
            video.gameObject.SetActive(true);
            video.Play();
            end_game = true;
            sounds.StopAll();
            other_sound.StopAll();
            menu.SetActive(false);
        }
    }
}
