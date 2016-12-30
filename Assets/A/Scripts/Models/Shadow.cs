using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class Shadow : AbstrackPlayer, IPlayer
{
    public void ExecuteCommandDirection(List<CommandDirection> listCmdDir)
    {

    }

    public void Init(float posX, float posY, int delay)
    {
        delayTurn = delay;
    }

    public void UpdatePosition(List<V3<float>> listPos)
    {
        if (delayTurn > listPos.Count)
        {

        }
        else
        {
            pos = transform.position;
            pos.x = listPos[delayTurn - 1].x;
            pos.y = listPos[delayTurn - 1].y;
            pos.z = listPos[delayTurn - 1].z;
            transform.position = pos;
        }
    }
}