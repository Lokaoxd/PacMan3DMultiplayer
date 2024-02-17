using System.Collections;

using UnityEngine;
using UnityEngine.AI;

public class Blinky : MonoBehaviour
{
    [SerializeField] Transform targetInicial;
    NavMeshAgent inimigo;
    Transform player;
    Vector3 pointTarget;
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
        pointTarget = targetInicial.position;

        for (int i = 0; i < 20; i++)
        {
            if (transform.position != pointTarget) yield return new WaitForSeconds(0.25f);
            else break;
        }
        
        block = false;
        print("Blinky perseguindo o player");
    }
}