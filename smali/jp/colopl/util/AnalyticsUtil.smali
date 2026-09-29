.class public Ljp/colopl/util/AnalyticsUtil;
.super Ljava/lang/Object;
.source "AnalyticsUtil.java"


# static fields
.field public static SCOPE_PAGE:I = 0x3

.field public static SCOPE_SESSION:I = 0x2

.field public static SCOPE_USER:I = 0x1

.field public static SLOT_NO1:I = 0x1

.field public static SLOT_NO2:I = 0x2

.field public static SLOT_NO3:I = 0x3

.field public static SLOT_NO4:I = 0x4

.field public static SLOT_NO5:I = 0x5

.field private static final TAG:Ljava/lang/String; = "AnalyticsUtil"


# instance fields
.field private mCode:Ljava/lang/String;

.field private mContext:Landroid/content/Context;

.field private mInterval:I

.field private mTracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 1

    const/4 v0, 0x1

    .line 38
    invoke-direct {p0, p1, v0}, Ljp/colopl/util/AnalyticsUtil;-><init>(Landroid/content/Context;Z)V

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Z)V
    .locals 2

    .line 41
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/16 v0, 0x3c

    .line 34
    iput v0, p0, Ljp/colopl/util/AnalyticsUtil;->mInterval:I

    const-string v0, "drapro"

    .line 35
    iput-object v0, p0, Ljp/colopl/util/AnalyticsUtil;->mCode:Ljava/lang/String;

    .line 42
    sget-boolean v0, Ljp/colopl/config/Config;->debuggable:Z

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    return-void

    .line 43
    :cond_0
    invoke-static {}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->getInstance()Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    move-result-object v0

    iput-object v0, p0, Ljp/colopl/util/AnalyticsUtil;->mTracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    .line 44
    iput-object p1, p0, Ljp/colopl/util/AnalyticsUtil;->mContext:Landroid/content/Context;

    if-eqz p2, :cond_1

    .line 46
    iget p1, p0, Ljp/colopl/util/AnalyticsUtil;->mInterval:I

    invoke-virtual {p0, p1}, Ljp/colopl/util/AnalyticsUtil;->startNewSession(I)V

    :cond_1
    return-void
.end method


# virtual methods
.method public dispatch()V
    .locals 2

    .line 97
    sget-boolean v0, Ljp/colopl/config/Config;->debuggable:Z

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    return-void

    :cond_0
    const-string v0, "AnalyticsUtil"

    const-string v1, "dispatch"

    .line 98
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 99
    iget-object v0, p0, Ljp/colopl/util/AnalyticsUtil;->mTracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    invoke-virtual {v0}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->dispatch()Z

    return-void
.end method

