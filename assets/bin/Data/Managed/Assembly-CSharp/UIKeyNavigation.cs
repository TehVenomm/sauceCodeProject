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
			if ((UnityEngine.Object)hoveredObject == (UnityEngine.Object)null)
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
			if (base.enabled && base.gameObject.activeInHierarchy)
			{
				Collider component = GetComponent<Collider>();
				if ((UnityEngine.Object)component != (UnityEngine.Object)null)
				{
					return component.enabled;
				}
				Collider2D component2 = GetComponent<Collider2D>();
				return (UnityEngine.Object)component2 != (UnityEngine.Object)null && component2.enabled;
			}
			return false;
		}
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
			UICamera.hoveredObject = base.gameObject;
		}
	}

	protected virtual void OnDisable()
	{
		list.Remove(this);
	}

	private static bool IsActive(GameObject go)
	{
		if ((bool)go && go.activeInHierarchy)
		{
			Collider component = go.GetComponent<Collider>();
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				return component.enabled;
			}
			Collider2D component2 = go.GetComponent<Collider2D>();
			return (UnityEngine.Object)component2 != (UnityEngine.Object)null && component2.enabled;
		}
		return false;
	}

	public GameObject GetLeft()
	{
		if (IsActive(onLeft))
		{
			return onLeft;
		}
		if (constraint == Constraint.Vertical || constraint == Constraint.Explicit)
		{
			return null;
		}
		return Get(Vector3.left, 1f, 2f);
	}

	public GameObject GetRight()
	{
		if (IsActive(onRight))
		{
			return onRight;
		}
		if (constraint == Constraint.Vertical || constraint == Constraint.Explicit)
		{
			return null;
		}
		return Get(Vector3.right, 1f, 2f);
	}

	public GameObject GetUp()
	{
		if (IsActive(onUp))
		{
			return onUp;
		}
		if (constraint == Constraint.Horizontal || constraint == Constraint.Explicit)
		{
			return null;
		}
		return Get(Vector3.up, 2f, 1f);
	}

	public GameObject GetDown()
	{
		if (IsActive(onDown))
		{
			return onDown;
		}
		if (constraint == Constraint.Horizontal || constraint == Constraint.Explicit)
		{
			return null;
		}
		return Get(Vector3.down, 2f, 1f);
	}

	public GameObject Get(Vector3 myDir, float x = 1f, float y = 1f)
	{
		Transform transform = base.transform;
		myDir = transform.TransformDirection(myDir);
		Vector3 center = GetCenter(base.gameObject);
		float num = 3.40282347E+38f;
		GameObject result = null;
		for (int i = 0; i < list.size; i++)
		{
			UIKeyNavigation uIKeyNavigation = list[i];
			if (!((UnityEngine.Object)uIKeyNavigation == (UnityEngine.Object)this) && uIKeyNavigation.constraint != Constraint.Explicit && uIKeyNavigation.isColliderEnabled)
			{
				UIWidget component = uIKeyNavigation.GetComponent<UIWidget>();
				if (!((UnityEngine.Object)component != (UnityEngine.Object)null) || component.alpha != 0f)
				{
					Vector3 direction = GetCenter(uIKeyNavigation.gameObject) - center;
					float num2 = Vector3.Dot(myDir, direction.normalized);
					if (!(num2 < 0.707f))
					{
						direction = transform.InverseTransformDirection(direction);
						direction.x *= x;
						direction.y *= y;
						float sqrMagnitude = direction.sqrMagnitude;
						if (!(sqrMagnitude > num))
						{
							result = uIKeyNavigation.gameObject;
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
		UIWidget component = go.GetComponent<UIWidget>();
		UICamera uICamera = UICamera.FindCameraForLayer(go.layer);
		if ((UnityEngine.Object)uICamera != (UnityEngine.Object)null)
		{
			Vector3 vector = go.transform.position;
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				Vector3[] worldCorners = component.worldCorners;
				vector = (worldCorners[0] + worldCorners[2]) * 0.5f;
			}
			vector = uICamera.cachedCamera.WorldToScreenPoint(vector);
			vector.z = 0f;
			return vector;
		}
		if ((UnityEngine.Object)component != (UnityEngine.Object)null)
		{
			Vector3[] worldCorners2 = component.worldCorners;
			return (worldCorners2[0] + worldCorners2[2]) * 0.5f;
		}
		return go.transform.position;
	}

	public virtual void OnNavigate(KeyCode key)
	{
		if (!UIPopupList.isOpen)
		{
			GameObject gameObject = null;
			switch (key)
			{
			case KeyCode.LeftArrow:
				gameObject = GetLeft();
				break;
			case KeyCode.RightArrow:
				gameObject = GetRight();
				break;
			case KeyCode.UpArrow:
				gameObject = GetUp();
				break;
			case KeyCode.DownArrow:
				gameObject = GetDown();
				break;
			}
			if ((UnityEngine.Object)gameObject != (UnityEngine.Object)null)
			{
				UICamera.hoveredObject = gameObject;
			}
		}
	}

	public virtual void OnKey(KeyCode key)
	{
		if (key == KeyCode.Tab)
		{
			GameObject gameObject = onTab;
			if ((UnityEngine.Object)gameObject == (UnityEngine.Object)null)
			{
				if (UICamera.GetKey(KeyCode.LeftShift) || UICamera.GetKey(KeyCode.RightShift))
				{
					gameObject = GetLeft();
					if ((UnityEngine.Object)gameObject == (UnityEngine.Object)null)
					{
						gameObject = GetUp();
					}
					if ((UnityEngine.Object)gameObject == (UnityEngine.Object)null)
					{
						gameObject = GetDown();
					}
					if ((UnityEngine.Object)gameObject == (UnityEngine.Object)null)
					{
						gameObject = GetRight();
					}
				}
				else
				{
					gameObject = GetRight();
					if ((UnityEngine.Object)gameObject == (UnityEngine.Object)null)
					{
						gameObject = GetDown();
					}
					if ((UnityEngine.Object)gameObject == (UnityEngine.Object)null)
					{
						gameObject = GetUp();
					}
					if ((UnityEngine.Object)gameObject == (UnityEngine.Object)null)
					{
						gameObject = GetLeft();
					}
				}
			}
			if ((UnityEngine.Object)gameObject != (UnityEngine.Object)null)
			{
				UICamera.selectedObject = gameObject;
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
