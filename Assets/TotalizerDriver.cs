using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TotalizerDriver : MonoBehaviour
{

    //refs
    [SerializeField] TextMeshProUGUI _valueTMP = null;

    public void SetValue(int value)
    {
        _valueTMP.text = value.ToString();
        //JUICE TODO show the value shaking or crumbling if very close to the losing threshold
        if (value >= TilesController.Instance.CollapseThreshold)
        {
            _valueTMP.color = Color.white;
        }
        else _valueTMP.color = Color.red;
    }
}
