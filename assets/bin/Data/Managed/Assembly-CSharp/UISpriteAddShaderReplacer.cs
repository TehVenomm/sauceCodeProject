using UnityEngine;

public class UISpriteAddShaderReplacer
{
	private UISprite sprite;

	private UIManager.AtlasEntry entry;

	public UISpriteAddShaderReplacer()
		: this()
	{
	}

	private void Awake()
	{
		sprite = this.GetComponent<UISprite>();
	}

	public void Replace(string shaderName)
	{
		if (!Object.op_Implicit(sprite))
		{
			Awake();
			if (!Object.op_Implicit(sprite))
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
