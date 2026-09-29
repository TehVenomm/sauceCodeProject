// Decompiled with JetBrains decompiler
// Type: TextTalk
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#nullable disable
public class TextTalk : MonoBehaviour
{
  private bool isStart;
  private UILabel lbl;
  private List<string[]> textList;
  private string[] text;
  private float speed;
  private int page;
  private TagAnalyzer analyzer = new TagAnalyzer();
  private StringBuilder sb = new StringBuilder();
  private int textIndex;
  private int textCount;
  private float textAnimTime;
  private bool isComplete;
  private bool isPause;
  private float breakTime;
  private const float CONST_BREAK_TIME = 1f;
  private const float DEFAULT_NUM_PER_SEC = 30f;
  private System.Action pageEndCallback;
  private Action<string, string> tagCallback;

  public void Initialize(
    Transform lbl_t,
    List<string[]> _text_list,
    System.Action page_end_call_back = null,
    Action<string, string> tag_call_back = null,
    int num_per_sec = 0)
  {
    if (Object.op_Equality((Object) lbl_t, (Object) null) || _text_list == null || _text_list.Count == 0)
      return;
    this.lbl = ((Component) lbl_t).GetComponent<UILabel>();
    if (Object.op_Equality((Object) this.lbl, (Object) null))
      return;
    this.isStart = false;
    this.lbl.text = string.Empty;
    this.page = 0;
    this.textList = _text_list;
    this.text = this.textList[this.page++];
    this.speed = Mathf.Max(1f, num_per_sec == 0 ? 30f : (float) num_per_sec);
    this.sb.Remove(0, this.sb.ToString().Length);
    this.textIndex = 0;
    this.textCount = 0;
    this.textAnimTime = 0.0f;
    this.breakTime = 0.0f;
    this.isComplete = false;
    this.pageEndCallback = page_end_call_back;
    this.tagCallback = tag_call_back;
  }

  public void StartTalk() => this.isStart = true;

  public bool IsComplete() => this.isStart && this.isComplete;

  public bool IsTalking() => this.isStart && !this.isComplete;

  public void SkipOneStep()
  {
    if (!this.isStart)
      return;
    if (this.isPause)
      this.NextPage();
    else if ((double) this.breakTime > 0.0)
      this.breakTime = 0.0f;
    else
      this.textAnimTime = (float) this.text[this.textIndex].Length;
  }

  public void SkipAll()
  {
    string[] text = this.textList[this.textList.Count - 1];
    this.sb.Remove(0, this.sb.ToString().Length);
    int index = 0;
    for (int length = text.Length; index < length; ++index)
      this.sb.Append(text[index]);
    this.lbl.text = this.sb.ToString();
    this.isComplete = true;
  }

  private void NextPage()
  {
    if (!this.isStart)
      return;
    this.text = this.textList[this.page++];
    this.sb.Remove(0, this.sb.ToString().Length);
    this.lbl.text = this.sb.ToString();
    this.textIndex = 0;
    this.textCount = 0;
    this.textAnimTime = 0.0f;
    this.breakTime = 0.0f;
    this.isPause = false;
  }

  private void Update()
  {
    if (!this.isStart || Object.op_Equality((Object) this.lbl, (Object) null) || this.text == null || this.text.Length == 0 || this.isComplete || this.isPause)
      return;
    float num1 = Time.deltaTime;
    if ((double) this.breakTime > 0.0)
    {
      this.breakTime -= num1;
      if ((double) this.breakTime > 0.0)
        return;
      num1 = -this.breakTime;
    }
    this.textAnimTime += num1;
    int num2 = (int) ((double) this.textAnimTime * (double) this.speed);
    if (num2 <= this.textCount)
      return;
    while (num2 > this.textCount)
    {
      if (this.textCount >= this.text[this.textIndex].Length)
      {
        if (this.textIndex == this.text.Length - 1)
        {
          this.textAnimTime = 0.0f;
          if (this.page < this.textList.Count)
            this.isPause = true;
          else
            this.isComplete = true;
          if (this.pageEndCallback != null)
          {
            this.pageEndCallback();
            break;
          }
          break;
        }
        this.sb.AppendLine();
        this.textCount = 0;
        this.textAnimTime = 0.0f;
        ++this.textIndex;
        this.breakTime = 1f;
        break;
      }
      int textCount = this.textCount;
      this.textCount = this.analyzer.Analyze(this.text[this.textIndex], this.textIndex, this.textCount);
      if (!this.analyzer.IsFindTag())
        this.sb.Append(this.text[this.textIndex][this.textCount++]);
      else if (this.tagCallback != null)
        this.tagCallback(this.analyzer.findTag, this.analyzer.findTagText);
    }
    this.lbl.text = this.sb.ToString();
  }
}
