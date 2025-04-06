using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController Instance { get; private set; }


    //settings
    [SerializeField] GameObject _playerPrefab = null;

    //state
    GameObject _player;


    private void Awake()
    {
        Instance = this;
    }



    public void StartRun()
    {
        if (_player) Destroy(_player);

        //Instantly excavate the place where player started
        TilesController.Instance.PushNewRandomTileValues();

        Vector2 startPos = TilesController.Instance.GetRandomStartPos();
        _player = Instantiate(_playerPrefab, startPos, Quaternion.identity);



    }

    


}
