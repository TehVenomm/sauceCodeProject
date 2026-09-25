// Decompiled with JetBrains decompiler
// Type: UISpriteMask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UISpriteMask : MonoBehaviour
{
  private static readonly int MASK_DEPTH = 100;
  [SerializeField]
  private float cutoff = 0.5f;
  [SerializeField]
  private UIWidget maskingObject;
  private UISprite sprite;
  private UITexture maskSprite;
  private UITexture maskedSprite;
  private bool valid;
  private static Dictionary<Material, UISpriteMask.MaterialEntry> maskMaterials = new Dictionary<Material, UISpriteMask.MaterialEntry>();

  private void Awake()
  {
    this.sprite = ((Component) this).GetComponent<UISprite>();
    if (!Object.op_Implicit((Object) this.sprite))
      return;
    this.SetupMaskSprite();
  }

  private void SetupMaskSprite()
  {
    Material material1 = this.GetMaterial(this.sprite.material, ResourceUtility.FindShader("mobile/Custom/UI/ui_alpha_mask"));
    UISpriteData atlasSprite1 = this.sprite.GetAtlasSprite();
    Rect texCoords1 = NGUIMath.ConvertToTexCoords(new Rect((float) atlasSprite1.x, (float) atlasSprite1.y, (float) atlasSprite1.width, (float) atlasSprite1.height), material1.mainTexture.width, material1.mainTexture.height);
    this.maskSprite = new GameObject(((Object) this.sprite).name).AddComponent<UITexture>();
    ((Component) this.maskSprite).gameObject.layer = ((Component) this.sprite).gameObject.layer;
    ((Component) this.maskSprite).transform.parent = ((Component) this.sprite).transform;
    ((Component) this.maskSprite).transform.localPosition = Vector3.zero;
    ((Component) this.maskSprite).transform.localScale = Vector3.one;
    this.maskSprite.depth = this.sprite.depth + UISpriteMask.MASK_DEPTH;
    this.maskSprite.width = this.sprite.width;
    this.maskSprite.height = this.sprite.height;
    this.maskSprite.uvRect = texCoords1;
    this.maskSprite.material = material1;
    if (Object.op_Implicit((Object) this.maskingObject))
    {
      UISprite maskingObject = this.maskingObject as UISprite;
      if (Object.op_Implicit((Object) maskingObject))
      {
        Shader shader = ResourceUtility.FindShader("mobile/Custom/UI/ui_add_depth_greater");
        Material material2 = this.GetMaterial(maskingObject.material, shader);
        UISpriteData atlasSprite2 = maskingObject.GetAtlasSprite();
        Rect texCoords2 = NGUIMath.ConvertToTexCoords(new Rect((float) atlasSprite2.x, (float) atlasSprite2.y, (float) atlasSprite2.width, (float) atlasSprite2.height), material2.mainTexture.width, material2.mainTexture.height);
        this.maskedSprite = new GameObject(((Object) maskingObject).name).AddComponent<UITexture>();
        ((Component) this.maskedSprite).gameObject.layer = ((Component) maskingObject).gameObject.layer;
        ((Component) this.maskedSprite).transform.parent = ((Component) maskingObject).transform;
        ((Component) this.maskedSprite).transform.localPosition = Vector3.zero;
        ((Component) this.maskedSprite).transform.localScale = Vector3.one;
        this.maskedSprite.depth = this.sprite.depth + UISpriteMask.MASK_DEPTH + 1;
        this.maskedSprite.width = maskingObject.width;
        this.maskedSprite.height = maskingObject.height;
        this.maskedSprite.uvRect = texCoords2;
        this.maskedSprite.color = maskingObject.color;
        this.maskedSprite.material = material2;
        ((Behaviour) maskingObject).enabled = false;
      }
      else
        this.maskingObject.depth = this.sprite.depth + UISpriteMask.MASK_DEPTH + 1;
    }
    this.valid = true;
  }

  private void OnDestroy()
  {
    if (!this.valid)
      return;
    if (!AppMain.isApplicationQuit)
    {
      if (Object.op_Implicit((Object) this.maskSprite))
      {
        if (Object.op_Implicit((Object) this.sprite))
        {
          this.ReleaseMaterial(this.sprite.material);
        }
        else
        {
          Material originalMaterial = this.FindOriginalMaterial(this.maskSprite.material);
          if (Object.op_Implicit((Object) originalMaterial))
            this.ReleaseMaterial(originalMaterial);
        }
      }
      if (!Object.op_Implicit((Object) this.maskedSprite))
        return;
      UISprite maskingObject = this.maskingObject as UISprite;
      if (Object.op_Implicit((Object) maskingObject))
      {
        this.ReleaseMaterial(maskingObject.material);
      }
      else
      {
        Material originalMaterial = this.FindOriginalMaterial(this.maskedSprite.material);
        if (!Object.op_Implicit((Object) originalMaterial))
          return;
        this.ReleaseMaterial(originalMaterial);
      }
    }
    else
      UISpriteMask.maskMaterials.Clear();
  }

  private Material FindOriginalMaterial(Material mat)
  {
    foreach (KeyValuePair<Material, UISpriteMask.MaterialEntry> maskMaterial in UISpriteMask.maskMaterials)
    {
      if (Object.op_Equality((Object) maskMaterial.Value.material, (Object) mat))
        return maskMaterial.Key;
    }
    return (Material) null;
  }

  private Material GetMaterial(Material orig, Shader shader)
  {
    UISpriteMask.MaterialEntry materialEntry;
    if (!UISpriteMask.maskMaterials.TryGetValue(orig, out materialEntry))
    {
      Material material = new Material(shader);
      try
      {
        material.mainTexture = orig.mainTexture;
      }
      catch (UnassignedReferenceException ex)
      {
        Debug.Log((object) ("UISPriteMask Error:" + (object) ex));
      }
      material.SetFloat("_Cutoff", this.cutoff);
      materialEntry = new UISpriteMask.MaterialEntry(material);
      UISpriteMask.maskMaterials.Add(orig, materialEntry);
    }
    ++materialEntry.refCount;
    return materialEntry.material;
  }

  private void ReleaseMaterial(Material orig)
  {
    UISpriteMask.MaterialEntry materialEntry = (UISpriteMask.MaterialEntry) null;
    if (UISpriteMask.maskMaterials.TryGetValue(orig, out materialEntry))
    {
      --materialEntry.refCount;
      if (materialEntry.refCount != 0)
        return;
      UISpriteMask.maskMaterials.Remove(orig);
      Object.Destroy((Object) materialEntry.material);
    }
    else
      Object.Destroy((Object) orig);
  }

  private class MaterialEntry
  {
    public Material material;
    public int refCount;

    public MaterialEntry(Material material) => this.material = material;
  }
}
