using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerPoints : MonoBehaviour, IObservablePoints
{
    public static ManagerPoints instance;
    public Image barPoints;
    public float maxChargePoints;
    public float barDownSpeed;
    float _actualChargePoints;

    Action _ArtificialUpdate;
    List<IObserverPoints> _observerPoints = new List<IObserverPoints>();
    private void Awake()
    {
        instance = this;
        _ArtificialUpdate = UpdateChargePoints;
    }
    // Update is called once per frame
    void Update()
    {
        _ArtificialUpdate();
    }

    public void AddPoints(float points)
    {
        _actualChargePoints = Mathf.Clamp(_actualChargePoints + points, 0, maxChargePoints);

        if(_actualChargePoints >= maxChargePoints)
        {
            Call(UtilsPoints.Actions.CompleteChargePoints);
            _ArtificialUpdate += CheckChargePoints;
        }
    }

    void UpdateChargePoints()
    {
        _actualChargePoints = Mathf.Clamp(_actualChargePoints - (barDownSpeed * Time.deltaTime), 0, maxChargePoints);
        //barPoints.fillAmount = _actualChargePoints / maxChargePoints;
    }

    void CheckChargePoints()
    {
        if(_actualChargePoints <= 0)
        {
            Call(UtilsPoints.Actions.DisableChargePoints);
            _ArtificialUpdate -= CheckChargePoints;
        }
    }

    public void Call(UtilsPoints.Actions action)
    {
        foreach (var o in _observerPoints)
            o.ReceiveCall(action);
    }

    #region Subscribe
    public void Subscribe(IObserverPoints obs)
    {
        if (!_observerPoints.Contains(obs))
            _observerPoints.Add(obs);
    }
    public void Unsubscribe(IObserverPoints obs)
    {
        if (_observerPoints.Contains(obs))
            _observerPoints.Remove(obs);
    }
    #endregion
}
