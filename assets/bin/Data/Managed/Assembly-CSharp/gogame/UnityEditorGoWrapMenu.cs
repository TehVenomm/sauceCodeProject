using UnityEngine;
using UnityEngine.UI;

namespace gogame
{
	public class UnityEditorGoWrapMenu : CustomWindow
	{
		public static readonly UnityEditorGoWrapMenu INSTANCE = new UnityEditorGoWrapMenu();

		public UnityEditorGoWrapMenu()
			: base("goWrap_Menu", "goWrap Menu (UnityEditor)")
		{
			AddOpenListener(OnOpen);
			AddCloseListener(OnClose);
		}

		private void OnOpen()
		{
			GoWrap.INSTANCE.SendMessage("handleMenuOpened", "{}");
		}

		private void OnClose()
		{
			GoWrap.INSTANCE.SendMessage("handleMenuClosed", "{}");
		}

		public static void ShowGoWrapMenu()
		{
			INSTANCE.Show();
		}

		protected override void DoShow(GameObject mainPanelContainer)
		{
			FlowLayoutGroup flowLayoutGroup = mainPanelContainer.AddComponent<FlowLayoutGroup>();
			flowLayoutGroup.spacing = new Vector2(10f, 10f);
			flowLayoutGroup.horizontal = true;
			flowLayoutGroup.padding = new RectOffset(10, 10, 10, 10);
			GameObject gameObject = UIHelper.NewGameObject("ExitButton", mainPanelContainer);
			ContentSizeFitter contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();
			contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			LayoutElement layoutElement = gameObject.AddComponent<LayoutElement>();
			layoutElement.preferredWidth = 100f;
			layoutElement.preferredHeight = 30f;
			UIHelper.MakeButton(gameObject, "Exit", ArialFont, 14, Color.black, Color.white).onClick.AddListener(base.Close);
		}
	}
}
