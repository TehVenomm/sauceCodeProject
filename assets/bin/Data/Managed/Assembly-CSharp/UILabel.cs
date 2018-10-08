using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/UI/NGUI Label")]
[ExecuteInEditMode]
public class UILabel : UIWidget
{
	public enum Effect
	{
		None,
		Shadow,
		Outline,
		Outline8
	}

	public enum Overflow
	{
		ShrinkContent,
		ClampContent,
		ResizeFreely,
		ResizeHeight
	}

	public enum Crispness
	{
		Never,
		OnDesktop,
		Always
	}

	public static bool OutlineLimit = false;

	public Crispness keepCrispWhenShrunk = Crispness.OnDesktop;

	[HideInInspector]
	[SerializeField]
	private Font mTrueTypeFont;

	[HideInInspector]
	[SerializeField]
	private UIFont mFont;

	[SerializeField]
	[HideInInspector]
	[Multiline(6)]
	private string mText = string.Empty;

	[HideInInspector]
	[SerializeField]
	private int mFontSize = 16;

	[HideInInspector]
	[SerializeField]
	private FontStyle mFontStyle;

	[SerializeField]
	[HideInInspector]
	private NGUIText.Alignment mAlignment;

	[SerializeField]
	[HideInInspector]
	private bool mEncoding = true;

	[HideInInspector]
	[SerializeField]
	private int mMaxLineCount;

	[HideInInspector]
	[SerializeField]
	private Effect mEffectStyle;

	[SerializeField]
	[HideInInspector]
	private Color mEffectColor = Color.get_black();

	[SerializeField]
	[HideInInspector]
	private NGUIText.SymbolStyle mSymbols = NGUIText.SymbolStyle.Normal;

	[HideInInspector]
	[SerializeField]
	private Vector2 mEffectDistance = Vector2.get_one();

	[SerializeField]
	[HideInInspector]
	private Overflow mOverflow;

	[HideInInspector]
	[SerializeField]
	private Material mMaterial;

	[HideInInspector]
	[SerializeField]
	private bool mApplyGradient;

	[SerializeField]
	[HideInInspector]
	private Color mGradientTop = Color.get_white();

	[SerializeField]
	[HideInInspector]
	private Color mGradientBottom = new Color(0.7f, 0.7f, 0.7f);

	[HideInInspector]
	[SerializeField]
	private int mSpacingX;

	[HideInInspector]
	[SerializeField]
	private int mSpacingY;

	[HideInInspector]
	[SerializeField]
	private bool mUseFloatSpacing;

	[SerializeField]
	[HideInInspector]
	private float mFloatSpacingX;

	[SerializeField]
	[HideInInspector]
	private float mFloatSpacingY;

	[SerializeField]
	[HideInInspector]
	private bool mShrinkToFit;

	[SerializeField]
	[HideInInspector]
	private int mMaxLineWidth;

	[SerializeField]
	[HideInInspector]
	private int mMaxLineHeight;

	[HideInInspector]
	[SerializeField]
	private float mLineWidth;

	[HideInInspector]
	[SerializeField]
	private bool mMultiline = true;

	[NonSerialized]
	private Font mActiveTTF;

	private float mDensity = 1f;

	private bool mShouldBeProcessed = true;

	private string mProcessedText;

	private bool mPremultiply;

	private Vector2 mCalculatedSize = Vector2.get_zero();

	private float mScale = 1f;

	private int mPrintedSize;

	private int mLastWidth;

	private int mLastHeight;

	private static BetterList<UILabel> mList = new BetterList<UILabel>();

	private static Dictionary<Font, int> mFontUsage = new Dictionary<Font, int>();

	private static bool mTexRebuildAdded = false;

	private static BetterList<Vector3> mTempVerts = new BetterList<Vector3>();

	private static BetterList<int> mTempIndices = new BetterList<int>();

	private bool shouldBeProcessed
	{
		get
		{
			return mShouldBeProcessed;
		}
		set
		{
			if (value)
			{
				mChanged = true;
				mShouldBeProcessed = true;
			}
			else
			{
				mShouldBeProcessed = false;
			}
		}
	}

	public override bool isAnchoredHorizontally => base.isAnchoredHorizontally || mOverflow == Overflow.ResizeFreely;

	public override bool isAnchoredVertically => base.isAnchoredVertically || mOverflow == Overflow.ResizeFreely || mOverflow == Overflow.ResizeHeight;

	public override Material material
	{
		get
		{
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Expected O, but got Unknown
			if (mMaterial != null)
			{
				return mMaterial;
			}
			if (mFont != null)
			{
				return mFont.material;
			}
			if (mTrueTypeFont != null)
			{
				return mTrueTypeFont.get_material();
			}
			return null;
		}
		set
		{
			if (mMaterial != value)
			{
				RemoveFromPanel();
				mMaterial = value;
				MarkAsChanged();
			}
		}
	}

	[Obsolete("Use UILabel.bitmapFont instead")]
	public UIFont font
	{
		get
		{
			return bitmapFont;
		}
		set
		{
			bitmapFont = value;
		}
	}

	public UIFont bitmapFont
	{
		get
		{
			return mFont;
		}
		set
		{
			if (mFont != value)
			{
				RemoveFromPanel();
				mFont = value;
				mTrueTypeFont = null;
				MarkAsChanged();
			}
		}
	}

