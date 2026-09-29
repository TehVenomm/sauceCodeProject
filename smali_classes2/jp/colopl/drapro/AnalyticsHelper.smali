.class public Ljp/colopl/drapro/AnalyticsHelper;
.super Ljava/lang/Object;
.source "AnalyticsHelper.java"


# static fields
.field private static final SETTING_KEY_INSTALL_DATE:Ljava/lang/String; = "install"

.field private static final appid:Ljava/lang/String; = "jp.colopl.drapro"

.field private static tracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker; = null

.field private static final uacode:Ljava/lang/String; = "drapro"

.field private static version:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 17
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static SetReferrer(Ljava/lang/String;)V
    .locals 1

    .line 53
    sget-object v0, Ljp/colopl/drapro/AnalyticsHelper;->tracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    invoke-virtual {v0, p0}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->setReferrer(Ljava/lang/String;)Z

    return-void
.end method

.method public static dispatch()V
    .locals 1

    .line 66
    sget-object v0, Ljp/colopl/drapro/AnalyticsHelper;->tracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    invoke-virtual {v0}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->dispatch()Z

    return-void
.end method

.method public static init(Landroid/app/Activity;)V
    .locals 3

    .line 27
    invoke-static {}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->getInstance()Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    move-result-object v0

    sput-object v0, Ljp/colopl/drapro/AnalyticsHelper;->tracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    .line 28
    sget-object v0, Ljp/colopl/drapro/AnalyticsHelper;->tracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    const-string v1, "drapro"

    const/16 v2, 0x12c

    invoke-virtual {v0, v1, v2, p0}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->startNewSession(Ljava/lang/String;ILandroid/content/Context;)V

    const-string v0, ""

    .line 29
    sput-object v0, Ljp/colopl/drapro/AnalyticsHelper;->version:Ljava/lang/String;

    .line 31
    :try_start_0
    invoke-virtual {p0}, Landroid/app/Activity;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    const-string v1, "jp.colopl.drapro"

    const/16 v2, 0x80

    invoke-virtual {v0, v1, v2}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v0

    iget-object v0, v0, Landroid/content/pm/PackageInfo;->versionName:Ljava/lang/String;

    sput-object v0, Ljp/colopl/drapro/AnalyticsHelper;->version:Ljava/lang/String;
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    .line 36
    :catch_0
    :try_start_1
    invoke-virtual {p0}, Landroid/app/Activity;->getApplicationContext()Landroid/content/Context;

    move-result-object p0

    invoke-static {p0}, Landroid/preference/PreferenceManager;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object p0

    const-string v0, "install"

    const-string v1, ""

    .line 37
    invoke-interface {p0, v0, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v1, ""

    if-ne v0, v1, :cond_0

    const-string v0, "/install"

    .line 40
    invoke-static {v0}, Ljp/colopl/drapro/AnalyticsHelper;->trackPageView(Ljava/lang/String;)V

    .line 41
    invoke-static {}, Ljp/colopl/drapro/AnalyticsHelper;->dispatch()V

    .line 42
    new-instance v0, Ljava/text/SimpleDateFormat;

    const-string v1, "yyMMdd"

    sget-object v2, Ljava/util/Locale;->JAPAN:Ljava/util/Locale;

    invoke-direct {v0, v1, v2}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    .line 43
    new-instance v1, Ljava/util/Date;

    invoke-direct {v1}, Ljava/util/Date;-><init>()V

    invoke-virtual {v0, v1}, Ljava/text/SimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v0

    .line 44
    invoke-interface {p0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object p0

    const-string v1, "install"

    invoke-interface {p0, v1, v0}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    move-result-object p0

    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 47
    :cond_0
    sget-object p0, Ljp/colopl/drapro/AnalyticsHelper;->tracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    const/4 v1, 0x1

    const-string v2, "installDate"

    invoke-virtual {p0, v1, v2, v0}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->setCustomVar(ILjava/lang/String;Ljava/lang/String;)Z
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    return-void
.end method

.method public static stopSession()V
    .locals 1

    .line 70
    sget-object v0, Ljp/colopl/drapro/AnalyticsHelper;->tracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    invoke-virtual {v0}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->stopSession()V

    return-void
.end method

.method public static trackPageView(Ljava/lang/String;)V
    .locals 2

    .line 59
    :try_start_0
    sget-object v0, Ljp/colopl/drapro/AnalyticsHelper;->tracker:Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "/a/"

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object p0, Ljp/colopl/drapro/AnalyticsHelper;->version:Ljava/lang/String;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->trackPageView(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    const-string v0, "Analytics"

    .line 61
    invoke-virtual {p0}, Ljava/lang/Exception;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    :goto_0
    return-void
.end method
