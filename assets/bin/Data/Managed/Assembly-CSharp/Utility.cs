// Decompiled with JetBrains decompiler
// Type: Utility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

#nullable disable
public static class Utility
{
  public static List<object> stack;
  public static int stackPointer;
  private static Utility.TempCameraInfo tempCameraInfo;
  private static Vector3 tempScreenPos;
  public static CustomBillboard customBillboard;

  public static void Initialize()
  {
    Utility.stack = new List<object>(32 /*0x20*/);
    Utility.stackPointer = 0;
  }

  public static void Push(object obj)
  {
    if (Utility.stack.Count == Utility.stackPointer)
      Utility.stack.Add(obj);
    else
      Utility.stack[Utility.stackPointer] = obj;
    ++Utility.stackPointer;
  }

  public static T Pop<T>() where T : class
  {
    --Utility.stackPointer;
    object obj = Utility.stack[Utility.stackPointer];
    Utility.stack[Utility.stackPointer] = (object) null;
    return obj as T;
  }

  public static int GetCurrentSecondFromNow(DateTime dt)
  {
    return (int) Utility.GetCurrentTimeSpanFromNow(dt).TotalSeconds;
  }

  public static TimeSpan GetCurrentTimeSpanFromNow(DateTime dt) => dt - DateTime.UtcNow;

  public static double DateTimeToTimestampMilliseconds(DateTime time)
  {
    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    return (time - dateTime).TotalMilliseconds;
  }

  public static Vector2 ToVector2XY(this Vector3 vector3) => new Vector2(vector3.x, vector3.y);

  public static Vector2 ToVector2XZ(this Vector3 vector3) => new Vector2(vector3.x, vector3.z);

  public static Vector3 ToVector3XY(this Vector2 vector2) => new Vector3(vector2.x, vector2.y);

  public static Vector3 ToVector3XY(this Vector2 vector2, float z)
  {
    return new Vector3(vector2.x, vector2.y, z);
  }

  public static Vector3 ToVector3XZ(this Vector2 vector2)
  {
    return new Vector3(vector2.x, 0.0f, vector2.y);
  }

  public static Color ToColor(this Vector3 vector3) => new Color(vector3.x, vector3.y, vector3.z);

  public static Vector3 ToVector3(this Color color) => new Vector3(color.r, color.g, color.b);

  public static Vector4 ToVector4(this Vector3 vec3) => new Vector4(vec3.x, vec3.y, vec3.z, 1f);

  public static Vector4 ToVector4(this Vector3 vec3, float w)
  {
    return new Vector4(vec3.x, vec3.y, vec3.z, w);
  }

  public static Vector2 ToVector2XY(this Vector4 vec4) => new Vector2(vec4.x, vec4.y);

  public static Vector2 ToVector2ZW(this Vector4 vec4) => new Vector2(vec4.z, vec4.w);

  public static Vector3 ToVector3(this Vector4 vec4) => new Vector3(vec4.x, vec4.y, vec4.z);

  public static Vector3 ToVector3XY(this Vector4 vec4) => new Vector3(vec4.x, vec4.y, 0.0f);

  public static Vector3 GetNearPosOnLine(Vector3 pos_a, Vector3 pos_b, Vector3 pos_p)
  {
    float dist_ax = 0.0f;
    return Utility.GetNearPosOnLine(pos_a, pos_b, pos_p, ref dist_ax);
  }

  public static Vector3 GetNearPosOnLine(
    Vector3 pos_a,
    Vector3 pos_b,
    Vector3 pos_p,
    ref float dist_ax)
  {
    Vector3 vector3 = Vector3.op_Subtraction(pos_b, pos_a);
    ((Vector3) ref vector3).Normalize();
    dist_ax = Vector3.Dot(vector3, Vector3.op_Subtraction(pos_p, pos_a));
    return Vector3.op_Addition(pos_a, Vector3.op_Multiply(vector3, dist_ax));
  }

  public static string ToJoinString<T>(this T[] array, string split = ",", string format = null)
  {
    string joinString = "";
    int index = 0;
    for (int length = array.Length; index < length; ++index)
      joinString += Utility._ToJoinString<T>(array[index], split, format, index == length - 1);
    return joinString;
  }

  public static string ToJoinString<T>(this List<T> list, string split = ",", string format = null)
  {
    string joinString = "";
    int index = 0;
    for (int count = list.Count; index < count; ++index)
      joinString += Utility._ToJoinString<T>(list[index], split, format, index == count - 1);
    return joinString;
  }

  private static string _ToJoinString<T>(T element, string split, string format, bool last)
  {
    string str = "";
    string joinString;
    if (format != null && (object) element is int)
    {
      int num = (int) (object) element;
      joinString = str + num.ToString(format);
    }
    else if (format != null && (object) element is float)
    {
      float num = (float) (object) element;
      joinString = str + num.ToString(format);
    }
    else
      joinString = str + element.ToString();
    if (!last)
      joinString += split;
    return joinString;
  }

  public static float Angle360(Vector2 p1, Vector2 p2)
  {
    float num = Vector2.Angle(p1, p2);
    if ((double) Vector3.Cross(Vector2.op_Implicit(p1), Vector2.op_Implicit(p2)).z > 0.0)
      num = 360f - num;
    return num;
  }

  public static float Random(float value) => UnityEngine.Random.Range(0.0f, value);

  public static int Random(int value) => UnityEngine.Random.Range(0, value);

  public static float SymmetryRandom(float value) => UnityEngine.Random.Range(-value, value);

