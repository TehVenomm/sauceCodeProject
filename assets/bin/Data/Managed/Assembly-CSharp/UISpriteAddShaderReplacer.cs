using UnityEngine;

public class UISpriteAddShaderReplacer : MonoBehaviour
{
	private UISprite sprite;

	private UIManager.AtlasEntry entry;

	private void Awake()
	{
		sprite = GetComponent<UISprite>();
	}

	public void Replace(string shaderName)
	{
		if (!(bool)sprite)
		{
			Awake();
			if (!(bool)sprite)
			{
				return;
			}
		}
		if (entry != null)
		{
			MonoBehaviourSingleton<UIManager>.I.ReleaseAtlas(sprite);
			entry = null;
		}
		entry = MonoBehaviourSingleton<UIManager>.I.ReplaceAtlas(sprite, shaderName);
	}

	private void OnDestroy()
	{
		if (!AppMain.isApplicationQuit && MonoBehaviourSingleton<UIManager>.IsValid() && entry != null)
		{
			MonoBehaviourSingleton<UIManager>.I.ReleaseAtlas(sprite);
		}
	}
}
