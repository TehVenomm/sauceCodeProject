using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace gogame
{
	public abstract class CustomWindow
	{
		private readonly string _name;

		private readonly UnityEvent _onClose = new UnityEvent();

		private readonly UnityEvent _onOpen = new UnityEvent();

		private readonly string _title;

		public readonly Font ArialFont = Resources.GetBuiltinResource<Font>("Arial.ttf");

		private Text _logText;

		private GameObject _rootContainer;

		public CustomWindow(string name, string title)
		{
			_name = name;
			_title = title;
		}

		public void AddOpenListener(UnityAction listener)
		{
			_onOpen.AddListener(listener);
		}

		public void AddCloseListener(UnityAction listener)
		{
			_onClose.AddListener(listener);
		}

		protected abstract void DoShow(GameObject mainPanelContainer);

		public void Close()
		{
			Object.Destroy(_rootContainer);
			_onClose.Invoke();
		}

		public void Log(string text)
		{
			if (!(_logText == null))
			{
				_logText.text = text;
			}
		}

		public void Show()
		{
			_rootContainer = UIHelper.NewGameObject(_name);
			GameObject gameObject = UIHelper.NewGameObject("Canvas", _rootContainer);
			gameObject.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
			CanvasScaler canvasScaler = gameObject.AddComponent<CanvasScaler>();
			canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
			canvasScaler.referenceResolution = new Vector2(640f, 1136f);
			canvasScaler.matchWidthOrHeight = 1f;
			canvasScaler.referencePixelsPerUnit = 100f;
			gameObject.AddComponent<GraphicRaycaster>();
			GameObject gameObject2 = UIHelper.NewGameObject("RootPanel", gameObject);
			RectTransform rectTransform = gameObject2.AddComponent<RectTransform>();
			rectTransform.offsetMin = new Vector2(10f, 10f);
			rectTransform.offsetMax = new Vector2(-10f, -10f);
			rectTransform.anchorMin = new Vector2(0f, 0f);
			rectTransform.anchorMax = new Vector2(1f, 1f);
			VerticalLayoutGroup verticalLayoutGroup = gameObject2.AddComponent<VerticalLayoutGroup>();
			verticalLayoutGroup.childControlWidth = true;
			verticalLayoutGroup.childControlHeight = true;
			verticalLayoutGroup.childForceExpandWidth = true;
			verticalLayoutGroup.childForceExpandHeight = false;
			UIHelper.SetBackgroundColor(gameObject2, new Color(0f, 0f, 0f, 0.75f));
			GameObject gameObject3 = UIHelper.NewGameObject("TitlePanel", gameObject2);
			gameObject3.AddComponent<LayoutElement>().preferredHeight = 30f;
			HorizontalLayoutGroup horizontalLayoutGroup = gameObject3.AddComponent<HorizontalLayoutGroup>();
			horizontalLayoutGroup.childControlWidth = true;
			horizontalLayoutGroup.childControlHeight = true;
			horizontalLayoutGroup.childForceExpandWidth = false;
			horizontalLayoutGroup.childForceExpandHeight = false;
			UIHelper.SetBackgroundColor(gameObject3, new Color(0f, 0f, 0f, 0.5f));
			GameObject gameObject4 = UIHelper.NewGameObject("BodyPanel", gameObject2);
			gameObject4.AddComponent<LayoutElement>().flexibleHeight = 1f;
			VerticalLayoutGroup verticalLayoutGroup2 = gameObject4.AddComponent<VerticalLayoutGroup>();
			verticalLayoutGroup2.childControlWidth = true;
			verticalLayoutGroup2.childControlHeight = true;
			verticalLayoutGroup2.childForceExpandWidth = true;
			verticalLayoutGroup2.childForceExpandHeight = false;
			UIHelper.SetBackgroundColor(gameObject4, new Color(1f, 1f, 1f, 0.5f));
			GameObject gameObject5 = UIHelper.NewGameObject("TitleText", gameObject3);
			LayoutElement layoutElement = gameObject5.AddComponent<LayoutElement>();
			layoutElement.flexibleWidth = 1f;
			layoutElement.preferredHeight = 30f;
			Text text = gameObject5.AddComponent<Text>();
			text.text = _title;
			text.font = ArialFont;
			text.fontSize = 14;
			text.color = Color.white;
			text.alignment = TextAnchor.MiddleCenter;
			GameObject gameObject6 = UIHelper.NewGameObject("CloseButton", gameObject3);
			LayoutElement layoutElement2 = gameObject6.AddComponent<LayoutElement>();
			layoutElement2.preferredWidth = 30f;
			layoutElement2.preferredHeight = 30f;
			UIHelper.MakeButton(gameObject6, "x", ArialFont, 14, Color.black, Color.white).onClick.AddListener(Close);
			GameObject gameObject7 = UIHelper.NewGameObject("MainPanel", gameObject4);
			gameObject7.AddComponent<LayoutElement>().flexibleHeight = 0.7f;
			UIHelper.SetBackgroundColor(gameObject7, new Color(0f, 0f, 0f, 0.5f));
			GameObject gameObject8 = UIHelper.NewGameObject("LogPanel", gameObject4);
			gameObject8.AddComponent<LayoutElement>().flexibleHeight = 0.3f;
			UIHelper.SetBackgroundColor(gameObject8, new Color(1f, 1f, 1f, 0.5f));
			GameObject gameObject9 = UIHelper.NewGameObject("LogText", gameObject8);
			RectTransform rectTransform2 = gameObject9.AddComponent<RectTransform>();
			rectTransform2.offsetMin = new Vector2(5f, 1f);
			rectTransform2.offsetMax = new Vector2(-5f, -1f);
			rectTransform2.anchorMin = new Vector2(0f, 0f);
			rectTransform2.anchorMax = new Vector2(1f, 1f);
			_logText = gameObject9.AddComponent<Text>();
			_logText.font = ArialFont;
			_logText.color = Color.black;
			_logText.fontSize = 12;
			_logText.horizontalOverflow = HorizontalWrapMode.Wrap;
			_logText.verticalOverflow = VerticalWrapMode.Truncate;
			_logText.alignment = TextAnchor.UpperLeft;
			DoShow(gameObject7);
			_onOpen.Invoke();
		}
	}
}