	public Font trueTypeFont
	{
		get
		{
			if (mTrueTypeFont != null)
			{
				return mTrueTypeFont;
			}
			return (!(mFont != null)) ? null : mFont.dynamicFont;
		}
		set
		{
			if (mTrueTypeFont != value)
			{
				SetActiveFont(null);
				RemoveFromPanel();
				mTrueTypeFont = value;
				shouldBeProcessed = true;
				mFont = null;
				SetActiveFont(value);
				ProcessAndRequest();
				if (mActiveTTF != null)
				{
					base.MarkAsChanged();
				}
			}
		}
	}

	public Object ambigiousFont
	{
		get
		{
			return ((object)mFont) ?? ((object)mTrueTypeFont);
		}
		set
		{
			UIFont uIFont = value as UIFont;
			if (uIFont != null)
			{
				bitmapFont = uIFont;
			}
			else
			{
				trueTypeFont = (value as Font);
			}
		}
	}

	public string text
	{
		get
		{
			return mText;
		}
		set
		{
			SetTextOnly(value);
			UILocalize component = this.GetComponent<UILocalize>();
			if (component != null)
			{
				component.set_enabled(false);
			}
		}
	}

	public int defaultFontSize => (trueTypeFont != null) ? mFontSize : ((!(mFont != null)) ? 16 : mFont.defaultSize);

	public int fontSize
	{
		get
		{
			return mFontSize;
		}
		set
		{
			value = Mathf.Clamp(value, 0, 256);
			if (mFontSize != value)
			{
				mFontSize = value;
				shouldBeProcessed = true;
				ProcessAndRequest();
			}
		}
	}

