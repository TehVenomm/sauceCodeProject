// Decompiled with JetBrains decompiler
// Type: ItemModelDisplayInfoEditor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
public class ItemModelDisplayInfoEditor : MonoBehaviour
{
  public GlobalSettingsManager globalSettingsManager;
  public Transform[] weapons;
  public Transform armor;
  public Transform helm;
  public Transform arm;
  public Transform leg;
  public Transform item;
  private bool pivotAutoRotate;

  private void OnValidate()
  {
    if (Application.isPlaying || !Object.op_Inequality((Object) this.globalSettingsManager, (Object) null))
      return;
    GlobalSettingsManager.UIModelRenderingParam uiModelRendering = this.globalSettingsManager.uiModelRendering;
    GlobalSettingsManager.UIModelRenderingParam.DisplayInfo[] weaponDisplayInfos = uiModelRendering.WeaponDisplayInfos;
    int index = 0;
    int length1 = weaponDisplayInfos.Length;
    for (int length2 = this.weapons.Length; index < length1 && index < length2; ++index)
      this.LoadSettings(weaponDisplayInfos[index], this.weapons[index]);
    this.LoadSettings(uiModelRendering.armorDisplayInfo, this.armor);
    this.LoadSettings(uiModelRendering.helmDisplayInfo, this.helm);
    this.LoadSettings(uiModelRendering.armDisplayInfo, this.arm);
    this.LoadSettings(uiModelRendering.legDisplayInfo, this.leg);
    this.LoadSettings(uiModelRendering.itemDisplayInfo, this.item);
  }

  private void LoadSettings(
    GlobalSettingsManager.UIModelRenderingParam.DisplayInfo info,
    Transform root)
  {
    if (info == null || Object.op_Equality((Object) root, (Object) null))
      return;
    Transform transform1 = root.Find("Pivot");
    if (Object.op_Equality((Object) transform1, (Object) null))
      return;
    root.localPosition = Vector3.zero;
    root.localEulerAngles = Vector3.zero;
    root.localScale = Vector3.one;
    transform1.localPosition = new Vector3(0.0f, 0.0f, info.zFromCamera);
    transform1.localEulerAngles = Vector3.zero;
    transform1.localScale = Vector3.one;
    Transform transform2 = root.Find("Pivot/Model0");
    Transform transform3 = root.Find("Pivot/Model1");
    if (Object.op_Inequality((Object) transform2, (Object) null))
    {
      transform2.localPosition = info.mainPos;
      transform2.localEulerAngles = info.mainRot;
      transform2.localScale = Vector3.one;
    }
    if (!Object.op_Inequality((Object) transform3, (Object) null))
      return;
    transform3.localPosition = info.subPos;
    transform3.localEulerAngles = info.subRot;
    transform3.localScale = Vector3.one;
  }

  private void SaveSettings(
    GlobalSettingsManager.UIModelRenderingParam.DisplayInfo info,
    Transform root)
  {
    if (info == null || Object.op_Equality((Object) root, (Object) null))
      return;
    Transform transform1 = root.Find("Pivot");
    if (Object.op_Equality((Object) transform1, (Object) null))
      return;
    info.zFromCamera = transform1.localPosition.z;
    Transform transform2 = root.Find("Pivot/Model0");
    Transform transform3 = root.Find("Pivot/Model1");
    if (Object.op_Inequality((Object) transform2, (Object) null))
    {
      info.mainPos = transform2.localPosition;
      info.mainRot = transform2.localEulerAngles;
    }
    if (Object.op_Inequality((Object) transform3, (Object) null))
    {
      info.subPos = transform3.localPosition;
      info.subRot = transform3.localEulerAngles;
    }
    else
    {
      info.subPos = Vector3.zero;
      info.subRot = Vector3.zero;
    }
  }

  private void Update() => this.ForEachModels((Action<Transform>) (t => this.UpdateInfo(t)));

  private void ForEachModels(Action<Transform> callback)
  {
    foreach (Transform weapon in this.weapons)
      callback(weapon);
    callback(this.armor);
    callback(this.helm);
    callback(this.arm);
    callback(this.leg);
    callback(this.item);
  }

