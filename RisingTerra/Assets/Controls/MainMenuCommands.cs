using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace RisingTerra.Assets.Controls
{
    public class MainMenuCommands : MonoBehaviour
    {
        public void ClickNewWorldButton()
        {
            Debug.Log("Click für neue Welt erstellen");

            var world = new World();
            world.Name = "Meine neue Welt";
            world.Biomes = new List<Biome>();

            var firstBiome = new Biome();
            firstBiome.WorldSize = Enums.WorldSize.Big;

            if (firstBiome.WorldSize == Enums.WorldSize.Big)
            {
                firstBiome.Height = 3800;
                firstBiome.Width = 4000;
            }
            else if (firstBiome.WorldSize == Enums.WorldSize.Normal)
            {
                firstBiome.Height = 1800;
                firstBiome.Width = 2000;
            }


            firstBiome.Blocks = new List<Block>();

            var typeRandomizer = new System.Random();


            using (var fs = File.Create("D:\\World_1.bak"))
            {
                var binaryWriter = new System.IO.BinaryWriter(fs);

                for (int i = 0; i < 700; i++)
                {
                    // Debug.Log("Reihe " + i);
                    for (int j = 0; j < 700; j++)
                    {
                        //binaryWriter.Write((ushort)i);
                        //binaryWriter.Write((ushort)j);
                        ////binaryWriter.Write((byte)i);
                        ////binaryWriter.Write((byte)typeRandomizer.Next(255));
                        ////binaryWriter.Write((byte)typeRandomizer.Next(255));
                        //binaryWriter.Write((ushort)typeRandomizer.Next(65000));
                        //binaryWriter.Write((ushort)typeRandomizer.Next(65000));
                        ////binaryWriter.Write(j);
                        ////binaryWriter.Write((byte)typeRandomizer.Next(200));
                        ////binaryWriter.Write((byte)typeRandomizer.Next(200));
                        var block = ScriptableObject.CreateInstance(typeof(Block)) as Block;
                        block.X = (ushort)j;
                        block.Y = (ushort)i;
                        block.ForegroundType = (ushort)typeRandomizer.Next(200);
                        block.BackgroundType = (ushort)typeRandomizer.Next(200);
                        firstBiome.Blocks.Add(block);
                    }
                }
            }

        }
    }
}
