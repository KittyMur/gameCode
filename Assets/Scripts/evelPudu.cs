using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class evelPudu : MonoBehaviour
{
    public float rad_play = 10;
    public float rad_atac = 10;
    public LayerMask player;
    public Transform[] waypoint;
    int ind = 0;

    NavMeshAgent agent;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoint[ind].position);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        anim.SetInteger("state", 0);
        FindObjectOfType<AudioManager>().Pause("pudu");

        if (Vector3.Distance(transform.position, waypoint[ind].position) < 1.5f)
        {
            ind++;
            if (ind >= waypoint.Length) ind = 0;
            agent.SetDestination(waypoint[ind].position);
        }

        Collider[] cols = Physics.OverlapSphere(transform.position, rad_play, player);

        if (agent.velocity.magnitude > 0.1f ) anim.SetInteger("state", 1);

        if (cols.Length > 0)
        {
            if (Vector3.Distance(transform.position, cols[0].transform.position) <= rad_atac)
            {
                agent.SetDestination(transform.position);
                anim.SetInteger("state", 2);
                FindObjectOfType<AudioManager>().Play("pudu");
            }
            else
            {
                agent.SetDestination(cols[0].transform.position);
            }
        }
        else
            agent.SetDestination(waypoint[ind].position);
    }

    public void attack()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, rad_atac, player);

        if (cols.Length > 0)
        {
            Move c = cols[0].transform.GetComponent<Move>();
            if (c != null) c.damage();
        }
    }
}
