.class final Lcom/appsflyer/s;
.super Lcom/appsflyer/OneLinkHttpTask;
.source ""


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/appsflyer/s$b;
    }
.end annotation


# instance fields
.field private ˋ:Lcom/appsflyer/s$b;

.field private ॱ:Ljava/lang/String;


# direct methods
.method constructor <init>(Landroid/net/Uri;Lcom/appsflyer/AppsFlyerLib;)V
    .locals 1

    .line 30
    invoke-direct {p0, p2}, Lcom/appsflyer/OneLinkHttpTask;-><init>(Lcom/appsflyer/AppsFlyerLib;)V

    .line 31
    invoke-virtual {p1}, Landroid/net/Uri;->getHost()Ljava/lang/String;

    move-result-object p2

    invoke-static {p2}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result p2

    if-nez p2, :cond_0

    invoke-virtual {p1}, Landroid/net/Uri;->getPath()Ljava/lang/String;

    move-result-object p2

    invoke-static {p2}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result p2

    if-nez p2, :cond_0

    .line 32
    invoke-virtual {p1}, Landroid/net/Uri;->getPath()Ljava/lang/String;

    move-result-object p2

    const-string v0, "/"

    invoke-virtual {p2, v0}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p2

    .line 33
    invoke-virtual {p1}, Landroid/net/Uri;->getHost()Ljava/lang/String;

    move-result-object p1

    const-string v0, "onelink.me"

    invoke-virtual {p1, v0}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result p1

    if-eqz p1, :cond_0

    array-length p1, p2

    const/4 v0, 0x3

    if-ne p1, v0, :cond_0

    const/4 p1, 0x1

    .line 34
    aget-object p1, p2, p1

    iput-object p1, p0, Lcom/appsflyer/OneLinkHttpTask;->ˊ:Ljava/lang/String;

    const/4 p1, 0x2

    .line 35
    aget-object p1, p2, p1

    iput-object p1, p0, Lcom/appsflyer/s;->ॱ:Ljava/lang/String;

    :cond_0
    return-void
.end method


# virtual methods
.method final ˊ()Ljava/lang/String;
    .locals 2

    .line 55
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "https://onelink.%s/shortlink-sdk/v1"

    invoke-static {v1}, Lcom/appsflyer/ServerConfigHandler;->getUrl(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "/"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/appsflyer/OneLinkHttpTask;->ˊ:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "?id="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/appsflyer/s;->ॱ:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method final ˊ(Ljava/lang/String;)V
    .locals 5

    .line 61
    :try_start_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 62
    new-instance v1, Lorg/json/JSONObject;

    invoke-direct {v1, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 63
    invoke-virtual {v1}, Lorg/json/JSONObject;->keys()Ljava/util/Iterator;

    move-result-object v2

    .line 64
    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_0

    .line 65
    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    .line 66
    invoke-virtual {v1, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-interface {v0, v3, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 68
    :cond_0
    iget-object v1, p0, Lcom/appsflyer/s;->ˋ:Lcom/appsflyer/s$b;

    invoke-interface {v1, v0}, Lcom/appsflyer/s$b;->ॱ(Ljava/util/Map;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception v0

    .line 70
    iget-object v1, p0, Lcom/appsflyer/s;->ˋ:Lcom/appsflyer/s$b;

    const-string v2, "Can\'t parse one link data"

    invoke-interface {v1, v2}, Lcom/appsflyer/s$b;->ॱ(Ljava/lang/String;)V

    const-string v1, "Error while parsing to json "

    .line 71
    invoke-static {p1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-static {p1, v0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    return-void
.end method

.method final ˎ()V
    .locals 2

    .line 77
    iget-object v0, p0, Lcom/appsflyer/s;->ˋ:Lcom/appsflyer/s$b;

    const-string v1, "Can\'t get one link data"

    invoke-interface {v0, v1}, Lcom/appsflyer/s$b;->ॱ(Ljava/lang/String;)V

    return-void
.end method

.method final ˏ(Lcom/appsflyer/s$b;)V
    .locals 0
    .param p1    # Lcom/appsflyer/s$b;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 41
    iput-object p1, p0, Lcom/appsflyer/s;->ˋ:Lcom/appsflyer/s$b;

    return-void
.end method

.method final ˏ()Z
    .locals 1

    .line 45
    iget-object v0, p0, Lcom/appsflyer/OneLinkHttpTask;->ˊ:Ljava/lang/String;

    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/appsflyer/s;->ॱ:Ljava/lang/String;

    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method final ॱ(Ljavax/net/ssl/HttpsURLConnection;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;,
            Ljava/io/IOException;
        }
    .end annotation

    const-string v0, "GET"

    .line 50
    invoke-virtual {p1, v0}, Ljava/net/HttpURLConnection;->setRequestMethod(Ljava/lang/String;)V

    return-void
.end method
