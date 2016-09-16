using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.GameObjects.Interfaces;
using System;
using UnityEngine.UI;

public class Biome : MonoBehaviour
{
    /// <summary>
    /// Die aktuelle X-Koordinate, von dem aus die Blöcke geladen werden sollen
    /// </summary>
    private int _currentX;

    /// <summary>
    /// Die aktuelle Y-Koordinate, von dem aus die Blöcke geladen werden sollen
    /// </summary>
    private int _currentY;

    /// <summary>
    /// Alle Blöcke im Biome im Vordergrund
    /// </summary>
    private List<GameObject> _allVisibleFGBlocks;

    /// <summary>
    /// Alle Blöcke im Biome im Hintergrund
    /// </summary>
    private List<GameObject> _allVisibleBGBlocks;

    /// <summary>
    /// Name des Bioms
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Name zur Bak-Datei (Save)
    /// </summary>
    public string SaveFileName;

    /// <summary>
    /// Ein leerer Block
    /// </summary>
    public GameObject BackgroundBlock;

    /// <summary>
    /// Ein leerer Block
    /// </summary>
    public GameObject ForegroundBlock;

    /// <summary>
    /// Ein Leerer Block zum befüllen des Grids, das ohne irgendeine Steuerung auskommt
    /// </summary>
    public GameObject EmptyBlock;

    /// <summary>
    /// Das Level, also wie stark z.B. die Gegner sind
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// Größe der Welt
    /// </summary>
    public Enums.WorldSize WorldSize { get; set; }

    /// <summary>
    /// Der Typ
    /// </summary>
    public Enums.BiomeTypes BiomeType { get; set; }

    /// <summary>
    /// Höhe des Bioms in Blöcken
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Breite des Bioms in Blöcken
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Das Grid im Hintergrund
    /// </summary>
    private GridLayoutGroup _bgGrid;

    /// <summary>
    /// Das Grid im Vordergrund
    /// </summary>
    private GridLayoutGroup _fgGrid;

    void Awake()
    {
        this._bgGrid = this.GetComponentsInChildren<GridLayoutGroup>()[0];
        this._fgGrid = this.GetComponentsInChildren<GridLayoutGroup>()[1];
    }

    void Start()
    {
        this.SaveFileName = ApplicationModel.CurrentBiomeFileName;
        this.SaveFileName = "D:\\Grass1.bak";
        this._allVisibleFGBlocks = new List<GameObject>();
        this._allVisibleBGBlocks = new List<GameObject>();

        using (var fs = File.OpenRead(this.SaveFileName))
        {
            var binaryReader = new BinaryReader(fs);
            this.Name = binaryReader.ReadString();
            this.WorldSize = (Enums.WorldSize)binaryReader.ReadInt32();
            this.BiomeType = (Enums.BiomeTypes)binaryReader.ReadInt32();

            this.Width = ApplicationModel.GetWorldWidth(this.WorldSize);
            this.Height = ApplicationModel.GetWorldHeight(this.WorldSize);


            this._currentX = this.Width / 2;
            this._currentY = 180;

            var beginPosY = (this._currentY * this.Width) * WorldCreationBlock.ByteSize;
            var beginPosX = (this._currentX - (ApplicationModel.VisibleBlocksHorizontal / 2)) * WorldCreationBlock.ByteSize;

            //Direkt auf die benötigte Reihe und Spalte springen
            fs.Seek(beginPosY + beginPosX, SeekOrigin.Current);
            for (int j = 0; j < ApplicationModel.VisibleBlocksVertical; j++)
            {
                for (int i = 0; i < ApplicationModel.VisibleBlocksHorizontal; i++)
                {
                    var blockTypeBG = binaryReader.ReadUInt16();
                    var blockTypeFG = binaryReader.ReadUInt16();
                    GameObject bgBlock = null;
                    GameObject fgBlock = null;
                    var vector = new Vector3((i * ApplicationModel.BlockWidth), (j * ApplicationModel.BlockHeight) - 1500);                    //var vector = new Vector3(200, 200);
                    var bgAsEnum = (Enums.BlockTypes)blockTypeBG;
                    var fgAsEnum = (Enums.BlockTypes)blockTypeFG;

                    if (bgAsEnum != Enums.BlockTypes.Nothing)
                    {
                        bgBlock = Instantiate(this.BackgroundBlock);
                        bgBlock.GetComponent<BackgroundBlock>().BlockType = bgAsEnum;
                        bgBlock.transform.SetParent(this._bgGrid.transform);
                    }
                    else
                    {
                        bgBlock = Instantiate(this.EmptyBlock);
                        bgBlock.transform.SetParent(this._bgGrid.transform);
                    }

                    if (fgAsEnum != Enums.BlockTypes.Nothing)
                    {
                        fgBlock = Instantiate(this.ForegroundBlock);
                        fgBlock.GetComponent<ForegroundBlock>().BlockType = bgAsEnum;
                        fgBlock.transform.SetParent(this._fgGrid.transform);
                    }
                    else
                    {
                        fgBlock = Instantiate(this.EmptyBlock);
                        fgBlock.transform.SetParent(this._fgGrid.transform);
                    }
                }
                //dann in die nächste Reihe springen, indem eimalig die maximale Breite hinzugefügt wird
                fs.Seek((this.Width - ApplicationModel.VisibleBlocksHorizontal) * WorldCreationBlock.ByteSize, SeekOrigin.Current);
            }
        }
    }

    void Update()
    {

    }
}
