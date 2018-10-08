using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Sprite")]
public class UISprite : UIBasicSprite
{
	[SerializeField]
	[HideInInspector]
	private UIAtlas mAtlas;

	[HideInInspector]
	[SerializeField]
	private string mSpriteName;

	[HideInInspector]
	[SerializeField]
	private bool mFillCenter = true;

	[NonSerialized]
	protected UISpriteData mSprite;

	[NonSerialized]
	private bool mSpriteSet;

	public override Material material => (!((UnityEngine.Object)mAtlas != (UnityEngine.Object)null)) ? null : mAtlas.spriteMaterial;

	public UIAtlas atlas
	{
		get
		{
			return mAtlas;
		}
		set
		{
			if ((UnityEngine.Object)mAtlas != (UnityEngine.Object)value)
			{
				RemoveFromPanel();
				mAtlas = value;
				mSpriteSet = false;
				mSprite = null;
				if (string.IsNullOrEmpty(mSpriteName) && (UnityEngine.Object)mAtlas != (UnityEngine.Object)null && mAtlas.spriteList.Count > 0)
				{
					SetAtlasSprite(mAtlas.spriteList[0]);
					mSpriteName = mSprite.name;
				}
				if (!string.IsNullOrEmpty(mSpriteName))
				{
					string spriteName = mSpriteName;
					mSpriteName = string.Empty;
					this.spriteName = spriteName;
					MarkAsChanged();
				}
			}
		}
	}

