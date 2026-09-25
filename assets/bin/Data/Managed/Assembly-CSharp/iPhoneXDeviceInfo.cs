// Decompiled with JetBrains decompiler
// Type: iPhoneXDeviceInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class iPhoneXDeviceInfo : DeviceIndividualInfo
{
  private static iPhoneXDeviceInfo.EDGE_SIZE edgeSize = iPhoneXDeviceInfo.EDGE_SIZE.X;
  private static readonly float DEVICE_SCALE = (Screen.width > Screen.height ? (float) Screen.width : (float) Screen.height) / 2436f;
  private EdgeInsets edgePortrait;
  private EdgeInsets edgeLandscape;

  public static bool IsiPhoneX => false;

  public iPhoneXDeviceInfo()
  {
    switch (iPhoneXDeviceInfo.edgeSize)
    {
      case iPhoneXDeviceInfo.EDGE_SIZE.XR:
        this.edgePortrait = new EdgeInsets(88f, 0.0f, 68f, 0.0f, DeviceIndividualInfo.shorterSide, DeviceIndividualInfo.longerSide);
        this.edgeLandscape = new EdgeInsets(0.0f, 88f, 42f, 88f, DeviceIndividualInfo.longerSide, DeviceIndividualInfo.shorterSide);
        break;
      default:
        this.edgePortrait = new EdgeInsets(132f, 0.0f, 102f, 0.0f, DeviceIndividualInfo.shorterSide, DeviceIndividualInfo.longerSide);
        this.edgeLandscape = new EdgeInsets(0.0f, 132f, 63f, 132f, DeviceIndividualInfo.longerSide, DeviceIndividualInfo.shorterSide);
        break;
    }
    this.WebViewInfoPortrait.Set(62, 62, 353, 183);
    this.WebViewHelpPortrait.Set(62, 62, 244, 317);
    this.WebViewInfoLandscape.Set(170, 170, 246, 50);
    this.WebViewInfoAnchorLandscape.Set(30, -30, 33, -5);
    this.SkillButtonAnchorPortrait.Set(-208, -108, -380, -150);
    this.SkillButtonAnchorLandscape.Set(-208, -103, -448, -92);
    this.ChatButtonAnchorPortrait.Set(-41, 480, 323, 400);
    this.ChatButtonAnchorLandscape.Set(-41, 1143, 22, 150);
    this.ChatBottomAnchorPortrait.Set(-240, 240 /*0xF0*/, 104, 432);
    this.ChatTopAnchorLandscape.Set(247, 762, 30, 0);
    this.ChatBottomAnchorLandscapeSmall.Set(-560, -62, 30, 450);
    this.ChatBottomAnchorLandscapeFull.Set(-480, -65, 30, 450);
    ((Vector4) ref this.FriendMessage_WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS).Set(783f, 0.0f, 40f, -70f);
    this.InGameStatusAnchorPortrait.Set(80 /*0x50*/, 181, (int) sbyte.MaxValue, 142);
    this.InGameStatusAnchorLandscape.Set(125, 181, 96 /*0x60*/, 142);
    this.MinimapAnchorPortrait.Set(10, 160 /*0xA0*/, -160, -10);
    this.MinimapAnchorLandscape.Set(30, 180, -160, -10);
    this.StoryMessageBaseAnchor.Set(-1, 1, -94, 467);
    this.StoryMainFukidashiBaseFlameAnchor.Set(-1, 1, 7, 23);
    this.InGameMenuAnchorLandscape.Set(-51, -3, -77, 50);
    this.WorldMapWorldSelectAnchor.Set(-1, 1, 160 /*0xA0*/, 0);
    this.RegionMapBorderTitleAnchor.Set(-112, 112 /*0x70*/, 32 /*0x20*/, 55);
    this.RegionMapDescriptionListBACKAnchorPortrait.Set(5, -117, -76, -16);
    this.RegionMapDescriptionListBTNTOFIELDAnchorPortrait.Set(-157, 157, -92, 2);
    this.RegionMapDescriptionListBACKAnchorLandscape.Set(5, -117, -76, 23);
    this.RegionMapDescriptionListBTNTOFIELDAnchorLandscape.Set(-157, 157, -74, 21);
    this.RatioVirtualScreenLandscape = 1.4f;
    this.RatioVirtualScreenPortrait = 1f;
    this.UIPlayerStatusGizmoScreenSideOffsetPortrait = 50f;
    this.UIPlayerStatusGizmoScreenSideOffsetLandScape = 125f;
    this.UIPlayerStatusGizmoScreenBottomOffsetPortrait = 260f;
    this.UIPlayerStatusGizmoScreenBottomOffsetLandScape = 180f;
    this.UIPortalGizmoScreenBottomOffsetPortrait = 260f;
    this.UIPortalGizmoScreenBottomOffsetLandscape = 180f;
    this.UIPortalGizmoScreenSideOffsetPortrait = 28f;
    this.UIPortalGizmoScreenSideOffsetLandscape = 130f;
    this.TitleTopCameraSize = 3.5f;
    ((Vector3) ref this.TitleTopBGScale).Set(1.2f, 1.2f, 1.2f);
    ((Vector3) ref this.OpeningCutScale).Set(0.85f, 0.85f, 0.85f);
    this.ClanRequestToQuestBoardAnchor.Set(-7, -7, 672, -184);
    this.LoadingUIIndicatorsAnchor.Set(200, 0, 300, 9);
    this.CharaMakeTexModelAnchor.Set(-35, 35, 384, -444);
    this.AdjustWebViewSize();
  }

  private void AdjustWebViewSize()
  {
    this.WebViewInfoPortrait.Scale(iPhoneXDeviceInfo.DEVICE_SCALE, iPhoneXDeviceInfo.DEVICE_SCALE);
    this.WebViewHelpPortrait.Scale(iPhoneXDeviceInfo.DEVICE_SCALE, iPhoneXDeviceInfo.DEVICE_SCALE);
    this.WebViewInfoLandscape.Scale(iPhoneXDeviceInfo.DEVICE_SCALE, iPhoneXDeviceInfo.DEVICE_SCALE);
  }

  public override bool HasSafeArea => true;

  public override EdgeInsets SafeArea
  {
    get => SpecialDeviceManager.IsPortrait ? this.edgePortrait : this.edgeLandscape;
  }

  public override bool NeedModifyWebView => true;

  public override bool NeedModifyInGamePlayerStatusPosition => true;

  public override bool NeedModifyInGameSkillButtonPosition => true;

  public override bool NeedModifyInGameChatOpenPosition => true;

  public override bool NeedModifyMinimapPosition => true;

  public override bool NeedModifyInGameMenuPosition => true;

  public override bool NeedModifyRegionMapBorderTitleAnchor => true;

  public override bool NeedModifyChatAnchor => true;

  public override bool NeedClanRequestToQuestBoard => true;

  public override bool NeedModifyVirtualScreenRatio => true;

  public override bool NeedModifyPlayerStatusGizmo => true;

  public override bool NeedModifyRegionMapDescriptionList => true;

  public override bool NeedModifyTitleTop => true;

  public override bool NeedModifyOpening => true;

  public override bool NeedLoadingUIIndicatorsAnchor => true;

  public override bool NeedCharaMakeModelAnchor => true;

  public override bool NeedModifyStoryAnchor => true;

  private enum EDGE_SIZE
  {
    X,
    XR,
  }
}
