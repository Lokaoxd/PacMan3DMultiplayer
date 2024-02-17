using System.Collections;

using UnityEngine;
using UnityEngine.AI;

public class Clyde : MonoBehaviour
{
    [SerializeField] MeshRenderer mapa;

    NavMeshAgent inimigo;

    Vector3 pointTarget;
    Transform player;

    bool blockCoroutine;
    float timer = 0f;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        inimigo = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia < 8f)
        {
            if (!blockCoroutine) StartCoroutine(Passear());
        }
        else if ((Time.time - timer) > 15f)
        {
            pointTarget = player.position;
            timer = Time.time;
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

        yield return new WaitForSeconds(Random.Range(5f, 12.5f));
        blockCoroutine = false;
    }
}