using UnityEngine;
using UnityEngine.UI;

public class MiniMapa : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] RawImage rawImage;
    [SerializeField] Transform target;

    private void Start()
    {
        rawImage.texture = new RenderTexture(Screen.width, Screen.height, 24);
        cam.targetTexture = (RenderTexture)rawImage.texture;
    }

    private void Update() => transform.LookAt(target);
}