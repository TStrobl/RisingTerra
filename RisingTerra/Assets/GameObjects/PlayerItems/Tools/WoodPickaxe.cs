using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameObjects.PlayerItems.Tools
{
    public class WoodPickaxe : Pickaxe
    {
        public override string ImageName
        {
            get
            {
                return "Items/Tools/Pickaxe_Wood";
            }
        }

        public override Enums.Materials Material
        {
            get
            {
                return Enums.Materials.Wood;
            }
        }

        public override float Strength
        {
            get
            {
                return 1;
            }
        }

        public override float SwingSpeed
        {
            get
            {
                return 1;
            }
        }

        public override void Awake()
        {
            base.Awake();
        }
    }
}
