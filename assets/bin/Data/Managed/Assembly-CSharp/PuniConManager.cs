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
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		Camera val = (!MonoBehaviourSingleton<UIManager>.IsValid()) ? MonoBehaviourSingleton<AppMain>.I.mainCamera : MonoBehaviourSingleton<UIManager>.I.uiCamera;
		punicon.get_transform().set_position(val.get_transform().get_position());
		punicon.uiCamera = val;
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
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
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
				punicon.SetStartPosition(Vector2.op_Implicit(link_info.beginPosition));
				if (link_info.activeAxis)
				{
					punicon.SetEndPosition(Vector2.op_Implicit(link_info.position));
				}
			}
		}
	}

	public void OnTouchOn(InputManager.TouchInfo touch_info)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (link_info == null && enableMultiTouch)
		{
			punicon.SetStartPosition(Vector2.op_Implicit(touch_info.position));
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
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		if (touch_info == link_info && (enableMultiTouch || MonoBehaviourSingleton<InputManager>.I.GetActiveInfoCount() < 2))
		{
			punicon.SetEndPosition(Vector2.op_Implicit(touch_info.position));
		}
	}

	public void OnLongTouch(InputManager.TouchInfo touch_info)
	{
	}

	public void OnDoubleDrag(InputManager.TouchInfo touch_info0, InputManager.TouchInfo touch_info1)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if (enableMultiTouch)
		{
			if (touch_info0 == link_info && touch_info0.enable)
			{
				punicon.SetEndPosition(Vector2.op_Implicit(touch_info0.position));
			}
			else if (touch_info1 == link_info && touch_info1.enable)
			{
				punicon.SetEndPosition(Vector2.op_Implicit(touch_info1.position));
			}
		}
	}
}
