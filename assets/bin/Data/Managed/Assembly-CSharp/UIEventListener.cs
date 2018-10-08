using UnityEngine;

[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener
{
	public delegate void VoidDelegate(GameObject go);

	public delegate void BoolDelegate(GameObject go, bool state);

	public delegate void FloatDelegate(GameObject go, float delta);

	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	public delegate void ObjectDelegate(GameObject go, GameObject obj);

	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);

	public object parameter;

	public VoidDelegate onSubmit;

	public VoidDelegate onClick;

	public VoidDelegate onDoubleClick;

	public BoolDelegate onHover;

	public BoolDelegate onPress;

	public BoolDelegate onSelect;

	public FloatDelegate onScroll;

	public VoidDelegate onDragStart;

	public VectorDelegate onDrag;

	public VoidDelegate onDragOver;

	public VoidDelegate onDragOut;

	public VoidDelegate onDragEnd;

	public ObjectDelegate onDrop;

	public KeyCodeDelegate onKey;

	public BoolDelegate onTooltip;

	private bool isColliderEnabled
	{
		get
		{
			Collider component = this.GetComponent<Collider>();
			if (component != null)
			{
				return component.get_enabled();
			}
			Collider2D component2 = this.GetComponent<Collider2D>();
			return component2 != null && component2.get_enabled();
		}
	}

	public UIEventListener()
		: this()
	{
	}

	private void OnSubmit()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		if (isColliderEnabled && onSubmit != null)
		{
			onSubmit(this.get_gameObject());
		}
	}

	private void OnClick()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		if (isColliderEnabled && onClick != null)
		{
			onClick(this.get_gameObject());
		}
	}

	private void OnDoubleClick()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		if (isColliderEnabled && onDoubleClick != null)
		{
			onDoubleClick(this.get_gameObject());
		}
	}

	private void OnHover(bool isOver)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		if (isColliderEnabled && onHover != null)
		{
			onHover(this.get_gameObject(), isOver);
		}
	}

	private void OnPress(bool isPressed)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		if (isColliderEnabled && onPress != null)
		{
			onPress(this.get_gameObject(), isPressed);
		}
	}

	private void OnSelect(bool selected)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		if (isColliderEnabled && onSelect != null)
		{
			onSelect(this.get_gameObject(), selected);
		}
	}

	private void OnScroll(float delta)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		if (isColliderEnabled && onScroll != null)
		{
			onScroll(this.get_gameObject(), delta);
		}
	}

	private void OnDragStart()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		if (onDragStart != null)
		{
			onDragStart(this.get_gameObject());
		}
	}

	private void OnDrag(Vector2 delta)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		if (onDrag != null)
		{
			onDrag(this.get_gameObject(), delta);
		}
	}

	private void OnDragOver()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		if (isColliderEnabled && onDragOver != null)
		{
			onDragOver(this.get_gameObject());
		}
	}

	private void OnDragOut()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		if (isColliderEnabled && onDragOut != null)
		{
			onDragOut(this.get_gameObject());
		}
	}

	private void OnDragEnd()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		if (onDragEnd != null)
		{
			onDragEnd(this.get_gameObject());
		}
	}

	private void OnDrop(GameObject go)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		if (isColliderEnabled && onDrop != null)
		{
			onDrop(this.get_gameObject(), go);
		}
	}

	private void OnKey(KeyCode key)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		if (isColliderEnabled && onKey != null)
		{
			onKey(this.get_gameObject(), key);
		}
	}

	private void OnTooltip(bool show)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		if (isColliderEnabled && onTooltip != null)
		{
			onTooltip(this.get_gameObject(), show);
		}
	}

	public static UIEventListener Get(GameObject go)
	{
		UIEventListener uIEventListener = go.GetComponent<UIEventListener>();
		if (uIEventListener == null)
		{
			uIEventListener = go.AddComponent<UIEventListener>();
		}
		return uIEventListener;
	}
}
