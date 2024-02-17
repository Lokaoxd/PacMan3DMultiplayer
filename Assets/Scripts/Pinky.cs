using UnityEngine;
using UnityEngine.AI;

public class Pinky : MonoBehaviour
{
    NavMeshAgent inimigo;
    [SerializeField] Transform player;

    private void Awake() => inimigo = GetComponent<NavMeshAgent>();


    private void Update() => inimigo.SetDestination(player.GetComponent<Raycast>().posOf);
}