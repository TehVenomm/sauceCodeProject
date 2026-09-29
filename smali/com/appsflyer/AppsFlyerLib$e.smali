.class final Lcom/appsflyer/AppsFlyerLib$e;
.super Ljava/lang/Object;
.source ""

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/appsflyer/AppsFlyerLib;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = "e"
.end annotation


# instance fields
.field private ˊ:I

.field private ˋ:Z

.field private ˎ:Ljava/lang/String;

.field private ˏ:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Landroid/content/Context;",
            ">;"
        }
    .end annotation
.end field

.field private ॱ:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field private synthetic ᐝ:Lcom/appsflyer/AppsFlyerLib;


# direct methods
.method private constructor <init>(Lcom/appsflyer/AppsFlyerLib;Ljava/lang/String;Ljava/util/Map;Landroid/content/Context;ZI)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Landroid/content/Context;",
            "ZI)V"
        }
    .end annotation

    .line 2891
    iput-object p1, p0, Lcom/appsflyer/AppsFlyerLib$e;->ᐝ:Lcom/appsflyer/AppsFlyerLib;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 p1, 0x0

    .line 2882
    iput-object p1, p0, Lcom/appsflyer/AppsFlyerLib$e;->ˏ:Ljava/lang/ref/WeakReference;

    .line 2892
    iput-object p2, p0, Lcom/appsflyer/AppsFlyerLib$e;->ˎ:Ljava/lang/String;

    .line 2893
    iput-object p3, p0, Lcom/appsflyer/AppsFlyerLib$e;->ॱ:Ljava/util/Map;

    .line 2894
    new-instance p1, Ljava/lang/ref/WeakReference;

    invoke-direct {p1, p4}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object p1, p0, Lcom/appsflyer/AppsFlyerLib$e;->ˏ:Ljava/lang/ref/WeakReference;

    .line 2895
    iput-boolean p5, p0, Lcom/appsflyer/AppsFlyerLib$e;->ˋ:Z

    .line 2896
    iput p6, p0, Lcom/appsflyer/AppsFlyerLib$e;->ˊ:I

    return-void
.end method

.method synthetic constructor <init>(Lcom/appsflyer/AppsFlyerLib;Ljava/lang/String;Ljava/util/Map;Landroid/content/Context;ZIB)V
    .locals 0

    .line 2879
    invoke-direct/range {p0 .. p6}, Lcom/appsflyer/AppsFlyerLib$e;-><init>(Lcom/appsflyer/AppsFlyerLib;Ljava/lang/String;Ljava/util/Map;Landroid/content/Context;ZI)V

    return-void
.end method


# virtual methods
.method public final run()V
    .locals 10

    .line 2901
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib$e;->ᐝ:Lcom/appsflyer/AppsFlyerLib;

    invoke-virtual {v0}, Lcom/appsflyer/AppsFlyerLib;->isTrackingStopped()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x0

    .line 2907
    iget-boolean v1, p0, Lcom/appsflyer/AppsFlyerLib$e;->ˋ:Z

    if-eqz v1, :cond_1

    iget v1, p0, Lcom/appsflyer/AppsFlyerLib$e;->ˊ:I

    const/4 v2, 0x2

    if-gt v1, v2, :cond_1

    iget-object v1, p0, Lcom/appsflyer/AppsFlyerLib$e;->ᐝ:Lcom/appsflyer/AppsFlyerLib;

    .line 2909
    invoke-static {v1}, Lcom/appsflyer/AppsFlyerLib;->ˏ(Lcom/appsflyer/AppsFlyerLib;)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 2911
    iget-object v1, p0, Lcom/appsflyer/AppsFlyerLib$e;->ॱ:Ljava/util/Map;

    const-string v2, "rfr"

    iget-object v3, p0, Lcom/appsflyer/AppsFlyerLib$e;->ᐝ:Lcom/appsflyer/AppsFlyerLib;

    invoke-static {v3}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Lcom/appsflyer/AppsFlyerLib;)Ljava/util/Map;

    move-result-object v3

    invoke-interface {v1, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 2915
    :cond_1
    :try_start_0
    iget-object v1, p0, Lcom/appsflyer/AppsFlyerLib$e;->ॱ:Ljava/util/Map;

    const-string v2, "appsflyerKey"

    invoke-interface {v1, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    move-object v5, v1

    check-cast v5, Ljava/lang/String;

    .line 2917
    iget-object v1, p0, Lcom/appsflyer/AppsFlyerLib$e;->ॱ:Ljava/util/Map;

    invoke-static {v1}, Lcom/appsflyer/AFHelper;->convertToJsonObject(Ljava/util/Map;)Lorg/json/JSONObject;

    move-result-object v1

    invoke-virtual {v1}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v1
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_1

    .line 2919
    :try_start_1
    iget-object v2, p0, Lcom/appsflyer/AppsFlyerLib$e;->ᐝ:Lcom/appsflyer/AppsFlyerLib;

    iget-object v3, p0, Lcom/appsflyer/AppsFlyerLib$e;->ˎ:Ljava/lang/String;

    iget-object v6, p0, Lcom/appsflyer/AppsFlyerLib$e;->ˏ:Ljava/lang/ref/WeakReference;

    const/4 v7, 0x0

    iget-boolean v8, p0, Lcom/appsflyer/AppsFlyerLib$e;->ˋ:Z

    move-object v4, v1

    invoke-static/range {v2 .. v8}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Lcom/appsflyer/AppsFlyerLib;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/ref/WeakReference;Ljava/lang/String;Z)V
    :try_end_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_1

    return-void

    :catch_0
    move-exception v0

    move-object v9, v1

    move-object v1, v0

    move-object v0, v9

    goto :goto_0

    :catch_1
    move-exception v0

    .line 2928
    invoke-virtual {v0}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1, v0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    return-void

    :catch_2
    move-exception v1

    :goto_0
    const-string v2, "Exception while sending request to server. "

    .line 2922
    invoke-static {v2, v1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    if-eqz v0, :cond_2

    .line 2923
    iget-object v2, p0, Lcom/appsflyer/AppsFlyerLib$e;->ˏ:Ljava/lang/ref/WeakReference;

    if-eqz v2, :cond_2

    iget-object v2, p0, Lcom/appsflyer/AppsFlyerLib$e;->ˎ:Ljava/lang/String;

    const-string v3, "&isCachedRequest=true&timeincache="

    invoke-virtual {v2, v3}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v2

    if-nez v2, :cond_2

    .line 2924
    invoke-static {}, Lcom/appsflyer/cache/CacheManager;->getInstance()Lcom/appsflyer/cache/CacheManager;

    move-result-object v2

    new-instance v3, Lcom/appsflyer/cache/RequestCacheData;

    iget-object v4, p0, Lcom/appsflyer/AppsFlyerLib$e;->ˎ:Ljava/lang/String;

    const-string v5, "4.8.11"

    invoke-direct {v3, v4, v0, v5}, Lcom/appsflyer/cache/RequestCacheData;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib$e;->ˏ:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/content/Context;

    invoke-virtual {v2, v3, v0}, Lcom/appsflyer/cache/CacheManager;->cacheRequest(Lcom/appsflyer/cache/RequestCacheData;Landroid/content/Context;)V

    .line 2925
    invoke-virtual {v1}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0, v1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_2
    return-void
.end method
