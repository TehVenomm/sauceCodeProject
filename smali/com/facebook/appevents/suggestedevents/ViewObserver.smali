.class final Lcom/facebook/appevents/suggestedevents/ViewObserver;
.super Ljava/lang/Object;
.source "ViewObserver.java"

# interfaces
.implements Landroid/view/ViewTreeObserver$OnGlobalLayoutListener;


# static fields
.field private static final MAX_TEXT_LENGTH:I = 0x12c

.field private static final TAG:Ljava/lang/String;

.field private static final observers:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/Integer;",
            "Lcom/facebook/appevents/suggestedevents/ViewObserver;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field private activityWeakReference:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Landroid/app/Activity;",
            ">;"
        }
    .end annotation
.end field

.field private isTracking:Ljava/util/concurrent/atomic/AtomicBoolean;

.field private final uiThreadHandler:Landroid/os/Handler;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 41
    const-class v0, Lcom/facebook/appevents/suggestedevents/ViewObserver;

    invoke-virtual {v0}, Ljava/lang/Class;->getCanonicalName()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/facebook/appevents/suggestedevents/ViewObserver;->TAG:Ljava/lang/String;

    .line 45
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    sput-object v0, Lcom/facebook/appevents/suggestedevents/ViewObserver;->observers:Ljava/util/Map;

    return-void
.end method

.method private constructor <init>(Landroid/app/Activity;)V
    .locals 1

    .line 66
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 67
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Lcom/facebook/appevents/suggestedevents/ViewObserver;->activityWeakReference:Ljava/lang/ref/WeakReference;

    .line 68
    new-instance p1, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v0

    invoke-direct {p1, v0}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object p1, p0, Lcom/facebook/appevents/suggestedevents/ViewObserver;->uiThreadHandler:Landroid/os/Handler;

    .line 69
    new-instance p1, Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v0, 0x0

    invoke-direct {p1, v0}, Ljava/util/concurrent/atomic/AtomicBoolean;-><init>(Z)V

    iput-object p1, p0, Lcom/facebook/appevents/suggestedevents/ViewObserver;->isTracking:Ljava/util/concurrent/atomic/AtomicBoolean;

    return-void
.end method

