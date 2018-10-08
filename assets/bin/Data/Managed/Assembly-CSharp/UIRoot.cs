using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Root")]
public class UIRoot
{
	public enum Scaling
	{
		Flexible,
		Constrained,
		ConstrainedOnMobiles
	}

	public enum Constraint
	{
		Fit,
		Fill,
		FitWidth,
		FitHeight
	}

	public static List<UIRoot> list = new List<UIRoot>();

	public Scaling scalingStyle;

	public int manualWidth = 1280;

	public int manualHeight = 720;

	public int minimumHeight = 320;

	public int maximumHeight = 1536;

	public bool fitWidth;

	public bool fitHeight = true;

	public bool adjustByDPI;

	public bool shrinkPortraitUI;

	private Transform mTrans;

	public Constraint constraint
	{
		get
		{
			if (fitWidth)
			{
				if (fitHeight)
				{
					return Constraint.Fit;
				}
				return Constraint.FitWidth;
			}
			if (fitHeight)
			{
				return Constraint.FitHeight;
			}
			return Constraint.Fill;
		}
	}

	public Scaling activeScaling
	{
		get
		{
			Scaling scaling = scalingStyle;
			if (scaling == Scaling.ConstrainedOnMobiles)
			{
				return Scaling.Constrained;
			}
			return scaling;
		}
	}

	public int activeHeight
	{
		get
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			if (activeScaling == Scaling.Flexible)
			{
				Vector2 screenSize = NGUITools.screenSize;
				float num = screenSize.x / screenSize.y;
				if (screenSize.y < (float)minimumHeight)
				{
					screenSize.y = (float)minimumHeight;
					screenSize.x = screenSize.y * num;
				}
				else if (screenSize.y > (float)maximumHeight)
				{
					screenSize.y = (float)maximumHeight;
					screenSize.x = screenSize.y * num;
				}
				int num2 = Mathf.RoundToInt((!shrinkPortraitUI || !(screenSize.y > screenSize.x)) ? screenSize.y : (screenSize.y / num));
				return (!adjustByDPI) ? num2 : NGUIMath.AdjustByDPI((float)num2);
			}
			Constraint constraint = this.constraint;
			if (constraint != Constraint.FitHeight)
			{
				Vector2 screenSize2 = NGUITools.screenSize;
				float num3 = screenSize2.x / screenSize2.y;
				float num4 = (float)manualWidth / (float)manualHeight;
				switch (constraint)
				{
				case Constraint.FitWidth:
					return Mathf.RoundToInt((float)manualWidth / num3);
				case Constraint.Fit:
					return (!(num4 > num3)) ? manualHeight : Mathf.RoundToInt((float)manualWidth / num3);
				case Constraint.Fill:
					return (!(num4 < num3)) ? manualHeight : Mathf.RoundToInt((float)manualWidth / num3);
				default:
					return manualHeight;
				}
			}
			return manualHeight;
		}
	}

	public float pixelSizeAdjustment
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			Vector2 screenSize = NGUITools.screenSize;
			int num = Mathf.RoundToInt(screenSize.y);
			return (num != -1) ? GetPixelSizeAdjustment(num) : 1f;
		}
	}

	public UIRoot()
		: this()
	{
	}

	public static float GetPixelSizeAdjustment(GameObject go)
	{
		UIRoot uIRoot = NGUITools.FindInParents<UIRoot>(go);
		return (!(uIRoot != null)) ? 1f : uIRoot.pixelSizeAdjustment;
	}

	public float GetPixelSizeAdjustment(int height)
	{
		height = Mathf.Max(2, height);
		if (activeScaling == Scaling.Constrained)
		{
			return (float)activeHeight / (float)height;
		}
		if (height < minimumHeight)
		{
			return (float)minimumHeight / (float)height;
		}
		if (height > maximumHeight)
		{
			return (float)maximumHeight / (float)height;
		}
		return 1f;
	}

	protected virtual void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		mTrans = this.get_transform();
	}

	protected virtual void OnEnable()
	{
		list.Add(this);
	}

	protected virtual void OnDisable()
	{
		list.Remove(this);
	}

	protected virtual void Start()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		UIOrthoCamera componentInChildren = this.GetComponentInChildren<UIOrthoCamera>();
		if (componentInChildren != null)
		{
			Debug.LogWarning((object)"UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", componentInChildren);
			Camera component = componentInChildren.get_gameObject().GetComponent<Camera>();
			componentInChildren.set_enabled(false);
			if (component != null)
			{
				component.set_orthographicSize(1f);
			}
		}
		else
		{
			UpdateScale(false);
		}
	}

	private void Update()
	{
		UpdateScale(true);
	}

	public void UpdateScale(bool updateAnchors = true)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		if (mTrans != null)
		{
			float num = (float)activeHeight;
			if (num > 0f)
			{
				float num2 = 2f / num;
				Vector3 localScale = mTrans.get_localScale();
				if (!(Mathf.Abs(localScale.x - num2) <= 1.401298E-45f) || !(Mathf.Abs(localScale.y - num2) <= 1.401298E-45f) || !(Mathf.Abs(localScale.z - num2) <= 1.401298E-45f))
				{
					mTrans.set_localScale(new Vector3(num2, num2, num2));
					if (updateAnchors)
					{
						this.BroadcastMessage("UpdateAnchors");
					}
				}
			}
		}
	}

	public static void Broadcast(string funcName)
	{
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			UIRoot uIRoot = list[i];
			if (uIRoot != null)
			{
				uIRoot.BroadcastMessage(funcName, 1);
			}
		}
	}

	public static void Broadcast(string funcName, object param)
	{
		if (param == null)
		{
			Debug.LogError((object)"SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
		}
		else
		{
			int i = 0;
			for (int count = list.Count; i < count; i++)
			{
				UIRoot uIRoot = list[i];
				if (uIRoot != null)
				{
					uIRoot.BroadcastMessage(funcName, param, 1);
				}
			}
		}
	}
}
