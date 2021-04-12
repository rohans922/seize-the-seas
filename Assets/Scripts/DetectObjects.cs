using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjects : MonoBehaviour
{
    private PlayerOnTick player;
    public Sounds sounds; 

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerOnTick>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Trash") && player.gameObject.transform.position.y <= 93.5)
        {
            TrashMechanics mechanics = other.gameObject.GetComponent<TrashMechanics>();
            if (!mechanics.picked) {
                sounds.PlaySound("pick");
                player.trash_count++;
                mechanics.picked_up(player);
                Debug.Log("Player " + player.pl_num + " now has " + player.trash_count + " trash");
                player.UI.set_trash(player.pl_num, player.trash_count);
            }
        }
        if (other.gameObject.CompareTag("TrashyT"))
        {
            sounds.PlaySound("trashy");
            other.gameObject.GetComponent<TrashyT>().player_caught();
            player.UI.set_caught(player.pl_num);
            player.gameObject.transform.position += new Vector3(0, 100, 0);
            player.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 20, 0), ForceMode.Impulse);
        }
    }
}
