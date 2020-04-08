using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AnimationCompletedEvent : UnityEvent<int> { }
public class AnimationListener : MonoBehaviour, IAnimationCompleted
{
	[HideInInspector]public readonly UnityEvent mOnAnimatorMove = new UnityEvent();
	readonly Dictionary<int, AnimationCompletedEvent> mOnAnimationCompleted = new Dictionary<int, AnimationCompletedEvent>();
	void OnAnimatorMove()
	{
		mOnAnimatorMove.Invoke();
	}
	public void AnimationCompleted(int pShortHashName)
    {
		AnimationCompletedEvent aEvent;
		if(mOnAnimationCompleted.TryGetValue(pShortHashName, out aEvent))
		{
			aEvent.Invoke(pShortHashName);
		}
    }

	public void RegisterOnAnimationCompleted(int pShortHashName, UnityAction<int> pOnAnimationCompletedAction)
	{
		AnimationCompletedEvent aEvent;
		if(!mOnAnimationCompleted.TryGetValue(pShortHashName, out aEvent))
		{
			aEvent = new AnimationCompletedEvent();
			mOnAnimationCompleted.Add(pShortHashName, aEvent);
		}
		aEvent.AddListener(pOnAnimationCompletedAction);
	}

	public void UnregisterOnAnimationCompleted(int pShortHashName, UnityAction<int> pOnAnimationCompletedAction)
	{
		AnimationCompletedEvent aEvent;
		if(mOnAnimationCompleted.TryGetValue(pShortHashName, out aEvent))
		{
			aEvent.RemoveListener(pOnAnimationCompletedAction);
		}
	}

}
