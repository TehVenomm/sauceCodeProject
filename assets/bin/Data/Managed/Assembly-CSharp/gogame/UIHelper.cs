using UnityEngine;
using UnityEngine.UI;

namespace gogame
{
	public class UIHelper
	{
		public static GameObject NewGameObject(string name, GameObject parent = null)
		{
			GameObject gameObject = new GameObject();
			gameObject.name = name;
			if (parent != null)
			{
				gameObject.transform.parent = parent.transform;
			}
			return gameObject;
		}

		public static void SetBackgroundColor(GameObject gameObject, Color color)
		{
			Image image = gameObject.AddComponent<Image>();
			image.color = color;
			image.material = null;
			image.raycastTarget = true;
		}

		public static Button MakeButton(GameObject gameObject, string caption, Font font, int fontSize, Color textColor, Color backgroundColor)
		{
			Image image = gameObject.AddComponent<Image>();
			image.color = backgroundColor;
			image.raycastTarget = true;
			Button result = gameObject.AddComponent<Button>();
			Text text = NewGameObject("Text", gameObject).AddComponent<Text>();
			text.text = caption;
			text.font = font;
			text.fontSize = fontSize;
			text.alignment = TextAnchor.MiddleCenter;
			text.color = textColor;
			return result;
		}
	}
}
