using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConexao 
{

    public void Start_Unity();
    public void Update_Unity();

    public void OnScan();
    public void OnCancelScan();
    public void OnConectar();
    public void OnDesconectar();

    public void OnReceber(byte[] value);
    public void Enviar(string dados);

    public void OnDeviceFound(string mac, string nome);
    public void OnConectado(string deviceUuid);
    public void OnDesconectado(string deviceUuid);
    public void Subescrever(string deviceUuid);
  
}
