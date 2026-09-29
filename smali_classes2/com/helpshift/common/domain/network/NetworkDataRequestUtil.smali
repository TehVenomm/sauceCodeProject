.class public Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;
.super Ljava/lang/Object;
.source "NetworkDataRequestUtil.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 16
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static cleanData(Ljava/util/Map;)Ljava/util/Map;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 77
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 78
    invoke-interface {p0}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_0
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    if-eqz v2, :cond_0

    .line 80
    invoke-interface {p0, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    if-eqz v3, :cond_0

    .line 82
    invoke-interface {v0, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_1
    return-object v0
.end method

.method public static getAdjustedTimestamp(Lcom/helpshift/common/platform/network/NetworkRequestDAO;)Ljava/lang/String;
    .locals 5

    .line 111
    invoke-interface {p0}, Lcom/helpshift/common/platform/network/NetworkRequestDAO;->getServerTimeDelta()F

    move-result p0

    .line 112
    new-instance v0, Ljava/text/DecimalFormat;

    const-string v1, "0.000"

    new-instance v2, Ljava/text/DecimalFormatSymbols;

    sget-object v3, Ljava/util/Locale;->US:Ljava/util/Locale;

    invoke-direct {v2, v3}, Ljava/text/DecimalFormatSymbols;-><init>(Ljava/util/Locale;)V

    invoke-direct {v0, v1, v2}, Ljava/text/DecimalFormat;-><init>(Ljava/lang/String;Ljava/text/DecimalFormatSymbols;)V

    .line 114
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    long-to-double v1, v1

    const-wide v3, 0x408f400000000000L    # 1000.0

    invoke-static {v1, v2}, Ljava/lang/Double;->isNaN(D)Z

    div-double/2addr v1, v3

    float-to-double v3, p0

    .line 115
    invoke-static {v3, v4}, Ljava/lang/Double;->isNaN(D)Z

    add-double/2addr v1, v3

    invoke-virtual {v0, v1, v2}, Ljava/text/DecimalFormat;->format(D)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static getSdkMeta()Ljava/util/Map;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 94
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "ia"

    const/4 v2, 0x1

    .line 96
    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    invoke-interface {v0, v1, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "rs"

    .line 98
    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    invoke-interface {v0, v1, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "clc"

    .line 100
    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    invoke-interface {v0, v1, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "atai_v2"

    .line 102
    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    invoke-interface {v0, v1, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "fp"

    .line 104
    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    invoke-interface {v0, v1, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "cb"

    .line 106
    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-object v0
.end method

.method public static getUserRequestData(Lcom/helpshift/account/domainmodel/ClearedUserDM;)Ljava/util/HashMap;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/account/domainmodel/ClearedUserDM;",
            ")",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 53
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    if-eqz p0, :cond_3

    .line 57
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->deviceId:Ljava/lang/String;

    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_0

    const-string v1, "did"

    .line 58
    iget-object v2, p0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->deviceId:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 61
    :cond_0
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->identifier:Ljava/lang/String;

    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_1

    const-string v1, "uid"

    .line 62
    iget-object v2, p0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->identifier:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 65
    :cond_1
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->email:Ljava/lang/String;

    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_2

    const-string v1, "email"

    .line 66
    iget-object v2, p0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->email:Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 69
    :cond_2
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->authToken:Ljava/lang/String;

    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_3

    const-string v1, "user_auth_token"

    .line 70
    iget-object p0, p0, Lcom/helpshift/account/domainmodel/ClearedUserDM;->authToken:Ljava/lang/String;

    invoke-virtual {v0, v1, p0}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_3
    return-object v0
.end method

.method public static getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/account/domainmodel/UserDM;",
            ")",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 29
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    if-eqz p0, :cond_3

    .line 33
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserDM;->getDeviceId()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_0

    const-string v1, "did"

    .line 34
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserDM;->getDeviceId()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 37
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserDM;->getIdentifier()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_1

    const-string v1, "uid"

    .line 38
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserDM;->getIdentifier()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 41
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserDM;->getEmail()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_2

    const-string v1, "email"

    .line 42
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserDM;->getEmail()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 45
    :cond_2
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserDM;->getAuthToken()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_3

    const-string v1, "user_auth_token"

    .line 46
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserDM;->getAuthToken()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, v1, p0}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_3
    return-object v0
.end method

.method public static getUserRequestData(Lcom/helpshift/account/domainmodel/UserManagerDM;)Ljava/util/HashMap;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/account/domainmodel/UserManagerDM;",
            ")",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    if-eqz p0, :cond_0

    .line 23
    invoke-virtual {p0}, Lcom/helpshift/account/domainmodel/UserManagerDM;->getActiveUser()Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object p0

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    .line 25
    :goto_0
    invoke-static {p0}, Lcom/helpshift/common/domain/network/NetworkDataRequestUtil;->getUserRequestData(Lcom/helpshift/account/domainmodel/UserDM;)Ljava/util/HashMap;

    move-result-object p0

    return-object p0
.end method
