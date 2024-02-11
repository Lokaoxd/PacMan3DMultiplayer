using UnityEditor;
using UnityEngine;

public class ColisaoGhosts : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print($"Você foi atingido pelo {gameObject.name}");
            EditorApplication.isPlaying = false;
        }
    }
}