// Decompiled with JetBrains decompiler
// Type: DeviceIndividualInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public abstract class DeviceIndividualInfo
{
  public RectInt WebViewInfoPortrait;
  public RectInt WebViewInfoLandscape;
  public RectInt WebViewInfoAnchorLandscape;
  public RectInt WebViewHelpPortrait;
  public RectInt SkillButtonAnchorPortrait;
  public RectInt SkillButtonAnchorLandscape;
  public RectInt ChatButtonAnchorPortrait;
  public RectInt ChatButtonAnchorLandscape;
  public RectInt ChatBottomAnchorPortrait;
  public RectInt ChatTopAnchorLandscape;
  public RectInt ChatBottomAnchorLandscapeFull;
  public RectInt ChatBottomAnchorLandscapeSmall;
  public Vector4 FriendMessage_WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS = new Vector4(465f, 0.0f, 40f, -70f);
  public RectInt InGameStatusAnchorPortrait;
  public RectInt InGameStatusAnchorLandscape;
  public RectInt MinimapAnchorPortrait;
  public RectInt MinimapAnchorLandscape;
  public RectInt StoryMessageBaseAnchor;
  public RectInt StoryMainFukidashiBaseFlameAnchor;
  public RectInt StoryFaderHeader;
  public RectInt InGameMenuAnchorLandscape;
  public RectInt WorldMapWorldSelectAnchor;
  public RectInt ClanRequestToQuestBoardAnchor;
  public RectInt RegionMapBorderTitleAnchor;
  public RectInt LoadingUIIndicatorsAnchor;
  public RectInt RegionMapDescriptionListBACKAnchorLandscape;
  public RectInt RegionMapDescriptionListBTNTOFIELDAnchorLandscape;
  public RectInt RegionMapDescriptionListBACKAnchorPortrait;
  public RectInt RegionMapDescriptionListBTNTOFIELDAnchorPortrait;
  public RectInt CharaMakeTexModelAnchor;
  public float RatioVirtualScreenPortrait = 1f;
  public float RatioVirtualScreenLandscape = 1f;
  public float UIPlayerStatusGizmoScreenSideOffsetPortrait = 36f;
  public float UIPlayerStatusGizmoScreenSideOffsetLandScape = 36f;
  public float UIPlayerStatusGizmoScreenBottomOffsetPortrait = 36f;
  public float UIPlayerStatusGizmoScreenBottomOffsetLandScape = 36f;
  public float UIPortalGizmoScreenSideOffsetPortrait = 22f;
  public float UIPortalGizmoScreenSideOffsetLandscape = 22f;
  public float UIPortalGizmoScreenBottomOffsetPortrait = 102f;
  public float UIPortalGizmoScreenBottomOffsetLandscape = 102f;
  public float TitleTopCameraSize = 3f;
  public Vector3 TitleTopBGScale = Vector3.one;
  public Vector3 OpeningCutScale = Vector3.one;

  public virtual bool HasSafeArea => false;

  public virtual EdgeInsets SafeArea => EdgeInsets.zero;

  public virtual bool NeedModifyWebView => false;

  public virtual bool NeedModifyInGamePlayerStatusPosition => false;

  public virtual bool NeedModifyInGameSkillButtonPosition => false;

  public virtual bool NeedModifyInGameChatOpenPosition => false;

  public virtual bool NeedModifyMinimapPosition => false;

  public virtual bool NeedModifyInGameMenuPosition => false;

  public virtual bool NeedModifyRegionMapBorderTitleAnchor => false;

  public virtual bool NeedModifyChatAnchor => false;

  public virtual bool NeedModifyVirtualScreenRatio => false;

  public virtual bool NeedModifyPlayerStatusGizmo => false;

  public virtual bool NeedModifyRegionMapDescriptionList => false;

  public virtual bool NeedModifyTitleTop => false;

  public virtual bool NeedModifyOpening => false;

  public virtual bool NeedClanRequestToQuestBoard => false;

  public virtual bool NeedLoadingUIIndicatorsAnchor => false;

  public virtual bool NeedCharaMakeModelAnchor => false;

  public virtual bool NeedModifyStoryAnchor => false;

  protected static float longerSide
  {
    get => Screen.width > Screen.height ? (float) Screen.width : (float) Screen.height;
  }

  protected static float shorterSide
  {
    get => Screen.width < Screen.height ? (float) Screen.width : (float) Screen.height;
  }

  public virtual void OnStart()
  {
  }
}
