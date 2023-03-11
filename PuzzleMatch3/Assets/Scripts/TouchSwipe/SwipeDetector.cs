using Puzzle.Match.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle.Match.SwipeDetect
{
    /// <summary>
    /// Contains the logic of swipe detection for editor and mobile device
    /// </summary>
    public class SwipeDetector : MonoBehaviour, ISwipeDetector
    {
        /// <summary>
        /// use fire the swipe direction
        /// </summary>
        /// <returns></returns>
        public SwipeDirectionEvent OnSwipe { get; set; } = new();
        [SerializeField] private float minSwipeDistance = 50f;
        [SerializeField] private float maxSwipeTime = 1f;

        private Vector2 startTouchPosition;
        private Vector3 startMousePosition;
        private float startTime;

        private void Update()
        {
#if UNITY_EDITOR
            DetectMouseSwipe();
#else
            DetectTouchSwipe();
#endif
        }

        /// <summary>
        /// Use to handle Swipe detection on from Mouse position
        /// </summary>
        private void DetectMouseSwipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                startMousePosition = Input.mousePosition;
                startTime = Time.time;
            }

            if (Input.GetMouseButtonUp(0))
            {
                float swipeDistance = Vector2.Distance(startMousePosition, Input.mousePosition);
                float swipeTime = Time.time - startTime;

                if (swipeDistance > minSwipeDistance && swipeTime < maxSwipeTime)
                {
                    Vector2 swipeDirection = Input.mousePosition - startMousePosition;

                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        if (swipeDirection.x < 0)
                        {
                            OnSwipe?.Invoke(SwipeDirection.Left);
                        }
                        else
                        {
                            OnSwipe?.Invoke(SwipeDirection.Right);
                        }
                    }
                    else
                    {
                        if (swipeDirection.y < 0)
                        {
                            OnSwipe?.Invoke(SwipeDirection.Down);
                        }
                        else
                        {
                            OnSwipe?.Invoke(SwipeDirection.Up);
                        }
                    }
                }
                else
                {
                    OnSwipe?.Invoke(0);
                }
            }
        }

        /// <summary>
        /// Use to handle Swipe detection on from mobile device touch position
        /// </summary>
        private void DetectTouchSwipe()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startTouchPosition = touch.position;
                        startTime = Time.time;
                        break;

                    case TouchPhase.Ended:
                        float swipeDistance = Vector2.Distance(startTouchPosition, touch.position);
                        float swipeTime = Time.time - startTime;

                        if (swipeDistance > minSwipeDistance && swipeTime < maxSwipeTime)
                        {
                            Vector2 swipeDirection = touch.position - startTouchPosition;

                            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                            {
                                if (swipeDirection.x < 0)
                                {
                                    OnSwipe?.Invoke(SwipeDirection.Left);
                                }
                                else
                                {
                                    OnSwipe?.Invoke(SwipeDirection.Right);
                                }
                            }
                            else
                            {
                                if (swipeDirection.y < 0)
                                {
                                    OnSwipe?.Invoke(SwipeDirection.Down);
                                }
                                else
                                {
                                    OnSwipe?.Invoke(SwipeDirection.Up);
                                }
                            }
                        }
                        else
                        {
                            OnSwipe?.Invoke(0);
                        }
                        break;
                }
            }
        }
    }
}