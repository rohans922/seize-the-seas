using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMech : MonoBehaviour
{
    private Transform[] segments;
    private float[] durability;
    public bool broken;
    private int seg_broken;
    // Start is called before the first frame update
    void Start()
    {
        durability = new float[10];
        broken = false;
        seg_broken = 0;
        segments = GetComponentsInChildren<Transform>(true);
        for (int i = 1; i < 10; i++) {
            durability[i] = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (seg_broken > 8) {
            broken = true;
        }
    }

    public void hit_segment(string loc)
    {
        Debug.Log("RAN");
        switch (loc)
        {
            case "1,1":
                durability[1]--;
                segments[1].localPosition -= new Vector3(0, 1.5f, 0);
                segments[1].localScale = new Vector3(6, 3, 2);
                if (durability[1] < 1 && durability[1] >= 0)
                {
                    segments[1].gameObject.SetActive(false);
                    seg_broken++;
                }
                break;
            case "2,1":
                durability[2]--;
                segments[2].localPosition -= new Vector3(0, 1.5f, 0);
                segments[2].localScale = new Vector3(6, 3, 2);
                if (durability[2] < 1 && durability[2] >= 0)
                {
                    segments[2].gameObject.SetActive(false);
                    seg_broken++;
                }
                break;
            case "3,1":
                durability[3]--;
                segments[3].localPosition -= new Vector3(0, 1.5f, 0);
                segments[3].localScale = new Vector3(6, 3, 2);
                if (durability[3] < 1 && durability[3] >= 0)
                {
                    segments[3].gameObject.SetActive(false);
                    seg_broken++;
                }
                break;
            case "1,2":
                durability[4]--;
                segments[4].localPosition -= new Vector3(0, 1.5f, 0);
                segments[4].localScale = new Vector3(6, 3, 2);
                if (durability[4] < 1 && durability[4] >= 0)
                {
                    segments[4].gameObject.SetActive(false);
                    seg_broken++;
                }
                break;
            case "2,2":
                durability[5]--;
                segments[5].localPosition -= new Vector3(0, 1.5f, 0);
                segments[5].localScale = new Vector3(6, 3, 2);
                if (durability[5] < 1 && durability[5] >= 0)
                {
                    segments[5].gameObject.SetActive(false);
                    seg_broken++;
                }
                break;
            case "3,2":
                durability[6]--;
                segments[6].localPosition -= new Vector3(0, 1.5f, 0);
                segments[6].localScale = new Vector3(6, 3, 2);
                if (durability[6] < 1 && durability[6] >= 0)
                {
                    segments[6].gameObject.SetActive(false);
                    seg_broken++;
                }
                break;
            case "1,3":
                durability[7]--;
                segments[7].localPosition -= new Vector3(0, 1.5f, 0);
                segments[7].localScale = new Vector3(6, 3, 2);
                if (durability[7] < 1 && durability[7] >= 0)
                {
                    segments[7].gameObject.SetActive(false);
                    seg_broken++;
                }
                break;
            case "2,3":
                durability[8]--;
                segments[8].localPosition -= new Vector3(0, 1.5f, 0);
                segments[8].localScale = new Vector3(6, 3, 2);
                if (durability[8] < 1 && durability[8] >= 0)
                {
                    segments[8].gameObject.SetActive(false);
                    seg_broken++;
                }
                break;
            case "3,3":
                durability[9]--;
                segments[9].localPosition -= new Vector3(0, 1.5f, 0);
                segments[9].localScale = new Vector3(6, 3, 2);
                if (durability[9] < 1 && durability[9] >= 0)
                {
                    segments[9].gameObject.SetActive(false);
                    seg_broken++;
                }
                break;
        }
    }
}
