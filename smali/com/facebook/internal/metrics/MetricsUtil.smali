.class public Lcom/facebook/internal/metrics/MetricsUtil;
.super Ljava/lang/Object;
.source "MetricsUtil.java"


# instance fields
.field private final CLASS_TAG:Ljava/lang/String;

.field private final STARTUP_METRICS_PREFERENCES:Ljava/lang/String;

.field private final TIME_DIFFERENCE_BASE_PREF:Ljava/lang/String;

.field private ctx:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Landroid/content/Context;",
            ">;"
        }
    .end annotation
.end field

.field private metricsUtil:Lcom/facebook/internal/metrics/MetricsUtil;

.field private taggedStartTimer:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Lcom/facebook/internal/metrics/Tag;",
            "Ljava/lang/Long;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method private constructor <init>(Landroid/content/Context;)V
    .locals 1

    .line 34
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "internal.MetricsUtil"

    .line 27
    iput-object v0, p0, Lcom/facebook/internal/metrics/MetricsUtil;->CLASS_TAG:Ljava/lang/String;

    const-string v0, "MetricsUtil"

    .line 28
    iput-object v0, p0, Lcom/facebook/internal/metrics/MetricsUtil;->STARTUP_METRICS_PREFERENCES:Ljava/lang/String;

    const-string v0, "time_difference"

    .line 29
    iput-object v0, p0, Lcom/facebook/internal/metrics/MetricsUtil;->TIME_DIFFERENCE_BASE_PREF:Ljava/lang/String;

    .line 35
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lcom/facebook/internal/metrics/MetricsUtil;->taggedStartTimer:Ljava/util/HashMap;

    .line 36
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Lcom/facebook/internal/metrics/MetricsUtil;->ctx:Ljava/lang/ref/WeakReference;

    return-void
.end method

.method private updateLastTimeDifferenceFor(Lcom/facebook/internal/metrics/Tag;J)V
    .locals 3

    .line 81
    iget-object v0, p0, Lcom/facebook/internal/metrics/MetricsUtil;->ctx:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    if-nez v0, :cond_0

    const-string p1, "internal.MetricsUtil"

    const-string p2, "updateLastTimeDifferenceFor: Context is null"

    .line 82
    invoke-static {p1, p2}, Lcom/facebook/internal/Utility;->logd(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    .line 85
    :cond_0
    iget-object v0, p0, Lcom/facebook/internal/metrics/MetricsUtil;->ctx:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/content/Context;

    const-string v1, "MetricsUtil"

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 88
    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "time_difference"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Lcom/facebook/internal/metrics/Tag;->getSuffix()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v0, p1, p2, p3}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    move-result-object p1

    invoke-interface {p1}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void
.end method


# virtual methods
.method public declared-synchronized getInstance(Landroid/content/Context;)Lcom/facebook/internal/metrics/MetricsUtil;
    .locals 1

    monitor-enter p0

    .line 42
    :try_start_0
    iget-object v0, p0, Lcom/facebook/internal/metrics/MetricsUtil;->metricsUtil:Lcom/facebook/internal/metrics/MetricsUtil;

    if-nez v0, :cond_0

    .line 43
    new-instance v0, Lcom/facebook/internal/metrics/MetricsUtil;

    invoke-direct {v0, p1}, Lcom/facebook/internal/metrics/MetricsUtil;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/facebook/internal/metrics/MetricsUtil;->metricsUtil:Lcom/facebook/internal/metrics/MetricsUtil;

    .line 44
    iget-object p1, p0, Lcom/facebook/internal/metrics/MetricsUtil;->metricsUtil:Lcom/facebook/internal/metrics/MetricsUtil;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    .line 46
    :cond_0
    :try_start_1
    iget-object p1, p0, Lcom/facebook/internal/metrics/MetricsUtil;->metricsUtil:Lcom/facebook/internal/metrics/MetricsUtil;
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    .line 41
    monitor-exit p0

    throw p1
.end method

.method public getLastTimeDifferenceFor(Lcom/facebook/internal/metrics/Tag;)J
    .locals 5

    .line 70
    iget-object v0, p0, Lcom/facebook/internal/metrics/MetricsUtil;->ctx:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    const-wide/16 v1, -0x1

    if-nez v0, :cond_0

    const-string p1, "internal.MetricsUtil"

    const-string v0, "getLastTimeDifferenceFor: Context is null"

    .line 71
    invoke-static {p1, v0}, Lcom/facebook/internal/Utility;->logd(Ljava/lang/String;Ljava/lang/String;)V

    return-wide v1

    .line 74
    :cond_0
    iget-object v0, p0, Lcom/facebook/internal/metrics/MetricsUtil;->ctx:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/content/Context;

    const-string v3, "MetricsUtil"

    const/4 v4, 0x0

    invoke-virtual {v0, v3, v4}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 77
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "time_difference"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Lcom/facebook/internal/metrics/Tag;->getSuffix()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v0, p1, v1, v2}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v0

    return-wide v0
.end method

.method public startMeasureFor(Lcom/facebook/internal/metrics/Tag;)V
    .locals 3

    .line 53
    iget-object v0, p0, Lcom/facebook/internal/metrics/MetricsUtil;->taggedStartTimer:Ljava/util/HashMap;

    invoke-static {}, Landroid/os/SystemClock;->elapsedRealtime()J

    move-result-wide v1

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v0, p1, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-void
.end method

.method public stopMeasureFor(Lcom/facebook/internal/metrics/Tag;)V
    .locals 4

    .line 60
    invoke-static {}, Landroid/os/SystemClock;->elapsedRealtime()J

    move-result-wide v0

    .line 61
    iget-object v2, p0, Lcom/facebook/internal/metrics/MetricsUtil;->taggedStartTimer:Ljava/util/HashMap;

    invoke-virtual {v2, p1}, Ljava/util/HashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_0

    return-void

    .line 64
    :cond_0
    iget-object v2, p0, Lcom/facebook/internal/metrics/MetricsUtil;->taggedStartTimer:Ljava/util/HashMap;

    invoke-virtual {v2, p1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/Long;

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    sub-long/2addr v0, v2

    .line 65
    iget-object v2, p0, Lcom/facebook/internal/metrics/MetricsUtil;->taggedStartTimer:Ljava/util/HashMap;

    invoke-virtual {v2, p1}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 66
    invoke-direct {p0, p1, v0, v1}, Lcom/facebook/internal/metrics/MetricsUtil;->updateLastTimeDifferenceFor(Lcom/facebook/internal/metrics/Tag;J)V

    return-void
.end method
