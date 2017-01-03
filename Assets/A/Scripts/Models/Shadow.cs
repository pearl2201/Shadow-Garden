using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class Shadow : AbstrackPlayer, IPlayer
{
    private bool isChangedTag;
    private int tick;

    [SerializeField]
    private MeshRenderer meshRenderer;
    private int countColor;
    private bool isBlack;
    private Color alphaColor = new Color(0, 0, 0, 0);
    public void ExecuteCommandDirection(List<CommandDirection> listCmdDir)
    {

    }

    public void Init(float posX, float posY, int delay)
    {
        delayTurn = delay;
        tick = delayTurn;
        isBlack = true;
        countColor = 0;
    }

    public void UpdatePosition(List<Vector3> listPos)
    {
        if (tick > 0 || listPos.Count < delayTurn)
        {
            tick--;
            countColor++;
            if (countColor == 3)
            {
                alphaColor.a = 1 - alphaColor.a;
                meshRenderer.material.color = alphaColor;
                countColor = 0;
                isBlack = !isBlack;
            }
        }
        else
        {
            if (!isChangedTag)
            {

                gameObject.tag = "Shadow";
                isChangedTag = true;
                meshRenderer.material.color = Color.black;
            }
            pos = transform.position;
            pos.x = listPos[listPos.Count - delayTurn].x;
            pos.y = listPos[listPos.Count - delayTurn].y;
            pos.z = listPos[listPos.Count - delayTurn].z;
            transform.position = pos;
        }
    }
}