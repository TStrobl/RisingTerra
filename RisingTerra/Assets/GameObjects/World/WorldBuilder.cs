using Assets.GameObjects.Attibutes;
using Assets.GameObjects.Biomes;
using Assets.GameObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameObjects.World
{
    public class WorldBuilder : ScriptableObject
    {
        /// <summary>
        /// Breite des Bioms
        /// </summary>
        private ushort _biomeWidth;

        /// <summary>
        /// Höhe des Bioms
        /// </summary>
        private ushort _biomeHeight;

        /// <summary>
        /// Speicherort des Bioms
        /// </summary>
        private string _biomeSaveFilePath;

        /// <summary>
        /// Maximaler Wert für das Ground-Level
        /// </summary>
        private ushort _groundLevelMax;

        /// <summary>
        /// Minimaler Wert für das Ground-Level
        /// </summary>
        private ushort _groundLevelMin;

        /// <summary>
        /// Der BinaryWriter, mit dem die Blöcke gespeichert werden
        /// </summary>
        private BinaryWriter _binaryWriter;

        /// <summary>
        /// Der Typ des Bioms, der gebaut werden soll
        /// </summary>
        private Enums.BiomeTypes _biomeType;

        /// <summary>
        /// Alle Blöcke
        /// </summary>
        private WorldCreationBlock[][] _blocks;

        /// <summary>
        /// Baut ein Biom zusammen
        /// </summary>
        /// <param name="biomeType">Der Typ des Bioms</param>
        public void BuildBiome(Enums.WorldSize size, Enums.BiomeTypes biomeType, string saveFilePath, string biomeName)
        {
            GC.Collect();
            this._biomeWidth = ApplicationModel.GetWorldWidth(size);
            this._biomeHeight = ApplicationModel.GetWorldHeight(size);
            this._biomeType = biomeType;
            this._groundLevelMax = 201;
            this._groundLevelMin = 199;

            this._biomeSaveFilePath = saveFilePath;

            IBiome biomeInst = null;

            switch (biomeType)
            {
                case Enums.BiomeTypes.Gras:
                    biomeInst = new GrassBiome();
                    break;
            }

            this._blocks = new WorldCreationBlock[this._biomeHeight][];
            var groundRandomizer = new System.Random();

            for (ushort i = 0; i < this._biomeHeight; i++)
            {
                for (ushort j = 0; j < this._biomeWidth; j++)
                {
                    var block = new WorldCreationBlock();

                    //Die ersten ca. 200 reihen besitzen keine Blöcke
                    if (i <= this._groundLevelMin)
                    {
                        block.BackgroundType = 0;
                        block.ForegroundType = 0;
                    }
                    else if (i > this._groundLevelMin && i <= this._groundLevelMax)
                    {
                        //50% Chance, dass es etwas enthält
                        if (groundRandomizer.Next(0, 2) == 0)
                        {
                            block.BackgroundType = 0;
                            block.ForegroundType = 0;
                        }
                        else
                        {
                            block.BackgroundType = (ushort)biomeInst.MainBlockType;
                            block.ForegroundType = (ushort)biomeInst.MainBlockType;
                        }
                    }
                    else
                    {
                        //zuerst alles mit dem Hauptblock füllen
                        block.BackgroundType = (ushort)biomeInst.MainBlockType;
                        block.ForegroundType = (ushort)biomeInst.MainBlockType;
                    }

                    if (this._blocks[i] == null || this._blocks[i].Length == 0)
                    {
                        this._blocks[i] = new WorldCreationBlock[this._biomeWidth];
                    }
                    this._blocks[i][j] = block;
                }
            }

            this.FillWithMinerals(biomeInst);

            //Biom vollständig. Jetzt muss es als Datei gespeichert werden
            using (var fs = File.Create(this._biomeSaveFilePath))
            {
                this._binaryWriter = new BinaryWriter(fs);
                //Die Header-Daten des Bioms
                this._binaryWriter.Write(biomeName);
                this._binaryWriter.Write((int)size);
                this._binaryWriter.Write((int)biomeType);

                foreach (var column in this._blocks)
                {
                    //Da immer Reihe für Reihe und dabei Block für Block angegeben werden, ist eine Koordinate
                    //normalerweise nicht nötig. Deswegen auskommentiert. Halbiert die Größe des Save-Files
                    //this._binaryWriter.Write(block.X);
                    //this._binaryWriter.Write(block.Y);
                    foreach (var block in column)
                    {
                        this._binaryWriter.Write(block.BackgroundType);
                        this._binaryWriter.Write(block.ForegroundType);
                    }
                }
            }
        }

        /// <summary>
        /// Befüllt das Biome mit den Mineralien in der Relation wie angegeben
        /// </summary>
        /// <param name="biomeInst">Die Instanz des Bioms, das befüllt werden soll</param>
        private void FillWithMinerals(IBiome biomeInst)
        {
            var relation = biomeInst.AvailableMinerals;
            double countOfBlocksComplete = this._biomeWidth * this._biomeHeight;

            var allOccuranceSizes = new Dictionary<Enums.MineralOccurance, int>();
            foreach (Enums.MineralOccurance field in Enum.GetValues(typeof(Enums.MineralOccurance)))
            {
                allOccuranceSizes.Add(field, field.GetMemberAttribute<MineralOccuranceSizeAttribute>().CountOfBlocks);
            }

            foreach (MineralRate mineralRate in relation)
            {
                var minSizeVal = Enums.MineralOccurance.SingleBlock;
                var maxSizeVal = mineralRate.MaxOccuranceSize;

                double countOfBlocksCurrentMineral = 0;
                double neededBlocks = countOfBlocksComplete * mineralRate.Rate;
                while (countOfBlocksCurrentMineral < neededBlocks) //Bis die prozentuale Menge erreicht ist.
                {
                    var randomY = UnityEngine.Random.Range(200, this._biomeHeight); //200 weil im oberen Breich natürlich nichts sein kann
                    var randomX = UnityEngine.Random.Range(0, this._biomeWidth);

                    int randomSize = UnityEngine.Random.Range(allOccuranceSizes[minSizeVal], allOccuranceSizes[maxSizeVal]);
                    var randomIsForeground = UnityEngine.Random.Range(0, 2);

                    var randomSizeFormX = UnityEngine.Random.Range(1, randomSize);
                    var randomSizeFormY = randomSize - randomSizeFormX;

                    var blockOnCoordinate = this._blocks[randomY][randomX];

                    //Wenn an dieser Koordinate Wasser oder eine Höhle ist, darf es wenn dann nur im Hintergrund gesetz werden
                    if (blockOnCoordinate.IsFluid || blockOnCoordinate.IsCave)
                    {

                    }
                    else if (blockOnCoordinate.IsMineral) //wenn hier nicht schon ein anderes Mineral existiert.
                    {

                    }
                    else
                    {
                        for (int i = 0; i < randomSizeFormX; i++)
                        {
                            for (int j = 0; j < randomSizeFormY; j++)
                            {
                                //Erst mal schauen, ob die Koordinate existiert
                                if (this._blocks.Length > randomY + j && this._blocks[randomY + j].Length > randomX + i)
                                {
                                    
                                    var block = this._blocks[randomY + j][randomX + i];
                                    if (randomIsForeground == 0)
                                    {
                                        block.ForegroundType = (ushort)mineralRate.MineralType;
                                    }
                                    else if(randomIsForeground == 1)
                                    {
                                        block.BackgroundType = (ushort)mineralRate.MineralType;
                                    }
                                    else
                                    {
                                        block.ForegroundType = (ushort)mineralRate.MineralType;
                                        block.BackgroundType = (ushort)mineralRate.MineralType;
                                    }

                                    block.IsMineral = true;
                                    this._blocks[randomY + j][randomX + i] = block; //Das ist ein struct, also muss es neu zugewiesen werden
                                    countOfBlocksCurrentMineral++;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
