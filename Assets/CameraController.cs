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
    [SerializeField] float _yPos_Gameplay = 4f;
    [SerializeField] float _moveTime = 3f;


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
        _posTween = _mainCam.transform.DOMoveY(_yPos_Title, _moveTime);
    }

    public void MoveToGameplay()
    {
        _posTween.Kill();
        _posTween = _mainCam.transform.DOMoveY(_yPos_Gameplay, _moveTime);
    }

}