  public static T Lot<T>(T[] ary)
  {
    return ary == null || ary.Length == 0 ? default (T) : ary[UnityEngine.Random.Range(0, ary.Length)];
  }

  public static int LotIndex(int[] probabilities, int total_probabilities = 100)
  {
    int num1 = UnityEngine.Random.Range(0, total_probabilities);
    int index = 0;
    int length = probabilities.Length;
    int num2 = 0;
    for (; index < length; ++index)
    {
      num2 += probabilities[index];
      if (num1 < num2)
        return index;
    }
    return probabilities.Length;
  }

  public static bool Coin() => UnityEngine.Random.Range(0, 2) == 0;

  public static bool Dice100(int per) => (double) UnityEngine.Random.value * 100.0 < (double) per;

  public static T[] CreateMergedArray<T>(T[] array_a, T[] array_b)
  {
    int index = 0;
    if (array_a != null)
      index = array_a.Length;
    int num = 0;
    if (array_b != null)
      num = array_b.Length;
    T[] mergedArray = new T[index + num];
    array_a?.CopyTo((Array) mergedArray, 0);
    array_b?.CopyTo((Array) mergedArray, index);
    return mergedArray;
  }

  public static T[] DistinctArray<T>(T[] array)
  {
    return array == null ? (T[]) null : ((IEnumerable<T>) array).Distinct<T>().ToArray<T>();
  }

  public static uint GetHash(string str)
  {
    uint hash = 0;
    int index = 0;
    for (int length = str.Length; index < length; ++index)
      hash = (uint) ((int) hash << 1 | (int) (hash >> 31 /*0x1F*/) & 1) + (uint) str[index];
    return hash;
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
    if (!Object.op_Inequality((Object) parent, (Object) null))
      return;
    int num = 0;
    for (int childCount = parent.childCount; num < childCount; ++num)
      ((Component) parent.GetChild(num)).gameObject.SetActive(is_active);
  }

  public static void ToggleActiveChildren(Transform parent, int index)
  {
    if (!Object.op_Inequality((Object) parent, (Object) null) || index < 0 || index >= parent.childCount)
      return;
    int num = 0;
    for (int childCount = parent.childCount; num < childCount; ++num)
      ((Component) parent.GetChild(num)).gameObject.SetActive(num == index);
  }

  public static void Destroy(ref Transform t)
  {
    if (!Object.op_Inequality((Object) t, (Object) null))
      return;
    Object.Destroy((Object) ((Component) t).gameObject);
    t = (Transform) null;
  }

  public static Transform Find(Transform transform, string name)
  {
    if (Object.op_Equality((Object) transform, (Object) null))
      return (Transform) null;
    Transform child = Utility.FindChild(transform, name);
    if (Object.op_Inequality((Object) child, (Object) null))
      return child;
    return ((Object) transform).name == name ? transform : (Transform) null;
  }

  public static Transform FindChild(Transform transform, string name)
  {
    if (Object.op_Equality((Object) transform, (Object) null))
      return (Transform) null;
    Transform child1 = transform.Find(name);
    if (Object.op_Inequality((Object) child1, (Object) null))
      return child1;
    int num = 0;
    for (int childCount = transform.childCount; num < childCount; ++num)
    {
      Transform child2 = Utility.FindChild(transform.GetChild(num), name);
      if (Object.op_Inequality((Object) child2, (Object) null))
        return child2;
    }
    return (Transform) null;
  }

  public static Transform FindActiveChild(Transform transform, string name)
  {
    Transform activeChild = transform.Find(name);
    if (Object.op_Inequality((Object) activeChild, (Object) null) && ((Component) activeChild).gameObject.activeSelf)
      return activeChild;
    int num = 0;
    for (int childCount = transform.childCount; num < childCount; ++num)
    {
      Transform child = Utility.FindChild(transform.GetChild(num), name);
      if (Object.op_Inequality((Object) child, (Object) null) && ((Component) child).gameObject.activeSelf)
        return child;
    }
    return (Transform) null;
  }

  public static bool ForEach(Transform transform, Predicate<Transform> callback)
  {
    if (Object.op_Equality((Object) transform, (Object) null))
      return false;
    if (callback(transform))
      return true;
    int num = 0;
    for (int childCount = transform.childCount; num < childCount; ++num)
    {
      if (Utility.ForEach(transform.GetChild(num), callback))
        return true;
    }
    return false;
  }

  public static void StackComponentInChildren<C>(Transform transform) where C : Component
  {
    Utility.Push((object) null);
    Utility._StackComponentInChildren(transform, typeof (C));
  }

  private static void _StackComponentInChildren(Transform transform, System.Type type)
  {
    Component component = ((Component) transform).GetComponent(type);
    if (Object.op_Inequality((Object) component, (Object) null))
      Utility.Push((object) component);
    int num = 0;
    for (int childCount = transform.childCount; num < childCount; ++num)
      Utility._StackComponentInChildren(transform.GetChild(num), type);
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
    ((Component) transform).gameObject.layer = layer;
    int num = 0;
    for (int childCount = transform.childCount; num < childCount; ++num)
      Utility.SetLayerWithChildren(transform.GetChild(num), layer);
  }

  public static void SetLayerWithChildren(Transform transform, int setLayer, int exceptLayer)
  {
    if (((Component) transform).gameObject.layer != exceptLayer)
      ((Component) transform).gameObject.layer = setLayer;
    for (int index = 0; index < transform.childCount; ++index)
    {
      GameObject gameObject = ((Component) transform.GetChild(index)).gameObject;
      if (gameObject.layer != exceptLayer)
        gameObject.layer = setLayer;
    }
  }

