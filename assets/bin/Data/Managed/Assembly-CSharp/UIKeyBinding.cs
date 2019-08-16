using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Key Binding")]
public class UIKeyBinding : MonoBehaviour
{
	public enum Action
	{
		PressAndClick,
		Select,
		All
	}

	public enum Modifier
	{
		Any,
		Shift,
		Control,
		Alt,
		None
	}

	private static List<UIKeyBinding> mList = new List<UIKeyBinding>();

	public KeyCode keyCode;

	public Modifier modifier;

	public Action action;

	[NonSerialized]
	private bool mIgnoreUp;

	[NonSerialized]
	private bool mIsInput;

	[NonSerialized]
	private bool mPress;

	public UIKeyBinding()
		: this()
	{
	}

	public static bool IsBound(KeyCode key)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		int i = 0;
		for (int count = mList.Count; i < count; i++)
		{
			UIKeyBinding uIKeyBinding = mList[i];
			if (uIKeyBinding != null && uIKeyBinding.keyCode == key)
			{
				return true;
			}
		}
		return false;
	}

	protected virtual void OnEnable()
	{
		mList.Add(this);
	}

	protected virtual void OnDisable()
	{
		mList.Remove(this);
	}

	protected virtual void Start()
	{
		UIInput component = this.GetComponent<UIInput>();
		mIsInput = (component != null);
		if (component != null)
		{
			EventDelegate.Add(component.onSubmit, OnSubmit);
		}
	}

	protected virtual void OnSubmit()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		if (UICamera.currentKey == keyCode && IsModifierActive())
		{
			mIgnoreUp = true;
		}
	}

	protected virtual bool IsModifierActive()
	{
		if (modifier == Modifier.Any)
		{
			return true;
		}
		if (modifier == Modifier.Alt)
		{
			if (UICamera.GetKey(308) || UICamera.GetKey(307))
			{
				return true;
			}
		}
		else if (modifier == Modifier.Control)
		{
			if (UICamera.GetKey(306) || UICamera.GetKey(305))
			{
				return true;
			}
		}
		else if (modifier == Modifier.Shift)
		{
			if (UICamera.GetKey(304) || UICamera.GetKey(303))
			{
				return true;
			}
		}
		else if (modifier == Modifier.None)
		{
			return !UICamera.GetKey(308) && !UICamera.GetKey(307) && !UICamera.GetKey(306) && !UICamera.GetKey(305) && !UICamera.GetKey(304) && !UICamera.GetKey(303);
		}
		return false;
	}

	protected virtual void Update()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		if (UICamera.inputHasFocus || (int)keyCode == 0 || !IsModifierActive())
		{
			return;
		}
		bool flag = UICamera.GetKeyDown(keyCode);
		bool flag2 = UICamera.GetKeyUp(keyCode);
		if (flag)
		{
			mPress = true;
		}
		if (action == Action.PressAndClick || action == Action.All)
		{
			if (flag)
			{
				UICamera.currentKey = keyCode;
				OnBindingPress(pressed: true);
			}
			if (mPress && flag2)
			{
				UICamera.currentKey = keyCode;
				OnBindingPress(pressed: false);
				OnBindingClick();
			}
		}
		if ((action == Action.Select || action == Action.All) && flag2)
		{
			if (mIsInput)
			{
				if (!mIgnoreUp && !UICamera.inputHasFocus && mPress)
				{
					UICamera.selectedObject = this.get_gameObject();
				}
				mIgnoreUp = false;
			}
			else if (mPress)
			{
				UICamera.hoveredObject = this.get_gameObject();
			}
		}
		if (flag2)
		{
			mPress = false;
		}
	}

	protected virtual void OnBindingPress(bool pressed)
	{
		UICamera.Notify(this.get_gameObject(), "OnPress", pressed);
	}

	protected virtual void OnBindingClick()
	{
		UICamera.Notify(this.get_gameObject(), "OnClick", null);
	}
}
