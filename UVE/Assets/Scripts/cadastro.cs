using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.Mail;

public class cadastro : MonoBehaviour
{
    //desativar o botão
    //ativar o botão quando o usuario preencher os txtmeshprogui
    //mostrar o nome digitado no campo email na tela seguinte
    public TMP_InputField nome;
    public TMP_InputField email;
    public TMP_InputField password;
    public TMP_InputField passwordc;
    public GameObject cadButton;

    void Update()
    {
        cadButton.SetActive(false);

        // Verifique se todos os campos estão preenchidos
        if (email.text != "" && password.text != "" && passwordc.text != "" && nome.text != "")
        {
            // Ative o botão de login
            cadButton.SetActive(true);
        }
        else
        {
            // Desative o botão de login
            cadButton.SetActive(false);
        }
    }
}