  public static void SetAllNotCollideLayers()
  {
    for (int index1 = 0; index1 < 32 /*0x20*/; ++index1)
    {
      for (int index2 = 31 /*0x1F*/; index2 >= index1; --index2)
        Physics.IgnoreLayerCollision(index2, index1, true);
    }
  }

  public static void SetCollideLayers(int target_layer, params int[] hit_layers)
  {
    int index = 0;
    for (int length = hit_layers.Length; index < length; ++index)
      Physics.IgnoreLayerCollision(target_layer, hit_layers[index], false);
  }

  public static void IgnoreCollision(Collider collider, Collider[] colliders, bool ignore)
  {
    int index = 0;
    for (int length = colliders.Length; index < length; ++index)
    {
      if (!Object.op_Equality((Object) colliders[index], (Object) null) && !colliders[index].isTrigger && colliders[index].enabled && ((Component) colliders[index]).gameObject.activeInHierarchy)
        Physics.IgnoreCollision(collider, colliders[index], ignore);
    }
  }

  public static Transform CreateGameObject(string name, Transform parent, int layer = -1)
  {
    GameObject gameObject = new GameObject(name);
    Transform transform = gameObject.transform;
    if (Object.op_Inequality((Object) parent, (Object) null))
      Utility.Attach(parent, transform);
    if (layer != -1)
      gameObject.layer = layer;
    return transform;
  }

  public static Component CreateGameObjectAndComponent(string name, Transform parent = null, int layer = -1)
  {
    return ((Component) Utility.CreateGameObject(name, parent, layer)).gameObject.AddComponent(System.Type.GetType(name));
  }

  public static T CreateGameObjectAndComponent<T>(Transform parent = null, int layer = -1) where T : Component
  {
    return ((Component) Utility.CreateGameObject(typeof (T).Name, parent, layer)).gameObject.AddComponent<T>();
  }

