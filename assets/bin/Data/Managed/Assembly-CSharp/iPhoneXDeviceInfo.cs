using UnityEngine;

public class iPhoneXDeviceInfo : DeviceIndividualInfo
{
	private enum EDGE_SIZE
	{
		X,
		XR
	}

	private static EDGE_SIZE edgeSize = EDGE_SIZE.X;

	private static readonly float DEVICE_SCALE = (float)((Screen.width > Screen.height) ? Screen.width : Screen.height) / 2436f;

	private EdgeInsets edgePortrait;

	private EdgeInsets edgeLandscape;

	public static bool IsiPhoneX => false;

	public override bool HasSafeArea => true;

	public override EdgeInsets SafeArea
	{
		get
		{
			if (SpecialDeviceManager.IsPortrait)
			{
				return edgePortrait;
			}
			return edgeLandscape;
		}
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

	public iPhoneXDeviceInfo()
	{
		EDGE_SIZE eDGE_SIZE = edgeSize;
		if (eDGE_SIZE != 0 && eDGE_SIZE == EDGE_SIZE.XR)
		{
			edgePortrait = new EdgeInsets(88f, 0f, 68f, 0f, DeviceIndividualInfo.shorterSide, DeviceIndividualInfo.longerSide);
			edgeLandscape = new EdgeInsets(0f, 88f, 42f, 88f, DeviceIndividualInfo.longerSide, DeviceIndividualInfo.shorterSide);
		}
		else
		{
			edgePortrait = new EdgeInsets(132f, 0f, 102f, 0f, DeviceIndividualInfo.shorterSide, DeviceIndividualInfo.longerSide);
			edgeLandscape = new EdgeInsets(0f, 132f, 63f, 132f, DeviceIndividualInfo.longerSide, DeviceIndividualInfo.shorterSide);
		}
		WebViewInfoPortrait.Set(62, 62, 353, 183);
		WebViewHelpPortrait.Set(62, 62, 244, 317);
		WebViewInfoLandscape.Set(170, 170, 246, 50);
		WebViewInfoAnchorLandscape.Set(30, -30, 33, -5);
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
		StoryMessageBaseAnchor.Set(-1, 1, -94, 467);
		StoryMainFukidashiBaseFlameAnchor.Set(-1, 1, 7, 23);
		InGameMenuAnchorLandscape.Set(-51, -3, -77, 50);
		WorldMapWorldSelectAnchor.Set(-1, 1, 160, 0);
		RegionMapBorderTitleAnchor.Set(-112, 112, 32, 55);
		RegionMapDescriptionListBACKAnchorPortrait.Set(5, -117, -76, -16);
		RegionMapDescriptionListBTNTOFIELDAnchorPortrait.Set(-157, 157, -92, 2);
		RegionMapDescriptionListBACKAnchorLandscape.Set(5, -117, -76, 23);
		RegionMapDescriptionListBTNTOFIELDAnchorLandscape.Set(-157, 157, -74, 21);
		RatioVirtualScreenLandscape = 1.4f;
		RatioVirtualScreenPortrait = 1f;
		UIPlayerStatusGizmoScreenSideOffsetPortrait = 50f;
		UIPlayerStatusGizmoScreenSideOffsetLandScape = 125f;
		UIPlayerStatusGizmoScreenBottomOffsetPortrait = 260f;
		UIPlayerStatusGizmoScreenBottomOffsetLandScape = 180f;
		UIPortalGizmoScreenBottomOffsetPortrait = 260f;
		UIPortalGizmoScreenBottomOffsetLandscape = 180f;
		UIPortalGizmoScreenSideOffsetPortrait = 28f;
		UIPortalGizmoScreenSideOffsetLandscape = 130f;
		TitleTopCameraSize = 3.5f;
		TitleTopBGScale.Set(1.2f, 1.2f, 1.2f);
		OpeningCutScale.Set(0.85f, 0.85f, 0.85f);
		ClanRequestToQuestBoardAnchor.Set(-7, -7, 672, -184);
		LoadingUIIndicatorsAnchor.Set(200, 0, 300, 9);
		CharaMakeTexModelAnchor.Set(-35, 35, 384, -444);
		AdjustWebViewSize();
	}

	private void AdjustWebViewSize()
	{
		WebViewInfoPortrait.Scale(DEVICE_SCALE, DEVICE_SCALE);
		WebViewHelpPortrait.Scale(DEVICE_SCALE, DEVICE_SCALE);
		WebViewInfoLandscape.Scale(DEVICE_SCALE, DEVICE_SCALE);
	}
}
