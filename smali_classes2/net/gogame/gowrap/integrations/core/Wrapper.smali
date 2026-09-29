.class public Lnet/gogame/gowrap/integrations/core/Wrapper;
.super Ljava/lang/Object;
.source "Wrapper.java"


# static fields
.field public static final INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;


# instance fields
.field private configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

.field private serverStatus:Lnet/gogame/gowrap/integrations/core/ServerStatus;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 34
    new-instance v0, Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-direct {v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;-><init>()V

    sput-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    .line 32
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 36
    iput-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->serverStatus:Lnet/gogame/gowrap/integrations/core/ServerStatus;

    return-void
.end method

.method static synthetic access$002(Lnet/gogame/gowrap/integrations/core/Wrapper;Lnet/gogame/gowrap/integrations/core/ServerStatus;)Lnet/gogame/gowrap/integrations/core/ServerStatus;
    .locals 0

    .line 32
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->serverStatus:Lnet/gogame/gowrap/integrations/core/ServerStatus;

    return-object p1
.end method

.method static synthetic access$100(Lnet/gogame/gowrap/integrations/core/Wrapper;Lorg/json/JSONObject;)Lnet/gogame/gowrap/integrations/core/ServerStatus;
    .locals 0

    .line 32
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/integrations/core/Wrapper;->parseServerStatus(Lorg/json/JSONObject;)Lnet/gogame/gowrap/integrations/core/ServerStatus;

    move-result-object p0

    return-object p0
.end method

.method private parseServerStatus(Lorg/json/JSONObject;)Lnet/gogame/gowrap/integrations/core/ServerStatus;
    .locals 9

    .line 201
    new-instance v0, Lnet/gogame/gowrap/integrations/core/ServerStatus;

    invoke-direct {v0}, Lnet/gogame/gowrap/integrations/core/ServerStatus;-><init>()V

    const-string v1, "status"

    const/4 v2, 0x0

    .line 202
    invoke-virtual {p1, v1, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 204
    invoke-static {v1}, Lnet/gogame/gowrap/integrations/core/ServerStatus$Status;->valueOf(Ljava/lang/String;)Lnet/gogame/gowrap/integrations/core/ServerStatus$Status;

    move-result-object v1

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/integrations/core/ServerStatus;->setStatus(Lnet/gogame/gowrap/integrations/core/ServerStatus$Status;)V

    .line 206
    :cond_0
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/integrations/core/ServerStatus;->setLocales(Ljava/util/Map;)V

    const-string v1, "locales"

    .line 207
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p1

    if-eqz p1, :cond_3

    .line 209
    invoke-virtual {p1}, Lorg/json/JSONObject;->keys()Ljava/util/Iterator;

    move-result-object v1

    .line 210
    :cond_1
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_3

    .line 211
    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    .line 212
    invoke-virtual {p1, v3}, Lorg/json/JSONObject;->optJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v4

    if-eqz v4, :cond_1

    .line 214
    new-instance v5, Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;

    invoke-direct {v5}, Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;-><init>()V

    .line 215
    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/ServerStatus;->getLocales()Ljava/util/Map;

    move-result-object v6

    invoke-interface {v6, v3, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v3, "title"

    .line 216
    invoke-virtual {v4, v3, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v5, v3}, Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;->setTitle(Ljava/lang/String;)V

    const-string v3, "message"

    .line 217
    invoke-virtual {v4, v3, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v5, v3}, Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;->setMessage(Ljava/lang/String;)V

    const-string v3, "url"

    .line 218
    invoke-virtual {v4, v3, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v5, v3}, Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;->setUrl(Ljava/lang/String;)V

    .line 219
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3}, Ljava/util/ArrayList;-><init>()V

    invoke-virtual {v5, v3}, Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;->setFaq(Ljava/util/List;)V

    const-string v3, "faq"

    .line 220
    invoke-virtual {v4, v3}, Lorg/json/JSONObject;->optJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v3

    if-eqz v3, :cond_1

    const/4 v4, 0x0

    .line 222
    :goto_0
    invoke-virtual {v3}, Lorg/json/JSONArray;->length()I

    move-result v6

    if-ge v4, v6, :cond_1

    .line 223
    invoke-virtual {v3, v4}, Lorg/json/JSONArray;->optJSONObject(I)Lorg/json/JSONObject;

    move-result-object v6

    if-eqz v6, :cond_2

    .line 225
    new-instance v7, Lnet/gogame/gowrap/integrations/core/ServerStatus$StatusFaqEntry;

    invoke-direct {v7}, Lnet/gogame/gowrap/integrations/core/ServerStatus$StatusFaqEntry;-><init>()V

    const-string v8, "question"

    .line 226
    invoke-virtual {v6, v8, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v7, v8}, Lnet/gogame/gowrap/integrations/core/ServerStatus$StatusFaqEntry;->setQuestion(Ljava/lang/String;)V

    const-string v8, "answer"

    .line 227
    invoke-virtual {v6, v8, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v7, v6}, Lnet/gogame/gowrap/integrations/core/ServerStatus$StatusFaqEntry;->setAnswer(Ljava/lang/String;)V

    .line 228
    invoke-virtual {v5}, Lnet/gogame/gowrap/integrations/core/ServerStatus$LocalizedStatus;->getFaq()Ljava/util/List;

    move-result-object v6

    invoke-interface {v6, v7}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_2
    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    :cond_3
    return-object v0
.end method

.method private readJson(Landroid/content/Context;Ljava/lang/String;)Lorg/json/JSONObject;
    .locals 6

    .line 266
    new-instance v0, Ljava/io/File;

    invoke-virtual {p1}, Landroid/content/Context;->getFilesDir()Ljava/io/File;

    move-result-object v1

    const-string v2, "net/gogame/gowrap/"

    invoke-direct {v0, v1, v2}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 267
    new-instance v1, Ljava/io/File;

    invoke-direct {v1, v0, p2}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 268
    invoke-virtual {v1}, Ljava/io/File;->exists()Z

    move-result v0

    const/4 v2, 0x0

    const/4 v3, 0x1

    if-eqz v0, :cond_0

    invoke-virtual {v1}, Ljava/io/File;->isFile()Z

    move-result v0

    if-eqz v0, :cond_0

    :try_start_0
    const-string v0, "goWrap"

    const-string v4, "Reading %s from internal storage"

    .line 270
    new-array v5, v3, [Ljava/lang/Object;

    aput-object p2, v5, v2

    invoke-static {v4, v5}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v4

    invoke-static {v0, v4}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 271
    invoke-static {v1}, Lnet/gogame/gowrap/support/JSONUtils;->read(Ljava/io/File;)Lorg/json/JSONObject;

    move-result-object v0
    :try_end_0
    .catch Ljava/io/FileNotFoundException; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v4, "JSON exception"

    .line 277
    invoke-static {v1, v4, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :catch_1
    move-exception v0

    const-string v1, "goWrap"

    const-string v4, "I/O exception"

    .line 275
    invoke-static {v1, v4, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_0

    :catch_2
    move-exception v0

    const-string v1, "goWrap"

    .line 273
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "File not found in internal storage:"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/io/FileNotFoundException;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v4, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :cond_0
    :goto_0
    :try_start_1
    const-string v0, "goWrap"

    const-string v1, "Reading %s from assets"

    .line 281
    new-array v3, v3, [Ljava/lang/Object;

    aput-object p2, v3, v2

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 282
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "net/gogame/gowrap/"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Lnet/gogame/gowrap/support/JSONUtils;->assetRead(Landroid/content/Context;Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p1
    :try_end_1
    .catch Ljava/io/FileNotFoundException; {:try_start_1 .. :try_end_1} :catch_5
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_4
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_3

    return-object p1

    :catch_3
    move-exception p1

    const-string p2, "goWrap"

    const-string v0, "JSON exception"

    .line 288
    invoke-static {p2, v0, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_1

    :catch_4
    move-exception p1

    const-string p2, "goWrap"

    const-string v0, "I/O exception"

    .line 286
    invoke-static {p2, v0, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_1

    :catch_5
    move-exception p1

    const-string p2, "goWrap"

    .line 284
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "File not found in assets:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/io/FileNotFoundException;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p2, p1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :goto_1
    const/4 p1, 0x0

    return-object p1
.end method

.method private toBoolean(Ljava/lang/Boolean;)Z
    .locals 1

    const/4 v0, 0x0

    .line 43
    invoke-direct {p0, p1, v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->toBoolean(Ljava/lang/Boolean;Z)Z

    move-result p1

    return p1
.end method

.method private toBoolean(Ljava/lang/Boolean;Z)Z
    .locals 0

    if-nez p1, :cond_0

    return p2

    .line 50
    :cond_0
    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    return p1
.end method


# virtual methods
.method public getConfiguration()Lnet/gogame/gowrap/model/configuration/Configuration;
    .locals 1

    .line 39
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    return-object v0
.end method

.method public getCurrentLocale(Landroid/content/Context;)Ljava/lang/String;
    .locals 1

    const-string v0, "Language"

    .line 115
    invoke-static {p1, v0}, Lnet/gogame/gowrap/support/PreferenceUtils;->getPreference(Landroid/content/Context;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    if-nez p1, :cond_0

    const-string p1, "default"

    return-object p1

    :cond_0
    return-object p1
.end method

.method public getLocaleConfiguration(Landroid/content/Context;)Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;
    .locals 0

    .line 55
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getCurrentLocale(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getLocaleConfiguration(Ljava/lang/String;)Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;

    move-result-object p1

    return-object p1
.end method

.method public getLocaleConfiguration(Ljava/lang/String;)Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;
    .locals 1

    .line 61
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 62
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 63
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    move-result-object v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 64
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->getLocales()Ljava/util/Map;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 65
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->getLocales()Ljava/util/Map;

    move-result-object v0

    .line 66
    invoke-interface {v0, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;

    if-nez p1, :cond_1

    .line 68
    iget-object p1, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    invoke-virtual {p1}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object p1

    invoke-virtual {p1}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    move-result-object p1

    invoke-virtual {p1}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->getLocales()Ljava/util/Map;

    move-result-object p1

    const-string v0, "default"

    .line 69
    invoke-interface {p1, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :cond_1
    :goto_0
    return-object p1
.end method

.method public getServerStatus()Lnet/gogame/gowrap/integrations/core/ServerStatus;
    .locals 1

    .line 111
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->serverStatus:Lnet/gogame/gowrap/integrations/core/ServerStatus;

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

    .line 96
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 97
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    if-eqz v0, :cond_1

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 98
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    move-result-object v0

    if-eqz v0, :cond_1

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 99
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->getSupportedLocales()Ljava/util/List;

    move-result-object v0

    if-nez v0, :cond_0

    goto :goto_0

    .line 102
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->getSupportedLocales()Ljava/util/List;

    move-result-object v0

    return-object v0

    :cond_1
    :goto_0
    const/4 v0, 0x0

    return-object v0
.end method

.method public isChatBotEnabled()Z
    .locals 1

    .line 90
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 91
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getSettings()Lnet/gogame/gowrap/model/configuration/Configuration$Settings;

    move-result-object v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 92
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getSettings()Lnet/gogame/gowrap/model/configuration/Configuration$Settings;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Settings;->getChatBotEnabled()Ljava/lang/Boolean;

    move-result-object v0

    invoke-direct {p0, v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->toBoolean(Ljava/lang/Boolean;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isServerDown()Z
    .locals 2

    .line 107
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->serverStatus:Lnet/gogame/gowrap/integrations/core/ServerStatus;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->serverStatus:Lnet/gogame/gowrap/integrations/core/ServerStatus;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/ServerStatus;->getStatus()Lnet/gogame/gowrap/integrations/core/ServerStatus$Status;

    move-result-object v0

    sget-object v1, Lnet/gogame/gowrap/integrations/core/ServerStatus$Status;->OK:Lnet/gogame/gowrap/integrations/core/ServerStatus$Status;

    if-eq v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isSlideIn()Z
    .locals 1

    .line 83
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 84
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 85
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    move-result-object v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 86
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->getSlideIn()Ljava/lang/Boolean;

    move-result-object v0

    invoke-direct {p0, v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->toBoolean(Ljava/lang/Boolean;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isSlideOut()Z
    .locals 1

    .line 76
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 77
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 78
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    move-result-object v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 79
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->getSlideOut()Ljava/lang/Boolean;

    move-result-object v0

    invoke-direct {p0, v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->toBoolean(Ljava/lang/Boolean;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public readConfiguration(Landroid/content/Context;)V
    .locals 4

    :try_start_0
    const-string v0, "config.json.gz"

    .line 240
    invoke-direct {p0, p1, v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->readJson(Landroid/content/Context;Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    .line 241
    new-instance v1, Lnet/gogame/gowrap/model/configuration/Configuration;

    invoke-direct {v1, v0}, Lnet/gogame/gowrap/model/configuration/Configuration;-><init>(Lorg/json/JSONObject;)V

    iput-object v1, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 243
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    if-eqz v0, :cond_2

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 244
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    if-eqz v0, :cond_2

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 245
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    move-result-object v0

    if-eqz v0, :cond_2

    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    .line 246
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->getSupportedLocales()Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 247
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    sget v0, Lnet/gogame/gowrap/R$array;->language_values:I

    .line 248
    invoke-virtual {p1, v0}, Landroid/content/res/Resources;->getStringArray(I)[Ljava/lang/String;

    move-result-object p1

    .line 247
    invoke-static {p1}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object p1

    .line 249
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const-string v1, "default"

    .line 250
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 252
    iget-object v1, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    invoke-virtual {v1}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object v1

    invoke-virtual {v1}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    move-result-object v1

    invoke-virtual {v1}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->getSupportedLocales()Ljava/util/List;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_0
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 253
    invoke-interface {p1, v2}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_0

    .line 254
    invoke-interface {v0, v2}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v3

    if-nez v3, :cond_0

    .line 255
    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 258
    :cond_1
    iget-object p1, p0, Lnet/gogame/gowrap/integrations/core/Wrapper;->configuration:Lnet/gogame/gowrap/model/configuration/Configuration;

    invoke-virtual {p1}, Lnet/gogame/gowrap/model/configuration/Configuration;->getIntegrations()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;

    move-result-object p1

    invoke-virtual {p1}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations;->getCore()Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;

    move-result-object p1

    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core;->setSupportedLocales(Ljava/util/List;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception p1

    const-string v0, "goWrap"

    const-string v1, "Exception"

    .line 261
    invoke-static {v0, v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_2
    :goto_1
    return-void
.end method

.method public setCurrentLocale(Landroid/content/Context;Ljava/lang/String;)V
    .locals 1

    const-string v0, "Language"

    .line 123
    invoke-static {p1, v0, p2}, Lnet/gogame/gowrap/support/PreferenceUtils;->setPreference(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public setup(Landroid/content/Context;)V
    .locals 10

    .line 127
    sget-object v0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_0

    const-string v0, "goWrap"

    const-string v1, "App ID not set"

    .line 128
    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 131
    :cond_0
    new-instance v0, Ljava/io/File;

    invoke-virtual {p1}, Landroid/content/Context;->getFilesDir()Ljava/io/File;

    move-result-object v1

    const-string v2, "net/gogame/gowrap/"

    invoke-direct {v0, v1, v2}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 132
    invoke-virtual {v0}, Ljava/io/File;->mkdirs()Z

    .line 135
    new-instance v1, Ljava/io/File;

    const-string v2, "config.json.gz"

    invoke-direct {v1, v0, v2}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 136
    sget-object v2, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {v2}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_2

    invoke-virtual {v1}, Ljava/io/File;->exists()Z

    move-result v2

    if-nez v2, :cond_1

    goto :goto_0

    :cond_1
    const-string v2, "goWrap"

    const-string v3, "config.json.gz already exists in internal storage"

    .line 148
    invoke-static {v2, v3}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_1

    :cond_2
    :goto_0
    :try_start_0
    const-string v2, "net/gogame/gowrap/config.json"

    .line 140
    invoke-static {p1, v2, v1}, Lnet/gogame/gowrap/io/utils/FileUtils;->gzipCopyFromAsset(Landroid/content/Context;Ljava/lang/String;Ljava/io/File;)V

    const-string v2, "goWrap"

    const-string v3, "Initialized config.json.gz"

    .line 141
    invoke-static {v2, v3}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_0
    .catch Ljava/io/FileNotFoundException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v2

    const-string v3, "goWrap"

    const-string v4, "Could not copy pre-packaged config file"

    .line 145
    invoke-static {v3, v4, v2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_1

    :catch_1
    const-string v2, "goWrap"

    const-string v3, "Could not copy pre-packaged config file (not found): net/gogame/gowrap/config.json"

    .line 143
    invoke-static {v2, v3}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 151
    :goto_1
    sget-object v2, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {v2}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object v2

    const/4 v3, 0x2

    const/4 v4, 0x3

    const/4 v5, 0x1

    const/4 v6, 0x0

    if-eqz v2, :cond_3

    .line 153
    :try_start_1
    new-instance v2, Ljava/net/URL;

    const-string v7, "%s/config/%s/%s"

    new-array v8, v4, [Ljava/lang/Object;

    const-string v9, "http://gw-content.gogame.net"

    aput-object v9, v8, v6

    sget-object v9, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    .line 154
    invoke-virtual {v9}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object v9

    aput-object v9, v8, v5

    const-string v9, "config.json.gz"

    aput-object v9, v8, v3

    .line 153
    invoke-static {v7, v8}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v7

    invoke-direct {v2, v7}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    .line 156
    new-instance v7, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;

    invoke-direct {v7, v1}, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;-><init>(Ljava/io/File;)V

    const/4 v1, 0x0

    invoke-static {p1, v2, v7, v5, v1}, Lnet/gogame/gowrap/support/DownloadUtils;->download(Landroid/content/Context;Ljava/net/URL;Lnet/gogame/gowrap/support/DownloadUtils$Target;ZLnet/gogame/gowrap/support/DownloadUtils$Callback;)V
    :try_end_1
    .catch Ljava/net/MalformedURLException; {:try_start_1 .. :try_end_1} :catch_2

    .line 163
    :catch_2
    :cond_3
    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/integrations/core/Wrapper;->readConfiguration(Landroid/content/Context;)V

    .line 166
    sget-object v1, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {v1}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_4

    .line 168
    :try_start_2
    new-instance v1, Ljava/io/File;

    const-string v2, "status.json.gz"

    invoke-direct {v1, v0, v2}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 169
    new-instance v0, Ljava/net/URL;

    const-string v2, "%s/status/%s/%s"

    new-array v4, v4, [Ljava/lang/Object;

    const-string v7, "http://gw-content.gogame.net"

    aput-object v7, v4, v6

    sget-object v7, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    .line 170
    invoke-virtual {v7}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v4, v5

    const-string v5, "status.json.gz"

    aput-object v5, v4, v3

    .line 169
    invoke-static {v2, v4}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    invoke-direct {v0, v2}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    .line 172
    new-instance v2, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;

    invoke-direct {v2, v1}, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;-><init>(Ljava/io/File;)V

    new-instance v3, Lnet/gogame/gowrap/integrations/core/Wrapper$1;

    invoke-direct {v3, p0, v1, p1}, Lnet/gogame/gowrap/integrations/core/Wrapper$1;-><init>(Lnet/gogame/gowrap/integrations/core/Wrapper;Ljava/io/File;Landroid/content/Context;)V

    invoke-static {p1, v0, v2, v6, v3}, Lnet/gogame/gowrap/support/DownloadUtils;->download(Landroid/content/Context;Ljava/net/URL;Lnet/gogame/gowrap/support/DownloadUtils$Target;ZLnet/gogame/gowrap/support/DownloadUtils$Callback;)V
    :try_end_2
    .catch Ljava/net/MalformedURLException; {:try_start_2 .. :try_end_2} :catch_3

    :catch_3
    :cond_4
    return-void
.end method
