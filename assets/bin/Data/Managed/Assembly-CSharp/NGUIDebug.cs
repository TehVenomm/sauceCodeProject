// Decompiled with JetBrains decompiler
// Type: NGUIDebug
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Internal/Debug")]
public class NGUIDebug : MonoBehaviour
{
  private static bool mRayDebug = false;
  private static List<string> mLines = new List<string>();
  private static NGUIDebug mInstance = (NGUIDebug) null;

  public static bool debugRaycast
  {
    get => NGUIDebug.mRayDebug;
    set
    {
      NGUIDebug.mRayDebug = value;
      if (!value || !Application.isPlaying)
        return;
      NGUIDebug.CreateInstance();
    }
  }

  public static void CreateInstance()
  {
    if (!Object.op_Equality((Object) NGUIDebug.mInstance, (Object) null))
      return;
    GameObject gameObject = new GameObject("_NGUI Debug");
    NGUIDebug.mInstance = gameObject.AddComponent<NGUIDebug>();
    Object.DontDestroyOnLoad((Object) gameObject);
  }

  private static void LogString(string text)
  {
    if (Application.isPlaying)
    {
      if (NGUIDebug.mLines.Count > 20)
        NGUIDebug.mLines.RemoveAt(0);
      NGUIDebug.mLines.Add(text);
      NGUIDebug.CreateInstance();
    }
    else
      Debug.Log((object) text);
  }

  public static void Log(params object[] objs)
  {
    string text = "";
    for (int index = 0; index < objs.Length; ++index)
      text = index != 0 ? $"{text}, {objs[index].ToString()}" : text + objs[index].ToString();
    NGUIDebug.LogString(text);
  }

  public static void Clear() => NGUIDebug.mLines.Clear();

  public static void DrawBounds(Bounds b)
  {
    Vector3 center = ((Bounds) ref b).center;
    Vector3 vector3_1 = Vector3.op_Subtraction(((Bounds) ref b).center, ((Bounds) ref b).extents);
    Vector3 vector3_2 = Vector3.op_Addition(((Bounds) ref b).center, ((Bounds) ref b).extents);
    Debug.DrawLine(new Vector3(vector3_1.x, vector3_1.y, center.z), new Vector3(vector3_2.x, vector3_1.y, center.z), Color.red);
    Debug.DrawLine(new Vector3(vector3_1.x, vector3_1.y, center.z), new Vector3(vector3_1.x, vector3_2.y, center.z), Color.red);
    Debug.DrawLine(new Vector3(vector3_2.x, vector3_1.y, center.z), new Vector3(vector3_2.x, vector3_2.y, center.z), Color.red);
    Debug.DrawLine(new Vector3(vector3_1.x, vector3_2.y, center.z), new Vector3(vector3_2.x, vector3_2.y, center.z), Color.red);
  }

  private void OnGUI()
  {
    Rect rect;
    // ISSUE: explicit constructor call
    ((Rect) ref rect).\u002Ector(5f, 5f, 1000f, 18f);
    if (NGUIDebug.mRayDebug)
    {
      string str1 = "Scheme: " + (object) UICamera.currentScheme;
      GUI.color = Color.black;
      GUI.Label(rect, str1);
      ref Rect local1 = ref rect;
      ((Rect) ref local1).y = ((Rect) ref local1).y - 1f;
      ref Rect local2 = ref rect;
      ((Rect) ref local2).x = ((Rect) ref local2).x - 1f;
      GUI.color = Color.white;
      GUI.Label(rect, str1);
      ref Rect local3 = ref rect;
      ((Rect) ref local3).y = ((Rect) ref local3).y + 18f;
      ref Rect local4 = ref rect;
      ((Rect) ref local4).x = ((Rect) ref local4).x + 1f;
      string str2 = "Hover: " + NGUITools.GetHierarchy(UICamera.hoveredObject).Replace("\"", "");
      GUI.color = Color.black;
      GUI.Label(rect, str2);
      ref Rect local5 = ref rect;
      ((Rect) ref local5).y = ((Rect) ref local5).y - 1f;
      ref Rect local6 = ref rect;
      ((Rect) ref local6).x = ((Rect) ref local6).x - 1f;
      GUI.color = Color.white;
      GUI.Label(rect, str2);
      ref Rect local7 = ref rect;
      ((Rect) ref local7).y = ((Rect) ref local7).y + 18f;
      ref Rect local8 = ref rect;
      ((Rect) ref local8).x = ((Rect) ref local8).x + 1f;
      string str3 = "Selection: " + NGUITools.GetHierarchy(UICamera.selectedObject).Replace("\"", "");
      GUI.color = Color.black;
      GUI.Label(rect, str3);
      ref Rect local9 = ref rect;
      ((Rect) ref local9).y = ((Rect) ref local9).y - 1f;
      ref Rect local10 = ref rect;
      ((Rect) ref local10).x = ((Rect) ref local10).x - 1f;
      GUI.color = Color.white;
      GUI.Label(rect, str3);
      ref Rect local11 = ref rect;
      ((Rect) ref local11).y = ((Rect) ref local11).y + 18f;
      ref Rect local12 = ref rect;
      ((Rect) ref local12).x = ((Rect) ref local12).x + 1f;
      string str4 = "Controller: " + NGUITools.GetHierarchy(UICamera.controllerNavigationObject).Replace("\"", "");
      GUI.color = Color.black;
      GUI.Label(rect, str4);
      ref Rect local13 = ref rect;
      ((Rect) ref local13).y = ((Rect) ref local13).y - 1f;
      ref Rect local14 = ref rect;
      ((Rect) ref local14).x = ((Rect) ref local14).x - 1f;
      GUI.color = Color.white;
      GUI.Label(rect, str4);
      ref Rect local15 = ref rect;
      ((Rect) ref local15).y = ((Rect) ref local15).y + 18f;
      ref Rect local16 = ref rect;
      ((Rect) ref local16).x = ((Rect) ref local16).x + 1f;
      string str5 = "Active events: " + (object) UICamera.CountInputSources();
      if (UICamera.disableController)
        str5 += ", disabled controller";
      if (UICamera.inputHasFocus)
        str5 += ", input focus";
      GUI.color = Color.black;
      GUI.Label(rect, str5);
      ref Rect local17 = ref rect;
      ((Rect) ref local17).y = ((Rect) ref local17).y - 1f;
      ref Rect local18 = ref rect;
      ((Rect) ref local18).x = ((Rect) ref local18).x - 1f;
      GUI.color = Color.white;
      GUI.Label(rect, str5);
      ref Rect local19 = ref rect;
      ((Rect) ref local19).y = ((Rect) ref local19).y + 18f;
      ref Rect local20 = ref rect;
      ((Rect) ref local20).x = ((Rect) ref local20).x + 1f;
    }
    int index = 0;
    for (int count = NGUIDebug.mLines.Count; index < count; ++index)
    {
      GUI.color = Color.black;
      GUI.Label(rect, NGUIDebug.mLines[index]);
      ref Rect local21 = ref rect;
      ((Rect) ref local21).y = ((Rect) ref local21).y - 1f;
      ref Rect local22 = ref rect;
      ((Rect) ref local22).x = ((Rect) ref local22).x - 1f;
      GUI.color = Color.white;
      GUI.Label(rect, NGUIDebug.mLines[index]);
      ref Rect local23 = ref rect;
      ((Rect) ref local23).y = ((Rect) ref local23).y + 18f;
      ref Rect local24 = ref rect;
      ((Rect) ref local24).x = ((Rect) ref local24).x + 1f;
    }
  }
}