.method public getVersionName()Ljava/lang/String;
    .locals 4

    .line 103
    sget-boolean v0, Ljp/colopl/config/Config;->debuggable:Z

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    const-string v0, ""

    return-object v0

    :cond_0
    const-string v0, ""

    .line 106
    :try_start_0
    iget-object v1, p0, Ljp/colopl/util/AnalyticsUtil;->mContext:Landroid/content/Context;

    invoke-virtual {v1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    iget-object v2, p0, Ljp/colopl/util/AnalyticsUtil;->mContext:Landroid/content/Context;

    invoke-virtual {v2}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v2

    const/16 v3, 0x80

    invoke-virtual {v1, v2, v3}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v1

    iget-object v1, v1, Landroid/content/pm/PackageInfo;->versionName:Ljava/lang/String;
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    move-object v0, v1

    :catch_0
    return-object v0
.end method

.method public setCustomVar(ILjava/lang/String;Ljava/lang/String;I)V
    .locals 3

    .line 91
    sget-boolean v0, Ljp/colopl/config/Config;->debuggable:Z

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    return-void

    :cond_0
    const-string v0, "AnalyticsUtil"

    .line 92
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "setCustomVar:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v2, ":"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ":"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ":"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p4}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 93
    iget-object v0, p0, Ljp/colopl/util/AnalyticsUtil;->mTracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    invoke-virtual {v0, p1, p2, p3, p4}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->setCustomVar(ILjava/lang/String;Ljava/lang/String;I)Z

    return-void
.end method

.method public startNewSession()V
    .locals 3

    .line 51
    sget-boolean v0, Ljp/colopl/config/Config;->debuggable:Z

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    return-void

    :cond_0
    const-string v0, "AnalyticsUtil"

    .line 52
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "startNewSession:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Ljp/colopl/util/AnalyticsUtil;->mCode:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 53
    iget-object v0, p0, Ljp/colopl/util/AnalyticsUtil;->mTracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    iget-object v1, p0, Ljp/colopl/util/AnalyticsUtil;->mCode:Ljava/lang/String;

    iget-object v2, p0, Ljp/colopl/util/AnalyticsUtil;->mContext:Landroid/content/Context;

    invoke-virtual {v0, v1, v2}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->startNewSession(Ljava/lang/String;Landroid/content/Context;)V

    return-void
.end method

.method public startNewSession(I)V
    .locals 3

    .line 57
    sget-boolean v0, Ljp/colopl/config/Config;->debuggable:Z

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    return-void

    :cond_0
    const-string v0, "AnalyticsUtil"

    .line 58
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "startNewSession:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Ljp/colopl/util/AnalyticsUtil;->mCode:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ":"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 59
    iget-object v0, p0, Ljp/colopl/util/AnalyticsUtil;->mTracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    iget-object v1, p0, Ljp/colopl/util/AnalyticsUtil;->mCode:Ljava/lang/String;

    iget-object v2, p0, Ljp/colopl/util/AnalyticsUtil;->mContext:Landroid/content/Context;

    invoke-virtual {v0, v1, p1, v2}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->startNewSession(Ljava/lang/String;ILandroid/content/Context;)V

    return-void
.end method

.method public stopSession()V
    .locals 2

    .line 63
    sget-boolean v0, Ljp/colopl/config/Config;->debuggable:Z

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    return-void

    :cond_0
    const-string v0, "AnalyticsUtil"

    const-string v1, "stopSession"

    .line 64
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 65
    iget-object v0, p0, Ljp/colopl/util/AnalyticsUtil;->mTracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    invoke-virtual {v0}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->stopSession()V

    return-void
.end method

.method public trackEvent(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    .line 75
    sget-boolean v0, Ljp/colopl/config/Config;->debuggable:Z

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    return-void

    :cond_0
    const-string v0, ""

    .line 76
    invoke-virtual {p0, p1, p2, v0}, Ljp/colopl/util/AnalyticsUtil;->trackEvent(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public trackEvent(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    .line 80
    sget-boolean v0, Ljp/colopl/config/Config;->debuggable:Z

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    return-void

    .line 81
    :cond_0
    invoke-virtual {p0, p1, p2, p3, v1}, Ljp/colopl/util/AnalyticsUtil;->trackEvent(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V

    return-void
.end method

.method public trackEvent(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V
    .locals 3

    .line 85
    sget-boolean v0, Ljp/colopl/config/Config;->debuggable:Z

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    return-void

    :cond_0
    const-string v0, "AnalyticsUtil"

    .line 86
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "trackEvent:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ":"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ":"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ":"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p4}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 87
    iget-object v0, p0, Ljp/colopl/util/AnalyticsUtil;->mTracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    invoke-virtual {v0, p1, p2, p3, p4}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->trackEvent(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V

    return-void
.end method

.method public trackPageView(Ljava/lang/String;)V
    .locals 3

    .line 69
    sget-boolean v0, Ljp/colopl/config/Config;->debuggable:Z

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    return-void

    :cond_0
    const-string v0, "AnalyticsUtil"

    .line 70
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "trackPageView:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 71
    iget-object v0, p0, Ljp/colopl/util/AnalyticsUtil;->mTracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    invoke-virtual {v0, p1}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->trackPageView(Ljava/lang/String;)V

    return-void
.end method
