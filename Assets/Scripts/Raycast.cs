using UnityEngine;

public class Raycast : MonoBehaviour
{
    public float laserLength, offset;
    public Vector3 posOf;
    public LayerMask layerMask;

    private void FixedUpdate() => Raycast3D(Vector3.forward);

    private void Raycast3D(Vector3 direction)
    {
        Vector3 fixedPosition = FixPosition();

        if (Physics.Raycast(fixedPosition, transform.TransformDirection(direction), out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.CompareTag("parede")) posOf = GetPositionOf(direction, hit.distance);

            Debug.DrawRay(fixedPosition, transform.TransformDirection(direction) * hit.distance, Color.green);
        }
        else Debug.DrawRay(fixedPosition, transform.TransformDirection(direction) * laserLength, Color.red);
    }

    private Vector3 FixPosition()
    {
        Vector3 temp = transform.position;

        float rotacaoY = transform.eulerAngles.y;

        switch (Mathf.Round(rotacaoY))
        {
            case 0f: temp.z += offset; break;
            case 90f: temp.x += offset; break;
            case 180f: temp.z -= offset; break;
            case 270f: temp.x -= offset; break;
        }

        return temp;
    }

    private Vector3 GetPositionOf(Vector3 direcao, float distancia)
    {
        Vector3 position = new();
        float offset = 0.5f;

        if (direcao == Vector3.forward || direcao == Vector3.back)
        {
            float rotacaoY = transform.eulerAngles.y;

            switch (Mathf.Round(rotacaoY))
            {
                case 0f: position = new(transform.position.x, 0f, transform.position.z + distancia - offset); break;
                case 90f: position = new(transform.position.x + distancia - offset, 0f, transform.position.z); break;
                case 180f: position = new(transform.position.x, 0f, transform.position.z - distancia + offset); break;
                case 270f: position = new(transform.position.x - distancia + offset, 0f, transform.position.z); break;
            }
        }

        return position;
    }
}