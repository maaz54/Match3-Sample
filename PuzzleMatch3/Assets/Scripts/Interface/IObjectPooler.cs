using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Match.Interface
{
    public interface IObjectPooler
    {
        /// <summary>
        /// Use to Pool object
        /// </summary>
        T Pool<T>(IPoolableObject _object, Transform parent = null) where T : MonoBehaviour;
        /// <summary>
        /// Use to remove gameoject
        /// </summary>
        // void Remove(IPoolableObject _object);
        void Remove<T>(T _object) where T : IPoolableObject;
    }
}