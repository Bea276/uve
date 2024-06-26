using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Android.BLE;
using Android.BLE.Commands;
using UnityEngine.Android;
using UnityEngine.UI;
using System.Text;
using TMPro;

public class BleInterador : MonoBehaviour
{
    [SerializeField]
    private Button botaoConectar, botaoDesconectar;
    [SerializeField]
    TextMeshProUGUI status, status2;
    [SerializeField]
    string nomeDispositivo;
    [SerializeField]
    private string _servico = "ffe0", _caracteristica = "ffe1";


    //aqui
    [SerializeField]
    private int _scanTime = 10;




    private ConnectToDevice _connectCommand;



    IConexao conexao;

    private void Start()
    {
#if !UNITY_EDITOR
        conexao = new BleInteradorAndroid(status, status2, _scanTime,nomeDispositivo,_servico,_caracteristica,Receber, botaoConectar,botaoDesconectar,null,null);
#endif
#if UNITY_EDITOR
        conexao = new BleInteradorWin(status, status2, _scanTime, nomeDispositivo, _servico, _caracteristica,Receber, botaoConectar, botaoDesconectar, null, null);
#endif
        botaoConectar.interactable = true;
        botaoDesconectar.interactable = false;
        conexao.OnScan();
    }
#if UNITY_EDITOR
    private void OnApplicationQuit()
    {
        BleApi.Quit();
    }
#endif


    public void ScanForDevices()
    {
        conexao.OnScan();
    }

    public void DisconnectDevice()
    {
        conexao.OnDesconectar();//desconecta o device     
    }

    private void Update()
    {
        conexao.Update_Unity();
    }

    private void OnDisconnected(string deviceUuid)
    {
        _connectCommand.End();
    }

    public void EnviarOn()
    {
        conexao.Enviar("on\n");
    }
    public void EnviarOff()
    {
        conexao.Enviar("off\n");
    }

    public void Receber(string dados)
    {

        Debug.LogWarning("dados################");
        Debug.LogWarning(dados[0]);
        Debug.LogWarning(dados.Substring(1, dados.Length - 1));

        if (dados[0] == 'i')
        {
            status.text = dados.Substring(1, dados.Length - 1);
        }
        else
        {
            status2.text = dados.Substring(1, dados.Length - 1);
        }

      //  status.text = dados;
    }
}
