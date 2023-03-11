using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Match.Interface
{
    public interface IPoolableObject
    {
        int ObjectID { get; }
        Transform Transform { get; }
    }
}