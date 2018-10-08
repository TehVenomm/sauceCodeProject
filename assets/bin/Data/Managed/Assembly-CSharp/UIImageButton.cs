using UnityEngine;

[AddComponentMenu("NGUI/UI/Image Button")]
public class UIImageButton
{
	public UISprite target;

	public string normalSprite;

	public string hoverSprite;

	public string pressedSprite;

	public string disabledSprite;

	public bool pixelSnap = true;

	public bool isEnabled
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			Collider component = this.get_gameObject().GetComponent<Collider>();
			return Object.op_Implicit(component) && component.get_enabled();
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			Collider component = this.get_gameObject().GetComponent<Collider>();
			if (Object.op_Implicit(component) && component.get_enabled() != value)
			{
				component.set_enabled(value);
				UpdateImage();
			}
		}
	}

	public UIImageButton()
		: this()
	{
	}

	private void OnEnable()
	{
		if (target == null)
		{
			target = this.GetComponentInChildren<UISprite>();
		}
		UpdateImage();
	}

	private void OnValidate()
	{
		if (target != null)
		{
			if (string.IsNullOrEmpty(normalSprite))
			{
				normalSprite = target.spriteName;
			}
			if (string.IsNullOrEmpty(hoverSprite))
			{
				hoverSprite = target.spriteName;
			}
			if (string.IsNullOrEmpty(pressedSprite))
			{
				pressedSprite = target.spriteName;
			}
			if (string.IsNullOrEmpty(disabledSprite))
			{
				disabledSprite = target.spriteName;
			}
		}
	}

	private void UpdateImage()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		if (target != null)
		{
			if (isEnabled)
			{
				SetSprite((!UICamera.IsHighlighted(this.get_gameObject())) ? normalSprite : hoverSprite);
			}
			else
			{
				SetSprite(disabledSprite);
			}
		}
	}

	private void OnHover(bool isOver)
	{
		if (isEnabled && target != null)
		{
			SetSprite((!isOver) ? normalSprite : hoverSprite);
		}
	}

	private void OnPress(bool pressed)
	{
		if (pressed)
		{
			SetSprite(pressedSprite);
		}
		else
		{
			UpdateImage();
		}
	}

	private void SetSprite(string sprite)
	{
		if (!(target.atlas == null) && target.atlas.GetSprite(sprite) != null)
		{
			target.spriteName = sprite;
			if (pixelSnap)
			{
				target.MakePixelPerfect();
			}
		}
	}
}
