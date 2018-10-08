using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button Message (Legacy)")]
public class UIButtonMessage
{
	public enum Trigger
	{
		OnClick,
		OnMouseOver,
		OnMouseOut,
		OnPress,
		OnRelease,
		OnDoubleClick
	}

	public GameObject target;

	public string functionName;

	public Trigger trigger;

	public bool includeChildren;

	private bool mStarted;

	public UIButtonMessage()
		: this()
	{
	}

	private void Start()
	{
		mStarted = true;
	}

	private void OnEnable()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		if (mStarted)
		{
			OnHover(UICamera.IsHighlighted(this.get_gameObject()));
		}
	}

	private void OnHover(bool isOver)
	{
		if (this.get_enabled() && ((isOver && trigger == Trigger.OnMouseOver) || (!isOver && trigger == Trigger.OnMouseOut)))
		{
			Send();
		}
	}

	private void OnPress(bool isPressed)
	{
		if (this.get_enabled() && ((isPressed && trigger == Trigger.OnPress) || (!isPressed && trigger == Trigger.OnRelease)))
		{
			Send();
		}
	}

	private void OnSelect(bool isSelected)
	{
		if (this.get_enabled() && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			OnHover(isSelected);
		}
	}

	private void OnClick()
	{
		if (this.get_enabled() && trigger == Trigger.OnClick)
		{
			Send();
		}
	}

	private void OnDoubleClick()
	{
		if (this.get_enabled() && trigger == Trigger.OnDoubleClick)
		{
			Send();
		}
	}

	private void Send()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		if (!string.IsNullOrEmpty(functionName))
		{
			if (target == null)
			{
				target = this.get_gameObject();
			}
			if (includeChildren)
			{
				Transform[] componentsInChildren = target.GetComponentsInChildren<Transform>();
				int i = 0;
				for (int num = componentsInChildren.Length; i < num; i++)
				{
					Transform val = componentsInChildren[i];
					val.get_gameObject().SendMessage(functionName, (object)this.get_gameObject(), 1);
				}
			}
			else
			{
				target.SendMessage(functionName, (object)this.get_gameObject(), 1);
			}
		}
	}
}
