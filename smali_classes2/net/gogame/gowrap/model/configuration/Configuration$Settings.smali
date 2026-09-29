.class public Lnet/gogame/gowrap/model/configuration/Configuration$Settings;
.super Ljava/lang/Object;
.source "Configuration.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/model/configuration/Configuration;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Settings"
.end annotation


# instance fields
.field private chatBotEnabled:Ljava/lang/Boolean;

.field private newsWidgetVersion:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 253
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public constructor <init>(Lorg/json/JSONObject;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 257
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "chatBotEnabled"

    .line 259
    invoke-static {p1, v0}, Lnet/gogame/gowrap/support/JSONUtils;->optBoolean(Lorg/json/JSONObject;Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Settings;->chatBotEnabled:Ljava/lang/Boolean;

    const-string v0, "newsWidgetVersion"

    .line 260
    invoke-static {p1, v0}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Lorg/json/JSONObject;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Settings;->newsWidgetVersion:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getChatBotEnabled()Ljava/lang/Boolean;
    .locals 1

    .line 264
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Settings;->chatBotEnabled:Ljava/lang/Boolean;

    return-object v0
.end method

.method public getNewsWidgetVersion()Ljava/lang/String;
    .locals 1

    .line 272
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Settings;->newsWidgetVersion:Ljava/lang/String;

    return-object v0
.end method

.method public setChatBotEnabled(Ljava/lang/Boolean;)V
    .locals 0

    .line 268
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Settings;->chatBotEnabled:Ljava/lang/Boolean;

    return-void
.end method

.method public setNewsWidgetVersion(Ljava/lang/String;)V
    .locals 0

    .line 276
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Settings;->newsWidgetVersion:Ljava/lang/String;

    return-void
.end method
