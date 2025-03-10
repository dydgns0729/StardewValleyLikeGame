using System.Collections.Generic;
using UnityEngine;

namespace MyStardewValleylikeGame
{
    public class ToolHit : MonoBehaviour
    {
        public virtual void Hit()
        {

        }

        public virtual bool CanBeHit(List<ResourceNodeType> canBeHit)
        {
            return true;
        }
    }
}