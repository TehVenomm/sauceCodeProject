using System.Collections.Generic;
using UnityEngine;

public class UISpriteMask
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

	public UISpriteMask()
		: this()
	{
	}

	private void Awake()
	{
		sprite = this.GetComponent<UISprite>();
		if (Object.op_Implicit(sprite))
		{
			SetupMaskSprite();
		}
	}

	private void SetupMaskSprite()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		Shader shader = ResourceUtility.FindShader("mobile/Custom/UI/ui_alpha_mask");
		Material material = GetMaterial(sprite.material, shader);
		UISpriteData atlasSprite = sprite.GetAtlasSprite();
		Rect rect = default(Rect);
		rect._002Ector((float)atlasSprite.x, (float)atlasSprite.y, (float)atlasSprite.width, (float)atlasSprite.height);
		Rect uvRect = NGUIMath.ConvertToTexCoords(rect, material.get_mainTexture().get_width(), material.get_mainTexture().get_height());
		maskSprite = new GameObject(sprite.get_name()).AddComponent<UITexture>();
		maskSprite.get_gameObject().set_layer(sprite.get_gameObject().get_layer());
		maskSprite.get_transform().set_parent(sprite.get_transform());
		maskSprite.get_transform().set_localPosition(Vector3.get_zero());
		maskSprite.get_transform().set_localScale(Vector3.get_one());
		maskSprite.depth = sprite.depth + MASK_DEPTH;
		maskSprite.width = sprite.width;
		maskSprite.height = sprite.height;
		maskSprite.uvRect = uvRect;
		maskSprite.material = material;
		if (Object.op_Implicit(maskingObject))
		{
			UISprite uISprite = maskingObject as UISprite;
			if (Object.op_Implicit(uISprite))
			{
				Shader shader2 = ResourceUtility.FindShader("mobile/Custom/UI/ui_add_depth_greater");
				Material material2 = GetMaterial(uISprite.material, shader2);
				UISpriteData atlasSprite2 = uISprite.GetAtlasSprite();
				Rect rect2 = default(Rect);
				rect2._002Ector((float)atlasSprite2.x, (float)atlasSprite2.y, (float)atlasSprite2.width, (float)atlasSprite2.height);
				Rect uvRect2 = NGUIMath.ConvertToTexCoords(rect2, material2.get_mainTexture().get_width(), material2.get_mainTexture().get_height());
				maskedSprite = new GameObject(uISprite.get_name()).AddComponent<UITexture>();
				maskedSprite.get_gameObject().set_layer(uISprite.get_gameObject().get_layer());
				maskedSprite.get_transform().set_parent(uISprite.get_transform());
				maskedSprite.get_transform().set_localPosition(Vector3.get_zero());
				maskedSprite.get_transform().set_localScale(Vector3.get_one());
				maskedSprite.depth = sprite.depth + MASK_DEPTH + 1;
				maskedSprite.width = uISprite.width;
				maskedSprite.height = uISprite.height;
				maskedSprite.uvRect = uvRect2;
				maskedSprite.color = uISprite.color;
				maskedSprite.material = material2;
				uISprite.set_enabled(false);
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
		if (valid)
		{
			if (!AppMain.isApplicationQuit)
			{
				if (Object.op_Implicit(maskSprite))
				{
					if (Object.op_Implicit(sprite))
					{
						ReleaseMaterial(sprite.material);
					}
					else
					{
						Material val = FindOriginalMaterial(maskSprite.material);
						if (Object.op_Implicit(val))
						{
							ReleaseMaterial(val);
						}
					}
				}
				if (Object.op_Implicit(maskedSprite))
				{
					UISprite uISprite = maskingObject as UISprite;
					if (Object.op_Implicit(uISprite))
					{
						ReleaseMaterial(uISprite.material);
					}
					else
					{
						Material val2 = FindOriginalMaterial(maskedSprite.material);
						if (Object.op_Implicit(val2))
						{
							ReleaseMaterial(val2);
						}
					}
				}
			}
			else
			{
				maskMaterials.Clear();
			}
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
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		if (!maskMaterials.TryGetValue(orig, out MaterialEntry value))
		{
			Material val = new Material(shader);
			try
			{
				val.set_mainTexture(orig.get_mainTexture());
			}
			catch (UnassignedReferenceException val2)
			{
				UnassignedReferenceException val3 = val2;
			}
			val.SetFloat("_Cutoff", cutoff);
			value = new MaterialEntry(val);
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
