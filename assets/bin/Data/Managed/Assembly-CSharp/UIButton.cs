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
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			if (!this.get_enabled())
			{
				return false;
			}
			Collider component = this.get_gameObject().GetComponent<Collider>();
			if (Object.op_Implicit(component) && component.get_enabled())
			{
				return true;
			}
			Collider2D component2 = this.GetComponent<Collider2D>();
			return Object.op_Implicit(component2) && component2.get_enabled();
		}
		set
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			if (isEnabled != value)
			{
				Collider component = this.get_gameObject().GetComponent<Collider>();
				if (component != null)
				{
					component.set_enabled(value);
					UIButton[] components = this.GetComponents<UIButton>();
					UIButton[] array = components;
					foreach (UIButton uIButton in array)
					{
						uIButton.SetState((!value) ? State.Disabled : State.Normal, false);
					}
				}
				else
				{
					Collider2D component2 = this.GetComponent<Collider2D>();
					if (component2 != null)
					{
						component2.set_enabled(value);
						UIButton[] components2 = this.GetComponents<UIButton>();
						UIButton[] array2 = components2;
						foreach (UIButton uIButton2 in array2)
						{
							uIButton2.SetState((!value) ? State.Disabled : State.Normal, false);
						}
					}
					else
					{
						this.set_enabled(value);
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
			if (mSprite != null && !string.IsNullOrEmpty(mNormalSprite) && mNormalSprite == mSprite.spriteName)
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
			if (mSprite2D != null && mNormalSprite2D == mSprite2D.sprite2D)
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
		if (mSprite != null)
		{
			mNormalSprite = mSprite.spriteName;
		}
		if (mSprite2D != null)
		{
			mNormalSprite2D = mSprite2D.sprite2D;
		}
	}

	protected override void OnEnable()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (isEnabled)
		{
			if (mInitDone)
			{
				OnHover(UICamera.hoveredObject == this.get_gameObject());
			}
		}
		else
		{
			SetState(State.Disabled, true);
		}
	}

	protected override void OnDragOver()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (isEnabled && (dragHighlight || UICamera.currentTouch.pressed == this.get_gameObject()))
		{
			base.OnDragOver();
		}
	}

	protected override void OnDragOut()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (isEnabled && (dragHighlight || UICamera.currentTouch.pressed == this.get_gameObject()))
		{
			base.OnDragOut();
		}
	}

	protected virtual void OnClick()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		if (current == null && isEnabled && TutorialMessage.IsActiveButton(this.get_gameObject()))
		{
			current = this;
			EventDelegate.Execute(onClick);
			current = null;
		}
	}

	public override void SetState(State state, bool immediate)
	{
		base.SetState(state, immediate);
		if (mSprite != null)
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
		else if (mSprite2D != null)
		{
			switch (state)
			{
			case State.Normal:
				SetSprite(mNormalSprite2D);
				break;
			case State.Hover:
				SetSprite((!(hoverSprite2D == null)) ? hoverSprite2D : mNormalSprite2D);
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
		if (mSprite != null && !string.IsNullOrEmpty(sp) && mSprite.spriteName != sp)
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
		if (sp != null && mSprite2D != null && mSprite2D.sprite2D != sp)
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
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (!isCheckNeedButtonEffect)
		{
			if (this.GetComponent<UINoAuto>() == null && null == this.GetComponent<UIButtonEffect>())
			{
				this.get_gameObject().AddComponent<UIButtonEffect>();
			}
			isCheckNeedButtonEffect = true;
		}
		base.OnPress(isPressed);
	}
}
