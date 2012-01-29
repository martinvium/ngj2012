using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TugOfBaby
{
    class Stats
    {
        private static int _pointsCollected;
        private static int _itemsCollected;
        private static int _deedsDone;
    
        public Stats()         
        {
            _pointsCollected = 0;
            _itemsCollected = 0;
            _deedsDone = 0;
        }

        public void CollectItem(GameObject go, int points)
        {
            _itemsCollected += 1;
            _pointsCollected += points;
            go.Label = new FloatingScoreLabel("+" + points);
        }

        public void CompleteDeed(GameObject go, int points)
        {
            _deedsDone += 1;
            _pointsCollected += points;
            go.Label = new FloatingScoreLabel("+" + points);
        }

        #region Properties
        public int DeedsDone
        {
            get { return Stats._deedsDone; }
            set { Stats._deedsDone = value; }
        }
        public int ItemsCollected
        {
            get { return Stats._itemsCollected; }
            set { Stats._itemsCollected = value; }
        }
        public int PointsCollected
        {
            get { return Stats._pointsCollected; }
            set { Stats._pointsCollected = value; }
        }
        #endregion
    }
}
