using UnityEngine;
using Android.BLE;
using Android.BLE.Commands;
using UnityEngine.Android;
using UnityEngine.UI;
using System.Text;
using TMPro;

public class ExampleBleInteractor : MonoBehaviour
{
    [SerializeField]
    private Button botaoConectar, botaoDesconectar;
    [SerializeField]
    TextMeshProUGUI status;
    [SerializeField]
    string nomeDispositivo;
    [SerializeField]
    private string _servico = "ffe0", _caracteristica = "ffe1";

    private bool _isScanning = false;
    //#if !UNITY_EDITOR
    //aqui
    [SerializeField]
    private int _scanTime = 10;

    private float _scanTimer = 0f;


    private ConnectToDevice _connectCommand;

    private string _deviceUuid = string.Empty;

    public SubscribeToCharacteristic sb;

    private void Start()
    {

        botaoConectar.interactable = true;
        botaoDesconectar.interactable = false;


    }


    public void ScanForDevices()
    {
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
            status.text = "";
        }
    }

    public void DisconnectDevice()
    {
        _connectCommand.Disconnect();//desconecta o device     
    }

    private void Update()
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

    private void OnDeviceFound(string mac, string nome)
    {


        if (nome == nomeDispositivo)
        {
            status.text = "...";
            _deviceUuid = mac;
            _connectCommand = new ConnectToDevice(_deviceUuid, OnConnected, OnDisconnected);
            _isScanning = false;
            BleManager.Instance.QueueCommand(_connectCommand);

        }
    }


    private void OnConnected(string deviceUuid)
    {
        botaoConectar.interactable = false;
        botaoDesconectar.interactable = true;
        status.text = "...";
        SubscribeToExampleService();
    }

    private void OnDisconnected(string deviceUuid)
    {
        botaoConectar.interactable = true;
        botaoDesconectar.interactable = false;
        // _connectCommand = null;
        _connectCommand.End();



    }
    public void SubscribeToExampleService()
    {
        //aqui

        SubscribeServico(_deviceUuid);
    }

    public void SubscribeServico(string _dvcUuid)
    {
        //aqui
        //ATENÇÃO, BLUETOOTH LOW ENERGY SÓ RECEBE 20 BYTES DE CADA VEZ, CONTANDO \r\n
        _deviceUuid = _dvcUuid;
        sb = new SubscribeToCharacteristic(_deviceUuid, _servico, _caracteristica, (byte[] value) =>
        {
            status.text = Encoding.ASCII.GetString(value);
        });
        BleManager.Instance.QueueCommand(sb);
        sb.Start();
    }
    public void EnviarOn()
    {
        //ATENÇÃO, BLUETOOTH LOW ENERGY SÓ TRANSMITE 20 BYTES DE CADA VEZ, CONTANDO \r\n
        WriteToCharacteristic w = new WriteToCharacteristic(_deviceUuid, _servico, _caracteristica, "on\n");
        w.Start();
    }
    public void EnviarOff()
    {
        //ATENÇÃO, BLUETOOTH LOW ENERGY SÓ TRANSMITE 20 BYTES DE CADA VEZ, CONTANDO \r\n
        WriteToCharacteristic w = new WriteToCharacteristic(_deviceUuid, _servico, _caracteristica, "off\n");
        w.Start();
    }
}
