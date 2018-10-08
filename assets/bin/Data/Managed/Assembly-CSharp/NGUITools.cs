using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;

public static class NGUITools
{
	private static AudioListener mListener;

	private static bool mLoaded = false;

	private static float mGlobalVolume = 1f;

	private static float mLastTimestamp = 0f;

	private static AudioClip mLastClip;

	private static Vector3[] mSides = (Vector3[])new Vector3[4];

	public static KeyCode[] keys = (KeyCode[])new KeyCode[145]
	{
		8,
		9,
		12,
		13,
		19,
		27,
		32,
		33,
		34,
		35,
		36,
		38,
		39,
		40,
		41,
		42,
		43,
		44,
		45,
		46,
		47,
		48,
		49,
		50,
		51,
		52,
		53,
		54,
		55,
		56,
		57,
		58,
		59,
		60,
		61,
		62,
		63,
		64,
		91,
		92,
		93,
		94,
		95,
		96,
		97,
		98,
		99,
		100,
		101,
		102,
		103,
		104,
		105,
		106,
		107,
		108,
		109,
		110,
		111,
		112,
		113,
		114,
		115,
		116,
		117,
		118,
		119,
		120,
		121,
		122,
		127,
		256,
		257,
		258,
		259,
		260,
		261,
		262,
		263,
		264,
		265,
		266,
		267,
		268,
		269,
		270,
		271,
		272,
		273,
		274,
		275,
		276,
		277,
		278,
		279,
		280,
		281,
		282,
		283,
		284,
		285,
		286,
		287,
		288,
		289,
		290,
		291,
		292,
		293,
		294,
		295,
		296,
		300,
		301,
		302,
		303,
		304,
		305,
		306,
		307,
		308,
		326,
		327,
		328,
		329,
		330,
		331,
		332,
		333,
		334,
		335,
		336,
		337,
		338,
		339,
		340,
		341,
		342,
		343,
		344,
		345,
		346,
		347,
		348,
		349
	};

	public static float soundVolume
	{
		get
		{
			if (!mLoaded)
			{
				mLoaded = true;
				mGlobalVolume = PlayerPrefs.GetFloat("Sound", 1f);
			}
			return mGlobalVolume;
		}
		set
		{
			if (mGlobalVolume != value)
			{
				mLoaded = true;
				mGlobalVolume = value;
				PlayerPrefs.SetFloat("Sound", value);
			}
		}
	}

	public static bool fileAccess => (int)Application.get_platform() != 5 && (int)Application.get_platform() != 3;

