// Decompiled with JetBrains decompiler
// Type: UIInput
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/UI/Input Field")]
public class UIInput : MonoBehaviour
{
  public static UIInput current;
  public static UIInput selection;
  public UILabel label;
  public UIInput.InputType inputType;
  public UIInput.OnReturnKey onReturnKey;
  public UIInput.KeyboardType keyboardType;
  public bool hideInput;
  [NonSerialized]
  public bool selectAllTextOnFocus = true;
  public UIInput.Validation validation;
  public int characterLimit;
  public string savedAs;
  [HideInInspector]
  [SerializeField]
  private GameObject selectOnTab;
  public Color activeTextColor = Color.white;
  public Color caretColor = new Color(1f, 1f, 1f, 0.8f);
  public Color selectionColor = new Color(1f, 0.8745098f, 0.5529412f, 0.5f);
  public List<EventDelegate> onSubmit = new List<EventDelegate>();
  public List<EventDelegate> onChange = new List<EventDelegate>();
  public UIInput.OnValidate onValidate;
  [SerializeField]
  [HideInInspector]
  protected string mValue;
  [NonSerialized]
  protected string mDefaultText = "";
  [NonSerialized]
  protected Color mDefaultColor = Color.white;
  [NonSerialized]
  protected float mPosition;
  [NonSerialized]
  protected bool mDoInit = true;
  [NonSerialized]
  protected UIWidget.Pivot mPivot;
  [NonSerialized]
  protected bool mLoadSavedValue = true;
  protected static int mDrawStart = 0;
  protected static string mLastIME = "";
  protected static TouchScreenKeyboard mKeyboard;
  private static bool mWaitForKeyboard = false;
  [NonSerialized]
  protected int mSelectionStart;
  [NonSerialized]
  protected int mSelectionEnd;
  [NonSerialized]
  protected UITexture mHighlight;
  [NonSerialized]
  protected UITexture mCaret;
  [NonSerialized]
  protected Texture2D mBlankTex;
  [NonSerialized]
  protected float mNextBlink;
  [NonSerialized]
  protected float mLastAlpha;
  [NonSerialized]
  protected string mCached = "";
  [NonSerialized]
  protected int mSelectMe = -1;
  [NonSerialized]
  protected int mSelectTime = -1;
  [NonSerialized]
  private UICamera mCam;
  private static int mIgnoreKey = 0;

  public string defaultText
  {
    get
    {
      if (this.mDoInit)
        this.Init();
      return this.mDefaultText;
    }
    set
    {
      if (this.mDoInit)
        this.Init();
      this.mDefaultText = value;
      this.UpdateLabel();
    }
  }

  public bool inputShouldBeHidden
  {
    get
    {
      return this.hideInput && Object.op_Inequality((Object) this.label, (Object) null) && !this.label.multiLine && this.inputType != UIInput.InputType.Password;
    }
  }

  [Obsolete("Use UIInput.value instead")]
  public string text
  {
    get => this.value;
    set => this.value = value;
  }

  public string value
  {
    get
    {
      if (this.mDoInit)
        this.Init();
      return this.mValue;
    }
    set
    {
      if (this.mDoInit)
        this.Init();
      UIInput.mDrawStart = 0;
      if (Application.platform == 22)
        value = value.Replace("\\b", "\b");
      value = this.Validate(value);
      if (this.isSelected && UIInput.mKeyboard != null && this.mCached != value)
      {
        UIInput.mKeyboard.text = value;
        this.mCached = value;
      }
      if (!(this.mValue != value))
        return;
      this.mValue = value;
      this.mLoadSavedValue = false;
      if (this.isSelected)
      {
        if (string.IsNullOrEmpty(value))
        {
          this.mSelectionStart = 0;
          this.mSelectionEnd = 0;
        }
        else
        {
          this.mSelectionStart = value.Length;
          this.mSelectionEnd = this.mSelectionStart;
        }
      }
      else
        this.SaveToPlayerPrefs(value);
      this.UpdateLabel();
      this.ExecuteOnChange();
    }
  }

