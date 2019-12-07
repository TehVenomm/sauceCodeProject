using Network;
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
		string text = "";
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			text += _ToJoinString(array[i], split, format, i == num - 1);
		}
		return text;
	}

	public static string ToJoinString<T>(this List<T> list, string split = ",", string format = null)
	{
		string text = "";
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			text += _ToJoinString(list[i], split, format, i == count - 1);
		}
		return text;
	}

	private static string _ToJoinString<T>(T element, string split, string format, bool last)
	{
		string str = "";
		str = ((format != null && element is int) ? (str + ((int)(object)element).ToString(format)) : ((format == null || !(element is float)) ? (str + element.ToString()) : (str + ((float)(object)element).ToString(format))));
		if (!last)
		{
			str += split;
		}
		return str;
	}

	public static float Angle360(Vector2 p1, Vector2 p2)
	{
		float num = Vector2.Angle(p1, p2);
		if (Vector3.Cross(p1, p2).z > 0f)
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
		if (parent != null)
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
		if (parent != null && index >= 0 && index < parent.childCount)
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
		if (t != null)
		{
			UnityEngine.Object.Destroy(t.gameObject);
			t = null;
		}
	}

	public static Transform Find(Transform transform, string name)
	{
		if (transform == null)
		{
			return null;
		}
		Transform transform2 = FindChild(transform, name);
		if (transform2 != null)
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
		if (transform == null)
		{
			return null;
		}
		Transform transform2 = transform.Find(name);
		if (transform2 != null)
		{
			return transform2;
		}
		int i = 0;
		for (int childCount = transform.childCount; i < childCount; i++)
		{
			transform2 = FindChild(transform.GetChild(i), name);
			if (transform2 != null)
			{
				return transform2;
			}
		}
		return null;
	}

	public static Transform FindActiveChild(Transform transform, string name)
	{
		Transform transform2 = transform.Find(name);
		if (transform2 != null && transform2.gameObject.activeSelf)
		{
			return transform2;
		}
		int i = 0;
		for (int childCount = transform.childCount; i < childCount; i++)
		{
			transform2 = FindChild(transform.GetChild(i), name);
			if (transform2 != null && transform2.gameObject.activeSelf)
			{
				return transform2;
			}
		}
		return null;
	}

	public static bool ForEach(Transform transform, Predicate<Transform> callback)
	{
		if (transform == null)
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
		if (component != null)
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
		Transform transform = new GameObject().transform;
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
				Physics.IgnoreLayerCollision(num, i, ignore: true);
			}
		}
	}

	public static void SetCollideLayers(int target_layer, params int[] hit_layers)
	{
		int i = 0;
		for (int num = hit_layers.Length; i < num; i++)
		{
			Physics.IgnoreLayerCollision(target_layer, hit_layers[i], ignore: false);
		}
	}

	public static void IgnoreCollision(Collider collider, Collider[] colliders, bool ignore)
	{
		int i = 0;
		for (int num = colliders.Length; i < num; i++)
		{
			if (!(colliders[i] == null) && !colliders[i].isTrigger && colliders[i].enabled && colliders[i].gameObject.activeInHierarchy)
			{
				Physics.IgnoreCollision(collider, colliders[i], ignore);
			}
		}
	}

	public static Transform CreateGameObject(string name, Transform parent, int layer = -1)
	{
		GameObject gameObject = new GameObject(name);
		Transform transform = gameObject.transform;
		if (parent != null)
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

	public static T CreateGameObjectAndComponent<T>(Transform parent = null, int layer = -1) where T : Component
	{
		return CreateGameObject(typeof(T).Name, parent, layer).gameObject.AddComponent<T>();
	}

	public static void CreateBoxColliderRing(Transform parent, float radius, int divide_num, float box_height = 3f, float box_thick = 3f)
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
			float f = (float)i / (float)divide_num * (float)Math.PI * 2f;
			boxCollider2 = (BoxCollider)CreateGameObjectAndComponent("BoxCollider", parent);
			transform3 = boxCollider2.transform;
			Vector3 vector2 = transform3.localPosition = new Vector3(Mathf.Cos(f) * radius, 0f, Mathf.Sin(f) * radius);
			transform3.LookAt(vector2 * 0.9f + parent.position);
			vector2.y = y;
			transform3.localPosition = vector2;
			if (x != null)
			{
				float magnitude = (transform3.localPosition - transform2.localPosition).magnitude;
				boxCollider2.size = new Vector3(magnitude, box_height, box_thick);
			}
			if (boxCollider == null)
			{
				boxCollider = boxCollider2;
				transform = transform3;
			}
			x = boxCollider2;
			transform2 = transform3;
		}
		if (boxCollider != null)
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
		if (renderers == null)
		{
			return;
		}
		int i = 0;
		for (int num = renderers.Length; i < num; i++)
		{
			if (renderers[i] == null)
			{
				continue;
			}
			Material[] materials = renderers[i].materials;
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

	public static void SharedMaterialForEach(Renderer[] renderers, Action<Material> callback)
	{
		if (renderers == null)
		{
			return;
		}
		int i = 0;
		for (int num = renderers.Length; i < num; i++)
		{
			Material[] sharedMaterials = renderers[i].sharedMaterials;
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
		return Mathf.Clamp01(Mathf.Pow(10f, num2 / num));
	}

	public static float HorizontalToVerticalFOV(float Horizontal_fov)
	{
		float num = (float)Screen.height / (float)Screen.width;
		if (num < 1f)
		{
			num = 1f;
		}
		float num2 = num * 9f / 16f;
		return Mathf.Atan(Mathf.Tan(Horizontal_fov * 0.5f * ((float)Math.PI / 180f)) * num2) * 57.29578f * 2f;
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
		if (sphereCollider != null)
		{
			Vector3 point2 = sphereCollider.transform.worldToLocalMatrix.MultiplyPoint3x4(point);
			point2 -= sphereCollider.center;
			point2.Normalize();
			point2 *= sphereCollider.radius;
			point2 += sphereCollider.center;
			return sphereCollider.transform.localToWorldMatrix.MultiplyPoint3x4(point2);
		}
		CapsuleCollider capsuleCollider = to_collider as CapsuleCollider;
		if (capsuleCollider != null)
		{
			Vector3 point3 = capsuleCollider.transform.worldToLocalMatrix.MultiplyPoint3x4(point);
			point3 -= capsuleCollider.center;
			float num = point3[capsuleCollider.direction];
			float num2 = capsuleCollider.height * 0.5f - capsuleCollider.radius;
			if (Mathf.Abs(num) > num2)
			{
				num = Mathf.Sign(num) * num2;
				point3[capsuleCollider.direction] -= num;
			}
			else
			{
				point3[capsuleCollider.direction] = 0f;
			}
			point3.Normalize();
			point3 *= capsuleCollider.radius;
			point3[capsuleCollider.direction] += num;
			point3 += capsuleCollider.center;
			return capsuleCollider.transform.localToWorldMatrix.MultiplyPoint3x4(point3);
		}
		BoxCollider boxCollider = to_collider as BoxCollider;
		if (boxCollider != null)
		{
			Vector3 point4 = boxCollider.transform.worldToLocalMatrix.MultiplyPoint3x4(point);
			point4 -= boxCollider.center;
			Vector3 zero = Vector3.zero;
			zero.x = Mathf.Abs(point4.x) - boxCollider.size.x * 0.5f;
			zero.y = Mathf.Abs(point4.y) - boxCollider.size.y * 0.5f;
			zero.z = Mathf.Abs(point4.z) - boxCollider.size.z * 0.5f;
			int num3 = -1;
			if (zero.x < 0f && zero.y < 0f && zero.z < 0f)
			{
				float num4 = 0f;
				for (int i = 0; i < 3; i++)
				{
					if (i == 0 || num4 < zero[i])
					{
						num3 = i;
						num4 = zero[i];
					}
				}
			}
			if (zero.x > 0f || num3 == 0)
			{
				point4.x = Mathf.Sign(point4.x) * boxCollider.size.x * 0.5f;
			}
			if (zero.y > 0f || num3 == 1)
			{
				point4.y = Mathf.Sign(point4.y) * boxCollider.size.y * 0.5f;
			}
			if (zero.z > 0f || num3 == 2)
			{
				point4.z = Mathf.Sign(point4.z) * boxCollider.size.z * 0.5f;
			}
			point4 += boxCollider.center;
			return boxCollider.transform.localToWorldMatrix.MultiplyPoint3x4(point4);
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
		if (!to_collider.Raycast(new Ray(point, normalized), out RaycastHit _, float.MaxValue))
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
		foreach (FieldInfo fieldInfo in fields)
		{
			string text = parentName + "." + fieldInfo.Name;
			object value = fieldInfo.GetValue(obj);
			if (value == null)
			{
				list.Add($"{text}=(null)");
				continue;
			}
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
		Vector3 point = cam_transform.worldToLocalMatrix.MultiplyPoint3x4(pos);
		float z = point.z;
		point.z = 0f;
		float magnitude = point.magnitude;
		float num = 1f;
		if (tempCameraInfo == null || tempCameraInfo.screenWidth != Screen.width || tempCameraInfo.screenHeight != Screen.height || tempCameraInfo.fov != camera.fieldOfView)
		{
			float num2 = (float)Screen.height * 0.5f;
			float num3 = Mathf.Sqrt(Screen.height * Screen.height + Screen.width * Screen.width) * 0.5f;
			num = Mathf.Tan(camera.fieldOfView * 0.5f * ((float)Math.PI / 180f)) * num3 / num2;
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
		float num4 = 1f;
		float num5 = num4 * num;
		if (z <= 0f || num5 < magnitude * num4 / z)
		{
			point *= num5 / magnitude;
			point.z = num4;
		}
		else
		{
			point *= num4 / z;
			point.z = num4;
		}
		pos = cam_transform.localToWorldMatrix.MultiplyPoint3x4(point);
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
		foreach (IEnumerable<T> enumerable in paramArray)
		{
			foreach (T item in enumerable)
			{
				yield return item;
			}
		}
	}

	public static IEnumerable<T> GetAllCompornent<T>(this Component root)
	{
		foreach (T item in MultipleEnumerator<T>(root.GetComponents<T>(), root.GetComponentsInChildren<T>(includeInactive: true)))
		{
			yield return item;
		}
	}

	public static IEnumerable<T> GetAllCompornent<T>(this GameObject root)
	{
		foreach (T item in MultipleEnumerator<T>(root.GetComponents<T>(), root.GetComponentsInChildren<T>(includeInactive: true)))
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
		if ((0x10 & spAtkEnableBit) != 0)
		{
			flag2 |= (spAttackType == SP_ATTACK_TYPE.ORACLE);
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
		return null;
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
		if (self != null)
		{
			return self.Trim() == "";
		}
		return true;
	}

	public static bool ContainIgnoreCase(this string self, string value)
	{
		return self.ToLower().Contains(value.ToLower());
	}

	public static bool IsNullOrEmpty<T>(this IList<T> self)
	{
		if (self != null)
		{
			return self.Count == 0;
		}
		return true;
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
		if (num != 0)
		{
			return (int)Mathf.Log10(num) + 1;
		}
		return 1;
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
			result = Singleton<ItemTable>.I.GetItemData(itemId).name;
			break;
		case REWARD_TYPE.EQUIP_ITEM:
			result = Singleton<EquipItemTable>.I.GetEquipItemData(itemId).name;
			break;
		case REWARD_TYPE.SKILL_ITEM:
			result = Singleton<SkillItemTable>.I.GetSkillItemData(itemId).name;
			break;
		case REWARD_TYPE.QUEST_ITEM:
			result = Singleton<QuestTable>.I.GetQuestData(itemId).questText;
			break;
		case REWARD_TYPE.AVATAR:
			result = Singleton<AvatarTable>.I.GetData(itemId).name;
			break;
		case REWARD_TYPE.STAMP:
			result = Singleton<StampTable>.I.GetData(itemId).desc;
			break;
		case REWARD_TYPE.DEGREE:
			result = Singleton<DegreeTable>.I.GetData(itemId).name;
			break;
		case REWARD_TYPE.POINT_SHOP_POINT:
			result = StringTable.Get(STRING_CATEGORY.POINT_SHOP, (itemId == 1) ? 100u : 101u);
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

	public static ELEMENT_TYPE GetAntiElementType(ELEMENT_TYPE type)
	{
		switch (type)
		{
		case ELEMENT_TYPE.FIRE:
			return ELEMENT_TYPE.WATER;
		case ELEMENT_TYPE.WATER:
			return ELEMENT_TYPE.THUNDER;
		case ELEMENT_TYPE.THUNDER:
			return ELEMENT_TYPE.SOIL;
		case ELEMENT_TYPE.SOIL:
			return ELEMENT_TYPE.FIRE;
		case ELEMENT_TYPE.LIGHT:
			return ELEMENT_TYPE.DARK;
		case ELEMENT_TYPE.DARK:
			return ELEMENT_TYPE.LIGHT;
		default:
			return ELEMENT_TYPE.MAX;
		}
	}

	public static ELEMENT_TYPE GetEffectiveElementType(ELEMENT_TYPE type)
	{
		switch (type)
		{
		case ELEMENT_TYPE.FIRE:
			return ELEMENT_TYPE.SOIL;
		case ELEMENT_TYPE.WATER:
			return ELEMENT_TYPE.FIRE;
		case ELEMENT_TYPE.THUNDER:
			return ELEMENT_TYPE.WATER;
		case ELEMENT_TYPE.SOIL:
			return ELEMENT_TYPE.THUNDER;
		case ELEMENT_TYPE.LIGHT:
			return ELEMENT_TYPE.DARK;
		case ELEMENT_TYPE.DARK:
			return ELEMENT_TYPE.LIGHT;
		default:
			return ELEMENT_TYPE.MAX;
		}
	}

	public static void UpdateAllAnchors(GameObject gameObject)
	{
		if (!(gameObject == null))
		{
			UIWidget component = gameObject.GetComponent<UIWidget>();
			if (component != null)
			{
				component.UpdateAnchors();
			}
			int childCount = gameObject.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				UpdateAllAnchors(gameObject.transform.GetChild(i).gameObject);
			}
		}
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
		if (!own && (tag == null || tag == string.Empty || tag == ""))
		{
			return name;
		}
		ClanData clanData = MonoBehaviourSingleton<ClanMatchingManager>.I.clanData;
		if (own)
		{
			if (clanData != null && !string.IsNullOrEmpty(clanData.cId))
			{
				return GetName(clanData.tag, name, isSameTeam);
			}
			return name;
		}
		return GetName(tag, name, isSameTeam);
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

	public static bool IsScreenHD()
	{
		int width = Screen.width;
		int num = Screen.height;
		if (width > num)
		{
			int num2 = width;
			width = num;
			num = num2;
		}
		if (num >= 1280)
		{
			return true;
		}
		return false;
	}

	public static int GetTipTypeFromTutorial()
	{
		if (!PlayerPrefs.HasKey("Tut_Weapon_Type"))
		{
			return -1;
		}
		int @int = PlayerPrefs.GetInt("Tut_Weapon_Type");
		UnityEngine.Random.Range(1, 3);
		switch (@int)
		{
		case 0:
			return UnityEngine.Random.Range(1, 3);
		case 1:
			return UnityEngine.Random.Range(5, 8);
		case 2:
			return UnityEngine.Random.Range(8, 10);
		case 4:
			return UnityEngine.Random.Range(10, 12);
		case 5:
			return UnityEngine.Random.Range(12, 15);
		default:
			return -1;
		}
	}

	public static string GetTrimLineText(int maxLine, string text)
	{
		int num = 0;
		for (int i = 0; i < text.Length; i++)
		{
			if (text[i] == '\n')
			{
				num++;
			}
			if (num == maxLine && i + 1 < text.Length)
			{
				return text.Substring(0, i) + "...";
			}
		}
		return text;
	}
}