	public FontStyle fontStyle
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mFontStyle;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			if (mFontStyle != value)
			{
				mFontStyle = value;
				shouldBeProcessed = true;
				ProcessAndRequest();
			}
		}
	}

	public NGUIText.Alignment alignment
	{
		get
		{
			return mAlignment;
		}
		set
		{
			if (mAlignment != value)
			{
				mAlignment = value;
				shouldBeProcessed = true;
				ProcessAndRequest();
			}
		}
	}

	public bool applyGradient
	{
		get
		{
			return mApplyGradient;
		}
		set
		{
			if (mApplyGradient != value)
			{
				mApplyGradient = value;
				MarkAsChanged();
			}
		}
	}

	public Color gradientTop
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mGradientTop;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			if (mGradientTop != value)
			{
				mGradientTop = value;
				if (mApplyGradient)
				{
					MarkAsChanged();
				}
			}
		}
	}

	public Color gradientBottom
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mGradientBottom;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			if (mGradientBottom != value)
			{
				mGradientBottom = value;
				if (mApplyGradient)
				{
					MarkAsChanged();
				}
			}
		}
	}

	public int spacingX
	{
		get
		{
			return mSpacingX;
		}
		set
		{
			if (mSpacingX != value)
			{
				mSpacingX = value;
				MarkAsChanged();
			}
		}
	}

	public int spacingY
	{
		get
		{
			return mSpacingY;
		}
		set
		{
			if (mSpacingY != value)
			{
				mSpacingY = value;
				MarkAsChanged();
			}
		}
	}

	public bool useFloatSpacing
	{
		get
		{
			return mUseFloatSpacing;
		}
		set
		{
			if (mUseFloatSpacing != value)
			{
				mUseFloatSpacing = value;
				shouldBeProcessed = true;
			}
		}
	}

	public float floatSpacingX
	{
		get
		{
			return mFloatSpacingX;
		}
		set
		{
			if (!Mathf.Approximately(mFloatSpacingX, value))
			{
				mFloatSpacingX = value;
				MarkAsChanged();
			}
		}
	}

	public float floatSpacingY
	{
		get
		{
			return mFloatSpacingY;
		}
		set
		{
			if (!Mathf.Approximately(mFloatSpacingY, value))
			{
				mFloatSpacingY = value;
				MarkAsChanged();
			}
		}
	}

	public float effectiveSpacingY => (!mUseFloatSpacing) ? ((float)mSpacingY) : mFloatSpacingY;

	public float effectiveSpacingX => (!mUseFloatSpacing) ? ((float)mSpacingX) : mFloatSpacingX;

	private bool keepCrisp
	{
		get
		{
			if (trueTypeFont != null && keepCrispWhenShrunk != 0)
			{
				return keepCrispWhenShrunk == Crispness.Always;
			}
			return false;
		}
	}

	public bool supportEncoding
	{
		get
		{
			return mEncoding;
		}
		set
		{
			if (mEncoding != value)
			{
				mEncoding = value;
				shouldBeProcessed = true;
			}
		}
	}

	public NGUIText.SymbolStyle symbolStyle
	{
		get
		{
			return mSymbols;
		}
		set
		{
			if (mSymbols != value)
			{
				mSymbols = value;
				shouldBeProcessed = true;
			}
		}
	}

	public Overflow overflowMethod
	{
		get
		{
			return mOverflow;
		}
		set
		{
			if (mOverflow != value)
			{
				mOverflow = value;
				shouldBeProcessed = true;
			}
		}
	}

	[Obsolete("Use 'width' instead")]
	public int lineWidth
	{
		get
		{
			return base.width;
		}
		set
		{
			base.width = value;
		}
	}

	[Obsolete("Use 'height' instead")]
	public int lineHeight
	{
		get
		{
			return base.height;
		}
		set
		{
			base.height = value;
		}
	}

	public bool multiLine
	{
		get
		{
			return mMaxLineCount != 1;
		}
		set
		{
			if (mMaxLineCount != 1 != value)
			{
				mMaxLineCount = ((!value) ? 1 : 0);
				shouldBeProcessed = true;
			}
		}
	}

	public override Vector3[] localCorners
	{
		get
		{
			if (shouldBeProcessed)
			{
				ProcessText();
			}
			return base.localCorners;
		}
	}

	public override Vector3[] worldCorners
	{
		get
		{
			if (shouldBeProcessed)
			{
				ProcessText();
			}
			return base.worldCorners;
		}
	}

	public override Vector4 drawingDimensions
	{
		get
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			if (shouldBeProcessed)
			{
				ProcessText();
			}
			return base.drawingDimensions;
		}
	}

	public int maxLineCount
	{
		get
		{
			return mMaxLineCount;
		}
		set
		{
			if (mMaxLineCount != value)
			{
				mMaxLineCount = Mathf.Max(value, 0);
				shouldBeProcessed = true;
				if (overflowMethod == Overflow.ShrinkContent)
				{
					MakePixelPerfect();
				}
			}
		}
	}

	public Effect effectStyle
	{
		get
		{
			return mEffectStyle;
		}
		set
		{
			if (mEffectStyle != value)
			{
				mEffectStyle = value;
				shouldBeProcessed = true;
			}
		}
	}

	public Color effectColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mEffectColor;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			if (mEffectColor != value)
			{
				mEffectColor = value;
				if (mEffectStyle != 0)
				{
					shouldBeProcessed = true;
				}
			}
		}
	}

	public Vector2 effectDistance
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return mEffectDistance;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			if (mEffectDistance != value)
			{
				mEffectDistance = value;
				shouldBeProcessed = true;
			}
		}
	}

	[Obsolete("Use 'overflowMethod == UILabel.Overflow.ShrinkContent' instead")]
	public bool shrinkToFit
	{
		get
		{
			return mOverflow == Overflow.ShrinkContent;
		}
		set
		{
			if (value)
			{
				overflowMethod = Overflow.ShrinkContent;
			}
		}
	}

	public string processedText
	{
		get
		{
			if (mLastWidth != mWidth || mLastHeight != mHeight)
			{
				mLastWidth = mWidth;
				mLastHeight = mHeight;
				mShouldBeProcessed = true;
			}
			if (shouldBeProcessed)
			{
				ProcessText();
			}
			return mProcessedText;
		}
	}

	public Vector2 printedSize
	{
		get
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			if (shouldBeProcessed)
			{
				ProcessText();
			}
			return mCalculatedSize;
		}
	}

	public override Vector2 localSize
	{
		get
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			if (shouldBeProcessed)
			{
				ProcessText();
			}
			return base.localSize;
		}
	}

	private bool isValid => mFont != null || mTrueTypeFont != null;

	public void SetTextOnly(string value)
	{
		if (!(mText == value))
		{
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(mText))
				{
					mText = string.Empty;
					MarkAsChanged();
					ProcessAndRequest();
				}
			}
			else if (mText != value)
			{
				mText = value;
				MarkAsChanged();
				ProcessAndRequest();
			}
			if (autoResizeBoxCollider)
			{
				ResizeCollider();
			}
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (Application.get_isPlaying())
		{
			fontStyle = 0;
			supportEncoding = false;
		}
	}

	protected override void OnInit()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		base.OnInit();
		mList.Add(this);
		SetActiveFont(trueTypeFont);
		if (Application.get_isPlaying())
		{
			if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
			{
				GameSection componentInParent = this.get_transform().GetComponentInParent<GameSection>();
				if (componentInParent != null && componentInParent.sectionData != (GameSceneTables.SectionData)null)
				{
					string text = componentInParent.sectionData.GetText(this.get_name());
					if (text.Length > 0)
					{
						this.text = text;
					}
				}
			}
			string text2 = this.text;
			if (UIManager.ProcessingStringForUILabel(ref text2))
			{
				this.text = text2;
			}
		}
	}

	protected override void OnDisable()
	{
		SetActiveFont(null);
		mList.Remove(this);
		base.OnDisable();
	}

	protected void SetActiveFont(Font fnt)
	{
		if (mActiveTTF != fnt)
		{
			if (mActiveTTF != null && mFontUsage.TryGetValue(mActiveTTF, out int value))
			{
				value = Mathf.Max(0, --value);
				if (value == 0)
				{
					mFontUsage.Remove(mActiveTTF);
				}
				else
				{
					mFontUsage[mActiveTTF] = value;
				}
			}
			mActiveTTF = fnt;
			if (mActiveTTF != null)
			{
				int num = 0;
				num = (mFontUsage[mActiveTTF] = num + 1);
			}
		}
	}

	private static void OnFontChanged(Font font)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < mList.size; i++)
		{
			UILabel uILabel = mList[i];
			if (uILabel != null)
			{
				Font trueTypeFont = uILabel.trueTypeFont;
				if (trueTypeFont == font)
				{
					trueTypeFont.RequestCharactersInTexture(uILabel.mText, uILabel.mPrintedSize, uILabel.mFontStyle);
				}
			}
		}
		for (int j = 0; j < mList.size; j++)
		{
			UILabel uILabel2 = mList[j];
			if (uILabel2 != null)
			{
				Font trueTypeFont2 = uILabel2.trueTypeFont;
				if (trueTypeFont2 == font)
				{
					uILabel2.RemoveFromPanel();
					uILabel2.CreatePanel();
				}
			}
		}
	}

	public override Vector3[] GetSides(Transform relativeTo)
	{
		if (shouldBeProcessed)
		{
			ProcessText();
		}
		return base.GetSides(relativeTo);
	}

	protected override void UpgradeFrom265()
	{
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		ProcessText(true, true);
		if (mShrinkToFit)
		{
			overflowMethod = Overflow.ShrinkContent;
			mMaxLineCount = 0;
		}
		if (mMaxLineWidth != 0)
		{
			base.width = mMaxLineWidth;
			overflowMethod = ((mMaxLineCount > 0) ? Overflow.ResizeHeight : Overflow.ShrinkContent);
		}
		else
		{
			overflowMethod = Overflow.ResizeFreely;
		}
		if (mMaxLineHeight != 0)
		{
			base.height = mMaxLineHeight;
		}
		if (mFont != null)
		{
			int defaultSize = mFont.defaultSize;
			if (base.height < defaultSize)
			{
				base.height = defaultSize;
			}
			fontSize = defaultSize;
		}
		mMaxLineWidth = 0;
		mMaxLineHeight = 0;
		mShrinkToFit = false;
		NGUITools.UpdateWidgetCollider(this.get_gameObject(), true);
	}

	protected override void OnAnchor()
	{
		if (mOverflow == Overflow.ResizeFreely)
		{
			if (base.isFullyAnchored)
			{
				mOverflow = Overflow.ShrinkContent;
			}
		}
		else if (mOverflow == Overflow.ResizeHeight && topAnchor.target != null && bottomAnchor.target != null)
		{
			mOverflow = Overflow.ShrinkContent;
		}
		base.OnAnchor();
	}

	private void ProcessAndRequest()
	{
		if (ambigiousFont != null)
		{
			ProcessText();
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (!mTexRebuildAdded)
		{
			mTexRebuildAdded = true;
			Font.add_textureRebuilt((Action<Font>)OnFontChanged);
		}
	}

	protected override void OnStart()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		base.OnStart();
		if (mLineWidth > 0f)
		{
			mMaxLineWidth = Mathf.RoundToInt(mLineWidth);
			mLineWidth = 0f;
		}
		if (!mMultiline)
		{
			mMaxLineCount = 1;
			mMultiline = true;
		}
		mPremultiply = (material != null && material.get_shader() != null && material.get_shader().get_name().Contains("Premultiplied"));
		ProcessAndRequest();
	}

	public override void MarkAsChanged()
	{
		shouldBeProcessed = true;
		base.MarkAsChanged();
	}

	public void ProcessText()
	{
		ProcessText(false, true);
	}

	private void ProcessText(bool legacyMode, bool full)
	{
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		if (isValid)
		{
			mChanged = true;
			shouldBeProcessed = false;
			float num = mDrawRegion.z - mDrawRegion.x;
			float num2 = mDrawRegion.w - mDrawRegion.y;
			NGUIText.rectWidth = ((!legacyMode) ? base.width : ((mMaxLineWidth == 0) ? 1000000 : mMaxLineWidth));
			NGUIText.rectHeight = ((!legacyMode) ? base.height : ((mMaxLineHeight == 0) ? 1000000 : mMaxLineHeight));
			NGUIText.regionWidth = ((num == 1f) ? NGUIText.rectWidth : Mathf.RoundToInt((float)NGUIText.rectWidth * num));
			NGUIText.regionHeight = ((num2 == 1f) ? NGUIText.rectHeight : Mathf.RoundToInt((float)NGUIText.rectHeight * num2));
			int num3;
			if (legacyMode)
			{
				Vector3 localScale = base.cachedTransform.get_localScale();
				num3 = Mathf.RoundToInt(localScale.x);
			}
			else
			{
				num3 = defaultFontSize;
			}
			mPrintedSize = Mathf.Abs(num3);
			mScale = 1f;
			if (NGUIText.regionWidth < 1 || NGUIText.regionHeight < 0)
			{
				mProcessedText = string.Empty;
			}
			else
			{
				bool flag = trueTypeFont != null;
				if (flag && this.keepCrisp)
				{
					UIRoot root = base.root;
					if (root != null)
					{
						mDensity = ((!(root != null)) ? 1f : root.pixelSizeAdjustment);
					}
				}
				else
				{
					mDensity = 1f;
				}
				if (full)
				{
					UpdateNGUIText();
				}
				if (mOverflow == Overflow.ResizeFreely)
				{
					NGUIText.rectWidth = 1000000;
					NGUIText.regionWidth = 1000000;
				}
				if (mOverflow == Overflow.ResizeFreely || mOverflow == Overflow.ResizeHeight)
				{
					NGUIText.rectHeight = 1000000;
					NGUIText.regionHeight = 1000000;
				}
				if (mPrintedSize > 0)
				{
					bool keepCrisp = this.keepCrisp;
					int num4;
					for (num4 = mPrintedSize; num4 > 0; num4--)
					{
						if (keepCrisp)
						{
							mPrintedSize = num4;
							NGUIText.fontSize = mPrintedSize;
						}
						else
						{
							mScale = (float)num4 / (float)mPrintedSize;
							NGUIText.fontScale = ((!flag) ? ((float)mFontSize / (float)mFont.defaultSize * mScale) : mScale);
						}
						NGUIText.Update(false);
						bool flag2 = NGUIText.WrapText(mText, out mProcessedText, true, false);
						if (mOverflow != 0 || flag2)
						{
							if (mOverflow == Overflow.ResizeFreely)
							{
								mCalculatedSize = NGUIText.CalculatePrintedSize(mProcessedText);
								mWidth = Mathf.Max(minWidth, Mathf.RoundToInt(mCalculatedSize.x));
								if (num != 1f)
								{
									mWidth = Mathf.RoundToInt((float)mWidth / num);
								}
								mHeight = Mathf.Max(minHeight, Mathf.RoundToInt(mCalculatedSize.y));
								if (num2 != 1f)
								{
									mHeight = Mathf.RoundToInt((float)mHeight / num2);
								}
								if ((mWidth & 1) == 1)
								{
									mWidth++;
								}
								if ((mHeight & 1) == 1)
								{
									mHeight++;
								}
							}
							else if (mOverflow == Overflow.ResizeHeight)
							{
								mCalculatedSize = NGUIText.CalculatePrintedSize(mProcessedText);
								mHeight = Mathf.Max(minHeight, Mathf.RoundToInt(mCalculatedSize.y));
								if (num2 != 1f)
								{
									mHeight = Mathf.RoundToInt((float)mHeight / num2);
								}
								if ((mHeight & 1) == 1)
								{
									mHeight++;
								}
							}
							else
							{
								mCalculatedSize = NGUIText.CalculatePrintedSize(mProcessedText);
							}
							if (legacyMode)
							{
								base.width = Mathf.RoundToInt(mCalculatedSize.x);
								base.height = Mathf.RoundToInt(mCalculatedSize.y);
								base.cachedTransform.set_localScale(Vector3.get_one());
							}
							break;
						}
						if (--num4 <= 1)
						{
							break;
						}
					}
				}
				else
				{
					base.cachedTransform.set_localScale(Vector3.get_one());
					mProcessedText = string.Empty;
					mScale = 1f;
				}
				if (full)
				{
					NGUIText.bitmapFont = null;
					NGUIText.dynamicFont = null;
				}
			}
		}
	}

	public override void MakePixelPerfect()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		if (ambigiousFont != null)
		{
			Vector3 localPosition = base.cachedTransform.get_localPosition();
			localPosition.x = (float)Mathf.RoundToInt(localPosition.x);
			localPosition.y = (float)Mathf.RoundToInt(localPosition.y);
			localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
			base.cachedTransform.set_localPosition(localPosition);
			base.cachedTransform.set_localScale(Vector3.get_one());
			if (mOverflow == Overflow.ResizeFreely)
			{
				AssumeNaturalSize();
			}
			else
			{
				int width = base.width;
				int height = base.height;
				Overflow overflow = mOverflow;
				if (overflow != Overflow.ResizeHeight)
				{
					mWidth = 100000;
				}
				mHeight = 100000;
				mOverflow = Overflow.ShrinkContent;
				ProcessText(false, true);
				mOverflow = overflow;
				int num = Mathf.RoundToInt(mCalculatedSize.x);
				int num2 = Mathf.RoundToInt(mCalculatedSize.y);
				num = Mathf.Max(num, base.minWidth);
				num2 = Mathf.Max(num2, base.minHeight);
				if ((num & 1) == 1)
				{
					num++;
				}
				if ((num2 & 1) == 1)
				{
					num2++;
				}
				mWidth = Mathf.Max(width, num);
				mHeight = Mathf.Max(height, num2);
				MarkAsChanged();
			}
		}
		else
		{
			base.MakePixelPerfect();
		}
	}

	public void AssumeNaturalSize()
	{
		if (ambigiousFont != null)
		{
			mWidth = 100000;
			mHeight = 100000;
			ProcessText(false, true);
			mWidth = Mathf.RoundToInt(mCalculatedSize.x);
			mHeight = Mathf.RoundToInt(mCalculatedSize.y);
			if ((mWidth & 1) == 1)
			{
				mWidth++;
			}
			if ((mHeight & 1) == 1)
			{
				mHeight++;
			}
			MarkAsChanged();
		}
	}

	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector3 worldPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return GetCharacterIndexAtPosition(worldPos, false);
	}

	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector2 localPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return GetCharacterIndexAtPosition(localPos, false);
	}

	public int GetCharacterIndexAtPosition(Vector3 worldPos, bool precise)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		Vector2 localPos = Vector2.op_Implicit(base.cachedTransform.InverseTransformPoint(worldPos));
		return GetCharacterIndexAtPosition(localPos, precise);
	}

	public int GetCharacterIndexAtPosition(Vector2 localPos, bool precise)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		if (isValid)
		{
			string processedText = this.processedText;
			if (string.IsNullOrEmpty(processedText))
			{
				return 0;
			}
			UpdateNGUIText();
			if (precise)
			{
				NGUIText.PrintExactCharacterPositions(processedText, mTempVerts, mTempIndices);
			}
			else
			{
				NGUIText.PrintApproximateCharacterPositions(processedText, mTempVerts, mTempIndices);
			}
			if (mTempVerts.size > 0)
			{
				ApplyOffset(mTempVerts, 0);
				int result = (!precise) ? NGUIText.GetApproximateCharacterIndex(mTempVerts, mTempIndices, localPos) : NGUIText.GetExactCharacterIndex(mTempVerts, mTempIndices, localPos);
				mTempVerts.Clear();
				mTempIndices.Clear();
				NGUIText.bitmapFont = null;
				NGUIText.dynamicFont = null;
				return result;
			}
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
		}
		return 0;
	}

	public string GetWordAtPosition(Vector3 worldPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		int characterIndexAtPosition = GetCharacterIndexAtPosition(worldPos, true);
		return GetWordAtCharacterIndex(characterIndexAtPosition);
	}

	public string GetWordAtPosition(Vector2 localPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		int characterIndexAtPosition = GetCharacterIndexAtPosition(localPos, true);
		return GetWordAtCharacterIndex(characterIndexAtPosition);
	}

	public string GetWordAtCharacterIndex(int characterIndex)
	{
		if (characterIndex != -1 && characterIndex < mText.Length)
		{
			int num = mText.LastIndexOfAny(new char[2]
			{
				' ',
				'\n'
			}, characterIndex) + 1;
			int num2 = mText.IndexOfAny(new char[4]
			{
				' ',
				'\n',
				',',
				'.'
			}, characterIndex);
			if (num2 == -1)
			{
				num2 = mText.Length;
			}
			if (num != num2)
			{
				int num3 = num2 - num;
				if (num3 > 0)
				{
					string text = mText.Substring(num, num3);
					return NGUIText.StripSymbols(text);
				}
			}
		}
		return null;
	}

	public string GetUrlAtPosition(Vector3 worldPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return GetUrlAtCharacterIndex(GetCharacterIndexAtPosition(worldPos, true));
	}

	public string GetUrlAtPosition(Vector2 localPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return GetUrlAtCharacterIndex(GetCharacterIndexAtPosition(localPos, true));
	}

	public string GetUrlAtCharacterIndex(int characterIndex)
	{
		if (characterIndex != -1 && characterIndex < mText.Length - 6)
		{
			int num = (mText[characterIndex] != '[' || mText[characterIndex + 1] != 'u' || mText[characterIndex + 2] != 'r' || mText[characterIndex + 3] != 'l' || mText[characterIndex + 4] != '=') ? mText.LastIndexOf("[url=", characterIndex) : characterIndex;
			if (num == -1)
			{
				return null;
			}
			num += 5;
			int num2 = mText.IndexOf("]", num);
			if (num2 == -1)
			{
				return null;
			}
			int num3 = mText.IndexOf("[/url]", num2);
			if (num3 == -1 || characterIndex <= num3)
			{
				return mText.Substring(num, num2 - num);
			}
		}
		return null;
	}

	public int GetCharacterIndex(int currentIndex, KeyCode key)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Invalid comparison between Unknown and I4
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Invalid comparison between Unknown and I4
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Invalid comparison between Unknown and I4
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Invalid comparison between Unknown and I4
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Invalid comparison between Unknown and I4
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Invalid comparison between Unknown and I4
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Invalid comparison between Unknown and I4
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Invalid comparison between Unknown and I4
		if (isValid)
		{
			string processedText = this.processedText;
			if (string.IsNullOrEmpty(processedText))
			{
				return 0;
			}
			int defaultFontSize = this.defaultFontSize;
			UpdateNGUIText();
			NGUIText.PrintApproximateCharacterPositions(processedText, mTempVerts, mTempIndices);
			if (mTempVerts.size > 0)
			{
				ApplyOffset(mTempVerts, 0);
				for (int i = 0; i < mTempIndices.size; i++)
				{
					if (mTempIndices[i] == currentIndex)
					{
						Vector2 pos = Vector2.op_Implicit(mTempVerts[i]);
						if ((int)key == 273)
						{
							pos.y += (float)defaultFontSize + effectiveSpacingY;
						}
						else if ((int)key == 274)
						{
							pos.y -= (float)defaultFontSize + effectiveSpacingY;
						}
						else if ((int)key == 278)
						{
							pos.x -= 1000f;
						}
						else if ((int)key == 279)
						{
							pos.x += 1000f;
						}
						int approximateCharacterIndex = NGUIText.GetApproximateCharacterIndex(mTempVerts, mTempIndices, pos);
						if (approximateCharacterIndex == currentIndex)
						{
							break;
						}
						mTempVerts.Clear();
						mTempIndices.Clear();
						return approximateCharacterIndex;
					}
				}
				mTempVerts.Clear();
				mTempIndices.Clear();
			}
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
			if ((int)key == 273 || (int)key == 278)
			{
				return 0;
			}
			if ((int)key == 274 || (int)key == 279)
			{
				return processedText.Length;
			}
		}
		return currentIndex;
	}

	public void PrintOverlay(int start, int end, UIGeometry caret, UIGeometry highlight, Color caretColor, Color highlightColor)
	{
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		caret?.Clear();
		highlight?.Clear();
		if (isValid)
		{
			string processedText = this.processedText;
			UpdateNGUIText();
			int size = caret.verts.size;
			Vector2 item = default(Vector2);
			item._002Ector(0.5f, 0.5f);
			float finalAlpha = base.finalAlpha;
			if (highlight != null && start != end)
			{
				int size2 = highlight.verts.size;
				NGUIText.PrintCaretAndSelection(processedText, start, end, caret.verts, highlight.verts);
				if (highlight.verts.size > size2)
				{
					ApplyOffset(highlight.verts, size2);
					Color32 item2 = Color32.op_Implicit(new Color(highlightColor.r, highlightColor.g, highlightColor.b, highlightColor.a * finalAlpha));
					for (int i = size2; i < highlight.verts.size; i++)
					{
						highlight.uvs.Add(item);
						highlight.cols.Add(item2);
					}
				}
			}
			else
			{
				NGUIText.PrintCaretAndSelection(processedText, start, end, caret.verts, null);
			}
			ApplyOffset(caret.verts, size);
			Color32 item3 = Color32.op_Implicit(new Color(caretColor.r, caretColor.g, caretColor.b, caretColor.a * finalAlpha));
			for (int j = size; j < caret.verts.size; j++)
			{
				caret.uvs.Add(item);
				caret.cols.Add(item3);
			}
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
		}
	}

	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Invalid comparison between Unknown and I4
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		if (isValid)
		{
			int num = verts.size;
			Color val = base.color;
			val.a = finalAlpha;
			if (mFont != null && mFont.premultipliedAlphaShader)
			{
				val = NGUITools.ApplyPMA(val);
			}
			if ((int)QualitySettings.get_activeColorSpace() == 1)
			{
				val.r = Mathf.GammaToLinearSpace(val.r);
				val.g = Mathf.GammaToLinearSpace(val.g);
				val.b = Mathf.GammaToLinearSpace(val.b);
			}
			string processedText = this.processedText;
			int size = verts.size;
			UpdateNGUIText();
			NGUIText.tint = val;
			NGUIText.Print(processedText, verts, uvs, cols);
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
			Vector2 val2 = ApplyOffset(verts, size);
			if (!(mFont != null) || !mFont.packedFontShader)
			{
				if (effectStyle != 0)
				{
					int size2 = verts.size;
					val2.x = mEffectDistance.x;
					val2.y = mEffectDistance.y;
					ApplyShadow(verts, uvs, cols, num, size2, val2.x, 0f - val2.y);
					if (effectStyle == Effect.Outline || (effectStyle == Effect.Outline8 && !OutlineLimit))
					{
						num = size2;
						size2 = verts.size;
						ApplyShadow(verts, uvs, cols, num, size2, 0f - val2.x, val2.y);
						num = size2;
						size2 = verts.size;
						ApplyShadow(verts, uvs, cols, num, size2, val2.x, val2.y);
						num = size2;
						size2 = verts.size;
						ApplyShadow(verts, uvs, cols, num, size2, 0f - val2.x, 0f - val2.y);
						if (effectStyle == Effect.Outline8)
						{
							num = size2;
							size2 = verts.size;
							ApplyShadow(verts, uvs, cols, num, size2, 0f - val2.x, 0f);
							num = size2;
							size2 = verts.size;
							ApplyShadow(verts, uvs, cols, num, size2, val2.x, 0f);
							num = size2;
							size2 = verts.size;
							ApplyShadow(verts, uvs, cols, num, size2, 0f, val2.y);
							num = size2;
							size2 = verts.size;
							ApplyShadow(verts, uvs, cols, num, size2, 0f, 0f - val2.y);
						}
					}
				}
				if (onPostFill != null)
				{
					onPostFill(this, num, verts, uvs, cols);
				}
			}
		}
	}

	public Vector2 ApplyOffset(BetterList<Vector3> verts, int start)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		Vector2 pivotOffset = base.pivotOffset;
		float num = Mathf.Lerp(0f, (float)(-mWidth), pivotOffset.x);
		float num2 = Mathf.Lerp((float)mHeight, 0f, pivotOffset.y) + Mathf.Lerp(mCalculatedSize.y - (float)mHeight, 0f, pivotOffset.y);
		num = Mathf.Round(num);
		num2 = Mathf.Round(num2);
		for (int i = start; i < verts.size; i++)
		{
			verts.buffer[i].x += num;
			verts.buffer[i].y += num2;
		}
		return new Vector2(num, num2);
	}

	public void ApplyShadow(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols, int start, int end, float x, float y)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		Color val = mEffectColor;
		val.a *= finalAlpha;
		Color32 val2 = Color32.op_Implicit((!(bitmapFont != null) || !bitmapFont.premultipliedAlphaShader) ? val : NGUITools.ApplyPMA(val));
		for (int i = start; i < end; i++)
		{
			verts.Add(verts.buffer[i]);
			uvs.Add(uvs.buffer[i]);
			cols.Add(cols.buffer[i]);
			Vector3 val3 = verts.buffer[i];
			val3.x += x;
			val3.y += y;
			verts.buffer[i] = val3;
			Color32 val4 = cols.buffer[i];
			if (val4.a == 255)
			{
				cols.buffer[i] = val2;
			}
			else
			{
				Color val5 = val;
				val5.a = (float)(int)val4.a / 255f * val.a;
				cols.buffer[i] = Color32.op_Implicit((!(bitmapFont != null) || !bitmapFont.premultipliedAlphaShader) ? val5 : NGUITools.ApplyPMA(val5));
			}
		}
	}

	public int CalculateOffsetToFit(string text)
	{
		UpdateNGUIText();
		NGUIText.encoding = false;
		NGUIText.symbolStyle = NGUIText.SymbolStyle.None;
		int result = NGUIText.CalculateOffsetToFit(text);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		return result;
	}

	public void SetCurrentProgress()
	{
		if (UIProgressBar.current != null)
		{
			text = UIProgressBar.current.value.ToString("F");
		}
	}

	public void SetCurrentPercent()
	{
		if (UIProgressBar.current != null)
		{
			text = Mathf.RoundToInt(UIProgressBar.current.value * 100f) + "%";
		}
	}

	public void SetCurrentSelection()
	{
		if (UIPopupList.current != null)
		{
			text = ((!UIPopupList.current.isLocalized) ? UIPopupList.current.value : Localization.Get(UIPopupList.current.value));
		}
	}

	public bool Wrap(string text, out string final)
	{
		return Wrap(text, out final, 1000000);
	}

	public bool Wrap(string text, out string final, int height)
	{
		UpdateNGUIText();
		NGUIText.rectHeight = height;
		NGUIText.regionHeight = height;
		bool result = NGUIText.WrapText(text, out final, false);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		return result;
	}

	public void UpdateNGUIText()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		Font trueTypeFont = this.trueTypeFont;
		bool flag = trueTypeFont != null;
		NGUIText.fontSize = mPrintedSize;
		NGUIText.fontStyle = mFontStyle;
		NGUIText.rectWidth = mWidth;
		NGUIText.rectHeight = mHeight;
		NGUIText.regionWidth = Mathf.RoundToInt((float)mWidth * (mDrawRegion.z - mDrawRegion.x));
		NGUIText.regionHeight = Mathf.RoundToInt((float)mHeight * (mDrawRegion.w - mDrawRegion.y));
		NGUIText.gradient = (mApplyGradient && (mFont == null || !mFont.packedFontShader));
		NGUIText.gradientTop = mGradientTop;
		NGUIText.gradientBottom = mGradientBottom;
		NGUIText.encoding = mEncoding;
		NGUIText.premultiply = mPremultiply;
		NGUIText.symbolStyle = mSymbols;
		NGUIText.maxLines = mMaxLineCount;
		NGUIText.spacingX = effectiveSpacingX;
		NGUIText.spacingY = effectiveSpacingY;
		NGUIText.fontScale = ((!flag) ? ((float)mFontSize / (float)mFont.defaultSize * mScale) : mScale);
		if (mFont != null)
		{
			NGUIText.bitmapFont = mFont;
			while (true)
			{
				UIFont replacement = NGUIText.bitmapFont.replacement;
				if (replacement == null)
				{
					break;
				}
				NGUIText.bitmapFont = replacement;
			}
			if (NGUIText.bitmapFont.isDynamic)
			{
				NGUIText.dynamicFont = NGUIText.bitmapFont.dynamicFont;
				NGUIText.bitmapFont = null;
			}
			else
			{
				NGUIText.dynamicFont = null;
			}
		}
		else
		{
			NGUIText.dynamicFont = trueTypeFont;
			NGUIText.bitmapFont = null;
		}
		if (flag && keepCrisp)
		{
			UIRoot root = base.root;
			if (root != null)
			{
				NGUIText.pixelDensity = ((!(root != null)) ? 1f : root.pixelSizeAdjustment);
			}
		}
		else
		{
			NGUIText.pixelDensity = 1f;
		}
		if (mDensity != NGUIText.pixelDensity)
		{
			ProcessText(false, false);
			NGUIText.rectWidth = mWidth;
			NGUIText.rectHeight = mHeight;
			NGUIText.regionWidth = Mathf.RoundToInt((float)mWidth * (mDrawRegion.z - mDrawRegion.x));
			NGUIText.regionHeight = Mathf.RoundToInt((float)mHeight * (mDrawRegion.w - mDrawRegion.y));
		}
		if (alignment == NGUIText.Alignment.Automatic)
		{
			switch (base.pivot)
			{
			case Pivot.TopLeft:
			case Pivot.Left:
			case Pivot.BottomLeft:
				NGUIText.alignment = NGUIText.Alignment.Left;
				break;
			case Pivot.TopRight:
			case Pivot.Right:
			case Pivot.BottomRight:
				NGUIText.alignment = NGUIText.Alignment.Right;
				break;
			default:
				NGUIText.alignment = NGUIText.Alignment.Center;
				break;
			}
		}
		else
		{
			NGUIText.alignment = alignment;
		}
		NGUIText.Update();
	}

	private void OnApplicationPause(bool paused)
	{
		if (!paused && mTrueTypeFont != null)
		{
			Invalidate(false);
		}
	}
}
