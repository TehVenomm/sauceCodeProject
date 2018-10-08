using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/UI/NGUI Font")]
[ExecuteInEditMode]
public class UIFont
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

	[HideInInspector]
	[SerializeField]
	private UIFont mReplacement;

	[HideInInspector]
	[SerializeField]
	private List<BMSymbol> mSymbols = new List<BMSymbol>();

	[HideInInspector]
	[SerializeField]
	private Font mDynamicFont;

	[HideInInspector]
	[SerializeField]
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
			return (!(mReplacement != null)) ? mFont : mReplacement.bmFont;
		}
		set
		{
			if (mReplacement != null)
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
			return (mReplacement != null) ? mReplacement.texWidth : ((mFont == null) ? 1 : mFont.texWidth);
		}
		set
		{
			if (mReplacement != null)
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
			return (mReplacement != null) ? mReplacement.texHeight : ((mFont == null) ? 1 : mFont.texHeight);
		}
		set
		{
			if (mReplacement != null)
			{
				mReplacement.texHeight = value;
			}
			else if (mFont != null)
			{
				mFont.texHeight = value;
			}
		}
	}

	public bool hasSymbols => (mReplacement != null) ? mReplacement.hasSymbols : (mSymbols != null && mSymbols.Count != 0);

	public List<BMSymbol> symbols => (!(mReplacement != null)) ? mSymbols : mReplacement.symbols;

	public UIAtlas atlas
	{
		get
		{
			return (!(mReplacement != null)) ? mAtlas : mReplacement.atlas;
		}
		set
		{
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			if (mReplacement != null)
			{
				mReplacement.atlas = value;
			}
			else if (mAtlas != value)
			{
				mPMA = -1;
				mAtlas = value;
				if (mAtlas != null)
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
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Expected O, but got Unknown
			if (mReplacement != null)
			{
				return mReplacement.material;
			}
			if (mAtlas != null)
			{
				return mAtlas.spriteMaterial;
			}
			if (mMat != null)
			{
				if (mDynamicFont != null && mMat != mDynamicFont.get_material())
				{
					mMat.set_mainTexture(mDynamicFont.get_material().get_mainTexture());
				}
				return mMat;
			}
			if (mDynamicFont != null)
			{
				return mDynamicFont.get_material();
			}
			return null;
		}
		set
		{
			if (mReplacement != null)
			{
				mReplacement.material = value;
			}
			else if (mMat != value)
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
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			if (mReplacement != null)
			{
				return mReplacement.premultipliedAlphaShader;
			}
			if (mAtlas != null)
			{
				return mAtlas.premultipliedAlpha;
			}
			if (mPMA == -1)
			{
				Material material = this.material;
				mPMA = ((material != null && material.get_shader() != null && material.get_shader().get_name().Contains("Premultiplied")) ? 1 : 0);
			}
			return mPMA == 1;
		}
	}

	public bool packedFontShader
	{
		get
		{
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			if (mReplacement != null)
			{
				return mReplacement.packedFontShader;
			}
			if (mAtlas != null)
			{
				return false;
			}
			if (mPacked == -1)
			{
				Material material = this.material;
				mPacked = ((material != null && material.get_shader() != null && material.get_shader().get_name().Contains("Packed")) ? 1 : 0);
			}
			return mPacked == 1;
		}
	}

	public Texture2D texture
	{
		get
		{
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Expected O, but got Unknown
			if (mReplacement != null)
			{
				return mReplacement.texture;
			}
			Material material = this.material;
			return (!(material != null)) ? null : (material.get_mainTexture() as Texture2D);
		}
	}

	public Rect uvRect
	{
		get
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			if (mReplacement != null)
			{
				return mReplacement.uvRect;
			}
			return (!(mAtlas != null) || sprite == null) ? new Rect(0f, 0f, 1f, 1f) : mUVRect;
		}
		set
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			if (mReplacement != null)
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
			return (!(mReplacement != null)) ? mFont.spriteName : mReplacement.spriteName;
		}
		set
		{
			if (mReplacement != null)
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

	public bool isValid => mDynamicFont != null || mFont.isValid;

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
			if (mReplacement != null)
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
			if (mReplacement != null)
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
			if (mReplacement != null)
			{
				return mReplacement.sprite;
			}
			if (mSprite == null && mAtlas != null && !string.IsNullOrEmpty(mFont.spriteName))
			{
				mSprite = mAtlas.GetSprite(mFont.spriteName);
				if (mSprite == null)
				{
					mSprite = mAtlas.GetSprite(this.get_name());
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
			if (uIFont == this)
			{
				uIFont = null;
			}
			if (mReplacement != uIFont)
			{
				if (uIFont != null && uIFont.replacement == this)
				{
					uIFont.replacement = null;
				}
				if (mReplacement != null)
				{
					MarkAsChanged();
				}
				mReplacement = uIFont;
				if (uIFont != null)
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

	public bool isDynamic => (!(mReplacement != null)) ? (mDynamicFont != null) : mReplacement.isDynamic;

	public Font dynamicFont
	{
		get
		{
			return (!(mReplacement != null)) ? ((object)mDynamicFont) : ((object)mReplacement.dynamicFont);
		}
		set
		{
			if (mReplacement != null)
			{
				mReplacement.dynamicFont = value;
			}
			else if (mDynamicFont != value)
			{
				if (mDynamicFont != null)
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
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			return (!(mReplacement != null)) ? mDynamicFontStyle : mReplacement.dynamicFontStyle;
		}
		set
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			if (mReplacement != null)
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
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			if (Object.op_Implicit(mReplacement))
			{
				return mReplacement.dynamicTexture;
			}
			if (isDynamic)
			{
				return mDynamicFont.get_material().get_mainTexture();
			}
			return null;
		}
	}

	public UIFont()
		: this()
	{
	}//IL_0015: Unknown result type (might be due to invalid IL or missing references)
	//IL_001a: Unknown result type (might be due to invalid IL or missing references)


	private void Trim()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Texture texture = mAtlas.texture;
		if (texture != null && mSprite != null)
		{
			Rect val = NGUIMath.ConvertToPixels(mUVRect, this.texture.get_width(), this.texture.get_height(), true);
			Rect val2 = default(Rect);
			val2._002Ector((float)mSprite.x, (float)mSprite.y, (float)mSprite.width, (float)mSprite.height);
			int xMin = Mathf.RoundToInt(val2.get_xMin() - val.get_xMin());
			int yMin = Mathf.RoundToInt(val2.get_yMin() - val.get_yMin());
			int xMax = Mathf.RoundToInt(val2.get_xMax() - val.get_xMin());
			int yMax = Mathf.RoundToInt(val2.get_yMax() - val.get_yMin());
			mFont.Trim(xMin, yMin, xMax, yMax);
		}
	}

	private bool References(UIFont font)
	{
		if (font == null)
		{
			return false;
		}
		if (font == this)
		{
			return true;
		}
		return mReplacement != null && mReplacement.References(font);
	}

	public static bool CheckIfRelated(UIFont a, UIFont b)
	{
		if (a == null || b == null)
		{
			return false;
		}
		if (a.isDynamic && b.isDynamic && a.dynamicFont.get_fontNames()[0] == b.dynamicFont.get_fontNames()[0])
		{
			return true;
		}
		return a == b || a.References(b) || b.References(a);
	}

	public void MarkAsChanged()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		if (mReplacement != null)
		{
			mReplacement.MarkAsChanged();
		}
		mSprite = null;
		UILabel[] array = NGUITools.FindActive<UILabel>();
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			UILabel uILabel = array[i];
			if (uILabel.get_enabled() && NGUITools.GetActive(uILabel.get_gameObject()) && CheckIfRelated(this, uILabel.bitmapFont))
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
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		if (!(mAtlas == null))
		{
			Texture texture = mAtlas.texture;
			if (texture != null)
			{
				mUVRect = new Rect((float)(mSprite.x - mSprite.paddingLeft), (float)(mSprite.y - mSprite.paddingTop), (float)(mSprite.width + mSprite.paddingLeft + mSprite.paddingRight), (float)(mSprite.height + mSprite.paddingTop + mSprite.paddingBottom));
				mUVRect = NGUIMath.ConvertToTexCoords(mUVRect, texture.get_width(), texture.get_height());
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