  private void UpdateInfo(Transform root)
  {
    if (Object.op_Equality((Object) root, (Object) null))
      return;
    Transform transform = root.Find("Pivot");
    if (Object.op_Equality((Object) root, (Object) null))
      return;
    root.localPosition = Vector3.zero;
    root.localEulerAngles = Vector3.zero;
    Vector3 localPosition = transform.localPosition;
    localPosition.x = 0.0f;
    localPosition.y = 0.0f;
    transform.localPosition = localPosition;
    Vector3 localEulerAngles = transform.localEulerAngles;
    localEulerAngles.x = 0.0f;
    localEulerAngles.z = 0.0f;
    if (this.pivotAutoRotate)
      localEulerAngles.y = (float) (((double) localEulerAngles.y + 5.0) % 360.0);
    transform.localEulerAngles = localEulerAngles;
    this.UpdateModel(root.Find("Pivot/Model0"));
    this.UpdateModel(root.Find("Pivot/Model1"));
  }

  private void UpdateModel(Transform model)
  {
    if (Object.op_Equality((Object) model, (Object) null))
      return;
    foreach (Transform transform in model)
    {
      transform.localPosition = Vector3.zero;
      transform.localEulerAngles = Vector3.zero;
    }
  }

  private void ResetPivotAngle(Transform root)
  {
    if (Object.op_Equality((Object) root, (Object) null))
      return;
    Transform transform = root.Find("Pivot");
    if (Object.op_Equality((Object) transform, (Object) null))
      return;
    transform.localEulerAngles = Vector3.zero;
  }

  private void OnGUI()
  {
    GUILayout.BeginArea(new Rect(0.0f, 0.0f, (float) Screen.width, (float) Screen.height));
    GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
    GUILayout.FlexibleSpace();
    GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
    GUILayout.Label("item model display info editor", Array.Empty<GUILayoutOption>());
    if (!Application.isPlaying)
    {
      GUILayout.Label("実行して下さい。", Array.Empty<GUILayoutOption>());
    }
    else
    {
      GUILayout.Label("中心点の前後の調整は「Pivot」のZ移動。\nモデルの位置・姿勢の調整は「Model0」「Model1」を移動・回転。", Array.Empty<GUILayoutOption>());
      if (!this.pivotAutoRotate)
      {
        if (GUILayout.Button("「Pivot」自動回転をON", Array.Empty<GUILayoutOption>()))
          this.pivotAutoRotate = true;
      }
      else if (GUILayout.Button("「Pivot」自動回転をOFF", Array.Empty<GUILayoutOption>()))
        this.pivotAutoRotate = false;
      if (GUILayout.Button("PivotのY回転をリセット", Array.Empty<GUILayoutOption>()))
      {
        this.ForEachModels((Action<Transform>) (t => this.ResetPivotAngle(t)));
        this.pivotAutoRotate = false;
      }
      if (GUILayout.Button("エディット内容をGlobalSettingsManagerに反映", Array.Empty<GUILayoutOption>()) && Object.op_Inequality((Object) this.globalSettingsManager, (Object) null))
      {
        GlobalSettingsManager.UIModelRenderingParam uiModelRendering = this.globalSettingsManager.uiModelRendering;
        GlobalSettingsManager.UIModelRenderingParam.DisplayInfo[] weaponDisplayInfos = this.globalSettingsManager.uiModelRendering.WeaponDisplayInfos;
        int index = 0;
        int length1 = weaponDisplayInfos.Length;
        for (int length2 = this.weapons.Length; index < length1 && index < length2; ++index)
          this.SaveSettings(weaponDisplayInfos[index], this.weapons[index]);
        this.SaveSettings(uiModelRendering.armorDisplayInfo, this.armor);
        this.SaveSettings(uiModelRendering.helmDisplayInfo, this.helm);
        this.SaveSettings(uiModelRendering.armDisplayInfo, this.arm);
        this.SaveSettings(uiModelRendering.legDisplayInfo, this.leg);
        this.SaveSettings(uiModelRendering.itemDisplayInfo, this.item);
      }
    }
    GUILayout.EndVertical();
    GUILayout.FlexibleSpace();
    GUILayout.EndHorizontal();
    GUILayout.EndArea();
  }
}
