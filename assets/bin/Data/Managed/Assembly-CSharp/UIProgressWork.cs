// Decompiled with JetBrains decompiler
// Type: UIProgressWork
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIProgressWork : MonoBehaviour
{
  private int min;
  private int max = 100;
  private int now;
  private bool enableUpdateValue = true;

  public int minValue
  {
    get => this.min;
    set
    {
      this.min = value;
      this.UpdateValue();
    }
  }

  public int maxValue
  {
    get => this.max;
    set
    {
      this.max = value;
      this.UpdateValue();
    }
  }

  public int value
  {
    get => this.now;
    set
    {
      this.now = Mathf.Clamp(value, this.min, this.max);
      this.enableUpdateValue = false;
      if (this.min == this.max)
        this.SetValue(1f);
      else
        this.SetValue((float) (this.now - this.min) / (float) (this.max - this.min));
      this.enableUpdateValue = true;
    }
  }

  public UIProgressBar progress { get; private set; }

  private void Awake()
  {
    this.progress = ((Component) this).GetComponent<UIProgressBar>();
    this.UpdateValue();
    EventDelegate.Add(this.progress.onChange, new EventDelegate.Callback(this.OnValueChange));
  }

  private void OnValueChange() => this.UpdateValue();

  private void UpdateValue()
  {
    if (!this.enableUpdateValue)
      return;
    if (this.min > this.max)
      this.min = this.max;
    int num = this.max - this.min;
    ((Behaviour) this.progress).enabled = true;
    if (num == 0)
    {
      this.SetValue(1f);
      this.progress.numberOfSteps = 0;
      ((Behaviour) this.progress).enabled = false;
    }
    else
    {
      this.now = Mathf.RoundToInt((float) num * this.progress.value) + this.min;
      this.progress.numberOfSteps = num + 1;
    }
  }

  private void SetValue(float value)
  {
    bool enabled = ((Behaviour) this.progress).enabled;
    bool enableUpdateValue = this.enableUpdateValue;
    ((Behaviour) this.progress).enabled = true;
    this.enableUpdateValue = false;
    if ((double) this.progress.value == (double) value)
      this.progress.value = 1f - value;
    this.progress.value = value;
    this.enableUpdateValue = enableUpdateValue;
    ((Behaviour) this.progress).enabled = enabled;
  }
}
