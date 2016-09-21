using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameObjects.PlayerItems.Tools
{
    public class StonePickaxe : Pickaxe
    {
        public override string ImageName
        {
            get
            {
                return "Items/Tools/Pickaxe_Stone";
            }
        }

        public override Enums.Materials Material
        {
            get
            {
                return Enums.Materials.Stone;
            }
        }

        public override float Strength
        {
            get
            {
                return 2;
            }
        }

        public override float SwingSpeed
        {
            get
            {
                return 5;
            }
        }
    }
}
