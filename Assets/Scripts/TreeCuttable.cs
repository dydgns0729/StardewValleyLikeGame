using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class TreeCuttable : ToolHit
    {
        public override void Hit()
        {
            Destroy(gameObject);
        }
    }
}