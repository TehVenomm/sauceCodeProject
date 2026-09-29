// Decompiled with JetBrains decompiler
// Type: ModelPrefabComponentsCopy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ModelPrefabComponentsCopy : MonoBehaviour
{
  public const int DEST_PREFAB_MAX = 10;
  public ModelPrefabComponentsCopy.TOOL_MODE toolMode;
  public GameObject beforeFbx;
  public GameObject afterFbx;
  public GameObject workPrefab;
  public GameObject[] changePrefabList = new GameObject[10];
  public int numDstPrefab = 1;
  public bool prefabAutoDestroy = true;
  public bool isForceApply2Bones = true;
  public GameObject srcPrefab;
  public GameObject srcBaseFbx;
  public GameObject destPrefab;
  public bool prefabAutoSave = true;
  public GameObject srcObject;
  public GameObject dstObject;
  public bool enableComponentLog;

  public enum TOOL_MODE
  {
    CHANGE_FBX_CONVERT,
    SETTINGS_COPY,
    HIERARCHY_CHECK,
  }
}
