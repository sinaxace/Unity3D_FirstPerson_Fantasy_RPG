using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // will import NavMesh for referencing

public class SkeletonAI1 : MonoBehaviour
{
    private NavMeshAgent nav;
    public GameObject player;
    public Animator anim;

    private float updateTime = 0;

    public void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        //Invoke("FindTarget", 2); // after every 2 seconds, call FindTarget method
    }

    public void Update()
    {
        updateTime += Time.deltaTime; // Time.deltaTime is a way for you to add the time that is flowing atm

        // this will return the distance between the skeleton and player.
        float dist = Vector3.Distance(this.transform.position, player.transform.position);
        if (dist <= 4)
        {
            //Attack when close to player
            anim.SetBool("Attack", true);

        } else
        {
            // walk
            anim.SetBool("Attack", false);
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(player.transform); // to have the skeleton face the player.
        if (updateTime > 2)
        {
            nav.destination = player.transform.position;
            updateTime = 0;
        }
    }
}
