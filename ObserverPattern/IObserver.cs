using System;
using UnityEngine;

public interface IObserver
{
    //Subject utiliza esse método para se comunicar com os observadores
    void OnNotify<T>(T data, int? viewID) where T : Enum; //Restringe os parâmetros do método apenas para tipos derivados de Enum
}
