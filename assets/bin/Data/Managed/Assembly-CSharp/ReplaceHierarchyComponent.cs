// Decompiled with JetBrains decompiler
// Type: ReplaceHierarchyComponent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ReplaceHierarchyComponent : MonoBehaviour
{
  [Tooltip("階層を入れ替えるオブジェクト")]
  public GameObject targetObject;

  public void Awake()
  {
    if (Object.op_Equality((Object) this.targetObject, (Object) null))
      return;
    this.targetObject.transform.parent = ((Component) this).transform.parent;
    this.targetObject.transform.localPosition = ((Component) this).transform.localPosition;
    this.targetObject.transform.localScale = ((Component) this).transform.localScale;
    this.targetObject.transform.localRotation = ((Component) this).transform.localRotation;
    Object.Destroy((Object) ((Component) this).gameObject);
  }
}
