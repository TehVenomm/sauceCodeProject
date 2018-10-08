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
		return new Vector2(vector3.x, vector3.y);
	}

	public static Vector2 ToVector2XZ(this Vector3 vector3)
	{
		return new Vector2(vector3.x, vector3.z);
	}

	public static Vector3 ToVector3XY(this Vector2 vector2)
	{
		return new Vector3(vector2.x, vector2.y);
	}

	public static Vector3 ToVector3XY(this Vector2 vector2, float z)
	{
		return new Vector3(vector2.x, vector2.y, z);
	}

	public static Vector3 ToVector3XZ(this Vector2 vector2)
	{
		return new Vector3(vector2.x, 0f, vector2.y);
	}

	public static Color ToColor(this Vector3 vector3)
	{
		return new Color(vector3.x, vector3.y, vector3.z);
	}

	public static Vector3 ToVector3(this Color color)
	{
		return new Vector3(color.r, color.g, color.b);
	}

	public static Vector4 ToVector4(this Vector3 vec3)
	{
		return new Vector4(vec3.x, vec3.y, vec3.z, 1f);
	}

	public static Vector4 ToVector4(this Vector3 vec3, float w)
	{
		return new Vector4(vec3.x, vec3.y, vec3.z, w);
	}

	public static Vector2 ToVector2XY(this Vector4 vec4)
	{
		return new Vector2(vec4.x, vec4.y);
	}

	public static Vector2 ToVector2ZW(this Vector4 vec4)
	{
		return new Vector2(vec4.z, vec4.w);
	}

	public static Vector3 ToVector3(this Vector4 vec4)
	{
		return new Vector3(vec4.x, vec4.y, vec4.z);
	}

	public static Vector3 ToVector3XY(this Vector4 vec4)
	{
		return new Vector3(vec4.x, vec4.y, 0f);
	}

	public static Vector3 GetNearPosOnLine(Vector3 pos_a, Vector3 pos_b, Vector3 pos_p)
	{
		float dist_ax = 0f;
		return GetNearPosOnLine(pos_a, pos_b, pos_p, ref dist_ax);
	}

	public static Vector3 GetNearPosOnLine(Vector3 pos_a, Vector3 pos_b, Vector3 pos_p, ref float dist_ax)
	{
		Vector3 vector = pos_b - pos_a;
		vector.Normalize();
		dist_ax = Vector3.Dot(vector, pos_p - pos_a);
		return pos_a + vector * dist_ax;
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
		float num = Vector2.Angle(p1, p2);
		Vector3 vector = Vector3.Cross(p1, p2);
		if (vector.z > 0f)
		{
			num = 360f - num;
		}
		return num;
	}

	public static float Random(float value)
	{
		return UnityEngine.Random.Range(0f, value);
	}

	public static int Random(int value)
	{
		return UnityEngine.Random.Range(0, value);
	}

	public static float SymmetryRandom(float value)
	{
		return UnityEngine.Random.Range(0f - value, value);
	}

	public static T Lot<T>(T[] ary)
	{
		if (ary == null || ary.Length == 0)
		{
			return default(T);
		}
		return ary[UnityEngine.Random.Range(0, ary.Length)];
	}

	public static int LotIndex(int[] probabilities, int total_probabilities = 100)
	{
		int num = UnityEngine.Random.Range(0, total_probabilities);
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
		return UnityEngine.Random.Range(0, 2) == 0;
	}

	public static bool Dice100(int per)
	{
		return UnityEngine.Random.value * 100f < (float)per;
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
		this_vector3.x *= mul_vector3.x;
		this_vector3.y *= mul_vector3.y;
		this_vector3.z *= mul_vector3.z;
		return this_vector3;
	}

	public static Vector3 Div(this Vector3 this_vector3, Vector3 div_vector3)
	{
		this_vector3.x /= div_vector3.x;
		this_vector3.y /= div_vector3.y;
		this_vector3.z /= div_vector3.z;
		return this_vector3;
	}

	public static void Set(this Transform transform, Vector3 pos, Quaternion rot)
	{
		transform.position = pos;
		transform.rotation = rot;
	}

	public static void Set(this Transform transform, Vector3 pos, Vector3 rot)
	{
		transform.position = pos;
		transform.eulerAngles = rot;
	}

	public static void CopyFrom(this Transform transform, Transform from_transform)
	{
		transform.position = from_transform.position;
		transform.rotation = from_transform.rotation;
	}

	public static void SetActiveChildren(Transform parent, bool is_active)
	{
		if ((UnityEngine.Object)parent != (UnityEngine.Object)null)
		{
			int i = 0;
			for (int childCount = parent.childCount; i < childCount; i++)
			{
				parent.GetChild(i).gameObject.SetActive(is_active);
			}
		}
	}

	public static void ToggleActiveChildren(Transform parent, int index)
	{
		if ((UnityEngine.Object)parent != (UnityEngine.Object)null && index >= 0 && index < parent.childCount)
		{
			int i = 0;
			for (int childCount = parent.childCount; i < childCount; i++)
			{
				parent.GetChild(i).gameObject.SetActive(i == index);
			}
		}
	}

	public static void Destroy(ref Transform t)
	{
		if ((UnityEngine.Object)t != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(t.gameObject);
			t = null;
		}
	}

	public static Transform Find(Transform transform, string name)
	{
		Transform transform2 = FindChild(transform, name);
		if ((UnityEngine.Object)transform2 != (UnityEngine.Object)null)
		{
			return transform2;
		}
		if (transform.name == name)
		{
			return transform;
		}
		return null;
	}

	public static Transform FindChild(Transform transform, string name)
	{
		Transform transform2 = transform.FindChild(name);
		if ((UnityEngine.Object)transform2 != (UnityEngine.Object)null)
		{
			return transform2;
		}
		int i = 0;
		for (int childCount = transform.childCount; i < childCount; i++)
		{
			transform2 = FindChild(transform.GetChild(i), name);
			if ((UnityEngine.Object)transform2 != (UnityEngine.Object)null)
			{
				return transform2;
			}
		}
		return null;
	}

	public static Transform FindActiveChild(Transform transform, string name)
	{
		Transform transform2 = transform.FindChild(name);
		if ((UnityEngine.Object)transform2 != (UnityEngine.Object)null && transform2.gameObject.activeSelf)
		{
			return transform2;
		}
		int i = 0;
		for (int childCount = transform.childCount; i < childCount; i++)
		{
			transform2 = FindChild(transform.GetChild(i), name);
			if ((UnityEngine.Object)transform2 != (UnityEngine.Object)null && transform2.gameObject.activeSelf)
			{
				return transform2;
			}
		}
		return null;
	}

	public static bool ForEach(Transform transform, Predicate<Transform> callback)
	{
		if ((UnityEngine.Object)transform == (UnityEngine.Object)null)
		{
			return false;
		}
		if (callback(transform))
		{
			return true;
		}
		int i = 0;
		for (int childCount = transform.childCount; i < childCount; i++)
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
		Component component = transform.GetComponent(type);
		if ((UnityEngine.Object)component != (UnityEngine.Object)null)
		{
			Push(component);
		}
		int i = 0;
		for (int childCount = transform.childCount; i < childCount; i++)
		{
			_StackComponentInChildren(transform.GetChild(i), type);
		}
	}

	public static void Attach(Transform parent, Transform child)
	{
		Vector3 localPosition = child.localPosition;
		Quaternion localRotation = child.localRotation;
		Vector3 localScale = child.localScale;
		child.parent = parent;
		child.localPosition = localPosition;
		child.localRotation = localRotation;
		child.localScale = localScale;
	}

	public static Transform Insert(Transform child, bool transfom_delegate = false)
	{
		GameObject gameObject = new GameObject();
		Transform transform = gameObject.transform;
		transform.parent = child.parent;
		child.parent = transform;
		if (transfom_delegate)
		{
			transform.localPosition = child.localPosition;
			transform.localRotation = child.localRotation;
			transform.localScale = child.localScale;
			child.localPosition = Vector3.zero;
			child.localRotation = Quaternion.identity;
			child.localScale = Vector3.one;
		}
		return transform;
	}

	public static void SetLayerWithChildren(Transform transform, int layer)
	{
		transform.gameObject.layer = layer;
		int i = 0;
		for (int childCount = transform.childCount; i < childCount; i++)
		{
			SetLayerWithChildren(transform.GetChild(i), layer);
		}
	}

	public static void SetLayerWithChildren(Transform transform, int setLayer, int exceptLayer)
	{
		if (transform.gameObject.layer != exceptLayer)
		{
			transform.gameObject.layer = setLayer;
		}
		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject gameObject = transform.GetChild(i).gameObject;
			if (gameObject.layer != exceptLayer)
			{
				gameObject.layer = setLayer;
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
		int i = 0;
		for (int num = colliders.Length; i < num; i++)
		{
			if (!((UnityEngine.Object)colliders[i] == (UnityEngine.Object)null) && !colliders[i].isTrigger && colliders[i].enabled && colliders[i].gameObject.activeInHierarchy)
			{
				Physics.IgnoreCollision(collider, colliders[i], ignore);
			}
		}
	}

	public static Transform CreateGameObject(string name, Transform parent, int layer = -1)
	{
		GameObject gameObject = new GameObject(name);
		Transform transform = gameObject.transform;
		if ((UnityEngine.Object)parent != (UnityEngine.Object)null)
		{
			Attach(parent, transform);
		}
		if (layer != -1)
		{
			gameObject.layer = layer;
		}
		return transform;
	}

	public static Component CreateGameObjectAndComponent(string name, Transform parent = null, int layer = -1)
	{
		Transform transform = CreateGameObject(name, parent, layer);
		Type type = Type.GetType(name);
		return transform.gameObject.AddComponent(type);
	}

	public static void CreateBoxColliderRing(Transform parent, float radius, int divide_num, float box_height = 3, float box_thick = 3)
	{
		radius += box_thick;
		float y = box_height * 0.5f;
		BoxCollider boxCollider = null;
		BoxCollider x = null;
		BoxCollider boxCollider2 = null;
		Transform transform = null;
		Transform transform2 = null;
		Transform transform3 = null;
		for (int i = 0; i < divide_num; i++)
		{
			float f = (float)i / (float)divide_num * 3.14159274f * 2f;
			boxCollider2 = (BoxCollider)CreateGameObjectAndComponent("BoxCollider", parent, -1);
			transform3 = boxCollider2.transform;
			Vector3 vector2 = transform3.localPosition = new Vector3(Mathf.Cos(f) * radius, 0f, Mathf.Sin(f) * radius);
			transform3.LookAt(vector2 * 0.9f + parent.position);
			vector2.y = y;
			transform3.localPosition = vector2;
			if ((UnityEngine.Object)x != (UnityEngine.Object)null)
			{
				float magnitude = (transform3.localPosition - transform2.localPosition).magnitude;
				boxCollider2.size = new Vector3(magnitude, box_height, box_thick);
			}
			if ((UnityEngine.Object)boxCollider == (UnityEngine.Object)null)
			{
				boxCollider = boxCollider2;
				transform = transform3;
			}
			x = boxCollider2;
			transform2 = transform3;
		}
		if ((UnityEngine.Object)boxCollider != (UnityEngine.Object)null)
		{
			float magnitude2 = (transform.localPosition - transform2.localPosition).magnitude;
			boxCollider.size = new Vector3(magnitude2, box_height, box_thick);
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
			C val = Pop<C>();
			if ((UnityEngine.Object)val == (UnityEngine.Object)null)
			{
				break;
			}
			string name = val.name;
			for (int i = 0; i < num; i++)
			{
				if ((UnityEngine.Object)array[i] == (UnityEngine.Object)null && names[i] == name)
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
				if (!((UnityEngine.Object)renderers[i] == (UnityEngine.Object)null))
				{
					Material[] materials = renderers[i].materials;
					int j = 0;
					for (int num2 = materials.Length; j < num2; j++)
					{
						if ((UnityEngine.Object)materials[j] != (UnityEngine.Object)null)
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
				Material[] sharedMaterials = renderers[i].sharedMaterials;
				int j = 0;
				for (int num2 = sharedMaterials.Length; j < num2; j++)
				{
					if ((UnityEngine.Object)sharedMaterials[j] != (UnityEngine.Object)null)
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
		float min = -80f;
		float num = 20f;
		float num2 = Mathf.Clamp(dB, min, num);
		float value = Mathf.Pow(10f, num2 / num);
		return Mathf.Clamp01(value);
	}

	public static float HorizontalToVerticalFOV(float Horizontal_fov)
	{
		float num = (float)Screen.height / (float)Screen.width;
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
		value = defaultValue(key);
		dict.Add(key, value);
		return value;
	}

	public static TValue GetValueOrAddNew<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key) where TValue : new()
	{
		return dict.GetValueOrAddDefault(key, (TKey k) => new TValue());
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
		SphereCollider sphereCollider = to_collider as SphereCollider;
		if ((UnityEngine.Object)sphereCollider != (UnityEngine.Object)null)
		{
			Vector3 vector = sphereCollider.transform.worldToLocalMatrix.MultiplyPoint3x4(point);
			vector -= sphereCollider.center;
			vector.Normalize();
			vector *= sphereCollider.radius;
			vector += sphereCollider.center;
			return sphereCollider.transform.localToWorldMatrix.MultiplyPoint3x4(vector);
		}
		CapsuleCollider capsuleCollider = to_collider as CapsuleCollider;
		if ((UnityEngine.Object)capsuleCollider != (UnityEngine.Object)null)
		{
			Vector3 vector2 = capsuleCollider.transform.worldToLocalMatrix.MultiplyPoint3x4(point);
			vector2 -= capsuleCollider.center;
			float num = vector2[capsuleCollider.direction];
			float num2 = capsuleCollider.height * 0.5f - capsuleCollider.radius;
			int direction;
			float num3;
			if (Mathf.Abs(num) > num2)
			{
				num = Mathf.Sign(num) * num2;
				ref Vector3 reference;
				ref Vector3 reference2 = ref reference = ref vector2;
				int index = direction = capsuleCollider.direction;
				num3 = reference[direction];
				reference2[index] = num3 - num;
			}
			else
			{
				vector2[capsuleCollider.direction] = 0f;
			}
			vector2.Normalize();
			vector2 *= capsuleCollider.radius;
			ref Vector3 reference3;
			ref Vector3 reference4 = ref reference3 = ref vector2;
			int index2 = direction = capsuleCollider.direction;
			num3 = reference3[direction];
			reference4[index2] = num3 + num;
			vector2 += capsuleCollider.center;
			return capsuleCollider.transform.localToWorldMatrix.MultiplyPoint3x4(vector2);
		}
		BoxCollider boxCollider = to_collider as BoxCollider;
		if ((UnityEngine.Object)boxCollider != (UnityEngine.Object)null)
		{
			Vector3 vector3 = boxCollider.transform.worldToLocalMatrix.MultiplyPoint3x4(point);
			vector3 -= boxCollider.center;
			Vector3 zero = Vector3.zero;
			float num4 = Mathf.Abs(vector3.x);
			Vector3 size = boxCollider.size;
			zero.x = num4 - size.x * 0.5f;
			float num5 = Mathf.Abs(vector3.y);
			Vector3 size2 = boxCollider.size;
			zero.y = num5 - size2.y * 0.5f;
			float num6 = Mathf.Abs(vector3.z);
			Vector3 size3 = boxCollider.size;
			zero.z = num6 - size3.z * 0.5f;
			int num7 = -1;
			if (zero.x < 0f && zero.y < 0f && zero.z < 0f)
			{
				float num8 = 0f;
				for (int i = 0; i < 3; i++)
				{
					if (i == 0 || num8 < zero[i])
					{
						num7 = i;
						num8 = zero[i];
					}
				}
			}
			if (zero.x > 0f || num7 == 0)
			{
				float num9 = Mathf.Sign(vector3.x);
				Vector3 size4 = boxCollider.size;
				vector3.x = num9 * size4.x * 0.5f;
			}
			if (zero.y > 0f || num7 == 1)
			{
				float num10 = Mathf.Sign(vector3.y);
				Vector3 size5 = boxCollider.size;
				vector3.y = num10 * size5.y * 0.5f;
			}
			if (zero.z > 0f || num7 == 2)
			{
				float num11 = Mathf.Sign(vector3.z);
				Vector3 size6 = boxCollider.size;
				vector3.z = num11 * size6.z * 0.5f;
			}
			vector3 += boxCollider.center;
			return boxCollider.transform.localToWorldMatrix.MultiplyPoint3x4(vector3);
		}
		return to_collider.ClosestPointOnBounds(point);
	}

	public static Vector3 ClosestPointOnColliderFix(Collider to_collider, Vector3 point)
	{
		Vector3 b = Vector3.zero;
		if (to_collider is BoxCollider)
		{
			b = (to_collider as BoxCollider).center;
		}
		else if (to_collider is SphereCollider)
		{
			b = (to_collider as SphereCollider).center;
		}
		else if (to_collider is CapsuleCollider)
		{
			b = (to_collider as CapsuleCollider).center;
		}
		Vector3 normalized = (to_collider.transform.position + b - point).normalized;
		if (!to_collider.Raycast(new Ray(point, normalized), out RaycastHit _, 3.40282347E+38f))
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
			Debug.Log(list[i].ToString());
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
		movieName = Application.temporaryCachePath + "/" + movieName;
		Handheld.PlayFullScreenMovie(movieName, Color.black, FullScreenMovieControlMode.Full);
	}

	public static float GetiOSVersion()
	{
		return -1f;
	}

	public static Vector3 GetScreenUIPosition(Camera camera, Transform cam_transform, Vector3 pos)
	{
		Vector3 vector = cam_transform.worldToLocalMatrix.MultiplyPoint3x4(pos);
		float z = vector.z;
		vector.z = 0f;
		float magnitude = vector.magnitude;
		float num = 1f;
		if (tempCameraInfo == null || tempCameraInfo.screenWidth != Screen.width || tempCameraInfo.screenHeight != Screen.height || tempCameraInfo.fov != camera.fieldOfView)
		{
			float num2 = (float)Screen.height * 0.5f;
			float num3 = Mathf.Sqrt((float)(Screen.height * Screen.height + Screen.width * Screen.width)) * 0.5f;
			float num4 = camera.fieldOfView * 0.5f;
			num = Mathf.Tan(num4 * 0.0174532924f) * num3 / num2;
			tempCameraInfo = new TempCameraInfo();
			tempCameraInfo.screenWidth = Screen.width;
			tempCameraInfo.screenHeight = Screen.height;
			tempCameraInfo.fov = camera.fieldOfView;
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
			vector *= num6 / magnitude;
			vector.z = num5;
		}
		else
		{
			vector *= num5 / z;
			vector.z = num5;
		}
		pos = cam_transform.localToWorldMatrix.MultiplyPoint3x4(vector);
		Vector3 result = camera.WorldToScreenPoint(pos);
		result.z = z;
		return result;
	}

	public static Color MakeColorByInt(int r, int g, int b, int a = 255)
	{
		return new Color((float)r / 255f, (float)g / 255f, (float)b / 255f, (float)a / 255f);
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
		action?.Invoke();
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
		if (absolutepath.StartsWith(Application.dataPath))
		{
			return "Assets" + absolutepath.Substring(Application.dataPath.Length);
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
