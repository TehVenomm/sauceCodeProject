using UnityEngine;

public class UINameInput : UIInput
{
	private bool isCaretDirection;

	private void OnDisable()
	{
		base.Cleanup();
	}

	protected override void Cleanup()
	{
		if (Object.op_Implicit(mHighlight))
		{
			mHighlight.set_enabled(false);
		}
		if (Object.op_Implicit(mCaret) && !isCaretDirection)
		{
			mCaret.set_enabled(false);
		}
	}

	private void SetColor(Color col)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		mDefaultColor = col;
		activeTextColor = col;
		this.GetComponent<UILabel>().color = col;
	}

	public void SetName(string _name)
	{
		if (!string.IsNullOrEmpty(_name))
		{
			base.value = _name;
		}
	}

	public void CreateCaret(bool is_enable)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (mBlankTex == null)
		{
			mBlankTex = new Texture2D(2, 2, 5, false);
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					mBlankTex.SetPixel(j, i, Color.get_white());
				}
			}
			mBlankTex.Apply();
		}
		if (mCaret == null)
		{
			mCaret = NGUITools.AddWidget<UITexture>(label.cachedGameObject);
			mCaret.set_name("Input Caret");
			mCaret.mainTexture = mBlankTex;
			mCaret.fillGeometry = false;
			mCaret.pivot = label.pivot;
			((UIRect)mCaret).SetAnchor(label.cachedTransform);
		}
		mCaret.set_enabled(is_enable);
		InActiveName();
		Init();
	}

	public void ActiveName()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		SetColor(Color.get_white());
		isCaretDirection = false;
		mNextBlink = RealTime.time;
	}

	public void InActiveName()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		UILabel component = this.GetComponent<UILabel>();
		string value = base.value;
		component.text = base.defaultText;
		base.value = string.Empty;
		SetInActiveNameColor();
		if (mCaret != null && string.IsNullOrEmpty(value))
		{
			label.PrintOverlay(0, 0, mCaret.geometry, null, caretColor, selectionColor);
		}
		isCaretDirection = true;
		mNextBlink = RealTime.time;
	}

	public void SetInActiveNameColor()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		SetColor(Color.get_gray());
	}

	protected override void Update()
	{
		base.Update();
		if (isCaretDirection && !base.isSelected && mCaret != null && mNextBlink < RealTime.time)
		{
			mNextBlink = RealTime.time + 0.5f;
			mCaret.set_enabled(!mCaret.get_enabled());
		}
	}
}
