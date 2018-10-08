using System.Collections.Generic;
using UnityEngine;

public class UISpriteShaderReplacer : MonoBehaviour
{
	private class AtlasEntry
	{
		public UIAtlas atlas;

		public int refCount;

		public AtlasEntry(UIAtlas atlas)
		{
			this.atlas = atlas;
		}
	}

	private UISprite sprite;

	private AtlasEntry entry;

	private static Dictionary<UIAtlas, AtlasEntry> atlases = new Dictionary<UIAtlas, AtlasEntry>();

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
			entry.refCount--;
			entry = null;
		}
		if (atlases.TryGetValue(sprite.atlas, out entry) && !(bool)entry.atlas)
		{
			atlases.Remove(sprite.atlas);
			entry = null;
		}
		if (entry == null)
		{
			UIAtlas uIAtlas = ResourceUtility.Instantiate(sprite.atlas);
			uIAtlas.spriteMaterial = new Material(uIAtlas.spriteMaterial);
			uIAtlas.spriteMaterial.shader = ResourceUtility.FindShader(shaderName);
			entry = new AtlasEntry(uIAtlas);
			atlases.Add(sprite.atlas, entry);
			if (MonoBehaviourSingleton<AppMain>.IsValid())
			{
				uIAtlas.transform.parent = MonoBehaviourSingleton<AppMain>.I._transform;
			}
		}
		entry.refCount++;
		sprite.atlas = entry.atlas;
	}

	private void OnDestroy()
	{
		if (!AppMain.isApplicationQuit && entry != null)
		{
			entry.refCount--;
			if (entry.refCount <= 0)
			{
				UIAtlas uIAtlas = null;
				foreach (KeyValuePair<UIAtlas, AtlasEntry> atlase in atlases)
				{
					if (atlase.Value == entry)
					{
						uIAtlas = atlase.Key;
					}
				}
				if ((Object)null != (Object)uIAtlas)
				{
					atlases.Remove(uIAtlas);
				}
				if ((bool)entry.atlas)
				{
					Object.Destroy(entry.atlas.spriteMaterial);
					Object.Destroy(entry.atlas.gameObject);
				}
			}
		}
	}
}
