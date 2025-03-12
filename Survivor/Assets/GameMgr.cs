using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{

    public static GameMgr inis;

    public GameObject obj;
    private void Awake()
    {
        inis = this;

        
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            obj.SetActive(true);
            Time.timeScale = 0;

        }
    }
}
