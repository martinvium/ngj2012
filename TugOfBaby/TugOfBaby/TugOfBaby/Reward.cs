using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TugOfBaby
{
    class Reward
    {
        bool IS_ACTIVE = false;
        int effect;

        public int Effect
        {
            get { return effect; }
            set { effect = value; }
        }

        public int enable()
        {
            IS_ACTIVE = true;
            return effect;
        }
    }
}
