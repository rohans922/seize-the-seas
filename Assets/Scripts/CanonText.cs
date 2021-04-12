using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonText : MonoBehaviour
{

    public GameObject player;
    private Transform player_trans;

    // Start is called before the first frame update
    void Start()
    {
        player_trans = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player_trans);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
