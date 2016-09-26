using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.GameObjects.Interfaces;
using System;
using UnityEngine.UI;
using Assets.GameObject.Blocks;
using Assets.GameObjects.Blocks;
using Assets.GameObjects.Biomes;
using Assets.GameObjects.Attibutes;

public class Biome : MonoBehaviour
{
    /// <summary>
    /// Die aktuelle X-Koordinate, von dem aus die Blöcke geladen werden sollen
    /// </summary>
    private ushort _currentX;

    /// <summary>
    /// Die aktuelle Y-Koordinate, von dem aus die Blöcke geladen werden sollen
    /// </summary>
    private ushort _currentY;

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
    private BiomeRenderer _bgGrid;

    /// <summary>
    /// Das Grid im Vordergrund
    /// </summary>
    private BiomeRenderer _fgGrid;

    void Awake()
    {
        this._bgGrid = this.GetComponentsInChildren<BiomeRenderer>()[0];
        this._fgGrid = this.GetComponentsInChildren<BiomeRenderer>()[1];
        ApplicationModel.ImagePaths = new Dictionary<Enums.BlockTypes, string>();

        foreach (Enums.BlockTypes blockType in Enum.GetValues(typeof(Enums.BlockTypes)))
        {
            if (!ApplicationModel.ImagePaths.ContainsKey(blockType))
            {
                var imageAttr = blockType.GetMemberAttribute<BlockImageAttribute>();
                if (imageAttr != null)
                {
                    ApplicationModel.ImagePaths.Add(blockType, imageAttr.ForegroundImagePath);
                }
            }
        }
    }

    void Start()
    {
        this.SaveFileName = ApplicationModel.CurrentBiomeFileName;
        this.SaveFileName = "D:\\Grass1.bak";

        using (var fs = File.OpenRead(this.SaveFileName))
        {
            var binaryReader = new BinaryReader(fs);
            this.Name = binaryReader.ReadString();
            this.WorldSize = (Enums.WorldSize)binaryReader.ReadInt32();
            this.BiomeType = (Enums.BiomeTypes)binaryReader.ReadInt32();

            this.Width = ApplicationModel.GetWorldWidth(this.WorldSize);
            this.Height = ApplicationModel.GetWorldHeight(this.WorldSize);


            this._currentX = (ushort)(this.Width / 2);
            this._currentY = 180;

            var beginPosY = (this._currentY * this.Width) * WorldCreationBlock.ByteSize;
            var beginPosX = (this._currentX - (ApplicationModel.VisibleBlocksHorizontal / 2)) * WorldCreationBlock.ByteSize;

            int counter = 0;
            //Direkt auf die benötigte Reihe und Spalte springen
            fs.Seek(beginPosY + beginPosX, SeekOrigin.Current);
            for (ushort j = 0; j < ApplicationModel.VisibleBlocksVertical; j++)
            {
                for (ushort i = 0; i < ApplicationModel.VisibleBlocksHorizontal; i++)
                {
                    var blockTypeBG = binaryReader.ReadUInt16();
                    var blockTypeFG = binaryReader.ReadUInt16();
                    GameObject bgBlock = null;
                    GameObject fgBlock = null;
                    var bgAsEnum = (Enums.BlockTypes)blockTypeBG;
                    var fgAsEnum = (Enums.BlockTypes)blockTypeFG;

                    if (bgAsEnum != Enums.BlockTypes.Nothing)
                    {
                        //bgBlock = Instantiate(this.BackgroundBlock);
                        // bgBlock.GetComponent<BackgroundBlock>().BlockType = bgAsEnum;
                        // bgBlock.transform.SetParent(this._bgGrid.transform);
                    }
                    else
                    {
                        // bgBlock = Instantiate(this.EmptyBlock);
                        // bgBlock.transform.SetParent(this._bgGrid.transform);
                    }

                    if (fgAsEnum != Enums.BlockTypes.Nothing)
                    {
                        fgBlock = Instantiate(this.ForegroundBlock);
                        ForegroundBlock blockComponent = fgBlock.GetComponent<ForegroundBlock>();
                        blockComponent.BlockType = bgAsEnum;
                        blockComponent.PosX = (ushort)(this._currentX + i);
                        blockComponent.PosY = (ushort)(this._currentY + j);
                        blockComponent.WorldPosition = this.CalcBlockToWorldPosition(blockComponent.PosX, blockComponent.PosY);

                        this._fgGrid.AddBlock(blockComponent, i, j);
                    }
                    else
                    {
                        // fgBlock = Instantiate(this.EmptyBlock);
                        //fgBlock.transform.SetParent(this._fgGrid.transform);
                    }
                    counter++;
                }
                //dann in die nächste Reihe springen, indem eimalig die maximale Breite hinzugefügt wird
                fs.Seek((this.Width - ApplicationModel.VisibleBlocksHorizontal) * WorldCreationBlock.ByteSize, SeekOrigin.Current);
            }
        }

        this._fgGrid.SetInitialPlayerPosition();
    }

    void Update()
    {

    }

    /// <summary>
    /// Berechnet die relative Block-Koordinate im "Gitter" auf die Koordinate in der Welt um
    /// </summary>
    /// <returns></returns>
    Vector2 CalcBlockToWorldPosition(ushort posX, ushort posY)
    {
        //x ist abhängig von der gesamten Breite des Bioms
        var horizontalPositon = (this.Width / 2) - posX;
        var verticalPos = (this.Height / 2) - posY;

        return new Vector2(horizontalPositon, verticalPos);
    }
}
