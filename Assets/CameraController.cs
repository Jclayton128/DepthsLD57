using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    //scene ref
    [SerializeField] CinemachineImpulseSource _cis = null;


    private void Awake()
    {
        Instance = this;
    }

    public void ShakeCamera(float magnitude)
    {
        _cis.GenerateImpulse(magnitude);
    }


}
