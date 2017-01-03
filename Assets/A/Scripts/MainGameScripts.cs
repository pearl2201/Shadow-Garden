using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
public class MainGameScripts : MonoBehaviour
{

    [SerializeField]
    private GameObject prefabCoin, prefabSha, prefabVatCan, prefabGround, prefabPlayer, parentBg, parentPlayer;

    private int currScore, currMaxTurn;
    [SerializeField]
    private List<Vector3> listPos;
    private List<Shadow> listSha;
    private Player player;

    void Awake()
    {
        listPos = new List<Vector3>();
        listSha = new List<Shadow>();
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

        AddCoin();
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

        if (currMaxTurn > 0 && !player.isDead)
        {
           
            listPos.Add(player.transform.position);
            if (listPos.Count>currMaxTurn)
            {
                listPos.RemoveAt(0);
            }
            for (int i = 0; i < listSha.Count; i++)
            {
                listSha[i].UpdatePosition(listPos);
            }
        }
    }

    void AddCoin()
    {
        float x;
        float z;
        {
            List<float> listPosX = new List<float>();
            List<float> listPosZ = new List<float>();
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i % 2 == 0 && j % 2 == 0)
                    {
                        listPosX.Add(i);
                        listPosZ.Add(j);
                    }
                }

            }

            for (int i = listPosX.Count - 1; i > -1; i--)
            {
                if (Mathf.Abs(listPosX[i] - player.transform.position.x) + Mathf.Abs(listPosZ[i] - player.transform.position.z) < 4)
                {
                    listPosX.RemoveAt(i);
                    listPosZ.RemoveAt(i);
                }
            }

            int r = UnityEngine.Random.Range(0, listPosX.Count);
            x = listPosX[r];
            z = listPosZ[r];
        }

        {

            GameObject go = Instantiate(prefabCoin) as GameObject;
            go.transform.SetParent(parentBg.transform);
            go.transform.position = new Vector3(x - 5, 0.2f, z - 5);
            go.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            go.tag = "Coin";

        }
    }

    public void PlayerDeath()
    {

    }

    public void ExeEatCoin(GameObject coin)
    {
        Destroy(coin.gameObject);
        currMaxTurn += Config.DEFAULT_DIFF_DELAY;
        {
            GameObject go = Instantiate(prefabSha) as GameObject;
            go.transform.SetParent(parentPlayer.transform);
            go.transform.position = player.transform.position;
            go.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            
            Shadow shaScript = go.GetComponent<Shadow>();
            shaScript.mainGameScript = this;
            shaScript.Init(0, 0, currMaxTurn);
            listSha.Add(shaScript);
        }
        AddCoin();
    }


}

