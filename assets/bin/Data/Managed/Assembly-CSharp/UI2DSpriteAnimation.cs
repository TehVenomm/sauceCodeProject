// Decompiled with JetBrains decompiler
// Type: UI2DSpriteAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UI2DSpriteAnimation : MonoBehaviour
{
  [SerializeField]
  protected int framerate = 20;
  public bool ignoreTimeScale = true;
  public bool loop = true;
  public Sprite[] frames;
  private SpriteRenderer mUnitySprite;
  private UI2DSprite mNguiSprite;
  private int mIndex;
  private float mUpdate;

  public bool isPlaying => ((Behaviour) this).enabled;

  public int framesPerSecond
  {
    get => this.framerate;
    set => this.framerate = value;
  }

  public void Play()
  {
    if (this.frames == null || this.frames.Length == 0)
      return;
    if (!((Behaviour) this).enabled && !this.loop)
    {
      int num = this.framerate > 0 ? this.mIndex + 1 : this.mIndex - 1;
      if (num < 0 || num >= this.frames.Length)
        this.mIndex = this.framerate < 0 ? this.frames.Length - 1 : 0;
    }
    ((Behaviour) this).enabled = true;
    this.UpdateSprite();
  }

  public void Pause() => ((Behaviour) this).enabled = false;

  public void ResetToBeginning()
  {
    this.mIndex = this.framerate < 0 ? this.frames.Length - 1 : 0;
    this.UpdateSprite();
  }

  private void Start() => this.Play();

  private void Update()
  {
    if (this.frames == null || this.frames.Length == 0)
    {
      ((Behaviour) this).enabled = false;
    }
    else
    {
      if (this.framerate == 0)
        return;
      float num = this.ignoreTimeScale ? RealTime.time : Time.time;
      if ((double) this.mUpdate >= (double) num)
        return;
      this.mUpdate = num;
      int val = this.framerate > 0 ? this.mIndex + 1 : this.mIndex - 1;
      if (!this.loop && (val < 0 || val >= this.frames.Length))
      {
        ((Behaviour) this).enabled = false;
      }
      else
      {
        this.mIndex = NGUIMath.RepeatIndex(val, this.frames.Length);
        this.UpdateSprite();
      }
    }
  }

  private void UpdateSprite()
  {
    if (Object.op_Equality((Object) this.mUnitySprite, (Object) null) && Object.op_Equality((Object) this.mNguiSprite, (Object) null))
    {
      this.mUnitySprite = ((Component) this).GetComponent<SpriteRenderer>();
      this.mNguiSprite = ((Component) this).GetComponent<UI2DSprite>();
      if (Object.op_Equality((Object) this.mUnitySprite, (Object) null) && Object.op_Equality((Object) this.mNguiSprite, (Object) null))
      {
        ((Behaviour) this).enabled = false;
        return;
      }
    }
    float num = this.ignoreTimeScale ? RealTime.time : Time.time;
    if (this.framerate != 0)
      this.mUpdate = num + Mathf.Abs(1f / (float) this.framerate);
    if (Object.op_Inequality((Object) this.mUnitySprite, (Object) null))
    {
      this.mUnitySprite.sprite = this.frames[this.mIndex];
    }
    else
    {
      if (!Object.op_Inequality((Object) this.mNguiSprite, (Object) null))
        return;
      this.mNguiSprite.nextSprite = this.frames[this.mIndex];
    }
  }
}
