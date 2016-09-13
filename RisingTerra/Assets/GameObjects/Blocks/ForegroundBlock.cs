using UnityEngine;
using System.Collections;
using Assets.GameObjects;
using System;

public class ForegroundBlock : BaseBlock, IBlock
{
    public Enums.BlockClass BlockClass
    {
        get
        {
            return Enums.BlockClass.Foreground;
        }
    }

    public string LayerName
    {
        get
        {
            return "Ground";
        }
    }

    /// <summary>
    /// Block-Typ
    /// </summary>
    public Enums.BaseBlockType BlockType;

    public Sprite LoadSprite()
    {
        Sprite result = null;
        switch (this.BlockType)
        {
            case Enums.BaseBlockType.Earth:
                result = Resources.Load<Sprite>("BaseBlocks/Earth_Foreground_1");
                break;
        }
        return result;
    }
}
