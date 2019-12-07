using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace gogame
{
	public class GoWrapDebugMenu : CustomWindow
	{
		public static readonly GoWrapDebugMenu INSTANCE = new GoWrapDebugMenu();

		public GoWrapDebugMenu()
			: base("goWrap_DebugMenu", "goWrap Debug Menu")
		{
		}

		public static void ShowGoWrapDebugMenu()
		{
			INSTANCE.Show();
		}

		protected override void DoShow(GameObject mainPanelContainer)
		{
			FlowLayoutGroup flowLayoutGroup = mainPanelContainer.AddComponent<FlowLayoutGroup>();
			flowLayoutGroup.spacing = new Vector2(10f, 10f);
			flowLayoutGroup.horizontal = true;
			flowLayoutGroup.padding = new RectOffset(10, 10, 10, 10);
			GameObject gameObject = UIHelper.NewGameObject("ShowGoWrapMenuButton", mainPanelContainer);
			ContentSizeFitter contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();
			contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			LayoutElement layoutElement = gameObject.AddComponent<LayoutElement>();
			layoutElement.preferredWidth = 100f;
			layoutElement.preferredHeight = 30f;
			UIHelper.MakeButton(gameObject, "Menu", ArialFont, 14, Color.black, Color.white).onClick.AddListener(delegate
			{
				GoWrap.INSTANCE.showMenu();
			});
			GameObject gameObject2 = UIHelper.NewGameObject("TestTrackingButton", mainPanelContainer);
			ContentSizeFitter contentSizeFitter2 = gameObject2.AddComponent<ContentSizeFitter>();
			contentSizeFitter2.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			contentSizeFitter2.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			LayoutElement layoutElement2 = gameObject2.AddComponent<LayoutElement>();
			layoutElement2.preferredWidth = 100f;
			layoutElement2.preferredHeight = 30f;
			UIHelper.MakeButton(gameObject2, "Test tracking", ArialFont, 14, Color.black, Color.white).onClick.AddListener(delegate
			{
				GoWrap.INSTANCE.trackEvent("event1", "TEST");
				GoWrap.INSTANCE.trackEvent("event2", "TEST", 123L);
				Dictionary<string, object> values = new Dictionary<string, object>
				{
					["sbyte"] = (sbyte)1,
					["short"] = (short)2,
					["int"] = 3,
					["long"] = 4L,
					["byte"] = (byte)5,
					["ushort"] = (ushort)6,
					["uint"] = 7u,
					["ulong"] = 8uL,
					["char"] = '\t',
					["float"] = 10.1f,
					["double"] = 11.2f,
					["decimal"] = 12.3m,
					["bool"] = true,
					["null"] = null,
					["string"] = "This is a string",
					["object"] = new Object()
				};
				GoWrap.INSTANCE.trackEvent("event3", "TEST", values);
			});
			GameObject gameObject3 = UIHelper.NewGameObject("ExitButton", mainPanelContainer);
			ContentSizeFitter contentSizeFitter3 = gameObject3.AddComponent<ContentSizeFitter>();
			contentSizeFitter3.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			contentSizeFitter3.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			LayoutElement layoutElement3 = gameObject3.AddComponent<LayoutElement>();
			layoutElement3.preferredWidth = 100f;
			layoutElement3.preferredHeight = 30f;
			UIHelper.MakeButton(gameObject3, "Exit", ArialFont, 14, Color.black, Color.white).onClick.AddListener(base.Close);
		}
	}
}
