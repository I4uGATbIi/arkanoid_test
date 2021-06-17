using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Views
{
    public class WallsView : MonoBehaviour
    {
        public List<WallToTypePair> WallToTypePairs;

        public WallType GetWallType(GameObject wall)
        {
            return WallToTypePairs.FirstOrDefault(pair => pair.Wall == wall).Type;
        }
    }

    public enum WallType
    {
        Bounce,
        Death
    }

    [Serializable]
    public struct WallToTypePair
    {
        public GameObject Wall;
        public WallType Type;
    }
}