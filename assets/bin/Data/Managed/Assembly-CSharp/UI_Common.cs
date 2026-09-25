// Decompiled with JetBrains decompiler
// Type: UI_Common
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UI_Common : UIBehaviour
{
  public override void UpdateUI()
  {
    this.SetActive((Enum) UI_Common.UI.OBJ_CAPTION_1, false);
    this.SetActive((Enum) UI_Common.UI.OBJ_CAPTION_2, false);
    this.SetActive((Enum) UI_Common.UI.OBJ_CAPTION_3, false);
    this.SetActive((Enum) UI_Common.UI.SPR_BADGE, false);
    this.SetActive((Enum) UI_Common.UI.SPR_NAMEPLATE, false);
    this.SetActive((Enum) UI_Common.UI.OBJ_QUEST_BALLOON, false);
    this.SetActive((Enum) UI_Common.UI.OBJ_EVENT_BALLOON, false);
    this.SetActive((Enum) UI_Common.UI.OBJ_LOUNGE_QUEST_BALLOON, false);
    this.SetActive((Enum) UI_Common.UI.OBJ_BACK_1, false);
    this.SetActive((Enum) UI_Common.UI.OBJ_BACK_2, false);
    this.SetActive((Enum) UI_Common.UI.OBJ_BACK_3, false);
    this.SetActive((Enum) UI_Common.UI.OBJ_LOUNGE_NAMEPLATE, false);
    this.SetActive((Enum) UI_Common.UI.OBJ_CHAT_APPEAL, false);
    this.SetActive((Enum) UI_Common.UI.OBJ_STAMP_APPEAL, false);
  }

  public void AttachBackButton(UIBehaviour target_ui, int button_index)
  {
    UI_Common.UI label_enum = (UI_Common.UI) (15 + button_index);
    ((Object) ((Component) this.Attach(target_ui, this.GetCtrl((Enum) label_enum))).gameObject).name = UI_Common.UI.OBJ_BACK.ToString();
  }

  public void AttachCaption(UIBehaviour target_ui, int button_index, string caption)
  {
    if (string.IsNullOrEmpty(caption) || button_index == 0)
      return;
    UI_Common.UI label_enum = (UI_Common.UI) button_index;
    Transform root = this.Attach(target_ui, this.GetCtrl((Enum) label_enum));
    ((Object) ((Component) root).gameObject).name = UI_Common.UI.OBJ_CAPTION.ToString();
    this.SetLabelText(root, (Enum) UI_Common.UI.LBL_CAPTION, caption);
    UITweenCtrl componentInChildren = ((Component) root).gameObject.GetComponentInChildren<UITweenCtrl>();
    if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
      return;
    componentInChildren.Reset();
    int index = 0;
    for (int length = componentInChildren.tweens.Length; index < length; ++index)
      componentInChildren.tweens[index].ResetToBeginning();
    componentInChildren.Play();
  }

  public void AttachBadge(
    UIWidget target_widget,
    int num,
    SpriteAlignment align,
    int offset_x = 5,
    int offset_y = 5,
    bool is_scale_normalize = false)
  {
    if (Object.op_Equality((Object) target_widget, (Object) null))
      return;
    Transform transform = ((Component) target_widget).transform;
    string text = (string) null;
    if (num < 0)
      text = "!";
    else if (num > 99)
      text = "99+";
    else if (num != 0)
      text = num.ToString();
    Transform ctrl = this.FindCtrl(transform, (Enum) UI_Common.UI.LBL_BADGE);
    if (Object.op_Inequality((Object) ctrl, (Object) null))
    {
      if (text == null)
        Object.DestroyImmediate((Object) ((Component) this.FindCtrl(transform, (Enum) UI_Common.UI.SPR_BADGE)).gameObject);
      else
        ((Component) ctrl).GetComponent<UILabel>().text = text;
    }
    else
    {
      if (text == null)
        return;
      Transform root = this.Attach(target_widget, this.GetCtrl((Enum) UI_Common.UI.SPR_BADGE), align, offset_x, offset_y);
      this.SetLabelText(root, (Enum) UI_Common.UI.LBL_BADGE, text);
      if (!is_scale_normalize)
        return;
      Vector3 localScale = MonoBehaviourSingleton<UIManager>.I.uiRootTransform.localScale;
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector(localScale.x / root.lossyScale.x, localScale.y / root.lossyScale.y);
      root.localScale = vector3;
    }
  }

  public Transform CreateQuestBalloon(UI_Common.BALLOON_TYPE type, Transform parent)
  {
    Transform root = this.Clone(this.GetCtrl((Enum) UI_Common.UI.OBJ_QUEST_BALLOON), parent);
    this.SetActive(root, (Enum) UI_Common.UI.OBJ_QUEST_BALLOON, true);
    this.SetActive(root, (Enum) UI_Common.UI.SPR_QUEST_BALLOON_N, type == UI_Common.BALLOON_TYPE.NEW_NORMAL_L);
    this.SetActive(root, (Enum) UI_Common.UI.SPR_QUEST_BALLOON_R, type == UI_Common.BALLOON_TYPE.NEW_NORMAL_R);
    this.SetActive(root, (Enum) UI_Common.UI.SPR_QUEST_BALLOON_E, type == UI_Common.BALLOON_TYPE.NEW_DAILY);
    this.SetActive(root, (Enum) UI_Common.UI.SPR_QUEST_BALLOON_CN, type == UI_Common.BALLOON_TYPE.COMPLETABLE_NORMAL_L);
    this.SetActive(root, (Enum) UI_Common.UI.SPR_QUEST_BALLOON_CE, type == UI_Common.BALLOON_TYPE.COMPLETABLE_DAILY);
    this.SetActive(root, (Enum) UI_Common.UI.SPR_QUEST_BALLOON_SC, type == UI_Common.BALLOON_TYPE.NEW_SHADOW_CHALLENGE);
    Transform ctrl;
    switch (type)
    {
      case UI_Common.BALLOON_TYPE.NEW_NORMAL_R:
        ctrl = this.FindCtrl(root, (Enum) UI_Common.UI.SPR_QUEST_BALLOON_R);
        break;
      case UI_Common.BALLOON_TYPE.NEW_DAILY:
        ctrl = this.FindCtrl(root, (Enum) UI_Common.UI.SPR_QUEST_BALLOON_E);
        break;
      case UI_Common.BALLOON_TYPE.COMPLETABLE_NORMAL_L:
        ctrl = this.FindCtrl(root, (Enum) UI_Common.UI.SPR_QUEST_BALLOON_CN);
        break;
      case UI_Common.BALLOON_TYPE.COMPLETABLE_DAILY:
        ctrl = this.FindCtrl(root, (Enum) UI_Common.UI.SPR_QUEST_BALLOON_CE);
        break;
      case UI_Common.BALLOON_TYPE.NEW_SHADOW_CHALLENGE:
        ctrl = this.FindCtrl(root, (Enum) UI_Common.UI.SPR_QUEST_BALLOON_SC);
        break;
      default:
        ctrl = this.FindCtrl(root, (Enum) UI_Common.UI.SPR_QUEST_BALLOON_N);
        break;
    }
    return ctrl;
  }

  public Transform CreateEventBalloon(Transform parent, UI_Common.EVENT_BALLOON_TYPE type)
  {
    Transform root = this.Clone(this.GetCtrl((Enum) UI_Common.UI.OBJ_EVENT_BALLOON), parent);
    this.SetActive(root, (Enum) UI_Common.UI.OBJ_EVENT_BALLOON, true);
    this.SetActive(root, (Enum) UI_Common.UI.SPR_EVENT_BALLOON, type == UI_Common.EVENT_BALLOON_TYPE.NEW);
    this.SetActive(root, (Enum) UI_Common.UI.SPR_EVENT_BALLOON_C, type == UI_Common.EVENT_BALLOON_TYPE.COMPLETABLE);
    return type == UI_Common.EVENT_BALLOON_TYPE.NEW || type != UI_Common.EVENT_BALLOON_TYPE.COMPLETABLE ? this.FindCtrl(root, (Enum) UI_Common.UI.SPR_EVENT_BALLOON) : this.FindCtrl(root, (Enum) UI_Common.UI.SPR_EVENT_BALLOON_C);
  }

  public Transform CreatePointShopBalloon(Transform parent)
  {
    Transform root = this.Clone(this.GetCtrl((Enum) UI_Common.UI.OBJ_POINT_SHOP_BALLOON), parent);
    this.SetActive(root, (Enum) UI_Common.UI.OBJ_POINT_SHOP_BALLOON, true);
    return this.FindCtrl(root, (Enum) UI_Common.UI.SPR_POINT_SHOP_BALLOON);
  }

  public Transform CreateBingoBalloon(Transform parent)
  {
    Transform root = this.Clone(this.GetCtrl((Enum) UI_Common.UI.OBJ_BINGO_BALLOON), parent);
    this.SetActive(root, (Enum) UI_Common.UI.OBJ_BINGO_BALLOON, true);
    return this.FindCtrl(root, (Enum) UI_Common.UI.SPR_BINGO_BALLOON);
  }

  public Transform CreateExploreBalloon(Transform parent)
  {
    Transform root = this.Clone(this.GetCtrl((Enum) UI_Common.UI.OBJ_EXPLORE_BALLOON), parent);
    this.SetActive(root, (Enum) UI_Common.UI.OBJ_EXPLORE_BALLOON, true);
    return this.FindCtrl(root, (Enum) UI_Common.UI.SPR_EXPLORE_BALLOON);
  }

  public Transform CreateLoungeQuestBalloon(Transform parent)
  {
    Transform root = this.Clone(this.GetCtrl((Enum) UI_Common.UI.OBJ_LOUNGE_QUEST_BALLOON), parent);
    this.SetActive(root, (Enum) UI_Common.UI.OBJ_LOUNGE_QUEST_BALLOON, true);
    return this.FindCtrl(root, (Enum) UI_Common.UI.SPR_LOUNGE_QUEST_BALLOON);
  }

  public Transform CreateNamePlate(string text)
  {
    Transform root = this.Clone(this.GetCtrl((Enum) UI_Common.UI.SPR_NAMEPLATE), MonoBehaviourSingleton<UIManager>.I._transform);
    this.SetLabelText(root, (Enum) UI_Common.UI.LBL_NAMEPLATE, text);
    return root;
  }

  public Transform CreateLoungeNamePlate(string text)
  {
    Transform root = this.Clone(this.GetCtrl((Enum) UI_Common.UI.OBJ_LOUNGE_NAMEPLATE), MonoBehaviourSingleton<UIManager>.I._transform);
    this.SetLabelText(root, (Enum) UI_Common.UI.LBL_NAMEPLATE, text);
    return root;
  }

  public Transform CreateChatAppeal()
  {
    return this.Clone(this.GetCtrl((Enum) UI_Common.UI.OBJ_CHAT_APPEAL), MonoBehaviourSingleton<UIManager>.I._transform);
  }

  public Transform CreateStampAppeal()
  {
    return this.Clone(this.GetCtrl((Enum) UI_Common.UI.OBJ_STAMP_APPEAL), MonoBehaviourSingleton<UIManager>.I._transform);
  }

  private Transform Clone(Transform base_ui, Transform parent)
  {
    ((Component) base_ui).gameObject.SetActive(true);
    Transform transform = ResourceUtility.Realizes((Object) ((Component) base_ui).gameObject, parent);
    ((Component) base_ui).gameObject.SetActive(false);
    return transform;
  }

  private Transform Attach(UIBehaviour target_ui, Transform base_ui)
  {
    UIVirtualScreen component1 = ((Component) target_ui.collectUI).GetComponent<UIVirtualScreen>();
    if (Object.op_Equality((Object) component1, (Object) null))
      return (Transform) null;
    Transform transform = this.Clone(base_ui, ((Component) component1).transform);
    UIWidget component2 = ((Component) transform).GetComponent<UIWidget>();
    if (!Object.op_Inequality((Object) component2, (Object) null))
      return transform;
    if (component2.leftAnchor != null)
    {
      if (FixedPanelNGUI.CheckResolutionCanFix())
      {
        if (((Object) component2.leftAnchor.target).name == "UI_Root")
        {
          component2.SetAnchor(FixedPanelNGUI.Root);
          return transform;
        }
        component2.SetAnchor(((Component) component1).gameObject);
        return transform;
      }
      component2.SetAnchor(((Component) component1).gameObject);
      return transform;
    }
    component2.SetAnchor((GameObject) null);
    return transform;
  }

  private Transform Attach(
    UIWidget target_widget,
    Transform base_ui,
    SpriteAlignment align,
    int offset_x,
    int offset_y)
  {
    if (Object.op_Equality((Object) target_widget, (Object) null))
      return (Transform) null;
    Transform transform = this.Clone(base_ui, ((Component) target_widget).transform);
    UISprite component = ((Component) transform).GetComponent<UISprite>();
    int num1 = target_widget.width >> 1;
    int num2 = target_widget.height >> 1;
    int num3 = component.width >> 1;
    int num4 = component.height >> 1;
    int num5 = num1 - num3;
    int num6 = -num1 + num3;
    int num7 = num2 - num4;
    int num8 = -num2 + num4;
    if (align == 4 || align == 6 || align == 1)
    {
      num5 -= num1;
      num6 -= num1;
    }
    else if (align == 5 || align == 8 || align == 3)
    {
      num5 += num1;
      num6 += num1;
    }
    if (align == 2 || align == 1 || align == 3)
    {
      num8 += num2;
      num7 += num2;
    }
    else if (align == 7 || align == 6 || align == 8)
    {
      num8 -= num2;
      num7 -= num2;
    }
    int left = num5 + offset_x;
    int right = num6 + offset_x;
    int top = num8 + offset_y;
    int bottom = num7 + offset_y;
    component.SetAnchor(((Component) target_widget).gameObject, left, bottom, right, top);
    return transform;
  }

  private enum UI
  {
    OBJ_CAPTION,
    OBJ_CAPTION_1,
    OBJ_CAPTION_2,
    OBJ_CAPTION_3,
    SPR_BADGE,
    LBL_BADGE,
    SPR_NAMEPLATE,
    LBL_NAMEPLATE,
    OBJ_QUEST_BALLOON,
    SPR_QUEST_BALLOON_N,
    SPR_QUEST_BALLOON_R,
    OBJ_EVENT_BALLOON,
    SPR_EVENT_BALLOON,
    OBJ_BACK,
    LBL_CAPTION,
    OBJ_BACK_1,
    OBJ_BACK_2,
    OBJ_BACK_3,
    SPR_QUEST_BALLOON_E,
    SPR_QUEST_BALLOON_CN,
    SPR_QUEST_BALLOON_CE,
    SPR_QUEST_BALLOON_SC,
    SPR_EVENT_BALLOON_C,
    OBJ_POINT_SHOP_BALLOON,
    SPR_POINT_SHOP_BALLOON,
    OBJ_BINGO_BALLOON,
    SPR_BINGO_BALLOON,
    OBJ_EXPLORE_BALLOON,
    SPR_EXPLORE_BALLOON,
    OBJ_LOUNGE_QUEST_BALLOON,
    SPR_LOUNGE_QUEST_BALLOON,
    OBJ_LOUNGE_NAMEPLATE,
    SPR_LOUNGE_NAMEPLATE,
    OBJ_CHAT_APPEAL,
    OBJ_STAMP_APPEAL,
  }

  public enum BALLOON_TYPE
  {
    NEW_NORMAL_L,
    NEW_NORMAL_R,
    NEW_DAILY,
    COMPLETABLE_NORMAL_L,
    COMPLETABLE_DAILY,
    POINT_SHOP,
    NEW_SHADOW_CHALLENGE,
  }

  public enum EVENT_BALLOON_TYPE
  {
    NONE,
    NEW,
    COMPLETABLE,
  }
}
