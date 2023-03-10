using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle.Match.Interface
{
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