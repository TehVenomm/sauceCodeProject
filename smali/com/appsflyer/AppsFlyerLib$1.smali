.class final Lcom/appsflyer/AppsFlyerLib$1;
.super Ljava/lang/Object;
.source ""

# interfaces
.implements Lcom/appsflyer/s$b;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/appsflyer/AppsFlyerLib;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field private synthetic ˋ:Ljava/lang/ref/WeakReference;

.field private synthetic ˏ:Ljava/util/Map;

.field private synthetic ॱ:Lcom/appsflyer/AppsFlyerLib;


# direct methods
.method constructor <init>(Lcom/appsflyer/AppsFlyerLib;Ljava/util/Map;Ljava/lang/ref/WeakReference;)V
    .locals 0

    .line 2081
    iput-object p1, p0, Lcom/appsflyer/AppsFlyerLib$1;->ॱ:Lcom/appsflyer/AppsFlyerLib;

    iput-object p2, p0, Lcom/appsflyer/AppsFlyerLib$1;->ˏ:Ljava/util/Map;

    iput-object p3, p0, Lcom/appsflyer/AppsFlyerLib$1;->ˋ:Ljava/lang/ref/WeakReference;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private ˋ(Ljava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 2104
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib$1;->ˋ:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 2105
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/util/Map;)V

    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    .line 2106
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib$1;->ˋ:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/content/Context;

    const-string v1, "deeplinkAttribution"

    invoke-static {v0, v1, p1}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    :cond_0
    return-void
.end method


# virtual methods
.method public final ॱ(Ljava/lang/String;)V
    .locals 1

    .line 2091
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->ˏ()Lcom/appsflyer/AppsFlyerConversionListener;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 2092
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib$1;->ˏ:Ljava/util/Map;

    invoke-direct {p0, v0}, Lcom/appsflyer/AppsFlyerLib$1;->ˋ(Ljava/util/Map;)V

    .line 2093
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->ˏ()Lcom/appsflyer/AppsFlyerConversionListener;

    move-result-object v0

    invoke-interface {v0, p1}, Lcom/appsflyer/AppsFlyerConversionListener;->onAttributionFailure(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public final ॱ(Ljava/util/Map;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 5098
    invoke-interface {p1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 5099
    iget-object v2, p0, Lcom/appsflyer/AppsFlyerLib$1;->ˏ:Ljava/util/Map;

    invoke-interface {p1, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    invoke-interface {v2, v1, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 2085
    :cond_0
    iget-object p1, p0, Lcom/appsflyer/AppsFlyerLib$1;->ˏ:Ljava/util/Map;

    invoke-direct {p0, p1}, Lcom/appsflyer/AppsFlyerLib$1;->ˋ(Ljava/util/Map;)V

    .line 2086
    iget-object p1, p0, Lcom/appsflyer/AppsFlyerLib$1;->ˏ:Ljava/util/Map;

    invoke-static {p1}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Ljava/util/Map;)V

    return-void
.end method
