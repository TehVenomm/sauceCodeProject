.class public Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;
.super Ljava/lang/Object;
.source "Configuration.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/model/configuration/Configuration;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Integrations"
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;
    }
.end annotation


# instance fields
.field private core:Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 64
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

    .line 68
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "core"

    .line 70
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "core"

    .line 71
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 73
    new-instance v0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    invoke-direct {v0, p1}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;-><init>(Lorg/json/JSONObject;)V

    iput-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->core:Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    :cond_0
    return-void
.end method


# virtual methods
.method public getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;
    .locals 1

    .line 79
    iget-object v0, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->core:Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    return-object v0
.end method

.method public setCore(Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;)V
    .locals 0

    .line 83
    iput-object p1, p0, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->core:Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    return-void
.end method
