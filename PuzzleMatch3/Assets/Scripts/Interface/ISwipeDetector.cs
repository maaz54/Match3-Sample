using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle.Match.Interface
{
    /// <summary>
    /// Use to detect swipe direction call back
    /// </summary>
    public interface ISwipeDetector
    {
        public SwipeDirectionEvent OnSwipe { get; set; }
    }

    public class SwipeDirectionEvent : UnityEvent<SwipeDirection> { }
    public enum SwipeDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}