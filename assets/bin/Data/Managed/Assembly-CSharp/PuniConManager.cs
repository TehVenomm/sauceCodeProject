using System;
using UnityEngine;

public class PuniConManager : MonoBehaviourSingleton<PuniConManager>
{
	[SerializeField]
	private PuniController punicon;

	protected InputManager.TouchInfo link_info;

	public bool enableMultiTouch
	{
		get;
		set;
	}

	protected override void Awake()
	{
		base.Awake();
		Camera camera = (!MonoBehaviourSingleton<UIManager>.IsValid()) ? MonoBehaviourSingleton<AppMain>.I.mainCamera : MonoBehaviourSingleton<UIManager>.I.uiCamera;
		punicon.transform.position = camera.transform.position;
		punicon.uiCamera = camera;
		link_info = null;
		InputManager.OnTouchOn = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnTouchOn, new InputManager.OnTouchDelegate(OnTouchOn));
		InputManager.OnTouchOff = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnTouchOff, new InputManager.OnTouchDelegate(OnTouchOff));
		InputManager.OnDrag = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnDrag, new InputManager.OnTouchDelegate(OnDrag));
		InputManager.OnLongTouch = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnLongTouch, new InputManager.OnTouchDelegate(OnLongTouch));
		InputManager.OnDoubleDrag = (InputManager.OnDoubleTouchDelegate)Delegate.Combine(InputManager.OnDoubleDrag, new InputManager.OnDoubleTouchDelegate(OnDoubleDrag));
	}

	protected override void _OnDestroy()
	{
		base._OnDestroy();
		InputManager.OnTouchOn = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnTouchOn, new InputManager.OnTouchDelegate(OnTouchOn));
		InputManager.OnTouchOff = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnTouchOff, new InputManager.OnTouchDelegate(OnTouchOff));
		InputManager.OnDrag = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnDrag, new InputManager.OnTouchDelegate(OnDrag));
		InputManager.OnLongTouch = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnLongTouch, new InputManager.OnTouchDelegate(OnLongTouch));
		InputManager.OnDoubleDrag = (InputManager.OnDoubleTouchDelegate)Delegate.Remove(InputManager.OnDoubleDrag, new InputManager.OnDoubleTouchDelegate(OnDoubleDrag));
	}

	public void Update()
	{
		bool flag = !enableMultiTouch && MonoBehaviourSingleton<InputManager>.I.GetActiveInfoCount() >= 2;
		if (link_info != null && (link_info.id == -1 || flag))
		{
			punicon.Reset();
			link_info = null;
		}
		if (link_info == null && !flag)
		{
			InputManager.TouchInfo activeInfo = MonoBehaviourSingleton<InputManager>.I.GetActiveInfo(true);
			if (activeInfo != null)
			{
				link_info = activeInfo;
				punicon.SetStartPosition(link_info.beginPosition);
				if (link_info.activeAxis)
				{
					punicon.SetEndPosition(link_info.position);
				}
			}
		}
	}

	public void OnTouchOn(InputManager.TouchInfo touch_info)
	{
		if (link_info == null && enableMultiTouch)
		{
			punicon.SetStartPosition(touch_info.position);
			link_info = touch_info;
		}
	}

	public void OnTouchOff(InputManager.TouchInfo touch_info)
	{
		if (touch_info == link_info)
		{
			punicon.Reset();
			link_info = null;
		}
	}

	public void OnDrag(InputManager.TouchInfo touch_info)
	{
		if (touch_info == link_info && (enableMultiTouch || MonoBehaviourSingleton<InputManager>.I.GetActiveInfoCount() < 2))
		{
			punicon.SetEndPosition(touch_info.position);
		}
	}

	public void OnLongTouch(InputManager.TouchInfo touch_info)
	{
	}

	public void OnDoubleDrag(InputManager.TouchInfo touch_info0, InputManager.TouchInfo touch_info1)
	{
		if (enableMultiTouch)
		{
			if (touch_info0 == link_info && touch_info0.enable)
			{
				punicon.SetEndPosition(touch_info0.position);
			}
			else if (touch_info1 == link_info && touch_info1.enable)
			{
				punicon.SetEndPosition(touch_info1.position);
			}
		}
	}
}
