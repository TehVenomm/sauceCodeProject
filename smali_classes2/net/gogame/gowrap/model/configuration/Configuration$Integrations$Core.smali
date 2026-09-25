.class public Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;
.super Ljava/lang/Object;
.source "Configuration.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Core"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;
    }
.end annotation


# instance fields
.field private locales:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;",
            ">;"
        }
    .end annotation
.end field

.field private slideIn:Ljava/lang/Boolean;

.field private slideOut:Ljava/lang/Boolean;

.field private supportedLocales:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 94
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public constructor <init>(Lorg/json/JSONObject;)V
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 98
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "locales"

    .line 100
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    const-string v0, "locales"

    .line 101
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 103
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    iput-object v1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->locales:Ljava/util/Map;

    .line 104
    invoke-virtual {v0}, Lorg/json/JSONObject;->keys()Ljava/util/Iterator;

    move-result-object v1

    .line 105
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    .line 106
    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 107
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v3

    if-eqz v3, :cond_0

    .line 109
    iget-object v4, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->locales:Ljava/util/Map;

    new-instance v5, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;

    invoke-direct {v5, v3}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;-><init>(Lorg/json/JSONObject;)V

    invoke-interface {v4, v2, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 111
    :cond_0
    iget-object v3, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->locales:Ljava/util/Map;

    const/4 v4, 0x0

    invoke-interface {v3, v2, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_1
    const-string v0, "supportedLocales"

    .line 116
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_2

    const-string v0, "supportedLocales"

    .line 117
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->optJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 120
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    iput-object v1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->supportedLocales:Ljava/util/List;

    const/4 v1, 0x0

    .line 121
    :goto_1
    invoke-virtual {v0}, Lorg/json/JSONArray;->length()I

    move-result v2

    if-ge v1, v2, :cond_2

    .line 122
    iget-object v2, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->supportedLocales:Ljava/util/List;

    invoke-virtual {v0, v1}, Lorg/json/JSONArray;->getString(I)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v2, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v1, v1, 0x1

    goto :goto_1

    :cond_2
    const-string v0, "slideOut"

    .line 126
    invoke-static {p1, v0}, Lnet/gogame/gowrap/support/JSONUtils;->optBoolean(Lorg/json/JSONObject;Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->slideOut:Ljava/lang/Boolean;

    const-string v0, "slideIn"

    .line 127
    invoke-static {p1, v0}, Lnet/gogame/gowrap/support/JSONUtils;->optBoolean(Lorg/json/JSONObject;Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->slideIn:Ljava/lang/Boolean;

    return-void
.end method


# virtual methods
.method public getLocales()Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;",
            ">;"
        }
    .end annotation

    .line 131
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->locales:Ljava/util/Map;

    return-object v0
.end method

.method public getSlideIn()Ljava/lang/Boolean;
    .locals 1

    .line 155
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->slideIn:Ljava/lang/Boolean;

    return-object v0
.end method

.method public getSlideOut()Ljava/lang/Boolean;
    .locals 1

    .line 147
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->slideOut:Ljava/lang/Boolean;

    return-object v0
.end method

.method public getSupportedLocales()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 139
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->supportedLocales:Ljava/util/List;

    return-object v0
.end method

.method public setLocales(Ljava/util/Map;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;",
            ">;)V"
        }
    .end annotation

    .line 135
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->locales:Ljava/util/Map;

    return-void
.end method

.method public setSlideIn(Ljava/lang/Boolean;)V
    .locals 0

    .line 159
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->slideIn:Ljava/lang/Boolean;

    return-void
.end method

.method public setSlideOut(Ljava/lang/Boolean;)V
    .locals 0

    .line 151
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->slideOut:Ljava/lang/Boolean;

    return-void
.end method

.method public setSupportedLocales(Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 143
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->supportedLocales:Ljava/util/List;

    return-void
.end method
