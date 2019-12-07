using System.Collections.Generic;
using UnityEngine;

public class UISpriteMask : MonoBehaviour
{
	private class MaterialEntry
	{
		public Material material;

		public int refCount;

		public MaterialEntry(Material material)
		{
			this.material = material;
		}
	}

	private static readonly int MASK_DEPTH = 100;

	[SerializeField]
	private float cutoff = 0.5f;

	[SerializeField]
	private UIWidget maskingObject;

	private UISprite sprite;

	private UITexture maskSprite;

	private UITexture maskedSprite;

	private bool valid;

	private static Dictionary<Material, MaterialEntry> maskMaterials = new Dictionary<Material, MaterialEntry>();

	private void Awake()
	{
		sprite = GetComponent<UISprite>();
		if ((bool)sprite)
		{
			SetupMaskSprite();
		}
	}

	private void SetupMaskSprite()
	{
		Shader shader = ResourceUtility.FindShader("mobile/Custom/UI/ui_alpha_mask");
		Material material = GetMaterial(sprite.material, shader);
		UISpriteData atlasSprite = sprite.GetAtlasSprite();
		Rect uvRect = NGUIMath.ConvertToTexCoords(new Rect(atlasSprite.x, atlasSprite.y, atlasSprite.width, atlasSprite.height), material.mainTexture.width, material.mainTexture.height);
		maskSprite = new GameObject(sprite.name).AddComponent<UITexture>();
		maskSprite.gameObject.layer = sprite.gameObject.layer;
		maskSprite.transform.parent = sprite.transform;
		maskSprite.transform.localPosition = Vector3.zero;
		maskSprite.transform.localScale = Vector3.one;
		maskSprite.depth = sprite.depth + MASK_DEPTH;
		maskSprite.width = sprite.width;
		maskSprite.height = sprite.height;
		maskSprite.uvRect = uvRect;
		maskSprite.material = material;
		if ((bool)maskingObject)
		{
			UISprite uISprite = maskingObject as UISprite;
			if ((bool)uISprite)
			{
				Shader shader2 = ResourceUtility.FindShader("mobile/Custom/UI/ui_add_depth_greater");
				Material material2 = GetMaterial(uISprite.material, shader2);
				UISpriteData atlasSprite2 = uISprite.GetAtlasSprite();
				Rect uvRect2 = NGUIMath.ConvertToTexCoords(new Rect(atlasSprite2.x, atlasSprite2.y, atlasSprite2.width, atlasSprite2.height), material2.mainTexture.width, material2.mainTexture.height);
				maskedSprite = new GameObject(uISprite.name).AddComponent<UITexture>();
				maskedSprite.gameObject.layer = uISprite.gameObject.layer;
				maskedSprite.transform.parent = uISprite.transform;
				maskedSprite.transform.localPosition = Vector3.zero;
				maskedSprite.transform.localScale = Vector3.one;
				maskedSprite.depth = sprite.depth + MASK_DEPTH + 1;
				maskedSprite.width = uISprite.width;
				maskedSprite.height = uISprite.height;
				maskedSprite.uvRect = uvRect2;
				maskedSprite.color = uISprite.color;
				maskedSprite.material = material2;
				uISprite.enabled = false;
			}
			else
			{
				maskingObject.depth = sprite.depth + MASK_DEPTH + 1;
			}
		}
		valid = true;
	}

	private void OnDestroy()
	{
		if (!valid)
		{
			return;
		}
		if (!AppMain.isApplicationQuit)
		{
			if ((bool)maskSprite)
			{
				if ((bool)sprite)
				{
					ReleaseMaterial(sprite.material);
				}
				else
				{
					Material material = FindOriginalMaterial(maskSprite.material);
					if ((bool)material)
					{
						ReleaseMaterial(material);
					}
				}
			}
			if (!maskedSprite)
			{
				return;
			}
			UISprite uISprite = maskingObject as UISprite;
			if ((bool)uISprite)
			{
				ReleaseMaterial(uISprite.material);
				return;
			}
			Material material2 = FindOriginalMaterial(maskedSprite.material);
			if ((bool)material2)
			{
				ReleaseMaterial(material2);
			}
		}
		else
		{
			maskMaterials.Clear();
		}
	}

	private Material FindOriginalMaterial(Material mat)
	{
		foreach (KeyValuePair<Material, MaterialEntry> maskMaterial in maskMaterials)
		{
			if (maskMaterial.Value.material == mat)
			{
				return maskMaterial.Key;
			}
		}
		return null;
	}

	private Material GetMaterial(Material orig, Shader shader)
	{
		if (!maskMaterials.TryGetValue(orig, out MaterialEntry value))
		{
			Material material = new Material(shader);
			try
			{
				material.mainTexture = orig.mainTexture;
			}
			catch (UnassignedReferenceException arg)
			{
				Debug.Log("UISPriteMask Error:" + arg);
			}
			material.SetFloat("_Cutoff", cutoff);
			value = new MaterialEntry(material);
			maskMaterials.Add(orig, value);
		}
		value.refCount++;
		return value.material;
	}

	private void ReleaseMaterial(Material orig)
	{
		MaterialEntry value = null;
		if (maskMaterials.TryGetValue(orig, out value))
		{
			value.refCount--;
			if (value.refCount == 0)
			{
				maskMaterials.Remove(orig);
				Object.Destroy(value.material);
			}
		}
		else
		{
			Object.Destroy(orig);
		}
	}
}
