// Decompiled with JetBrains decompiler
// Type: UISpriteAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[RequireComponent(typeof (UISprite))]
[AddComponentMenu("NGUI/UI/Sprite Animation")]
public class UISpriteAnimation : MonoBehaviour
{
  [HideInInspector]
  [SerializeField]
  protected int mFPS = 30;
  [HideInInspector]
  [SerializeField]
  protected string mPrefix = "";
  [HideInInspector]
  [SerializeField]
  protected bool mLoop = true;
  [HideInInspector]
  [SerializeField]
  protected bool mSnap = true;
  protected UISprite mSprite;
  protected float mDelta;
  protected int mIndex;
  protected bool mActive = true;
  protected List<string> mSpriteNames = new List<string>();

  public int frames => this.mSpriteNames.Count;

  public int framesPerSecond
  {
    get => this.mFPS;
    set => this.mFPS = value;
  }

  public string namePrefix
  {
    get => this.mPrefix;
    set
    {
      if (!(this.mPrefix != value))
        return;
      this.mPrefix = value;
      this.RebuildSpriteList();
    }
  }

  public bool loop
  {
    get => this.mLoop;
    set => this.mLoop = value;
  }

  public bool isPlaying => this.mActive;

  protected virtual void Start() => this.RebuildSpriteList();

  protected virtual void Update()
  {
    if (!this.mActive || this.mSpriteNames.Count <= 1 || !Application.isPlaying || this.mFPS <= 0)
      return;
    this.mDelta += RealTime.deltaTime;
    float num = 1f / (float) this.mFPS;
    if ((double) num >= (double) this.mDelta)
      return;
    this.mDelta = (double) num > 0.0 ? this.mDelta - num : 0.0f;
    if (++this.mIndex >= this.mSpriteNames.Count)
    {
      this.mIndex = 0;
      this.mActive = this.mLoop;
    }
    if (!this.mActive)
      return;
    this.mSprite.spriteName = this.mSpriteNames[this.mIndex];
    if (!this.mSnap)
      return;
    this.mSprite.MakePixelPerfect();
  }

  public void RebuildSpriteList()
  {
    if (Object.op_Equality((Object) this.mSprite, (Object) null))
      this.mSprite = ((Component) this).GetComponent<UISprite>();
    this.mSpriteNames.Clear();
    if (!Object.op_Inequality((Object) this.mSprite, (Object) null) || !Object.op_Inequality((Object) this.mSprite.atlas, (Object) null))
      return;
    List<UISpriteData> spriteList = this.mSprite.atlas.spriteList;
    int index = 0;
    for (int count = spriteList.Count; index < count; ++index)
    {
      UISpriteData uiSpriteData = spriteList[index];
      if (string.IsNullOrEmpty(this.mPrefix) || uiSpriteData.name.StartsWith(this.mPrefix))
        this.mSpriteNames.Add(uiSpriteData.name);
    }
    this.mSpriteNames.Sort();
  }

  public void Play() => this.mActive = true;

  public void Pause() => this.mActive = false;

  public void ResetToBeginning()
  {
    this.mActive = true;
    this.mIndex = 0;
    if (!Object.op_Inequality((Object) this.mSprite, (Object) null) || this.mSpriteNames.Count <= 0)
      return;
    this.mSprite.spriteName = this.mSpriteNames[this.mIndex];
    if (!this.mSnap)
      return;
    this.mSprite.MakePixelPerfect();
  }
}
