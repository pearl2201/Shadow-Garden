using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface IPlayer
{
    void ExecuteCommandDirection(List<CommandDirection> listCmdDir);
    void Init(float posX, float posY, int delay);
    void UpdatePosition(List<V3<float>> pos);

}

