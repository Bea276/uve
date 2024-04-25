using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class login : MonoBehaviour
{
//desativar o botão
//ativar o botão quando o usuario preencher os txtmeshprogui
//mostrar o nome digitado no campo email na tela seguinte
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;
    public GameObject loginButton;

    void Update()
    {
        loginButton.SetActive(false);
        // Verifique se todos os campos estão preenchidos
        if (emailInputField.text != "" && passwordInputField.text != "")
        {
            // Ative o botão de login
            loginButton.SetActive(true);
        }
        else
        {
            // Desative o botão de login
            loginButton.SetActive(false);
        }
    }
}