	public static string clipboard
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Expected O, but got Unknown
			TextEditor val = new TextEditor();
			val.Paste();
			return val.get_text();
		}
		set
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Expected O, but got Unknown
			TextEditor val = new TextEditor();
			val.set_text(value);
			val.OnFocus();
			val.Copy();
		}
	}

	public static Vector2 screenSize => new Vector2((float)Screen.get_width(), (float)Screen.get_height());

	public static AudioSource PlaySound(AudioClip clip)
	{
		return PlaySound(clip, 1f, 1f);
	}

	public static AudioSource PlaySound(AudioClip clip, float volume)
	{
		return PlaySound(clip, volume, 1f);
	}

	public static AudioSource PlaySound(AudioClip clip, float volume, float pitch)
	{
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		float time = RealTime.time;
		if (mLastClip == clip && mLastTimestamp + 0.1f > time)
		{
			return null;
		}
		mLastClip = clip;
		mLastTimestamp = time;
		volume *= soundVolume;
		if (clip != null && volume > 0.01f)
		{
			if (mListener == null || !NGUITools.GetActive(mListener))
			{
				AudioListener[] array = Object.FindObjectsOfType(typeof(AudioListener)) as AudioListener[];
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (NGUITools.GetActive(array[i]))
						{
							mListener = array[i];
							break;
						}
					}
				}
				if (mListener == null)
				{
					Camera val = Camera.get_main();
					if (val == null)
					{
						val = (Object.FindObjectOfType(typeof(Camera)) as Camera);
					}
					if (val != null)
					{
						mListener = val.get_gameObject().AddComponent<AudioListener>();
					}
				}
			}
			if (mListener != null && mListener.get_enabled() && NGUITools.GetActive(mListener.get_gameObject()))
			{
				AudioSource val2 = mListener.GetComponent<AudioSource>();
				if (val2 == null)
				{
					val2 = mListener.get_gameObject().AddComponent<AudioSource>();
				}
				val2.set_priority(50);
				val2.set_pitch(pitch);
				val2.PlayOneShot(clip, volume);
				return val2;
			}
		}
		return null;
	}

	public static int RandomRange(int min, int max)
	{
		if (min == max)
		{
			return min;
		}
		return Random.Range(min, max + 1);
	}

	public static string GetHierarchy(GameObject obj)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (obj == null)
		{
			return string.Empty;
		}
		string text = obj.get_name();
		while (obj.get_transform().get_parent() != null)
		{
			obj = obj.get_transform().get_parent().get_gameObject();
			text = obj.get_name() + "\\" + text;
		}
		return text;
	}

	public static T[] FindActive<T>() where T : Component
	{
		return Object.FindObjectsOfType(typeof(T)) as T[];
	}

	public static Camera FindCameraForLayer(int layer)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		int num = 1 << layer;
		Camera cachedCamera;
		for (int i = 0; i < UICamera.list.size; i++)
		{
			cachedCamera = UICamera.list.buffer[i].cachedCamera;
			if (Object.op_Implicit(cachedCamera) && (cachedCamera.get_cullingMask() & num) != 0)
			{
				return cachedCamera;
			}
		}
		cachedCamera = Camera.get_main();
		if (Object.op_Implicit(cachedCamera) && (cachedCamera.get_cullingMask() & num) != 0)
		{
			return cachedCamera;
		}
		Camera[] array = (Camera[])new Camera[Camera.get_allCamerasCount()];
		int allCameras = Camera.GetAllCameras(array);
		for (int j = 0; j < allCameras; j++)
		{
			cachedCamera = array[j];
			if (Object.op_Implicit(cachedCamera) && cachedCamera.get_enabled() && (cachedCamera.get_cullingMask() & num) != 0)
			{
				return cachedCamera;
			}
		}
		return null;
	}

	public static void AddWidgetCollider(GameObject go)
	{
		AddWidgetCollider(go, false);
	}

	public static void AddWidgetCollider(GameObject go, bool considerInactive)
	{
		if (go != null)
		{
			Collider component = go.GetComponent<Collider>();
			BoxCollider val = component as BoxCollider;
			if (val != null)
			{
				UpdateWidgetCollider(val, considerInactive);
			}
			else if (!(component != null))
			{
				BoxCollider2D component2 = go.GetComponent<BoxCollider2D>();
				if (component2 != null)
				{
					UpdateWidgetCollider(component2, considerInactive);
				}
				else
				{
					UICamera uICamera = UICamera.FindCameraForLayer(go.get_layer());
					if (uICamera != null && (uICamera.eventType == UICamera.EventType.World_2D || uICamera.eventType == UICamera.EventType.UI_2D))
					{
						component2 = go.AddComponent<BoxCollider2D>();
						component2.set_isTrigger(true);
						UIWidget component3 = go.GetComponent<UIWidget>();
						if (component3 != null)
						{
							component3.autoResizeBoxCollider = true;
						}
						UpdateWidgetCollider(component2, considerInactive);
					}
					else
					{
						val = go.AddComponent<BoxCollider>();
						val.set_isTrigger(true);
						UIWidget component4 = go.GetComponent<UIWidget>();
						if (component4 != null)
						{
							component4.autoResizeBoxCollider = true;
						}
						UpdateWidgetCollider(val, considerInactive);
					}
				}
			}
		}
	}

	public static void UpdateWidgetCollider(GameObject go)
	{
		UpdateWidgetCollider(go, false);
	}

	public static void UpdateWidgetCollider(GameObject go, bool considerInactive)
	{
		if (go != null)
		{
			BoxCollider component = go.GetComponent<BoxCollider>();
			if (component != null)
			{
				UpdateWidgetCollider(component, considerInactive);
			}
			else
			{
				BoxCollider2D component2 = go.GetComponent<BoxCollider2D>();
				if (component2 != null)
				{
					UpdateWidgetCollider(component2, considerInactive);
				}
			}
		}
	}

	public static void UpdateWidgetCollider(BoxCollider box, bool considerInactive)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		if (box != null)
		{
			GameObject val = box.get_gameObject();
			UIWidget component = val.GetComponent<UIWidget>();
			if (component != null)
			{
				Vector4 drawRegion = component.drawRegion;
				if (drawRegion.x != 0f || drawRegion.y != 0f || drawRegion.z != 1f || drawRegion.w != 1f)
				{
					Vector4 drawingDimensions = component.drawingDimensions;
					box.set_center(new Vector3((drawingDimensions.x + drawingDimensions.z) * 0.5f, (drawingDimensions.y + drawingDimensions.w) * 0.5f));
					box.set_size(new Vector3(drawingDimensions.z - drawingDimensions.x, drawingDimensions.w - drawingDimensions.y));
				}
				else
				{
					Vector3[] localCorners = component.localCorners;
					box.set_center(Vector3.Lerp(localCorners[0], localCorners[2], 0.5f));
					box.set_size(localCorners[2] - localCorners[0]);
				}
			}
			else
			{
				Bounds val2 = NGUIMath.CalculateRelativeWidgetBounds(val.get_transform(), considerInactive);
				box.set_center(val2.get_center());
				Vector3 size = val2.get_size();
				float x = size.x;
				Vector3 size2 = val2.get_size();
				box.set_size(new Vector3(x, size2.y, 0f));
			}
		}
	}

	public static void UpdateWidgetCollider(BoxCollider2D box, bool considerInactive)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		if (box != null)
		{
			GameObject val = box.get_gameObject();
			UIWidget component = val.GetComponent<UIWidget>();
			if (component != null)
			{
				Vector3[] localCorners = component.localCorners;
				box.set_offset(Vector2.op_Implicit(Vector3.Lerp(localCorners[0], localCorners[2], 0.5f)));
				box.set_size(Vector2.op_Implicit(localCorners[2] - localCorners[0]));
			}
			else
			{
				Bounds val2 = NGUIMath.CalculateRelativeWidgetBounds(val.get_transform(), considerInactive);
				box.set_offset(Vector2.op_Implicit(val2.get_center()));
				Vector3 size = val2.get_size();
				float x = size.x;
				Vector3 size2 = val2.get_size();
				box.set_size(new Vector2(x, size2.y));
			}
		}
	}

	public static string GetTypeName<T>()
	{
		string text = typeof(T).ToString();
		if (text.StartsWith("UI"))
		{
			text = text.Substring(2);
		}
		else if (text.StartsWith("UnityEngine."))
		{
			text = text.Substring(12);
		}
		return text;
	}

	public static string GetTypeName(Object obj)
	{
		if (obj == null)
		{
			return "Null";
		}
		string text = ((object)obj).GetType().ToString();
		if (text.StartsWith("UI"))
		{
			text = text.Substring(2);
		}
		else if (text.StartsWith("UnityEngine."))
		{
			text = text.Substring(12);
		}
		return text;
	}

	public static void RegisterUndo(Object obj, string name)
	{
	}

	public static void SetDirty(Object obj)
	{
	}

	public static GameObject AddChild(GameObject parent)
	{
		return AddChild(parent, true);
	}

	public static GameObject AddChild(GameObject parent, bool undo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = new GameObject();
		if (parent != null)
		{
			Transform val2 = val.get_transform();
			val2.set_parent(parent.get_transform());
			val2.set_localPosition(Vector3.get_zero());
			val2.set_localRotation(Quaternion.get_identity());
			val2.set_localScale(Vector3.get_one());
			val.set_layer(parent.get_layer());
		}
		return val;
	}

	public static GameObject AddChild(GameObject parent, GameObject prefab)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = Object.Instantiate<GameObject>(prefab);
		if (val != null && parent != null)
		{
			Transform val2 = val.get_transform();
			val2.set_parent(parent.get_transform());
			val2.set_localPosition(Vector3.get_zero());
			val2.set_localRotation(Quaternion.get_identity());
			val2.set_localScale(Vector3.get_one());
			val.set_layer(parent.get_layer());
		}
		return val;
	}

	public static int CalculateRaycastDepth(GameObject go)
	{
		UIWidget component = go.GetComponent<UIWidget>();
		if (component != null)
		{
			return component.raycastDepth;
		}
		go.GetComponentsInChildren<UIWidget>(Temporary.uiWidgetList);
		if (Temporary.uiWidgetList.Count == 0)
		{
			return 0;
		}
		int num = 2147483647;
		int i = 0;
		for (int count = Temporary.uiWidgetList.Count; i < count; i++)
		{
			if (Temporary.uiWidgetList[i].get_enabled())
			{
				num = Mathf.Min(num, Temporary.uiWidgetList[i].raycastDepth);
			}
		}
		Temporary.uiWidgetList.Clear();
		return num;
	}

	public static int CalculateNextDepth(GameObject go)
	{
		if (Object.op_Implicit(go))
		{
			int num = -1;
			UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
			int i = 0;
			for (int num2 = componentsInChildren.Length; i < num2; i++)
			{
				num = Mathf.Max(num, componentsInChildren[i].depth);
			}
			return num + 1;
		}
		return 0;
	}

	public static int CalculateNextDepth(GameObject go, bool ignoreChildrenWithColliders)
	{
		if (Object.op_Implicit(go) && ignoreChildrenWithColliders)
		{
			int num = -1;
			UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
			int i = 0;
			for (int num2 = componentsInChildren.Length; i < num2; i++)
			{
				UIWidget uIWidget = componentsInChildren[i];
				if (!(uIWidget.cachedGameObject != go) || (!(uIWidget.GetComponent<Collider>() != null) && !(uIWidget.GetComponent<Collider2D>() != null)))
				{
					num = Mathf.Max(num, uIWidget.depth);
				}
			}
			return num + 1;
		}
		return CalculateNextDepth(go);
	}

	public static int AdjustDepth(GameObject go, int adjustment)
	{
		if (go != null)
		{
			UIPanel component = go.GetComponent<UIPanel>();
			if (component != null)
			{
				UIPanel[] componentsInChildren = go.GetComponentsInChildren<UIPanel>(true);
				foreach (UIPanel uIPanel in componentsInChildren)
				{
					uIPanel.depth += adjustment;
				}
				return 1;
			}
			component = NGUITools.FindInParents<UIPanel>(go);
			if (component == null)
			{
				return 0;
			}
			UIWidget[] componentsInChildren2 = go.GetComponentsInChildren<UIWidget>(true);
			int j = 0;
			for (int num = componentsInChildren2.Length; j < num; j++)
			{
				UIWidget uIWidget = componentsInChildren2[j];
				if (!(uIWidget.panel != component))
				{
					uIWidget.depth += adjustment;
				}
			}
			return 2;
		}
		return 0;
	}

	public static void BringForward(GameObject go)
	{
		switch (AdjustDepth(go, 1000))
		{
		case 1:
			NormalizePanelDepths();
			break;
		case 2:
			NormalizeWidgetDepths();
			break;
		}
	}

	public static void PushBack(GameObject go)
	{
		switch (AdjustDepth(go, -1000))
		{
		case 1:
			NormalizePanelDepths();
			break;
		case 2:
			NormalizeWidgetDepths();
			break;
		}
	}

	public static void NormalizeDepths()
	{
		NormalizeWidgetDepths();
		NormalizePanelDepths();
	}

	public static void NormalizeWidgetDepths()
	{
		NormalizeWidgetDepths(NGUITools.FindActive<UIWidget>());
	}

	public static void NormalizeWidgetDepths(GameObject go)
	{
		NormalizeWidgetDepths(go.GetComponentsInChildren<UIWidget>());
	}

	public static void NormalizeWidgetDepths(UIWidget[] list)
	{
		int num = list.Length;
		if (num > 0)
		{
			Array.Sort(list, UIWidget.FullCompareFunc);
			int num2 = 0;
			int depth = list[0].depth;
			for (int i = 0; i < num; i++)
			{
				UIWidget uIWidget = list[i];
				if (uIWidget.depth == depth)
				{
					uIWidget.depth = num2;
				}
				else
				{
					depth = uIWidget.depth;
					num2 = (uIWidget.depth = num2 + 1);
				}
			}
		}
	}

	public static void NormalizePanelDepths()
	{
		UIPanel[] array = NGUITools.FindActive<UIPanel>();
		int num = array.Length;
		if (num > 0)
		{
			Array.Sort(array, UIPanel.CompareFunc);
			int num2 = 0;
			int depth = array[0].depth;
			for (int i = 0; i < num; i++)
			{
				UIPanel uIPanel = array[i];
				if (uIPanel.depth == depth)
				{
					uIPanel.depth = num2;
				}
				else
				{
					depth = uIPanel.depth;
					num2 = (uIPanel.depth = num2 + 1);
				}
			}
		}
	}

	public static UIPanel CreateUI(bool advanced3D)
	{
		return CreateUI(null, advanced3D, -1);
	}

	public static UIPanel CreateUI(bool advanced3D, int layer)
	{
		return CreateUI(null, advanced3D, layer);
	}

	public static UIPanel CreateUI(Transform trans, bool advanced3D, int layer)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Invalid comparison between Unknown and I4
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Invalid comparison between Unknown and I4
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Expected O, but got Unknown
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Expected O, but got Unknown
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Expected O, but got Unknown
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		UIRoot uIRoot = (!(trans != null)) ? null : NGUITools.FindInParents<UIRoot>(trans.get_gameObject());
		if (uIRoot == null && UIRoot.list.Count > 0)
		{
			foreach (UIRoot item in UIRoot.list)
			{
				if (item.get_gameObject().get_layer() == layer)
				{
					uIRoot = item;
					break;
				}
			}
		}
		if (uIRoot == null)
		{
			int i = 0;
			for (int count = UIPanel.list.Count; i < count; i++)
			{
				UIPanel uIPanel = UIPanel.list[i];
				GameObject val = uIPanel.get_gameObject();
				if ((int)val.get_hideFlags() == 0 && val.get_layer() == layer)
				{
					trans.set_parent(uIPanel.get_transform());
					trans.set_localScale(Vector3.get_one());
					return uIPanel;
				}
			}
		}
		if (uIRoot != null)
		{
			UICamera componentInChildren = uIRoot.GetComponentInChildren<UICamera>();
			if (componentInChildren != null && componentInChildren.GetComponent<Camera>().get_orthographic() == advanced3D)
			{
				trans = null;
				uIRoot = null;
			}
		}
		if (uIRoot == null)
		{
			GameObject val2 = AddChild(null, false);
			uIRoot = val2.AddComponent<UIRoot>();
			if (layer == -1)
			{
				layer = LayerMask.NameToLayer("UI");
			}
			if (layer == -1)
			{
				layer = LayerMask.NameToLayer("2D UI");
			}
			val2.set_layer(layer);
			if (advanced3D)
			{
				val2.set_name("UI Root (3D)");
				uIRoot.scalingStyle = UIRoot.Scaling.Constrained;
			}
			else
			{
				val2.set_name("UI Root");
				uIRoot.scalingStyle = UIRoot.Scaling.Flexible;
			}
		}
		UIPanel uIPanel2 = uIRoot.GetComponentInChildren<UIPanel>();
		if (uIPanel2 == null)
		{
			Camera[] array = NGUITools.FindActive<Camera>();
			float num = -1f;
			bool flag = false;
			int num2 = 1 << uIRoot.get_gameObject().get_layer();
			foreach (Camera val3 in array)
			{
				if ((int)val3.get_clearFlags() == 2 || (int)val3.get_clearFlags() == 1)
				{
					flag = true;
				}
				num = Mathf.Max(num, val3.get_depth());
				val3.set_cullingMask(val3.get_cullingMask() & ~num2);
			}
			Camera val4 = NGUITools.AddChild<Camera>(uIRoot.get_gameObject(), false);
			val4.get_gameObject().AddComponent<UICamera>();
			val4.set_clearFlags((!flag) ? 2 : 3);
			val4.set_backgroundColor(Color.get_grey());
			val4.set_cullingMask(num2);
			val4.set_depth(num + 1f);
			if (advanced3D)
			{
				val4.set_nearClipPlane(0.1f);
				val4.set_farClipPlane(4f);
				val4.get_transform().set_localPosition(new Vector3(0f, 0f, -700f));
			}
			else
			{
				val4.set_orthographic(true);
				val4.set_orthographicSize(1f);
				val4.set_nearClipPlane(-10f);
				val4.set_farClipPlane(10f);
			}
			AudioListener[] array2 = NGUITools.FindActive<AudioListener>();
			if (array2 == null || array2.Length == 0)
			{
				val4.get_gameObject().AddComponent<AudioListener>();
			}
			uIPanel2 = uIRoot.get_gameObject().AddComponent<UIPanel>();
		}
		if (trans != null)
		{
			while (trans.get_parent() != null)
			{
				trans = trans.get_parent();
			}
			if (IsChild(trans, uIPanel2.get_transform()))
			{
				uIPanel2 = trans.get_gameObject().AddComponent<UIPanel>();
			}
			else
			{
				trans.set_parent(uIPanel2.get_transform());
				trans.set_localScale(Vector3.get_one());
				trans.set_localPosition(Vector3.get_zero());
				SetChildLayer(uIPanel2.cachedTransform, uIPanel2.cachedGameObject.get_layer());
			}
		}
		return uIPanel2;
	}

	public static void SetChildLayer(Transform t, int layer)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < t.get_childCount(); i++)
		{
			Transform val = t.GetChild(i);
			val.get_gameObject().set_layer(layer);
			SetChildLayer(val, layer);
		}
	}

	public static T AddChild<T>(GameObject parent) where T : Component
	{
		GameObject val = AddChild(parent);
		val.set_name(GetTypeName<T>());
		return val.AddComponent<T>();
	}

	public static T AddChild<T>(GameObject parent, bool undo) where T : Component
	{
		GameObject val = AddChild(parent, undo);
		val.set_name(GetTypeName<T>());
		return val.AddComponent<T>();
	}

	public static T AddWidget<T>(GameObject go) where T : UIWidget
	{
		int depth = CalculateNextDepth(go);
		T result = NGUITools.AddChild<T>(go);
		result.width = 100;
		result.height = 100;
		result.depth = depth;
		return result;
	}

	public static T AddWidget<T>(GameObject go, int depth) where T : UIWidget
	{
		T result = NGUITools.AddChild<T>(go);
		result.width = 100;
		result.height = 100;
		result.depth = depth;
		return result;
	}

	public static UISprite AddSprite(GameObject go, UIAtlas atlas, string spriteName)
	{
		UISpriteData uISpriteData = (!(atlas != null)) ? null : atlas.GetSprite(spriteName);
		UISprite uISprite = AddWidget<UISprite>(go);
		uISprite.type = ((uISpriteData != null && uISpriteData.hasBorder) ? UIBasicSprite.Type.Sliced : UIBasicSprite.Type.Simple);
		uISprite.atlas = atlas;
		uISprite.spriteName = spriteName;
		return uISprite;
	}

	public static GameObject GetRoot(GameObject go)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		Transform val = go.get_transform();
		while (true)
		{
			Transform val2 = val.get_parent();
			if (val2 == null)
			{
				break;
			}
			val = val2;
		}
		return val.get_gameObject();
	}

	public static T FindInParents<T>(GameObject go) where T : Component
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		if (go == null)
		{
			return (T)(object)null;
		}
		T component = go.GetComponent<T>();
		if ((object)component == null)
		{
			Transform val = go.get_transform().get_parent();
			while (val != null && (object)component == null)
			{
				component = val.get_gameObject().GetComponent<T>();
				val = val.get_parent();
			}
		}
		return component;
	}

	public static T FindInParents<T>(Transform trans) where T : Component
	{
		if (trans == null)
		{
			return (T)(object)null;
		}
		return trans.GetComponentInParent<T>();
	}

	public static void Destroy(Object obj)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		if (Object.op_Implicit(obj))
		{
			if (obj is Transform)
			{
				Transform val = obj as Transform;
				GameObject val2 = val.get_gameObject();
				if (Application.get_isPlaying())
				{
					val.set_parent(null);
					Object.Destroy(val2);
				}
				else
				{
					Object.DestroyImmediate(val2);
				}
			}
			else if (obj is GameObject)
			{
				GameObject val3 = obj as GameObject;
				Transform val4 = val3.get_transform();
				if (Application.get_isPlaying())
				{
					val4.set_parent(null);
					Object.Destroy(val3);
				}
				else
				{
					Object.DestroyImmediate(val3);
				}
			}
			else if (Application.get_isPlaying())
			{
				Object.Destroy(obj);
			}
			else
			{
				Object.DestroyImmediate(obj);
			}
		}
	}

	public static void DestroyChildren(this Transform t)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		bool isPlaying = Application.get_isPlaying();
		while (t.get_childCount() != 0)
		{
			Transform val = t.GetChild(0);
			if (isPlaying)
			{
				val.set_parent(null);
				Object.Destroy(val.get_gameObject());
			}
			else
			{
				Object.DestroyImmediate(val.get_gameObject());
			}
		}
	}

	public static void DestroyImmediate(Object obj)
	{
		if (obj != null)
		{
			if (Application.get_isEditor())
			{
				Object.DestroyImmediate(obj);
			}
			else
			{
				Object.Destroy(obj);
			}
		}
	}

	public static void Broadcast(string funcName)
	{
		GameObject[] array = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			array[i].SendMessage(funcName, 1);
		}
	}

	public static void Broadcast(string funcName, object param)
	{
		GameObject[] array = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			array[i].SendMessage(funcName, param, 1);
		}
	}

	public static bool IsChild(Transform parent, Transform child)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		if (parent == null || child == null)
		{
			return false;
		}
		while (child != null)
		{
			if (child == parent)
			{
				return true;
			}
			child = child.get_parent();
		}
		return false;
	}

	private static void Activate(Transform t)
	{
		Activate(t, false);
	}

	private static void Activate(Transform t, bool compatibilityMode)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		SetActiveSelf(t.get_gameObject(), true);
		if (compatibilityMode)
		{
			int i = 0;
			for (int childCount = t.get_childCount(); i < childCount; i++)
			{
				Transform val = t.GetChild(i);
				if (val.get_gameObject().get_activeSelf())
				{
					return;
				}
			}
			int j = 0;
			for (int childCount2 = t.get_childCount(); j < childCount2; j++)
			{
				Transform t2 = t.GetChild(j);
				Activate(t2, true);
			}
		}
	}

	private static void Deactivate(Transform t)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		SetActiveSelf(t.get_gameObject(), false);
	}

	public static void SetActive(GameObject go, bool state)
	{
		SetActive(go, state, true);
	}

	public static void SetActive(GameObject go, bool state, bool compatibilityMode)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		if (Object.op_Implicit(go))
		{
			if (state)
			{
				Activate(go.get_transform(), compatibilityMode);
				CallCreatePanel(go.get_transform());
			}
			else
			{
				Deactivate(go.get_transform());
			}
		}
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	private static void CallCreatePanel(Transform t)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		UIWidget component = t.GetComponent<UIWidget>();
		if (component != null)
		{
			component.CreatePanel();
		}
		int i = 0;
		for (int childCount = t.get_childCount(); i < childCount; i++)
		{
			CallCreatePanel(t.GetChild(i));
		}
	}

	public static void SetActiveChildren(GameObject go, bool state)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		Transform val = go.get_transform();
		if (state)
		{
			int i = 0;
			for (int childCount = val.get_childCount(); i < childCount; i++)
			{
				Transform t = val.GetChild(i);
				Activate(t);
			}
		}
		else
		{
			int j = 0;
			for (int childCount2 = val.get_childCount(); j < childCount2; j++)
			{
				Transform t2 = val.GetChild(j);
				Deactivate(t2);
			}
		}
	}

	[Obsolete("Use NGUITools.GetActive instead")]
	public static bool IsActive(Behaviour mb)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		return mb != null && mb.get_enabled() && mb.get_gameObject().get_activeInHierarchy();
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static bool GetActive(Behaviour mb)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return Object.op_Implicit(mb) && mb.get_enabled() && mb.get_gameObject().get_activeInHierarchy();
	}

	[DebuggerStepThrough]
	[DebuggerHidden]
	public static bool GetActive(GameObject go)
	{
		return Object.op_Implicit(go) && go.get_activeInHierarchy();
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static void SetActiveSelf(GameObject go, bool state)
	{
		go.SetActive(state);
	}

	public static void SetLayer(GameObject go, int layer)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		go.set_layer(layer);
		Transform val = go.get_transform();
		int i = 0;
		for (int childCount = val.get_childCount(); i < childCount; i++)
		{
			Transform val2 = val.GetChild(i);
			SetLayer(val2.get_gameObject(), layer);
		}
	}

	public static Vector3 Round(Vector3 v)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		v.x = Mathf.Round(v.x);
		v.y = Mathf.Round(v.y);
		v.z = Mathf.Round(v.z);
		return v;
	}

	public static void MakePixelPerfect(Transform t)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		UIWidget component = t.GetComponent<UIWidget>();
		if (component != null)
		{
			component.MakePixelPerfect();
		}
		if (t.GetComponent<UIAnchor>() == null && t.GetComponent<UIRoot>() == null)
		{
			t.set_localPosition(Round(t.get_localPosition()));
			t.set_localScale(Round(t.get_localScale()));
		}
		int i = 0;
		for (int childCount = t.get_childCount(); i < childCount; i++)
		{
			MakePixelPerfect(t.GetChild(i));
		}
	}

	public static bool Save(string fileName, byte[] bytes)
	{
		if (!fileAccess)
		{
			return false;
		}
		string path = Application.get_persistentDataPath() + "/" + fileName;
		if (bytes == null)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			return true;
		}
		FileStream fileStream = null;
		try
		{
			fileStream = File.Create(path);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex.Message);
			return false;
			IL_0057:;
		}
		fileStream.Write(bytes, 0, bytes.Length);
		fileStream.Close();
		return true;
	}

	public static byte[] Load(string fileName)
	{
		if (!fileAccess)
		{
			return null;
		}
		string path = Application.get_persistentDataPath() + "/" + fileName;
		if (File.Exists(path))
		{
			return File.ReadAllBytes(path);
		}
		return null;
	}

	public static Color ApplyPMA(Color c)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		if (c.a != 1f)
		{
			c.r *= c.a;
			c.g *= c.a;
			c.b *= c.a;
		}
		return c;
	}

	public static void MarkParentAsChanged(GameObject go)
	{
		UIRect[] componentsInChildren = go.GetComponentsInChildren<UIRect>();
		int i = 0;
		for (int num = componentsInChildren.Length; i < num; i++)
		{
			componentsInChildren[i].ParentHasChanged();
		}
	}

	[Obsolete("Use NGUIText.EncodeColor instead")]
	public static string EncodeColor(Color c)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return NGUIText.EncodeColor24(c);
	}

	[Obsolete("Use NGUIText.ParseColor instead")]
	public static Color ParseColor(string text, int offset)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return NGUIText.ParseColor24(text, offset);
	}

	[Obsolete("Use NGUIText.StripSymbols instead")]
	public static string StripSymbols(string text)
	{
		return NGUIText.StripSymbols(text);
	}

	public static T AddMissingComponent<T>(this GameObject go) where T : Component
	{
		T val = go.GetComponent<T>();
		if ((object)val == null)
		{
			val = go.AddComponent<T>();
		}
		return val;
	}

	public static Vector3[] GetSides(this Camera cam)
	{
		return cam.GetSides(Mathf.Lerp(cam.get_nearClipPlane(), cam.get_farClipPlane(), 0.5f), null);
	}

	public static Vector3[] GetSides(this Camera cam, float depth)
	{
		return cam.GetSides(depth, null);
	}

	public static Vector3[] GetSides(this Camera cam, Transform relativeTo)
	{
		return cam.GetSides(Mathf.Lerp(cam.get_nearClipPlane(), cam.get_farClipPlane(), 0.5f), relativeTo);
	}

	public static Vector3[] GetSides(this Camera cam, float depth, Transform relativeTo)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		if (cam.get_orthographic())
		{
			float orthographicSize = cam.get_orthographicSize();
			float num = 0f - orthographicSize;
			float num2 = orthographicSize;
			float num3 = 0f - orthographicSize;
			float num4 = orthographicSize;
			Rect rect = cam.get_rect();
			Vector2 screenSize = NGUITools.screenSize;
			float num5 = screenSize.x / screenSize.y;
			num5 *= rect.get_width() / rect.get_height();
			num *= num5;
			num2 *= num5;
			Transform val = cam.get_transform();
			Quaternion rotation = val.get_rotation();
			Vector3 position = val.get_position();
			int num6 = Mathf.RoundToInt(screenSize.x);
			int num7 = Mathf.RoundToInt(screenSize.y);
			if ((num6 & 1) == 1)
			{
				position.x -= 1f / screenSize.x;
			}
			if ((num7 & 1) == 1)
			{
				position.y += 1f / screenSize.y;
			}
			mSides[0] = rotation * new Vector3(num, 0f, depth) + position;
			mSides[1] = rotation * new Vector3(0f, num4, depth) + position;
			mSides[2] = rotation * new Vector3(num2, 0f, depth) + position;
			mSides[3] = rotation * new Vector3(0f, num3, depth) + position;
		}
		else
		{
			mSides[0] = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, depth));
			mSides[1] = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, depth));
			mSides[2] = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, depth));
			mSides[3] = cam.ViewportToWorldPoint(new Vector3(0.5f, 0f, depth));
		}
		if (relativeTo != null)
		{
			for (int i = 0; i < 4; i++)
			{
				mSides[i] = relativeTo.InverseTransformPoint(mSides[i]);
			}
		}
		return mSides;
	}

	public static Vector3[] GetWorldCorners(this Camera cam)
	{
		float depth = Mathf.Lerp(cam.get_nearClipPlane(), cam.get_farClipPlane(), 0.5f);
		return cam.GetWorldCorners(depth, null);
	}

	public static Vector3[] GetWorldCorners(this Camera cam, float depth)
	{
		return cam.GetWorldCorners(depth, null);
	}

	public static Vector3[] GetWorldCorners(this Camera cam, Transform relativeTo)
	{
		return cam.GetWorldCorners(Mathf.Lerp(cam.get_nearClipPlane(), cam.get_farClipPlane(), 0.5f), relativeTo);
	}

	public static Vector3[] GetWorldCorners(this Camera cam, float depth, Transform relativeTo)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		if (cam.get_orthographic())
		{
			float orthographicSize = cam.get_orthographicSize();
			float num = 0f - orthographicSize;
			float num2 = orthographicSize;
			float num3 = 0f - orthographicSize;
			float num4 = orthographicSize;
			Rect rect = cam.get_rect();
			Vector2 screenSize = NGUITools.screenSize;
			float num5 = screenSize.x / screenSize.y;
			num5 *= rect.get_width() / rect.get_height();
			num *= num5;
			num2 *= num5;
			Transform val = cam.get_transform();
			Quaternion rotation = val.get_rotation();
			Vector3 position = val.get_position();
			mSides[0] = rotation * new Vector3(num, num3, depth) + position;
			mSides[1] = rotation * new Vector3(num, num4, depth) + position;
			mSides[2] = rotation * new Vector3(num2, num4, depth) + position;
			mSides[3] = rotation * new Vector3(num2, num3, depth) + position;
		}
		else
		{
			mSides[0] = cam.ViewportToWorldPoint(new Vector3(0f, 0f, depth));
			mSides[1] = cam.ViewportToWorldPoint(new Vector3(0f, 1f, depth));
			mSides[2] = cam.ViewportToWorldPoint(new Vector3(1f, 1f, depth));
			mSides[3] = cam.ViewportToWorldPoint(new Vector3(1f, 0f, depth));
		}
		if (relativeTo != null)
		{
			for (int i = 0; i < 4; i++)
			{
				mSides[i] = relativeTo.InverseTransformPoint(mSides[i]);
			}
		}
		return mSides;
	}

	public static string GetFuncName(object obj, string method)
	{
		if (obj == null)
		{
			return "<null>";
		}
		string text = obj.GetType().ToString();
		int num = text.LastIndexOf('/');
		if (num > 0)
		{
			text = text.Substring(num + 1);
		}
		return (!string.IsNullOrEmpty(method)) ? (text + "/" + method) : text;
	}

	public static void Execute<T>(GameObject go, string funcName) where T : Component
	{
		T[] components = go.GetComponents<T>();
		T[] array = components;
		for (int i = 0; i < array.Length; i++)
		{
			T val = array[i];
			((object)val).GetType().GetMethod(funcName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.Invoke(val, null);
		}
	}

	public static void ExecuteAll<T>(GameObject root, string funcName) where T : Component
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		Execute<T>(root, funcName);
		Transform val = root.get_transform();
		int i = 0;
		for (int childCount = val.get_childCount(); i < childCount; i++)
		{
			ExecuteAll<T>(val.GetChild(i).get_gameObject(), funcName);
		}
	}

	public static void ImmediatelyCreateDrawCalls(GameObject root)
	{
		NGUITools.ExecuteAll<UIWidget>(root, "Start");
		NGUITools.ExecuteAll<UIPanel>(root, "Start");
		NGUITools.ExecuteAll<UIWidget>(root, "Update");
		NGUITools.ExecuteAll<UIPanel>(root, "Update");
		NGUITools.ExecuteAll<UIPanel>(root, "LateUpdate");
	}

	public static string KeyToCaption(KeyCode key)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Expected I4, but got Unknown
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Expected I4, but got Unknown
		switch ((int)key)
		{
		default:
			switch (key - 256)
			{
			case 0:
				return "K0";
			case 1:
				return "K1";
			case 2:
				return "K2";
			case 3:
				return "K3";
			case 4:
				return "K4";
			case 5:
				return "K5";
			case 6:
				return "K6";
			case 7:
				return "K7";
			case 8:
				return "K8";
			case 9:
				return "K9";
			case 10:
				return ".";
			case 11:
				return "/";
			case 12:
				return "*";
			case 13:
				return "-";
			case 14:
				return "+";
			case 15:
				return "NT";
			case 16:
				return "=";
			case 17:
				return "UP";
			case 18:
				return "DN";
			case 19:
				return "LT";
			case 20:
				return "RT";
			case 21:
				return "Ins";
			case 22:
				return "Home";
			case 23:
				return "End";
			case 24:
				return "PU";
			case 25:
				return "PD";
			case 26:
				return "F1";
			case 27:
				return "F2";
			case 28:
				return "F3";
			case 29:
				return "F4";
			case 30:
				return "F5";
			case 31:
				return "F6";
			case 32:
				return "F7";
			case 33:
				return "F8";
			case 34:
				return "F9";
			case 35:
				return "F10";
			case 36:
				return "F11";
			case 37:
				return "F12";
			case 38:
				return "F13";
			case 39:
				return "F14";
			case 40:
				return "F15";
			case 44:
				return "Num";
			case 45:
				return "Cap";
			case 46:
				return "Scr";
			case 47:
				return "RS";
			case 48:
				return "LS";
			case 49:
				return "RC";
			case 50:
				return "LC";
			case 51:
				return "RA";
			case 52:
				return "LA";
			case 67:
				return "M0";
			case 68:
				return "M1";
			case 69:
				return "M2";
			case 70:
				return "M3";
			case 71:
				return "M4";
			case 72:
				return "M5";
			case 73:
				return "M6";
			case 74:
				return "(A)";
			case 75:
				return "(B)";
			case 76:
				return "(X)";
			case 77:
				return "(Y)";
			case 78:
				return "(RB)";
			case 79:
				return "(LB)";
			case 80:
				return "(Back)";
			case 81:
				return "(Start)";
			case 82:
				return "(LS)";
			case 83:
				return "(RS)";
			case 84:
				return "J10";
			case 85:
				return "J11";
			case 86:
				return "J12";
			case 87:
				return "J13";
			case 88:
				return "J14";
			case 89:
				return "J15";
			case 90:
				return "J16";
			case 91:
				return "J17";
			case 92:
				return "J18";
			case 93:
				return "J19";
			default:
				return null;
			}
		case 0:
			return null;
		case 8:
			return "BS";
		case 9:
			return "Tab";
		case 12:
			return "Clr";
		case 13:
			return "NT";
		case 19:
			return "PS";
		case 27:
			return "Esc";
		case 32:
			return "SP";
		case 33:
			return "!";
		case 34:
			return "\"";
		case 35:
			return "#";
		case 36:
			return "$";
		case 38:
			return "&";
		case 39:
			return "'";
		case 40:
			return "(";
		case 41:
			return ")";
		case 42:
			return "*";
		case 43:
			return "+";
		case 44:
			return ",";
		case 45:
			return "-";
		case 46:
			return ".";
		case 47:
			return "/";
		case 48:
			return "0";
		case 49:
			return "1";
		case 50:
			return "2";
		case 51:
			return "3";
		case 52:
			return "4";
		case 53:
			return "5";
		case 54:
			return "6";
		case 55:
			return "7";
		case 56:
			return "8";
		case 57:
			return "9";
		case 58:
			return ":";
		case 59:
			return ";";
		case 60:
			return "<";
		case 61:
			return "=";
		case 62:
			return ">";
		case 63:
			return "?";
		case 64:
			return "@";
		case 91:
			return "[";
		case 92:
			return "\\";
		case 93:
			return "]";
		case 94:
			return "^";
		case 95:
			return "_";
		case 96:
			return "`";
		case 97:
			return "A";
		case 98:
			return "B";
		case 99:
			return "C";
		case 100:
			return "D";
		case 101:
			return "E";
		case 102:
			return "F";
		case 103:
			return "G";
		case 104:
			return "H";
		case 105:
			return "I";
		case 106:
			return "J";
		case 107:
			return "K";
		case 108:
			return "L";
		case 109:
			return "M";
		case 110:
			return "N0";
		case 111:
			return "O";
		case 112:
			return "P";
		case 113:
			return "Q";
		case 114:
			return "R";
		case 115:
			return "S";
		case 116:
			return "T";
		case 117:
			return "U";
		case 118:
			return "V";
		case 119:
			return "W";
		case 120:
			return "X";
		case 121:
			return "Y";
		case 122:
			return "Z";
		case 127:
			return "Del";
		}
	}
}
