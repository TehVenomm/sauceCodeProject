.class public Lnet/gogame/gowrap/model/configuration/Configuration;
.super Ljava/lang/Object;
.source "Configuration.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/model/configuration/Configuration$Settings;,
        Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;
    }
.end annotation


# instance fields
.field private integrations:Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

.field private settings:Lnet/gogame/gowrap/model/configuration/Configuration$Settings;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 21
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public constructor <init>(Lorg/json/JSONObject;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 25
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    if-eqz p1, :cond_1

    const-string v0, "integrations"

    .line 28
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "integrations"

    .line 29
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 31
    new-instance v1, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    invoke-direct {v1, v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;-><init>(Lorg/json/JSONObject;)V

    iput-object v1, p0, Lnet/gogame/gowrap/model/configuration/Configuration;->integrations:Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    :cond_0
    const-string v0, "settings"

    .line 34
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    const-string v0, "settings"

    .line 35
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p1

    if-eqz p1, :cond_1

    .line 37
    new-instance v0, Lnet/gogame/gowrap/model/configuration/Configuration$Settings;

    invoke-direct {v0, p1}, Lnet/gogame/gowrap/model/configuration/Configuration$Settings;-><init>(Lorg/json/JSONObject;)V

    iput-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration;->settings:Lnet/gogame/gowrap/model/configuration/Configuration$Settings;

    :cond_1
    return-void
.end method


# virtual methods
.method public getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;
    .locals 1

    .line 44
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration;->integrations:Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    return-object v0
.end method

.method public getSettings()Lnet/gogame/gowrap/model/configuration/Configuration$Settings;
    .locals 1

    .line 52
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration;->settings:Lnet/gogame/gowrap/model/configuration/Configuration$Settings;

    return-object v0
.end method

.method public setIntegrations(Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;)V
    .locals 0

    .line 48
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration;->integrations:Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    return-void
.end method

.method public setSettings(Lnet/gogame/gowrap/model/configuration/Configuration$Settings;)V
    .locals 0

    .line 56
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration;->settings:Lnet/gogame/gowrap/model/configuration/Configuration$Settings;

    return-void
.end method
