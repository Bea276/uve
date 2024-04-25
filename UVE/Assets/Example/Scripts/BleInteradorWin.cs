using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;
using UnityEngine.UI;
using System;

public class BleInteradorWin : MonoBehaviour, IConexao
{
    TextMeshProUGUI status, status2;
    private int _scanTime;
  //  private float _scanTimer = 0f;
    private string nomeDispositivo;

    private string _deviceUuid = string.Empty;
    private string _servico, _caracteristica;

    Button connectarBtn, desconectarBtn, enviarOnBtn, enviarOffBtn;
    private bool _isScanning = false;
    private bool _isSubscribed = false;

    Dictionary<string, Dictionary<string, string>> devices = new Dictionary<string, Dictionary<string, string>>();
    Action<String> Receber;

    //Aqui no construtor vc deve colocar os objetos que est�o na interface que
    //voc� deseja usar
    public BleInteradorWin(TextMeshProUGUI p_status, TextMeshProUGUI p_status2, int p_scanTime, string p_nomeDispositivo,
                                                                       string p_servico,
                                                                       string p_caracteristica,
                                                                       Action<String> p_receber,
                                                                       Button p_connectar,
                                                                       Button p_desconectar,
                                                                       Button p_enviarOn,
                                                                       Button p_enviarOff
                                                                    )
    {
        _caracteristica = p_caracteristica;
        _scanTime = p_scanTime;
        status = p_status;
        status2 = p_status2;
        nomeDispositivo = p_nomeDispositivo;
        connectarBtn = p_connectar;
        desconectarBtn = p_desconectar;
        enviarOnBtn = p_enviarOn;
        enviarOffBtn = p_enviarOff;
        enviarOnBtn = p_enviarOn;
        enviarOffBtn = p_enviarOff;
        _servico = p_servico;
    }
    public void Start_Unity()
    {
        //no android faz nada
    }

    public void Subescrever(string _dvcUuid)
    {
        //aqui
        //ATEN��O, BLUETOOTH LOW ENERGY S� RECEBE 20 BYTES DE CADA VEZ, CONTANDO \r\n
        BleApi.ScanServices(_dvcUuid);
        BleApi.SubscribeCharacteristic(_deviceUuid, _servico, _caracteristica, false);
        _isSubscribed = true;

    }
    public void OnScan()//No Android era o ScanForDevices
    {
        _isScanning = true;
        BleApi.StartDeviceScan();
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

    }

    public void OnConectado(string deviceUuid)
    {
        //  connectarBtn.interactable = false;
        //  desconectarBtn.interactable = true;
        //  status.text = "subescrevendo";
        //  Subescrever(deviceUuid);
    }


    public void OnDesconectado(string deviceUuid)
    {
        //   connectarBtn.interactable = true;
        //   desconectarBtn.interactable = false;
        //   _connectCommand.End();
    }


    public void OnDeviceFound(string mac, string nome)
    {
        // if (nome == nomeDispositivo)
        // {
        //     status.text = "Nome: " + nome + " Device: " + mac + "\nAguarda mais um pouco";
        //     _deviceUuid = mac;
        //     _connectCommand = new ConnectToDevice(_deviceUuid, OnConectado, OnDesconectado);
        //     _isScanning = false;
        //     BleManager.Instance.QueueCommand(_connectCommand);
        // }
    }

    public void Update_Unity()
    {
        BleApi.ScanStatus statusBle;
        if (_isScanning)
        {
            status.text = "Procurado " + nomeDispositivo;
            BleApi.DeviceUpdate res = new BleApi.DeviceUpdate();
            do
            {
                statusBle = BleApi.PollDevice(ref res, false);
                if (statusBle == BleApi.ScanStatus.AVAILABLE)
                {
                    if (res.name == nomeDispositivo)
                    {
                        _isScanning = false;
                        Subescrever(res.id);
                        BleApi.StopDeviceScan();
                        break;
                    }
                }

            } while (statusBle == BleApi.ScanStatus.AVAILABLE);
        }
        else if (_isSubscribed)
        {
            BleApi.BLEData res = new BleApi.BLEData();
            while (BleApi.PollData(out res, false))
            {
                OnReceber(res.buf);
                // subcribeText.text = Encoding.ASCII.GetString(res.buf, 0, res.size);
            }

        }

    }
    private void OnApplicationQuit()
    {
        BleApi.Quit();
    }


    //Aqui � que as coisas podem ser alteradas
    public void Enviar(string dados)
    {
        byte[] payload = Encoding.ASCII.GetBytes(dados);
        BleApi.BLEData data = new BleApi.BLEData();
        data.buf = new byte[512];
        data.size = (short)payload.Length;
        data.deviceId = _deviceUuid;
        data.serviceUuid = _servico;
        data.characteristicUuid = _caracteristica;
        for (int i = 0; i < payload.Length; i++)
            data.buf[i] = payload[i];
        // no error code available in non-blocking mode
        BleApi.SendData(in data, false);
    }
    

    public void OnReceber(byte[] value)
    {
        Receber(BitConverter.ToString(value));
    }
}
