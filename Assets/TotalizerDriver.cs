using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TotalizerDriver : MonoBehaviour
{

    //refs
    int _currentValue;
    [SerializeField] TextMeshProUGUI _valueTMPGUI = null;
    [SerializeField] TextMeshPro _valueTMP = null;

    public void SetValue(int newValue)
    {

        if ( newValue <= TilesController.Instance.CollapseThreshold)
        {
            _valueTMP.color = Color.yellow;
            _valueTMP.text = "<shake>" + newValue.ToString();
            if (newValue < _currentValue)
            {
                CameraController.Instance.ShakeCamera(newValue/9f);
            }
        }
        else
        {
            _valueTMP.text = newValue.ToString();
            _valueTMP.color = Color.white;
        }
            


        _currentValue = newValue;
    }
}
