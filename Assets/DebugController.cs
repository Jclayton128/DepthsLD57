using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("showing values");
            TilesController.Instance.ShowAllValues();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("hiding values");
            TilesController.Instance.HideAllValues();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("starting new run");
            GameController.Instance.StartRun();
        }

    }
}
