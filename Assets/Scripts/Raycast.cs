using UnityEngine;

public class Raycast : MonoBehaviour
{
    public float laserLength, offset;
    public Vector3 posOfFrente, posOfAtras, posOfDireita, posOfEsquerda;
    public LayerMask layerMask;

    private void FixedUpdate() => Raycasts();

    private void Raycasts()
    {
        Raycast3D(Vector3.forward, "frente");
        Raycast3D(Vector3.back, "atras");
        Raycast3D(Vector3.right, "direita");
        Raycast3D(Vector3.left, "esquerda");
    }

    private void Raycast3D(Vector3 direction, string direcao)
    {
        Vector3 fixedPosition = FixPosition(direcao);

        if (Physics.Raycast(fixedPosition, transform.TransformDirection(direction), out RaycastHit hit, laserLength, layerMask))
        {
            if (hit.collider.CompareTag("parede"))
            {
                switch (direcao)
                {
                    case "frente": posOfFrente = GetPositionOf(hit.collider.transform, direcao); break;
                    case "atras": posOfAtras = GetPositionOf(hit.collider.transform, direcao); break;
                    case "direita": posOfDireita = GetPositionOf(hit.collider.transform, direcao); break;
                    case "esquerda": posOfEsquerda = GetPositionOf(hit.collider.transform, direcao); break;
                }
            }

            Debug.DrawRay(fixedPosition, transform.TransformDirection(direction) * laserLength, Color.green);
        }
        else Debug.DrawRay(fixedPosition, transform.TransformDirection(direction) * laserLength, Color.red);
    }

    private Vector3 FixPosition(string param)
    {
        Vector3 temp = transform.position;

        switch (param)
        {
            case "frente": temp.z += offset; break;
            case "atras": temp.z -= offset; break;
            case "direita": temp.x += offset; break;
            case "esquerda": temp.x -= offset; break;
        }

        return temp;
    }

    private Vector3 GetPositionOf(Transform objeto, string direcao)
    {
        Vector3 position = new();

        switch (direcao)
        {
            case "frente":
            case "atras":
                {
                    position = new(transform.position.x, 0f, objeto.position.z);
                    break;
                }
            case "direita":
            case "esquerda":
                {
                    position = new(objeto.position.x, 0f, transform.position.z);
                    break;
                }
        }

        return position;
    }
}