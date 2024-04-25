using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GirarRoleta : MonoBehaviour
{
    public GameObject nivel;
    public GameObject pivot;
    private Vector3 target;
    private Vector3 targetPos;

    public TextMeshProUGUI Status;
    public TextMeshProUGUI status_indice;

    public void Update ()
    {
        string textoValor = Status.text;

        if (int.TryParse(textoValor, out int novoValor))
        {
            switch (novoValor) {
                case 0:
                    target = new Vector3(0, 0, 0);
                    targetPos = new Vector3(0f, 0f, Mathf.LerpAngle(pivot.transform.eulerAngles.z, target.z, 1f * Time.deltaTime));
                    pivot.transform.eulerAngles = targetPos;
                    status_indice.text = "Baixo";
                    break;

                case 1:
                    target = new Vector3(0, 0, -4.6f);
                    targetPos = new Vector3(0f, 0f, Mathf.LerpAngle(pivot.transform.eulerAngles.z, target.z, 1f * Time.deltaTime));
                    pivot.transform.eulerAngles = targetPos;
                    status_indice.text = "Baixo";
                    break;

                case 2:
                    target = new Vector3(0, 0, -18.5f);
                    targetPos = new Vector3(0f, 0f, Mathf.LerpAngle(pivot.transform.eulerAngles.z, target.z, 1f * Time.deltaTime));
                    pivot.transform.eulerAngles = targetPos;
                    status_indice.text = "Baixo";
                    break;

                case 3:
                    target = new Vector3(0, 0, -32.8f);
                    targetPos = new Vector3(0f, 0f, Mathf.LerpAngle(pivot.transform.eulerAngles.z, target.z, 1f * Time.deltaTime));
                    pivot.transform.eulerAngles = targetPos;
                    status_indice.text = "Moderado";
                    break;

                case 4:
                    target = new Vector3(0, 0, -48.9f);
                    targetPos = new Vector3(0f, 0f, Mathf.LerpAngle(pivot.transform.eulerAngles.z, target.z, 1f * Time.deltaTime));
                    pivot.transform.eulerAngles = targetPos;
                    status_indice.text = "Moderado";
                    break;

                case 5:
                    target = new Vector3(0, 0, -64.2f);
                    targetPos = new Vector3(0f, 0f, Mathf.LerpAngle(pivot.transform.eulerAngles.z, target.z, 1f * Time.deltaTime));
                    pivot.transform.eulerAngles = targetPos;
                    status_indice.text = "Moderado";
                    break;

                case 6:
                    target = new Vector3(0, 0, -80.2f);
                    targetPos = new Vector3(0f, 0f, Mathf.LerpAngle(pivot.transform.eulerAngles.z, target.z, 1f * Time.deltaTime));
                    pivot.transform.eulerAngles = targetPos;
                    status_indice.text = "Alto";
                    break;

                case 7:
                    target = new Vector3(0, 0, -98.8f);
                    targetPos = new Vector3(0f, 0f, Mathf.LerpAngle(pivot.transform.eulerAngles.z, target.z, 1f * Time.deltaTime));
                    pivot.transform.eulerAngles = targetPos;
                    status_indice.text = "Alto";
                    break;

                case 8:
                    target = new Vector3(0, 0, -115.7f);
                    targetPos = new Vector3(0f, 0f, Mathf.LerpAngle(pivot.transform.eulerAngles.z, target.z, 1f * Time.deltaTime));
                    pivot.transform.eulerAngles = targetPos;
                    status_indice.text = "Muito alto";
                    break;

                case 9:
                    target = new Vector3(0, 0, -132.4f);
                    targetPos = new Vector3(0f, 0f, Mathf.LerpAngle(pivot.transform.eulerAngles.z, target.z, 1f * Time.deltaTime));
                    pivot.transform.eulerAngles = targetPos;
                    status_indice.text = "Muito alto";
                    break;

                case 10:
                    target = new Vector3(0, 0, -148.4f);
                    targetPos = new Vector3(0f, 0f, Mathf.LerpAngle(pivot.transform.eulerAngles.z, target.z, 1f * Time.deltaTime));
                    pivot.transform.eulerAngles = targetPos;
                    status_indice.text = "Muito alto";
                    break;

                case 11:
                    target = new Vector3(0, 0, -161.7f);
                    targetPos = new Vector3(0f, 0f, Mathf.LerpAngle(pivot.transform.eulerAngles.z, target.z, 1f * Time.deltaTime));
                    pivot.transform.eulerAngles = targetPos;
                    status_indice.text = "Extremo";
                    break;

                case 12:
                    target = new Vector3(0, 0, -175.5f);
                    targetPos = new Vector3(0f, 0f, Mathf.LerpAngle(pivot.transform.eulerAngles.z, target.z, 1f * Time.deltaTime));
                    pivot.transform.eulerAngles = targetPos;
                    status_indice.text = "Extremo";
                    break;
            }
        }

        else
        {
            Debug.LogWarning("Inv�lido");
        }
    }
}