  [Obsolete("Use UIInput.isSelected instead")]
  public bool selected
  {
    get => this.isSelected;
    set => this.isSelected = value;
  }

  public bool isSelected
  {
    get => Object.op_Equality((Object) UIInput.selection, (Object) this);
    set
    {
      if (!value)
      {
        if (!this.isSelected)
          return;
        UICamera.selectedObject = (GameObject) null;
      }
      else
        UICamera.selectedObject = ((Component) this).gameObject;
    }
  }

  public int cursorPosition
  {
    get
    {
      return UIInput.mKeyboard != null && !this.inputShouldBeHidden || !this.isSelected ? this.value.Length : this.mSelectionEnd;
    }
    set
    {
      if (!this.isSelected || UIInput.mKeyboard != null && !this.inputShouldBeHidden)
        return;
      this.mSelectionEnd = value;
      this.UpdateLabel();
    }
  }

  public int selectionStart
  {
    get
    {
      if (UIInput.mKeyboard != null && !this.inputShouldBeHidden)
        return 0;
      return !this.isSelected ? this.value.Length : this.mSelectionStart;
    }
    set
    {
      if (!this.isSelected || UIInput.mKeyboard != null && !this.inputShouldBeHidden)
        return;
      this.mSelectionStart = value;
      this.UpdateLabel();
    }
  }

  public int selectionEnd
  {
    get
    {
      return UIInput.mKeyboard != null && !this.inputShouldBeHidden || !this.isSelected ? this.value.Length : this.mSelectionEnd;
    }
    set
    {
      if (!this.isSelected || UIInput.mKeyboard != null && !this.inputShouldBeHidden)
        return;
      this.mSelectionEnd = value;
      this.UpdateLabel();
    }
  }

  public UITexture caret => this.mCaret;

