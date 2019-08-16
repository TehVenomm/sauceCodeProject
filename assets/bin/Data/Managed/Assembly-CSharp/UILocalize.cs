using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/UI/Localize")]
public class UILocalize : MonoBehaviour
{
	public string key;

	private bool mStarted;

	public string value
	{
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
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
		if (!Localization.dictionary.ContainsKey(key))
		{
			return;
		}
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
