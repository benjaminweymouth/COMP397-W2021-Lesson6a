using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CryptoState
{
    IDLE, 
    RUN, 
    JUMP
}

public class CryptoBehaviour : MonoBehaviour
{
    [Header("Line of Sight")]
    //public Vector3 playerLocation;
    public GameObject player;
    //public LayerMask collisionLayer;
    private NavMeshAgent agent;
    //public Vector3 LOSoffset = new Vector3(0.0f, 2.0f, -5.0f);
    private Animator animator;
    public bool HasLOS;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit hit;

        //var size = new Vector3(4.0f, 4.0f, 10.0f);
        // HasLOS = Physics.BoxCast(transform.position + LOSoffset, size *0.5f, transform.forward, out hit, transform.rotation, 0.0f, collisionLayer);
        // HasLOS = Physics.BoxCast(transform.position + LOSoffset, size * 0.5f, transform.forward, transform.rotation, 10.0f);
        //HasLOS = Physics.BoxCast(transform.position + LOSoffset, size * 0.5f, transform.forward, out hit, transform.rotation);



        if (HasLOS)
        {
            agent.SetDestination(player.transform.position);
            

            if (Vector3.Distance(transform.position, player.transform.position) < 2.5)
            {
                //could be enemy doing an attack here 
                animator.SetInteger("AnimState", (int)CryptoState.IDLE);
                transform.LookAt(transform.position - player.transform.forward);
            }
            else
            {
                animator.SetInteger("AnimState", (int)CryptoState.RUN);
            }
        }
        else
        {
            animator.SetInteger("AnimState", (int)CryptoState.IDLE);
        }


        

        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    animator.SetInteger("AnimState", (int)CryptoState.IDLE);
        //}

        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    animator.SetInteger("AnimState", (int)CryptoState.RUN);
        //}

        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    animator.SetInteger("AnimState", (int)CryptoState.JUMP);
        //}

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = true;
            player  = other.transform.gameObject;
        }
        Debug.Log(other.gameObject.name);
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = false;
            
        }
    }

     
 
}

