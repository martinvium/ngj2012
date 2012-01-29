using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TugOfBaby
{
    class Reward
    {
        public enum RewardType
        {
            COLLECT,
            BUNNY
        }

        bool IS_ACTIVE = false;
        int effect;
        int _evilPoints = 0;
        int _goodPoints = 0;
        RewardType _type = RewardType.COLLECT;

        public RewardType Type
        {
            get { return _type; }
            set { _type = value; }
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

        public void Apply(GameObject collider, GameObject good, GameObject evil)
        {
            if (collider.Reward.GoodPoints > 0)
            {
                good.Statistics.CollectItem(good, collider.Reward.GetAgnosticPoints());
                HeadsUpDisplay.HOW_EVIL += collider.Reward.GoodPoints;
            }
            else
            {
                evil.Statistics.CollectItem(evil, collider.Reward.GetAgnosticPoints());
                HeadsUpDisplay.HOW_EVIL -= collider.Reward.EvilPoints;
            }
        }
    }
}