  public string Validate(string val)
  {
    val = Regex.Replace(val, "\\p{Cs}", "");
    if (string.IsNullOrEmpty(val))
      return "";
    StringBuilder stringBuilder = new StringBuilder(val.Length);
    for (int index = 0; index < val.Length; ++index)
    {
      char ch = val[index];
      if (this.onValidate != null)
        ch = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, ch);
      else if (this.validation != UIInput.Validation.None)
        ch = this.Validate(stringBuilder.ToString(), stringBuilder.Length, ch);
      if (ch != char.MinValue)
        stringBuilder.Append(ch);
    }
    return this.characterLimit > 0 && stringBuilder.Length > this.characterLimit ? stringBuilder.ToString(0, this.characterLimit) : stringBuilder.ToString();
  }

  private void Start()
  {
    if (Object.op_Inequality((Object) this.selectOnTab, (Object) null))
    {
      if (Object.op_Equality((Object) ((Component) this).GetComponent<UIKeyNavigation>(), (Object) null))
        ((Component) this).gameObject.AddComponent<UIKeyNavigation>().onDown = this.selectOnTab;
      this.selectOnTab = (GameObject) null;
      NGUITools.SetDirty((Object) this);
    }
    if (this.mLoadSavedValue && !string.IsNullOrEmpty(this.savedAs))
      this.LoadValue();
    else
      this.value = this.mValue.Replace("\\n", "\n");
  }

  protected void Init()
  {
    if (!this.mDoInit || !Object.op_Inequality((Object) this.label, (Object) null))
      return;
    this.mDoInit = false;
    this.mDefaultText = this.label.text;
    this.mDefaultColor = this.label.color;
    this.label.supportEncoding = false;
    if (this.label.alignment == NGUIText.Alignment.Justified)
    {
      this.label.alignment = NGUIText.Alignment.Left;
      Debug.LogWarning((object) "Input fields using labels with justified alignment are not supported at this time", (Object) this);
    }
    this.mPivot = this.label.pivot;
    this.mPosition = this.label.cachedTransform.localPosition.x;
    this.UpdateLabel();
  }

  protected void SaveToPlayerPrefs(string val)
  {
    if (string.IsNullOrEmpty(this.savedAs))
      return;
    if (string.IsNullOrEmpty(val))
      PlayerPrefs.DeleteKey(this.savedAs);
    else
      PlayerPrefs.SetString(this.savedAs, val);
  }

  protected virtual void OnSelect(bool isSelected)
  {
    if (isSelected)
      this.OnSelectEvent();
    else
      this.OnDeselectEvent();
  }

  protected void OnSelectEvent()
  {
    this.mSelectTime = Time.frameCount;
    UIInput.selection = this;
    if (this.mDoInit)
      this.Init();
    if (!Object.op_Inequality((Object) this.label, (Object) null) || !NGUITools.GetActive((Behaviour) this))
      return;
    this.mSelectMe = Time.frameCount;
  }

  protected void OnDeselectEvent()
  {
    if (this.mDoInit)
      this.Init();
    if (Object.op_Inequality((Object) this.label, (Object) null) && NGUITools.GetActive((Behaviour) this))
    {
      this.mValue = this.value;
      if (UIInput.mKeyboard != null)
      {
        UIInput.mWaitForKeyboard = false;
        UIInput.mKeyboard.active = false;
        UIInput.mKeyboard = (TouchScreenKeyboard) null;
      }
      if (string.IsNullOrEmpty(this.mValue))
      {
        this.label.text = this.mDefaultText;
        this.label.color = this.mDefaultColor;
      }
      else
        this.label.text = this.mValue;
      Input.imeCompositionMode = (IMECompositionMode) 0;
      this.RestoreLabelPivot();
    }
    UIInput.selection = (UIInput) null;
    this.UpdateLabel();
  }

  protected virtual void Update()
  {
    if (!this.isSelected || this.mSelectTime == Time.frameCount)
      return;
    if (this.mDoInit)
      this.Init();
    if (UIInput.mWaitForKeyboard)
    {
      if (UIInput.mKeyboard != null && !UIInput.mKeyboard.active)
        return;
      UIInput.mWaitForKeyboard = false;
    }
    if (this.mSelectMe != -1 && this.mSelectMe != Time.frameCount)
    {
      this.mSelectMe = -1;
      this.mSelectionEnd = string.IsNullOrEmpty(this.mValue) ? 0 : this.mValue.Length;
      UIInput.mDrawStart = 0;
      this.mSelectionStart = this.selectAllTextOnFocus ? 0 : this.mSelectionEnd;
      this.label.color = this.activeTextColor;
      RuntimePlatform platform = Application.platform;
      if (platform == 8 || platform == 11 || platform == 21 || platform == 22 || platform == 20 || platform == 19 || platform == 18)
      {
        TouchScreenKeyboardType screenKeyboardType;
        string str;
        if (this.inputShouldBeHidden)
        {
          TouchScreenKeyboard.hideInput = true;
          screenKeyboardType = (TouchScreenKeyboardType) this.keyboardType;
          str = "|";
        }
        else if (this.inputType == UIInput.InputType.Password)
        {
          TouchScreenKeyboard.hideInput = false;
          screenKeyboardType = (TouchScreenKeyboardType) 0;
          str = this.mValue;
          this.mSelectionStart = this.mSelectionEnd;
        }
        else
        {
          TouchScreenKeyboard.hideInput = false;
          screenKeyboardType = (TouchScreenKeyboardType) this.keyboardType;
          str = this.mValue;
          this.mSelectionStart = this.mSelectionEnd;
        }
        UIInput.mWaitForKeyboard = true;
        UIInput.mKeyboard = this.inputType == UIInput.InputType.Password ? TouchScreenKeyboard.Open(str, screenKeyboardType, false, false, true) : TouchScreenKeyboard.Open(str, screenKeyboardType, !this.inputShouldBeHidden && this.inputType == UIInput.InputType.AutoCorrect, this.label.multiLine && !this.hideInput, false, false, this.defaultText);
      }
      else
      {
        Vector2 vector2 = Vector2.op_Implicit(!Object.op_Inequality((Object) UICamera.current, (Object) null) || !Object.op_Inequality((Object) UICamera.current.cachedCamera, (Object) null) ? this.label.worldCorners[0] : UICamera.current.cachedCamera.WorldToScreenPoint(this.label.worldCorners[0]));
        vector2.y = (float) Screen.height - vector2.y;
        Input.imeCompositionMode = (IMECompositionMode) 1;
        Input.compositionCursorPos = vector2;
      }
      this.UpdateLabel();
      if (string.IsNullOrEmpty(Input.inputString))
        return;
    }
    if (UIInput.mKeyboard != null)
    {
      string str = UIInput.mKeyboard.done || !UIInput.mKeyboard.active ? this.mCached : UIInput.mKeyboard.text;
      if (this.inputShouldBeHidden)
      {
        if (str != "|")
        {
          if (!string.IsNullOrEmpty(str))
            this.Insert(str.Substring(1));
          else
            this.DoBackspace();
          UIInput.mKeyboard.text = "|";
        }
      }
      else if (this.mCached != str)
      {
        this.mCached = str;
        if (!UIInput.mKeyboard.done && UIInput.mKeyboard.active)
          this.value = str;
      }
      if (UIInput.mKeyboard.done || !UIInput.mKeyboard.active)
      {
        if (!UIInput.mKeyboard.wasCanceled)
          this.Submit();
        UIInput.mKeyboard = (TouchScreenKeyboard) null;
        this.isSelected = false;
        this.mCached = "";
      }
    }
    else
    {
      string compositionString = Input.compositionString;
      if (string.IsNullOrEmpty(compositionString) && !string.IsNullOrEmpty(Input.inputString))
      {
        foreach (char ch in Input.inputString)
        {
          if (ch >= ' ' && ch != '\uF700' && ch != '\uF701' && ch != '\uF702' && ch != '\uF703')
            this.Insert(ch.ToString());
        }
      }
      if (UIInput.mLastIME != compositionString)
      {
        this.mSelectionEnd = string.IsNullOrEmpty(compositionString) ? this.mSelectionStart : this.mValue.Length + compositionString.Length;
        UIInput.mLastIME = compositionString;
        this.UpdateLabel();
        this.ExecuteOnChange();
      }
    }
    if (Object.op_Inequality((Object) this.mCaret, (Object) null) && (double) this.mNextBlink < (double) RealTime.time)
    {
      this.mNextBlink = RealTime.time + 0.5f;
      ((Behaviour) this.mCaret).enabled = !((Behaviour) this.mCaret).enabled;
    }
    if (this.isSelected && (double) this.mLastAlpha != (double) this.label.finalAlpha)
      this.UpdateLabel();
    if (Object.op_Equality((Object) this.mCam, (Object) null))
      this.mCam = UICamera.FindCameraForLayer(((Component) this).gameObject.layer);
    if (!Object.op_Inequality((Object) this.mCam, (Object) null))
      return;
    if (UICamera.GetKeyDown(this.mCam.submitKey0))
    {
      if ((this.onReturnKey == UIInput.OnReturnKey.NewLine ? 1 : (this.onReturnKey != UIInput.OnReturnKey.Default || !this.label.multiLine || Input.GetKey((KeyCode) 306) || Input.GetKey((KeyCode) 305) || this.label.overflowMethod == UILabel.Overflow.ClampContent ? 0 : (this.validation == UIInput.Validation.None ? 1 : 0))) != 0)
      {
        this.Insert("\n");
      }
      else
      {
        if (Object.op_Inequality((Object) UICamera.controller.current, (Object) null))
          UICamera.controller.clickNotification = UICamera.ClickNotification.None;
        UICamera.currentKey = this.mCam.submitKey0;
        this.Submit();
      }
    }
    if (UICamera.GetKeyDown(this.mCam.submitKey1))
    {
      if ((this.onReturnKey == UIInput.OnReturnKey.NewLine ? 1 : (this.onReturnKey != UIInput.OnReturnKey.Default || !this.label.multiLine || Input.GetKey((KeyCode) 306) || Input.GetKey((KeyCode) 305) || this.label.overflowMethod == UILabel.Overflow.ClampContent ? 0 : (this.validation == UIInput.Validation.None ? 1 : 0))) != 0)
      {
        this.Insert("\n");
      }
      else
      {
        if (Object.op_Inequality((Object) UICamera.controller.current, (Object) null))
          UICamera.controller.clickNotification = UICamera.ClickNotification.None;
        UICamera.currentKey = this.mCam.submitKey1;
        this.Submit();
      }
    }
    if (this.mCam.useKeyboard || !UICamera.GetKeyUp((KeyCode) 9))
      return;
    this.OnKey((KeyCode) 9);
  }

  private void OnKey(KeyCode key)
  {
    int frameCount = Time.frameCount;
    if (UIInput.mIgnoreKey == frameCount)
      return;
    if (key == this.mCam.cancelKey0 || key == this.mCam.cancelKey1)
    {
      UIInput.mIgnoreKey = frameCount;
      this.isSelected = false;
    }
    else
    {
      if (key != 9)
        return;
      UIInput.mIgnoreKey = frameCount;
      this.isSelected = false;
      UIKeyNavigation component = ((Component) this).GetComponent<UIKeyNavigation>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.OnKey((KeyCode) 9);
    }
  }

  protected void DoBackspace()
  {
    if (string.IsNullOrEmpty(this.mValue))
      return;
    if (this.mSelectionStart == this.mSelectionEnd)
    {
      if (this.mSelectionStart < 1)
        return;
      --this.mSelectionEnd;
    }
    this.Insert("");
  }

  protected virtual void Insert(string text)
  {
    string leftText = this.GetLeftText();
    string rightText = this.GetRightText();
    int length1 = rightText.Length;
    StringBuilder stringBuilder = new StringBuilder(leftText.Length + rightText.Length + text.Length);
    stringBuilder.Append(leftText);
    int index1 = 0;
    for (int length2 = text.Length; index1 < length2; ++index1)
    {
      char ch = text[index1];
      if (ch == '\b')
        this.DoBackspace();
      else if (this.characterLimit <= 0 || stringBuilder.Length + length1 < this.characterLimit)
      {
        if (this.onValidate != null)
          ch = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, ch);
        else if (this.validation != UIInput.Validation.None)
          ch = this.Validate(stringBuilder.ToString(), stringBuilder.Length, ch);
        if (ch != char.MinValue)
          stringBuilder.Append(ch);
      }
      else
        break;
    }
    this.mSelectionStart = stringBuilder.Length;
    this.mSelectionEnd = this.mSelectionStart;
    int index2 = 0;
    for (int length3 = rightText.Length; index2 < length3; ++index2)
    {
      char ch = rightText[index2];
      if (this.onValidate != null)
        ch = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, ch);
      else if (this.validation != UIInput.Validation.None)
        ch = this.Validate(stringBuilder.ToString(), stringBuilder.Length, ch);
      if (ch != char.MinValue)
        stringBuilder.Append(ch);
    }
    this.mValue = stringBuilder.ToString();
    this.UpdateLabel();
    this.ExecuteOnChange();
  }

  protected string GetLeftText()
  {
    int length = Mathf.Min(this.mSelectionStart, this.mSelectionEnd);
    return !string.IsNullOrEmpty(this.mValue) && length >= 0 ? this.mValue.Substring(0, length) : "";
  }

  protected string GetRightText()
  {
    int startIndex = Mathf.Max(this.mSelectionStart, this.mSelectionEnd);
    return !string.IsNullOrEmpty(this.mValue) && startIndex < this.mValue.Length ? this.mValue.Substring(startIndex) : "";
  }

  protected string GetSelection()
  {
    if (string.IsNullOrEmpty(this.mValue) || this.mSelectionStart == this.mSelectionEnd)
      return "";
    int startIndex = Mathf.Min(this.mSelectionStart, this.mSelectionEnd);
    int num = Mathf.Max(this.mSelectionStart, this.mSelectionEnd);
    return this.mValue.Substring(startIndex, num - startIndex);
  }

  protected int GetCharUnderMouse()
  {
    Vector3[] worldCorners = this.label.worldCorners;
    Ray currentRay = UICamera.currentRay;
    Plane plane;
    // ISSUE: explicit constructor call
    ((Plane) ref plane).\u002Ector(worldCorners[0], worldCorners[1], worldCorners[2]);
    float num;
    return !((Plane) ref plane).Raycast(currentRay, ref num) ? 0 : UIInput.mDrawStart + this.label.GetCharacterIndexAtPosition(((Ray) ref currentRay).GetPoint(num), false);
  }

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed || !this.isSelected || !Object.op_Inequality((Object) this.label, (Object) null) || UICamera.currentScheme != UICamera.ControlScheme.Mouse && UICamera.currentScheme != UICamera.ControlScheme.Touch)
      return;
    this.selectionEnd = this.GetCharUnderMouse();
    if (Input.GetKey((KeyCode) 304) || Input.GetKey((KeyCode) 303))
      return;
    this.selectionStart = this.mSelectionEnd;
  }

  protected virtual void OnDrag(Vector2 delta)
  {
    if (!Object.op_Inequality((Object) this.label, (Object) null) || UICamera.currentScheme != UICamera.ControlScheme.Mouse && UICamera.currentScheme != UICamera.ControlScheme.Touch)
      return;
    this.selectionEnd = this.GetCharUnderMouse();
  }

  private void OnDisable() => this.Cleanup();

  protected virtual void Cleanup()
  {
    if (Object.op_Implicit((Object) this.mHighlight))
      ((Behaviour) this.mHighlight).enabled = false;
    if (Object.op_Implicit((Object) this.mCaret))
      ((Behaviour) this.mCaret).enabled = false;
    if (!Object.op_Implicit((Object) this.mBlankTex))
      return;
    NGUITools.Destroy((Object) this.mBlankTex);
    this.mBlankTex = (Texture2D) null;
  }

  public void Submit()
  {
    if (!NGUITools.GetActive((Behaviour) this))
      return;
    this.mValue = this.value;
    if (Object.op_Equality((Object) UIInput.current, (Object) null))
    {
      UIInput.current = this;
      EventDelegate.Execute(this.onSubmit);
      UIInput.current = (UIInput) null;
    }
    this.SaveToPlayerPrefs(this.mValue);
  }

  public void UpdateLabel()
  {
    if (!Object.op_Inequality((Object) this.label, (Object) null))
      return;
    if (this.mDoInit)
      this.Init();
    bool isSelected = this.isSelected;
    string str1 = this.value;
    bool flag = string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(Input.compositionString);
    this.label.color = !flag || isSelected ? this.activeTextColor : this.mDefaultColor;
    string text;
    if (flag)
    {
      text = isSelected ? "" : this.mDefaultText;
      this.RestoreLabelPivot();
    }
    else
    {
      string str2;
      if (this.inputType == UIInput.InputType.Password)
      {
        str2 = "";
        string str3 = "*";
        if (Object.op_Inequality((Object) this.label.bitmapFont, (Object) null) && this.label.bitmapFont.bmFont != null && this.label.bitmapFont.bmFont.GetGlyph(42) == null)
          str3 = "x";
        int num = 0;
        for (int length = str1.Length; num < length; ++num)
          str2 += str3;
      }
      else
        str2 = str1;
      int num1 = isSelected ? Mathf.Min(str2.Length, this.cursorPosition) : 0;
      string str4 = str2.Substring(0, num1);
      if (isSelected)
        str4 += Input.compositionString;
      text = str4 + str2.Substring(num1, str2.Length - num1);
      if (isSelected && this.label.overflowMethod == UILabel.Overflow.ClampContent && this.label.maxLineCount == 1)
      {
        int offsetToFit1 = this.label.CalculateOffsetToFit(text);
        if (offsetToFit1 == 0)
        {
          UIInput.mDrawStart = 0;
          this.RestoreLabelPivot();
        }
        else if (num1 < UIInput.mDrawStart)
        {
          UIInput.mDrawStart = num1;
          this.SetPivotToLeft();
        }
        else if (offsetToFit1 < UIInput.mDrawStart)
        {
          UIInput.mDrawStart = offsetToFit1;
          this.SetPivotToLeft();
        }
        else
        {
          int offsetToFit2 = this.label.CalculateOffsetToFit(text.Substring(0, num1));
          if (offsetToFit2 > UIInput.mDrawStart)
          {
            UIInput.mDrawStart = offsetToFit2;
            this.SetPivotToRight();
          }
        }
        if (UIInput.mDrawStart != 0)
          text = text.Substring(UIInput.mDrawStart, text.Length - UIInput.mDrawStart);
      }
      else
      {
        UIInput.mDrawStart = 0;
        this.RestoreLabelPivot();
      }
    }
    this.label.text = text;
    if (isSelected && (UIInput.mKeyboard == null || this.inputShouldBeHidden))
    {
      int start = this.mSelectionStart - UIInput.mDrawStart;
      int end = this.mSelectionEnd - UIInput.mDrawStart;
      if (Object.op_Equality((Object) this.mBlankTex, (Object) null))
      {
        this.mBlankTex = new Texture2D(2, 2, (TextureFormat) 5, false);
        for (int index1 = 0; index1 < 2; ++index1)
        {
          for (int index2 = 0; index2 < 2; ++index2)
            this.mBlankTex.SetPixel(index2, index1, Color.white);
        }
        this.mBlankTex.Apply();
      }
      if (start != end)
      {
        if (Object.op_Equality((Object) this.mHighlight, (Object) null))
        {
          this.mHighlight = NGUITools.AddWidget<UITexture>(this.label.cachedGameObject);
          ((Object) this.mHighlight).name = "Input Highlight";
          this.mHighlight.mainTexture = (Texture) this.mBlankTex;
          this.mHighlight.fillGeometry = false;
          this.mHighlight.pivot = this.label.pivot;
          this.mHighlight.SetAnchor(this.label.cachedTransform);
        }
        else
        {
          this.mHighlight.pivot = this.label.pivot;
          this.mHighlight.mainTexture = (Texture) this.mBlankTex;
          this.mHighlight.MarkAsChanged();
          ((Behaviour) this.mHighlight).enabled = true;
        }
      }
      if (Object.op_Equality((Object) this.mCaret, (Object) null))
      {
        this.mCaret = NGUITools.AddWidget<UITexture>(this.label.cachedGameObject);
        ((Object) this.mCaret).name = "Input Caret";
        this.mCaret.mainTexture = (Texture) this.mBlankTex;
        this.mCaret.fillGeometry = false;
        this.mCaret.pivot = this.label.pivot;
        this.mCaret.SetAnchor(this.label.cachedTransform);
      }
      else
      {
        this.mCaret.pivot = this.label.pivot;
        this.mCaret.mainTexture = (Texture) this.mBlankTex;
        this.mCaret.MarkAsChanged();
        ((Behaviour) this.mCaret).enabled = true;
      }
      if (start != end)
      {
        this.label.PrintOverlay(start, end, this.mCaret.geometry, this.mHighlight.geometry, this.caretColor, this.selectionColor);
        ((Behaviour) this.mHighlight).enabled = this.mHighlight.geometry.hasVertices;
      }
      else
      {
        this.label.PrintOverlay(start, end, this.mCaret.geometry, (UIGeometry) null, this.caretColor, this.selectionColor);
        if (Object.op_Inequality((Object) this.mHighlight, (Object) null))
          ((Behaviour) this.mHighlight).enabled = false;
      }
      this.mNextBlink = RealTime.time + 0.5f;
      this.mLastAlpha = this.label.finalAlpha;
    }
    else
      this.Cleanup();
  }

  protected void SetPivotToLeft()
  {
    Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.mPivot);
    pivotOffset.x = 0.0f;
    this.label.pivot = NGUIMath.GetPivot(pivotOffset);
  }

  protected void SetPivotToRight()
  {
    Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.mPivot);
    pivotOffset.x = 1f;
    this.label.pivot = NGUIMath.GetPivot(pivotOffset);
  }

  protected void RestoreLabelPivot()
  {
    if (!Object.op_Inequality((Object) this.label, (Object) null) || this.label.pivot == this.mPivot)
      return;
    this.label.pivot = this.mPivot;
  }

  protected char Validate(string text, int pos, char ch)
  {
    if (this.validation == UIInput.Validation.None || !((Behaviour) this).enabled)
      return ch;
    if (this.validation == UIInput.Validation.Integer)
    {
      if (ch >= '0' && ch <= '9' || ch == '-' && pos == 0 && !text.Contains("-"))
        return ch;
    }
    else if (this.validation == UIInput.Validation.Float)
    {
      if (ch >= '0' && ch <= '9' || ch == '-' && pos == 0 && !text.Contains("-") || ch == '.' && !text.Contains("."))
        return ch;
    }
    else if (this.validation == UIInput.Validation.Alphanumeric)
    {
      if (ch >= 'A' && ch <= 'Z' || ch >= 'a' && ch <= 'z' || ch >= '0' && ch <= '9')
        return ch;
    }
    else if (this.validation == UIInput.Validation.Username)
    {
      if (ch >= 'A' && ch <= 'Z')
        return (char) ((int) ch - 65 + 97);
      if (ch >= 'a' && ch <= 'z' || ch >= '0' && ch <= '9')
        return ch;
    }
    else
    {
      if (this.validation == UIInput.Validation.Filename)
        return ch == ':' || ch == '/' || ch == '\\' || ch == '<' || ch == '>' || ch == '|' || ch == '^' || ch == '*' || ch == ';' || ch == '"' || ch == '`' || ch == '\t' || ch == '\n' ? char.MinValue : ch;
      if (this.validation == UIInput.Validation.Name)
      {
        char ch1 = text.Length > 0 ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ';
        char ch2 = text.Length > 0 ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n';
        if (ch >= 'a' && ch <= 'z')
          return ch1 == ' ' ? (char) ((int) ch - 97 + 65) : ch;
        if (ch >= 'A' && ch <= 'Z')
          return ch1 != ' ' && ch1 != '\'' ? (char) ((int) ch - 65 + 97) : ch;
        if (ch == '\'')
        {
          if (ch1 != ' ' && ch1 != '\'' && ch2 != '\'' && !text.Contains("'"))
            return ch;
        }
        else if (ch == ' ' && ch1 != ' ' && ch1 != '\'' && ch2 != ' ' && ch2 != '\'')
          return ch;
      }
    }
    return char.MinValue;
  }

  protected void ExecuteOnChange()
  {
    if (!Object.op_Equality((Object) UIInput.current, (Object) null) || !EventDelegate.IsValid(this.onChange))
      return;
    UIInput.current = this;
    EventDelegate.Execute(this.onChange);
    UIInput.current = (UIInput) null;
  }

  public void RemoveFocus() => this.isSelected = false;

  public void SaveValue() => this.SaveToPlayerPrefs(this.mValue);

  public void LoadValue()
  {
    if (string.IsNullOrEmpty(this.savedAs))
      return;
    string str = this.mValue.Replace("\\n", "\n");
    this.mValue = "";
    this.value = PlayerPrefs.HasKey(this.savedAs) ? PlayerPrefs.GetString(this.savedAs) : str;
  }

  public enum InputType
  {
    Standard,
    AutoCorrect,
    Password,
  }

  public enum Validation
  {
    None,
    Integer,
    Float,
    Alphanumeric,
    Username,
    Name,
    Filename,
  }

  public enum KeyboardType
  {
    Default,
    ASCIICapable,
    NumbersAndPunctuation,
    URL,
    NumberPad,
    PhonePad,
    NamePhonePad,
    EmailAddress,
  }

  public enum OnReturnKey
  {
    Default,
    Submit,
    NewLine,
  }

  public delegate char OnValidate(string text, int charIndex, char addedChar);
}
