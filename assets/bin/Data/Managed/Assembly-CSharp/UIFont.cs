using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/UI/NGUI Font")]
[ExecuteInEditMode]
public class UIFont : MonoBehaviour
{
	[SerializeField]
	[HideInInspector]
	private Material mMat;

	[HideInInspector]
	[SerializeField]
	private Rect mUVRect = new Rect(0f, 0f, 1f, 1f);

	[SerializeField]
	[HideInInspector]
	private BMFont mFont = new BMFont();

	[SerializeField]
	[HideInInspector]
	private UIAtlas mAtlas;

	[SerializeField]
	[HideInInspector]
	private UIFont mReplacement;

	[HideInInspector]
	[SerializeField]
	private List<BMSymbol> mSymbols = new List<BMSymbol>();

	[SerializeField]
	[HideInInspector]
	private Font mDynamicFont;

	[SerializeField]
	[HideInInspector]
	private int mDynamicFontSize = 16;

	[SerializeField]
	[HideInInspector]
	private FontStyle mDynamicFontStyle;

	[NonSerialized]
	private UISpriteData mSprite;

	private int mPMA = -1;

	private int mPacked = -1;

	public BMFont bmFont
	{
		get
		{
			return (!((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)) ? mFont : mReplacement.bmFont;
		}
		set
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				mReplacement.bmFont = value;
			}
			else
			{
				mFont = value;
			}
		}
	}

	public int texWidth
	{
		get
		{
			return ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null) ? mReplacement.texWidth : ((mFont == null) ? 1 : mFont.texWidth);
		}
		set
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				mReplacement.texWidth = value;
			}
			else if (mFont != null)
			{
				mFont.texWidth = value;
			}
		}
	}

	public int texHeight
	{
		get
		{
			return ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null) ? mReplacement.texHeight : ((mFont == null) ? 1 : mFont.texHeight);
		}
		set
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				mReplacement.texHeight = value;
			}
			else if (mFont != null)
			{
				mFont.texHeight = value;
			}
		}
	}

	public bool hasSymbols => ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null) ? mReplacement.hasSymbols : (mSymbols != null && mSymbols.Count != 0);

	public List<BMSymbol> symbols => (!((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)) ? mSymbols : mReplacement.symbols;

	public UIAtlas atlas
	{
		get
		{
			return (!((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)) ? mAtlas : mReplacement.atlas;
		}
		set
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				mReplacement.atlas = value;
			}
			else if ((UnityEngine.Object)mAtlas != (UnityEngine.Object)value)
			{
				mPMA = -1;
				mAtlas = value;
				if ((UnityEngine.Object)mAtlas != (UnityEngine.Object)null)
				{
					mMat = mAtlas.spriteMaterial;
					if (sprite != null)
					{
						mUVRect = uvRect;
					}
				}
				MarkAsChanged();
			}
		}
	}

	public Material material
	{
		get
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				return mReplacement.material;
			}
			if ((UnityEngine.Object)mAtlas != (UnityEngine.Object)null)
			{
				return mAtlas.spriteMaterial;
			}
			if ((UnityEngine.Object)mMat != (UnityEngine.Object)null)
			{
				if ((UnityEngine.Object)mDynamicFont != (UnityEngine.Object)null && (UnityEngine.Object)mMat != (UnityEngine.Object)mDynamicFont.material)
				{
					mMat.mainTexture = mDynamicFont.material.mainTexture;
				}
				return mMat;
			}
			if ((UnityEngine.Object)mDynamicFont != (UnityEngine.Object)null)
			{
				return mDynamicFont.material;
			}
			return null;
		}
		set
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				mReplacement.material = value;
			}
			else if ((UnityEngine.Object)mMat != (UnityEngine.Object)value)
			{
				mPMA = -1;
				mMat = value;
				MarkAsChanged();
			}
		}
	}

	[Obsolete("Use UIFont.premultipliedAlphaShader instead")]
	public bool premultipliedAlpha
	{
		get
		{
			return premultipliedAlphaShader;
		}
	}

	public bool premultipliedAlphaShader
	{
		get
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				return mReplacement.premultipliedAlphaShader;
			}
			if ((UnityEngine.Object)mAtlas != (UnityEngine.Object)null)
			{
				return mAtlas.premultipliedAlpha;
			}
			if (mPMA == -1)
			{
				Material material = this.material;
				mPMA = (((UnityEngine.Object)material != (UnityEngine.Object)null && (UnityEngine.Object)material.shader != (UnityEngine.Object)null && material.shader.name.Contains("Premultiplied")) ? 1 : 0);
			}
			return mPMA == 1;
		}
	}

	public bool packedFontShader
	{
		get
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				return mReplacement.packedFontShader;
			}
			if ((UnityEngine.Object)mAtlas != (UnityEngine.Object)null)
			{
				return false;
			}
			if (mPacked == -1)
			{
				Material material = this.material;
				mPacked = (((UnityEngine.Object)material != (UnityEngine.Object)null && (UnityEngine.Object)material.shader != (UnityEngine.Object)null && material.shader.name.Contains("Packed")) ? 1 : 0);
			}
			return mPacked == 1;
		}
	}

	public Texture2D texture
	{
		get
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				return mReplacement.texture;
			}
			Material material = this.material;
			return (!((UnityEngine.Object)material != (UnityEngine.Object)null)) ? null : (material.mainTexture as Texture2D);
		}
	}

	public Rect uvRect
	{
		get
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				return mReplacement.uvRect;
			}
			return (!((UnityEngine.Object)mAtlas != (UnityEngine.Object)null) || sprite == null) ? new Rect(0f, 0f, 1f, 1f) : mUVRect;
		}
		set
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				mReplacement.uvRect = value;
			}
			else if (sprite == null && mUVRect != value)
			{
				mUVRect = value;
				MarkAsChanged();
			}
		}
	}

	public string spriteName
	{
		get
		{
			return (!((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)) ? mFont.spriteName : mReplacement.spriteName;
		}
		set
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				mReplacement.spriteName = value;
			}
			else if (mFont.spriteName != value)
			{
				mFont.spriteName = value;
				MarkAsChanged();
			}
		}
	}

	public bool isValid => (UnityEngine.Object)mDynamicFont != (UnityEngine.Object)null || mFont.isValid;

	[Obsolete("Use UIFont.defaultSize instead")]
	public int size
	{
		get
		{
			return defaultSize;
		}
		set
		{
			defaultSize = value;
		}
	}

	public int defaultSize
	{
		get
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				return mReplacement.defaultSize;
			}
			if (isDynamic || mFont == null)
			{
				return mDynamicFontSize;
			}
			return mFont.charSize;
		}
		set
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				mReplacement.defaultSize = value;
			}
			else
			{
				mDynamicFontSize = value;
			}
		}
	}

	public UISpriteData sprite
	{
		get
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				return mReplacement.sprite;
			}
			if (mSprite == null && (UnityEngine.Object)mAtlas != (UnityEngine.Object)null && !string.IsNullOrEmpty(mFont.spriteName))
			{
				mSprite = mAtlas.GetSprite(mFont.spriteName);
				if (mSprite == null)
				{
					mSprite = mAtlas.GetSprite(base.name);
				}
				if (mSprite == null)
				{
					mFont.spriteName = null;
				}
				else
				{
					UpdateUVRect();
				}
				int i = 0;
				for (int count = mSymbols.Count; i < count; i++)
				{
					symbols[i].MarkAsChanged();
				}
			}
			return mSprite;
		}
	}

	public UIFont replacement
	{
		get
		{
			return mReplacement;
		}
		set
		{
			UIFont uIFont = value;
			if ((UnityEngine.Object)uIFont == (UnityEngine.Object)this)
			{
				uIFont = null;
			}
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)uIFont)
			{
				if ((UnityEngine.Object)uIFont != (UnityEngine.Object)null && (UnityEngine.Object)uIFont.replacement == (UnityEngine.Object)this)
				{
					uIFont.replacement = null;
				}
				if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
				{
					MarkAsChanged();
				}
				mReplacement = uIFont;
				if ((UnityEngine.Object)uIFont != (UnityEngine.Object)null)
				{
					mPMA = -1;
					mMat = null;
					mFont = null;
					mDynamicFont = null;
				}
				MarkAsChanged();
			}
		}
	}

	public bool isDynamic => (!((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)) ? ((UnityEngine.Object)mDynamicFont != (UnityEngine.Object)null) : mReplacement.isDynamic;

	public Font dynamicFont
	{
		get
		{
			return (!((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)) ? mDynamicFont : mReplacement.dynamicFont;
		}
		set
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				mReplacement.dynamicFont = value;
			}
			else if ((UnityEngine.Object)mDynamicFont != (UnityEngine.Object)value)
			{
				if ((UnityEngine.Object)mDynamicFont != (UnityEngine.Object)null)
				{
					material = null;
				}
				mDynamicFont = value;
				MarkAsChanged();
			}
		}
	}

	public FontStyle dynamicFontStyle
	{
		get
		{
			return (!((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)) ? mDynamicFontStyle : mReplacement.dynamicFontStyle;
		}
		set
		{
			if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
			{
				mReplacement.dynamicFontStyle = value;
			}
			else if (mDynamicFontStyle != value)
			{
				mDynamicFontStyle = value;
				MarkAsChanged();
			}
		}
	}

	private Texture dynamicTexture
	{
		get
		{
			if ((bool)mReplacement)
			{
				return mReplacement.dynamicTexture;
			}
			if (isDynamic)
			{
				return mDynamicFont.material.mainTexture;
			}
			return null;
		}
	}

	private void Trim()
	{
		Texture texture = mAtlas.texture;
		if ((UnityEngine.Object)texture != (UnityEngine.Object)null && mSprite != null)
		{
			Rect rect = NGUIMath.ConvertToPixels(mUVRect, this.texture.width, this.texture.height, true);
			Rect rect2 = new Rect((float)mSprite.x, (float)mSprite.y, (float)mSprite.width, (float)mSprite.height);
			int xMin = Mathf.RoundToInt(rect2.xMin - rect.xMin);
			int yMin = Mathf.RoundToInt(rect2.yMin - rect.yMin);
			int xMax = Mathf.RoundToInt(rect2.xMax - rect.xMin);
			int yMax = Mathf.RoundToInt(rect2.yMax - rect.yMin);
			mFont.Trim(xMin, yMin, xMax, yMax);
		}
	}

	private bool References(UIFont font)
	{
		if ((UnityEngine.Object)font == (UnityEngine.Object)null)
		{
			return false;
		}
		if ((UnityEngine.Object)font == (UnityEngine.Object)this)
		{
			return true;
		}
		return (UnityEngine.Object)mReplacement != (UnityEngine.Object)null && mReplacement.References(font);
	}

	public static bool CheckIfRelated(UIFont a, UIFont b)
	{
		if ((UnityEngine.Object)a == (UnityEngine.Object)null || (UnityEngine.Object)b == (UnityEngine.Object)null)
		{
			return false;
		}
		if (a.isDynamic && b.isDynamic && a.dynamicFont.fontNames[0] == b.dynamicFont.fontNames[0])
		{
			return true;
		}
		return (UnityEngine.Object)a == (UnityEngine.Object)b || a.References(b) || b.References(a);
	}

	public void MarkAsChanged()
	{
		if ((UnityEngine.Object)mReplacement != (UnityEngine.Object)null)
		{
			mReplacement.MarkAsChanged();
		}
		mSprite = null;
		UILabel[] array = NGUITools.FindActive<UILabel>();
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			UILabel uILabel = array[i];
			if (uILabel.enabled && NGUITools.GetActive(uILabel.gameObject) && CheckIfRelated(this, uILabel.bitmapFont))
			{
				UIFont bitmapFont = uILabel.bitmapFont;
				uILabel.bitmapFont = null;
				uILabel.bitmapFont = bitmapFont;
			}
		}
		int j = 0;
		for (int count = symbols.Count; j < count; j++)
		{
			symbols[j].MarkAsChanged();
		}
	}

	public void UpdateUVRect()
	{
		if (!((UnityEngine.Object)mAtlas == (UnityEngine.Object)null))
		{
			Texture texture = mAtlas.texture;
			if ((UnityEngine.Object)texture != (UnityEngine.Object)null)
			{
				mUVRect = new Rect((float)(mSprite.x - mSprite.paddingLeft), (float)(mSprite.y - mSprite.paddingTop), (float)(mSprite.width + mSprite.paddingLeft + mSprite.paddingRight), (float)(mSprite.height + mSprite.paddingTop + mSprite.paddingBottom));
				mUVRect = NGUIMath.ConvertToTexCoords(mUVRect, texture.width, texture.height);
				if (mSprite.hasPadding)
				{
					Trim();
				}
			}
		}
	}

	private BMSymbol GetSymbol(string sequence, bool createIfMissing)
	{
		int i = 0;
		for (int count = mSymbols.Count; i < count; i++)
		{
			BMSymbol bMSymbol = mSymbols[i];
			if (bMSymbol.sequence == sequence)
			{
				return bMSymbol;
			}
		}
		if (createIfMissing)
		{
			BMSymbol bMSymbol2 = new BMSymbol();
			bMSymbol2.sequence = sequence;
			mSymbols.Add(bMSymbol2);
			return bMSymbol2;
		}
		return null;
	}

	public BMSymbol MatchSymbol(string text, int offset, int textLength)
	{
		int count = mSymbols.Count;
		if (count == 0)
		{
			return null;
		}
		textLength -= offset;
		for (int i = 0; i < count; i++)
		{
			BMSymbol bMSymbol = mSymbols[i];
			int length = bMSymbol.length;
			if (length != 0 && textLength >= length)
			{
				bool flag = true;
				for (int j = 0; j < length; j++)
				{
					if (text[offset + j] != bMSymbol.sequence[j])
					{
						flag = false;
						break;
					}
				}
				if (flag && bMSymbol.Validate(atlas))
				{
					return bMSymbol;
				}
			}
		}
		return null;
	}

	public void AddSymbol(string sequence, string spriteName)
	{
		BMSymbol symbol = GetSymbol(sequence, true);
		symbol.spriteName = spriteName;
		MarkAsChanged();
	}

	public void RemoveSymbol(string sequence)
	{
		BMSymbol symbol = GetSymbol(sequence, false);
		if (symbol != null)
		{
			symbols.Remove(symbol);
		}
		MarkAsChanged();
	}

	public void RenameSymbol(string before, string after)
	{
		BMSymbol symbol = GetSymbol(before, false);
		if (symbol != null)
		{
			symbol.sequence = after;
		}
		MarkAsChanged();
	}

	public bool UsesSprite(string s)
	{
		if (!string.IsNullOrEmpty(s))
		{
			if (s.Equals(spriteName))
			{
				return true;
			}
			int i = 0;
			for (int count = symbols.Count; i < count; i++)
			{
				BMSymbol bMSymbol = symbols[i];
				if (s.Equals(bMSymbol.spriteName))
				{
					return true;
				}
			}
		}
		return false;
	}
}
