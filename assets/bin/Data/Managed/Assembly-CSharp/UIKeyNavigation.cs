using System;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Key Navigation")]
public class UIKeyNavigation : MonoBehaviour
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
		return Get(Vector3.get_up(), 2f);
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
		return Get(Vector3.get_down(), 2f);
	}

	public GameObject Get(Vector3 myDir, float x = 1f, float y = 1f)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		Transform transform = this.get_transform();
		myDir = transform.TransformDirection(myDir);
		Vector3 center = GetCenter(this.get_gameObject());
		float num = float.MaxValue;
		GameObject result = null;
		for (int i = 0; i < list.size; i++)
		{
			UIKeyNavigation uIKeyNavigation = list[i];
			if (uIKeyNavigation == this || uIKeyNavigation.constraint == Constraint.Explicit || !uIKeyNavigation.isColliderEnabled)
			{
				continue;
			}
			UIWidget component = uIKeyNavigation.GetComponent<UIWidget>();
			if (component != null && component.alpha == 0f)
			{
				continue;
			}
			Vector3 val = GetCenter(uIKeyNavigation.get_gameObject()) - center;
			float num2 = Vector3.Dot(myDir, val.get_normalized());
			if (!(num2 < 0.707f))
			{
				val = transform.InverseTransformDirection(val);
				val.x *= x;
				val.y *= y;
				float sqrMagnitude = val.get_sqrMagnitude();
				if (!(sqrMagnitude > num))
				{
					result = uIKeyNavigation.get_gameObject();
					num = sqrMagnitude;
				}
			}
		}
		return result;
	}

	protected static Vector3 GetCenter(GameObject go)
	{
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
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected I4, but got Unknown
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
		if ((int)key != 9)
		{
			return;
		}
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

	protected virtual void OnClick()
	{
		if (NGUITools.GetActive(onClick))
		{
			UICamera.hoveredObject = onClick;
		}
	}
}
