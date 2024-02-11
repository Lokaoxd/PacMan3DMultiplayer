using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inky : MonoBehaviour
{
    [SerializeField] MeshRenderer mapa;

    NavMeshAgent inimigo;

    Vector3 pointTarget;
    Transform player, blinky;
    Transform lastTarget;

    bool blockCoroutine;

    float timer = 0f;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        blinky = GameObject.Find("Blinky").transform;
        inimigo = GetComponent<NavMeshAgent>();
    }

    private void Start() => timer = Time.time;

    private void Update()
    {
        if ((player.GetComponent<PacMan>().pontuacao < 30 && !blockCoroutine) || transform.position == pointTarget) StartCoroutine(Passear());
        else if (player.GetComponent<PacMan>().pontuacao >= 30)
        {
            Vector3 temp;

            if (lastTarget == null || (Time.time - timer) > 10f)
            {
                float chance = Random.Range(0f, 100f);

                if (chance > 50f)
                {
                    temp = player.position;
                    lastTarget = player;
                    print("Inky perseguindo o Player.");
                }
                else
                {
                    temp = blinky.position;
                    lastTarget = blinky;
                    print("Inky perseguindo o Blinky");
                }

                timer = Time.time;
            }
            else temp = lastTarget.position;

            temp.y = transform.position.y;
            pointTarget = temp;
        }

        inimigo.SetDestination(pointTarget);
    }

    private IEnumerator Passear()
    {
        blockCoroutine = true;

        var posicaoCentral = mapa.transform.position;

        float posXRandom = Random.Range(posicaoCentral.x - mapa.bounds.extents.x, posicaoCentral.x + mapa.bounds.extents.x),
              posZRandom = Random.Range(posicaoCentral.z - mapa.bounds.extents.z, posicaoCentral.z + mapa.bounds.extents.z);
        var posicaoAleatoria = new Vector3(posXRandom, transform.position.y, posZRandom);

        pointTarget = posicaoAleatoria;

        yield return new WaitForSeconds(5f);
        blockCoroutine = false;
    }
}