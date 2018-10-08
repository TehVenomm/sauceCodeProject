using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button")]
public class UIButton : UIButtonColor
{
	public static UIButton current;

	public bool dragHighlight;

	public string hoverSprite;

	public string pressedSprite;

	public string disabledSprite;

	public Sprite hoverSprite2D;

	public Sprite pressedSprite2D;

	public Sprite disabledSprite2D;

	public bool pixelSnap;

	public List<EventDelegate> onClick = new List<EventDelegate>();

	[NonSerialized]
	private UISprite mSprite;

	[NonSerialized]
	private UI2DSprite mSprite2D;

	[NonSerialized]
	private string mNormalSprite;

	[NonSerialized]
	private Sprite mNormalSprite2D;

	private bool isCheckNeedButtonEffect;

	public override bool isEnabled
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			Collider component = base.gameObject.GetComponent<Collider>();
			if ((bool)component && component.enabled)
			{
				return true;
			}
			Collider2D component2 = GetComponent<Collider2D>();
			return (bool)component2 && component2.enabled;
		}
		set
		{
			if (isEnabled != value)
			{
				Collider component = base.gameObject.GetComponent<Collider>();
				if ((UnityEngine.Object)component != (UnityEngine.Object)null)
				{
					component.enabled = value;
					UIButton[] components = GetComponents<UIButton>();
					UIButton[] array = components;
					foreach (UIButton uIButton in array)
					{
						uIButton.SetState((!value) ? State.Disabled : State.Normal, false);
					}
				}
				else
				{
					Collider2D component2 = GetComponent<Collider2D>();
					if ((UnityEngine.Object)component2 != (UnityEngine.Object)null)
					{
						component2.enabled = value;
						UIButton[] components2 = GetComponents<UIButton>();
						UIButton[] array2 = components2;
						foreach (UIButton uIButton2 in array2)
						{
							uIButton2.SetState((!value) ? State.Disabled : State.Normal, false);
						}
					}
					else
					{
						base.enabled = value;
					}
				}
			}
		}
	}

	public string normalSprite
	{
		get
		{
			if (!mInitDone)
			{
				OnInit();
			}
			return mNormalSprite;
		}
		set
		{
			if (!mInitDone)
			{
				OnInit();
			}
			if ((UnityEngine.Object)mSprite != (UnityEngine.Object)null && !string.IsNullOrEmpty(mNormalSprite) && mNormalSprite == mSprite.spriteName)
			{
				mNormalSprite = value;
				SetSprite(value);
				NGUITools.SetDirty(mSprite);
			}
			else
			{
				mNormalSprite = value;
				if (mState == State.Normal)
				{
					SetSprite(value);
				}
			}
		}
	}

	public Sprite normalSprite2D
	{
		get
		{
			if (!mInitDone)
			{
				OnInit();
			}
			return mNormalSprite2D;
		}
		set
		{
			if (!mInitDone)
			{
				OnInit();
			}
			if ((UnityEngine.Object)mSprite2D != (UnityEngine.Object)null && (UnityEngine.Object)mNormalSprite2D == (UnityEngine.Object)mSprite2D.sprite2D)
			{
				mNormalSprite2D = value;
				SetSprite(value);
				NGUITools.SetDirty(mSprite);
			}
			else
			{
				mNormalSprite2D = value;
				if (mState == State.Normal)
				{
					SetSprite(value);
				}
			}
		}
	}

	protected override void OnInit()
	{
		base.OnInit();
		mSprite = (mWidget as UISprite);
		mSprite2D = (mWidget as UI2DSprite);
		if ((UnityEngine.Object)mSprite != (UnityEngine.Object)null)
		{
			mNormalSprite = mSprite.spriteName;
		}
		if ((UnityEngine.Object)mSprite2D != (UnityEngine.Object)null)
		{
			mNormalSprite2D = mSprite2D.sprite2D;
		}
	}

	protected override void OnEnable()
	{
		if (isEnabled)
		{
			if (mInitDone)
			{
				OnHover((UnityEngine.Object)UICamera.hoveredObject == (UnityEngine.Object)base.gameObject);
			}
		}
		else
		{
			SetState(State.Disabled, true);
		}
	}

	protected override void OnDragOver()
	{
		if (isEnabled && (dragHighlight || (UnityEngine.Object)UICamera.currentTouch.pressed == (UnityEngine.Object)base.gameObject))
		{
			base.OnDragOver();
		}
	}

	protected override void OnDragOut()
	{
		if (isEnabled && (dragHighlight || (UnityEngine.Object)UICamera.currentTouch.pressed == (UnityEngine.Object)base.gameObject))
		{
			base.OnDragOut();
		}
	}

	protected virtual void OnClick()
	{
		if ((UnityEngine.Object)current == (UnityEngine.Object)null && isEnabled && TutorialMessage.IsActiveButton(base.gameObject))
		{
			current = this;
			EventDelegate.Execute(onClick);
			current = null;
		}
	}

	public override void SetState(State state, bool immediate)
	{
		base.SetState(state, immediate);
		if ((UnityEngine.Object)mSprite != (UnityEngine.Object)null)
		{
			switch (state)
			{
			case State.Normal:
				SetSprite(mNormalSprite);
				break;
			case State.Hover:
				SetSprite((!string.IsNullOrEmpty(hoverSprite)) ? hoverSprite : mNormalSprite);
				break;
			case State.Pressed:
				SetSprite(pressedSprite);
				break;
			case State.Disabled:
				SetSprite(disabledSprite);
				break;
			}
		}
		else if ((UnityEngine.Object)mSprite2D != (UnityEngine.Object)null)
		{
			switch (state)
			{
			case State.Normal:
				SetSprite(mNormalSprite2D);
				break;
			case State.Hover:
				SetSprite((!((UnityEngine.Object)hoverSprite2D == (UnityEngine.Object)null)) ? hoverSprite2D : mNormalSprite2D);
				break;
			case State.Pressed:
				SetSprite(pressedSprite2D);
				break;
			case State.Disabled:
				SetSprite(disabledSprite2D);
				break;
			}
		}
	}

	protected void SetSprite(string sp)
	{
		if ((UnityEngine.Object)mSprite != (UnityEngine.Object)null && !string.IsNullOrEmpty(sp) && mSprite.spriteName != sp)
		{
			mSprite.spriteName = sp;
			if (pixelSnap)
			{
				mSprite.MakePixelPerfect();
			}
		}
	}

	protected void SetSprite(Sprite sp)
	{
		if ((UnityEngine.Object)sp != (UnityEngine.Object)null && (UnityEngine.Object)mSprite2D != (UnityEngine.Object)null && (UnityEngine.Object)mSprite2D.sprite2D != (UnityEngine.Object)sp)
		{
			mSprite2D.sprite2D = sp;
			if (pixelSnap)
			{
				mSprite2D.MakePixelPerfect();
			}
		}
	}

	protected override void OnPress(bool isPressed)
	{
		if (!isCheckNeedButtonEffect)
		{
			if ((UnityEngine.Object)GetComponent<UINoAuto>() == (UnityEngine.Object)null && (UnityEngine.Object)null == (UnityEngine.Object)GetComponent<UIButtonEffect>())
			{
				base.gameObject.AddComponent<UIButtonEffect>();
			}
			isCheckNeedButtonEffect = true;
		}
		base.OnPress(isPressed);
	}
}
