using Assets.GameObjects.World;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            var builder = ScriptableObject.CreateInstance<WorldBuilder>();
            builder.BuildBiome(Enums.WorldSize.Big, Enums.BiomeTypes.Home, "D:\\Home1.bak", "Home");
        }

        public void ClickStartGameButton()
        {
            //Jetzt ins Hauptspiel wechseln
            ApplicationModel.CurrentBiomeFileName = "D:\\Home1.bak";
            SceneManager.LoadScene("Scenes/MainWorld");
        }
    }
}
