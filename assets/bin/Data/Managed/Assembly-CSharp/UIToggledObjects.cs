// Decompiled with JetBrains decompiler
// Type: UIToggledObjects
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Toggled Objects")]
public class UIToggledObjects : MonoBehaviour
{
  public List<GameObject> activate;
  public List<GameObject> deactivate;
  [HideInInspector]
  [SerializeField]
  private GameObject target;
  [HideInInspector]
  [SerializeField]
  private bool inverse;

  private void Awake()
  {
    if (Object.op_Inequality((Object) this.target, (Object) null))
    {
      if (this.activate.Count == 0 && this.deactivate.Count == 0)
      {
        if (this.inverse)
          this.deactivate.Add(this.target);
        else
          this.activate.Add(this.target);
      }
      else
        this.target = (GameObject) null;
    }
    EventDelegate.Add(((Component) this).GetComponent<UIToggle>().onChange, new EventDelegate.Callback(this.Toggle));
  }

  public void Toggle()
  {
    bool state = UIToggle.current.value;
    if (!((Behaviour) this).enabled)
      return;
    for (int index = 0; index < this.activate.Count; ++index)
      this.Set(this.activate[index], state);
    for (int index = 0; index < this.deactivate.Count; ++index)
      this.Set(this.deactivate[index], !state);
  }

  private void Set(GameObject go, bool state)
  {
    if (!Object.op_Inequality((Object) go, (Object) null))
      return;
    NGUITools.SetActive(go, state);
  }
}