.method static synthetic access$000(Lcom/facebook/appevents/suggestedevents/ViewObserver;)Ljava/lang/ref/WeakReference;
    .locals 2

    const-class v0, Lcom/facebook/appevents/suggestedevents/ViewObserver;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    .line 40
    :cond_0
    :try_start_0
    iget-object p0, p0, Lcom/facebook/appevents/suggestedevents/ViewObserver;->activityWeakReference:Ljava/lang/ref/WeakReference;
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    const-class v0, Lcom/facebook/appevents/suggestedevents/ViewObserver;

    invoke-static {p0, v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-object v1
.end method

.method private process()V
    .locals 3

    invoke-static {p0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 112
    :cond_0
    :try_start_0
    new-instance v0, Lcom/facebook/appevents/suggestedevents/ViewObserver$1;

    invoke-direct {v0, p0}, Lcom/facebook/appevents/suggestedevents/ViewObserver$1;-><init>(Lcom/facebook/appevents/suggestedevents/ViewObserver;)V

    .line 142
    invoke-static {}, Ljava/lang/Thread;->currentThread()Ljava/lang/Thread;

    move-result-object v1

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v2

    invoke-virtual {v2}, Landroid/os/Looper;->getThread()Ljava/lang/Thread;

    move-result-object v2

    if-ne v1, v2, :cond_1

    .line 143
    invoke-interface {v0}, Ljava/lang/Runnable;->run()V

    goto :goto_0

    .line 145
    :cond_1
    iget-object v1, p0, Lcom/facebook/appevents/suggestedevents/ViewObserver;->uiThreadHandler:Landroid/os/Handler;

    invoke-virtual {v1, v0}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    :goto_0
    return-void

    :catch_0
    move-exception v0

    .line 147
    invoke-static {v0, p0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method

.method private startTracking()V
    .locals 2

    invoke-static {p0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 73
    :cond_0
    :try_start_0
    iget-object v0, p0, Lcom/facebook/appevents/suggestedevents/ViewObserver;->isTracking:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;->getAndSet(Z)Z

    move-result v0

    if-eqz v0, :cond_1

    return-void

    .line 76
    :cond_1
    iget-object v0, p0, Lcom/facebook/appevents/suggestedevents/ViewObserver;->activityWeakReference:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/app/Activity;

    invoke-static {v0}, Lcom/facebook/appevents/internal/AppEventUtility;->getRootView(Landroid/app/Activity;)Landroid/view/View;

    move-result-object v0

    if-nez v0, :cond_2

    return-void

    .line 80
    :cond_2
    invoke-virtual {v0}, Landroid/view/View;->getViewTreeObserver()Landroid/view/ViewTreeObserver;

    move-result-object v0

    .line 81
    invoke-virtual {v0}, Landroid/view/ViewTreeObserver;->isAlive()Z

    move-result v1

    if-eqz v1, :cond_3

    .line 82
    invoke-virtual {v0, p0}, Landroid/view/ViewTreeObserver;->addOnGlobalLayoutListener(Landroid/view/ViewTreeObserver$OnGlobalLayoutListener;)V

    .line 83
    invoke-direct {p0}, Lcom/facebook/appevents/suggestedevents/ViewObserver;->process()V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    :cond_3
    return-void

    :catch_0
    move-exception v0

    .line 85
    invoke-static {v0, p0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method

.method static startTrackingActivity(Landroid/app/Activity;)V
    .locals 3

    const-class v0, Lcom/facebook/appevents/suggestedevents/ViewObserver;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 49
    :cond_0
    :try_start_0
    invoke-virtual {p0}, Ljava/lang/Object;->hashCode()I

    move-result v0

    .line 50
    sget-object v1, Lcom/facebook/appevents/suggestedevents/ViewObserver;->observers:Ljava/util/Map;

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {v1, v2}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_1

    .line 51
    new-instance v1, Lcom/facebook/appevents/suggestedevents/ViewObserver;

    invoke-direct {v1, p0}, Lcom/facebook/appevents/suggestedevents/ViewObserver;-><init>(Landroid/app/Activity;)V

    .line 52
    sget-object p0, Lcom/facebook/appevents/suggestedevents/ViewObserver;->observers:Ljava/util/Map;

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-interface {p0, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 53
    invoke-direct {v1}, Lcom/facebook/appevents/suggestedevents/ViewObserver;->startTracking()V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    :cond_1
    return-void

    :catch_0
    move-exception p0

    .line 55
    const-class v0, Lcom/facebook/appevents/suggestedevents/ViewObserver;

    invoke-static {p0, v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method

.method private stopTracking()V
    .locals 3

    invoke-static {p0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 88
    :cond_0
    :try_start_0
    iget-object v0, p0, Lcom/facebook/appevents/suggestedevents/ViewObserver;->isTracking:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;->getAndSet(Z)Z

    move-result v0

    if-nez v0, :cond_1

    return-void

    .line 91
    :cond_1
    iget-object v0, p0, Lcom/facebook/appevents/suggestedevents/ViewObserver;->activityWeakReference:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/app/Activity;

    invoke-static {v0}, Lcom/facebook/appevents/internal/AppEventUtility;->getRootView(Landroid/app/Activity;)Landroid/view/View;

    move-result-object v0

    if-nez v0, :cond_2

    return-void

    .line 95
    :cond_2
    invoke-virtual {v0}, Landroid/view/View;->getViewTreeObserver()Landroid/view/ViewTreeObserver;

    move-result-object v0

    .line 96
    invoke-virtual {v0}, Landroid/view/ViewTreeObserver;->isAlive()Z

    move-result v1

    if-nez v1, :cond_3

    return-void

    .line 99
    :cond_3
    sget v1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v2, 0x10

    if-ge v1, v2, :cond_4

    .line 100
    invoke-virtual {v0, p0}, Landroid/view/ViewTreeObserver;->removeGlobalOnLayoutListener(Landroid/view/ViewTreeObserver$OnGlobalLayoutListener;)V

    goto :goto_0

    .line 102
    :cond_4
    invoke-virtual {v0, p0}, Landroid/view/ViewTreeObserver;->removeOnGlobalLayoutListener(Landroid/view/ViewTreeObserver$OnGlobalLayoutListener;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    :goto_0
    return-void

    :catch_0
    move-exception v0

    .line 104
    invoke-static {v0, p0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method

.method static stopTrackingActivity(Landroid/app/Activity;)V
    .locals 2

    const-class v0, Lcom/facebook/appevents/suggestedevents/ViewObserver;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 58
    :cond_0
    :try_start_0
    invoke-virtual {p0}, Ljava/lang/Object;->hashCode()I

    move-result p0

    .line 59
    sget-object v0, Lcom/facebook/appevents/suggestedevents/ViewObserver;->observers:Ljava/util/Map;

    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 60
    sget-object v0, Lcom/facebook/appevents/suggestedevents/ViewObserver;->observers:Ljava/util/Map;

    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/facebook/appevents/suggestedevents/ViewObserver;

    .line 61
    sget-object v1, Lcom/facebook/appevents/suggestedevents/ViewObserver;->observers:Ljava/util/Map;

    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p0

    invoke-interface {v1, p0}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 62
    invoke-direct {v0}, Lcom/facebook/appevents/suggestedevents/ViewObserver;->stopTracking()V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    :cond_1
    return-void

    :catch_0
    move-exception p0

    .line 64
    const-class v0, Lcom/facebook/appevents/suggestedevents/ViewObserver;

    invoke-static {p0, v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method


# virtual methods
.method public onGlobalLayout()V
    .locals 1

    invoke-static {p0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 108
    :cond_0
    :try_start_0
    invoke-direct {p0}, Lcom/facebook/appevents/suggestedevents/ViewObserver;->process()V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception v0

    .line 109
    invoke-static {v0, p0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method
