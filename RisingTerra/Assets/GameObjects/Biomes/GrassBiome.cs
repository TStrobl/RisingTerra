using Assets.GameObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameObjects.World;

namespace Assets.GameObjects.Biomes
{
    public class GrassBiome : Biome, IBiome
    {
        public Enums.BlockTypes MainBlockType
        {
            get
            {
                return Enums.BlockTypes.Earth;
            }
        }

        List<MineralRate> IBiome.AvailableMinerals
        {
            get
            {
                var result = new List<MineralRate>();
                result.Add(new MineralRate { MineralType = Enums.BlockTypes.Coal, Rate = 0.001, MaxOccuranceSize = Enums.MineralOccurance.Large });
                result.Add(new MineralRate { MineralType = Enums.BlockTypes.Iron, Rate = 0.005, MaxOccuranceSize = Enums.MineralOccurance.Small });
                result.Add(new MineralRate { MineralType = Enums.BlockTypes.Stone, Rate = 0.4, MaxOccuranceSize = Enums.MineralOccurance.Unlimited });
                return result;
            }
        }
    }
}
