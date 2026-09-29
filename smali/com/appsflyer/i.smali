.class final Lcom/appsflyer/i;
.super Ljava/lang/Object;
.source ""

# interfaces
.implements Ljava/lang/Runnable;


# static fields
.field private static ʼ:Ljava/lang/String;


# instance fields
.field private ʻ:Ljava/util/concurrent/ScheduledExecutorService;

.field private ʽ:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private ˊ:Ljava/lang/String;

.field private final ˊॱ:Landroid/content/Intent;

.field private ˋ:Ljava/lang/String;

.field private ˎ:Ljava/lang/String;

.field private ˏ:Ljava/lang/String;

.field private ॱ:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Landroid/content/Context;",
            ">;"
        }
    .end annotation
.end field

.field private ॱॱ:Ljava/lang/String;

.field private ᐝ:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 30
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "https://validate.%s/api/v"

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    sget-object v1, Lcom/appsflyer/AppsFlyerLib;->ॱ:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "/androidevent?buildnumber=4.8.11&app_id="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/appsflyer/i;->ʼ:Ljava/lang/String;

    return-void
.end method

.method constructor <init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;Ljava/util/concurrent/ScheduledExecutorService;Landroid/content/Intent;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;",
            "Ljava/util/concurrent/ScheduledExecutorService;",
            "Landroid/content/Intent;",
            ")V"
        }
    .end annotation

    .line 42
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 25
    iput-object v0, p0, Lcom/appsflyer/i;->ॱ:Ljava/lang/ref/WeakReference;

    .line 43
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Lcom/appsflyer/i;->ॱ:Ljava/lang/ref/WeakReference;

    .line 44
    iput-object p2, p0, Lcom/appsflyer/i;->ˊ:Ljava/lang/String;

    .line 45
    iput-object p3, p0, Lcom/appsflyer/i;->ˋ:Ljava/lang/String;

    .line 46
    iput-object p5, p0, Lcom/appsflyer/i;->ˏ:Ljava/lang/String;

    .line 47
    iput-object p6, p0, Lcom/appsflyer/i;->ॱॱ:Ljava/lang/String;

    .line 48
    iput-object p7, p0, Lcom/appsflyer/i;->ᐝ:Ljava/lang/String;

    .line 49
    iput-object p8, p0, Lcom/appsflyer/i;->ʽ:Ljava/util/Map;

    .line 50
    iput-object p4, p0, Lcom/appsflyer/i;->ˎ:Ljava/lang/String;

    .line 51
    iput-object p9, p0, Lcom/appsflyer/i;->ʻ:Ljava/util/concurrent/ScheduledExecutorService;

    .line 52
    iput-object p10, p0, Lcom/appsflyer/i;->ˊॱ:Landroid/content/Intent;

    return-void
.end method

.method static synthetic ˎ(Lcom/appsflyer/i;)Ljava/util/Map;
    .locals 0

    .line 23
    iget-object p0, p0, Lcom/appsflyer/i;->ʽ:Ljava/util/Map;

    return-object p0
.end method

.method static synthetic ˏ(Lcom/appsflyer/i;)Ljava/lang/ref/WeakReference;
    .locals 0

    .line 23
    iget-object p0, p0, Lcom/appsflyer/i;->ॱ:Ljava/lang/ref/WeakReference;

    return-object p0
.end method

