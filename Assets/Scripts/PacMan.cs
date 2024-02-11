using System.Collections;
using UnityEngine;

public class PacMan : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform target;
    Vector3 posInicialCam, rotInicialCam;

    public float velocidade = 5f;
    Vector3 direcao;

    bool blockCoroutine;
    public float pontuacao = 0f;

    private void Start()
    {
        posInicialCam = cam.gameObject.transform.localPosition;
        rotInicialCam = cam.gameObject.transform.eulerAngles;
    }

    private void Update()
    {
        Move();
        LookBack();
        cam.transform.LookAt(target);
        pontuacao += Time.deltaTime;
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W)) direcao = Vector3.forward;
        else if (Input.GetKeyDown(KeyCode.S)) StartCoroutine(CoroutineMovimento(Vector3.back.x));
        else if (Input.GetKeyDown(KeyCode.A)) StartCoroutine(CoroutineMovimento(Vector3.left.x));
        else if (Input.GetKeyDown(KeyCode.D)) StartCoroutine(CoroutineMovimento(Vector3.right.x));
        else if (Input.GetKey(KeyCode.Space)) direcao = Vector3.zero;

        transform.Translate(Time.deltaTime * velocidade * direcao);
    }

    private void LookBack()
    {
        if (Input.GetKey(KeyCode.C))
        {
            cam.gameObject.transform.localPosition = new(posInicialCam.x, posInicialCam.y, posInicialCam.z * -1);
            cam.gameObject.transform.eulerAngles = new(rotInicialCam.x, 180f, rotInicialCam.z);
        }
        else
        {
            cam.gameObject.transform.localPosition = posInicialCam;
            cam.gameObject.transform.eulerAngles = rotInicialCam;
        }
    }

    private IEnumerator CoroutineMovimento(float direction)
    {
        if (!blockCoroutine)
        {
            blockCoroutine = true;

            direcao = Vector3.zero;

            if (direction != 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    transform.Rotate(new(0f, 4.5f * direction, 0f));
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    transform.Rotate(new(0f, 9f, 0f));
                    yield return new WaitForSeconds(0.01f);
                }
            }

            direcao = Vector3.forward;

            blockCoroutine = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("parede")) direcao = Vector3.zero;
    }
}