// Decompiled with JetBrains decompiler
// Type: ClanMemberListUIController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class ClanMemberListUIController : ScrollItemListControllerBase
{
  private static readonly string LIST_ITEM_PREFAB_NAME = "";

  public ClanMemberListUIController()
  {
  }

  public ClanMemberListUIController(ClanMemberListUIController.InitParam _initParam)
    : base((ScrollItemListControllerBase.InitializeParameter) _initParam)
  {
  }

  protected override IEnumerator RequestNextPageInfo(int _nextPageNum, Action<bool, int> _callback)
  {
    yield return (object) null;
  }

  protected override void OnCallbackRequestPageInfo(bool _isSucceeded, int _nextPageNum)
  {
    base.OnCallbackRequestPageInfo(_isSucceeded, _nextPageNum);
  }

  public override string GetItemPrefabName() => ClanMemberListUIController.LIST_ITEM_PREFAB_NAME;

  public override void SetListItem(int i, Transform t, bool is_recycle)
  {
  }

  public class InitParam : ScrollItemListControllerBase.InitializeParameter
  {
  }
}
