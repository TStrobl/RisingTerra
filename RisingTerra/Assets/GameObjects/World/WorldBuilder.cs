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
        private List<WorldCreationBlock> _blocks;

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

            this._blocks = new List<WorldCreationBlock>();
            var groundRandomizer = new System.Random();

            for (ushort i = 0; i < this._biomeHeight; i++)
            {
                for (ushort j = 0; j < this._biomeWidth; j++)
                {
                    var block = new WorldCreationBlock();
                    block.X = j;
                    block.Y = i;

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
                    this._blocks.Add(block);
                }
            }

            this.FillWithMinerals();

            //Biom vollständig. Jetzt muss es als Datei gespeichert werden
            using (var fs = File.Create(this._biomeSaveFilePath))
            {
                this._binaryWriter = new BinaryWriter(fs);
                //Die Header-Daten des Bioms
                this._binaryWriter.Write(biomeName);
                this._binaryWriter.Write((int)size);
                this._binaryWriter.Write((int)biomeType);

                foreach (var block in this._blocks)
                {
                    //Da immer Reihe für Reihe und dabei Block für Block angegeben werden, ist eine Koordinate
                    //normalerweise nicht nötig. Deswegen auskommentiert. Halbiert die Größe des Save-Files
                    //this._binaryWriter.Write(block.X);
                    //this._binaryWriter.Write(block.Y);
                    this._binaryWriter.Write(block.BackgroundType);
                    this._binaryWriter.Write(block.ForegroundType);
                }
            }
        }

        private void FillWithMinerals()
        {
            foreach(var block in this._blocks)
            {

            }
        }
    }
}
