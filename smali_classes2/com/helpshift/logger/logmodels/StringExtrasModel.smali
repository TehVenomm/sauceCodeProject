.class Lcom/helpshift/logger/logmodels/StringExtrasModel;
.super Ljava/lang/Object;
.source "StringExtrasModel.java"

# interfaces
.implements Lcom/helpshift/logger/logmodels/ILogExtrasModel;


# instance fields
.field private key:Ljava/lang/String;

.field private value:Ljava/lang/String;


# direct methods
.method constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 14
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 15
    iput-object p1, p0, Lcom/helpshift/logger/logmodels/StringExtrasModel;->key:Ljava/lang/String;

    .line 16
    iput-object p2, p0, Lcom/helpshift/logger/logmodels/StringExtrasModel;->value:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getConsoleLoggingMessage()Ljava/lang/String;
    .locals 2

    .line 21
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/helpshift/logger/logmodels/StringExtrasModel;->key:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " : "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/logger/logmodels/StringExtrasModel;->value:Ljava/lang/String;

    if-nez v1, :cond_0

    const-string v1, ""

    goto :goto_0

    :cond_0
    iget-object v1, p0, Lcom/helpshift/logger/logmodels/StringExtrasModel;->value:Ljava/lang/String;

    :goto_0
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public toJSONObject()Ljava/lang/Object;
    .locals 3

    .line 27
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 30
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/logger/logmodels/StringExtrasModel;->key:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/logger/logmodels/StringExtrasModel;->value:Ljava/lang/String;

    if-nez v2, :cond_0

    const-string v2, ""

    goto :goto_0

    :cond_0
    iget-object v2, p0, Lcom/helpshift/logger/logmodels/StringExtrasModel;->value:Ljava/lang/String;

    :goto_0
    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    return-object v0
.end method
