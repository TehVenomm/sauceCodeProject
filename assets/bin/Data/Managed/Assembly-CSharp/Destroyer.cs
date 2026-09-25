// Decompiled with JetBrains decompiler
// Type: Destroyer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Destroyer : MonoBehaviour
{
  public float time;

  private void Start()
  {
    if ((double) this.time > 0.0)
      return;
    ((Behaviour) this).enabled = false;
  }

  private void Update()
  {
    this.time -= Time.deltaTime;
    if ((double) this.time > 0.0)
      return;
    this.DestroyGameObject();
  }

  public void DestroyGameObject() => Object.Destroy((Object) ((Component) this).gameObject);
}
