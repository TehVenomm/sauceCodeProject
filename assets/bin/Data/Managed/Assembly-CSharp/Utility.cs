using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class Utility
{
	private class TempCameraInfo
	{
		public int screenWidth;

		public int screenHeight;

		public float fov;

		public float limitRate = 1f;
	}

	public static List<object> stack;

	public static int stackPointer;

	private static TempCameraInfo tempCameraInfo;

	public static CustomBillboard customBillboard;

	public static void Initialize()
	{
		stack = new List<object>(32);
		stackPointer = 0;
	}

	public static void Push(object obj)
	{
		if (stack.Count == stackPointer)
		{
			stack.Add(obj);
		}
		else
		{
			stack[stackPointer] = obj;
		}
		stackPointer++;
	}

	public static T Pop<T>() where T : class
	{
		stackPointer--;
		object obj = stack[stackPointer];
		stack[stackPointer] = null;
		return obj as T;
	}

	public static int GetCurrentSecondFromNow(DateTime dt)
	{
		return (int)GetCurrentTimeSpanFromNow(dt).TotalSeconds;
	}

	public static TimeSpan GetCurrentTimeSpanFromNow(DateTime dt)
	{
		return dt - DateTime.UtcNow;
	}

	public static double DateTimeToTimestampMilliseconds(DateTime time)
	{
		DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (time - d).TotalMilliseconds;
	}

	public static Vector2 ToVector2XY(this Vector3 vector3)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(vector3.x, vector3.y);
	}

	public static Vector2 ToVector2XZ(this Vector3 vector3)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(vector3.x, vector3.z);
	}

	public static Vector3 ToVector3XY(this Vector2 vector2)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		return new Vector3(vector2.x, vector2.y);
	}

	public static Vector3 ToVector3XY(this Vector2 vector2, float z)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return new Vector3(vector2.x, vector2.y, z);
	}

	public static Vector3 ToVector3XZ(this Vector2 vector2)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		return new Vector3(vector2.x, 0f, vector2.y);
	}

	public static Color ToColor(this Vector3 vector3)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		return new Color(vector3.x, vector3.y, vector3.z);
	}

	public static Vector3 ToVector3(this Color color)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		return new Vector3(color.r, color.g, color.b);
	}

	public static Vector4 ToVector4(this Vector3 vec3)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector4(vec3.x, vec3.y, vec3.z, 1f);
	}

	public static Vector4 ToVector4(this Vector3 vec3, float w)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		return new Vector4(vec3.x, vec3.y, vec3.z, w);
	}

	public static Vector2 ToVector2XY(this Vector4 vec4)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(vec4.x, vec4.y);
	}

	public static Vector2 ToVector2ZW(this Vector4 vec4)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(vec4.z, vec4.w);
	}

	public static Vector3 ToVector3(this Vector4 vec4)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		return new Vector3(vec4.x, vec4.y, vec4.z);
	}

	public static Vector3 ToVector3XY(this Vector4 vec4)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		return new Vector3(vec4.x, vec4.y, 0f);
	}

	public static Vector3 GetNearPosOnLine(Vector3 pos_a, Vector3 pos_b, Vector3 pos_p)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		float dist_ax = 0f;
		return GetNearPosOnLine(pos_a, pos_b, pos_p, ref dist_ax);
	}

	public static Vector3 GetNearPosOnLine(Vector3 pos_a, Vector3 pos_b, Vector3 pos_p, ref float dist_ax)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = pos_b - pos_a;
		val.Normalize();
		dist_ax = Vector3.Dot(val, pos_p - pos_a);
		return pos_a + val * dist_ax;
	}

	public static string ToJoinString<T>(this T[] array, string split = ",", string format = null)
	{
		string text = string.Empty;
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			text += _ToJoinString(array[i], split, format, i == num - 1);
		}
		return text;
	}

	public static string ToJoinString<T>(this List<T> list, string split = ",", string format = null)
	{
		string text = string.Empty;
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			text += _ToJoinString(list[i], split, format, i == count - 1);
		}
		return text;
	}

	private static string _ToJoinString<T>(T element, string split, string format, bool last)
	{
		string empty = string.Empty;
		empty = ((format != null && ((object)element) is int) ? (empty + ((int)(object)element).ToString(format)) : ((format == null || !(((object)element) is float)) ? (empty + element.ToString()) : (empty + ((float)(object)element).ToString(format))));
		if (!last)
		{
			empty += split;
		}
		return empty;
	}

	public static float Angle360(Vector2 p1, Vector2 p2)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		float num = Vector2.Angle(p1, p2);
		Vector3 val = Vector3.Cross(Vector2.op_Implicit(p1), Vector2.op_Implicit(p2));
		if (val.z > 0f)
		{
			num = 360f - num;
		}
		return num;
	}

	public static float Random(float value)
	{
		return Random.Range(0f, value);
	}

	public static int Random(int value)
	{
		return Random.Range(0, value);
	}

	public static float SymmetryRandom(float value)
	{
		return Random.Range(0f - value, value);
	}

	public static T Lot<T>(T[] ary)
	{
		if (ary == null || ary.Length == 0)
		{
			return default(T);
		}
		return ary[Random.Range(0, ary.Length)];
	}

	public static int LotIndex(int[] probabilities, int total_probabilities = 100)
	{
		int num = Random.Range(0, total_probabilities);
		int i = 0;
		int num2 = probabilities.Length;
		int num3 = 0;
		for (; i < num2; i++)
		{
			num3 += probabilities[i];
			if (num < num3)
			{
				return i;
			}
		}
		return probabilities.Length;
	}

	public static bool Coin()
	{
		return Random.Range(0, 2) == 0;
	}

	public static bool Dice100(int per)
	{
		return Random.get_value() * 100f < (float)per;
	}

	public static T[] CreateMergedArray<T>(T[] array_a, T[] array_b)
	{
		int num = 0;
		if (array_a != null)
		{
			num = array_a.Length;
		}
		int num2 = 0;
		if (array_b != null)
		{
			num2 = array_b.Length;
		}
		T[] array = new T[num + num2];
		array_a?.CopyTo(array, 0);
		array_b?.CopyTo(array, num);
		return array;
	}

	public static T[] DistinctArray<T>(T[] array)
	{
		return array?.Distinct().ToArray();
	}

	public static uint GetHash(string str)
	{
		uint num = 0u;
		int i = 0;
		for (int length = str.Length; i < length; i++)
		{
			num = ((num << 1) | ((num >> 31) & 1));
			num += str[i];
		}
		return num;
	}

	public static Vector3 Mul(this Vector3 this_vector3, Vector3 mul_vector3)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		this_vector3.x *= mul_vector3.x;
		this_vector3.y *= mul_vector3.y;
		this_vector3.z *= mul_vector3.z;
		return this_vector3;
	}

	public static Vector3 Div(this Vector3 this_vector3, Vector3 div_vector3)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		this_vector3.x /= div_vector3.x;
		this_vector3.y /= div_vector3.y;
		this_vector3.z /= div_vector3.z;
		return this_vector3;
	}

	public static void Set(this Transform transform, Vector3 pos, Quaternion rot)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		transform.set_position(pos);
		transform.set_rotation(rot);
	}

	public static void Set(this Transform transform, Vector3 pos, Vector3 rot)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		transform.set_position(pos);
		transform.set_eulerAngles(rot);
	}

	public static void CopyFrom(this Transform transform, Transform from_transform)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		transform.set_position(from_transform.get_position());
		transform.set_rotation(from_transform.get_rotation());
	}

	public static void SetActiveChildren(Transform parent, bool is_active)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (parent != null)
		{
			int i = 0;
			for (int childCount = parent.get_childCount(); i < childCount; i++)
			{
				parent.GetChild(i).get_gameObject().SetActive(is_active);
			}
		}
	}

	public static void ToggleActiveChildren(Transform parent, int index)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (parent != null && index >= 0 && index < parent.get_childCount())
		{
			int i = 0;
			for (int childCount = parent.get_childCount(); i < childCount; i++)
			{
				parent.GetChild(i).get_gameObject().SetActive(i == index);
			}
		}
	}

	public static void Destroy(ref Transform t)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (t != null)
		{
			Object.Destroy(t.get_gameObject());
			t = null;
		}
	}

	public static Transform Find(Transform transform, string name)
	{
		Transform val = FindChild(transform, name);
		if (val != null)
		{
			return val;
		}
		if (transform.get_name() == name)
		{
			return transform;
		}
		return null;
	}

	public static Transform FindChild(Transform transform, string name)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		Transform val = transform.FindChild(name);
		if (val != null)
		{
			return val;
		}
		int i = 0;
		for (int childCount = transform.get_childCount(); i < childCount; i++)
		{
			val = FindChild(transform.GetChild(i), name);
			if (val != null)
			{
				return val;
			}
		}
		return null;
	}

	public static Transform FindActiveChild(Transform transform, string name)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Transform val = transform.FindChild(name);
		if (val != null && val.get_gameObject().get_activeSelf())
		{
			return val;
		}
		int i = 0;
		for (int childCount = transform.get_childCount(); i < childCount; i++)
		{
			val = FindChild(transform.GetChild(i), name);
			if (val != null && val.get_gameObject().get_activeSelf())
			{
				return val;
			}
		}
		return null;
	}

	public static bool ForEach(Transform transform, Predicate<Transform> callback)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		if (transform == null)
		{
			return false;
		}
		if (callback(transform))
		{
			return true;
		}
		int i = 0;
		for (int childCount = transform.get_childCount(); i < childCount; i++)
		{
			if (ForEach(transform.GetChild(i), callback))
			{
				return true;
			}
		}
		return false;
	}

	public static void StackComponentInChildren<C>(Transform transform) where C : Component
	{
		Push(null);
		_StackComponentInChildren(transform, typeof(C));
	}

	private static void _StackComponentInChildren(Transform transform, Type type)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		Component val = transform.GetComponent(type);
		if (val != null)
		{
			Push(val);
		}
		int i = 0;
		for (int childCount = transform.get_childCount(); i < childCount; i++)
		{
			_StackComponentInChildren(transform.GetChild(i), type);
		}
	}

	public static void Attach(Transform parent, Transform child)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		Vector3 localPosition = child.get_localPosition();
		Quaternion localRotation = child.get_localRotation();
		Vector3 localScale = child.get_localScale();
		child.set_parent(parent);
		child.set_localPosition(localPosition);
		child.set_localRotation(localRotation);
		child.set_localScale(localScale);
	}

	public static Transform Insert(Transform child, bool transfom_delegate = false)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = new GameObject();
		Transform val2 = val.get_transform();
		val2.set_parent(child.get_parent());
		child.set_parent(val2);
		if (transfom_delegate)
		{
			val2.set_localPosition(child.get_localPosition());
			val2.set_localRotation(child.get_localRotation());
			val2.set_localScale(child.get_localScale());
			child.set_localPosition(Vector3.get_zero());
			child.set_localRotation(Quaternion.get_identity());
			child.set_localScale(Vector3.get_one());
		}
		return val2;
	}

	public static void SetLayerWithChildren(Transform transform, int layer)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		transform.get_gameObject().set_layer(layer);
		int i = 0;
		for (int childCount = transform.get_childCount(); i < childCount; i++)
		{
			SetLayerWithChildren(transform.GetChild(i), layer);
		}
	}

	public static void SetLayerWithChildren(Transform transform, int setLayer, int exceptLayer)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		if (transform.get_gameObject().get_layer() != exceptLayer)
		{
			transform.get_gameObject().set_layer(setLayer);
		}
		for (int i = 0; i < transform.get_childCount(); i++)
		{
			GameObject val = transform.GetChild(i).get_gameObject();
			if (val.get_layer() != exceptLayer)
			{
				val.set_layer(setLayer);
			}
		}
	}

	public static void SetAllNotCollideLayers()
	{
		for (int i = 0; i < 32; i++)
		{
			for (int num = 31; num >= i; num--)
			{
				Physics.IgnoreLayerCollision(num, i, true);
			}
		}
	}

	public static void SetCollideLayers(int target_layer, params int[] hit_layers)
	{
		int i = 0;
		for (int num = hit_layers.Length; i < num; i++)
		{
			Physics.IgnoreLayerCollision(target_layer, hit_layers[i], false);
		}
	}

	public static void IgnoreCollision(Collider collider, Collider[] colliders, bool ignore)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		int i = 0;
		for (int num = colliders.Length; i < num; i++)
		{
			if (!(colliders[i] == null) && !colliders[i].get_isTrigger() && colliders[i].get_enabled() && colliders[i].get_gameObject().get_activeInHierarchy())
			{
				Physics.IgnoreCollision(collider, colliders[i], ignore);
			}
		}
	}

	public static Transform CreateGameObject(string name, Transform parent, int layer = -1)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		GameObject val = new GameObject(name);
		Transform val2 = val.get_transform();
		if (parent != null)
		{
			Attach(parent, val2);
		}
		if (layer != -1)
		{
			val.set_layer(layer);
		}
		return val2;
	}

	public static Component CreateGameObjectAndComponent(string name, Transform parent = null, int layer = -1)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		Transform val = CreateGameObject(name, parent, layer);
		Type type = Type.GetType(name);
		return val.get_gameObject().AddComponent(type);
	}

	public static void CreateBoxColliderRing(Transform parent, float radius, int divide_num, float box_height = 3, float box_thick = 3)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		radius += box_thick;
		float y = box_height * 0.5f;
		BoxCollider val = null;
		BoxCollider val2 = null;
		BoxCollider val3 = null;
		Transform val4 = null;
		Transform val5 = null;
		Transform val6 = null;
		for (int i = 0; i < divide_num; i++)
		{
			float num = (float)i / (float)divide_num * 3.14159274f * 2f;
			val3 = CreateGameObjectAndComponent("BoxCollider", parent, -1);
			val6 = val3.get_transform();
			Vector3 val7 = default(Vector3);
			val7._002Ector(Mathf.Cos(num) * radius, 0f, Mathf.Sin(num) * radius);
			val6.set_localPosition(val7);
			val6.LookAt(val7 * 0.9f + parent.get_position());
			val7.y = y;
			val6.set_localPosition(val7);
			if (val2 != null)
			{
				Vector3 val8 = val6.get_localPosition() - val5.get_localPosition();
				float magnitude = val8.get_magnitude();
				val3.set_size(new Vector3(magnitude, box_height, box_thick));
			}
			if (val == null)
			{
				val = val3;
				val4 = val6;
			}
			val2 = val3;
			val5 = val6;
		}
		if (val != null)
		{
			Vector3 val9 = val4.get_localPosition() - val5.get_localPosition();
			float magnitude2 = val9.get_magnitude();
			val.set_size(new Vector3(magnitude2, box_height, box_thick));
		}
	}

	public static C[] CollectEnumNameComponents<C, E>(Transform root) where C : Component
	{
		return CollectEnumNameComponents<C>(root, typeof(E));
	}

	public static C[] CollectEnumNameComponents<C>(Transform root, Type enum_type) where C : Component
	{
		return CollectEnumNameComponents<C>(root, enum_type, Enum.GetNames(enum_type));
	}

	public static C[] CollectEnumNameComponents<C>(Transform root, Type enum_type, string[] names) where C : Component
	{
		int num = names.Length;
		C[] array = new C[num];
		StackComponentInChildren<C>(root);
		while (true)
		{
			C val = Utility.Pop<C>();
			if ((object)val == null)
			{
				break;
			}
			string name = val.get_name();
			for (int i = 0; i < num; i++)
			{
				if ((object)array[i] == null && names[i] == name)
				{
					array[i] = val;
					break;
				}
			}
		}
		return array;
	}

	public static void MaterialForEach(Renderer[] renderers, Action<Material> callback)
	{
		if (renderers != null)
		{
			int i = 0;
			for (int num = renderers.Length; i < num; i++)
			{
				if (!(renderers[i] == null))
				{
					Material[] materials = renderers[i].get_materials();
					int j = 0;
					for (int num2 = materials.Length; j < num2; j++)
					{
						if (materials[j] != null)
						{
							callback(materials[j]);
						}
					}
				}
			}
		}
	}

	public static void SharedMaterialForEach(Renderer[] renderers, Action<Material> callback)
	{
		if (renderers != null)
		{
			int i = 0;
			for (int num = renderers.Length; i < num; i++)
			{
				Material[] sharedMaterials = renderers[i].get_sharedMaterials();
				int j = 0;
				for (int num2 = sharedMaterials.Length; j < num2; j++)
				{
					if (sharedMaterials[j] != null)
					{
						callback(sharedMaterials[j]);
					}
				}
			}
		}
	}

	public static float VolumeToDecibel(float volume)
	{
		float num = 20f * Mathf.Log10(volume);
		if (float.IsInfinity(num) || float.IsNaN(num))
		{
			return -80f;
		}
		return num;
	}

	public static float DecibelToVolume(float dB)
	{
		float num = -80f;
		float num2 = 20f;
		float num3 = Mathf.Clamp(dB, num, num2);
		float num4 = Mathf.Pow(10f, num3 / num2);
		return Mathf.Clamp01(num4);
	}

	public static float HorizontalToVerticalFOV(float Horizontal_fov)
	{
		float num = (float)Screen.get_height() / (float)Screen.get_width();
		if (num < 1f)
		{
			num = 1f;
		}
		float num2 = num * 9f / 16f;
		return Mathf.Atan(Mathf.Tan(Horizontal_fov * 0.5f * 0.0174532924f) * num2) * 57.29578f * 2f;
	}

	public static TValue GetValueOrAddDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> defaultValue)
	{
		if (dict.TryGetValue(key, out TValue value))
		{
			return value;
		}
		value = defaultValue.Invoke(key);
		dict.Add(key, value);
		return value;
	}

	public unsafe static TValue GetValueOrAddNew<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key) where TValue : new()
	{
		return dict.GetValueOrAddDefault(key, new Func<_003F, _003F>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
	{
		if (dict.TryGetValue(key, out TValue value))
		{
			return value;
		}
		return default(TValue);
	}

	public static Vector3 ClosestPointOnCollider(Collider to_collider, Vector3 point)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		SphereCollider val = to_collider as SphereCollider;
		if (val != null)
		{
			Matrix4x4 worldToLocalMatrix = val.get_transform().get_worldToLocalMatrix();
			Vector3 val2 = worldToLocalMatrix.MultiplyPoint3x4(point);
			val2 -= val.get_center();
			val2.Normalize();
			val2 *= val.get_radius();
			val2 += val.get_center();
			Matrix4x4 localToWorldMatrix = val.get_transform().get_localToWorldMatrix();
			return localToWorldMatrix.MultiplyPoint3x4(val2);
		}
		CapsuleCollider val3 = to_collider as CapsuleCollider;
		if (val3 != null)
		{
			Matrix4x4 worldToLocalMatrix2 = val3.get_transform().get_worldToLocalMatrix();
			Vector3 val4 = worldToLocalMatrix2.MultiplyPoint3x4(point);
			val4 -= val3.get_center();
			float num = val4.get_Item(val3.get_direction());
			float num2 = val3.get_height() * 0.5f - val3.get_radius();
			int direction;
			float num4;
			if (Mathf.Abs(num) > num2)
			{
				num = Mathf.Sign(num) * num2;
				ref Vector3 reference;
				ref Vector3 reference2 = ref reference = ref val4;
				int num3 = direction = val3.get_direction();
				num4 = reference.get_Item(direction);
				reference2.set_Item(num3, num4 - num);
			}
			else
			{
				val4.set_Item(val3.get_direction(), 0f);
			}
			val4.Normalize();
			val4 *= val3.get_radius();
			ref Vector3 reference3;
			ref Vector3 reference4 = ref reference3 = ref val4;
			int num5 = direction = val3.get_direction();
			num4 = reference3.get_Item(direction);
			reference4.set_Item(num5, num4 + num);
			val4 += val3.get_center();
			Matrix4x4 localToWorldMatrix2 = val3.get_transform().get_localToWorldMatrix();
			return localToWorldMatrix2.MultiplyPoint3x4(val4);
		}
		BoxCollider val5 = to_collider as BoxCollider;
		if (val5 != null)
		{
			Matrix4x4 worldToLocalMatrix3 = val5.get_transform().get_worldToLocalMatrix();
			Vector3 val6 = worldToLocalMatrix3.MultiplyPoint3x4(point);
			val6 -= val5.get_center();
			Vector3 zero = Vector3.get_zero();
			float num6 = Mathf.Abs(val6.x);
			Vector3 size = val5.get_size();
			zero.x = num6 - size.x * 0.5f;
			float num7 = Mathf.Abs(val6.y);
			Vector3 size2 = val5.get_size();
			zero.y = num7 - size2.y * 0.5f;
			float num8 = Mathf.Abs(val6.z);
			Vector3 size3 = val5.get_size();
			zero.z = num8 - size3.z * 0.5f;
			int num9 = -1;
			if (zero.x < 0f && zero.y < 0f && zero.z < 0f)
			{
				float num10 = 0f;
				for (int i = 0; i < 3; i++)
				{
					if (i == 0 || num10 < zero.get_Item(i))
					{
						num9 = i;
						num10 = zero.get_Item(i);
					}
				}
			}
			if (zero.x > 0f || num9 == 0)
			{
				float num11 = Mathf.Sign(val6.x);
				Vector3 size4 = val5.get_size();
				val6.x = num11 * size4.x * 0.5f;
			}
			if (zero.y > 0f || num9 == 1)
			{
				float num12 = Mathf.Sign(val6.y);
				Vector3 size5 = val5.get_size();
				val6.y = num12 * size5.y * 0.5f;
			}
			if (zero.z > 0f || num9 == 2)
			{
				float num13 = Mathf.Sign(val6.z);
				Vector3 size6 = val5.get_size();
				val6.z = num13 * size6.z * 0.5f;
			}
			val6 += val5.get_center();
			Matrix4x4 localToWorldMatrix3 = val5.get_transform().get_localToWorldMatrix();
			return localToWorldMatrix3.MultiplyPoint3x4(val6);
		}
		return to_collider.ClosestPointOnBounds(point);
	}

	public static Vector3 ClosestPointOnColliderFix(Collider to_collider, Vector3 point)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = Vector3.get_zero();
		if (to_collider is BoxCollider)
		{
			val = (to_collider as BoxCollider).get_center();
		}
		else if (to_collider is SphereCollider)
		{
			val = (to_collider as SphereCollider).get_center();
		}
		else if (to_collider is CapsuleCollider)
		{
			val = (to_collider as CapsuleCollider).get_center();
		}
		Vector3 val2 = to_collider.get_transform().get_position() + val - point;
		Vector3 normalized = val2.get_normalized();
		RaycastHit val3 = default(RaycastHit);
		if (!to_collider.Raycast(new Ray(point, normalized), ref val3, 3.40282347E+38f))
		{
			return point;
		}
		return ClosestPointOnCollider(to_collider, point);
	}

	public static bool IsExist(ICollection collection)
	{
		if (collection == null)
		{
			return false;
		}
		return collection.Count > 0;
	}

	public static void LogString<T>(this List<T> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			Debug.Log((object)list[i].ToString());
		}
	}

	public static void DoAction<T>(this List<T> list, Action<T> callback)
	{
		for (int i = 0; i < list.Count; i++)
		{
			callback?.Invoke(list[i]);
		}
	}

	public static void DoAction<T>(this List<T> list, Action<T, int> callback)
	{
		for (int i = 0; i < list.Count; i++)
		{
			callback?.Invoke(list[i], i);
		}
	}

	public static string[] DumpList(IList listobj, string parentName = "")
	{
		List<string> list = new List<string>();
		if (listobj.Count == 0)
		{
			list.Add($"{parentName}=[(empty)]");
		}
		else
		{
			int num = 0;
			foreach (object item in listobj)
			{
				string @namespace = item.GetType().Namespace;
				if (@namespace == null || !@namespace.ToString().StartsWith("System"))
				{
					list.AddRange(Dump(item, parentName + "[" + num + "]"));
				}
				else
				{
					list.Add($"{parentName}[{num}]={item.ToString()}");
				}
				num++;
			}
		}
		return list.ToArray();
	}

	public static string[] Dump(object obj, string parentName = "")
	{
		if (obj is IList)
		{
			return DumpList((IList)obj, parentName);
		}
		List<string> list = new List<string>();
		FieldInfo[] fields = obj.GetType().GetFields();
		FieldInfo[] array = fields;
		foreach (FieldInfo fieldInfo in array)
		{
			string text = parentName + "." + fieldInfo.Name;
			object value = fieldInfo.GetValue(obj);
			if (value == null)
			{
				list.Add($"{text}=(null)");
			}
			else
			{
				if (value is IList)
				{
					list.AddRange(DumpList((IList)value, text));
				}
				else
				{
					list.Add($"{text}={value.ToString()}");
				}
				string @namespace = value.GetType().Namespace;
				if (@namespace == null || !@namespace.ToString().StartsWith("System"))
				{
					list.AddRange(Dump(value, text));
				}
			}
		}
		return list.ToArray();
	}

	public static void PlayFullScreenMovie(string movieName)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		movieName = Application.get_temporaryCachePath() + "/" + movieName;
		Handheld.PlayFullScreenMovie(movieName, Color.get_black(), 0);
	}

	public static float GetiOSVersion()
	{
		return -1f;
	}

	public static Vector3 GetScreenUIPosition(Camera camera, Transform cam_transform, Vector3 pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		Matrix4x4 worldToLocalMatrix = cam_transform.get_worldToLocalMatrix();
		Vector3 val = worldToLocalMatrix.MultiplyPoint3x4(pos);
		float z = val.z;
		val.z = 0f;
		float magnitude = val.get_magnitude();
		float num = 1f;
		if (tempCameraInfo == null || tempCameraInfo.screenWidth != Screen.get_width() || tempCameraInfo.screenHeight != Screen.get_height() || tempCameraInfo.fov != camera.get_fieldOfView())
		{
			float num2 = (float)Screen.get_height() * 0.5f;
			float num3 = Mathf.Sqrt((float)(Screen.get_height() * Screen.get_height() + Screen.get_width() * Screen.get_width())) * 0.5f;
			float num4 = camera.get_fieldOfView() * 0.5f;
			num = Mathf.Tan(num4 * 0.0174532924f) * num3 / num2;
			tempCameraInfo = new TempCameraInfo();
			tempCameraInfo.screenWidth = Screen.get_width();
			tempCameraInfo.screenHeight = Screen.get_height();
			tempCameraInfo.fov = camera.get_fieldOfView();
			tempCameraInfo.limitRate = num;
		}
		else
		{
			num = tempCameraInfo.limitRate;
		}
		float num5 = 1f;
		float num6 = num5 * num;
		if (z <= 0f || num6 < magnitude * num5 / z)
		{
			val *= num6 / magnitude;
			val.z = num5;
		}
		else
		{
			val *= num5 / z;
			val.z = num5;
		}
		Matrix4x4 localToWorldMatrix = cam_transform.get_localToWorldMatrix();
		pos = localToWorldMatrix.MultiplyPoint3x4(val);
		Vector3 result = camera.WorldToScreenPoint(pos);
		result.z = z;
		return result;
	}

	public static Color MakeColorByInt(int r, int g, int b, int a = 255)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		Color result = default(Color);
		result._002Ector((float)r / 255f, (float)g / 255f, (float)b / 255f, (float)a / 255f);
		return result;
	}

	public static IEnumerable<T> MultipleEnumerator<T>(params IEnumerable<T>[] paramArray)
	{
		foreach (IEnumerable<T> param in paramArray)
		{
			foreach (T item in param)
			{
				yield return item;
			}
		}
	}

	public static IEnumerable<T> GetAllCompornent<T>(this Component root)
	{
		foreach (T item in Utility.MultipleEnumerator<T>(new IEnumerable<T>[2]
		{
			root.GetComponents<T>(),
			root.GetComponentsInChildren<T>(true)
		}))
		{
			yield return item;
		}
	}

	public static IEnumerable<T> GetAllCompornent<T>(this GameObject root)
	{
		foreach (T item in Utility.MultipleEnumerator<T>(new IEnumerable<T>[2]
		{
			root.GetComponents<T>(),
			root.GetComponentsInChildren<T>(true)
		}))
		{
			yield return item;
		}
	}

	public static bool IsEnableEquip(EQUIPMENT_TYPE type, ABILITY_ENABLE_TYPE abilityEnableType)
	{
		return IsEnableEquip(type, GetEnableEquipType(abilityEnableType));
	}

	public static bool IsEnableEquip(EQUIPMENT_TYPE type, ENABLE_EQUIP_TYPE enableType)
	{
		if (enableType == ENABLE_EQUIP_TYPE.ALL)
		{
			return true;
		}
		if (type == EQUIPMENT_TYPE.ONE_HAND_SWORD && enableType == ENABLE_EQUIP_TYPE.ONE_HAND_SWORD)
		{
			return true;
		}
		if (type == EQUIPMENT_TYPE.TWO_HAND_SWORD && enableType == ENABLE_EQUIP_TYPE.TWO_HAND_SWORD)
		{
			return true;
		}
		if (type == EQUIPMENT_TYPE.SPEAR && enableType == ENABLE_EQUIP_TYPE.SPEAR)
		{
			return true;
		}
		if (type == EQUIPMENT_TYPE.PAIR_SWORDS && enableType == ENABLE_EQUIP_TYPE.PAIR_SWORDS)
		{
			return true;
		}
		if (type == EQUIPMENT_TYPE.ARROW && enableType == ENABLE_EQUIP_TYPE.ARROW)
		{
			return true;
		}
		if (enableType == ENABLE_EQUIP_TYPE.ARMORS && (type == EQUIPMENT_TYPE.ARM || type == EQUIPMENT_TYPE.ARMOR || type == EQUIPMENT_TYPE.HELM || type == EQUIPMENT_TYPE.LEG))
		{
			return true;
		}
		return false;
	}

	public static bool IsConditionsAbilityType(ABILITY_TYPE abilityType)
	{
		return GetAbilityEnableType(abilityType) != ABILITY_ENABLE_TYPE.NONE;
	}

	public static ENABLE_EQUIP_TYPE GetEnableEquipType(ABILITY_ENABLE_TYPE enableType)
	{
		switch (enableType)
		{
		case ABILITY_ENABLE_TYPE.ONE_HAND_SWORD:
			return ENABLE_EQUIP_TYPE.ONE_HAND_SWORD;
		case ABILITY_ENABLE_TYPE.TWO_HAND_SWORD:
			return ENABLE_EQUIP_TYPE.TWO_HAND_SWORD;
		case ABILITY_ENABLE_TYPE.SPEAR:
			return ENABLE_EQUIP_TYPE.SPEAR;
		case ABILITY_ENABLE_TYPE.PAIR_SWORDS:
			return ENABLE_EQUIP_TYPE.PAIR_SWORDS;
		case ABILITY_ENABLE_TYPE.ARROW:
			return ENABLE_EQUIP_TYPE.ARROW;
		case ABILITY_ENABLE_TYPE.ARMORS:
			return ENABLE_EQUIP_TYPE.ARMORS;
		default:
			return ENABLE_EQUIP_TYPE.ALL;
		}
	}

	public static ABILITY_ENABLE_TYPE GetAbilityEnableType(ENABLE_EQUIP_TYPE enableType)
	{
		switch (enableType)
		{
		case ENABLE_EQUIP_TYPE.ONE_HAND_SWORD:
			return ABILITY_ENABLE_TYPE.ONE_HAND_SWORD;
		case ENABLE_EQUIP_TYPE.TWO_HAND_SWORD:
			return ABILITY_ENABLE_TYPE.TWO_HAND_SWORD;
		case ENABLE_EQUIP_TYPE.SPEAR:
			return ABILITY_ENABLE_TYPE.SPEAR;
		case ENABLE_EQUIP_TYPE.PAIR_SWORDS:
			return ABILITY_ENABLE_TYPE.PAIR_SWORDS;
		case ENABLE_EQUIP_TYPE.ARROW:
			return ABILITY_ENABLE_TYPE.ARROW;
		default:
			return ABILITY_ENABLE_TYPE.NONE;
		}
	}

	public static ABILITY_ENABLE_TYPE GetAbilityEnableType(ABILITY_TYPE abilityType)
	{
		switch (abilityType)
		{
		case ABILITY_TYPE.IF_HP_LOW:
			return ABILITY_ENABLE_TYPE.IF_HP_LOW;
		case ABILITY_TYPE.IF_HP_HIGH:
			return ABILITY_ENABLE_TYPE.IF_HP_HIGH;
		case ABILITY_TYPE.IF_COUNTER_ATTACK:
			return ABILITY_ENABLE_TYPE.IF_COUNTER_ATTACK;
		case ABILITY_TYPE.FINISH_A_CLEAVE_COMBO:
			return ABILITY_ENABLE_TYPE.FINISH_A_CLEAVE_COMBO;
		default:
			return ABILITY_ENABLE_TYPE.NONE;
		}
	}

	public static bool CheckEnableSpAttackType(ref bool rEnable, SP_ATTACK_TYPE spAttackType, ABILITY_ENABLE_TYPE abilityEnableType)
	{
		switch (abilityEnableType)
		{
		default:
			return false;
		case ABILITY_ENABLE_TYPE.NORMAL:
			rEnable = (spAttackType == SP_ATTACK_TYPE.NONE);
			break;
		case ABILITY_ENABLE_TYPE.HEAT:
			rEnable = (spAttackType == SP_ATTACK_TYPE.HEAT);
			break;
		case ABILITY_ENABLE_TYPE.SOUL:
			rEnable = (spAttackType == SP_ATTACK_TYPE.SOUL);
			break;
		case ABILITY_ENABLE_TYPE.NORMAL_HEAT:
			rEnable = (spAttackType == SP_ATTACK_TYPE.NONE || spAttackType == SP_ATTACK_TYPE.HEAT);
			break;
		case ABILITY_ENABLE_TYPE.NORMAL_SOUL:
			rEnable = (spAttackType == SP_ATTACK_TYPE.NONE || spAttackType == SP_ATTACK_TYPE.SOUL);
			break;
		case ABILITY_ENABLE_TYPE.HEAT_SOUL:
			rEnable = (spAttackType == SP_ATTACK_TYPE.HEAT || spAttackType == SP_ATTACK_TYPE.SOUL);
			break;
		}
		return true;
	}

	public static bool CheckEnableSpAttackType(ref bool rEnable, SP_ATTACK_TYPE spAttackType, int spAtkEnableBit)
	{
		bool flag = false;
		bool flag2 = false;
		if ((1 & spAtkEnableBit) != 0)
		{
			flag2 |= (spAttackType == SP_ATTACK_TYPE.NONE);
			flag = ((byte)((flag ? 1 : 0) | 1) != 0);
		}
		if ((2 & spAtkEnableBit) != 0)
		{
			flag2 |= (spAttackType == SP_ATTACK_TYPE.HEAT);
			flag = ((byte)((flag ? 1 : 0) | 1) != 0);
		}
		if ((4 & spAtkEnableBit) != 0)
		{
			flag2 |= (spAttackType == SP_ATTACK_TYPE.SOUL);
			flag = ((byte)((flag ? 1 : 0) | 1) != 0);
		}
		if ((8 & spAtkEnableBit) != 0)
		{
			flag2 |= (spAttackType == SP_ATTACK_TYPE.BURST);
			flag = ((byte)((flag ? 1 : 0) | 1) != 0);
		}
		if (flag)
		{
			rEnable = flag2;
		}
		return flag;
	}

	public static bool IsEnableSpAttackType(SP_ATTACK_TYPE skillSpAttackType, SP_ATTACK_TYPE equipSpAttackType)
	{
		if (skillSpAttackType == SP_ATTACK_TYPE.NONE)
		{
			return true;
		}
		return skillSpAttackType == equipSpAttackType;
	}

	public static int GetCurrentEventID()
	{
		if (!MonoBehaviourSingleton<FieldManager>.IsValid() || !Singleton<FieldMapTable>.IsValid())
		{
			return 0;
		}
		FieldManager i = MonoBehaviourSingleton<FieldManager>.I;
		List<FieldMapTable.EnemyPopTableData> enemyPopList = Singleton<FieldMapTable>.I.GetEnemyPopList(i.currentMapID);
		if (enemyPopList != null && enemyPopList.Count > 0)
		{
			foreach (FieldMapTable.EnemyPopTableData item in enemyPopList)
			{
				if (item.bossFlag)
				{
					QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
					if (questData != null)
					{
						return questData.eventId;
					}
				}
			}
		}
		if (i.currentMapData != null)
		{
			return i.currentMapData.eventId;
		}
		return 0;
	}

	public static T Find<T>(this T[] array, Predicate<T> match) where T : class
	{
		foreach (T val in array)
		{
			if (match(val))
			{
				return val;
			}
		}
		return (T)null;
	}

	public static void SafeInvoke(this Action action)
	{
		if (action != null)
		{
			action.Invoke();
		}
	}

	public static void SafeInvoke<T>(this Action<T> action, T t)
	{
		action?.Invoke(t);
	}

	public static void SafeInvoke<T1, T2>(this Action<T1, T2> action, T1 t1, T2 t2)
	{
		action?.Invoke(t1, t2);
	}

	public static void SafeInvoke<T1, T2, T3>(this Action<T1, T2, T3> action, T1 t1, T2 t2, T3 t3)
	{
		action?.Invoke(t1, t2, t3);
	}

	public static void SafeInvoke<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 t1, T2 t2, T3 t3, T4 t4)
	{
		action?.Invoke(t1, t2, t3, t4);
	}

	public static bool IsNullOrWhiteSpace(this string self)
	{
		return self == null || self.Trim() == string.Empty;
	}

	public static bool ContainIgnoreCase(this string self, string value)
	{
		return self.ToLower().Contains(value.ToLower());
	}

	public static bool IsNullOrEmpty<T>(this IList<T> self)
	{
		return self == null || ((ICollection<T>)self).Count == 0;
	}

	public static int ToInt32OrDefault(this string s, int defaultValue = 0)
	{
		if (int.TryParse(s, out int result))
		{
			return result;
		}
		return defaultValue;
	}

	public static string AbsoluteToAssetPath(string absolutepath)
	{
		if (absolutepath.StartsWith(Application.get_dataPath()))
		{
			return "Assets" + absolutepath.Substring(Application.get_dataPath().Length);
		}
		return absolutepath;
	}

	public static float ToFloatOrDefault(this string s, float defaultValue = 0f)
	{
		if (float.TryParse(s, out float result))
		{
			return result;
		}
		return defaultValue;
	}

	public static int Digit(int num)
	{
		return (num == 0) ? 1 : ((int)Mathf.Log10((float)num) + 1);
	}

	public static string GetRewardName(REWARD_TYPE rewardType, uint itemId)
	{
		string result = string.Empty;
		switch (rewardType)
		{
		case REWARD_TYPE.CRYSTAL:
			result = StringTable.Get(STRING_CATEGORY.COMMON, 100u);
			break;
		case REWARD_TYPE.MONEY:
			result = StringTable.Get(STRING_CATEGORY.COMMON, 101u);
			break;
		case REWARD_TYPE.EXP:
			result = StringTable.Get(STRING_CATEGORY.COMMON, 102u);
			break;
		case REWARD_TYPE.ITEM:
		case REWARD_TYPE.ABILITY_ITEM:
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(itemId);
			result = itemData.name;
			break;
		}
		case REWARD_TYPE.EQUIP_ITEM:
		{
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(itemId);
			result = equipItemData.name;
			break;
		}
		case REWARD_TYPE.SKILL_ITEM:
		{
			SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(itemId);
			result = skillItemData.name;
			break;
		}
		case REWARD_TYPE.QUEST_ITEM:
		{
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(itemId);
			result = questData.questText;
			break;
		}
		case REWARD_TYPE.AVATAR:
		{
			AvatarTable.AvatarData data4 = Singleton<AvatarTable>.I.GetData(itemId);
			result = data4.name;
			break;
		}
		case REWARD_TYPE.STAMP:
		{
			StampTable.Data data3 = Singleton<StampTable>.I.GetData(itemId);
			result = data3.desc;
			break;
		}
		case REWARD_TYPE.DEGREE:
		{
			DegreeTable.DegreeData data2 = Singleton<DegreeTable>.I.GetData(itemId);
			result = data2.name;
			break;
		}
		case REWARD_TYPE.POINT_SHOP_POINT:
			result = StringTable.Get(STRING_CATEGORY.POINT_SHOP, (uint)((itemId != 1) ? 101 : 100));
			break;
		case REWARD_TYPE.ACCESSORY:
		{
			AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData(itemId);
			if (data != null)
			{
				result = data.name;
			}
			break;
		}
		}
		return result;
	}

	public static string GetDaySuffix(int rank)
	{
		switch (rank)
		{
		case 1:
		case 21:
		case 31:
			return rank + "st";
		case 2:
		case 22:
			return rank + "nd";
		case 3:
		case 23:
			return rank + "rd";
		default:
			return rank + "th";
		}
	}

	public static string TrimText(string text, UILabel lblContainer)
	{
		string text2 = lblContainer.text;
		lblContainer.text = text;
		lblContainer.UpdateNGUIText();
		int num = NGUIText.CalculateOffsetToFit(text);
		lblContainer.text = text2;
		if (num > 0)
		{
			return text.Substring(0, text.Length - Mathf.Clamp(num + 2, 0, text.Length)) + "...";
		}
		return text;
	}

	public static string GetNameWithColoredClanTag(string tag, string name, bool own, bool isSameTeam)
	{
		if (!own && (tag == null || tag == string.Empty || tag == string.Empty))
		{
			return name;
		}
		GuildModel.Guild guildData = MonoBehaviourSingleton<GuildManager>.I.guildData;
		if (!own)
		{
			return GetName(tag, name, isSameTeam);
		}
		if (guildData != null && guildData.clanId != 0)
		{
			return GetName(guildData.tag, name, isSameTeam);
		}
		return name;
	}

	public static string GetName(string tag, string name, bool isSameTeam)
	{
		string text = "08FF00";
		if (isSameTeam)
		{
			return "[" + text + "][[b][/b]" + tag + "][-]" + name;
		}
		return "[[b][/b]" + tag + "]" + name;
	}
}
