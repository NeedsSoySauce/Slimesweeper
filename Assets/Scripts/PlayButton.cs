using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
    }

    public void onClick()
    {
        gm.Play();
    }

}
