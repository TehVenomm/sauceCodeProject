// Decompiled with JetBrains decompiler
// Type: AndroidAdjustUIDeviceInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AndroidAdjustUIDeviceInfo : DeviceIndividualInfo
{
  private const float DEFAULT_ANDROID_ASPECT_RATE = 1.77777779f;

  private static float currentAspectRate
  {
    get => DeviceIndividualInfo.longerSide / DeviceIndividualInfo.shorterSide;
  }

  public static bool MustBeAdustUI
  {
    get => (double) AndroidAdjustUIDeviceInfo.currentAspectRate > 1.7777777910232544;
  }

  public static float adjustCoefficient
  {
    get => AndroidAdjustUIDeviceInfo.currentAspectRate - 1.77777779f;
  }

  public AndroidAdjustUIDeviceInfo()
  {
    Rect rect1;
    // ISSUE: explicit constructor call
    ((Rect) ref rect1).\u002Ector(0.05f, 0.03f, 0.055f, 0.115f);
    Rect rect2;
    // ISSUE: explicit constructor call
    ((Rect) ref rect2).\u002Ector(0.05f, 0.1f, 0.055f, 0.06f);
    int shorterSide = (int) DeviceIndividualInfo.shorterSide;
    int longerSide = (int) DeviceIndividualInfo.longerSide;
    this.WebViewHelpPortrait.Set((int) ((double) shorterSide * (double) ((Rect) ref rect2).xMin), (int) ((double) shorterSide * (double) ((Rect) ref rect2).width), (int) ((double) longerSide * (double) ((Rect) ref rect2).height), (int) ((double) longerSide * (double) ((Rect) ref rect2).yMin));
    int num1 = (int) ((double) longerSide / 840.0 * (-45.400001525878906 * (double) AndroidAdjustUIDeviceInfo.adjustCoefficient));
    this.WebViewInfoPortrait.Set((int) ((double) shorterSide * (double) ((Rect) ref rect1).xMin), (int) ((double) shorterSide * (double) ((Rect) ref rect1).width), (int) ((double) longerSide * (double) ((Rect) ref rect1).height) + num1, (int) ((double) longerSide * (double) ((Rect) ref rect1).yMin));
    double num2 = (double) longerSide / 840.0;
    int num3 = (int) (num2 * (-27.299999237060547 * (double) AndroidAdjustUIDeviceInfo.adjustCoefficient));
    int num4 = (int) (num2 * (-36.400001525878906 * (double) AndroidAdjustUIDeviceInfo.adjustCoefficient));
    int num5 = (int) (num2 * -30.0);
    int num6 = (int) (num2 * -10.0);
    this.WebViewInfoLandscape.Set((int) ((double) shorterSide * (double) ((Rect) ref rect1).xMin) + num3, (int) ((double) shorterSide * (double) ((Rect) ref rect1).width) + num4, (int) ((double) longerSide * (double) ((Rect) ref rect1).height) + num5, (int) ((double) longerSide * (double) ((Rect) ref rect1).yMin) + num6);
    this.SkillButtonAnchorPortrait.Set(-208, -108, -380, -150);
    this.SkillButtonAnchorLandscape.Set(-208, -103, -448, -92);
    this.ChatButtonAnchorPortrait.Set(-41, 480, 323, 400);
    this.ChatButtonAnchorLandscape.Set(-41, 1143, 22, 150);
    double num7 = (double) longerSide / (double) shorterSide / 1.6666666269302368;
    this.ChatBottomAnchorPortrait.Set(-240, 240 /*0xF0*/, 71 + (int) (num7 * 70.0), 390 + (int) (num7 * 70.0));
    this.ChatTopAnchorLandscape.Set(247, 762, 30, 0);
    this.ChatBottomAnchorLandscapeSmall.Set(-560, -62, 30, 450);
    this.ChatBottomAnchorLandscapeFull.Set(-480, -65, 30, 450);
    ((Vector4) ref this.FriendMessage_WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS).Set(783f, 0.0f, 40f, -70f);
    this.InGameStatusAnchorPortrait.Set(80 /*0x50*/, 181, (int) sbyte.MaxValue, 142);
    this.InGameStatusAnchorLandscape.Set(125, 181, 96 /*0x60*/, 142);
    this.MinimapAnchorPortrait.Set(10, 160 /*0xA0*/, -160, -10);
    this.MinimapAnchorLandscape.Set(30, 180, -160, -10);
    this.StoryMessageBaseAnchor.Set(-1, 1, -38, 388 + (int) ((double) AndroidAdjustUIDeviceInfo.adjustCoefficient * 500.0));
    int bottom1 = (int) ((double) AndroidAdjustUIDeviceInfo.adjustCoefficient * 249.0) - 33;
    this.StoryMainFukidashiBaseFlameAnchor.Set(-1, 1, bottom1, bottom1 + 17);
    int bottom2 = 927 + (int) ((double) AndroidAdjustUIDeviceInfo.adjustCoefficient * 220.0);
    this.StoryFaderHeader.Set(-1, 1, bottom2, bottom2 + 101);
    this.InGameMenuAnchorLandscape.Set(-51, -3, -77, 50);
    this.WorldMapWorldSelectAnchor.Set(-1, 1, 160 /*0xA0*/, 0);
    this.RegionMapBorderTitleAnchor.Set(-112, 112 /*0x70*/, 32 /*0x20*/, 55);
    this.RegionMapDescriptionListBACKAnchorPortrait.Set(5, -117, -76, -16);
    this.RegionMapDescriptionListBTNTOFIELDAnchorPortrait.Set(-157, 157, -92, 2);
    this.RegionMapDescriptionListBACKAnchorLandscape.Set(5, -117, -76, 23);
    this.RegionMapDescriptionListBTNTOFIELDAnchorLandscape.Set(-157, 157, -74, 21);
    this.RatioVirtualScreenLandscape = 1.4f;
    this.RatioVirtualScreenPortrait = 1f;
    this.TitleTopCameraSize = 3.5f;
    ((Vector3) ref this.TitleTopBGScale).Set(1.2f, 1.2f, 1.2f);
    ((Vector3) ref this.OpeningCutScale).Set(0.85f, 0.85f, 0.85f);
    this.ClanRequestToQuestBoardAnchor.Set(-7, -7, -210, -129);
    this.LoadingUIIndicatorsAnchor.Set(200, 0, 300, 9);
    this.CharaMakeTexModelAnchor.Set(-35, 35, 384, -444);
  }

  public override bool HasSafeArea => true;

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

  public override bool NeedModifyPlayerStatusGizmo => false;

  public override bool NeedModifyRegionMapDescriptionList => true;

  public override bool NeedModifyTitleTop => true;

  public override bool NeedModifyOpening => true;

  public override bool NeedLoadingUIIndicatorsAnchor => true;

  public override bool NeedCharaMakeModelAnchor => true;

  public override bool NeedModifyStoryAnchor => true;
}
