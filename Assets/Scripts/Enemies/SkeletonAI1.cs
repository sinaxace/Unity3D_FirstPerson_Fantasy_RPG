using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // will import NavMesh for referencing

public class SkeletonAI1 : MonoBehaviour
{
    private NavMeshAgent nav;
    public GameObject player;
    public Animator anim;

    public float minDamage;
    public float maxDamage;

    private float updateTime = 0;

    private bool isAttacking = false;

    private PlayerData data;

    public void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        data = FindObjectOfType<PlayerData>();
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
            //anim.SetBool("Attack", true);
            if (!isAttacking)
                StartCoroutine(Attack());

        } else
        {
            // walk
            anim.SetBool("Attack", false);
            isAttacking = false;
        }
    }

    // This coroutine is for updating the PlayerData's currentDamage member by calling TakeDamage once the player is within proximity of the skeleton.
    IEnumerator Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            anim.SetBool("Attack", true);
            yield return new WaitForSeconds(1.2f);
            // after 3 seconds
            data.TakeDamage(Random.Range(minDamage, maxDamage)); // the return for take damage will be anywhere between the min and max damage.
            isAttacking = false;
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
