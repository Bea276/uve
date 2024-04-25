using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Android.BLE;
using Android.BLE.Commands;
using System.Text;
using TMPro;
using UnityEngine.UI;
using System;

public class BleInteradorAndroid : MonoBehaviour,IConexao
{
    TextMeshProUGUI status, status2;
    private int _scanTime;
    private float _scanTimer = 0f;
    private string nomeDispositivo;
    private ConnectToDevice _connectCommand;

    private string _deviceUuid = string.Empty;
    public SubscribeToCharacteristic sb;
    [SerializeField]
    private string _servico, _caracteristica;

    Button connectarBtn, desconectarBtn, enviarOnBtn, enviarOffBtn;
    Action<String> Receber;


    //Aqui no construtor vc deve colocar os objetos que est�o na interface que
    //voc� deseja usar
    public BleInteradorAndroid(TextMeshProUGUI p_status, TextMeshProUGUI p_status2, int p_scanTime,string p_nomeDispositivo,
                                                                       string p_servico,
                                                                       string p_caracteristica,
                                                                       Action<String> p_receber,
                                                                       Button p_connectar,
                                                                       Button p_desconectar,
                                                                       Button p_enviarOn,
                                                                       Button p_enviarOff
                                                                    )
    {
        _caracteristica= p_caracteristica;
        _scanTime=p_scanTime;
        status = p_status;
        status2 = p_status2;
        nomeDispositivo =p_nomeDispositivo;
        connectarBtn = p_connectar;
        desconectarBtn = p_desconectar;
        enviarOnBtn = p_enviarOn;
        enviarOffBtn = p_enviarOff;
        enviarOnBtn= p_enviarOn;
        enviarOffBtn= p_enviarOff;
        _servico= p_servico;
        Receber= p_receber;
    }
    private bool _isScanning = false;
    public void Start_Unity()
    {
        //no android faz nada
    }

    public void Subescrever(string _dvcUuid)
    {
        //aqui
        //ATEN��O, BLUETOOTH LOW ENERGY S� RECEBE 20 BYTES DE CADA VEZ, CONTANDO \r\n
        _deviceUuid = _dvcUuid;
        sb = new SubscribeToCharacteristic(_deviceUuid, _servico, _caracteristica, (byte[] value) =>
        {
            //status.text = Encoding.ASCII.GetString(value);
            OnReceber(value);
        });
        BleManager.Instance.QueueCommand(sb);
        sb.Start();
    }
    public void OnScan()//No Android era o ScanForDevices
    {
        status.text = "...";
        if (!_isScanning)
        {
            BleManager.CheckForLog("acionar busca");
            _scanTimer = 0;
            status.text = "...";
            _isScanning = true;
            BleManager.Instance.QueueCommand(new DiscoverDevices(OnDeviceFound, _scanTime * 1000));
        }
        else
        {
            status.text = "...";
        }
    }
    public void OnCancelScan()
    {
        throw new System.NotImplementedException();
    }
    public void OnConectar()
    {
        throw new System.NotImplementedException();
    }
    public void OnDesconectar()
    {
        _connectCommand.Disconnect();
    }

    public void OnConectado(string deviceUuid)
    {
        connectarBtn.interactable = false;
        desconectarBtn.interactable = true;
        status.text = "...";
        Subescrever(deviceUuid);
    }


    public void OnDesconectado(string deviceUuid)
    {
        connectarBtn.interactable = true;
        desconectarBtn.interactable = false;
        _connectCommand.End();
    }


    public void OnDeviceFound(string mac, string nome)
    {
        if (nome == nomeDispositivo)
        {
            status.text = "...";
            _deviceUuid = mac;
            _connectCommand = new ConnectToDevice(_deviceUuid, OnConectado, OnDesconectado);
            _isScanning = false;
            BleManager.Instance.QueueCommand(_connectCommand);
        }
    }

    public void Update_Unity()
    {
        if (_isScanning)
        {
            _scanTimer += Time.deltaTime;
            if (_scanTimer > _scanTime)
            {
                _scanTimer = 0f;
                _isScanning = false;
            }
        }
    }


    public void Enviar(string dados)
    {
        //ATEN��O, BLUETOOTH LOW ENERGY S� TRANSMITE 20 BYTES DE CADA VEZ, CONTANDO \r\n
        WriteToCharacteristic w = new WriteToCharacteristic(_deviceUuid, _servico, _caracteristica, dados);
        w.Start();
    }


    public void OnReceber(byte[] value)
    {
        Receber(Encoding.ASCII.GetString(value));
    }

 
}