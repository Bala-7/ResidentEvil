using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private NavMeshAgent nma;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        nma.SetDestination(player.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
