using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnim : MonoBehaviour
{
    public Animator[] animators;
    private bool swimming;

    // Start is called before the first frame update
    void Start()
    {
        animators = GetComponentsInChildren<Animator>(true);
        animators[0].SetBool("Swimming", false);
        swimming = false;
    }

    void Update()
    {

    }

    public void Start_Swimming()
    {
        if (!swimming)
        {
            swimming = true;
            animators[1].SetBool("Swimming", true);
        }
       
    }
    public void Stop_Swimming()
    {
        if (swimming)
        {
            swimming = false;
            animators[1].SetBool("Swimming", false);
        }
    }

}
