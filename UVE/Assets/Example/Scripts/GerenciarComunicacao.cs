using Android.BLE;
using Android.BLE.Commands;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GerenciarComunicacao : MonoBehaviour
{
    [SerializeField]
    private string _servico="ffe0",_caracteristica="ffe1";

    public SubscribeToCharacteristic sb;
    string _deviceUuid=string.Empty;
    
    Text receiveTXT;
    InputField sendInput;

    private void Start()
    {
        receiveTXT = GameObject.Find("ReceiveTXT").GetComponent<Text>();
        sendInput=GameObject.Find("SendINPUT").GetComponent<InputField>();

    }
    public void SubscribeServico(string _dvcUuid)
    {
        //aqui
        //ATENÇÃO, BLUETOOTH LOW ENERGY SÓ RECEBE 20 BYTES DE CADA VEZ, CONTANDO \r\n
        _deviceUuid = _dvcUuid;
         sb = new SubscribeToCharacteristic(_deviceUuid, _servico, _caracteristica, (byte[] value) =>
        {
            receiveTXT.text = Encoding.ASCII.GetString(value);
        });
        BleManager.Instance.QueueCommand(sb);
        sb.Start();
    }
    public void Enviar()
    {
        //ATENÇÃO, BLUETOOTH LOW ENERGY SÓ TRANSMITE 20 BYTES DE CADA VEZ, CONTANDO \r\n
        WriteToCharacteristic w = new WriteToCharacteristic(_deviceUuid, _servico, _caracteristica, sendInput.text);
        w.Start();
    }
}
