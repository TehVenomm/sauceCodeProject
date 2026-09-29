// Decompiled with JetBrains decompiler
// Type: Helpshift.HelpshiftJSONUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using HSMiniJSON;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helpshift;

public static class HelpshiftJSONUtility
{
  public static HelpshiftUser getHelpshiftUser(string serializedJSONHelpshiftUser)
  {
    Dictionary<string, object> dictionary = (Dictionary<string, object>) Json.Deserialize(serializedJSONHelpshiftUser);
    string identifier = Convert.ToString(dictionary["identifier"]);
    string email = Convert.ToString(dictionary["email"]);
    string authToken = Convert.ToString(dictionary["authToken"]);
    string name = Convert.ToString(dictionary["name"]);
    HelpshiftUser.Builder builder = new HelpshiftUser.Builder(identifier, email);
    builder.setName(name);
    builder.setAuthToken(authToken);
    return builder.build();
  }

  public static HelpshiftAuthFailureReason getAuthFailureReason(string serializedJSONAuthFailure)
  {
    string str = Convert.ToString(((Dictionary<string, object>) Json.Deserialize(serializedJSONAuthFailure))["authFailureReason"]);
    HelpshiftAuthFailureReason authFailureReason = HelpshiftAuthFailureReason.INVALID_AUTH_TOKEN;
    if ("0".Equals(str))
      authFailureReason = HelpshiftAuthFailureReason.AUTH_TOKEN_NOT_PROVIDED;
    else if ("1".Equals(str))
      authFailureReason = HelpshiftAuthFailureReason.INVALID_AUTH_TOKEN;
    return authFailureReason;
  }
}
