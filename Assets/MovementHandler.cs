using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{

    [SerializeField] int _row;
    [SerializeField] int _col;

    private void Start()
    {
        _col = Mathf.RoundToInt(transform.position.x);
        _row = Mathf.RoundToInt(transform.position.y);
        TilesController.Instance.ProcessMove(_row, _col);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_row - 1 < 0)
            {
                GameController.Instance.StartNextMine();

                transform.position = new Vector2(_col,7);
                _col = Mathf.RoundToInt(transform.position.x);
                _row = Mathf.RoundToInt(transform.position.y);
                TilesController.Instance.ProcessMove(_row, _col);
            }

            else if (TilesController.Instance.CheckIfPositionIsValid(_col, _row - 1))
            {
                transform.position = new Vector2(_col, _row - 1);
                _col = Mathf.RoundToInt(transform.position.x);
                _row = Mathf.RoundToInt(transform.position.y);
                TilesController.Instance.ProcessMove(_row, _col);
            }

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (TilesController.Instance.CheckIfPositionIsValid(_col, _row + 1))
            {
                transform.position = new Vector2(_col, _row + 1);
                _col = Mathf.RoundToInt(transform.position.x);
                _row = Mathf.RoundToInt(transform.position.y);
                TilesController.Instance.ProcessMove(_row, _col);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (TilesController.Instance.CheckIfPositionIsValid(_col - 1, _row))
            {
                transform.position = new Vector2(_col - 1, _row);
                _col = Mathf.RoundToInt(transform.position.x);
                _row = Mathf.RoundToInt(transform.position.y);
                TilesController.Instance.ProcessMove(_row, _col);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (TilesController.Instance.CheckIfPositionIsValid(_col +1 , _row))
            {
                transform.position = new Vector2(_col + 1, _row);
                _col = Mathf.RoundToInt(transform.position.x);
                _row = Mathf.RoundToInt(transform.position.y);
                TilesController.Instance.ProcessMove(_row, _col);
            }
        }


    }
}
