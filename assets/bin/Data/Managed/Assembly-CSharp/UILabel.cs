// Decompiled with JetBrains decompiler
// Type: UILabel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Label")]
public class UILabel : UIWidget
{
  public static bool OutlineLimit = false;
  public UILabel.Crispness keepCrispWhenShrunk = UILabel.Crispness.OnDesktop;
  [HideInInspector]
  [SerializeField]
  private Font mTrueTypeFont;
  [HideInInspector]
  [SerializeField]
  private UIFont mFont;
  [Multiline(6)]
  [HideInInspector]
  [SerializeField]
  private string mText = "";
  [HideInInspector]
  [SerializeField]
  private int mFontSize = 16 /*0x10*/;
  [HideInInspector]
  [SerializeField]
  private FontStyle mFontStyle;
  [HideInInspector]
  [SerializeField]
  private NGUIText.Alignment mAlignment;
  [HideInInspector]
  [SerializeField]
  private bool mEncoding = true;
  [HideInInspector]
  [SerializeField]
  private int mMaxLineCount;
  [HideInInspector]
  [SerializeField]
  private UILabel.Effect mEffectStyle;
  [HideInInspector]
  [SerializeField]
  private Color mEffectColor = Color.black;
  [HideInInspector]
  [SerializeField]
  private NGUIText.SymbolStyle mSymbols = NGUIText.SymbolStyle.Normal;
  [HideInInspector]
  [SerializeField]
  private Vector2 mEffectDistance = Vector2.one;
  [HideInInspector]
  [SerializeField]
  private UILabel.Overflow mOverflow;
  [HideInInspector]
  [SerializeField]
  private Material mMaterial;
  [HideInInspector]
  [SerializeField]
  private bool mApplyGradient;
  [HideInInspector]
  [SerializeField]
  private Color mGradientTop = Color.white;
  [HideInInspector]
  [SerializeField]
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
  [HideInInspector]
  [SerializeField]
  private float mFloatSpacingX;
  [HideInInspector]
  [SerializeField]
  private float mFloatSpacingY;
  [HideInInspector]
  [SerializeField]
  private bool mShrinkToFit;
  [HideInInspector]
  [SerializeField]
  private int mMaxLineWidth;
  [HideInInspector]
  [SerializeField]
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
  private Vector2 mCalculatedSize = Vector2.zero;
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
    get => this.mShouldBeProcessed;
    set
    {
      if (value)
      {
        this.mChanged = true;
        this.mShouldBeProcessed = true;
      }
      else
        this.mShouldBeProcessed = false;
    }
  }

  public override bool isAnchoredHorizontally
  {
    get => base.isAnchoredHorizontally || this.mOverflow == UILabel.Overflow.ResizeFreely;
  }

  public override bool isAnchoredVertically
  {
    get
    {
      return base.isAnchoredVertically || this.mOverflow == UILabel.Overflow.ResizeFreely || this.mOverflow == UILabel.Overflow.ResizeHeight;
    }
  }

  public override Material material
  {
    get
    {
      if (Object.op_Inequality((Object) this.mMaterial, (Object) null))
        return this.mMaterial;
      if (Object.op_Inequality((Object) this.mFont, (Object) null))
        return this.mFont.material;
      return Object.op_Inequality((Object) this.mTrueTypeFont, (Object) null) ? this.mTrueTypeFont.material : (Material) null;
    }
    set
    {
      if (!Object.op_Inequality((Object) this.mMaterial, (Object) value))
        return;
      this.RemoveFromPanel();
      this.mMaterial = value;
      this.MarkAsChanged();
    }
  }

  [Obsolete("Use UILabel.bitmapFont instead")]
  public UIFont font
  {
    get => this.bitmapFont;
    set => this.bitmapFont = value;
  }

  public UIFont bitmapFont
  {
    get => this.mFont;
    set
    {
      if (!Object.op_Inequality((Object) this.mFont, (Object) value))
        return;
      this.RemoveFromPanel();
      this.mFont = value;
      this.mTrueTypeFont = (Font) null;
      this.MarkAsChanged();
    }
  }

  public Font trueTypeFont
  {
    get
    {
      if (Object.op_Inequality((Object) this.mTrueTypeFont, (Object) null))
        return this.mTrueTypeFont;
      return !Object.op_Inequality((Object) this.mFont, (Object) null) ? (Font) null : this.mFont.dynamicFont;
    }
    set
    {
      if (!Object.op_Inequality((Object) this.mTrueTypeFont, (Object) value))
        return;
      this.SetActiveFont((Font) null);
      this.RemoveFromPanel();
      this.mTrueTypeFont = value;
      this.shouldBeProcessed = true;
      this.mFont = (UIFont) null;
      this.SetActiveFont(value);
      this.ProcessAndRequest();
      if (!Object.op_Inequality((Object) this.mActiveTTF, (Object) null))
        return;
      base.MarkAsChanged();
    }
  }

  public Object ambigiousFont
  {
    get => (Object) this.mFont ?? (Object) this.mTrueTypeFont;
    set
    {
      UIFont uiFont = value as UIFont;
      if (Object.op_Inequality((Object) uiFont, (Object) null))
        this.bitmapFont = uiFont;
      else
        this.trueTypeFont = value as Font;
    }
  }

  public void SetTextOnly(string value)
  {
    if (this.mText == value)
      return;
    if (string.IsNullOrEmpty(value))
    {
      if (!string.IsNullOrEmpty(this.mText))
      {
        this.mText = "";
        this.MarkAsChanged();
        this.ProcessAndRequest();
      }
    }
    else if (this.mText != value)
    {
      this.mText = value;
      this.MarkAsChanged();
      this.ProcessAndRequest();
    }
    if (!this.autoResizeBoxCollider)
      return;
    this.ResizeCollider();
  }

  public string text
  {
    get => this.mText;
    set
    {
      this.SetTextOnly(value);
      UILocalize component = ((Component) this).GetComponent<UILocalize>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Behaviour) component).enabled = false;
    }
  }

  public int defaultFontSize
  {
    get
    {
      if (Object.op_Inequality((Object) this.trueTypeFont, (Object) null))
        return this.mFontSize;
      return !Object.op_Inequality((Object) this.mFont, (Object) null) ? 16 /*0x10*/ : this.mFont.defaultSize;
    }
  }

  public int fontSize
  {
    get => this.mFontSize;
    set
    {
      value = Mathf.Clamp(value, 0, 256 /*0x0100*/);
      if (this.mFontSize == value)
        return;
      this.mFontSize = value;
      this.shouldBeProcessed = true;
      this.ProcessAndRequest();
    }
  }

  public FontStyle fontStyle
  {
    get => this.mFontStyle;
    set
    {
      if (this.mFontStyle == value)
        return;
      this.mFontStyle = value;
      this.shouldBeProcessed = true;
      this.ProcessAndRequest();
    }
  }

  public NGUIText.Alignment alignment
  {
    get => this.mAlignment;
    set
    {
      if (this.mAlignment == value)
        return;
      this.mAlignment = value;
      this.shouldBeProcessed = true;
      this.ProcessAndRequest();
    }
  }

  public bool applyGradient
  {
    get => this.mApplyGradient;
    set
    {
      if (this.mApplyGradient == value)
        return;
      this.mApplyGradient = value;
      this.MarkAsChanged();
    }
  }

  public Color gradientTop
  {
    get => this.mGradientTop;
    set
    {
      if (!Color.op_Inequality(this.mGradientTop, value))
        return;
      this.mGradientTop = value;
      if (!this.mApplyGradient)
        return;
      this.MarkAsChanged();
    }
  }

  public Color gradientBottom
  {
    get => this.mGradientBottom;
    set
    {
      if (!Color.op_Inequality(this.mGradientBottom, value))
        return;
      this.mGradientBottom = value;
      if (!this.mApplyGradient)
        return;
      this.MarkAsChanged();
    }
  }

  public int spacingX
  {
    get => this.mSpacingX;
    set
    {
      if (this.mSpacingX == value)
        return;
      this.mSpacingX = value;
      this.MarkAsChanged();
    }
  }

  public int spacingY
  {
    get => this.mSpacingY;
    set
    {
      if (this.mSpacingY == value)
        return;
      this.mSpacingY = value;
      this.MarkAsChanged();
    }
  }

  public bool useFloatSpacing
  {
    get => this.mUseFloatSpacing;
    set
    {
      if (this.mUseFloatSpacing == value)
        return;
      this.mUseFloatSpacing = value;
      this.shouldBeProcessed = true;
    }
  }

  public float floatSpacingX
  {
    get => this.mFloatSpacingX;
    set
    {
      if (Mathf.Approximately(this.mFloatSpacingX, value))
        return;
      this.mFloatSpacingX = value;
      this.MarkAsChanged();
    }
  }

  public float floatSpacingY
  {
    get => this.mFloatSpacingY;
    set
    {
      if (Mathf.Approximately(this.mFloatSpacingY, value))
        return;
      this.mFloatSpacingY = value;
      this.MarkAsChanged();
    }
  }

  public float effectiveSpacingY
  {
    get => !this.mUseFloatSpacing ? (float) this.mSpacingY : this.mFloatSpacingY;
  }

  public float effectiveSpacingX
  {
    get => !this.mUseFloatSpacing ? (float) this.mSpacingX : this.mFloatSpacingX;
  }

  private bool keepCrisp
  {
    get
    {
      return Object.op_Inequality((Object) this.trueTypeFont, (Object) null) && this.keepCrispWhenShrunk != UILabel.Crispness.Never && this.keepCrispWhenShrunk == UILabel.Crispness.Always;
    }
  }

  public bool supportEncoding
  {
    get => this.mEncoding;
    set
    {
      if (this.mEncoding == value)
        return;
      this.mEncoding = value;
      this.shouldBeProcessed = true;
    }
  }

  public NGUIText.SymbolStyle symbolStyle
  {
    get => this.mSymbols;
    set
    {
      if (this.mSymbols == value)
        return;
      this.mSymbols = value;
      this.shouldBeProcessed = true;
    }
  }

  public UILabel.Overflow overflowMethod
  {
    get => this.mOverflow;
    set
    {
      if (this.mOverflow == value)
        return;
      this.mOverflow = value;
      this.shouldBeProcessed = true;
    }
  }

  [Obsolete("Use 'width' instead")]
  public int lineWidth
  {
    get => this.width;
    set => this.width = value;
  }

  [Obsolete("Use 'height' instead")]
  public int lineHeight
  {
    get => this.height;
    set => this.height = value;
  }

  public bool multiLine
  {
    get => this.mMaxLineCount != 1;
    set
    {
      if (this.mMaxLineCount != 1 == value)
        return;
      this.mMaxLineCount = value ? 0 : 1;
      this.shouldBeProcessed = true;
    }
  }

  public override Vector3[] localCorners
  {
    get
    {
      if (this.shouldBeProcessed)
        this.ProcessText();
      return base.localCorners;
    }
  }

  public override Vector3[] worldCorners
  {
    get
    {
      if (this.shouldBeProcessed)
        this.ProcessText();
      return base.worldCorners;
    }
  }

  public override Vector4 drawingDimensions
  {
    get
    {
      if (this.shouldBeProcessed)
        this.ProcessText();
      return base.drawingDimensions;
    }
  }

  public int maxLineCount
  {
    get => this.mMaxLineCount;
    set
    {
      if (this.mMaxLineCount == value)
        return;
      this.mMaxLineCount = Mathf.Max(value, 0);
      this.shouldBeProcessed = true;
      if (this.overflowMethod != UILabel.Overflow.ShrinkContent)
        return;
      this.MakePixelPerfect();
    }
  }

  public UILabel.Effect effectStyle
  {
    get => this.mEffectStyle;
    set
    {
      if (this.mEffectStyle == value)
        return;
      this.mEffectStyle = value;
      this.shouldBeProcessed = true;
    }
  }

  public Color effectColor
  {
    get => this.mEffectColor;
    set
    {
      if (!Color.op_Inequality(this.mEffectColor, value))
        return;
      this.mEffectColor = value;
      if (this.mEffectStyle == UILabel.Effect.None)
        return;
      this.shouldBeProcessed = true;
    }
  }

  public Vector2 effectDistance
  {
    get => this.mEffectDistance;
    set
    {
      if (!Vector2.op_Inequality(this.mEffectDistance, value))
        return;
      this.mEffectDistance = value;
      this.shouldBeProcessed = true;
    }
  }

  [Obsolete("Use 'overflowMethod == UILabel.Overflow.ShrinkContent' instead")]
  public bool shrinkToFit
  {
    get => this.mOverflow == UILabel.Overflow.ShrinkContent;
    set
    {
      if (!value)
        return;
      this.overflowMethod = UILabel.Overflow.ShrinkContent;
    }
  }

  public string processedText
  {
    get
    {
      if (this.mLastWidth != this.mWidth || this.mLastHeight != this.mHeight)
      {
        this.mLastWidth = this.mWidth;
        this.mLastHeight = this.mHeight;
        this.mShouldBeProcessed = true;
      }
      if (this.shouldBeProcessed)
        this.ProcessText();
      return this.mProcessedText;
    }
  }

  public Vector2 printedSize
  {
    get
    {
      if (this.shouldBeProcessed)
        this.ProcessText();
      return this.mCalculatedSize;
    }
  }

  public override Vector2 localSize
  {
    get
    {
      if (this.shouldBeProcessed)
        this.ProcessText();
      return base.localSize;
    }
  }

  private bool isValid
  {
    get
    {
      return Object.op_Inequality((Object) this.mFont, (Object) null) || Object.op_Inequality((Object) this.mTrueTypeFont, (Object) null);
    }
  }

  protected override void Awake()
  {
    base.Awake();
    if (!Application.isPlaying)
      return;
    this.fontStyle = (FontStyle) 0;
    this.supportEncoding = false;
  }

  protected override void OnInit()
  {
    base.OnInit();
    UILabel.mList.Add(this);
    this.SetActiveFont(this.trueTypeFont);
    if (!Application.isPlaying)
      return;
    if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
    {
      GameSection componentInParent = ((Component) ((Component) this).transform).GetComponentInParent<GameSection>();
      if (Object.op_Inequality((Object) componentInParent, (Object) null) && componentInParent.sectionData != (GameSceneTables.SectionData) null)
      {
        string text = componentInParent.sectionData.GetText(((Object) this).name);
        if (text.Length > 0)
          this.text = text;
      }
    }
    string text1 = this.text;
    if (!UIManager.ProcessingStringForUILabel(ref text1))
      return;
    this.text = text1;
  }

  protected override void OnDisable()
  {
    this.SetActiveFont((Font) null);
    UILabel.mList.Remove(this);
    base.OnDisable();
  }

  protected void SetActiveFont(Font fnt)
  {
    if (!Object.op_Inequality((Object) this.mActiveTTF, (Object) fnt))
      return;
    int num1;
    if (Object.op_Inequality((Object) this.mActiveTTF, (Object) null) && UILabel.mFontUsage.TryGetValue(this.mActiveTTF, out num1))
    {
      int num2;
      int num3 = Mathf.Max(0, num2 = num1 - 1);
      if (num3 == 0)
        UILabel.mFontUsage.Remove(this.mActiveTTF);
      else
        UILabel.mFontUsage[this.mActiveTTF] = num3;
    }
    this.mActiveTTF = fnt;
    if (!Object.op_Inequality((Object) this.mActiveTTF, (Object) null))
      return;
    int num4 = 0;
    int num5;
    UILabel.mFontUsage[this.mActiveTTF] = num5 = num4 + 1;
  }

  private static void OnFontChanged(Font font)
  {
    for (int i = 0; i < UILabel.mList.size; ++i)
    {
      UILabel m = UILabel.mList[i];
      if (Object.op_Inequality((Object) m, (Object) null))
      {
        Font trueTypeFont = m.trueTypeFont;
        if (Object.op_Equality((Object) trueTypeFont, (Object) font))
          trueTypeFont.RequestCharactersInTexture(m.mText, m.mPrintedSize, m.mFontStyle);
      }
    }
    for (int i = 0; i < UILabel.mList.size; ++i)
    {
      UILabel m = UILabel.mList[i];
      if (Object.op_Inequality((Object) m, (Object) null) && Object.op_Equality((Object) m.trueTypeFont, (Object) font))
      {
        m.RemoveFromPanel();
        m.CreatePanel();
      }
    }
  }

  public override Vector3[] GetSides(Transform relativeTo)
  {
    if (this.shouldBeProcessed)
      this.ProcessText();
    return base.GetSides(relativeTo);
  }

  protected override void UpgradeFrom265()
  {
    this.ProcessText(true, true);
    if (this.mShrinkToFit)
    {
      this.overflowMethod = UILabel.Overflow.ShrinkContent;
      this.mMaxLineCount = 0;
    }
    if (this.mMaxLineWidth != 0)
    {
      this.width = this.mMaxLineWidth;
      this.overflowMethod = this.mMaxLineCount > 0 ? UILabel.Overflow.ResizeHeight : UILabel.Overflow.ShrinkContent;
    }
    else
      this.overflowMethod = UILabel.Overflow.ResizeFreely;
    if (this.mMaxLineHeight != 0)
      this.height = this.mMaxLineHeight;
    if (Object.op_Inequality((Object) this.mFont, (Object) null))
    {
      int defaultSize = this.mFont.defaultSize;
      if (this.height < defaultSize)
        this.height = defaultSize;
      this.fontSize = defaultSize;
    }
    this.mMaxLineWidth = 0;
    this.mMaxLineHeight = 0;
    this.mShrinkToFit = false;
    NGUITools.UpdateWidgetCollider(((Component) this).gameObject, true);
  }

  protected override void OnAnchor()
  {
    if (this.mOverflow == UILabel.Overflow.ResizeFreely)
    {
      if (this.isFullyAnchored)
        this.mOverflow = UILabel.Overflow.ShrinkContent;
    }
    else if (this.mOverflow == UILabel.Overflow.ResizeHeight && Object.op_Inequality((Object) this.topAnchor.target, (Object) null) && Object.op_Inequality((Object) this.bottomAnchor.target, (Object) null))
      this.mOverflow = UILabel.Overflow.ShrinkContent;
    base.OnAnchor();
  }

  private void ProcessAndRequest()
  {
    if (!Object.op_Inequality(this.ambigiousFont, (Object) null))
      return;
    this.ProcessText();
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    if (UILabel.mTexRebuildAdded)
      return;
    UILabel.mTexRebuildAdded = true;
    Font.textureRebuilt += new Action<Font>(UILabel.OnFontChanged);
  }

  protected override void OnStart()
  {
    base.OnStart();
    if ((double) this.mLineWidth > 0.0)
    {
      this.mMaxLineWidth = Mathf.RoundToInt(this.mLineWidth);
      this.mLineWidth = 0.0f;
    }
    if (!this.mMultiline)
    {
      this.mMaxLineCount = 1;
      this.mMultiline = true;
    }
    this.mPremultiply = Object.op_Inequality((Object) this.material, (Object) null) && Object.op_Inequality((Object) this.material.shader, (Object) null) && ((Object) this.material.shader).name.Contains("Premultiplied");
    this.ProcessAndRequest();
  }

  public override void MarkAsChanged()
  {
    this.shouldBeProcessed = true;
    base.MarkAsChanged();
  }

  public void ProcessText() => this.ProcessText(false, true);

  private void ProcessText(bool legacyMode, bool full)
  {
    if (!this.isValid)
      return;
    this.mChanged = true;
    this.shouldBeProcessed = false;
    float num1 = this.mDrawRegion.z - this.mDrawRegion.x;
    float num2 = this.mDrawRegion.w - this.mDrawRegion.y;
    NGUIText.rectWidth = legacyMode ? (this.mMaxLineWidth != 0 ? this.mMaxLineWidth : 1000000) : this.width;
    NGUIText.rectHeight = legacyMode ? (this.mMaxLineHeight != 0 ? this.mMaxLineHeight : 1000000) : this.height;
    NGUIText.regionWidth = (double) num1 != 1.0 ? Mathf.RoundToInt((float) NGUIText.rectWidth * num1) : NGUIText.rectWidth;
    NGUIText.regionHeight = (double) num2 != 1.0 ? Mathf.RoundToInt((float) NGUIText.rectHeight * num2) : NGUIText.rectHeight;
    this.mPrintedSize = Mathf.Abs(legacyMode ? Mathf.RoundToInt(this.cachedTransform.localScale.x) : this.defaultFontSize);
    this.mScale = 1f;
    if (NGUIText.regionWidth < 1 || NGUIText.regionHeight < 0)
    {
      this.mProcessedText = "";
    }
    else
    {
      bool flag1 = Object.op_Inequality((Object) this.trueTypeFont, (Object) null);
      if (flag1 && this.keepCrisp)
      {
        UIRoot root = this.root;
        if (Object.op_Inequality((Object) root, (Object) null))
          this.mDensity = Object.op_Inequality((Object) root, (Object) null) ? root.pixelSizeAdjustment : 1f;
      }
      else
        this.mDensity = 1f;
      if (full)
        this.UpdateNGUIText();
      if (this.mOverflow == UILabel.Overflow.ResizeFreely)
      {
        NGUIText.rectWidth = 1000000;
        NGUIText.regionWidth = 1000000;
      }
      if (this.mOverflow == UILabel.Overflow.ResizeFreely || this.mOverflow == UILabel.Overflow.ResizeHeight)
      {
        NGUIText.rectHeight = 1000000;
        NGUIText.regionHeight = 1000000;
      }
      if (this.mPrintedSize > 0)
      {
        bool keepCrisp = this.keepCrisp;
        int num3;
        for (int index = this.mPrintedSize; index > 0; index = num3 - 1)
        {
          if (keepCrisp)
          {
            this.mPrintedSize = index;
            NGUIText.fontSize = this.mPrintedSize;
          }
          else
          {
            this.mScale = (float) index / (float) this.mPrintedSize;
            NGUIText.fontScale = flag1 ? this.mScale : (float) this.mFontSize / (float) this.mFont.defaultSize * this.mScale;
          }
          NGUIText.Update(false);
          bool flag2 = NGUIText.WrapText(this.mText, out this.mProcessedText, true, false);
          if (this.mOverflow == UILabel.Overflow.ShrinkContent && !flag2)
          {
            if ((num3 = index - 1) <= 1)
              break;
          }
          else
          {
            if (this.mOverflow == UILabel.Overflow.ResizeFreely)
            {
              this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
              this.mWidth = Mathf.Max(this.minWidth, Mathf.RoundToInt(this.mCalculatedSize.x));
              if ((double) num1 != 1.0)
                this.mWidth = Mathf.RoundToInt((float) this.mWidth / num1);
              this.mHeight = Mathf.Max(this.minHeight, Mathf.RoundToInt(this.mCalculatedSize.y));
              if ((double) num2 != 1.0)
                this.mHeight = Mathf.RoundToInt((float) this.mHeight / num2);
              if ((this.mWidth & 1) == 1)
                ++this.mWidth;
              if ((this.mHeight & 1) == 1)
                ++this.mHeight;
            }
            else if (this.mOverflow == UILabel.Overflow.ResizeHeight)
            {
              this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
              this.mHeight = Mathf.Max(this.minHeight, Mathf.RoundToInt(this.mCalculatedSize.y));
              if ((double) num2 != 1.0)
                this.mHeight = Mathf.RoundToInt((float) this.mHeight / num2);
              if ((this.mHeight & 1) == 1)
                ++this.mHeight;
            }
            else
              this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
            if (legacyMode)
            {
              this.width = Mathf.RoundToInt(this.mCalculatedSize.x);
              this.height = Mathf.RoundToInt(this.mCalculatedSize.y);
              this.cachedTransform.localScale = Vector3.one;
              break;
            }
            break;
          }
        }
      }
      else
      {
        this.cachedTransform.localScale = Vector3.one;
        this.mProcessedText = "";
        this.mScale = 1f;
      }
      if (!full)
        return;
      NGUIText.bitmapFont = (UIFont) null;
      NGUIText.dynamicFont = (Font) null;
    }
  }

  public override void MakePixelPerfect()
  {
    if (Object.op_Inequality(this.ambigiousFont, (Object) null))
    {
      Vector3 localPosition = this.cachedTransform.localPosition;
      localPosition.x = (float) Mathf.RoundToInt(localPosition.x);
      localPosition.y = (float) Mathf.RoundToInt(localPosition.y);
      localPosition.z = (float) Mathf.RoundToInt(localPosition.z);
      this.cachedTransform.localPosition = localPosition;
      this.cachedTransform.localScale = Vector3.one;
      if (this.mOverflow == UILabel.Overflow.ResizeFreely)
      {
        this.AssumeNaturalSize();
      }
      else
      {
        int width = this.width;
        int height = this.height;
        UILabel.Overflow mOverflow = this.mOverflow;
        if (mOverflow != UILabel.Overflow.ResizeHeight)
          this.mWidth = 100000;
        this.mHeight = 100000;
        this.mOverflow = UILabel.Overflow.ShrinkContent;
        this.ProcessText(false, true);
        this.mOverflow = mOverflow;
        int num1 = Mathf.RoundToInt(this.mCalculatedSize.x);
        int num2 = Mathf.RoundToInt(this.mCalculatedSize.y);
        int num3 = Mathf.Max(num1, this.minWidth);
        int num4 = Mathf.Max(num2, this.minHeight);
        if ((num3 & 1) == 1)
          ++num3;
        if ((num4 & 1) == 1)
          ++num4;
        this.mWidth = Mathf.Max(width, num3);
        this.mHeight = Mathf.Max(height, num4);
        this.MarkAsChanged();
      }
    }
    else
      base.MakePixelPerfect();
  }

  public void AssumeNaturalSize()
  {
    if (!Object.op_Inequality(this.ambigiousFont, (Object) null))
      return;
    this.mWidth = 100000;
    this.mHeight = 100000;
    this.ProcessText(false, true);
    this.mWidth = Mathf.RoundToInt(this.mCalculatedSize.x);
    this.mHeight = Mathf.RoundToInt(this.mCalculatedSize.y);
    if ((this.mWidth & 1) == 1)
      ++this.mWidth;
    if ((this.mHeight & 1) == 1)
      ++this.mHeight;
    this.MarkAsChanged();
  }

  [Obsolete("Use UILabel.GetCharacterAtPosition instead")]
  public int GetCharacterIndex(Vector3 worldPos)
  {
    return this.GetCharacterIndexAtPosition(worldPos, false);
  }

  [Obsolete("Use UILabel.GetCharacterAtPosition instead")]
  public int GetCharacterIndex(Vector2 localPos)
  {
    return this.GetCharacterIndexAtPosition(localPos, false);
  }

  public int GetCharacterIndexAtPosition(Vector3 worldPos, bool precise)
  {
    return this.GetCharacterIndexAtPosition(Vector2.op_Implicit(this.cachedTransform.InverseTransformPoint(worldPos)), precise);
  }

  public int GetCharacterIndexAtPosition(Vector2 localPos, bool precise)
  {
    if (this.isValid)
    {
      string processedText = this.processedText;
      if (string.IsNullOrEmpty(processedText))
        return 0;
      this.UpdateNGUIText();
      if (precise)
        NGUIText.PrintExactCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
      else
        NGUIText.PrintApproximateCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
      if (UILabel.mTempVerts.size > 0)
      {
        this.ApplyOffset(UILabel.mTempVerts, 0);
        int characterIndexAtPosition = precise ? NGUIText.GetExactCharacterIndex(UILabel.mTempVerts, UILabel.mTempIndices, localPos) : NGUIText.GetApproximateCharacterIndex(UILabel.mTempVerts, UILabel.mTempIndices, localPos);
        UILabel.mTempVerts.Clear();
        UILabel.mTempIndices.Clear();
        NGUIText.bitmapFont = (UIFont) null;
        NGUIText.dynamicFont = (Font) null;
        return characterIndexAtPosition;
      }
      NGUIText.bitmapFont = (UIFont) null;
      NGUIText.dynamicFont = (Font) null;
    }
    return 0;
  }

  public string GetWordAtPosition(Vector3 worldPos)
  {
    return this.GetWordAtCharacterIndex(this.GetCharacterIndexAtPosition(worldPos, true));
  }

  public string GetWordAtPosition(Vector2 localPos)
  {
    return this.GetWordAtCharacterIndex(this.GetCharacterIndexAtPosition(localPos, true));
  }

  public string GetWordAtCharacterIndex(int characterIndex)
  {
    if (characterIndex != -1 && characterIndex < this.mText.Length)
    {
      int startIndex = this.mText.LastIndexOfAny(new char[2]
      {
        ' ',
        '\n'
      }, characterIndex) + 1;
      int num = this.mText.IndexOfAny(new char[4]
      {
        ' ',
        '\n',
        ',',
        '.'
      }, characterIndex);
      if (num == -1)
        num = this.mText.Length;
      if (startIndex != num)
      {
        int length = num - startIndex;
        if (length > 0)
          return NGUIText.StripSymbols(this.mText.Substring(startIndex, length));
      }
    }
    return (string) null;
  }

  public string GetUrlAtPosition(Vector3 worldPos)
  {
    return this.GetUrlAtCharacterIndex(this.GetCharacterIndexAtPosition(worldPos, true));
  }

  public string GetUrlAtPosition(Vector2 localPos)
  {
    return this.GetUrlAtCharacterIndex(this.GetCharacterIndexAtPosition(localPos, true));
  }

  public string GetUrlAtCharacterIndex(int characterIndex)
  {
    if (characterIndex != -1 && characterIndex < this.mText.Length - 6)
    {
      int num1 = this.mText[characterIndex] != '[' || this.mText[characterIndex + 1] != 'u' || this.mText[characterIndex + 2] != 'r' || this.mText[characterIndex + 3] != 'l' || this.mText[characterIndex + 4] != '=' ? this.mText.LastIndexOf("[url=", characterIndex) : characterIndex;
      if (num1 == -1)
        return (string) null;
      int startIndex1 = num1 + 5;
      int startIndex2 = this.mText.IndexOf("]", startIndex1);
      if (startIndex2 == -1)
        return (string) null;
      int num2 = this.mText.IndexOf("[/url]", startIndex2);
      if (num2 == -1 || characterIndex <= num2)
        return this.mText.Substring(startIndex1, startIndex2 - startIndex1);
    }
    return (string) null;
  }

  public int GetCharacterIndex(int currentIndex, KeyCode key)
  {
    if (this.isValid)
    {
      string processedText = this.processedText;
      if (string.IsNullOrEmpty(processedText))
        return 0;
      int defaultFontSize = this.defaultFontSize;
      this.UpdateNGUIText();
      NGUIText.PrintApproximateCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
      if (UILabel.mTempVerts.size > 0)
      {
        this.ApplyOffset(UILabel.mTempVerts, 0);
        for (int i = 0; i < UILabel.mTempIndices.size; ++i)
        {
          if (UILabel.mTempIndices[i] == currentIndex)
          {
            Vector2 pos = Vector2.op_Implicit(UILabel.mTempVerts[i]);
            if (key == 273)
              pos.y += (float) defaultFontSize + this.effectiveSpacingY;
            else if (key == 274)
              pos.y -= (float) defaultFontSize + this.effectiveSpacingY;
            else if (key == 278)
              pos.x -= 1000f;
            else if (key == 279)
              pos.x += 1000f;
            int approximateCharacterIndex = NGUIText.GetApproximateCharacterIndex(UILabel.mTempVerts, UILabel.mTempIndices, pos);
            if (approximateCharacterIndex != currentIndex)
            {
              UILabel.mTempVerts.Clear();
              UILabel.mTempIndices.Clear();
              return approximateCharacterIndex;
            }
            break;
          }
        }
        UILabel.mTempVerts.Clear();
        UILabel.mTempIndices.Clear();
      }
      NGUIText.bitmapFont = (UIFont) null;
      NGUIText.dynamicFont = (Font) null;
      if (key == 273 || key == 278)
        return 0;
      if (key == 274 || key == 279)
        return processedText.Length;
    }
    return currentIndex;
  }

  public void PrintOverlay(
    int start,
    int end,
    UIGeometry caret,
    UIGeometry highlight,
    Color caretColor,
    Color highlightColor)
  {
    caret?.Clear();
    highlight?.Clear();
    if (!this.isValid)
      return;
    string processedText = this.processedText;
    this.UpdateNGUIText();
    int size1 = caret.verts.size;
    Vector2 vector2;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2).\u002Ector(0.5f, 0.5f);
    float finalAlpha = this.finalAlpha;
    if (highlight != null && start != end)
    {
      int size2 = highlight.verts.size;
      NGUIText.PrintCaretAndSelection(processedText, start, end, caret.verts, highlight.verts);
      if (highlight.verts.size > size2)
      {
        this.ApplyOffset(highlight.verts, size2);
        Color32 color32 = Color32.op_Implicit(new Color(highlightColor.r, highlightColor.g, highlightColor.b, highlightColor.a * finalAlpha));
        for (int index = size2; index < highlight.verts.size; ++index)
        {
          highlight.uvs.Add(vector2);
          highlight.cols.Add(color32);
        }
      }
    }
    else
      NGUIText.PrintCaretAndSelection(processedText, start, end, caret.verts, (BetterList<Vector3>) null);
    this.ApplyOffset(caret.verts, size1);
    Color32 color32_1 = Color32.op_Implicit(new Color(caretColor.r, caretColor.g, caretColor.b, caretColor.a * finalAlpha));
    for (int index = size1; index < caret.verts.size; ++index)
    {
      caret.uvs.Add(vector2);
      caret.cols.Add(color32_1);
    }
    NGUIText.bitmapFont = (UIFont) null;
    NGUIText.dynamicFont = (Font) null;
  }

  public override void OnFill(
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols)
  {
    if (!this.isValid)
      return;
    int num = verts.size;
    Color c = this.color;
    c.a = this.finalAlpha;
    if (Object.op_Inequality((Object) this.mFont, (Object) null) && this.mFont.premultipliedAlphaShader)
      c = NGUITools.ApplyPMA(c);
    if (QualitySettings.activeColorSpace == 1)
    {
      c.r = Mathf.GammaToLinearSpace(c.r);
      c.g = Mathf.GammaToLinearSpace(c.g);
      c.b = Mathf.GammaToLinearSpace(c.b);
    }
    string processedText = this.processedText;
    int size1 = verts.size;
    this.UpdateNGUIText();
    NGUIText.tint = c;
    BetterList<Vector3> verts1 = verts;
    BetterList<Vector2> uvs1 = uvs;
    BetterList<Color32> cols1 = cols;
    NGUIText.Print(processedText, verts1, uvs1, cols1);
    NGUIText.bitmapFont = (UIFont) null;
    NGUIText.dynamicFont = (Font) null;
    Vector2 vector2 = this.ApplyOffset(verts, size1);
    if (Object.op_Inequality((Object) this.mFont, (Object) null) && this.mFont.packedFontShader)
      return;
    if (this.effectStyle != UILabel.Effect.None)
    {
      int size2 = verts.size;
      vector2.x = this.mEffectDistance.x;
      vector2.y = this.mEffectDistance.y;
      this.ApplyShadow(verts, uvs, cols, num, size2, vector2.x, -vector2.y);
      if (this.effectStyle == UILabel.Effect.Outline || this.effectStyle == UILabel.Effect.Outline8 && !UILabel.OutlineLimit)
      {
        int start1 = size2;
        int size3 = verts.size;
        this.ApplyShadow(verts, uvs, cols, start1, size3, -vector2.x, vector2.y);
        int start2 = size3;
        int size4 = verts.size;
        this.ApplyShadow(verts, uvs, cols, start2, size4, vector2.x, vector2.y);
        num = size4;
        int size5 = verts.size;
        this.ApplyShadow(verts, uvs, cols, num, size5, -vector2.x, -vector2.y);
        if (this.effectStyle == UILabel.Effect.Outline8)
        {
          int start3 = size5;
          int size6 = verts.size;
          this.ApplyShadow(verts, uvs, cols, start3, size6, -vector2.x, 0.0f);
          int start4 = size6;
          int size7 = verts.size;
          this.ApplyShadow(verts, uvs, cols, start4, size7, vector2.x, 0.0f);
          int start5 = size7;
          int size8 = verts.size;
          this.ApplyShadow(verts, uvs, cols, start5, size8, 0.0f, vector2.y);
          num = size8;
          int size9 = verts.size;
          this.ApplyShadow(verts, uvs, cols, num, size9, 0.0f, -vector2.y);
        }
      }
    }
    if (this.onPostFill == null)
      return;
    this.onPostFill((UIWidget) this, num, verts, uvs, cols);
  }

  public Vector2 ApplyOffset(BetterList<Vector3> verts, int start)
  {
    Vector2 pivotOffset = this.pivotOffset;
    float num1 = Mathf.Lerp(0.0f, (float) -this.mWidth, pivotOffset.x);
    float num2 = Mathf.Lerp((float) this.mHeight, 0.0f, pivotOffset.y) + Mathf.Lerp(this.mCalculatedSize.y - (float) this.mHeight, 0.0f, pivotOffset.y);
    float num3 = Mathf.Round(num1);
    float num4 = Mathf.Round(num2);
    for (int index = start; index < verts.size; ++index)
    {
      verts.buffer[index].x += num3;
      verts.buffer[index].y += num4;
    }
    return new Vector2(num3, num4);
  }

  public void ApplyShadow(
    BetterList<Vector3> verts,
    BetterList<Vector2> uvs,
    BetterList<Color32> cols,
    int start,
    int end,
    float x,
    float y)
  {
    Color mEffectColor = this.mEffectColor;
    mEffectColor.a *= this.finalAlpha;
    Color32 color32_1 = Color32.op_Implicit(!Object.op_Inequality((Object) this.bitmapFont, (Object) null) || !this.bitmapFont.premultipliedAlphaShader ? mEffectColor : NGUITools.ApplyPMA(mEffectColor));
    for (int index = start; index < end; ++index)
    {
      verts.Add(verts.buffer[index]);
      uvs.Add(uvs.buffer[index]);
      cols.Add(cols.buffer[index]);
      Vector3 vector3 = verts.buffer[index];
      vector3.x += x;
      vector3.y += y;
      verts.buffer[index] = vector3;
      Color32 color32_2 = cols.buffer[index];
      if (color32_2.a == byte.MaxValue)
      {
        cols.buffer[index] = color32_1;
      }
      else
      {
        Color c = mEffectColor;
        c.a = (float) color32_2.a / (float) byte.MaxValue * mEffectColor.a;
        cols.buffer[index] = Color32.op_Implicit(!Object.op_Inequality((Object) this.bitmapFont, (Object) null) || !this.bitmapFont.premultipliedAlphaShader ? c : NGUITools.ApplyPMA(c));
      }
    }
  }

  public int CalculateOffsetToFit(string text)
  {
    this.UpdateNGUIText();
    NGUIText.encoding = false;
    NGUIText.symbolStyle = NGUIText.SymbolStyle.None;
    int offsetToFit = NGUIText.CalculateOffsetToFit(text);
    NGUIText.bitmapFont = (UIFont) null;
    NGUIText.dynamicFont = (Font) null;
    return offsetToFit;
  }

  public void SetCurrentProgress()
  {
    if (!Object.op_Inequality((Object) UIProgressBar.current, (Object) null))
      return;
    this.text = UIProgressBar.current.value.ToString("F");
  }

  public void SetCurrentPercent()
  {
    if (!Object.op_Inequality((Object) UIProgressBar.current, (Object) null))
      return;
    this.text = Mathf.RoundToInt(UIProgressBar.current.value * 100f).ToString() + "%";
  }

  public void SetCurrentSelection()
  {
    if (!Object.op_Inequality((Object) UIPopupList.current, (Object) null))
      return;
    this.text = UIPopupList.current.isLocalized ? Localization.Get(UIPopupList.current.value) : UIPopupList.current.value;
  }

  public bool Wrap(string text, out string final) => this.Wrap(text, out final, 1000000);

  public bool Wrap(string text, out string final, int height)
  {
    this.UpdateNGUIText();
    NGUIText.rectHeight = height;
    NGUIText.regionHeight = height;
    int num = NGUIText.WrapText(text, out final) ? 1 : 0;
    NGUIText.bitmapFont = (UIFont) null;
    NGUIText.dynamicFont = (Font) null;
    return num != 0;
  }

  public void UpdateNGUIText()
  {
    Font trueTypeFont = this.trueTypeFont;
    bool flag = Object.op_Inequality((Object) trueTypeFont, (Object) null);
    NGUIText.fontSize = this.mPrintedSize;
    NGUIText.fontStyle = this.mFontStyle;
    NGUIText.rectWidth = this.mWidth;
    NGUIText.rectHeight = this.mHeight;
    NGUIText.regionWidth = Mathf.RoundToInt((float) this.mWidth * (this.mDrawRegion.z - this.mDrawRegion.x));
    NGUIText.regionHeight = Mathf.RoundToInt((float) this.mHeight * (this.mDrawRegion.w - this.mDrawRegion.y));
    NGUIText.gradient = this.mApplyGradient && (Object.op_Equality((Object) this.mFont, (Object) null) || !this.mFont.packedFontShader);
    NGUIText.gradientTop = this.mGradientTop;
    NGUIText.gradientBottom = this.mGradientBottom;
    NGUIText.encoding = this.mEncoding;
    NGUIText.premultiply = this.mPremultiply;
    NGUIText.symbolStyle = this.mSymbols;
    NGUIText.maxLines = this.mMaxLineCount;
    NGUIText.spacingX = this.effectiveSpacingX;
    NGUIText.spacingY = this.effectiveSpacingY;
    NGUIText.fontScale = flag ? this.mScale : (float) this.mFontSize / (float) this.mFont.defaultSize * this.mScale;
    if (Object.op_Inequality((Object) this.mFont, (Object) null))
    {
      NGUIText.bitmapFont = this.mFont;
      while (true)
      {
        UIFont replacement = NGUIText.bitmapFont.replacement;
        if (!Object.op_Equality((Object) replacement, (Object) null))
          NGUIText.bitmapFont = replacement;
        else
          break;
      }
      if (NGUIText.bitmapFont.isDynamic)
      {
        NGUIText.dynamicFont = NGUIText.bitmapFont.dynamicFont;
        NGUIText.bitmapFont = (UIFont) null;
      }
      else
        NGUIText.dynamicFont = (Font) null;
    }
    else
    {
      NGUIText.dynamicFont = trueTypeFont;
      NGUIText.bitmapFont = (UIFont) null;
    }
    if (flag && this.keepCrisp)
    {
      UIRoot root = this.root;
      if (Object.op_Inequality((Object) root, (Object) null))
        NGUIText.pixelDensity = Object.op_Inequality((Object) root, (Object) null) ? root.pixelSizeAdjustment : 1f;
    }
    else
      NGUIText.pixelDensity = 1f;
    if ((double) this.mDensity != (double) NGUIText.pixelDensity)
    {
      this.ProcessText(false, false);
      NGUIText.rectWidth = this.mWidth;
      NGUIText.rectHeight = this.mHeight;
      NGUIText.regionWidth = Mathf.RoundToInt((float) this.mWidth * (this.mDrawRegion.z - this.mDrawRegion.x));
      NGUIText.regionHeight = Mathf.RoundToInt((float) this.mHeight * (this.mDrawRegion.w - this.mDrawRegion.y));
    }
    if (this.alignment == NGUIText.Alignment.Automatic)
    {
      switch (this.pivot)
      {
        case UIWidget.Pivot.TopLeft:
        case UIWidget.Pivot.Left:
        case UIWidget.Pivot.BottomLeft:
          NGUIText.alignment = NGUIText.Alignment.Left;
          break;
        case UIWidget.Pivot.TopRight:
        case UIWidget.Pivot.Right:
        case UIWidget.Pivot.BottomRight:
          NGUIText.alignment = NGUIText.Alignment.Right;
          break;
        default:
          NGUIText.alignment = NGUIText.Alignment.Center;
          break;
      }
    }
    else
      NGUIText.alignment = this.alignment;
    NGUIText.Update();
  }

  private void OnApplicationPause(bool paused)
  {
    if (paused || !Object.op_Inequality((Object) this.mTrueTypeFont, (Object) null))
      return;
    this.Invalidate(false);
  }

  public enum Effect
  {
    None,
    Shadow,
    Outline,
    Outline8,
  }

  public enum Overflow
  {
    ShrinkContent,
    ClampContent,
    ResizeFreely,
    ResizeHeight,
  }

  public enum Crispness
  {
    Never,
    OnDesktop,
    Always,
  }
}
