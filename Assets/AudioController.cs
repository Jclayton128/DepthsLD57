using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    //refs
    [SerializeField] AudioSource _auso_FX = null;
    [SerializeField] TextMeshPro _volumeTMP = null;

    //settings
    [SerializeField] AudioClip[] _emptySteps = null;
    [SerializeField] AudioClip[] _sandSteps = null;
    [SerializeField] AudioClip[] _dirtSteps = null;
    [SerializeField] AudioClip[] _rockSteps = null;

    [SerializeField] AudioClip[] _framingInstall = null;
    [SerializeField] AudioClip[] _collapseSoon = null;
    [SerializeField] AudioClip[] _collapseNow = null;
    [SerializeField] AudioClip[] _exhaustSoon = null;
    [SerializeField] AudioClip[] _exhaustNow = null;

    [SerializeField] AudioClip[] _energyGain = null;
    [SerializeField] AudioClip[] _emeraldGain = null;
    [SerializeField] AudioClip[] _framingGain = null;

    [SerializeField] AudioClip _chest = null;
    [SerializeField] AudioClip _key = null;
    [SerializeField] AudioClip _locked = null;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (_auso_FX.volume < 0.5)
            {
                _auso_FX.volume = 1;
                _volumeTMP.text = "(V)OLUME ON";
            }
            else
            {
                _auso_FX.volume = 0;
                _volumeTMP.text = "(V)OLUME OFF";
            }

        }

    }

    public void PlaySound_Empty()
    {
        int rand = UnityEngine.Random.Range(0, _emptySteps.Length);
        _auso_FX.PlayOneShot(_emptySteps[rand]);
    }

    public void PlaySound_Sand()
    {
        int rand = UnityEngine.Random.Range(0, _sandSteps.Length);
        _auso_FX.PlayOneShot(_sandSteps[rand]);
    }

    public void PlaySound_Dirt()
    {
        int rand = UnityEngine.Random.Range(0, _dirtSteps.Length);
        _auso_FX.PlayOneShot(_dirtSteps[rand]);
    }

    public void PlaySound_Rock()
    {
        int rand = UnityEngine.Random.Range(0, _rockSteps.Length);
        _auso_FX.PlayOneShot(_rockSteps[rand]);
    }

    public void PlaySound_FramingInstall()
    {
        int rand = UnityEngine.Random.Range(0, _framingInstall.Length);
        _auso_FX.PlayOneShot(_framingInstall[rand]);
    }
    public void PlaySound_CollapseSoon()
    {
        int rand = UnityEngine.Random.Range(0, _collapseSoon.Length);
        _auso_FX.PlayOneShot(_collapseSoon[rand]);
    }

    public void PlaySound_CollapseNow()
    {
        int rand = UnityEngine.Random.Range(0, _collapseNow.Length);
        _auso_FX.PlayOneShot(_collapseNow[rand]);
    }

    public void PlaySound_ExhaustSoon()
    {
        int rand = UnityEngine.Random.Range(0, _exhaustSoon.Length);
        _auso_FX.PlayOneShot(_exhaustSoon[rand]);
    }

    public void PlaySound_ExhaustNow()
    {
        int rand = UnityEngine.Random.Range(0, _exhaustNow.Length);
        _auso_FX.PlayOneShot(_exhaustNow[rand]);
    }



    public void PlaySound_EnergyGain()
    {
        int rand = UnityEngine.Random.Range(0, _energyGain.Length);
        _auso_FX.PlayOneShot(_energyGain[rand]);
    }
    public void PlaySound_EmeraldGain()
    {
        int rand = UnityEngine.Random.Range(0, _emeraldGain.Length);
        _auso_FX.PlayOneShot(_emeraldGain[rand]);
    }
    public void PlaySound_FramingGain()
    {
        int rand = UnityEngine.Random.Range(0, _framingGain.Length);
        _auso_FX.PlayOneShot(_framingGain[rand]);
    }

    public void PlaySound_Key()
    {
        _auso_FX.PlayOneShot(_key);
    }

    public void PlaySound_Locked()
    {
        _auso_FX.PlayOneShot(_locked);
    }

    public void PlaySound_Chest()
    {
        _auso_FX.PlayOneShot(_chest);
    }
}
