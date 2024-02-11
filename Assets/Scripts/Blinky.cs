using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Blinky : MonoBehaviour
{
    NavMeshAgent inimigo;
    Transform player;
    Vector3 pointTarget = new(13.76f, 1.048f, 13.66f);
    bool block = true;

    private void Awake()
    {
        inimigo = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    private void Start() => StartCoroutine(Coroutine());

    private void Update()
    {
        if (!block) pointTarget = player.position;

        var temp = pointTarget;
        temp.y = transform.position.y;
        inimigo.SetDestination(temp);
    }

    private IEnumerator Coroutine()
    {
        for (int i = 0; i < 20; i++)
        {
            if (transform.position != pointTarget) yield return new WaitForSeconds(0.25f);
            else break;
        }
        
        block = false;
        print("Blinky perseguindo o player");
    }
}