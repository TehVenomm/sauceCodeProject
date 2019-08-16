using UnityEngine;

public class AndroidAdjustUIDeviceInfo : DeviceIndividualInfo
{
	private const float DEFAULT_ANDROID_ASPECT_RATE = 1.77777779f;

	private static float currentAspectRate => DeviceIndividualInfo.longerSide / DeviceIndividualInfo.shorterSide;

	public static bool MustBeAdustUI => currentAspectRate > 1.77777779f;

	public static float adjustCoefficient => currentAspectRate - 1.77777779f;

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

	public AndroidAdjustUIDeviceInfo()
	{
		Rect val = default(Rect);
		val._002Ector(0.05f, 0.03f, 0.055f, 0.115f);
		Rect val2 = default(Rect);
		val2._002Ector(0.05f, 0.1f, 0.055f, 0.06f);
		int num = (int)DeviceIndividualInfo.shorterSide;
		int num2 = (int)DeviceIndividualInfo.longerSide;
		WebViewHelpPortrait.Set((int)((float)num * val2.get_xMin()), (int)((float)num * val2.get_width()), (int)((float)num2 * val2.get_height()), (int)((float)num2 * val2.get_yMin()));
		int num3 = (int)((float)num2 / 840f * (-45.4f * adjustCoefficient));
		WebViewInfoPortrait.Set((int)((float)num * val.get_xMin()), (int)((float)num * val.get_width()), (int)((float)num2 * val.get_height()) + num3, (int)((float)num2 * val.get_yMin()));
		float num4 = (float)num2 / 840f;
		int num5 = (int)(num4 * (-27.3f * adjustCoefficient));
		int num6 = (int)(num4 * (-36.4f * adjustCoefficient));
		int num7 = (int)(num4 * -30f);
		int num8 = (int)(num4 * -10f);
		WebViewInfoLandscape.Set((int)((float)num * val.get_xMin()) + num5, (int)((float)num * val.get_width()) + num6, (int)((float)num2 * val.get_height()) + num7, (int)((float)num2 * val.get_yMin()) + num8);
		SkillButtonAnchorPortrait.Set(-208, -108, -380, -150);
		SkillButtonAnchorLandscape.Set(-208, -103, -448, -92);
		ChatButtonAnchorPortrait.Set(-41, 480, 323, 400);
		ChatButtonAnchorLandscape.Set(-41, 1143, 22, 150);
		ChatBottomAnchorPortrait.Set(-240, 240, 104, 432);
		ChatTopAnchorLandscape.Set(247, 762, 30, 0);
		ChatBottomAnchorLandscapeSmall.Set(-560, -62, 30, 450);
		ChatBottomAnchorLandscapeFull.Set(-480, -65, 30, 450);
		FriendMessage_WIDGET_ANCHOR_BOT_SPLIT_LANDSCAPE_SETTINGS.Set(783f, 0f, 40f, -70f);
		InGameStatusAnchorPortrait.Set(80, 181, 127, 142);
		InGameStatusAnchorLandscape.Set(125, 181, 96, 142);
		MinimapAnchorPortrait.Set(10, 160, -160, -10);
		MinimapAnchorLandscape.Set(30, 180, -160, -10);
		int num9 = 388;
		num9 += (int)(adjustCoefficient * 400f);
		StoryMessageBaseAnchor.Set(-1, 1, -38, num9);
		int num10 = -33;
		num10 += (int)(adjustCoefficient * 182f);
		StoryMainFukidashiBaseFlameAnchor.Set(-1, 1, num10, num10 + 17);
		InGameMenuAnchorLandscape.Set(-51, -3, -77, 50);
		WorldMapWorldSelectAnchor.Set(-1, 1, 160, 0);
		RegionMapBorderTitleAnchor.Set(-112, 112, 32, 55);
		RegionMapDescriptionListBACKAnchorPortrait.Set(5, -117, -76, -16);
		RegionMapDescriptionListBTNTOFIELDAnchorPortrait.Set(-157, 157, -92, 2);
		RegionMapDescriptionListBACKAnchorLandscape.Set(5, -117, -76, 23);
		RegionMapDescriptionListBTNTOFIELDAnchorLandscape.Set(-157, 157, -74, 21);
		RatioVirtualScreenLandscape = 1.4f;
		RatioVirtualScreenPortrait = 1f;
		TitleTopCameraSize = 3.5f;
		TitleTopBGScale.Set(1.2f, 1.2f, 1.2f);
		OpeningCutScale.Set(0.85f, 0.85f, 0.85f);
		ClanRequestToQuestBoardAnchor.Set(-7, -7, 672, -184);
		LoadingUIIndicatorsAnchor.Set(200, 0, 300, 9);
		CharaMakeTexModelAnchor.Set(-35, 35, 384, -444);
	}
}
