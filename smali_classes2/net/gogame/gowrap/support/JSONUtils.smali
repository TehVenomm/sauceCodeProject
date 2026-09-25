.class public final Lnet/gogame/gowrap/support/JSONUtils;
.super Ljava/lang/Object;
.source "JSONUtils.java"


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 21
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static assetRead(Landroid/content/Context;Ljava/lang/String;)Lorg/json/JSONObject;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lorg/json/JSONException;
        }
    .end annotation

    .line 39
    invoke-static {p0, p1}, Lnet/gogame/gowrap/io/utils/IOUtils;->newInputStream(Landroid/content/Context;Ljava/lang/String;)Ljava/io/InputStream;

    move-result-object p0

    .line 41
    :try_start_0
    invoke-static {p0}, Lnet/gogame/gowrap/support/JSONUtils;->read(Ljava/io/InputStream;)Lorg/json/JSONObject;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 43
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    return-object p1

    :catchall_0
    move-exception p1

    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 44
    throw p1
.end method

.method public static closeQuietly(Landroid/util/JsonReader;)V
    .locals 0

    if-nez p0, :cond_0

    return-void

    .line 136
    :cond_0
    :try_start_0
    invoke-virtual {p0}, Landroid/util/JsonReader;->close()V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    return-void
.end method

.method public static optBoolean(Lorg/json/JSONObject;Ljava/lang/String;)Ljava/lang/Boolean;
    .locals 1

    const/4 v0, 0x0

    .line 63
    invoke-static {p0, p1, v0}, Lnet/gogame/gowrap/support/JSONUtils;->optBoolean(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/Boolean;)Ljava/lang/Boolean;

    move-result-object p0

    return-object p0
.end method

.method public static optBoolean(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/Boolean;)Ljava/lang/Boolean;
    .locals 1

    .line 67
    invoke-virtual {p0, p1}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    return-object p2

    :cond_0
    if-eqz p2, :cond_1

    .line 71
    invoke-virtual {p2}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p2

    invoke-virtual {p0, p1, p2}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result p0

    invoke-static {p0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p0

    return-object p0

    .line 73
    :cond_1
    invoke-virtual {p0, p1}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;)Z

    move-result p0

    invoke-static {p0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p0

    return-object p0
.end method

.method public static optInt(Landroid/util/JsonReader;)Ljava/lang/Integer;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 121
    invoke-virtual {p0}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->NUMBER:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_0

    .line 122
    invoke-virtual {p0}, Landroid/util/JsonReader;->nextInt()I

    move-result p0

    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p0

    return-object p0

    .line 123
    :cond_0
    invoke-virtual {p0}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->NULL:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_1

    .line 124
    invoke-virtual {p0}, Landroid/util/JsonReader;->nextNull()V

    const/4 p0, 0x0

    return-object p0

    .line 127
    :cond_1
    new-instance p0, Ljava/lang/IllegalArgumentException;

    const-string v0, "int or null expected"

    invoke-direct {p0, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p0
.end method

.method public static optLong(Landroid/util/JsonReader;)Ljava/lang/Long;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 110
    invoke-virtual {p0}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->NUMBER:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_0

    .line 111
    invoke-virtual {p0}, Landroid/util/JsonReader;->nextLong()J

    move-result-wide v0

    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p0

    return-object p0

    .line 112
    :cond_0
    invoke-virtual {p0}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->NULL:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_1

    .line 113
    invoke-virtual {p0}, Landroid/util/JsonReader;->nextNull()V

    const/4 p0, 0x0

    return-object p0

    .line 116
    :cond_1
    new-instance p0, Ljava/lang/IllegalArgumentException;

    const-string v0, "long or null expected"

    invoke-direct {p0, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p0
.end method

.method public static optString(Landroid/util/JsonReader;)Ljava/lang/String;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 99
    invoke-virtual {p0}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->STRING:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_0

    .line 100
    invoke-virtual {p0}, Landroid/util/JsonReader;->nextString()Ljava/lang/String;

    move-result-object p0

    return-object p0

    .line 101
    :cond_0
    invoke-virtual {p0}, Landroid/util/JsonReader;->peek()Landroid/util/JsonToken;

    move-result-object v0

    sget-object v1, Landroid/util/JsonToken;->NULL:Landroid/util/JsonToken;

    if-ne v0, v1, :cond_1

    .line 102
    invoke-virtual {p0}, Landroid/util/JsonReader;->nextNull()V

    const/4 p0, 0x0

    return-object p0

    .line 105
    :cond_1
    new-instance p0, Ljava/lang/IllegalArgumentException;

    const-string v0, "string or null expected"

    invoke-direct {p0, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p0
.end method

.method public static optString(Lorg/json/JSONObject;Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    const/4 v0, 0x0

    .line 48
    invoke-static {p0, p1, v0}, Lnet/gogame/gowrap/support/JSONUtils;->optString(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static optString(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    .line 52
    invoke-virtual {p0, p1}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    return-object p2

    :cond_0
    if-eqz p2, :cond_1

    .line 56
    invoke-virtual {p0, p1, p2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0

    .line 58
    :cond_1
    invoke-virtual {p0, p1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static optUrl(Lorg/json/JSONObject;Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    const/4 v0, 0x0

    .line 78
    invoke-static {p0, p1, v0}, Lnet/gogame/gowrap/support/JSONUtils;->optUrl(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static optUrl(Lorg/json/JSONObject;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    .line 82
    invoke-virtual {p0, p1}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    return-object p2

    .line 85
    :cond_0
    invoke-virtual {p0, p1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    if-nez p0, :cond_1

    return-object p2

    .line 89
    :cond_1
    invoke-static {p0}, Lnet/gogame/gowrap/support/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    .line 91
    :try_start_0
    new-instance p1, Ljava/net/URL;

    invoke-direct {p1, p0}, Ljava/net/URL;-><init>(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/net/MalformedURLException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    return-object p2
.end method

.method public static read(Ljava/io/File;)Lorg/json/JSONObject;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lorg/json/JSONException;
        }
    .end annotation

    .line 29
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->newInputStream(Ljava/io/File;)Ljava/io/InputStream;

    move-result-object p0

    .line 31
    :try_start_0
    invoke-static {p0}, Lnet/gogame/gowrap/support/JSONUtils;->read(Ljava/io/InputStream;)Lorg/json/JSONObject;

    move-result-object v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 33
    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    return-object v0

    :catchall_0
    move-exception v0

    invoke-static {p0}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 34
    throw v0
.end method

.method public static read(Ljava/io/InputStream;)Lorg/json/JSONObject;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Lorg/json/JSONException;
        }
    .end annotation

    .line 25
    new-instance v0, Lorg/json/JSONObject;

    const-string v1, "UTF-8"

    invoke-static {p0, v1}, Lnet/gogame/gowrap/io/utils/IOUtils;->toString(Ljava/io/InputStream;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, p0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    return-object v0
.end method