  public static void CreateBoxColliderRing(
    Transform parent,
    float radius,
    int divide_num,
    float box_height = 3f,
    float box_thick = 3f)
  {
    radius += box_thick;
    float num1 = box_height * 0.5f;
    BoxCollider boxCollider1 = (BoxCollider) null;
    BoxCollider boxCollider2 = (BoxCollider) null;
    Transform transform1 = (Transform) null;
    Transform transform2 = (Transform) null;
    for (int index = 0; index < divide_num; ++index)
    {
      float num2 = (float) ((double) index / (double) divide_num * 3.1415927410125732 * 2.0);
      BoxCollider objectAndComponent = (BoxCollider) Utility.CreateGameObjectAndComponent("BoxCollider", parent);
      Transform transform3 = ((Component) objectAndComponent).transform;
      Vector3 vector3_1;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3_1).\u002Ector(Mathf.Cos(num2) * radius, 0.0f, Mathf.Sin(num2) * radius);
      transform3.localPosition = vector3_1;
      transform3.LookAt(Vector3.op_Addition(Vector3.op_Multiply(vector3_1, 0.9f), parent.position));
      vector3_1.y = num1;
      transform3.localPosition = vector3_1;
      if (Object.op_Inequality((Object) boxCollider2, (Object) null))
      {
        Vector3 vector3_2 = Vector3.op_Subtraction(transform3.localPosition, transform2.localPosition);
        float magnitude = ((Vector3) ref vector3_2).magnitude;
        objectAndComponent.size = new Vector3(magnitude, box_height, box_thick);
      }
      if (Object.op_Equality((Object) boxCollider1, (Object) null))
      {
        boxCollider1 = objectAndComponent;
        transform1 = transform3;
      }
      boxCollider2 = objectAndComponent;
      transform2 = transform3;
    }
    if (!Object.op_Inequality((Object) boxCollider1, (Object) null))
      return;
    Vector3 vector3 = Vector3.op_Subtraction(transform1.localPosition, transform2.localPosition);
    float magnitude1 = ((Vector3) ref vector3).magnitude;
    boxCollider1.size = new Vector3(magnitude1, box_height, box_thick);
  }

  public static C[] CollectEnumNameComponents<C, E>(Transform root) where C : Component
  {
    return Utility.CollectEnumNameComponents<C>(root, typeof (E));
  }

  public static C[] CollectEnumNameComponents<C>(Transform root, System.Type enum_type) where C : Component
  {
    return Utility.CollectEnumNameComponents<C>(root, enum_type, Enum.GetNames(enum_type));
  }

  public static C[] CollectEnumNameComponents<C>(Transform root, System.Type enum_type, string[] names) where C : Component
  {
    int length = names.Length;
    C[] cArray = new C[length];
    Utility.StackComponentInChildren<C>(root);
label_1:
    C c = Utility.Pop<C>();
    if (Object.op_Equality((Object) (object) c, (Object) null))
      return cArray;
    string name = ((Object) (object) c).name;
    for (int index = 0; index < length; ++index)
    {
      if (Object.op_Equality((Object) (object) cArray[index], (Object) null) && names[index] == name)
      {
        cArray[index] = c;
        break;
      }
    }
    goto label_1;
  }

  public static void MaterialForEach(Renderer[] renderers, Action<Material> callback)
  {
    if (renderers == null)
      return;
    int index1 = 0;
    for (int length1 = renderers.Length; index1 < length1; ++index1)
    {
      if (!Object.op_Equality((Object) renderers[index1], (Object) null))
      {
        Material[] materials = renderers[index1].materials;
        int index2 = 0;
        for (int length2 = materials.Length; index2 < length2; ++index2)
        {
          if (Object.op_Inequality((Object) materials[index2], (Object) null))
            callback(materials[index2]);
        }
      }
    }
  }

  public static void SharedMaterialForEach(Renderer[] renderers, Action<Material> callback)
  {
    if (renderers == null)
      return;
    int index1 = 0;
    for (int length1 = renderers.Length; index1 < length1; ++index1)
    {
      Material[] sharedMaterials = renderers[index1].sharedMaterials;
      int index2 = 0;
      for (int length2 = sharedMaterials.Length; index2 < length2; ++index2)
      {
        if (Object.op_Inequality((Object) sharedMaterials[index2], (Object) null))
          callback(sharedMaterials[index2]);
      }
    }
  }

  public static float VolumeToDecibel(float volume)
  {
    float f = 20f * Mathf.Log10(volume);
    return float.IsInfinity(f) || float.IsNaN(f) ? -80f : f;
  }

  public static float DecibelToVolume(float dB)
  {
    float num1 = -80f;
    float num2 = 20f;
    return Mathf.Clamp01(Mathf.Pow(10f, Mathf.Clamp(dB, num1, num2) / num2));
  }

  public static float HorizontalToVerticalFOV(float Horizontal_fov)
  {
    float num1 = (float) Screen.height / (float) Screen.width;
    if ((double) num1 < 1.0)
      num1 = 1f;
    float num2 = (float) ((double) num1 * 9.0 / 16.0);
    return (float) ((double) Mathf.Atan(Mathf.Tan((float) ((double) Horizontal_fov * 0.5 * (Math.PI / 180.0))) * num2) * 57.295780181884766 * 2.0);
  }

  public static TValue GetValueOrAddDefault<TKey, TValue>(
    this Dictionary<TKey, TValue> dict,
    TKey key,
    Func<TKey, TValue> defaultValue)
  {
    TValue valueOrAddDefault1;
    if (dict.TryGetValue(key, out valueOrAddDefault1))
      return valueOrAddDefault1;
    TValue valueOrAddDefault2 = defaultValue(key);
    dict.Add(key, valueOrAddDefault2);
    return valueOrAddDefault2;
  }

  public static TValue GetValueOrAddNew<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key) where TValue : new()
  {
    return dict.GetValueOrAddDefault<TKey, TValue>(key, (Func<TKey, TValue>) (k => new TValue()));
  }

  public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
  {
    TValue obj;
    return dict.TryGetValue(key, out obj) ? obj : default (TValue);
  }

  public static Vector3 ClosestPointOnCollider(Collider to_collider, Vector3 point)
  {
    SphereCollider sphereCollider = to_collider as SphereCollider;
    if (Object.op_Inequality((Object) sphereCollider, (Object) null))
    {
      Matrix4x4 worldToLocalMatrix = ((Component) sphereCollider).transform.worldToLocalMatrix;
      Vector3 vector3_1 = Vector3.op_Subtraction(((Matrix4x4) ref worldToLocalMatrix).MultiplyPoint3x4(point), sphereCollider.center);
      ((Vector3) ref vector3_1).Normalize();
      Vector3 vector3_2 = Vector3.op_Addition(Vector3.op_Multiply(vector3_1, sphereCollider.radius), sphereCollider.center);
      Matrix4x4 localToWorldMatrix = ((Component) sphereCollider).transform.localToWorldMatrix;
      return ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint3x4(vector3_2);
    }
    CapsuleCollider capsuleCollider = to_collider as CapsuleCollider;
    if (Object.op_Inequality((Object) capsuleCollider, (Object) null))
    {
      Matrix4x4 worldToLocalMatrix = ((Component) capsuleCollider).transform.worldToLocalMatrix;
      Vector3 vector3_3 = Vector3.op_Subtraction(((Matrix4x4) ref worldToLocalMatrix).MultiplyPoint3x4(point), capsuleCollider.center);
      float num1 = ((Vector3) ref vector3_3)[capsuleCollider.direction];
      float num2 = capsuleCollider.height * 0.5f - capsuleCollider.radius;
      if ((double) Mathf.Abs(num1) > (double) num2)
      {
        num1 = Mathf.Sign(num1) * num2;
        ref Vector3 local = ref vector3_3;
        int direction = capsuleCollider.direction;
        ((Vector3) ref local)[direction] = ((Vector3) ref local)[direction] - num1;
      }
      else
        ((Vector3) ref vector3_3)[capsuleCollider.direction] = 0.0f;
      ((Vector3) ref vector3_3).Normalize();
      Vector3 vector3_4 = Vector3.op_Multiply(vector3_3, capsuleCollider.radius);
      ref Vector3 local1 = ref vector3_4;
      int direction1 = capsuleCollider.direction;
      ((Vector3) ref local1)[direction1] = ((Vector3) ref local1)[direction1] + num1;
      Vector3 vector3_5 = Vector3.op_Addition(vector3_4, capsuleCollider.center);
      Matrix4x4 localToWorldMatrix = ((Component) capsuleCollider).transform.localToWorldMatrix;
      return ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint3x4(vector3_5);
    }
    BoxCollider boxCollider = to_collider as BoxCollider;
    if (!Object.op_Inequality((Object) boxCollider, (Object) null))
      return to_collider.ClosestPointOnBounds(point);
    Matrix4x4 worldToLocalMatrix1 = ((Component) boxCollider).transform.worldToLocalMatrix;
    Vector3 vector3_6 = Vector3.op_Subtraction(((Matrix4x4) ref worldToLocalMatrix1).MultiplyPoint3x4(point), boxCollider.center);
    Vector3 zero = Vector3.zero;
    zero.x = Mathf.Abs(vector3_6.x) - boxCollider.size.x * 0.5f;
    zero.y = Mathf.Abs(vector3_6.y) - boxCollider.size.y * 0.5f;
    zero.z = Mathf.Abs(vector3_6.z) - boxCollider.size.z * 0.5f;
    int num3 = -1;
    if ((double) zero.x < 0.0 && (double) zero.y < 0.0 && (double) zero.z < 0.0)
    {
      float num4 = 0.0f;
      for (int index = 0; index < 3; ++index)
      {
        if (index == 0 || (double) num4 < (double) ((Vector3) ref zero)[index])
        {
          num3 = index;
          num4 = ((Vector3) ref zero)[index];
        }
      }
    }
    if ((double) zero.x > 0.0 || num3 == 0)
      vector3_6.x = (float) ((double) Mathf.Sign(vector3_6.x) * (double) boxCollider.size.x * 0.5);
    if ((double) zero.y > 0.0 || num3 == 1)
      vector3_6.y = (float) ((double) Mathf.Sign(vector3_6.y) * (double) boxCollider.size.y * 0.5);
    if ((double) zero.z > 0.0 || num3 == 2)
      vector3_6.z = (float) ((double) Mathf.Sign(vector3_6.z) * (double) boxCollider.size.z * 0.5);
    Vector3 vector3_7 = Vector3.op_Addition(vector3_6, boxCollider.center);
    Matrix4x4 localToWorldMatrix1 = ((Component) boxCollider).transform.localToWorldMatrix;
    return ((Matrix4x4) ref localToWorldMatrix1).MultiplyPoint3x4(vector3_7);
  }

  public static Vector3 ClosestPointOnColliderFix(Collider to_collider, Vector3 point)
  {
    Vector3 vector3_1 = Vector3.zero;
    switch (to_collider)
    {
      case BoxCollider _:
        vector3_1 = (to_collider as BoxCollider).center;
        break;
      case SphereCollider _:
        vector3_1 = (to_collider as SphereCollider).center;
        break;
      case CapsuleCollider _:
        vector3_1 = (to_collider as CapsuleCollider).center;
        break;
    }
    Vector3 vector3_2 = Vector3.op_Subtraction(Vector3.op_Addition(((Component) to_collider).transform.position, vector3_1), point);
    Vector3 normalized = ((Vector3) ref vector3_2).normalized;
    RaycastHit raycastHit;
    return !to_collider.Raycast(new Ray(point, normalized), ref raycastHit, float.MaxValue) ? point : Utility.ClosestPointOnCollider(to_collider, point);
  }

  public static bool IsExist(ICollection collection) => collection != null && collection.Count > 0;

  public static void LogString<T>(this List<T> list)
  {
    for (int index = 0; index < list.Count; ++index)
      Debug.Log((object) list[index].ToString());
  }

  public static void DoAction<T>(this List<T> list, Action<T> callback)
  {
    for (int index = 0; index < list.Count; ++index)
    {
      if (callback != null)
        callback(list[index]);
    }
  }

  public static void DoAction<T>(this List<T> list, Action<T, int> callback)
  {
    for (int index = 0; index < list.Count; ++index)
    {
      if (callback != null)
        callback(list[index], index);
    }
  }

  public static string[] DumpList(IList listobj, string parentName = "")
  {
    List<string> stringList = new List<string>();
    string str1 = parentName;
    if (listobj.Count == 0)
    {
      stringList.Add($"{str1}=[(empty)]");
    }
    else
    {
      int num = 0;
      foreach (object obj in (IEnumerable) listobj)
      {
        string str2 = obj.GetType().Namespace;
        if (str2 == null || !str2.ToString().StartsWith("System"))
          stringList.AddRange((IEnumerable<string>) Utility.Dump(obj, $"{str1}[{(object) num}]"));
        else
          stringList.Add($"{str1}[{num}]={obj.ToString()}");
        ++num;
      }
    }
    return stringList.ToArray();
  }

  public static string[] Dump(object obj, string parentName = "")
  {
    if (obj is IList)
      return Utility.DumpList((IList) obj, parentName);
    List<string> stringList = new List<string>();
    foreach (FieldInfo field in obj.GetType().GetFields())
    {
      string parentName1 = $"{parentName}.{field.Name}";
      object listobj = field.GetValue(obj);
      if (listobj == null)
      {
        stringList.Add($"{parentName1}=(null)");
      }
      else
      {
        if (listobj is IList)
          stringList.AddRange((IEnumerable<string>) Utility.DumpList((IList) listobj, parentName1));
        else
          stringList.Add($"{parentName1}={listobj.ToString()}");
        string str = listobj.GetType().Namespace;
        if (str == null || !str.ToString().StartsWith("System"))
          stringList.AddRange((IEnumerable<string>) Utility.Dump(listobj, parentName1));
      }
    }
    return stringList.ToArray();
  }

  public static void PlayFullScreenMovie(string movieName)
  {
    movieName = $"{Application.temporaryCachePath}/{movieName}";
    Handheld.PlayFullScreenMovie(movieName, Color.black, (FullScreenMovieControlMode) 0);
  }

  public static float GetiOSVersion() => -1f;

  public static Vector3 GetScreenUIPosition(Camera camera, Transform cam_transform, Vector3 pos)
  {
    if (Object.op_Equality((Object) camera, (Object) null) || Object.op_Equality((Object) cam_transform, (Object) null))
      return Utility.tempScreenPos;
    Matrix4x4 matrix4x4 = cam_transform.worldToLocalMatrix;
    Vector3 vector3_1 = ((Matrix4x4) ref matrix4x4).MultiplyPoint3x4(pos);
    float z = vector3_1.z;
    vector3_1.z = 0.0f;
    float magnitude = ((Vector3) ref vector3_1).magnitude;
    float num1;
    if (Utility.tempCameraInfo == null || Utility.tempCameraInfo.screenWidth != Screen.width || Utility.tempCameraInfo.screenHeight != Screen.height || (double) Utility.tempCameraInfo.fov != (double) camera.fieldOfView)
    {
      float num2 = (float) Screen.height * 0.5f;
      float num3 = Mathf.Sqrt((float) (Screen.height * Screen.height + Screen.width * Screen.width)) * 0.5f;
      num1 = Mathf.Tan((float) ((double) camera.fieldOfView * 0.5 * (Math.PI / 180.0))) * num3 / num2;
      Utility.tempCameraInfo = new Utility.TempCameraInfo();
      Utility.tempCameraInfo.screenWidth = Screen.width;
      Utility.tempCameraInfo.screenHeight = Screen.height;
      Utility.tempCameraInfo.fov = camera.fieldOfView;
      Utility.tempCameraInfo.limitRate = num1;
    }
    else
      num1 = Utility.tempCameraInfo.limitRate;
    float num4 = 1f;
    float num5 = num4 * num1;
    Vector3 vector3_2;
    if ((double) z <= 0.0 || (double) num5 < (double) magnitude * (double) num4 / (double) z)
    {
      vector3_2 = Vector3.op_Multiply(vector3_1, num5 / magnitude);
      vector3_2.z = num4;
    }
    else
    {
      vector3_2 = Vector3.op_Multiply(vector3_1, num4 / z);
      vector3_2.z = num4;
    }
    matrix4x4 = cam_transform.localToWorldMatrix;
    pos = ((Matrix4x4) ref matrix4x4).MultiplyPoint3x4(vector3_2);
    Vector3 screenPoint = camera.WorldToScreenPoint(pos);
    screenPoint.z = z;
    Utility.tempScreenPos = screenPoint;
    return screenPoint;
  }

  public static Color MakeColorByInt(int r, int g, int b, int a = 255 /*0xFF*/)
  {
    return new Color((float) r / (float) byte.MaxValue, (float) g / (float) byte.MaxValue, (float) b / (float) byte.MaxValue, (float) a / (float) byte.MaxValue);
  }

  public static IEnumerable<T> MultipleEnumerator<T>(params IEnumerable<T>[] paramArray)
  {
    IEnumerable<T>[] objsArray = paramArray;
    for (int index = 0; index < objsArray.Length; ++index)
    {
      foreach (T obj in objsArray[index])
        yield return obj;
    }
    objsArray = (IEnumerable<T>[]) null;
  }

  public static IEnumerable<T> GetAllCompornent<T>(this Component root)
  {
    IEnumerable<T>[] objsArray = new IEnumerable<T>[2]
    {
      (IEnumerable<T>) root.GetComponents<T>(),
      (IEnumerable<T>) root.GetComponentsInChildren<T>(true)
    };
    foreach (T obj in Utility.MultipleEnumerator<T>(objsArray))
      yield return obj;
  }

  public static IEnumerable<T> GetAllCompornent<T>(this GameObject root)
  {
    IEnumerable<T>[] objsArray = new IEnumerable<T>[2]
    {
      (IEnumerable<T>) root.GetComponents<T>(),
      (IEnumerable<T>) root.GetComponentsInChildren<T>(true)
    };
    foreach (T obj in Utility.MultipleEnumerator<T>(objsArray))
      yield return obj;
  }

  public static bool IsEnableEquip(EQUIPMENT_TYPE type, ABILITY_ENABLE_TYPE abilityEnableType)
  {
    return Utility.IsEnableEquip(type, Utility.GetEnableEquipType(abilityEnableType));
  }

  public static bool IsEnableEquip(EQUIPMENT_TYPE type, ENABLE_EQUIP_TYPE enableType)
  {
    return enableType == ENABLE_EQUIP_TYPE.ALL || type == EQUIPMENT_TYPE.ONE_HAND_SWORD && enableType == ENABLE_EQUIP_TYPE.ONE_HAND_SWORD || type == EQUIPMENT_TYPE.TWO_HAND_SWORD && enableType == ENABLE_EQUIP_TYPE.TWO_HAND_SWORD || type == EQUIPMENT_TYPE.SPEAR && enableType == ENABLE_EQUIP_TYPE.SPEAR || type == EQUIPMENT_TYPE.PAIR_SWORDS && enableType == ENABLE_EQUIP_TYPE.PAIR_SWORDS || type == EQUIPMENT_TYPE.ARROW && enableType == ENABLE_EQUIP_TYPE.ARROW || enableType == ENABLE_EQUIP_TYPE.ARMORS && (type == EQUIPMENT_TYPE.ARM || type == EQUIPMENT_TYPE.ARMOR || type == EQUIPMENT_TYPE.HELM || type == EQUIPMENT_TYPE.LEG);
  }

  public static bool IsConditionsAbilityType(ABILITY_TYPE abilityType)
  {
    return Utility.GetAbilityEnableType(abilityType) != 0;
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

  public static bool CheckEnableSpAttackType(
    ref bool rEnable,
    SP_ATTACK_TYPE spAttackType,
    ABILITY_ENABLE_TYPE abilityEnableType)
  {
    switch (abilityEnableType)
    {
      case ABILITY_ENABLE_TYPE.NORMAL:
        rEnable = spAttackType == SP_ATTACK_TYPE.NONE;
        break;
      case ABILITY_ENABLE_TYPE.HEAT:
        rEnable = spAttackType == SP_ATTACK_TYPE.HEAT;
        break;
      case ABILITY_ENABLE_TYPE.SOUL:
        rEnable = spAttackType == SP_ATTACK_TYPE.SOUL;
        break;
      case ABILITY_ENABLE_TYPE.NORMAL_HEAT:
        rEnable = spAttackType == SP_ATTACK_TYPE.NONE || spAttackType == SP_ATTACK_TYPE.HEAT;
        break;
      case ABILITY_ENABLE_TYPE.NORMAL_SOUL:
        rEnable = spAttackType == SP_ATTACK_TYPE.NONE || spAttackType == SP_ATTACK_TYPE.SOUL;
        break;
      case ABILITY_ENABLE_TYPE.HEAT_SOUL:
        rEnable = spAttackType == SP_ATTACK_TYPE.HEAT || spAttackType == SP_ATTACK_TYPE.SOUL;
        break;
      default:
        return false;
    }
    return true;
  }

  public static bool CheckEnableSpAttackType(
    ref bool rEnable,
    SP_ATTACK_TYPE spAttackType,
    int spAtkEnableBit)
  {
    bool flag1 = false;
    bool flag2 = false;
    if ((1 & spAtkEnableBit) != 0)
    {
      flag2 |= spAttackType == SP_ATTACK_TYPE.NONE;
      flag1 = ((flag1 ? 1 : 0) | 1) != 0;
    }
    if ((2 & spAtkEnableBit) != 0)
    {
      flag2 |= spAttackType == SP_ATTACK_TYPE.HEAT;
      flag1 = ((flag1 ? 1 : 0) | 1) != 0;
    }
    if ((4 & spAtkEnableBit) != 0)
    {
      flag2 |= spAttackType == SP_ATTACK_TYPE.SOUL;
      flag1 = ((flag1 ? 1 : 0) | 1) != 0;
    }
    if ((8 & spAtkEnableBit) != 0)
    {
      flag2 |= spAttackType == SP_ATTACK_TYPE.BURST;
      flag1 = ((flag1 ? 1 : 0) | 1) != 0;
    }
    if ((16 /*0x10*/ & spAtkEnableBit) != 0)
    {
      flag2 |= spAttackType == SP_ATTACK_TYPE.ORACLE;
      flag1 = ((flag1 ? 1 : 0) | 1) != 0;
    }
    if (flag1)
      rEnable = flag2;
    return flag1;
  }

  public static bool IsEnableSpAttackType(
    SP_ATTACK_TYPE skillSpAttackType,
    SP_ATTACK_TYPE equipSpAttackType)
  {
    return skillSpAttackType == SP_ATTACK_TYPE.NONE || skillSpAttackType == equipSpAttackType;
  }

  public static int GetCurrentEventID()
  {
    if (!MonoBehaviourSingleton<FieldManager>.IsValid() || !Singleton<FieldMapTable>.IsValid())
      return 0;
    FieldManager i = MonoBehaviourSingleton<FieldManager>.I;
    List<FieldMapTable.EnemyPopTableData> enemyPopList = Singleton<FieldMapTable>.I.GetEnemyPopList(i.currentMapID);
    if (enemyPopList != null && enemyPopList.Count > 0)
    {
      foreach (FieldMapTable.EnemyPopTableData enemyPopTableData in enemyPopList)
      {
        if (enemyPopTableData.bossFlag)
        {
          QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
          if (questData != null)
            return questData.eventId;
        }
      }
    }
    return i.currentMapData != null ? i.currentMapData.eventId : 0;
  }

  public static T Find<T>(this T[] array, Predicate<T> match) where T : class
  {
    for (int index = 0; index < array.Length; ++index)
    {
      T obj = array[index];
      if (match(obj))
        return obj;
    }
    return default (T);
  }

  public static void SafeInvoke(this System.Action action)
  {
    if (action == null)
      return;
    action();
  }

  public static void SafeInvoke<T>(this Action<T> action, T t)
  {
    if (action == null)
      return;
    action(t);
  }

  public static void SafeInvoke<T1, T2>(this Action<T1, T2> action, T1 t1, T2 t2)
  {
    if (action == null)
      return;
    action(t1, t2);
  }

  public static void SafeInvoke<T1, T2, T3>(this Action<T1, T2, T3> action, T1 t1, T2 t2, T3 t3)
  {
    if (action == null)
      return;
    action(t1, t2, t3);
  }

  public static void SafeInvoke<T1, T2, T3, T4>(
    this Action<T1, T2, T3, T4> action,
    T1 t1,
    T2 t2,
    T3 t3,
    T4 t4)
  {
    if (action == null)
      return;
    action(t1, t2, t3, t4);
  }

  public static bool IsNullOrWhiteSpace(this string self) => self == null || self.Trim() == "";

  public static bool ContainIgnoreCase(this string self, string value)
  {
    return self.ToLower().Contains(value.ToLower());
  }

  public static bool IsNullOrEmpty<T>(this IList<T> self) => self == null || self.Count == 0;

  public static int ToInt32OrDefault(this string s, int defaultValue = 0)
  {
    int result;
    return int.TryParse(s, out result) ? result : defaultValue;
  }

  public static string AbsoluteToAssetPath(string absolutepath)
  {
    return absolutepath.StartsWith(Application.dataPath) ? "Assets" + absolutepath.Substring(Application.dataPath.Length) : absolutepath;
  }

  public static float ToFloatOrDefault(this string s, float defaultValue = 0.0f)
  {
    float result;
    return float.TryParse(s, out result) ? result : defaultValue;
  }

  public static int Digit(int num) => num != 0 ? (int) Mathf.Log10((float) num) + 1 : 1;

  public static string GetRewardName(REWARD_TYPE rewardType, uint itemId)
  {
    string rewardName = string.Empty;
    switch (rewardType)
    {
      case REWARD_TYPE.CRYSTAL:
        rewardName = StringTable.Get(STRING_CATEGORY.COMMON, 100U);
        break;
      case REWARD_TYPE.MONEY:
        rewardName = StringTable.Get(STRING_CATEGORY.COMMON, 101U);
        break;
      case REWARD_TYPE.ITEM:
      case REWARD_TYPE.ABILITY_ITEM:
        rewardName = Singleton<ItemTable>.I.GetItemData(itemId).name;
        break;
      case REWARD_TYPE.EQUIP_ITEM:
        rewardName = Singleton<EquipItemTable>.I.GetEquipItemData(itemId).name;
        break;
      case REWARD_TYPE.SKILL_ITEM:
        rewardName = Singleton<SkillItemTable>.I.GetSkillItemData(itemId).name;
        break;
      case REWARD_TYPE.QUEST_ITEM:
        rewardName = Singleton<QuestTable>.I.GetQuestData(itemId).questText;
        break;
      case REWARD_TYPE.AVATAR:
        rewardName = Singleton<AvatarTable>.I.GetData(itemId).name;
        break;
      case REWARD_TYPE.STAMP:
        rewardName = Singleton<StampTable>.I.GetData(itemId).desc;
        break;
      case REWARD_TYPE.DEGREE:
        rewardName = Singleton<DegreeTable>.I.GetData(itemId).name;
        break;
      case REWARD_TYPE.POINT_SHOP_POINT:
        rewardName = StringTable.Get(STRING_CATEGORY.POINT_SHOP, itemId == 1U ? 100U : 101U);
        break;
      case REWARD_TYPE.ACCESSORY:
        AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData(itemId);
        if (data != null)
        {
          rewardName = data.name;
          break;
        }
        break;
      case REWARD_TYPE.EXP:
        rewardName = StringTable.Get(STRING_CATEGORY.COMMON, 102U);
        break;
    }
    return rewardName;
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
    if (Object.op_Equality((Object) gameObject, (Object) null))
      return;
    UIWidget component = gameObject.GetComponent<UIWidget>();
    if (Object.op_Inequality((Object) component, (Object) null))
      component.UpdateAnchors();
    int childCount = gameObject.transform.childCount;
    for (int index = 0; index < childCount; ++index)
      Utility.UpdateAllAnchors(((Component) gameObject.transform.GetChild(index)).gameObject);
  }

  public static string GetDaySuffix(int rank)
  {
    switch (rank)
    {
      case 1:
      case 21:
      case 31 /*0x1F*/:
        return rank.ToString() + "st";
      case 2:
      case 22:
        return rank.ToString() + "nd";
      case 3:
      case 23:
        return rank.ToString() + "rd";
      default:
        return rank.ToString() + "th";
    }
  }

  public static string TrimText(string text, UILabel lblContainer)
  {
    string text1 = lblContainer.text;
    lblContainer.text = text;
    lblContainer.UpdateNGUIText();
    int offsetToFit = NGUIText.CalculateOffsetToFit(text);
    lblContainer.text = text1;
    return offsetToFit > 0 ? text.Substring(0, text.Length - Mathf.Clamp(offsetToFit + 2, 0, text.Length)) + "..." : text;
  }

  public static string GetNameWithColoredClanTag(
    string tag,
    string name,
    bool own,
    bool isSameTeam)
  {
    if (!own && (tag == null || tag == string.Empty || tag == ""))
      return name;
    ClanData clanData = MonoBehaviourSingleton<ClanMatchingManager>.I.clanData;
    if (!own)
      return Utility.GetName(tag, name, isSameTeam);
    return clanData != null && !string.IsNullOrEmpty(clanData.cId) ? Utility.GetName(clanData.tag, name, isSameTeam) : name;
  }

  public static string GetName(string tag, string name, bool isSameTeam)
  {
    string str = "08FF00";
    if (!isSameTeam)
      return $"[[b][/b]{tag}]{name}";
    return $"[{str}][[b][/b]{tag}][-]{name}";
  }

  public static bool IsScreenHD()
  {
    int width = Screen.width;
    int num = Screen.height;
    if (width > num)
      num = width;
    return num >= 1280 /*0x0500*/;
  }

  public static int GetTipTypeFromTutorial()
  {
    if (!PlayerPrefs.HasKey("Tut_Weapon_Type"))
      return -1;
    int num = PlayerPrefs.GetInt("Tut_Weapon_Type");
    UnityEngine.Random.Range(1, 3);
    switch (num)
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
    for (int index = 0; index < text.Length; ++index)
    {
      if (text[index] == '\n')
        ++num;
      if (num == maxLine && index + 1 < text.Length)
        return text.Substring(0, index) + "...";
    }
    return text;
  }

  private class TempCameraInfo
  {
    public int screenWidth;
    public int screenHeight;
    public float fov;
    public float limitRate = 1f;
  }
}
