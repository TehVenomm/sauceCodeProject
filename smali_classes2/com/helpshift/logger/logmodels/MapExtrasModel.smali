.class Lcom/helpshift/logger/logmodels/MapExtrasModel;
.super Ljava/lang/Object;
.source "MapExtrasModel.java"

# interfaces
.implements Lcom/helpshift/logger/logmodels/ILogExtrasModel;


# instance fields
.field private key:Ljava/lang/String;

.field private value:Ljava/util/Map;


# direct methods
.method constructor <init>(Ljava/lang/String;Ljava/util/Map;)V
    .locals 0

    .line 18
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 19
    iput-object p1, p0, Lcom/helpshift/logger/logmodels/MapExtrasModel;->key:Ljava/lang/String;

    .line 20
    iput-object p2, p0, Lcom/helpshift/logger/logmodels/MapExtrasModel;->value:Ljava/util/Map;

    return-void
.end method


# virtual methods
.method public getConsoleLoggingMessage()Ljava/lang/String;
    .locals 3

    .line 25
    iget-object v0, p0, Lcom/helpshift/logger/logmodels/MapExtrasModel;->value:Ljava/util/Map;

    if-nez v0, :cond_0

    .line 26
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/logger/logmodels/MapExtrasModel;->key:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " : "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/logger/logmodels/MapExtrasModel;->value:Ljava/util/Map;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0

    .line 29
    :cond_0
    new-instance v0, Lorg/json/JSONObject;

    iget-object v1, p0, Lcom/helpshift/logger/logmodels/MapExtrasModel;->value:Ljava/util/Map;

    invoke-direct {v0, v1}, Lorg/json/JSONObject;-><init>(Ljava/util/Map;)V

    .line 31
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v2, p0, Lcom/helpshift/logger/logmodels/MapExtrasModel;->key:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public toJSONObject()Ljava/lang/Object;
    .locals 3

    .line 37
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 40
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/logger/logmodels/MapExtrasModel;->key:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/logger/logmodels/MapExtrasModel;->value:Ljava/util/Map;

    if-nez v2, :cond_0

    const-string v2, ""

    goto :goto_0

    :cond_0
    iget-object v2, p0, Lcom/helpshift/logger/logmodels/MapExtrasModel;->value:Ljava/util/Map;

    invoke-virtual {v2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    :goto_0
    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    return-object v0
.end method
