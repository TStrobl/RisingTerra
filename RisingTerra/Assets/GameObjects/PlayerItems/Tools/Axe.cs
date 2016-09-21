using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameObjects.PlayerItems.Tools
{
    public class Axe : PlayerItem, IPlayerItem
    {
        public Enums.ItemClass ItemClass
        {
            get
            {
                return Enums.ItemClass.Tool;
            }
        }

        public Enums.ItemType ItemType
        {
            get
            {
                return Enums.ItemType.Axe;
            }
        }

        public void LoadAnimator()
        {
        }
    }
}
