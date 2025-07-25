using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public abstract class Subject : MonoBehaviourPunCallbacks
{
    // Uma coleção com todos observadores desse disparador de eventos
    private List<IObserver> _observers = new List<IObserver>();

    //Adiciona o observador a coleção do disparador de eventos
    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }
    
    //Remove o observador a coleção do disparador de eventos
    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    //Dispara uma notificação para todos observadores contidos na coleção
    protected void NotifyObservers<T>(T data, int? viewID) where T : Enum //Restringe os parâmetros do método apenas para tipos derivados de Enum
    {
        _observers.ForEach((_observer) =>
        {
            _observer.OnNotify(data, viewID);
        });
    }
}