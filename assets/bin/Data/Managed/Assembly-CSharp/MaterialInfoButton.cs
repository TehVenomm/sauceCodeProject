// Decompiled with JetBrains decompiler
// Type: MaterialInfoButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class MaterialInfoButton : MonoBehaviour
{
  private Transform parentButton;
  private Transform parentScroll;
  private MaterialInfo materialInfo;
  private UILabel lblItem;
  private string itemName;
  private bool touched;

  public static void Set(
    Transform icon,
    Transform material_info,
    REWARD_TYPE reward_type,
    uint id,
    string section_name,
    Transform parentScroll)
  {
    UIButton componentInChildren = ((Component) icon).GetComponentInChildren<UIButton>();
    if (Object.op_Equality((Object) componentInChildren, (Object) null))
      return;
    MaterialInfoButton materialInfoButton = ((Component) icon).GetComponent<MaterialInfoButton>();
    if (Object.op_Equality((Object) materialInfoButton, (Object) null))
      materialInfoButton = ((Component) icon).gameObject.AddComponent<MaterialInfoButton>();
    materialInfoButton.parentButton = ((Component) componentInChildren).transform;
    materialInfoButton.itemName = Utility.GetRewardName(reward_type, id);
    materialInfoButton.parentScroll = parentScroll;
    MaterialInfo component = ((Component) material_info).GetComponent<MaterialInfo>();
    component.Initialize(section_name);
    materialInfoButton.materialInfo = component;
  }

  private void OnHover(bool isOver)
  {
    if (isOver || !this.touched)
      return;
    this.Send(false);
  }

  private void OnPress(bool isPressed) => this.Send(isPressed);

  private void OnDisable()
  {
    if (AppMain.isApplicationQuit || !this.touched)
      return;
    this.Send(false);
  }

  private void Send(bool is_touch)
  {
    if (this.touched == is_touch)
      return;
    this.touched = is_touch;
    if (!Object.op_Inequality((Object) this.materialInfo, (Object) null))
      return;
    this.materialInfo.Send(is_touch, this.parentButton, this.itemName, this.parentScroll);
  }

  private void Update()
  {
    if (!this.touched)
      return;
    this.materialInfo.UpdatePosision(this.parentButton);
  }
}
