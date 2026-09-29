.class public Lcom/facebook/internal/instrument/threadcheck/ThreadCheckHandler;
.super Ljava/lang/Object;
.source "ThreadCheckHandler.java"


# annotations
.annotation build Landroidx/annotation/RestrictTo;
    value = {
        .enum Landroidx/annotation/RestrictTo$Scope;->LIBRARY_GROUP:Landroidx/annotation/RestrictTo$Scope;
    }
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String;

.field private static enabled:Z


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 34
    const-class v0, Lcom/facebook/internal/instrument/threadcheck/ThreadCheckHandler;

    invoke-virtual {v0}, Ljava/lang/Class;->getCanonicalName()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/facebook/internal/instrument/threadcheck/ThreadCheckHandler;->TAG:Ljava/lang/String;

    const/4 v0, 0x0

    .line 35
    sput-boolean v0, Lcom/facebook/internal/instrument/threadcheck/ThreadCheckHandler;->enabled:Z

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 37
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static enable()V
    .locals 1

    const/4 v0, 0x1

    .line 40
    sput-boolean v0, Lcom/facebook/internal/instrument/threadcheck/ThreadCheckHandler;->enabled:Z

    return-void
.end method

.method private static log(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/Class<",
            "*>;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    .line 61
    sget-boolean v0, Lcom/facebook/internal/instrument/threadcheck/ThreadCheckHandler;->enabled:Z

    if-nez v0, :cond_0

    return-void

    .line 65
    :cond_0
    sget-object v0, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v1, "%s annotation violation detected in %s.%s%s. Current looper is %s and main looper is %s."

    const/4 v2, 0x6

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p0, v2, v3

    const/4 p0, 0x1

    .line 69
    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    aput-object p1, v2, p0

    const/4 p0, 0x2

    aput-object p2, v2, p0

    const/4 p0, 0x3

    aput-object p3, v2, p0

    const/4 p0, 0x4

    .line 72
    invoke-static {}, Landroid/os/Looper;->myLooper()Landroid/os/Looper;

    move-result-object p1

    aput-object p1, v2, p0

    const/4 p0, 0x5

    .line 73
    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object p1

    aput-object p1, v2, p0

    .line 65
    invoke-static {v0, v1, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    .line 74
    new-instance p1, Ljava/lang/Exception;

    invoke-direct {p1}, Ljava/lang/Exception;-><init>()V

    .line 75
    sget-object p2, Lcom/facebook/internal/instrument/threadcheck/ThreadCheckHandler;->TAG:Ljava/lang/String;

    invoke-static {p2, p0, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 76
    sget-object p0, Lcom/facebook/internal/instrument/InstrumentData$Type;->ThreadCheck:Lcom/facebook/internal/instrument/InstrumentData$Type;

    invoke-static {p1, p0}, Lcom/facebook/internal/instrument/InstrumentData$Builder;->build(Ljava/lang/Throwable;Lcom/facebook/internal/instrument/InstrumentData$Type;)Lcom/facebook/internal/instrument/InstrumentData;

    move-result-object p0

    invoke-virtual {p0}, Lcom/facebook/internal/instrument/InstrumentData;->save()V

    return-void
.end method

.method public static uiThreadViolationDetected(Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    const-string v0, "@UiThread"

    .line 47
    invoke-static {v0, p0, p1, p2}, Lcom/facebook/internal/instrument/threadcheck/ThreadCheckHandler;->log(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public static workerThreadViolationDetected(Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    const-string v0, "@WorkerThread"

    .line 54
    invoke-static {v0, p0, p1, p2}, Lcom/facebook/internal/instrument/threadcheck/ThreadCheckHandler;->log(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