	public string spriteName
	{
		get
		{
			return mSpriteName;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(mSpriteName))
				{
					mSpriteName = string.Empty;
					mSprite = null;
					mChanged = true;
					mSpriteSet = false;
				}
			}
			else if (mSpriteName != value)
			{
				mSpriteName = value;
				mSprite = null;
				mChanged = true;
				mSpriteSet = false;
			}
		}
	}

	public bool isValid => GetAtlasSprite() != null;

	[Obsolete("Use 'centerType' instead")]
	public bool fillCenter
	{
		get
		{
			return centerType != AdvancedType.Invisible;
		}
		set
		{
			if (value != (centerType != AdvancedType.Invisible))
			{
				centerType = (value ? AdvancedType.Sliced : AdvancedType.Invisible);
				MarkAsChanged();
			}
		}
	}

	public override Vector4 border
	{
		get
		{
			UISpriteData atlasSprite = GetAtlasSprite();
			if (atlasSprite == null)
			{
				return base.border;
			}
			return new Vector4((float)atlasSprite.borderLeft, (float)atlasSprite.borderBottom, (float)atlasSprite.borderRight, (float)atlasSprite.borderTop);
		}
	}

	public override float pixelSize => (!((UnityEngine.Object)mAtlas != (UnityEngine.Object)null)) ? 1f : mAtlas.pixelSize;

	public override int minWidth
	{
		get
		{
			if (type == Type.Sliced || type == Type.Advanced)
			{
				float pixelSize = this.pixelSize;
				Vector4 vector = border * this.pixelSize;
				int num = Mathf.RoundToInt(vector.x + vector.z);
				UISpriteData atlasSprite = GetAtlasSprite();
				if (atlasSprite != null)
				{
					num += Mathf.RoundToInt(pixelSize * (float)(atlasSprite.paddingLeft + atlasSprite.paddingRight));
				}
				return Mathf.Max(base.minWidth, ((num & 1) != 1) ? num : (num + 1));
			}
			return base.minWidth;
		}
	}

	public override int minHeight
	{
		get
		{
			if (type == Type.Sliced || type == Type.Advanced)
			{
				Vector4 vector = border * pixelSize;
				int num = Mathf.RoundToInt(vector.y + vector.w);
				UISpriteData atlasSprite = GetAtlasSprite();
				if (atlasSprite != null)
				{
					num += atlasSprite.paddingTop + atlasSprite.paddingBottom;
				}
				return Mathf.Max(base.minHeight, ((num & 1) != 1) ? num : (num + 1));
			}
			return base.minHeight;
		}
	}

	public override Vector4 drawingDimensions
	{
		get
		{
			Vector2 pivotOffset = base.pivotOffset;
			float num = (0f - pivotOffset.x) * (float)mWidth;
			float num2 = (0f - pivotOffset.y) * (float)mHeight;
			float num3 = num + (float)mWidth;
			float num4 = num2 + (float)mHeight;
			if (GetAtlasSprite() != null && mType != Type.Tiled)
			{
				int num5 = mSprite.paddingLeft;
				int num6 = mSprite.paddingBottom;
				int num7 = mSprite.paddingRight;
				int num8 = mSprite.paddingTop;
				float pixelSize = this.pixelSize;
				if (pixelSize != 1f)
				{
					num5 = Mathf.RoundToInt(pixelSize * (float)num5);
					num6 = Mathf.RoundToInt(pixelSize * (float)num6);
					num7 = Mathf.RoundToInt(pixelSize * (float)num7);
					num8 = Mathf.RoundToInt(pixelSize * (float)num8);
				}
				int num9 = mSprite.width + num5 + num7;
				int num10 = mSprite.height + num6 + num8;
				float num11 = 1f;
				float num12 = 1f;
				if (num9 > 0 && num10 > 0 && (mType == Type.Simple || mType == Type.Filled))
				{
					if ((num9 & 1) != 0)
					{
						num7++;
					}
					if ((num10 & 1) != 0)
					{
						num8++;
					}
					num11 = 1f / (float)num9 * (float)mWidth;
					num12 = 1f / (float)num10 * (float)mHeight;
				}
				if (mFlip == Flip.Horizontally || mFlip == Flip.Both)
				{
					num += (float)num7 * num11;
					num3 -= (float)num5 * num11;
				}
				else
				{
					num += (float)num5 * num11;
					num3 -= (float)num7 * num11;
				}
				if (mFlip == Flip.Vertically || mFlip == Flip.Both)
				{
					num2 += (float)num8 * num12;
					num4 -= (float)num6 * num12;
				}
				else
				{
					num2 += (float)num6 * num12;
					num4 -= (float)num8 * num12;
				}
			}
			Vector4 vector = (!((UnityEngine.Object)mAtlas != (UnityEngine.Object)null)) ? Vector4.zero : (border * this.pixelSize);
			float num13 = vector.x + vector.z;
			float num14 = vector.y + vector.w;
			float x = Mathf.Lerp(num, num3 - num13, mDrawRegion.x);
			float y = Mathf.Lerp(num2, num4 - num14, mDrawRegion.y);
			float z = Mathf.Lerp(num + num13, num3, mDrawRegion.z);
			float w = Mathf.Lerp(num2 + num14, num4, mDrawRegion.w);
			return new Vector4(x, y, z, w);
		}
	}

	public override bool premultipliedAlpha => (UnityEngine.Object)mAtlas != (UnityEngine.Object)null && mAtlas.premultipliedAlpha;

	public UISpriteData GetAtlasSprite()
	{
		if (!mSpriteSet)
		{
			mSprite = null;
		}
		if (mSprite == null && (UnityEngine.Object)mAtlas != (UnityEngine.Object)null)
		{
			if (!string.IsNullOrEmpty(mSpriteName))
			{
				UISpriteData sprite = mAtlas.GetSprite(mSpriteName);
				if (sprite == null)
				{
					return null;
				}
				SetAtlasSprite(sprite);
			}
			if (mSprite == null && mAtlas.spriteList.Count > 0)
			{
				UISpriteData uISpriteData = mAtlas.spriteList[0];
				if (uISpriteData == null)
				{
					return null;
				}
				SetAtlasSprite(uISpriteData);
				if (mSprite == null)
				{
					Debug.LogError(mAtlas.name + " seems to have a null sprite!");
					return null;
				}
				mSpriteName = mSprite.name;
			}
		}
		return mSprite;
	}

	protected void SetAtlasSprite(UISpriteData sp)
	{
		mChanged = true;
		mSpriteSet = true;
		if (sp != null)
		{
			mSprite = sp;
			mSpriteName = mSprite.name;
		}
		else
		{
			mSpriteName = ((mSprite == null) ? string.Empty : mSprite.name);
			mSprite = sp;
		}
	}

	public override void MakePixelPerfect()
	{
		if (isValid)
		{
			base.MakePixelPerfect();
			if (mType != Type.Tiled)
			{
				UISpriteData atlasSprite = GetAtlasSprite();
				if (atlasSprite != null)
				{
					Texture mainTexture = this.mainTexture;
					if (!((UnityEngine.Object)mainTexture == (UnityEngine.Object)null) && (mType == Type.Simple || mType == Type.Filled || !atlasSprite.hasBorder) && (UnityEngine.Object)mainTexture != (UnityEngine.Object)null)
					{
						int num = Mathf.RoundToInt(pixelSize * (float)(atlasSprite.width + atlasSprite.paddingLeft + atlasSprite.paddingRight));
						int num2 = Mathf.RoundToInt(pixelSize * (float)(atlasSprite.height + atlasSprite.paddingTop + atlasSprite.paddingBottom));
						if ((num & 1) == 1)
						{
							num++;
						}
						if ((num2 & 1) == 1)
						{
							num2++;
						}
						base.width = num;
						base.height = num2;
					}
				}
			}
		}
	}

	protected override void OnInit()
	{
		if (!mFillCenter)
		{
			mFillCenter = true;
			centerType = AdvancedType.Invisible;
		}
		base.OnInit();
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (mChanged || !mSpriteSet)
		{
			mSpriteSet = true;
			mSprite = null;
			mChanged = true;
		}
	}

	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Texture mainTexture = this.mainTexture;
		if (!((UnityEngine.Object)mainTexture == (UnityEngine.Object)null))
		{
			if (mSprite == null)
			{
				mSprite = atlas.GetSprite(spriteName);
			}
			if (mSprite != null)
			{
				Rect rect = new Rect((float)mSprite.x, (float)mSprite.y, (float)mSprite.width, (float)mSprite.height);
				Rect rect2 = new Rect((float)(mSprite.x + mSprite.borderLeft), (float)(mSprite.y + mSprite.borderTop), (float)(mSprite.width - mSprite.borderLeft - mSprite.borderRight), (float)(mSprite.height - mSprite.borderBottom - mSprite.borderTop));
				rect = NGUIMath.ConvertToTexCoords(rect, mainTexture.width, mainTexture.height);
				rect2 = NGUIMath.ConvertToTexCoords(rect2, mainTexture.width, mainTexture.height);
				int size = verts.size;
				Fill(verts, uvs, cols, rect, rect2);
				if (onPostFill != null)
				{
					onPostFill(this, size, verts, uvs, cols);
				}
			}
		}
	}
}
