using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mostrar_nome : MonoBehaviour
{
    public TextMeshProUGUI nomemostra;

    void Update()
    {
      nomemostra.text = "Fernanda!";
    }
    
}
