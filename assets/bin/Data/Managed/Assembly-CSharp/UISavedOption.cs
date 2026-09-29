// Decompiled with JetBrains decompiler
// Type: UISavedOption
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Saved Option")]
public class UISavedOption : MonoBehaviour
{
  public string keyName;
  private UIPopupList mList;
  private UIToggle mCheck;
  private UIProgressBar mSlider;

  private string key
  {
    get
    {
      return !string.IsNullOrEmpty(this.keyName) ? this.keyName : "NGUI State: " + ((Object) this).name;
    }
  }

  private void Awake()
  {
    this.mList = ((Component) this).GetComponent<UIPopupList>();
    this.mCheck = ((Component) this).GetComponent<UIToggle>();
    this.mSlider = ((Component) this).GetComponent<UIProgressBar>();
  }

  private void OnEnable()
  {
    if (Object.op_Inequality((Object) this.mList, (Object) null))
    {
      EventDelegate.Add(this.mList.onChange, new EventDelegate.Callback(this.SaveSelection));
      string str = PlayerPrefs.GetString(this.key);
      if (string.IsNullOrEmpty(str))
        return;
      this.mList.value = str;
    }
    else if (Object.op_Inequality((Object) this.mCheck, (Object) null))
    {
      EventDelegate.Add(this.mCheck.onChange, new EventDelegate.Callback(this.SaveState));
      this.mCheck.value = PlayerPrefs.GetInt(this.key, this.mCheck.startsActive ? 1 : 0) != 0;
    }
    else if (Object.op_Inequality((Object) this.mSlider, (Object) null))
    {
      EventDelegate.Add(this.mSlider.onChange, new EventDelegate.Callback(this.SaveProgress));
      this.mSlider.value = PlayerPrefs.GetFloat(this.key, this.mSlider.value);
    }
    else
    {
      string str = PlayerPrefs.GetString(this.key);
      UIToggle[] componentsInChildren = ((Component) this).GetComponentsInChildren<UIToggle>(true);
      int index = 0;
      for (int length = componentsInChildren.Length; index < length; ++index)
      {
        UIToggle uiToggle = componentsInChildren[index];
        uiToggle.value = ((Object) uiToggle).name == str;
      }
    }
  }

  private void OnDisable()
  {
    if (Object.op_Inequality((Object) this.mCheck, (Object) null))
      EventDelegate.Remove(this.mCheck.onChange, new EventDelegate.Callback(this.SaveState));
    else if (Object.op_Inequality((Object) this.mList, (Object) null))
      EventDelegate.Remove(this.mList.onChange, new EventDelegate.Callback(this.SaveSelection));
    else if (Object.op_Inequality((Object) this.mSlider, (Object) null))
    {
      EventDelegate.Remove(this.mSlider.onChange, new EventDelegate.Callback(this.SaveProgress));
    }
    else
    {
      UIToggle[] componentsInChildren = ((Component) this).GetComponentsInChildren<UIToggle>(true);
      int index = 0;
      for (int length = componentsInChildren.Length; index < length; ++index)
      {
        UIToggle uiToggle = componentsInChildren[index];
        if (uiToggle.value)
        {
          PlayerPrefs.SetString(this.key, ((Object) uiToggle).name);
          break;
        }
      }
    }
  }

  public void SaveSelection() => PlayerPrefs.SetString(this.key, UIPopupList.current.value);

  public void SaveState() => PlayerPrefs.SetInt(this.key, UIToggle.current.value ? 1 : 0);

  public void SaveProgress() => PlayerPrefs.SetFloat(this.key, UIProgressBar.current.value);
}
