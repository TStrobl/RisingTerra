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
    /// Name des Bioms
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Name zur Bak-Datei (Save)
    /// </summary>
    public string SaveFileName;

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

        using (var fs = File.OpenRead(this.SaveFileName))
        {
            var binaryReader = new BinaryReader(fs);
            this.Name = binaryReader.ReadString();
            this.WorldSize = (Enums.WorldSize)binaryReader.ReadInt32();
            this.BiomeType = (Enums.BiomeTypes)binaryReader.ReadInt32();

            this.Width = ApplicationModel.GetWorldWidth(this.WorldSize);
            this.Height = ApplicationModel.GetWorldHeight(this.WorldSize);

            //Wir beginnen erst einmal auf dem Boden in der horizontalen Mitte
            this._currentX = this.Width / 2;
            this._currentY = 203;

            ushort relativePositionX = 0;
            ushort relativePositionY = 0;

            var beginPosY = (this._currentY * this.Width) * 8;
            var beginPosX = (this._currentX - 100) * 8;

            //Direkt auf die benöitgte Reihe und Spalte springen
            fs.Seek(beginPosY + beginPosX, SeekOrigin.Current);

            for (int j = 0; j < 200; j++)
            {
                for (int i = 0; i < 200; i++) //200 Blöcke auslesen
                {
                    var blockPosX = binaryReader.ReadUInt16();
                    var blockPosY = binaryReader.ReadUInt16();
                    var blockTypeBG = binaryReader.ReadUInt16();
                    var blockTypeFG = binaryReader.ReadUInt16();
                }
                //dann in die nächste Reihe springen, indem eimalig die maximale Breite hinzugefügt wird
                fs.Seek((this.Width - 200) * 8, SeekOrigin.Current);
            }
        }
    }

    void Update()
    {

    }
}
