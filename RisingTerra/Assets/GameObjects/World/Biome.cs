using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[RequireComponent(typeof(RectTransform))]
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
    /// Block für Erde im Vordergrund
    /// </summary>
    public GameObject EarthBlockForeground;

    /// <summary>
    /// Block für Erde im Vordergrund
    /// </summary>
    public GameObject EarthBlockBackground;

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
    /// Gibt die möglichen Basis-Blöcke zurück, die ein Biom enthalten kann
    /// </summary>
    /// <param name="biomeType">Der Typ des Bioms</param>
    /// <returns>Die möglichen Block-Typen</returns>
    public static List<Enums.BaseBlockType> GetPossibleBaseBlockTypesByBiome(Enums.BiomeTypes biomeType)
    {
        var result = new List<Enums.BaseBlockType>();
        switch (biomeType)
        {
            case Enums.BiomeTypes.Home:
                result.Add(Enums.BaseBlockType.Nothing);
                result.Add(Enums.BaseBlockType.Earth);
                result.Add(Enums.BaseBlockType.Stone);
                result.Add(Enums.BaseBlockType.Water);
                result.Add(Enums.BaseBlockType.Iron);
                result.Add(Enums.BaseBlockType.Coal);
                break;
        }
        return result;
    }



    void Start()
    {
        this.SaveFileName = ApplicationModel.CurrentBiomeFileName;
        this.SaveFileName = "D:\\Home1.bak";
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
            this._currentY = 203;

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
                    GameObject block = null;
                    if (blockTypeFG > 0)
                    {
                        block = Instantiate(EarthBlockForeground, new Vector3((i * ApplicationModel.BlockWidth) - (ApplicationModel.VisibleBlocksHorizontal * ApplicationModel.BlockWidth / 2), (j * ApplicationModel.BlockHeight) - (ApplicationModel.VisibleBlocksVertical * ApplicationModel.BlockHeight / 2)), Quaternion.identity) as GameObject;
                        this._allVisibleFGBlocks.Add(block);
                    }
                    else
                    {
                        block = Instantiate(EarthBlockBackground, new Vector3((i * ApplicationModel.BlockWidth) - (ApplicationModel.VisibleBlocksHorizontal * ApplicationModel.BlockWidth / 2), (j * ApplicationModel.BlockHeight) - (ApplicationModel.VisibleBlocksVertical * ApplicationModel.BlockHeight / 2)), Quaternion.identity) as GameObject;
                        this._allVisibleBGBlocks.Add(block);
                    }

                    var baseBlockComponent = block.GetComponent<BaseBlock>();
                    baseBlockComponent.RelativePosX = (ushort)i;
                    baseBlockComponent.RelativePosY = (ushort)j;
                    baseBlockComponent.PosX = (ushort)(this._currentX + i);
                    baseBlockComponent.PosY = (ushort)(this._currentY + j);
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
