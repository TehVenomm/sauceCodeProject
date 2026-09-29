.class public Lcom/facebook/appevents/codeless/CodelessLoggingEventListener;
.super Ljava/lang/Object;
.source "CodelessLoggingEventListener.java"


# annotations
.annotation build Landroidx/annotation/RestrictTo;
    value = {
        .enum Landroidx/annotation/RestrictTo$Scope;->LIBRARY_GROUP:Landroidx/annotation/RestrictTo$Scope;
    }
.end annotation

.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/facebook/appevents/codeless/CodelessLoggingEventListener$AutoLoggingOnItemClickListener;,
        Lcom/facebook/appevents/codeless/CodelessLoggingEventListener$AutoLoggingOnClickListener;
    }
.end annotation


# static fields
.field private static final TAG:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 44
    const-class v0, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener;

    invoke-virtual {v0}, Ljava/lang/Class;->getCanonicalName()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener;->TAG:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 43
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static synthetic access$200(Lcom/facebook/appevents/codeless/internal/EventBinding;Landroid/view/View;Landroid/view/View;)V
    .locals 1

    const-class v0, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 43
    :cond_0
    :try_start_0
    invoke-static {p0, p1, p2}, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener;->logEvent(Lcom/facebook/appevents/codeless/internal/EventBinding;Landroid/view/View;Landroid/view/View;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p0

    const-class p1, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method

.method public static getOnClickListener(Lcom/facebook/appevents/codeless/internal/EventBinding;Landroid/view/View;Landroid/view/View;)Lcom/facebook/appevents/codeless/CodelessLoggingEventListener$AutoLoggingOnClickListener;
    .locals 2

    const-class v0, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    .line 48
    :cond_0
    :try_start_0
    new-instance v0, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener$AutoLoggingOnClickListener;

    invoke-direct {v0, p0, p1, p2, v1}, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener$AutoLoggingOnClickListener;-><init>(Lcom/facebook/appevents/codeless/internal/EventBinding;Landroid/view/View;Landroid/view/View;Lcom/facebook/appevents/codeless/CodelessLoggingEventListener$1;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception p0

    const-class p1, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-object v1
.end method

.method public static getOnItemClickListener(Lcom/facebook/appevents/codeless/internal/EventBinding;Landroid/view/View;Landroid/widget/AdapterView;)Lcom/facebook/appevents/codeless/CodelessLoggingEventListener$AutoLoggingOnItemClickListener;
    .locals 2

    const-class v0, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    .line 53
    :cond_0
    :try_start_0
    new-instance v0, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener$AutoLoggingOnItemClickListener;

    invoke-direct {v0, p0, p1, p2, v1}, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener$AutoLoggingOnItemClickListener;-><init>(Lcom/facebook/appevents/codeless/internal/EventBinding;Landroid/view/View;Landroid/widget/AdapterView;Lcom/facebook/appevents/codeless/CodelessLoggingEventListener$1;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception p0

    const-class p1, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-object v1
.end method

.method private static logEvent(Lcom/facebook/appevents/codeless/internal/EventBinding;Landroid/view/View;Landroid/view/View;)V
    .locals 3

    const-class v0, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener;

    invoke-static {v0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 59
    :cond_0
    :try_start_0
    invoke-virtual {p0}, Lcom/facebook/appevents/codeless/internal/EventBinding;->getEventName()Ljava/lang/String;

    move-result-object v0

    .line 60
    invoke-static {p0, p1, p2}, Lcom/facebook/appevents/codeless/CodelessMatcher;->getParameters(Lcom/facebook/appevents/codeless/internal/EventBinding;Landroid/view/View;Landroid/view/View;)Landroid/os/Bundle;

    move-result-object p0

    const-string p1, "_valueToSum"

    .line 65
    invoke-virtual {p0, p1}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_1

    const-string p1, "_valueToSum"

    .line 66
    invoke-virtual {p0, p1}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    const-string p2, "_valueToSum"

    .line 69
    invoke-static {p1}, Lcom/facebook/appevents/internal/AppEventUtility;->normalizePrice(Ljava/lang/String;)D

    move-result-wide v1

    .line 67
    invoke-virtual {p0, p2, v1, v2}, Landroid/os/Bundle;->putDouble(Ljava/lang/String;D)V

    :cond_1
    const-string p1, "_is_fb_codeless"

    const-string p2, "1"

    .line 72
    invoke-virtual {p0, p1, p2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 74
    invoke-static {}, Lcom/facebook/FacebookSdk;->getExecutor()Ljava/util/concurrent/Executor;

    move-result-object p1

    new-instance p2, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener$1;

    invoke-direct {p2, v0, p0}, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener$1;-><init>(Ljava/lang/String;Landroid/os/Bundle;)V

    invoke-interface {p1, p2}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p0

    .line 82
    const-class p1, Lcom/facebook/appevents/codeless/CodelessLoggingEventListener;

    invoke-static {p0, p1}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method
