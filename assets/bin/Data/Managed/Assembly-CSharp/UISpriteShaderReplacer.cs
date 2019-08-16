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

	public UISpriteShaderReplacer()
		: this()
	{
	}

	private void Awake()
	{
		sprite = this.GetComponent<UISprite>();
	}

	public void Replace(string shaderName)
	{
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
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
			entry.refCount--;
			entry = null;
		}
		if (atlases.TryGetValue(sprite.atlas, out entry) && !Object.op_Implicit(entry.atlas))
		{
			atlases.Remove(sprite.atlas);
			entry = null;
		}
		if (entry == null)
		{
			UIAtlas uIAtlas = ResourceUtility.Instantiate<UIAtlas>(sprite.atlas);
			uIAtlas.spriteMaterial = new Material(uIAtlas.spriteMaterial);
			uIAtlas.spriteMaterial.set_shader(ResourceUtility.FindShader(shaderName));
			entry = new AtlasEntry(uIAtlas);
			atlases.Add(sprite.atlas, entry);
			if (MonoBehaviourSingleton<AppMain>.IsValid())
			{
				uIAtlas.get_transform().set_parent(MonoBehaviourSingleton<AppMain>.I._transform);
			}
		}
		entry.refCount++;
		sprite.atlas = entry.atlas;
	}

	private void OnDestroy()
	{
		if (AppMain.isApplicationQuit || entry == null)
		{
			return;
		}
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
			if (null != uIAtlas)
			{
				atlases.Remove(uIAtlas);
			}
			if (Object.op_Implicit(entry.atlas))
			{
				Object.Destroy(entry.atlas.spriteMaterial);
				Object.Destroy(entry.atlas.get_gameObject());
			}
		}
	}
}
