using UnityEngine;

[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/UI/Localize")]
[ExecuteInEditMode]
public class UILocalize : MonoBehaviour
{
	public string key;

	private bool mStarted;

	public string value
	{
		set
		{
			if (!string.IsNullOrEmpty(value))
			{
				UIWidget component = GetComponent<UIWidget>();
				UILabel uILabel = component as UILabel;
				UISprite uISprite = component as UISprite;
				if ((Object)uILabel != (Object)null)
				{
					UIInput uIInput = NGUITools.FindInParents<UIInput>(uILabel.gameObject);
					if ((Object)uIInput != (Object)null && (Object)uIInput.label == (Object)uILabel)
					{
						uIInput.defaultText = value;
					}
					else
					{
						uILabel.SetTextOnly(value);
					}
				}
				else if ((Object)uISprite != (Object)null)
				{
					UIButton uIButton = NGUITools.FindInParents<UIButton>(uISprite.gameObject);
					if ((Object)uIButton != (Object)null && (Object)uIButton.tweenTarget == (Object)uISprite.gameObject)
					{
						uIButton.normalSprite = value;
					}
					uISprite.spriteName = value;
					uISprite.MakePixelPerfect();
				}
			}
		}
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
				UILabel component = GetComponent<UILabel>();
				if ((Object)component != (Object)null)
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
