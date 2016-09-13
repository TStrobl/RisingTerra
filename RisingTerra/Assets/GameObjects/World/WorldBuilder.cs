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
        /// Die möglichen Block-Typen für ein Biom
        /// </summary>
        private List<Enums.BaseBlockType> _possibleBlockTypes;

        /// <summary>
        /// Der Typ des Bioms, der gebaut werden soll
        /// </summary>
        private Enums.BiomeTypes _biomeType;

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
            this._groundLevelMax = 203;
            this._groundLevelMin = 197;

            this._biomeSaveFilePath = saveFilePath;
            this._possibleBlockTypes = Biome.GetPossibleBaseBlockTypesByBiome(biomeType);


            var blocks = new List<WorldCreationBlock>();
            var possibleBlockTypes = Biome.GetPossibleBaseBlockTypesByBiome(this._biomeType);
            var blockRandomizer = new System.Random();


            for (ushort i = 0; i < this._biomeHeight; i++)
            {
                for (ushort j = 0; j < this._biomeWidth; j++)
                {
                    var block = new WorldCreationBlock();
                    block.X = j;
                    block.Y = i;
                    block.BackgroundType = (ushort)blockRandomizer.Next(possibleBlockTypes.Count - 1);
                    if (block.BackgroundType != (ushort)Enums.BaseBlockType.Nothing)
                    {
                        //ein vorderer Block kann nur existieren, wenn auch einer im Hintergrund existiert.
                        block.ForegroundType = (ushort)blockRandomizer.Next(possibleBlockTypes.Count - 1);
                    }
                    else
                    {
                        block.ForegroundType = (ushort)Enums.BaseBlockType.Nothing;
                    }
                    blocks.Add(block);
                }
            }

            //Biom vollständig. Jetzt muss es als Datei gespeichert werden
            using (var fs = File.Create(this._biomeSaveFilePath))
            {
                this._binaryWriter = new BinaryWriter(fs);
                //Die Header-Daten des Bioms
                this._binaryWriter.Write(biomeName);
                this._binaryWriter.Write((int)size);
                this._binaryWriter.Write((int)biomeType);

                foreach (var block in blocks)
                {
                    this._binaryWriter.Write(block.X);
                    this._binaryWriter.Write(block.Y);
                    this._binaryWriter.Write(block.BackgroundType);
                    this._binaryWriter.Write(block.ForegroundType);
                }
            }
        }
    }
}
