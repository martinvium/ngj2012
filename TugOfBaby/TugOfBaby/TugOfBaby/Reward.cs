using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TugOfBaby
{
    class Reward
    {
        public enum Type
        {
            COLLECT,
            BUNNY
        }

        bool IS_ACTIVE = false;
        int effect;
        int _evilPoints = 0;
        int _goodPoints = 0;
        Type _type = Type.COLLECT;

        public Type GetType()
        {
            return _type;
        }

        public int EvilPoints
        {
            get { return _evilPoints; }
            set { _evilPoints = value; }
        }

        public int GoodPoints
        {
            get { return _goodPoints; }
            set { _goodPoints = value; }
        }

        public int GetAgnosticPoints()
        {
            if (_evilPoints > 0)
            {
                return _evilPoints;
            }
            else
            {
                return _goodPoints;
            }
        }

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
