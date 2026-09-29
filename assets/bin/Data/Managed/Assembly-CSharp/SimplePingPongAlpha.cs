// Decompiled with JetBrains decompiler
// Type: SimplePingPongAlpha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SimplePingPongAlpha : MonoBehaviour
{
  [SerializeField]
  private UISprite target;
  [SerializeField]
  private float from;
  [SerializeField]
  private float to;
  [SerializeField]
  private float sec;
  [SerializeField]
  private int endCount;
  [SerializeField]
  private bool endDisable;
  private SimplePingPongAlpha.eState state;
  private int nowCount;
  private float addValue;
  private Color color;

  public void Initialize()
  {
    if (this.state != SimplePingPongAlpha.eState.None)
      return;
    this.from = this.LimitValue(this.from);
    this.to = this.LimitValue(this.to);
    this.addValue = (double) this.sec == 0.0 ? 0.0f : (this.to - this.from) / this.sec;
    this.color = this.target.color;
    this.state = SimplePingPongAlpha.eState.Idle;
  }

  public void Play(bool startDefaultValue)
  {
    if (this.state == SimplePingPongAlpha.eState.None)
      this.Initialize();
    else if (this.state == SimplePingPongAlpha.eState.Forward || this.state == SimplePingPongAlpha.eState.Back)
      return;
    if (startDefaultValue)
      this.SetValue(this.from);
    this.nowCount = 0;
    this.state = SimplePingPongAlpha.eState.Forward;
    ((Component) this.target).gameObject.SetActive(true);
  }

  private void Update()
  {
    if (this.state == SimplePingPongAlpha.eState.None || this.state == SimplePingPongAlpha.eState.Idle)
      return;
    float num = this.addValue * Time.deltaTime;
    if (this.state == SimplePingPongAlpha.eState.Forward)
    {
      if (!this.AddValue(num))
        return;
      this.state = SimplePingPongAlpha.eState.Back;
    }
    else
    {
      if (this.state != SimplePingPongAlpha.eState.Back || !this.SubValue(num))
        return;
      if (++this.nowCount >= this.endCount)
      {
        if (this.endDisable)
          ((Component) this.target).gameObject.SetActive(false);
        this.state = SimplePingPongAlpha.eState.Idle;
      }
      else
        this.state = SimplePingPongAlpha.eState.Forward;
    }
  }

  private void SetValue(float value)
  {
    this.color.a = value;
    this.target.color = this.color;
  }

  private bool AddValue(float value)
  {
    bool flag = false;
    this.color.a += value;
    if ((double) this.color.a >= (double) this.to)
    {
      this.color.a = this.to;
      flag = true;
    }
    this.target.color = this.color;
    return flag;
  }

  private bool SubValue(float value)
  {
    bool flag = false;
    this.color.a -= value;
    if ((double) this.color.a <= (double) this.from)
    {
      this.color.a = this.from;
      flag = true;
    }
    this.target.color = this.color;
    return flag;
  }

  private float LimitValue(float value)
  {
    if ((double) value < 0.0)
      value = 0.0f;
    if ((double) value > 1.0)
      value = 1f;
    return value;
  }

  private enum eState
  {
    None,
    Idle,
    Forward,
    Back,
  }
}
