﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
public class MainGameScripts : MonoBehaviour
{

    [SerializeField]
    private GameObject prefabCoin, prefabSha, prefabVatCan, prefabGround, prefabPlayer, parentBg, parentPlayer;

    private int currScore, currMaxTurn;

    private List<V3<float>> listPos;

    private Player player;

    void Awake()
    {
        listPos = new List<V3<float>>();
    }

    void Start()
    {
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                {
                    GameObject go = Instantiate(prefabGround) as GameObject;
                    go.transform.SetParent(parentBg.transform);
                    go.transform.position = new Vector3(i - 5, -0.5f, j - 5);
                    go.tag = "Ground";
                }
            }
        }
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if (i % 2 != 0 && j % 2 != 0)
                {
                    GameObject go = Instantiate(prefabVatCan) as GameObject;
                    go.transform.SetParent(parentBg.transform);
                    go.transform.position = new Vector3(i - 5, 0f, j - 5);
                    go.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                    go.tag = "wall";
                    MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
                    meshRenderer.material.color = Color.red;

                }
            }

        }

        {
            GameObject go = Instantiate(prefabPlayer) as GameObject;
            go.transform.SetParent(parentPlayer.transform);
            go.transform.position = new Vector3(-5, 0.2f, 5);
            go.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            player = go.GetComponent<Player>();
            player.mainGameScript = this;
        }
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            player.ExecuteCommandDirection(CommandDirection.NORTH);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            player.ExecuteCommandDirection(CommandDirection.SOUTH);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            player.ExecuteCommandDirection(CommandDirection.WEST);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            player.ExecuteCommandDirection(CommandDirection.EAST);
        }
    }

    void AddCoin()
    {

    }

    public void PlayerDeath()
    {

    }

    public void ExeEatCoin(Coin coin)
    {

    }


}

