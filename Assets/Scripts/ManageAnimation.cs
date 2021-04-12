using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAnimation : MonoBehaviour
{
    private bool curr_state;

    public Animator[] animators;
    // Start is called before the first frame update
    void Start()
    {
        curr_state = false;
        animators = GetComponentsInChildren<Animator>(true);
        //animators[1].gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void SetAnimation(bool underwater)
    {
        if (!curr_state && underwater)
        {
            curr_state = true;
            //animators[0].gameObject.SetActive(false);
            //animators[1].gameObject.SetActive(true);
            animators[1].SetBool("Swimming", true);

        }
        else if (curr_state && !underwater)
        {   
            curr_state = false;
            //animators[0].gameObject.SetActive(true);
            //animators[1].gameObject.SetActive(false);
            animators[0].SetBool("Swimming", false);
        }
    }
}
