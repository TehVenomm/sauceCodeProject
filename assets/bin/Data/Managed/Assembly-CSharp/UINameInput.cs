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
		if ((bool)mHighlight)
		{
			mHighlight.enabled = false;
		}
		if ((bool)mCaret && !isCaretDirection)
		{
			mCaret.enabled = false;
		}
	}

	private void SetColor(Color col)
	{
		mDefaultColor = col;
		activeTextColor = col;
		GetComponent<UILabel>().color = col;
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
		if ((Object)mBlankTex == (Object)null)
		{
			mBlankTex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					mBlankTex.SetPixel(j, i, Color.white);
				}
			}
			mBlankTex.Apply();
		}
		if ((Object)mCaret == (Object)null)
		{
			mCaret = NGUITools.AddWidget<UITexture>(label.cachedGameObject);
			mCaret.name = "Input Caret";
			mCaret.mainTexture = mBlankTex;
			mCaret.fillGeometry = false;
			mCaret.pivot = label.pivot;
			mCaret.SetAnchor(label.cachedTransform);
		}
		mCaret.enabled = is_enable;
		InActiveName();
		Init();
	}

	public void ActiveName()
	{
		SetColor(Color.white);
		isCaretDirection = false;
		mNextBlink = RealTime.time;
	}

	public void InActiveName()
	{
		UILabel component = GetComponent<UILabel>();
		string value = base.value;
		component.text = base.defaultText;
		base.value = string.Empty;
		SetInActiveNameColor();
		if ((Object)mCaret != (Object)null && string.IsNullOrEmpty(value))
		{
			label.PrintOverlay(0, 0, mCaret.geometry, null, caretColor, selectionColor);
		}
		isCaretDirection = true;
		mNextBlink = RealTime.time;
	}

	public void SetInActiveNameColor()
	{
		SetColor(Color.gray);
	}

	protected override void Update()
	{
		base.Update();
		if (isCaretDirection && !base.isSelected && (Object)mCaret != (Object)null && mNextBlink < RealTime.time)
		{
			mNextBlink = RealTime.time + 0.5f;
			mCaret.enabled = !mCaret.enabled;
		}
	}
}