.method private static ॱ(Ljava/lang/String;Ljava/lang/String;)Ljava/net/HttpURLConnection;
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const/4 v0, 0x0

    .line 224
    :try_start_0
    new-instance v1, Lcom/appsflyer/l;

    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object v2

    invoke-virtual {v2}, Lcom/appsflyer/AppsFlyerLib;->isTrackingStopped()Z

    move-result v2

    invoke-direct {v1, v0, v2}, Lcom/appsflyer/l;-><init>(Landroid/content/Context;Z)V

    .line 225
    iput-object p0, v1, Lcom/appsflyer/l;->ˊ:Ljava/lang/String;

    .line 226
    invoke-virtual {v1}, Lcom/appsflyer/l;->ॱ()V

    .line 227
    invoke-static {}, Ljava/lang/Thread;->currentThread()Ljava/lang/Thread;

    move-result-object p0

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v2

    invoke-virtual {v2}, Landroid/os/Looper;->getThread()Ljava/lang/Thread;

    move-result-object v2

    const/4 v3, 0x0

    const/4 v4, 0x1

    if-ne p0, v2, :cond_0

    .line 228
    new-instance p0, Ljava/lang/StringBuilder;

    const-string v2, "Main thread detected. Calling "

    invoke-direct {p0, v2}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " in a new thread."

    invoke-virtual {p0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    .line 229
    new-array p0, v4, [Ljava/lang/String;

    aput-object p1, p0, v3

    invoke-virtual {v1, p0}, Landroid/os/AsyncTask;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    goto :goto_0

    .line 231
    :cond_0
    new-instance p0, Ljava/lang/StringBuilder;

    const-string v2, "Calling "

    invoke-direct {p0, v2}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " (on current thread: "

    invoke-virtual {p0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {}, Ljava/lang/Thread;->currentThread()Ljava/lang/Thread;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " )"

    invoke-virtual {p0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    .line 233
    invoke-virtual {v1}, Lcom/appsflyer/l;->onPreExecute()V

    .line 234
    new-array p0, v4, [Ljava/lang/String;

    aput-object p1, p0, v3

    invoke-virtual {v1, p0}, Lcom/appsflyer/l;->ˋ([Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v1, p0}, Lcom/appsflyer/l;->ॱ(Ljava/lang/String;)V

    .line 236
    :goto_0
    invoke-virtual {v1}, Lcom/appsflyer/l;->ˏ()Ljava/net/HttpURLConnection;

    move-result-object p0
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    const-string p1, "Could not send callStats request"

    .line 238
    invoke-static {p1, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    return-object v0
.end method

.method static synthetic ॱ(Lcom/appsflyer/i;Ljava/util/Map;Ljava/util/Map;Ljava/lang/ref/WeakReference;)V
    .locals 15

    move-object v0, p0

    .line 4138
    invoke-virtual/range {p3 .. p3}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v1

    if-eqz v1, :cond_7

    .line 4142
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    sget-object v2, Lcom/appsflyer/i;->ʼ:Ljava/lang/String;

    invoke-static {v2}, Lcom/appsflyer/ServerConfigHandler;->getUrl(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual/range {p3 .. p3}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/content/Context;

    invoke-virtual {v2}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    .line 4144
    invoke-virtual/range {p3 .. p3}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/content/Context;

    const-string v3, "appsflyer-data"

    const/4 v4, 0x0

    invoke-virtual {v2, v3, v4}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v12

    const-string v2, "referrer"

    const/4 v3, 0x0

    .line 4217
    invoke-interface {v12, v2, v3}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    if-nez v2, :cond_0

    const-string v2, ""

    :cond_0
    move-object v10, v2

    .line 5148
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object v5

    .line 5149
    invoke-virtual/range {p3 .. p3}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v2

    move-object v6, v2

    check-cast v6, Landroid/content/Context;

    iget-object v7, v0, Lcom/appsflyer/i;->ˊ:Ljava/lang/String;

    const-string v8, "af_purchase"

    const-string v9, ""

    const/4 v11, 0x1

    const/4 v13, 0x0

    iget-object v14, v0, Lcom/appsflyer/i;->ˊॱ:Landroid/content/Intent;

    .line 5148
    invoke-virtual/range {v5 .. v14}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZLandroid/content/SharedPreferences;ZLandroid/content/Intent;)Ljava/util/Map;

    move-result-object v2

    const-string v4, "price"

    .line 6211
    iget-object v5, v0, Lcom/appsflyer/i;->ॱॱ:Ljava/lang/String;

    invoke-interface {v2, v4, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v4, "currency"

    .line 6212
    iget-object v0, v0, Lcom/appsflyer/i;->ᐝ:Ljava/lang/String;

    invoke-interface {v2, v4, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 6154
    new-instance v4, Lorg/json/JSONObject;

    invoke-direct {v4, v2}, Lorg/json/JSONObject;-><init>(Ljava/util/Map;)V

    .line 6155
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 6158
    :try_start_0
    invoke-interface/range {p1 .. p1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v2

    invoke-interface {v2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_1

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/util/Map$Entry;

    .line 6159
    invoke-interface {v5}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    invoke-interface {v5}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v5

    invoke-virtual {v0, v6, v5}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    goto :goto_0

    :cond_1
    const-string v2, "receipt_data"

    .line 6162
    invoke-virtual {v4, v2, v0}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v0

    const-string v2, "Failed to build \'receipt_data\'"

    .line 6164
    invoke-static {v2, v0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_1
    if-eqz p2, :cond_3

    .line 6168
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    .line 6170
    :try_start_1
    invoke-interface/range {p2 .. p2}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v2

    invoke-interface {v2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_2
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_2

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/util/Map$Entry;

    .line 6171
    invoke-interface {v5}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    invoke-interface {v5}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v5

    invoke-virtual {v0, v6, v5}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    goto :goto_2

    :cond_2
    const-string v2, "extra_prms"

    .line 6174
    invoke-virtual {v4, v2, v0}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_3

    :catch_1
    move-exception v0

    const-string v2, "Failed to build \'extra_prms\'"

    .line 6176
    invoke-static {v2, v0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 6180
    :cond_3
    :goto_3
    invoke-virtual {v4}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v0

    .line 6181
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v2

    invoke-virtual {v2, v1, v0}, Lcom/appsflyer/y;->ˎ(Ljava/lang/String;Ljava/lang/String;)V

    .line 6185
    :try_start_2
    invoke-static {v0, v1}, Lcom/appsflyer/i;->ॱ(Ljava/lang/String;Ljava/lang/String;)Ljava/net/HttpURLConnection;

    move-result-object v2
    :try_end_2
    .catch Ljava/lang/Throwable; {:try_start_2 .. :try_end_2} :catch_3
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    const/4 v0, -0x1

    if-eqz v2, :cond_4

    .line 6189
    :try_start_3
    invoke-virtual {v2}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v0

    goto :goto_4

    :catchall_0
    move-exception v0

    goto :goto_7

    :catch_2
    move-exception v0

    move-object v3, v2

    goto :goto_6

    .line 6191
    :cond_4
    :goto_4
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    invoke-static {v2}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Ljava/net/HttpURLConnection;)Ljava/lang/String;

    move-result-object v3

    .line 6192
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v4

    invoke-virtual {v4, v1, v0, v3}, Lcom/appsflyer/y;->ˎ(Ljava/lang/String;ILjava/lang/String;)V

    .line 6194
    new-instance v1, Lorg/json/JSONObject;

    invoke-direct {v1, v3}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const/16 v3, 0xc8

    if-ne v0, v3, :cond_5

    .line 6197
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v3, "Validate-WH response - 200: "

    invoke-direct {v0, v3}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    goto :goto_5

    .line 6199
    :cond_5
    new-instance v3, Ljava/lang/StringBuilder;

    const-string v4, "Validate-WH response failed - "

    invoke-direct {v3, v4}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v0, ": "

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V
    :try_end_3
    .catch Ljava/lang/Throwable; {:try_start_3 .. :try_end_3} :catch_2
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    :goto_5
    if-eqz v2, :cond_7

    .line 6205
    invoke-virtual {v2}, Ljava/net/HttpURLConnection;->disconnect()V

    return-void

    :catchall_1
    move-exception v0

    move-object v2, v3

    goto :goto_7

    :catch_3
    move-exception v0

    .line 6202
    :goto_6
    :try_start_4
    invoke-virtual {v0}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1, v0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    if-eqz v3, :cond_7

    .line 6205
    invoke-virtual {v3}, Ljava/net/HttpURLConnection;->disconnect()V

    return-void

    :goto_7
    if-eqz v2, :cond_6

    invoke-virtual {v2}, Ljava/net/HttpURLConnection;->disconnect()V

    :cond_6
    throw v0

    :cond_7
    return-void
.end method

.method private static ॱ(ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(Z",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    .line 244
    sget-object v0, Lcom/appsflyer/AppsFlyerLib;->ˏ:Lcom/appsflyer/AppsFlyerInAppPurchaseValidatorListener;

    if-eqz v0, :cond_2

    .line 245
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "Validate callback parameters: "

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, " "

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, " "

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    if-eqz p0, :cond_0

    const-string p0, "Validate in app purchase success: "

    .line 247
    invoke-static {p4}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    .line 248
    sget-object p0, Lcom/appsflyer/AppsFlyerLib;->ˏ:Lcom/appsflyer/AppsFlyerInAppPurchaseValidatorListener;

    invoke-interface {p0}, Lcom/appsflyer/AppsFlyerInAppPurchaseValidatorListener;->onValidateInApp()V

    return-void

    :cond_0
    const-string p0, "Validate in app purchase failed: "

    .line 250
    invoke-static {p4}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    .line 251
    sget-object p0, Lcom/appsflyer/AppsFlyerLib;->ˏ:Lcom/appsflyer/AppsFlyerInAppPurchaseValidatorListener;

    if-nez p4, :cond_1

    const-string p4, "Failed validating"

    :cond_1
    invoke-interface {p0, p4}, Lcom/appsflyer/AppsFlyerInAppPurchaseValidatorListener;->onValidateInAppFailure(Ljava/lang/String;)V

    :cond_2
    return-void
.end method


# virtual methods
.method public final run()V
    .locals 10

    .line 57
    iget-object v0, p0, Lcom/appsflyer/i;->ˊ:Ljava/lang/String;

    if-eqz v0, :cond_9

    iget-object v0, p0, Lcom/appsflyer/i;->ˊ:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    if-nez v0, :cond_0

    goto/16 :goto_5

    .line 62
    :cond_0
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object v0

    invoke-virtual {v0}, Lcom/appsflyer/AppsFlyerLib;->isTrackingStopped()Z

    move-result v0

    if-eqz v0, :cond_1

    return-void

    :cond_1
    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 68
    :try_start_0
    iget-object v2, p0, Lcom/appsflyer/i;->ॱ:Ljava/lang/ref/WeakReference;

    invoke-virtual {v2}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/content/Context;

    if-nez v2, :cond_2

    return-void

    .line 72
    :cond_2
    new-instance v3, Ljava/util/HashMap;

    invoke-direct {v3}, Ljava/util/HashMap;-><init>()V

    const-string v4, "public-key"

    .line 73
    iget-object v5, p0, Lcom/appsflyer/i;->ˋ:Ljava/lang/String;

    invoke-interface {v3, v4, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v4, "sig-data"

    .line 74
    iget-object v5, p0, Lcom/appsflyer/i;->ˏ:Ljava/lang/String;

    invoke-interface {v3, v4, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v4, "signature"

    .line 75
    iget-object v5, p0, Lcom/appsflyer/i;->ˎ:Ljava/lang/String;

    invoke-interface {v3, v4, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 77
    new-instance v4, Ljava/util/HashMap;

    invoke-direct {v4}, Ljava/util/HashMap;-><init>()V

    .line 78
    invoke-interface {v4, v3}, Ljava/util/Map;->putAll(Ljava/util/Map;)V

    .line 80
    invoke-static {}, Ljava/util/concurrent/Executors;->newSingleThreadScheduledExecutor()Ljava/util/concurrent/ScheduledExecutorService;

    move-result-object v5

    .line 81
    new-instance v6, Lcom/appsflyer/i$1;

    invoke-direct {v6, p0, v4}, Lcom/appsflyer/i$1;-><init>(Lcom/appsflyer/i;Ljava/util/Map;)V

    const-wide/16 v7, 0x5

    sget-object v4, Ljava/util/concurrent/TimeUnit;->MILLISECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-interface {v5, v6, v7, v8, v4}, Ljava/util/concurrent/ScheduledExecutorService;->schedule(Ljava/lang/Runnable;JLjava/util/concurrent/TimeUnit;)Ljava/util/concurrent/ScheduledFuture;

    const-string v4, "dev_key"

    .line 88
    iget-object v5, p0, Lcom/appsflyer/i;->ˊ:Ljava/lang/String;

    invoke-interface {v3, v4, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v4, "app_id"

    .line 89
    invoke-virtual {v2}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-interface {v3, v4, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v4, "uid"

    .line 90
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object v5

    invoke-virtual {v5, v2}, Lcom/appsflyer/AppsFlyerLib;->getAppsFlyerUID(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v2

    invoke-interface {v3, v4, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "advertiserId"

    .line 91
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v4

    const-string v5, "advertiserId"

    invoke-virtual {v4, v5}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-interface {v3, v2, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 92
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2, v3}, Lorg/json/JSONObject;-><init>(Ljava/util/Map;)V

    .line 93
    invoke-virtual {v2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v2

    const-string v3, "https://sdk-services.%s/validate-android-signature"

    .line 94
    invoke-static {v3}, Lcom/appsflyer/ServerConfigHandler;->getUrl(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 95
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v4

    invoke-virtual {v4, v3, v2}, Lcom/appsflyer/y;->ˎ(Ljava/lang/String;Ljava/lang/String;)V

    .line 97
    invoke-static {v2, v3}, Lcom/appsflyer/i;->ॱ(Ljava/lang/String;Ljava/lang/String;)Ljava/net/HttpURLConnection;

    move-result-object v2
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    const/4 v0, -0x1

    if-eqz v2, :cond_3

    .line 101
    :try_start_1
    invoke-virtual {v2}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v0

    goto :goto_0

    :catchall_0
    move-exception v0

    move-object v1, v0

    move-object v0, v2

    goto/16 :goto_4

    :catch_0
    move-exception v0

    move-object v9, v2

    move-object v2, v0

    move-object v0, v9

    goto :goto_3

    .line 103
    :cond_3
    :goto_0
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    invoke-static {v2}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Ljava/net/HttpURLConnection;)Ljava/lang/String;

    move-result-object v4

    .line 104
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v5

    invoke-virtual {v5, v3, v0, v4}, Lcom/appsflyer/y;->ˎ(Ljava/lang/String;ILjava/lang/String;)V

    .line 106
    new-instance v3, Lorg/json/JSONObject;

    invoke-direct {v3, v4}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v4, "code"

    .line 107
    invoke-virtual {v3, v4, v0}, Lorg/json/JSONObject;->put(Ljava/lang/String;I)Lorg/json/JSONObject;

    const/16 v4, 0xc8

    if-ne v0, v4, :cond_5

    .line 109
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v4, "Validate response 200 ok: "

    invoke-direct {v0, v4}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v3}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    const-string v0, "result"

    .line 112
    invoke-virtual {v3, v0}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_4

    const-string v0, "result"

    .line 113
    invoke-virtual {v3, v0}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    goto :goto_1

    :cond_4
    const/4 v0, 0x0

    .line 115
    :goto_1
    iget-object v4, p0, Lcom/appsflyer/i;->ˏ:Ljava/lang/String;

    iget-object v5, p0, Lcom/appsflyer/i;->ॱॱ:Ljava/lang/String;

    iget-object v6, p0, Lcom/appsflyer/i;->ᐝ:Ljava/lang/String;

    invoke-virtual {v3}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v0, v4, v5, v6, v3}, Lcom/appsflyer/i;->ॱ(ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_2

    :cond_5
    const-string v0, "Failed Validate request"

    .line 118
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    .line 119
    iget-object v0, p0, Lcom/appsflyer/i;->ˏ:Ljava/lang/String;

    iget-object v4, p0, Lcom/appsflyer/i;->ॱॱ:Ljava/lang/String;

    iget-object v5, p0, Lcom/appsflyer/i;->ᐝ:Ljava/lang/String;

    invoke-virtual {v3}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v1, v0, v4, v5, v3}, Lcom/appsflyer/i;->ॱ(ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_1
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    :goto_2
    if-eqz v2, :cond_7

    .line 130
    invoke-virtual {v2}, Ljava/net/HttpURLConnection;->disconnect()V

    return-void

    :catchall_1
    move-exception v1

    goto :goto_4

    :catch_1
    move-exception v2

    .line 122
    :goto_3
    :try_start_2
    sget-object v3, Lcom/appsflyer/AppsFlyerLib;->ˏ:Lcom/appsflyer/AppsFlyerInAppPurchaseValidatorListener;

    if-eqz v3, :cond_6

    const-string v3, "Failed Validate request + ex"

    .line 123
    invoke-static {v3, v2}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 124
    iget-object v3, p0, Lcom/appsflyer/i;->ˏ:Ljava/lang/String;

    iget-object v4, p0, Lcom/appsflyer/i;->ॱॱ:Ljava/lang/String;

    iget-object v5, p0, Lcom/appsflyer/i;->ᐝ:Ljava/lang/String;

    invoke-virtual {v2}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v6

    invoke-static {v1, v3, v4, v5, v6}, Lcom/appsflyer/i;->ॱ(ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 127
    :cond_6
    invoke-virtual {v2}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1, v2}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz v0, :cond_7

    .line 130
    invoke-virtual {v0}, Ljava/net/HttpURLConnection;->disconnect()V

    return-void

    :cond_7
    return-void

    :goto_4
    if-eqz v0, :cond_8

    invoke-virtual {v0}, Ljava/net/HttpURLConnection;->disconnect()V

    :cond_8
    throw v1

    :cond_9
    :goto_5
    return-void
.end method
