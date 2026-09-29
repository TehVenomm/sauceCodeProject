.class final Lcom/appsflyer/AppsFlyerLib$a;
.super Lcom/appsflyer/AppsFlyerLib$d;
.source ""


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/appsflyer/AppsFlyerLib;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = "a"
.end annotation


# instance fields
.field private synthetic ˎ:Lcom/appsflyer/AppsFlyerLib;


# direct methods
.method public constructor <init>(Lcom/appsflyer/AppsFlyerLib;Landroid/content/Context;Ljava/lang/String;Ljava/util/concurrent/ScheduledExecutorService;)V
    .locals 0

    .line 2935
    iput-object p1, p0, Lcom/appsflyer/AppsFlyerLib$a;->ˎ:Lcom/appsflyer/AppsFlyerLib;

    .line 2936
    invoke-direct {p0, p1, p2, p3, p4}, Lcom/appsflyer/AppsFlyerLib$d;-><init>(Lcom/appsflyer/AppsFlyerLib;Landroid/content/Context;Ljava/lang/String;Ljava/util/concurrent/ScheduledExecutorService;)V

    return-void
.end method


# virtual methods
.method protected final ˊ(Ljava/lang/String;I)V
    .locals 1

    .line 2951
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->ˏ()Lcom/appsflyer/AppsFlyerConversionListener;

    move-result-object v0

    invoke-interface {v0, p1}, Lcom/appsflyer/AppsFlyerConversionListener;->onInstallConversionFailure(Ljava/lang/String;)V

    const/16 p1, 0x190

    if-lt p2, p1, :cond_0

    const/16 p1, 0x1f4

    if-ge p2, p1, :cond_0

    .line 2954
    iget-object p1, p0, Lcom/appsflyer/AppsFlyerLib$d;->ˋ:Ljava/lang/ref/WeakReference;

    invoke-virtual {p1}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/content/Context;

    invoke-static {p1}, Lcom/appsflyer/AppsFlyerLib;->ˏ(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object p1

    const-string p2, "appsflyerConversionDataRequestRetries"

    const/4 v0, 0x0

    .line 2955
    invoke-interface {p1, p2, v0}, Landroid/content/SharedPreferences;->getInt(Ljava/lang/String;I)I

    move-result p1

    .line 2956
    iget-object p2, p0, Lcom/appsflyer/AppsFlyerLib$d;->ˋ:Ljava/lang/ref/WeakReference;

    invoke-virtual {p2}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Landroid/content/Context;

    const-string v0, "appsflyerConversionDataRequestRetries"

    add-int/lit8 p1, p1, 0x1

    invoke-static {p2, v0, p1}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/Context;Ljava/lang/String;I)V

    :cond_0
    return-void
.end method

.method protected final ˊ(Ljava/util/Map;)V
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

    const-string v0, "is_first_launch"

    const/4 v1, 0x1

    .line 2945
    invoke-static {v1}, Ljava/lang/Boolean;->toString(Z)Ljava/lang/String;

    move-result-object v1

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 2946
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->ˏ()Lcom/appsflyer/AppsFlyerConversionListener;

    move-result-object v0

    invoke-interface {v0, p1}, Lcom/appsflyer/AppsFlyerConversionListener;->onInstallConversionDataLoaded(Ljava/util/Map;)V

    .line 2947
    iget-object p1, p0, Lcom/appsflyer/AppsFlyerLib$d;->ˋ:Ljava/lang/ref/WeakReference;

    invoke-virtual {p1}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/content/Context;

    const-string v0, "appsflyerConversionDataRequestRetries"

    const/4 v1, 0x0

    invoke-static {p1, v0, v1}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/Context;Ljava/lang/String;I)V

    return-void
.end method

.method public final ॱ()Ljava/lang/String;
    .locals 1

    const-string v0, "https://api.%s/install_data/v3/"

    .line 2941
    invoke-static {v0}, Lcom/appsflyer/ServerConfigHandler;->getUrl(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
