using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    //scene ref
    [SerializeField] CinemachineImpulseSource _cis = null;
    [SerializeField] Camera _mainCam = null;

    //settings
    [SerializeField] float _yPos_Title = 18f;
    [SerializeField] float _yPos_Tutorial_High = 18f;
    [SerializeField] float _yPos_Tutorial_Low = 18f;
    [SerializeField] float _yPos_Gameplay = 4f;
    [SerializeField] float _moveTime_Start = 6f;


    //state
    Tween _posTween;

    private void Awake()
    {
        Instance = this;
        _mainCam.transform.position = new Vector3(_mainCam.transform.position.x, _yPos_Title, -10);
    }

    public void ShakeCamera(float magnitude)
    {
        _cis.GenerateImpulse(magnitude);
    }

    public void MoveToTitle()
    {
        _posTween.Kill();
        _posTween = _mainCam.transform.DOMoveY(_yPos_Title, _moveTime_Start);
    }

    public void MoveToTutorial_High()
    {
        _posTween.Kill();
        _posTween = _mainCam.transform.DOMoveY(_yPos_Tutorial_High, _moveTime_Start);
    }

    public void MoveToTutorial_Low()
    {
        _posTween.Kill();
        _posTween = _mainCam.transform.DOMoveY(_yPos_Tutorial_Low, _moveTime_Start);
    }

    public void MoveToGameplay()
    {
        _posTween.Kill();
        _posTween = _mainCam.transform.DOMoveY(_yPos_Gameplay, _moveTime_Start);
    }

}
