using UnityEngine;

[AddComponentMenu("NGUI/UI/Localize")]
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
public class UILocalize
{
	public string key;

	private bool mStarted;

	public string value
	{
		set
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected O, but got Unknown
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			if (!string.IsNullOrEmpty(value))
			{
				UIWidget component = this.GetComponent<UIWidget>();
				UILabel uILabel = component as UILabel;
				UISprite uISprite = component as UISprite;
				if (uILabel != null)
				{
					UIInput uIInput = NGUITools.FindInParents<UIInput>(uILabel.get_gameObject());
					if (uIInput != null && uIInput.label == uILabel)
					{
						uIInput.defaultText = value;
					}
					else
					{
						uILabel.SetTextOnly(value);
					}
				}
				else if (uISprite != null)
				{
					UIButton uIButton = NGUITools.FindInParents<UIButton>(uISprite.get_gameObject());
					if (uIButton != null && uIButton.tweenTarget == uISprite.get_gameObject())
					{
						uIButton.normalSprite = value;
					}
					uISprite.spriteName = value;
					uISprite.MakePixelPerfect();
				}
			}
		}
	}

	public UILocalize()
		: this()
	{
	}

	private void OnEnable()
	{
		if (mStarted && Localization.dictionary.ContainsKey(key))
		{
			OnLocalize();
		}
	}

	private void Start()
	{
		mStarted = true;
		OnLocalize();
	}

	private void OnLocalize()
	{
		if (Localization.dictionary.ContainsKey(key))
		{
			if (string.IsNullOrEmpty(key))
			{
				UILabel component = this.GetComponent<UILabel>();
				if (component != null)
				{
					key = component.text;
				}
			}
			if (!string.IsNullOrEmpty(key))
			{
				value = Localization.Get(key);
			}
		}
	}
}
