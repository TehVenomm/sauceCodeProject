using System;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Key Navigation")]
public class UIKeyNavigation
{
	public enum Constraint
	{
		None,
		Vertical,
		Horizontal,
		Explicit
	}

	public static BetterList<UIKeyNavigation> list = new BetterList<UIKeyNavigation>();

	public Constraint constraint;

	public GameObject onUp;

	public GameObject onDown;

	public GameObject onLeft;

	public GameObject onRight;

	public GameObject onClick;

	public GameObject onTab;

	public bool startsSelected;

	[NonSerialized]
	private bool mStarted;

	public static UIKeyNavigation current
	{
		get
		{
			GameObject hoveredObject = UICamera.hoveredObject;
			if (hoveredObject == null)
			{
				return null;
			}
			return hoveredObject.GetComponent<UIKeyNavigation>();
		}
	}

	public bool isColliderEnabled
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			if (this.get_enabled() && this.get_gameObject().get_activeInHierarchy())
			{
				Collider component = this.GetComponent<Collider>();
				if (component != null)
				{
					return component.get_enabled();
				}
				Collider2D component2 = this.GetComponent<Collider2D>();
				return component2 != null && component2.get_enabled();
			}
			return false;
		}
	}

	public UIKeyNavigation()
		: this()
	{
	}

	protected virtual void OnEnable()
	{
		list.Add(this);
		if (mStarted)
		{
			Start();
		}
	}

	private void Start()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		mStarted = true;
		if (startsSelected && isColliderEnabled)
		{
			UICamera.hoveredObject = this.get_gameObject();
		}
	}

	protected virtual void OnDisable()
	{
		list.Remove(this);
	}

	private static bool IsActive(GameObject go)
	{
		if (Object.op_Implicit(go) && go.get_activeInHierarchy())
		{
			Collider component = go.GetComponent<Collider>();
			if (component != null)
			{
				return component.get_enabled();
			}
			Collider2D component2 = go.GetComponent<Collider2D>();
			return component2 != null && component2.get_enabled();
		}
		return false;
	}

	public GameObject GetLeft()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (IsActive(onLeft))
		{
			return onLeft;
		}
		if (constraint == Constraint.Vertical || constraint == Constraint.Explicit)
		{
			return null;
		}
		return Get(Vector3.get_left(), 1f, 2f);
	}

	public GameObject GetRight()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (IsActive(onRight))
		{
			return onRight;
		}
		if (constraint == Constraint.Vertical || constraint == Constraint.Explicit)
		{
			return null;
		}
		return Get(Vector3.get_right(), 1f, 2f);
	}

	public GameObject GetUp()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (IsActive(onUp))
		{
			return onUp;
		}
		if (constraint == Constraint.Horizontal || constraint == Constraint.Explicit)
		{
			return null;
		}
		return Get(Vector3.get_up(), 2f, 1f);
	}

	public GameObject GetDown()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (IsActive(onDown))
		{
			return onDown;
		}
		if (constraint == Constraint.Horizontal || constraint == Constraint.Explicit)
		{
			return null;
		}
		return Get(Vector3.get_down(), 2f, 1f);
	}

	public GameObject Get(Vector3 myDir, float x = 1f, float y = 1f)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		Transform val = this.get_transform();
		myDir = val.TransformDirection(myDir);
		Vector3 center = GetCenter(this.get_gameObject());
		float num = 3.40282347E+38f;
		GameObject result = null;
		for (int i = 0; i < list.size; i++)
		{
			UIKeyNavigation uIKeyNavigation = list[i];
			if (!(uIKeyNavigation == this) && uIKeyNavigation.constraint != Constraint.Explicit && uIKeyNavigation.isColliderEnabled)
			{
				UIWidget component = uIKeyNavigation.GetComponent<UIWidget>();
				if (!(component != null) || component.alpha != 0f)
				{
					Vector3 val2 = GetCenter(uIKeyNavigation.get_gameObject()) - center;
					float num2 = Vector3.Dot(myDir, val2.get_normalized());
					if (!(num2 < 0.707f))
					{
						val2 = val.InverseTransformDirection(val2);
						val2.x *= x;
						val2.y *= y;
						float sqrMagnitude = val2.get_sqrMagnitude();
						if (!(sqrMagnitude > num))
						{
							result = uIKeyNavigation.get_gameObject();
							num = sqrMagnitude;
						}
					}
				}
			}
		}
		return result;
	}

	protected static Vector3 GetCenter(GameObject go)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		UIWidget component = go.GetComponent<UIWidget>();
		UICamera uICamera = UICamera.FindCameraForLayer(go.get_layer());
		if (uICamera != null)
		{
			Vector3 val = go.get_transform().get_position();
			if (component != null)
			{
				Vector3[] worldCorners = component.worldCorners;
				val = (worldCorners[0] + worldCorners[2]) * 0.5f;
			}
			val = uICamera.cachedCamera.WorldToScreenPoint(val);
			val.z = 0f;
			return val;
		}
		if (component != null)
		{
			Vector3[] worldCorners2 = component.worldCorners;
			return (worldCorners2[0] + worldCorners2[2]) * 0.5f;
		}
		return go.get_transform().get_position();
	}

	public virtual void OnNavigate(KeyCode key)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected I4, but got Unknown
		if (!UIPopupList.isOpen)
		{
			GameObject val = null;
			switch (key - 273)
			{
			case 3:
				val = GetLeft();
				break;
			case 2:
				val = GetRight();
				break;
			case 0:
				val = GetUp();
				break;
			case 1:
				val = GetDown();
				break;
			}
			if (val != null)
			{
				UICamera.hoveredObject = val;
			}
		}
	}

	public virtual void OnKey(KeyCode key)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Invalid comparison between Unknown and I4
		if ((int)key == 9)
		{
			GameObject val = onTab;
			if (val == null)
			{
				if (UICamera.GetKey(304) || UICamera.GetKey(303))
				{
					val = GetLeft();
					if (val == null)
					{
						val = GetUp();
					}
					if (val == null)
					{
						val = GetDown();
					}
					if (val == null)
					{
						val = GetRight();
					}
				}
				else
				{
					val = GetRight();
					if (val == null)
					{
						val = GetDown();
					}
					if (val == null)
					{
						val = GetUp();
					}
					if (val == null)
					{
						val = GetLeft();
					}
				}
			}
			if (val != null)
			{
				UICamera.selectedObject = val;
			}
		}
	}

	protected virtual void OnClick()
	{
		if (NGUITools.GetActive(onClick))
		{
			UICamera.hoveredObject = onClick;
		}
	}
}
