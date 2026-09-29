.class public Lcom/appsflyer/AppsFlyerLib;
.super Ljava/lang/Object;
.source ""

# interfaces
.implements Lcom/appsflyer/b;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/appsflyer/AppsFlyerLib$c;,
        Lcom/appsflyer/AppsFlyerLib$d;,
        Lcom/appsflyer/AppsFlyerLib$a;,
        Lcom/appsflyer/AppsFlyerLib$e;,
        Lcom/appsflyer/AppsFlyerLib$b;
    }
.end annotation


# static fields
.field public static final AF_PRE_INSTALL_PATH:Ljava/lang/String; = "AF_PRE_INSTALL_PATH"

.field public static final ATTRIBUTION_ID_COLUMN_NAME:Ljava/lang/String; = "aid"

.field public static final ATTRIBUTION_ID_CONTENT_URI:Ljava/lang/String; = "content://com.facebook.katana.provider.AttributionIdProvider"

.field public static final IS_STOP_TRACKING_USED:Ljava/lang/String; = "is_stop_tracking_used"

.field public static final LOG_TAG:Ljava/lang/String; = "AppsFlyer_4.8.11"

.field public static final PRE_INSTALL_SYSTEM_DEFAULT:Ljava/lang/String; = "/data/local/tmp/pre_install.appsflyer"

.field public static final PRE_INSTALL_SYSTEM_DEFAULT_ETC:Ljava/lang/String; = "/etc/pre_install.appsflyer"

.field public static final PRE_INSTALL_SYSTEM_RO_PROP:Ljava/lang/String; = "ro.appsflyer.preinstall.path"

.field private static ʼ:Ljava/lang/String;

.field private static final ʽ:Ljava/lang/String;

.field private static ˈ:Lcom/appsflyer/AppsFlyerLib;

.field private static final ˊॱ:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private static final ˋॱ:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field static final ˎ:Ljava/lang/String;

.field static ˏ:Lcom/appsflyer/AppsFlyerInAppPurchaseValidatorListener;

.field private static ˏॱ:Lcom/appsflyer/AppsFlyerConversionListener;

.field static final ॱ:Ljava/lang/String;

.field private static ॱॱ:Ljava/lang/String;

.field private static ᐝ:Ljava/lang/String;


# instance fields
.field private ʻ:J

.field private ʻॱ:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private ʼॱ:Lcom/appsflyer/q$a;

.field private ʽॱ:J

.field private ʾ:J

.field private ʿ:Landroid/net/Uri;

.field private ˉ:Z

.field ˊ:Ljava/lang/String;

.field private ˊˊ:J

.field private ˊˋ:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/Long;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private ˊᐝ:Z

.field ˋ:Ljava/lang/String;

.field private ˋˊ:Ljava/lang/String;

.field private ˋˋ:Z

.field private ˋᐝ:Z

.field private ˌ:Lcom/appsflyer/t;

.field private ˍ:Z

.field private ˎˎ:Z

.field private ͺ:J

.field private ॱˊ:J

.field private ॱˋ:Ljava/util/concurrent/ScheduledExecutorService;

.field private ॱˎ:Lcom/appsflyer/e;

.field private ॱᐝ:Z

.field private ᐝॱ:J


# direct methods
.method static constructor <clinit>()V
    .locals 3

    const-string v0, "4.8.11"

    const-string v1, "4.8.11"

    const-string v2, "."

    .line 76
    invoke-virtual {v1, v2}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v1

    const/4 v2, 0x0

    invoke-virtual {v0, v2, v1}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/appsflyer/AppsFlyerLib;->ॱ:Ljava/lang/String;

    .line 81
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    sget-object v1, Lcom/appsflyer/AppsFlyerLib;->ॱ:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "/androidevent?buildnumber=4.8.11&app_id="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/appsflyer/AppsFlyerLib;->ʽ:Ljava/lang/String;

    .line 82
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "https://attr.%s/api/v"

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    sget-object v1, Lcom/appsflyer/AppsFlyerLib;->ʽ:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/appsflyer/AppsFlyerLib;->ॱॱ:Ljava/lang/String;

    .line 83
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "https://t.%s/api/v"

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    sget-object v1, Lcom/appsflyer/AppsFlyerLib;->ʽ:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/appsflyer/AppsFlyerLib;->ʼ:Ljava/lang/String;

    .line 84
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "https://events.%s/api/v"

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    sget-object v1, Lcom/appsflyer/AppsFlyerLib;->ʽ:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/appsflyer/AppsFlyerLib;->ᐝ:Ljava/lang/String;

    .line 85
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "https://register.%s/api/v"

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    sget-object v1, Lcom/appsflyer/AppsFlyerLib;->ʽ:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/appsflyer/AppsFlyerLib;->ˎ:Ljava/lang/String;

    const-string v0, "is_cache"

    .line 117
    filled-new-array {v0}, [Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v0

    sput-object v0, Lcom/appsflyer/AppsFlyerLib;->ˊॱ:Ljava/util/List;

    const-string v0, "googleplay"

    const-string v1, "playstore"

    const-string v2, "googleplaystore"

    .line 118
    filled-new-array {v0, v1, v2}, [Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v0

    sput-object v0, Lcom/appsflyer/AppsFlyerLib;->ˋॱ:Ljava/util/List;

    const/4 v0, 0x0

    .line 143
    sput-object v0, Lcom/appsflyer/AppsFlyerLib;->ˏॱ:Lcom/appsflyer/AppsFlyerConversionListener;

    .line 144
    sput-object v0, Lcom/appsflyer/AppsFlyerLib;->ˏ:Lcom/appsflyer/AppsFlyerInAppPurchaseValidatorListener;

    .line 161
    new-instance v0, Lcom/appsflyer/AppsFlyerLib;

    invoke-direct {v0}, Lcom/appsflyer/AppsFlyerLib;-><init>()V

    sput-object v0, Lcom/appsflyer/AppsFlyerLib;->ˈ:Lcom/appsflyer/AppsFlyerLib;

    return-void
.end method

.method private constructor <init>()V
    .locals 3

    .line 374
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-wide/16 v0, -0x1

    .line 94
    iput-wide v0, p0, Lcom/appsflyer/AppsFlyerLib;->ʻ:J

    .line 95
    iput-wide v0, p0, Lcom/appsflyer/AppsFlyerLib;->ͺ:J

    .line 140
    sget-object v0, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v1, 0x5

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v0

    iput-wide v0, p0, Lcom/appsflyer/AppsFlyerLib;->ॱˊ:J

    const/4 v0, 0x0

    .line 146
    iput-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ॱˎ:Lcom/appsflyer/e;

    const/4 v1, 0x0

    .line 149
    iput-boolean v1, p0, Lcom/appsflyer/AppsFlyerLib;->ॱᐝ:Z

    .line 151
    iput-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ॱˋ:Ljava/util/concurrent/ScheduledExecutorService;

    .line 167
    iput-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ʿ:Landroid/net/Uri;

    .line 169
    iput-boolean v1, p0, Lcom/appsflyer/AppsFlyerLib;->ˉ:Z

    .line 170
    iput-boolean v1, p0, Lcom/appsflyer/AppsFlyerLib;->ˊᐝ:Z

    .line 175
    new-instance v0, Lcom/appsflyer/t;

    invoke-direct {v0}, Lcom/appsflyer/t;-><init>()V

    iput-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ˌ:Lcom/appsflyer/t;

    .line 176
    iput-boolean v1, p0, Lcom/appsflyer/AppsFlyerLib;->ˋᐝ:Z

    .line 177
    iput-boolean v1, p0, Lcom/appsflyer/AppsFlyerLib;->ˋˋ:Z

    .line 375
    invoke-static {}, Lcom/appsflyer/AFVersionDeclaration;->init()V

    return-void
.end method

.method public static getInstance()Lcom/appsflyer/AppsFlyerLib;
    .locals 1

    .line 381
    sget-object v0, Lcom/appsflyer/AppsFlyerLib;->ˈ:Lcom/appsflyer/AppsFlyerLib;

    return-object v0
.end method

.method private static ˊ(Landroid/content/SharedPreferences;Ljava/lang/String;Z)I
    .locals 1

    const/4 v0, 0x0

    .line 2578
    invoke-interface {p0, p1, v0}, Landroid/content/SharedPreferences;->getInt(Ljava/lang/String;I)I

    move-result v0

    if-eqz p2, :cond_1

    add-int/lit8 v0, v0, 0x1

    .line 2582
    invoke-interface {p0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object p0

    .line 2583
    invoke-interface {p0, p1, v0}, Landroid/content/SharedPreferences$Editor;->putInt(Ljava/lang/String;I)Landroid/content/SharedPreferences$Editor;

    sget p1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 p2, 0x9

    if-lt p1, p2, :cond_0

    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->apply()V

    goto :goto_0

    :cond_0
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 2587
    :cond_1
    :goto_0
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object p0

    invoke-virtual {p0}, Lcom/appsflyer/y;->ʽ()Z

    move-result p0

    if-eqz p0, :cond_2

    .line 2588
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object p0

    invoke-static {v0}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/appsflyer/y;->ˊ(Ljava/lang/String;)V

    :cond_2
    return v0
.end method

.method private static ˊ(Landroid/content/Context;Ljava/lang/String;)Ljava/lang/String;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/content/pm/PackageManager$NameNotFoundException;
        }
    .end annotation

    const-string v0, "appsflyer-data"

    const/4 v1, 0x0

    .line 2511
    invoke-virtual {p0, v0, v1}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v1, "CACHED_CHANNEL"

    .line 2512
    invoke-interface {v0, v1}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    const-string p0, "CACHED_CHANNEL"

    const/4 p1, 0x0

    .line 2513
    invoke-interface {v0, p0, p1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0

    :cond_0
    const-string v0, "CACHED_CHANNEL"

    .line 2516
    invoke-static {p0, v0, p1}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    return-object p1
.end method

.method private static ˊ(Ljava/lang/String;)Ljava/lang/String;
    .locals 7

    const/4 v0, 0x0

    :try_start_0
    const-string v1, "android.os.SystemProperties"

    .line 2307
    invoke-static {v1}, Ljava/lang/Class;->forName(Ljava/lang/String;)Ljava/lang/Class;

    move-result-object v1

    const-string v2, "get"

    const/4 v3, 0x1

    new-array v4, v3, [Ljava/lang/Class;

    const-class v5, Ljava/lang/String;

    const/4 v6, 0x0

    aput-object v5, v4, v6

    .line 2308
    invoke-virtual {v1, v2, v4}, Ljava/lang/Class;->getMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v1

    new-array v2, v3, [Ljava/lang/Object;

    aput-object p0, v2, v6

    invoke-virtual {v1, v0, v2}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    .line 2310
    invoke-virtual {p0}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    move-object p0, v0

    :goto_0
    return-object p0
.end method

.method static synthetic ˊ()Ljava/util/List;
    .locals 1

    .line 71
    sget-object v0, Lcom/appsflyer/AppsFlyerLib;->ˋॱ:Ljava/util/List;

    return-object v0
.end method

.method private static ˊ(Landroid/content/Context;)Ljava/util/Map;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            ")",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/appsflyer/k;
        }
    .end annotation

    const-string v0, "appsflyer-data"

    const/4 v1, 0x0

    .line 1221
    invoke-virtual {p0, v0, v1}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p0

    const-string v0, "attributionId"

    const/4 v1, 0x0

    .line 1227
    invoke-interface {p0, v0, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    if-eqz p0, :cond_0

    .line 1229
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    if-lez v0, :cond_0

    .line 1230
    invoke-static {p0}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Ljava/lang/String;)Ljava/util/Map;

    move-result-object p0

    return-object p0

    .line 1232
    :cond_0
    new-instance p0, Lcom/appsflyer/k;

    invoke-direct {p0}, Lcom/appsflyer/k;-><init>()V

    throw p0
.end method

.method static synthetic ˊ(Lcom/appsflyer/AppsFlyerLib;)Ljava/util/Map;
    .locals 0

    .line 71
    iget-object p0, p0, Lcom/appsflyer/AppsFlyerLib;->ʻॱ:Ljava/util/Map;

    return-object p0
.end method

.method private ˊ(Landroid/app/Application;)V
    .locals 3

    .line 411
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/appsflyer/AppsFlyerProperties;->loadProperties(Landroid/content/Context;)V

    .line 413
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0xe

    if-lt v0, v1, :cond_0

    .line 415
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v0, v1, :cond_1

    .line 417
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ʼॱ:Lcom/appsflyer/q$a;

    if-nez v0, :cond_1

    .line 419
    invoke-static {}, Lcom/appsflyer/q;->ˊ()Lcom/appsflyer/q;

    .line 421
    new-instance v0, Lcom/appsflyer/AppsFlyerLib$3;

    invoke-direct {v0, p0}, Lcom/appsflyer/AppsFlyerLib$3;-><init>(Lcom/appsflyer/AppsFlyerLib;)V

    iput-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ʼॱ:Lcom/appsflyer/q$a;

    .line 435
    invoke-static {}, Lcom/appsflyer/q;->ˏ()Lcom/appsflyer/q;

    move-result-object v0

    iget-object v1, p0, Lcom/appsflyer/AppsFlyerLib;->ʼॱ:Lcom/appsflyer/q$a;

    invoke-virtual {v0, p1, v1}, Lcom/appsflyer/q;->ˋ(Landroid/app/Application;Lcom/appsflyer/q$a;)V

    return-void

    :cond_0
    const-string v0, "SDK<14 call trackEvent manually"

    .line 439
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    const-string v0, "onBecameForeground"

    .line 59794
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    .line 59795
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object v0

    .line 59800
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    iput-wide v1, v0, Lcom/appsflyer/AppsFlyerLib;->ʽॱ:J

    .line 59804
    invoke-static {}, Lcom/appsflyer/AppsFlyerLib;->getInstance()Lcom/appsflyer/AppsFlyerLib;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1, v1}, Lcom/appsflyer/AppsFlyerLib;->ˏ(Landroid/content/Context;Ljava/lang/String;Ljava/util/Map;)V

    .line 59806
    invoke-static {}, Lcom/appsflyer/AFLogger;->resetDeltaTime()V

    :cond_1
    return-void
.end method

.method static synthetic ˊ(Landroid/content/Context;Ljava/lang/String;I)V
    .locals 0

    .line 71
    invoke-static {p0, p1, p2}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Landroid/content/Context;Ljava/lang/String;I)V

    return-void
.end method

.method private static ˊ(Landroid/content/Context;Ljava/lang/String;J)V
    .locals 2

    const-string v0, "appsflyer-data"

    const/4 v1, 0x0

    .line 61828
    invoke-virtual {p0, v0, v1}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p0

    .line 64829
    invoke-interface {p0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object p0

    .line 64830
    invoke-interface {p0, p1, p2, p3}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    sget p1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 p2, 0x9

    if-lt p1, p2, :cond_0

    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void

    :cond_0
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    return-void
.end method

.method static synthetic ˊ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 71
    invoke-static {p0, p1, p2}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method private ˊ(Ljava/net/URL;Ljava/lang/String;Ljava/lang/String;Ljava/lang/ref/WeakReference;Ljava/lang/String;Z)V
    .locals 17
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/net/URL;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/ref/WeakReference<",
            "Landroid/content/Context;",
            ">;",
            "Ljava/lang/String;",
            "Z)V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    move-object/from16 v1, p0

    move-object/from16 v0, p2

    move-object/from16 v2, p3

    move-object/from16 v3, p5

    .line 2657
    invoke-virtual/range {p4 .. p4}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Landroid/content/Context;

    const/4 v6, 0x1

    const/4 v7, 0x0

    if-eqz p6, :cond_0

    .line 2658
    sget-object v8, Lcom/appsflyer/AppsFlyerLib;->ˏॱ:Lcom/appsflyer/AppsFlyerConversionListener;

    if-eqz v8, :cond_0

    const/4 v8, 0x1

    goto :goto_0

    :cond_0
    const/4 v8, 0x0

    :goto_0
    const/4 v9, 0x0

    .line 2661
    :try_start_0
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v10

    invoke-virtual/range {p1 .. p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v11

    invoke-virtual {v10, v11, v0}, Lcom/appsflyer/y;->ˎ(Ljava/lang/String;Ljava/lang/String;)V

    .line 2662
    invoke-virtual/range {p1 .. p1}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v10

    check-cast v10, Ljava/net/HttpURLConnection;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_3

    :try_start_1
    const-string v11, "POST"

    .line 2663
    invoke-virtual {v10, v11}, Ljava/net/HttpURLConnection;->setRequestMethod(Ljava/lang/String;)V

    .line 2664
    invoke-virtual/range {p2 .. p2}, Ljava/lang/String;->getBytes()[B

    move-result-object v11

    array-length v11, v11

    const-string v12, "Content-Length"

    .line 2665
    invoke-static {v11}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v11

    invoke-virtual {v10, v12, v11}, Ljava/net/URLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    const-string v11, "Content-Type"

    const-string v12, "application/json"

    .line 2666
    invoke-virtual {v10, v11, v12}, Ljava/net/URLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    const/16 v11, 0x2710

    .line 2667
    invoke-virtual {v10, v11}, Ljava/net/URLConnection;->setConnectTimeout(I)V

    .line 2668
    invoke-virtual {v10, v6}, Ljava/net/URLConnection;->setDoOutput(Z)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_2

    .line 2673
    :try_start_2
    new-instance v11, Ljava/io/OutputStreamWriter;

    invoke-virtual {v10}, Ljava/net/URLConnection;->getOutputStream()Ljava/io/OutputStream;

    move-result-object v12

    const-string v13, "UTF-8"

    invoke-direct {v11, v12, v13}, Ljava/io/OutputStreamWriter;-><init>(Ljava/io/OutputStream;Ljava/lang/String;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    .line 2674
    :try_start_3
    invoke-virtual {v11, v0}, Ljava/io/Writer;->write(Ljava/lang/String;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    .line 2677
    :try_start_4
    invoke-virtual {v11}, Ljava/io/Writer;->close()V

    .line 2682
    invoke-virtual {v10}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v0

    .line 2684
    invoke-static {v10}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Ljava/net/HttpURLConnection;)Ljava/lang/String;

    move-result-object v11

    .line 2685
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v12

    invoke-virtual/range {p1 .. p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v13

    invoke-virtual {v12, v13, v0, v11}, Lcom/appsflyer/y;->ˎ(Ljava/lang/String;ILjava/lang/String;)V

    const-string v12, "response code: "

    .line 2686
    invoke-static {v0}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v13

    invoke-virtual {v12, v13}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v12

    invoke-static {v12}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    const-string v12, "AppsFlyer_4.8.11"

    const-string v13, "SERVER_RESPONSE_CODE"

    .line 2687
    invoke-static {v0}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v14

    invoke-static {v5, v12, v13, v14}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    const-string v12, "appsflyer-data"

    .line 2688
    invoke-virtual {v5, v12, v7}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v12

    const/16 v13, 0xc8

    if-ne v0, v13, :cond_8

    .line 2693
    invoke-virtual/range {p4 .. p4}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_1

    if-eqz p6, :cond_1

    .line 2694
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v13

    iput-wide v13, v1, Lcom/appsflyer/AppsFlyerLib;->ͺ:J

    :cond_1
    const-string v0, "afUninstallToken"

    .line 2697
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v4

    invoke-virtual {v4, v0}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_2

    const-string v4, "Uninstall Token exists: "

    .line 2699
    invoke-static {v0}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v13

    invoke-virtual {v4, v13}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-static {v4}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    const-string v4, "sentRegisterRequestToAF"

    .line 2701
    invoke-interface {v12, v4, v7}, Landroid/content/SharedPreferences;->getBoolean(Ljava/lang/String;Z)Z

    move-result v4

    if-nez v4, :cond_3

    const-string v4, "Resending Uninstall token to AF servers: "

    .line 2703
    invoke-static {v0}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v13

    invoke-virtual {v4, v13}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-static {v4}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    .line 2704
    new-instance v4, Lcom/appsflyer/d;

    invoke-direct {v4, v0}, Lcom/appsflyer/d;-><init>(Ljava/lang/String;)V

    .line 2705
    invoke-static {v5, v4}, Lcom/appsflyer/u;->ॱ(Landroid/content/Context;Lcom/appsflyer/d;)V

    goto :goto_1

    :cond_2
    const-string v0, "gcmProjectNumber"

    .line 2708
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v4

    invoke-virtual {v4, v0}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_3

    const-string v0, "GCM Project number exists. Fetching token and sending to AF servers"

    .line 2709
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    .line 2710
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, v5}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    new-instance v4, Lcom/appsflyer/u$c;

    invoke-direct {v4, v0}, Lcom/appsflyer/u$c;-><init>(Ljava/lang/ref/WeakReference;)V

    new-array v0, v7, [Ljava/lang/Void;

    invoke-virtual {v4, v0}, Landroid/os/AsyncTask;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    .line 2713
    :cond_3
    :goto_1
    iget-object v0, v1, Lcom/appsflyer/AppsFlyerLib;->ʿ:Landroid/net/Uri;

    if-eqz v0, :cond_4

    .line 2714
    iput-object v9, v1, Lcom/appsflyer/AppsFlyerLib;->ʿ:Landroid/net/Uri;

    :cond_4
    if-eqz v3, :cond_5

    .line 2717
    invoke-static {}, Lcom/appsflyer/cache/CacheManager;->getInstance()Lcom/appsflyer/cache/CacheManager;

    move-result-object v0

    invoke-virtual {v0, v3, v5}, Lcom/appsflyer/cache/CacheManager;->deleteRequest(Ljava/lang/String;Landroid/content/Context;)V

    .line 2719
    :cond_5
    invoke-virtual/range {p4 .. p4}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_7

    if-nez v3, :cond_7

    const-string v0, "sentSuccessfully"

    const-string v3, "true"

    .line 2721
    invoke-static {v5, v0, v3}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    .line 2722
    iget-boolean v0, v1, Lcom/appsflyer/AppsFlyerLib;->ॱᐝ:Z

    if-nez v0, :cond_7

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v3

    iget-wide v13, v1, Lcom/appsflyer/AppsFlyerLib;->ᐝॱ:J

    const/4 v0, 0x0

    sub-long/2addr v3, v13

    const-wide/16 v13, 0x3a98

    cmp-long v0, v3, v13

    if-gez v0, :cond_6

    goto :goto_2

    :cond_6
    iget-object v0, v1, Lcom/appsflyer/AppsFlyerLib;->ॱˋ:Ljava/util/concurrent/ScheduledExecutorService;

    if-nez v0, :cond_7

    invoke-static {}, Lcom/appsflyer/AFExecutor;->getInstance()Lcom/appsflyer/AFExecutor;

    move-result-object v0

    invoke-virtual {v0}, Lcom/appsflyer/AFExecutor;->ॱ()Ljava/util/concurrent/ScheduledThreadPoolExecutor;

    move-result-object v0

    iput-object v0, v1, Lcom/appsflyer/AppsFlyerLib;->ॱˋ:Ljava/util/concurrent/ScheduledExecutorService;

    new-instance v0, Lcom/appsflyer/AppsFlyerLib$c;

    invoke-direct {v0, v1, v5}, Lcom/appsflyer/AppsFlyerLib$c;-><init>(Lcom/appsflyer/AppsFlyerLib;Landroid/content/Context;)V

    iget-object v3, v1, Lcom/appsflyer/AppsFlyerLib;->ॱˋ:Ljava/util/concurrent/ScheduledExecutorService;

    const-wide/16 v13, 0x1

    sget-object v4, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-static {v3, v0, v13, v14, v4}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Ljava/util/concurrent/ScheduledExecutorService;Ljava/lang/Runnable;JLjava/util/concurrent/TimeUnit;)V

    .line 2725
    :cond_7
    :goto_2
    invoke-static {v11}, Lcom/appsflyer/ServerConfigHandler;->ˋ(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    const-string v3, "send_background"

    .line 2727
    invoke-virtual {v0, v3, v7}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v0

    iput-boolean v0, v1, Lcom/appsflyer/AppsFlyerLib;->ˋᐝ:Z

    :cond_8
    const-string v0, "appsflyerConversionDataRequestRetries"

    .line 2730
    invoke-interface {v12, v0, v7}, Landroid/content/SharedPreferences;->getInt(Ljava/lang/String;I)I

    move-result v0

    const-string v3, "appsflyerConversionDataCacheExpiration"

    const-wide/16 v13, 0x0

    .line 2732
    invoke-interface {v12, v3, v13, v14}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v3

    cmp-long v11, v3, v13

    if-eqz v11, :cond_9

    .line 2733
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v15

    const/4 v11, 0x0

    sub-long/2addr v15, v3

    const-wide v3, 0x134fd9000L

    cmp-long v11, v15, v3

    if-lez v11, :cond_9

    const-string v3, "attributionId"

    .line 2734
    invoke-static {v5, v3, v9}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    const-string v3, "appsflyerConversionDataCacheExpiration"

    .line 2735
    invoke-static {v5, v3, v13, v14}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/Context;Ljava/lang/String;J)V

    :cond_9
    const-string v3, "attributionId"

    .line 2738
    invoke-interface {v12, v3, v9}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-nez v3, :cond_a

    if-eqz v2, :cond_a

    if-eqz v8, :cond_a

    sget-object v3, Lcom/appsflyer/AppsFlyerLib;->ˏॱ:Lcom/appsflyer/AppsFlyerConversionListener;

    if-eqz v3, :cond_a

    const/4 v3, 0x5

    if-gt v0, v3, :cond_a

    .line 2740
    invoke-static {}, Lcom/appsflyer/AFExecutor;->getInstance()Lcom/appsflyer/AFExecutor;

    move-result-object v0

    invoke-virtual {v0}, Lcom/appsflyer/AFExecutor;->ॱ()Ljava/util/concurrent/ScheduledThreadPoolExecutor;

    move-result-object v0

    .line 2742
    new-instance v3, Lcom/appsflyer/AppsFlyerLib$a;

    invoke-virtual {v5}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object v4

    invoke-direct {v3, v1, v4, v2, v0}, Lcom/appsflyer/AppsFlyerLib$a;-><init>(Lcom/appsflyer/AppsFlyerLib;Landroid/content/Context;Ljava/lang/String;Ljava/util/concurrent/ScheduledExecutorService;)V

    const-wide/16 v4, 0xa

    .line 2745
    sget-object v2, Ljava/util/concurrent/TimeUnit;->MILLISECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-static {v0, v3, v4, v5, v2}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Ljava/util/concurrent/ScheduledExecutorService;Ljava/lang/Runnable;JLjava/util/concurrent/TimeUnit;)V

    goto :goto_3

    :cond_a
    if-nez v2, :cond_b

    const-string v0, "AppsFlyer dev key is missing."

    .line 2749
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    goto :goto_3

    :cond_b
    if-eqz v8, :cond_d

    .line 2750
    sget-object v0, Lcom/appsflyer/AppsFlyerLib;->ˏॱ:Lcom/appsflyer/AppsFlyerConversionListener;

    if-eqz v0, :cond_d

    const-string v0, "attributionId"

    .line 2752
    invoke-interface {v12, v0, v9}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_d

    const-string v0, "appsFlyerCount"

    invoke-static {v12, v0, v7}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/SharedPreferences;Ljava/lang/String;Z)I

    move-result v0
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    if-le v0, v6, :cond_d

    .line 2757
    :try_start_5
    invoke-static {v5}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/Context;)Ljava/util/Map;

    move-result-object v0
    :try_end_5
    .catch Lcom/appsflyer/k; {:try_start_5 .. :try_end_5} :catch_1
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    if-eqz v0, :cond_d

    :try_start_6
    const-string v2, "is_first_launch"

    .line 2761
    invoke-interface {v0, v2}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_c

    const-string v2, "is_first_launch"

    .line 2762
    invoke-static {v7}, Ljava/lang/Boolean;->toString(Z)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v0, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 2765
    :cond_c
    sget-object v2, Lcom/appsflyer/AppsFlyerLib;->ˏॱ:Lcom/appsflyer/AppsFlyerConversionListener;

    invoke-interface {v2, v0}, Lcom/appsflyer/AppsFlyerConversionListener;->onInstallConversionDataLoaded(Ljava/util/Map;)V
    :try_end_6
    .catch Ljava/lang/Throwable; {:try_start_6 .. :try_end_6} :catch_0
    .catch Lcom/appsflyer/k; {:try_start_6 .. :try_end_6} :catch_1
    .catchall {:try_start_6 .. :try_end_6} :catchall_2

    goto :goto_3

    :catch_0
    move-exception v0

    .line 2767
    :try_start_7
    invoke-virtual {v0}, Ljava/lang/Throwable;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2, v0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_7
    .catch Lcom/appsflyer/k; {:try_start_7 .. :try_end_7} :catch_1
    .catchall {:try_start_7 .. :try_end_7} :catchall_2

    goto :goto_3

    :catch_1
    move-exception v0

    .line 2771
    :try_start_8
    invoke-virtual {v0}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2, v0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_8
    .catchall {:try_start_8 .. :try_end_8} :catchall_2

    :cond_d
    :goto_3
    if-eqz v10, :cond_e

    .line 2776
    invoke-virtual {v10}, Ljava/net/HttpURLConnection;->disconnect()V

    return-void

    :cond_e
    return-void

    :catchall_0
    move-exception v0

    move-object v9, v11

    goto :goto_4

    :catchall_1
    move-exception v0

    :goto_4
    if-eqz v9, :cond_f

    .line 2677
    :try_start_9
    invoke-virtual {v9}, Ljava/io/Writer;->close()V

    :cond_f
    throw v0
    :try_end_9
    .catchall {:try_start_9 .. :try_end_9} :catchall_2

    :catchall_2
    move-exception v0

    goto :goto_5

    :catchall_3
    move-exception v0

    move-object v10, v9

    :goto_5
    if-eqz v10, :cond_10

    .line 2776
    invoke-virtual {v10}, Ljava/net/HttpURLConnection;->disconnect()V

    :cond_10
    throw v0
.end method

.method private static ˊ(Lorg/json/JSONObject;)V
    .locals 14

    .line 254
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 256
    invoke-virtual {p0}, Lorg/json/JSONObject;->keys()Ljava/util/Iterator;

    move-result-object v1

    .line 257
    :catch_0
    :cond_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    const/4 v3, 0x0

    if-eqz v2, :cond_1

    .line 258
    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 263
    :try_start_0
    new-instance v4, Lorg/json/JSONArray;

    invoke-virtual {p0, v2}, Lorg/json/JSONObject;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-direct {v4, v2}, Lorg/json/JSONArray;-><init>(Ljava/lang/String;)V

    .line 264
    :goto_0
    invoke-virtual {v4}, Lorg/json/JSONArray;->length()I

    move-result v2

    if-ge v3, v2, :cond_0

    .line 265
    invoke-virtual {v4, v3}, Lorg/json/JSONArray;->getLong(I)J

    move-result-wide v5

    invoke-static {v5, v6}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 273
    :cond_1
    invoke-static {v0}, Ljava/util/Collections;->sort(Ljava/util/List;)V

    .line 277
    invoke-virtual {p0}, Lorg/json/JSONObject;->keys()Ljava/util/Iterator;

    move-result-object v1

    const/4 v2, 0x0

    :cond_2
    :goto_1
    move-object v4, v2

    .line 278
    :catch_1
    :goto_2
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_5

    if-nez v4, :cond_5

    .line 279
    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    .line 282
    :try_start_1
    new-instance v6, Lorg/json/JSONArray;

    invoke-virtual {p0, v5}, Lorg/json/JSONObject;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/lang/String;

    invoke-direct {v6, v7}, Lorg/json/JSONArray;-><init>(Ljava/lang/String;)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_1

    move-object v7, v4

    const/4 v4, 0x0

    .line 285
    :goto_3
    :try_start_2
    invoke-virtual {v6}, Lorg/json/JSONArray;->length()I

    move-result v8

    if-ge v4, v8, :cond_4

    .line 287
    invoke-virtual {v6, v4}, Lorg/json/JSONArray;->getLong(I)J

    move-result-wide v8

    invoke-interface {v0, v3}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v10

    check-cast v10, Ljava/lang/Long;

    invoke-virtual {v10}, Ljava/lang/Number;->longValue()J

    move-result-wide v10

    cmp-long v12, v8, v10

    if-eqz v12, :cond_2

    .line 288
    invoke-virtual {v6, v4}, Lorg/json/JSONArray;->getLong(I)J

    move-result-wide v8

    const/4 v10, 0x1

    invoke-interface {v0, v10}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v11

    check-cast v11, Ljava/lang/Long;

    invoke-virtual {v11}, Ljava/lang/Number;->longValue()J

    move-result-wide v11

    cmp-long v13, v8, v11

    if-eqz v13, :cond_2

    .line 289
    invoke-virtual {v6, v4}, Lorg/json/JSONArray;->getLong(I)J

    move-result-wide v8

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v11

    sub-int/2addr v11, v10

    invoke-interface {v0, v11}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v10

    check-cast v10, Ljava/lang/Long;

    invoke-virtual {v10}, Ljava/lang/Number;->longValue()J

    move-result-wide v10
    :try_end_2
    .catch Lorg/json/JSONException; {:try_start_2 .. :try_end_2} :catch_2

    cmp-long v7, v8, v10

    if-nez v7, :cond_3

    goto :goto_1

    :cond_3
    add-int/lit8 v4, v4, 0x1

    move-object v7, v5

    goto :goto_3

    :catch_2
    :cond_4
    move-object v4, v7

    goto :goto_2

    :cond_5
    if-eqz v4, :cond_6

    .line 304
    invoke-virtual {p0, v4}, Lorg/json/JSONObject;->remove(Ljava/lang/String;)Ljava/lang/Object;

    :cond_6
    return-void
.end method

.method static synthetic ˊ(Lcom/appsflyer/AppsFlyerLib;Z)Z
    .locals 0

    .line 71
    iput-boolean p1, p0, Lcom/appsflyer/AppsFlyerLib;->ॱᐝ:Z

    return p1
.end method

.method static synthetic ˋ(Lcom/appsflyer/AppsFlyerLib;J)J
    .locals 0

    .line 71
    iput-wide p1, p0, Lcom/appsflyer/AppsFlyerLib;->ᐝॱ:J

    return-wide p1
.end method

.method private static ˋ(Ljava/io/File;Ljava/lang/String;)Ljava/lang/String;
    .locals 4

    const/4 v0, 0x0

    .line 2404
    :try_start_0
    new-instance v1, Ljava/util/Properties;

    invoke-direct {v1}, Ljava/util/Properties;-><init>()V

    .line 2405
    new-instance v2, Ljava/io/FileReader;

    invoke-direct {v2, p0}, Ljava/io/FileReader;-><init>(Ljava/io/File;)V
    :try_end_0
    .catch Ljava/io/FileNotFoundException; {:try_start_0 .. :try_end_0} :catch_4
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_2
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 2406
    :try_start_1
    invoke-virtual {v1, v2}, Ljava/util/Properties;->load(Ljava/io/Reader;)V

    const-string v3, "Found PreInstall property!"

    .line 2407
    invoke-static {v3}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    .line 2408
    invoke-virtual {v1, p1}, Ljava/util/Properties;->getProperty(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1
    :try_end_1
    .catch Ljava/io/FileNotFoundException; {:try_start_1 .. :try_end_1} :catch_5
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 2416
    :try_start_2
    invoke-virtual {v2}, Ljava/io/Reader;->close()V
    :try_end_2
    .catch Ljava/lang/Throwable; {:try_start_2 .. :try_end_2} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    .line 2419
    invoke-virtual {p0}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_0
    return-object p1

    :catch_1
    move-exception p0

    goto :goto_1

    :catchall_0
    move-exception p0

    move-object v2, v0

    goto :goto_3

    :catch_2
    move-exception p0

    move-object v2, v0

    .line 2412
    :goto_1
    :try_start_3
    invoke-virtual {p0}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    if-eqz v2, :cond_0

    .line 2416
    :try_start_4
    invoke-virtual {v2}, Ljava/io/Reader;->close()V
    :try_end_4
    .catch Ljava/lang/Throwable; {:try_start_4 .. :try_end_4} :catch_3

    goto :goto_2

    :catch_3
    move-exception p0

    .line 2419
    invoke-virtual {p0}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_2

    :catch_4
    move-object v2, v0

    .line 2410
    :catch_5
    :try_start_5
    new-instance p1, Ljava/lang/StringBuilder;

    const-string v1, "PreInstall file wasn\'t found: "

    invoke-direct {p1, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p0}, Ljava/io/File;->getAbsolutePath()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {p1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_1

    if-eqz v2, :cond_0

    .line 2416
    :try_start_6
    invoke-virtual {v2}, Ljava/io/Reader;->close()V
    :try_end_6
    .catch Ljava/lang/Throwable; {:try_start_6 .. :try_end_6} :catch_3

    :cond_0
    :goto_2
    return-object v0

    :catchall_1
    move-exception p0

    :goto_3
    if-eqz v2, :cond_1

    :try_start_7
    invoke-virtual {v2}, Ljava/io/Reader;->close()V
    :try_end_7
    .catch Ljava/lang/Throwable; {:try_start_7 .. :try_end_7} :catch_6

    goto :goto_4

    :catch_6
    move-exception p1

    .line 2419
    invoke-virtual {p1}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0, p1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 2420
    :cond_1
    :goto_4
    throw p0
.end method

.method static synthetic ˋ(Ljava/lang/ref/WeakReference;)Ljava/lang/String;
    .locals 2

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "channel"

    invoke-virtual {v0, v1}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_0

    const-string v0, "CHANNEL"

    invoke-static {p0, v0}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Ljava/lang/ref/WeakReference;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    :cond_0
    return-object v0
.end method

.method private static ˋ(Ljava/lang/ref/WeakReference;Ljava/lang/String;)Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/ref/WeakReference<",
            "Landroid/content/Context;",
            ">;",
            "Ljava/lang/String;",
            ")",
            "Ljava/lang/String;"
        }
    .end annotation

    .line 2318
    invoke-virtual {p0}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v0

    if-nez v0, :cond_0

    const/4 p0, 0x0

    return-object p0

    .line 2321
    :cond_0
    invoke-virtual {p0}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    invoke-virtual {p0}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Landroid/content/Context;

    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p0

    invoke-static {p1, v0, p0}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Ljava/lang/String;Landroid/content/pm/PackageManager;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method static synthetic ˋ(Ljava/lang/String;)Ljava/util/Map;
    .locals 0

    .line 71
    invoke-static {p0}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Ljava/lang/String;)Ljava/util/Map;

    move-result-object p0

    return-object p0
.end method

.method static synthetic ˋ(Lcom/appsflyer/AppsFlyerLib;)Ljava/util/concurrent/ScheduledExecutorService;
    .locals 1

    const/4 v0, 0x0

    .line 71
    iput-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ॱˋ:Ljava/util/concurrent/ScheduledExecutorService;

    return-object v0
.end method

.method private static ˋ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 3

    .line 1099
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "shouldMonitor"

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/AppsFlyerProperties;->getBoolean(Ljava/lang/String;Z)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 1101
    new-instance v0, Landroid/content/Intent;

    const-string v1, "com.appsflyer.MonitorBroadcast"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const-string v1, "com.appsflyer.nightvision"

    .line 1102
    invoke-virtual {v0, v1}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    const-string v1, "message"

    .line 1103
    invoke-virtual {v0, v1, p2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p2, "value"

    .line 1104
    invoke-virtual {v0, p2, p3}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p2, "packageName"

    const-string p3, "true"

    .line 1105
    invoke-virtual {v0, p2, p3}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p2, "pid"

    .line 1106
    new-instance p3, Ljava/lang/Integer;

    invoke-static {}, Landroid/os/Process;->myPid()I

    move-result v1

    invoke-direct {p3, v1}, Ljava/lang/Integer;-><init>(I)V

    invoke-virtual {v0, p2, p3}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    const-string p2, "eventIdentifier"

    .line 1107
    invoke-virtual {v0, p2, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p1, "sdk"

    const-string p2, "4.8.11"

    .line 1108
    invoke-virtual {v0, p1, p2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    .line 1110
    invoke-virtual {p0, v0}, Landroid/content/Context;->sendBroadcast(Landroid/content/Intent;)V

    :cond_0
    return-void
.end method

.method private ˋ()Z
    .locals 11

    .line 1409
    iget-wide v0, p0, Lcom/appsflyer/AppsFlyerLib;->ʻ:J

    const-wide/16 v2, 0x0

    cmp-long v4, v0, v2

    const/4 v0, 0x0

    if-lez v4, :cond_1

    .line 1410
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    .line 1411
    iget-wide v3, p0, Lcom/appsflyer/AppsFlyerLib;->ʻ:J

    sub-long/2addr v1, v3

    const-string v3, "yyyy/MM/dd HH:mm:ss.SSS Z"

    .line 1412
    new-instance v4, Ljava/text/SimpleDateFormat;

    sget-object v5, Ljava/util/Locale;->US:Ljava/util/Locale;

    invoke-direct {v4, v3, v5}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    .line 1413
    iget-wide v5, p0, Lcom/appsflyer/AppsFlyerLib;->ʻ:J

    const-string v3, "UTC"

    invoke-static {v3}, Ljava/util/TimeZone;->getTimeZone(Ljava/lang/String;)Ljava/util/TimeZone;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/text/DateFormat;->setTimeZone(Ljava/util/TimeZone;)V

    new-instance v3, Ljava/util/Date;

    invoke-direct {v3, v5, v6}, Ljava/util/Date;-><init>(J)V

    invoke-virtual {v4, v3}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v3

    .line 1414
    iget-wide v5, p0, Lcom/appsflyer/AppsFlyerLib;->ͺ:J

    const-string v7, "UTC"

    invoke-static {v7}, Ljava/util/TimeZone;->getTimeZone(Ljava/lang/String;)Ljava/util/TimeZone;

    move-result-object v7

    invoke-virtual {v4, v7}, Ljava/text/DateFormat;->setTimeZone(Ljava/util/TimeZone;)V

    new-instance v7, Ljava/util/Date;

    invoke-direct {v7, v5, v6}, Ljava/util/Date;-><init>(J)V

    invoke-virtual {v4, v7}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v4

    .line 1416
    iget-wide v5, p0, Lcom/appsflyer/AppsFlyerLib;->ॱˊ:J

    cmp-long v7, v1, v5

    const/4 v5, 0x3

    const/4 v6, 0x2

    const/4 v8, 0x1

    if-gez v7, :cond_0

    invoke-virtual {p0}, Lcom/appsflyer/AppsFlyerLib;->isTrackingStopped()Z

    move-result v7

    if-nez v7, :cond_0

    const-string v7, "Last Launch attempt: %s;\nLast successful Launch event: %s;\nThis launch is blocked: %s ms < %s ms"

    .line 1418
    sget-object v9, Ljava/util/Locale;->US:Ljava/util/Locale;

    const/4 v10, 0x4

    new-array v10, v10, [Ljava/lang/Object;

    aput-object v3, v10, v0

    aput-object v4, v10, v8

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    aput-object v0, v10, v6

    iget-wide v0, p0, Lcom/appsflyer/AppsFlyerLib;->ॱˊ:J

    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    aput-object v0, v10, v5

    invoke-static {v9, v7, v10}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    return v8

    .line 1422
    :cond_0
    invoke-virtual {p0}, Lcom/appsflyer/AppsFlyerLib;->isTrackingStopped()Z

    move-result v7

    if-nez v7, :cond_2

    const-string v7, "Last Launch attempt: %s;\nLast successful Launch event: %s;\nSending launch (+%s ms)"

    .line 1424
    sget-object v9, Ljava/util/Locale;->US:Ljava/util/Locale;

    new-array v5, v5, [Ljava/lang/Object;

    aput-object v3, v5, v0

    aput-object v4, v5, v8

    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    aput-object v1, v5, v6

    invoke-static {v9, v7, v5}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    goto :goto_0

    .line 1428
    :cond_1
    invoke-virtual {p0}, Lcom/appsflyer/AppsFlyerLib;->isTrackingStopped()Z

    move-result v1

    if-nez v1, :cond_2

    const-string v1, "Sending first launch for this session!"

    .line 1429
    invoke-static {v1}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    :cond_2
    :goto_0
    return v0
.end method

.method private static ˋ(Landroid/content/Context;)Z
    .locals 3

    const/4 v0, 0x1

    .line 2173
    :try_start_0
    invoke-static {}, Lcom/google/android/gms/common/GoogleApiAvailability;->getInstance()Lcom/google/android/gms/common/GoogleApiAvailability;

    move-result-object v1

    invoke-virtual {v1, p0}, Lcom/google/android/gms/common/GoogleApiAvailability;->isGooglePlayServicesAvailable(Landroid/content/Context;)I

    move-result v1
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    if-nez v1, :cond_0

    return v0

    :catch_0
    move-exception v1

    const-string v2, "WARNING:  Google play services is unavailable. "

    .line 2179
    invoke-static {v2, v1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_0
    const/4 v1, 0x0

    .line 2183
    :try_start_1
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object p0

    const-string v2, "com.google.android.gms"

    invoke-virtual {p0, v2, v1}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;
    :try_end_1
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_1 .. :try_end_1} :catch_1

    return v0

    :catch_1
    move-exception p0

    const-string v0, "WARNING:  Google Play Services is unavailable. "

    .line 2186
    invoke-static {v0, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    return v1
.end method

.method static synthetic ˎ(Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    invoke-virtual {v0, p0}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method static ˎ(Ljava/net/HttpURLConnection;)Ljava/lang/String;
    .locals 7
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .line 3118
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const/4 v1, 0x0

    .line 3121
    :try_start_0
    invoke-virtual {p0}, Ljava/net/HttpURLConnection;->getErrorStream()Ljava/io/InputStream;

    move-result-object v2

    if-nez v2, :cond_0

    .line 3123
    invoke-virtual {p0}, Ljava/net/URLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v2

    .line 3126
    :cond_0
    new-instance v3, Ljava/io/InputStreamReader;

    invoke-direct {v3, v2}, Ljava/io/InputStreamReader;-><init>(Ljava/io/InputStream;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_2
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 3127
    :try_start_1
    new-instance v2, Ljava/io/BufferedReader;

    invoke-direct {v2, v3}, Ljava/io/BufferedReader;-><init>(Ljava/io/Reader;)V
    :try_end_1
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_2

    .line 3130
    :goto_0
    :try_start_2
    invoke-virtual {v2}, Ljava/io/BufferedReader;->readLine()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 3131
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 v1, 0xa

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;
    :try_end_2
    .catch Ljava/lang/Throwable; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    goto :goto_0

    .line 3138
    :cond_1
    :try_start_3
    invoke-virtual {v2}, Ljava/io/Reader;->close()V

    .line 3141
    :goto_1
    invoke-virtual {v3}, Ljava/io/Reader;->close()V
    :try_end_3
    .catch Ljava/lang/Throwable; {:try_start_3 .. :try_end_3} :catch_3

    goto :goto_3

    :catchall_0
    move-exception p0

    move-object v1, v2

    goto :goto_4

    :catch_0
    move-exception v1

    move-object v6, v2

    move-object v2, v1

    move-object v1, v6

    goto :goto_2

    :catch_1
    move-exception v2

    goto :goto_2

    :catchall_1
    move-exception p0

    move-object v3, v1

    goto :goto_4

    :catch_2
    move-exception v2

    move-object v3, v1

    .line 3134
    :goto_2
    :try_start_4
    new-instance v4, Ljava/lang/StringBuilder;

    const-string v5, "Could not read connection response from: "

    invoke-direct {v4, v5}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p0}, Ljava/net/URLConnection;->getURL()Ljava/net/URL;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v4, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {p0, v2}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_2

    if-eqz v1, :cond_2

    .line 3138
    :try_start_5
    invoke-virtual {v1}, Ljava/io/Reader;->close()V
    :try_end_5
    .catch Ljava/lang/Throwable; {:try_start_5 .. :try_end_5} :catch_3

    :cond_2
    if-eqz v3, :cond_3

    goto :goto_1

    .line 3146
    :catch_3
    :cond_3
    :goto_3
    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p0

    .line 3148
    :try_start_6
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V
    :try_end_6
    .catch Lorg/json/JSONException; {:try_start_6 .. :try_end_6} :catch_4

    return-object p0

    .line 3151
    :catch_4
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    :try_start_7
    const-string v1, "string_response"

    .line 3153
    invoke-virtual {v0, v1, p0}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 3154
    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p0
    :try_end_7
    .catch Lorg/json/JSONException; {:try_start_7 .. :try_end_7} :catch_5

    return-object p0

    .line 3156
    :catch_5
    new-instance p0, Lorg/json/JSONObject;

    invoke-direct {p0}, Lorg/json/JSONObject;-><init>()V

    invoke-virtual {p0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0

    :catchall_2
    move-exception p0

    :goto_4
    if-eqz v1, :cond_4

    .line 3138
    :try_start_8
    invoke-virtual {v1}, Ljava/io/Reader;->close()V

    :cond_4
    if-eqz v3, :cond_5

    .line 3141
    invoke-virtual {v3}, Ljava/io/Reader;->close()V
    :try_end_8
    .catch Ljava/lang/Throwable; {:try_start_8 .. :try_end_8} :catch_6

    .line 3144
    :catch_6
    :cond_5
    throw p0
.end method

.method static synthetic ˎ(Landroid/content/Context;)Ljava/util/Map;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/appsflyer/k;
        }
    .end annotation

    .line 71
    invoke-static {p0}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/Context;)Ljava/util/Map;

    move-result-object p0

    return-object p0
.end method

.method private static ˎ(Landroid/content/Context;Ljava/lang/String;)Ljava/util/Map;
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 1290
    new-instance v0, Ljava/util/LinkedHashMap;

    invoke-direct {v0}, Ljava/util/LinkedHashMap;-><init>()V

    const-string v1, "&"

    .line 1291
    invoke-virtual {p1, v1}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p1

    .line 1294
    array-length v1, p1

    const/4 v2, 0x0

    const/4 v3, 0x0

    const/4 v4, 0x0

    :goto_0
    if-ge v3, v1, :cond_6

    aget-object v5, p1, v3

    const-string v6, "="

    .line 1295
    invoke-virtual {v5, v6}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v6

    if-lez v6, :cond_0

    .line 1296
    invoke-virtual {v5, v2, v6}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v7

    goto :goto_1

    :cond_0
    move-object v7, v5

    .line 1297
    :goto_1
    invoke-interface {v0, v7}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v8

    if-nez v8, :cond_4

    const-string v8, "c"

    .line 1299
    invoke-virtual {v7, v8}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v8

    if-eqz v8, :cond_1

    const-string v7, "campaign"

    goto :goto_2

    :cond_1
    const-string v8, "pid"

    .line 1301
    invoke-virtual {v7, v8}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v8

    if-eqz v8, :cond_2

    const-string v7, "media_source"

    goto :goto_2

    :cond_2
    const-string v8, "af_prt"

    .line 1303
    invoke-virtual {v7, v8}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v8

    if-eqz v8, :cond_3

    const-string v4, "agency"

    const/4 v7, 0x1

    move-object v7, v4

    const/4 v4, 0x1

    :cond_3
    :goto_2
    const-string v8, ""

    .line 1308
    invoke-interface {v0, v7, v8}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_4
    if-lez v6, :cond_5

    .line 1310
    invoke-virtual {v5}, Ljava/lang/String;->length()I

    move-result v8

    add-int/lit8 v6, v6, 0x1

    if-le v8, v6, :cond_5

    invoke-virtual {v5, v6}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v5

    goto :goto_3

    :cond_5
    const/4 v5, 0x0

    .line 1311
    :goto_3
    invoke-interface {v0, v7, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    :cond_6
    :try_start_0
    const-string p1, "install_time"

    .line 1314
    invoke-interface {v0, p1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result p1

    if-nez p1, :cond_7

    .line 1315
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object p1

    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {p1, p0, v2}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object p0

    const-string p1, "install_time"

    const-string v1, "yyyy-MM-dd HH:mm:ss"

    .line 1317
    new-instance v2, Ljava/text/SimpleDateFormat;

    sget-object v3, Ljava/util/Locale;->US:Ljava/util/Locale;

    invoke-direct {v2, v1, v3}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    iget-wide v5, p0, Landroid/content/pm/PackageInfo;->firstInstallTime:J

    const-string p0, "UTC"

    invoke-static {p0}, Ljava/util/TimeZone;->getTimeZone(Ljava/lang/String;)Ljava/util/TimeZone;

    move-result-object p0

    invoke-virtual {v2, p0}, Ljava/text/DateFormat;->setTimeZone(Ljava/util/TimeZone;)V

    new-instance p0, Ljava/util/Date;

    invoke-direct {p0, v5, v6}, Ljava/util/Date;-><init>(J)V

    invoke-virtual {v2, p0}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object p0

    invoke-interface {v0, p1, p0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_4

    :catch_0
    move-exception p0

    const-string p1, "Could not fetch install time. "

    .line 1321
    invoke-static {p1, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_7
    :goto_4
    const-string p0, "af_status"

    .line 1323
    invoke-interface {v0, p0}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result p0

    if-nez p0, :cond_8

    const-string p0, "af_status"

    const-string p1, "Non-organic"

    .line 1324
    invoke-interface {v0, p0, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_8
    if-eqz v4, :cond_9

    const-string p0, "media_source"

    .line 1328
    invoke-interface {v0, p0}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    :cond_9
    return-object v0
.end method

.method private static ˎ(Landroid/content/Context;Ljava/lang/String;I)V
    .locals 2

    const-string v0, "appsflyer-data"

    const/4 v1, 0x0

    .line 60821
    invoke-virtual {p0, v0, v1}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p0

    .line 532
    invoke-interface {p0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object p0

    .line 533
    invoke-interface {p0, p1, p2}, Landroid/content/SharedPreferences$Editor;->putInt(Ljava/lang/String;I)Landroid/content/SharedPreferences$Editor;

    .line 63822
    sget p1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 p2, 0x9

    if-lt p1, p2, :cond_0

    .line 63823
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void

    .line 63825
    :cond_0
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    return-void
.end method

.method static synthetic ˎ(Lcom/appsflyer/AppsFlyerLib;Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZZLandroid/content/Intent;)V
    .locals 16

    move-object/from16 v10, p0

    move-object/from16 v11, p1

    if-nez v11, :cond_0

    const-string v0, "sendTrackingWithEvent - got null context. skipping event/launch."

    .line 71
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    return-void

    :cond_0
    const-string v0, "appsflyer-data"

    const/4 v12, 0x0

    invoke-virtual {v11, v0, v12}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v13

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    invoke-virtual {v0, v13}, Lcom/appsflyer/AppsFlyerProperties;->saveProperties(Landroid/content/SharedPreferences;)V

    invoke-virtual/range {p0 .. p0}, Lcom/appsflyer/AppsFlyerLib;->isTrackingStopped()Z

    move-result v0

    if-nez v0, :cond_1

    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "sendTrackingWithEvent from activity: "

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual/range {p1 .. p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    :cond_1
    const/4 v14, 0x1

    if-nez p3, :cond_2

    const/4 v15, 0x1

    goto :goto_0

    :cond_2
    const/4 v15, 0x0

    :goto_0
    move-object/from16 v0, p0

    move-object/from16 v1, p1

    move-object/from16 v2, p2

    move-object/from16 v3, p3

    move-object/from16 v4, p4

    move-object/from16 v5, p5

    move/from16 v6, p6

    move-object v7, v13

    move v8, v15

    move-object/from16 v9, p8

    invoke-virtual/range {v0 .. v9}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZLandroid/content/SharedPreferences;ZLandroid/content/Intent;)Ljava/util/Map;

    move-result-object v3

    const-string v0, "appsflyerKey"

    invoke-interface {v3, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    if-eqz v0, :cond_9

    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    if-nez v0, :cond_3

    goto/16 :goto_2

    :cond_3
    invoke-virtual/range {p0 .. p0}, Lcom/appsflyer/AppsFlyerLib;->isTrackingStopped()Z

    move-result v0

    if-nez v0, :cond_4

    const-string v0, "AppsFlyerLib.sendTrackingWithEvent"

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    :cond_4
    if-eqz v15, :cond_6

    if-eqz p7, :cond_5

    sget-object v0, Lcom/appsflyer/AppsFlyerLib;->ॱॱ:Ljava/lang/String;

    goto :goto_1

    :cond_5
    sget-object v0, Lcom/appsflyer/AppsFlyerLib;->ʼ:Ljava/lang/String;

    goto :goto_1

    :cond_6
    sget-object v0, Lcom/appsflyer/AppsFlyerLib;->ᐝ:Ljava/lang/String;

    :goto_1
    invoke-static {v0}, Lcom/appsflyer/ServerConfigHandler;->getUrl(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    const-string v0, "appsFlyerCount"

    invoke-static {v13, v0, v12}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/SharedPreferences;Ljava/lang/String;Z)I

    move-result v6

    new-instance v8, Lcom/appsflyer/AppsFlyerLib$e;

    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object v4

    const/4 v7, 0x0

    move-object v0, v8

    move-object/from16 v1, p0

    move v5, v15

    invoke-direct/range {v0 .. v7}, Lcom/appsflyer/AppsFlyerLib$e;-><init>(Lcom/appsflyer/AppsFlyerLib;Ljava/lang/String;Ljava/util/Map;Landroid/content/Context;ZIB)V

    if-eqz v15, :cond_8

    invoke-static/range {p1 .. p1}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Landroid/content/Context;)Z

    move-result v0

    if-eqz v0, :cond_8

    iget-object v0, v10, Lcom/appsflyer/AppsFlyerLib;->ʻॱ:Ljava/util/Map;

    if-eqz v0, :cond_7

    iget-object v0, v10, Lcom/appsflyer/AppsFlyerLib;->ʻॱ:Ljava/util/Map;

    invoke-interface {v0}, Ljava/util/Map;->size()I

    move-result v0

    if-lez v0, :cond_7

    const/4 v12, 0x1

    :cond_7
    if-nez v12, :cond_8

    const-string v0, "Failed to get new referrer, wait ..."

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    invoke-static {}, Lcom/appsflyer/AFExecutor;->getInstance()Lcom/appsflyer/AFExecutor;

    move-result-object v0

    invoke-virtual {v0}, Lcom/appsflyer/AFExecutor;->ॱ()Ljava/util/concurrent/ScheduledThreadPoolExecutor;

    move-result-object v0

    const-wide/16 v1, 0x1f4

    sget-object v3, Ljava/util/concurrent/TimeUnit;->MILLISECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-static {v0, v8, v1, v2, v3}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Ljava/util/concurrent/ScheduledExecutorService;Ljava/lang/Runnable;JLjava/util/concurrent/TimeUnit;)V

    return-void

    :cond_8
    invoke-interface {v8}, Ljava/lang/Runnable;->run()V

    return-void

    :cond_9
    :goto_2
    const-string v0, "Not sending data yet, waiting for dev key"

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    return-void
.end method

.method static synthetic ˎ(Lcom/appsflyer/AppsFlyerLib;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/ref/WeakReference;Ljava/lang/String;Z)V
    .locals 9
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 71
    new-instance v1, Ljava/net/URL;

    invoke-direct {v1, p1}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    new-instance v0, Ljava/lang/StringBuilder;

    const-string v2, "url: "

    invoke-direct {v0, v2}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    const-string v0, "data: "

    invoke-static {p2}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/f$5;->ॱ(Ljava/lang/String;)V

    invoke-virtual {p4}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/content/Context;

    const-string v2, "AppsFlyer_4.8.11"

    const-string v3, "EVENT_DATA"

    invoke-static {v0, v2, v3, p2}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    move-object v0, p0

    move-object v2, p2

    move-object v3, p3

    move-object v4, p4

    move-object v5, p5

    move v6, p6

    :try_start_0
    invoke-direct/range {v0 .. v6}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Ljava/net/URL;Ljava/lang/String;Ljava/lang/String;Ljava/lang/ref/WeakReference;Ljava/lang/String;Z)V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception v0

    const-string v1, "Exception in sendRequestToServer. "

    invoke-static {v1, v0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    const-string v2, "useHttpFallback"

    const/4 v3, 0x0

    invoke-virtual {v1, v2, v3}, Lcom/appsflyer/AppsFlyerProperties;->getBoolean(Ljava/lang/String;Z)Z

    move-result v1

    if-eqz v1, :cond_0

    new-instance v3, Ljava/net/URL;

    const-string v0, "https:"

    const-string v1, "http:"

    invoke-virtual {p1, v0, v1}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {v3, p1}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    move-object v2, p0

    move-object v4, p2

    move-object v5, p3

    move-object v6, p4

    move-object v7, p5

    move v8, p6

    invoke-direct/range {v2 .. v8}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Ljava/net/URL;Ljava/lang/String;Ljava/lang/String;Ljava/lang/ref/WeakReference;Ljava/lang/String;Z)V

    return-void

    :cond_0
    new-instance p0, Ljava/lang/StringBuilder;

    const-string p1, "failed to send requeset to server. "

    invoke-direct {p0, p1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0}, Ljava/lang/Throwable;->getLocalizedMessage()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    invoke-virtual {p4}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Landroid/content/Context;

    invoke-virtual {v0}, Ljava/lang/Throwable;->getLocalizedMessage()Ljava/lang/String;

    move-result-object p1

    const-string p2, "AppsFlyer_4.8.11"

    const-string p3, "ERROR"

    invoke-static {p0, p2, p3, p1}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    throw v0
.end method

.method private static ˎ(Ljava/util/concurrent/ScheduledExecutorService;Ljava/lang/Runnable;JLjava/util/concurrent/TimeUnit;)V
    .locals 1

    if-eqz p0, :cond_0

    .line 2817
    :try_start_0
    invoke-interface {p0}, Ljava/util/concurrent/ScheduledExecutorService;->isShutdown()Z

    move-result v0

    if-nez v0, :cond_0

    .line 2818
    invoke-interface {p0}, Ljava/util/concurrent/ScheduledExecutorService;->isTerminated()Z

    move-result v0

    if-nez v0, :cond_0

    .line 2820
    invoke-interface {p0, p1, p2, p3, p4}, Ljava/util/concurrent/ScheduledExecutorService;->schedule(Ljava/lang/Runnable;JLjava/util/concurrent/TimeUnit;)Ljava/util/concurrent/ScheduledFuture;

    return-void

    :catch_0
    move-exception p0

    goto :goto_0

    :catch_1
    move-exception p0

    goto :goto_1

    :cond_0
    const-string p0, "scheduler is null, shut downed or terminated"

    .line 2823
    invoke-static {p0}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/util/concurrent/RejectedExecutionException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :goto_0
    const-string p1, "scheduleJob failed with Exception"

    .line 2828
    invoke-static {p1, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    return-void

    :goto_1
    const-string p1, "scheduleJob failed with RejectedExecutionException Exception"

    .line 2826
    invoke-static {p1, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    return-void
.end method

.method static synthetic ˎ(Lcom/appsflyer/AppsFlyerLib;)Z
    .locals 0

    .line 71
    iget-boolean p0, p0, Lcom/appsflyer/AppsFlyerLib;->ॱᐝ:Z

    return p0
.end method

.method static ˏ(Landroid/content/SharedPreferences;)I
    .locals 2

    const-string v0, "appsFlyerCount"

    const/4 v1, 0x0

    .line 2568
    invoke-static {p0, v0, v1}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/SharedPreferences;Ljava/lang/String;Z)I

    move-result p0

    return p0
.end method

.method static ˏ(Landroid/content/Context;)Landroid/content/SharedPreferences;
    .locals 2

    const-string v0, "appsflyer-data"

    const/4 v1, 0x0

    .line 2564
    invoke-virtual {p0, v0, v1}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p0

    return-object p0
.end method

.method static synthetic ˏ()Lcom/appsflyer/AppsFlyerConversionListener;
    .locals 1

    .line 71
    sget-object v0, Lcom/appsflyer/AppsFlyerLib;->ˏॱ:Lcom/appsflyer/AppsFlyerConversionListener;

    return-object v0
.end method

.method private static ˏ(Ljava/lang/String;)Ljava/io/File;
    .locals 1

    if-eqz p0, :cond_0

    .line 2431
    :try_start_0
    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    if-lez v0, :cond_0

    .line 2432
    new-instance v0, Ljava/io/File;

    invoke-virtual {p0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, p0}, Ljava/io/File;-><init>(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception p0

    .line 2435
    invoke-virtual {p0}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_0
    const/4 p0, 0x0

    return-object p0
.end method

.method static ˏ(Landroid/content/Context;Ljava/lang/String;)V
    .locals 11

    const-string v0, "received a new (extra) referrer: "

    .line 312
    invoke-static {p1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    .line 316
    :try_start_0
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    const-string v2, "appsflyer-data"

    const/4 v3, 0x0

    .line 318
    invoke-virtual {p0, v2, v3}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v2

    const-string v4, "extraReferrers"

    const/4 v5, 0x0

    .line 319
    invoke-interface {v2, v4, v5}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    if-nez v2, :cond_0

    .line 321
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2}, Lorg/json/JSONObject;-><init>()V

    .line 322
    new-instance v4, Lorg/json/JSONArray;

    invoke-direct {v4}, Lorg/json/JSONArray;-><init>()V

    move-object v10, v4

    move-object v4, v2

    move-object v2, v10

    goto :goto_0

    .line 324
    :cond_0
    new-instance v4, Lorg/json/JSONObject;

    invoke-direct {v4, v2}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 325
    invoke-virtual {v4, p1}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_1

    .line 326
    new-instance v2, Lorg/json/JSONArray;

    invoke-virtual {v4, p1}, Lorg/json/JSONObject;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    invoke-direct {v2, v5}, Lorg/json/JSONArray;-><init>(Ljava/lang/String;)V

    goto :goto_0

    .line 328
    :cond_1
    new-instance v2, Lorg/json/JSONArray;

    invoke-direct {v2}, Lorg/json/JSONArray;-><init>()V

    .line 331
    :goto_0
    invoke-virtual {v2}, Lorg/json/JSONArray;->length()I

    move-result v5

    int-to-long v5, v5

    const-wide/16 v7, 0x5

    cmp-long v9, v5, v7

    if-gez v9, :cond_2

    .line 332
    invoke-virtual {v2, v0, v1}, Lorg/json/JSONArray;->put(J)Lorg/json/JSONArray;

    .line 336
    :cond_2
    invoke-virtual {v4}, Lorg/json/JSONObject;->length()I

    move-result v0

    int-to-long v0, v0

    const-wide/16 v5, 0x4

    cmp-long v7, v0, v5

    if-ltz v7, :cond_3

    .line 337
    invoke-static {v4}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Lorg/json/JSONObject;)V

    .line 340
    :cond_3
    invoke-virtual {v2}, Lorg/json/JSONArray;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v4, p1, v0}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v0, "extraReferrers"

    .line 343
    invoke-virtual {v4}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v1

    const-string v2, "appsflyer-data"

    .line 53753
    invoke-virtual {p0, v2, v3}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p0

    .line 55755
    invoke-interface {p0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object p0

    .line 55756
    invoke-interface {p0, v0, v1}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 56759
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x9

    if-lt v0, v1, :cond_4

    .line 56760
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void

    .line 56762
    :cond_4
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p0

    .line 347
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "Couldn\'t save referrer - "

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, ": "

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    return-void

    :catch_1
    return-void
.end method

.method static synthetic ˏ(Landroid/content/Context;Ljava/lang/String;J)V
    .locals 0

    .line 71
    invoke-static {p0, p1, p2, p3}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/Context;Ljava/lang/String;J)V

    return-void
.end method

.method private ˏ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Landroid/content/Intent;)V
    .locals 15

    .line 1370
    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    const/4 v1, 0x0

    const/4 v2, 0x1

    if-nez p3, :cond_0

    const/4 v3, 0x1

    goto :goto_0

    :cond_0
    const/4 v3, 0x0

    :goto_0
    const-string v4, "waitForCustomerId"

    .line 1371
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v5

    invoke-virtual {v5, v4, v1}, Lcom/appsflyer/AppsFlyerProperties;->getBoolean(Ljava/lang/String;Z)Z

    move-result v4

    if-eqz v4, :cond_1

    const-string v4, "AppUserId"

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v5

    invoke-virtual {v5, v4}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    if-nez v4, :cond_1

    const/4 v1, 0x1

    :cond_1
    if-eqz v1, :cond_2

    const-string v0, "CustomerUserId not set, Tracking is disabled"

    .line 1374
    invoke-static {v0, v2}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;Z)V

    return-void

    :cond_2
    if-eqz v3, :cond_5

    .line 1381
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    const-string v3, "launchProtectEnabled"

    invoke-virtual {v1, v3, v2}, Lcom/appsflyer/AppsFlyerProperties;->getBoolean(Ljava/lang/String;Z)Z

    move-result v1

    if-eqz v1, :cond_3

    .line 1383
    invoke-direct {p0}, Lcom/appsflyer/AppsFlyerLib;->ˋ()Z

    move-result v1

    if-eqz v1, :cond_4

    return-void

    :cond_3
    const-string v1, "Allowing multiple launches within a 5 second time window."

    .line 1387
    invoke-static {v1}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    .line 1389
    :cond_4
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    move-object v14, p0

    iput-wide v1, v14, Lcom/appsflyer/AppsFlyerLib;->ʻ:J

    goto :goto_1

    :cond_5
    move-object v14, p0

    .line 1391
    :goto_1
    invoke-static {}, Lcom/appsflyer/AFExecutor;->getInstance()Lcom/appsflyer/AFExecutor;

    move-result-object v1

    invoke-virtual {v1}, Lcom/appsflyer/AFExecutor;->ॱ()Ljava/util/concurrent/ScheduledThreadPoolExecutor;

    move-result-object v1

    .line 1395
    new-instance v2, Lcom/appsflyer/AppsFlyerLib$b;

    new-instance v5, Ljava/lang/ref/WeakReference;

    invoke-direct {v5, v0}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    const/4 v11, 0x0

    const/4 v13, 0x0

    move-object v3, v2

    move-object v4, p0

    move-object/from16 v6, p2

    move-object/from16 v7, p3

    move-object/from16 v8, p4

    move-object/from16 v9, p5

    move-object v10, v1

    move-object/from16 v12, p6

    invoke-direct/range {v3 .. v13}, Lcom/appsflyer/AppsFlyerLib$b;-><init>(Lcom/appsflyer/AppsFlyerLib;Ljava/lang/ref/WeakReference;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/concurrent/ExecutorService;ZLandroid/content/Intent;B)V

    const-wide/16 v3, 0x96

    .line 1405
    sget-object v0, Ljava/util/concurrent/TimeUnit;->MILLISECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-static {v1, v2, v3, v4, v0}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Ljava/util/concurrent/ScheduledExecutorService;Ljava/lang/Runnable;JLjava/util/concurrent/TimeUnit;)V

    return-void
.end method

.method private ˏ(Landroid/content/Context;Ljava/util/Map;Landroid/net/Uri;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Landroid/net/Uri;",
            ")V"
        }
    .end annotation

    const-string v0, "af_deeplink"

    .line 2035
    invoke-virtual {p3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-interface {p2, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p2, "af_deeplink"

    .line 2038
    invoke-virtual {p3, p2}, Landroid/net/Uri;->getQueryParameter(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    if-eqz p2, :cond_3

    const-string p2, "media_source"

    .line 2040
    invoke-virtual {p3, p2}, Landroid/net/Uri;->getQueryParameter(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    const-string v0, "is_retargeting"

    .line 2041
    invoke-virtual {p3, v0}, Landroid/net/Uri;->getQueryParameter(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "AppsFlyer_Test"

    .line 2043
    invoke-virtual {v1, p2}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result p2

    if-eqz p2, :cond_0

    invoke-static {v0}, Ljava/lang/Boolean;->parseBoolean(Ljava/lang/String;)Z

    move-result p2

    if-eqz p2, :cond_0

    const/4 p2, 0x1

    goto :goto_0

    :cond_0
    const/4 p2, 0x0

    :goto_0
    iput-boolean p2, p0, Lcom/appsflyer/AppsFlyerLib;->ˊᐝ:Z

    .line 2045
    invoke-virtual {p3}, Landroid/net/Uri;->getQuery()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Landroid/content/Context;Ljava/lang/String;)Ljava/util/Map;

    move-result-object p2

    const-string v0, "path"

    .line 2046
    invoke-virtual {p3}, Landroid/net/Uri;->getPath()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_1

    invoke-interface {p2, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    const-string v0, "scheme"

    .line 2047
    invoke-virtual {p3}, Landroid/net/Uri;->getScheme()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_2

    invoke-interface {p2, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_2
    const-string v0, "host"

    .line 2048
    invoke-virtual {p3}, Landroid/net/Uri;->getHost()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_4

    invoke-interface {p2, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_1

    .line 2051
    :cond_3
    new-instance p2, Ljava/util/HashMap;

    invoke-direct {p2}, Ljava/util/HashMap;-><init>()V

    const-string v0, "link"

    .line 2052
    invoke-virtual {p3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-interface {p2, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 2055
    :cond_4
    :goto_1
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    .line 2056
    new-instance p1, Lcom/appsflyer/s;

    invoke-direct {p1, p3, p0}, Lcom/appsflyer/s;-><init>(Landroid/net/Uri;Lcom/appsflyer/AppsFlyerLib;)V

    .line 2057
    new-instance p3, Lcom/appsflyer/OneLinkHttpTask$HttpsUrlConnectionProvider;

    invoke-direct {p3}, Lcom/appsflyer/OneLinkHttpTask$HttpsUrlConnectionProvider;-><init>()V

    invoke-virtual {p1, p3}, Lcom/appsflyer/OneLinkHttpTask;->setConnProvider(Lcom/appsflyer/OneLinkHttpTask$HttpsUrlConnectionProvider;)V

    .line 2058
    invoke-virtual {p1}, Lcom/appsflyer/s;->ˏ()Z

    move-result p3

    if-eqz p3, :cond_5

    .line 2059
    new-instance p3, Lcom/appsflyer/AppsFlyerLib$1;

    invoke-direct {p3, p0, p2, v0}, Lcom/appsflyer/AppsFlyerLib$1;-><init>(Lcom/appsflyer/AppsFlyerLib;Ljava/util/Map;Ljava/lang/ref/WeakReference;)V

    .line 2060
    invoke-virtual {p1, p3}, Lcom/appsflyer/s;->ˏ(Lcom/appsflyer/s$b;)V

    .line 2061
    invoke-static {}, Lcom/appsflyer/AFExecutor;->getInstance()Lcom/appsflyer/AFExecutor;

    move-result-object p2

    invoke-virtual {p2}, Lcom/appsflyer/AFExecutor;->getThreadPoolExecutor()Ljava/util/concurrent/Executor;

    move-result-object p2

    invoke-interface {p2, p1}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    return-void

    .line 2063
    :cond_5
    sget-object p1, Lcom/appsflyer/AppsFlyerLib;->ˏॱ:Lcom/appsflyer/AppsFlyerConversionListener;

    if-eqz p1, :cond_6

    :try_start_0
    sget-object p1, Lcom/appsflyer/AppsFlyerLib;->ˏॱ:Lcom/appsflyer/AppsFlyerConversionListener;

    invoke-interface {p1, p2}, Lcom/appsflyer/AppsFlyerConversionListener;->onAppOpenAttribution(Ljava/util/Map;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p1

    invoke-virtual {p1}, Ljava/lang/Throwable;->getLocalizedMessage()Ljava/lang/String;

    move-result-object p2

    invoke-static {p2, p1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_6
    return-void
.end method

.method static synthetic ˏ(Lcom/appsflyer/AppsFlyerLib;)Z
    .locals 1

    .line 71
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ʻॱ:Ljava/util/Map;

    if-eqz v0, :cond_0

    iget-object p0, p0, Lcom/appsflyer/AppsFlyerLib;->ʻॱ:Ljava/util/Map;

    invoke-interface {p0}, Ljava/util/Map;->size()I

    move-result p0

    if-lez p0, :cond_0

    const/4 p0, 0x1

    return p0

    :cond_0
    const/4 p0, 0x0

    return p0
.end method

.method static synthetic ॱ(Landroid/content/Context;Ljava/lang/String;)Ljava/lang/String;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/content/pm/PackageManager$NameNotFoundException;
        }
    .end annotation

    .line 71
    invoke-static {p0, p1}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/Context;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static ॱ(Ljava/lang/String;Landroid/content/pm/PackageManager;Ljava/lang/String;)Ljava/lang/String;
    .locals 2
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    const/16 v0, 0x80

    const/4 v1, 0x0

    .line 2328
    :try_start_0
    invoke-virtual {p1, p2, v0}, Landroid/content/pm/PackageManager;->getApplicationInfo(Ljava/lang/String;I)Landroid/content/pm/ApplicationInfo;

    move-result-object p1

    .line 2329
    iget-object p1, p1, Landroid/content/pm/PackageItemInfo;->metaData:Landroid/os/Bundle;

    if-eqz p1, :cond_0

    .line 2331
    invoke-virtual {p1, p0}, Landroid/os/Bundle;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 2333
    invoke-virtual {p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    move-object v1, p1

    goto :goto_0

    :catch_0
    move-exception p1

    .line 2337
    new-instance p2, Ljava/lang/StringBuilder;

    const-string v0, "Could not find "

    invoke-direct {p2, v0}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, " value in the manifest"

    invoke-virtual {p2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {p0, p1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_0
    :goto_0
    return-object v1
.end method

.method private static ॱ(Ljava/lang/String;)Ljava/util/Map;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 1346
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 1349
    :try_start_0
    new-instance v1, Lorg/json/JSONObject;

    invoke-direct {v1, p0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 1350
    invoke-virtual {v1}, Lorg/json/JSONObject;->keys()Ljava/util/Iterator;

    move-result-object p0

    .line 1351
    :cond_0
    :goto_0
    invoke-interface {p0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    .line 1352
    invoke-interface {p0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 1353
    sget-object v3, Lcom/appsflyer/AppsFlyerLib;->ˊॱ:Ljava/util/List;

    invoke-interface {v3, v2}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v3

    if-nez v3, :cond_0

    .line 1354
    invoke-virtual {v1, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 1355
    invoke-static {v3}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v4

    if-nez v4, :cond_0

    const-string v4, "null"

    invoke-virtual {v4, v3}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-nez v4, :cond_0

    .line 1356
    invoke-interface {v0, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :cond_1
    return-object v0

    :catch_0
    move-exception p0

    .line 1361
    invoke-virtual {p0}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    const/4 p0, 0x0

    return-object p0
.end method

.method static synthetic ॱ(Lcom/appsflyer/AppsFlyerLib;)Ljava/util/concurrent/ScheduledExecutorService;
    .locals 0

    .line 71
    iget-object p0, p0, Lcom/appsflyer/AppsFlyerLib;->ॱˋ:Ljava/util/concurrent/ScheduledExecutorService;

    return-object p0
.end method

.method private static ॱ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    const-string v0, "appsflyer-data"

    const/4 v1, 0x0

    .line 59814
    invoke-virtual {p0, v0, v1}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p0

    .line 517
    invoke-interface {p0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object p0

    .line 518
    invoke-interface {p0, p1, p2}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 62815
    sget p1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 p2, 0x9

    if-lt p1, p2, :cond_0

    .line 62816
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void

    .line 62818
    :cond_0
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    return-void
.end method

.method private static ॱ(Landroid/content/Context;Ljava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "-",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    sget-object v0, Lcom/appsflyer/g$c;->ˏ:Lcom/appsflyer/g;

    .line 1968
    invoke-static {p0}, Lcom/appsflyer/g;->ˊ(Landroid/content/Context;)Lcom/appsflyer/g$a;

    move-result-object p0

    const-string v0, "network"

    .line 1969
    invoke-virtual {p0}, Lcom/appsflyer/g$a;->ॱ()Ljava/lang/String;

    move-result-object v1

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1970
    invoke-virtual {p0}, Lcom/appsflyer/g$a;->ˎ()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    const-string v0, "operator"

    .line 1971
    invoke-virtual {p0}, Lcom/appsflyer/g$a;->ˎ()Ljava/lang/String;

    move-result-object v1

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1973
    :cond_0
    invoke-virtual {p0}, Lcom/appsflyer/g$a;->ˊ()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_1

    const-string v0, "carrier"

    .line 1974
    invoke-virtual {p0}, Lcom/appsflyer/g$a;->ˊ()Ljava/lang/String;

    move-result-object p0

    invoke-interface {p1, v0, p0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1
    return-void
.end method

.method static synthetic ॱ(Ljava/util/Map;)V
    .locals 1

    sget-object v0, Lcom/appsflyer/AppsFlyerLib;->ˏॱ:Lcom/appsflyer/AppsFlyerConversionListener;

    if-eqz v0, :cond_0

    :try_start_0
    sget-object v0, Lcom/appsflyer/AppsFlyerLib;->ˏॱ:Lcom/appsflyer/AppsFlyerConversionListener;

    invoke-interface {v0, p0}, Lcom/appsflyer/AppsFlyerConversionListener;->onAppOpenAttribution(Ljava/util/Map;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p0

    invoke-virtual {p0}, Ljava/lang/Throwable;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_0
    return-void
.end method

.method private static ॱ(Landroid/content/Context;)Z
    .locals 3
    .param p0    # Landroid/content/Context;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    const-string v0, "appsflyer-data"

    const/4 v1, 0x0

    .line 834
    invoke-virtual {p0, v0, v1}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v2, "appsFlyerCount"

    invoke-static {v0, v2, v1}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/SharedPreferences;Ljava/lang/String;Z)I

    move-result v0

    const/4 v2, 0x2

    if-le v0, v2, :cond_0

    const-string p0, "Install referrer will not load, the counter > 2, "

    .line 838
    invoke-static {p0}, Lcom/appsflyer/AFLogger;->afRDLog(Ljava/lang/String;)V

    return v1

    :cond_0
    :try_start_0
    const-string v0, "com.android.installreferrer.api.InstallReferrerClient"

    .line 843
    invoke-static {v0}, Ljava/lang/Class;->forName(Ljava/lang/String;)Ljava/lang/Class;

    const-string v0, "com.google.android.finsky.permission.BIND_GET_INSTALL_REFERRER_SERVICE"

    .line 844
    invoke-static {p0, v0}, Lcom/appsflyer/g$a;->ˎ(Landroid/content/Context;Ljava/lang/String;)Z

    move-result p0

    if-eqz p0, :cond_1

    const-string p0, "Install referrer is allowed"

    .line 845
    invoke-static {p0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/ClassNotFoundException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    const/4 p0, 0x1

    return p0

    :cond_1
    const-string p0, "Install referrer is not allowed"

    .line 855
    invoke-static {p0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    return v1

    :catch_0
    move-exception p0

    const-string v0, "An error occurred while trying to verify manifest : com.android.installreferrer.api.InstallReferrerClient"

    .line 852
    invoke-static {v0, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    return v1

    :catch_1
    const-string p0, "Class com.android.installreferrer.api.InstallReferrerClient not found"

    .line 849
    invoke-static {p0}, Lcom/appsflyer/AFLogger;->afRDLog(Ljava/lang/String;)V

    return v1
.end method

.method private static ॱॱ(Landroid/content/Context;)F
    .locals 3

    .line 3218
    :try_start_0
    invoke-virtual {p0}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p0

    const/4 v0, 0x0

    new-instance v1, Landroid/content/IntentFilter;

    const-string v2, "android.intent.action.BATTERY_CHANGED"

    invoke-direct {v1, v2}, Landroid/content/IntentFilter;-><init>(Ljava/lang/String;)V

    invoke-virtual {p0, v0, v1}, Landroid/content/Context;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)Landroid/content/Intent;

    move-result-object p0

    const-string v0, "level"

    const/4 v1, -0x1

    .line 3219
    invoke-virtual {p0, v0, v1}, Landroid/content/Intent;->getIntExtra(Ljava/lang/String;I)I

    move-result v0

    const-string v2, "scale"

    .line 3220
    invoke-virtual {p0, v2, v1}, Landroid/content/Intent;->getIntExtra(Ljava/lang/String;I)I

    move-result p0
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    if-eq v0, v1, :cond_1

    if-ne p0, v1, :cond_0

    goto :goto_0

    :cond_0
    int-to-float v0, v0

    int-to-float p0, p0

    div-float/2addr v0, p0

    const/high16 p0, 0x42c80000    # 100.0f

    mul-float v0, v0, p0

    goto :goto_1

    :cond_1
    :goto_0
    const/high16 p0, 0x42480000    # 50.0f

    return p0

    :catch_0
    move-exception p0

    .line 3230
    invoke-virtual {p0}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0, p0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    const/high16 v0, 0x3f800000    # 1.0f

    :goto_1
    return v0
.end method


# virtual methods
.method public enableLocationCollection(Z)Lcom/appsflyer/AppsFlyerLib;
    .locals 0

    .line 507
    iput-boolean p1, p0, Lcom/appsflyer/AppsFlyerLib;->ˉ:Z

    return-object p0
.end method

.method public enableUninstallTracking(Ljava/lang/String;)V
    .locals 4

    .line 478
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "enableUninstallTracking"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const-string v0, "gcmProjectNumber"

    .line 59808
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public getAppsFlyerUID(Landroid/content/Context;)Ljava/lang/String;
    .locals 3

    .line 2620
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "getAppsFlyerUID"

    const/4 v2, 0x0

    new-array v2, v2, [Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 2621
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    invoke-static {v0}, Lcom/appsflyer/p;->ˋ(Ljava/lang/ref/WeakReference;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getAttributionId(Landroid/content/ContentResolver;)Ljava/lang/String;
    .locals 7

    const-string v0, "aid"

    .line 2540
    filled-new-array {v0}, [Ljava/lang/String;

    move-result-object v3

    const-string v0, "content://com.facebook.katana.provider.AttributionIdProvider"

    .line 2541
    invoke-static {v0}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v2

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v6, 0x0

    move-object v1, p1

    invoke-virtual/range {v1 .. v6}, Landroid/content/ContentResolver;->query(Landroid/net/Uri;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1

    const/4 v0, 0x0

    if-eqz p1, :cond_4

    .line 2544
    :try_start_0
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v1

    if-nez v1, :cond_0

    goto :goto_4

    :cond_0
    const-string v1, "aid"

    .line 2547
    invoke-interface {p1, v1}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v1

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz p1, :cond_1

    .line 2554
    :try_start_1
    invoke-interface {p1}, Landroid/database/Cursor;->close()V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 2557
    invoke-virtual {p1}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0, p1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_1
    :goto_0
    move-object v0, v1

    goto :goto_1

    :catchall_0
    move-exception v0

    goto :goto_2

    :catch_1
    move-exception v1

    :try_start_2
    const-string v2, "Could not collect cursor attribution. "

    .line 2550
    invoke-static {v2, v1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    if-eqz p1, :cond_2

    .line 2554
    :try_start_3
    invoke-interface {p1}, Landroid/database/Cursor;->close()V
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_2

    goto :goto_1

    :catch_2
    move-exception p1

    .line 2557
    invoke-virtual {p1}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1, p1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_2
    :goto_1
    return-object v0

    :goto_2
    if-eqz p1, :cond_3

    .line 2554
    :try_start_4
    invoke-interface {p1}, Landroid/database/Cursor;->close()V
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_3

    goto :goto_3

    :catch_3
    move-exception p1

    .line 2557
    invoke-virtual {p1}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1, p1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 2558
    :cond_3
    :goto_3
    throw v0

    :cond_4
    :goto_4
    if-eqz p1, :cond_5

    .line 2554
    :try_start_5
    invoke-interface {p1}, Landroid/database/Cursor;->close()V
    :try_end_5
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_4

    goto :goto_5

    :catch_4
    move-exception p1

    .line 2557
    invoke-virtual {p1}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1, p1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_5
    :goto_5
    return-object v0
.end method

.method protected getConversionData(Landroid/content/Context;Lcom/appsflyer/ConversionDataListener;)V
    .locals 0

    .line 1269
    new-instance p1, Lcom/appsflyer/AppsFlyerLib$2;

    invoke-direct {p1, p0, p2}, Lcom/appsflyer/AppsFlyerLib$2;-><init>(Lcom/appsflyer/AppsFlyerLib;Lcom/appsflyer/ConversionDataListener;)V

    sput-object p1, Lcom/appsflyer/AppsFlyerLib;->ˏॱ:Lcom/appsflyer/AppsFlyerConversionListener;

    return-void
.end method

.method public getHost()Ljava/lang/String;
    .locals 2

    const-string v0, "custom_host"

    .line 3247
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    return-object v0

    :cond_0
    const-string v0, "appsflyer.com"

    return-object v0
.end method

.method public getSdkVersion()Ljava/lang/String;
    .locals 3

    .line 394
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "getSdkVersion"

    const/4 v2, 0x0

    new-array v2, v2, [Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const-string v0, "version: 4.8.11 (build 383)"

    return-object v0
.end method

.method public init(Ljava/lang/String;Lcom/appsflyer/AppsFlyerConversionListener;)Lcom/appsflyer/AppsFlyerLib;
    .locals 7

    .line 793
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "init"

    const/4 v2, 0x2

    new-array v3, v2, [Ljava/lang/String;

    const/4 v4, 0x0

    aput-object p1, v3, v4

    if-nez p2, :cond_0

    const-string v5, "null"

    goto :goto_0

    :cond_0
    const-string v5, "conversionDataListener"

    :goto_0
    const/4 v6, 0x1

    aput-object v5, v3, v6

    invoke-virtual {v0, v1, v3}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const-string v0, "Initializing AppsFlyer SDK: (v%s.%s)"

    .line 794
    new-array v1, v2, [Ljava/lang/Object;

    const-string v2, "4.8.11"

    aput-object v2, v1, v4

    const-string v2, "383"

    aput-object v2, v1, v6

    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->ˎ(Ljava/lang/String;)V

    .line 795
    iput-boolean v6, p0, Lcom/appsflyer/AppsFlyerLib;->ˎˎ:Z

    const-string v0, "AppsFlyerKey"

    .line 796
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    .line 797
    invoke-static {p1}, Lcom/appsflyer/f$5;->ˊ(Ljava/lang/String;)V

    .line 798
    sput-object p2, Lcom/appsflyer/AppsFlyerLib;->ˏॱ:Lcom/appsflyer/AppsFlyerConversionListener;

    return-object p0
.end method

.method public init(Ljava/lang/String;Lcom/appsflyer/AppsFlyerConversionListener;Landroid/content/Context;)Lcom/appsflyer/AppsFlyerLib;
    .locals 1

    if-eqz p3, :cond_1

    .line 817
    invoke-static {p3}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Landroid/content/Context;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 820
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ॱˎ:Lcom/appsflyer/e;

    if-nez v0, :cond_0

    .line 821
    new-instance v0, Lcom/appsflyer/e;

    invoke-direct {v0}, Lcom/appsflyer/e;-><init>()V

    iput-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ॱˎ:Lcom/appsflyer/e;

    .line 822
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ॱˎ:Lcom/appsflyer/e;

    invoke-virtual {v0, p3, p0}, Lcom/appsflyer/e;->ˏ(Landroid/content/Context;Lcom/appsflyer/b;)V

    goto :goto_0

    :cond_0
    const-string p3, "AFInstallReferrer instance already created"

    .line 825
    invoke-static {p3}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    .line 829
    :cond_1
    :goto_0
    invoke-virtual {p0, p1, p2}, Lcom/appsflyer/AppsFlyerLib;->init(Ljava/lang/String;Lcom/appsflyer/AppsFlyerConversionListener;)Lcom/appsflyer/AppsFlyerLib;

    move-result-object p1

    return-object p1
.end method

.method public isPreInstalledApp(Landroid/content/Context;)Z
    .locals 2

    const/4 v0, 0x0

    .line 2496
    :try_start_0
    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1, v0}, Landroid/content/pm/PackageManager;->getApplicationInfo(Ljava/lang/String;I)Landroid/content/pm/ApplicationInfo;

    move-result-object p1

    .line 2501
    iget p1, p1, Landroid/content/pm/ApplicationInfo;->flags:I
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    const/4 v1, 0x1

    and-int/2addr p1, v1

    if-eqz p1, :cond_0

    return v1

    :catch_0
    move-exception p1

    const-string v1, "Could not check if app is pre installed"

    .line 2505
    invoke-static {v1, p1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_0
    return v0
.end method

.method public isTrackingStopped()Z
    .locals 1

    .line 2838
    iget-boolean v0, p0, Lcom/appsflyer/AppsFlyerLib;->ˋˋ:Z

    return v0
.end method

.method public onHandleReferrer(Ljava/util/Map;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 2834
    iput-object p1, p0, Lcom/appsflyer/AppsFlyerLib;->ʻॱ:Ljava/util/Map;

    return-void
.end method

.method public onPause(Landroid/content/Context;)V
    .locals 1

    .line 404
    invoke-static {p1}, Lcom/appsflyer/d;->ˋ(Landroid/content/Context;)V

    .line 405
    invoke-static {p1}, Lcom/appsflyer/f;->ˏ(Landroid/content/Context;)Lcom/appsflyer/f;

    move-result-object p1

    .line 58792
    iget-object v0, p1, Lcom/appsflyer/f;->ˋ:Landroid/os/Handler;

    iget-object p1, p1, Lcom/appsflyer/f;->ʼ:Ljava/lang/Runnable;

    invoke-virtual {v0, p1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method public registerConversionListener(Landroid/content/Context;Lcom/appsflyer/AppsFlyerConversionListener;)V
    .locals 2

    .line 1237
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object p1

    const-string v0, "registerConversionListener"

    const/4 v1, 0x0

    new-array v1, v1, [Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    if-eqz p2, :cond_0

    .line 1238
    sput-object p2, Lcom/appsflyer/AppsFlyerLib;->ˏॱ:Lcom/appsflyer/AppsFlyerConversionListener;

    :cond_0
    return-void
.end method

.method public registerValidatorListener(Landroid/content/Context;Lcom/appsflyer/AppsFlyerInAppPurchaseValidatorListener;)V
    .locals 2

    .line 1255
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object p1

    const-string v0, "registerValidatorListener"

    const/4 v1, 0x0

    new-array v1, v1, [Ljava/lang/String;

    invoke-virtual {p1, v0, v1}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const-string p1, "registerValidatorListener called"

    .line 1257
    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    if-nez p2, :cond_0

    const-string p1, "registerValidatorListener null listener"

    .line 1260
    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    return-void

    .line 1263
    :cond_0
    sput-object p2, Lcom/appsflyer/AppsFlyerLib;->ˏ:Lcom/appsflyer/AppsFlyerInAppPurchaseValidatorListener;

    return-void
.end method

.method public reportTrackSession(Landroid/content/Context;)V
    .locals 3

    .line 1074
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "reportTrackSession"

    const/4 v2, 0x0

    new-array v2, v2, [Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 1076
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    invoke-virtual {v0}, Lcom/appsflyer/y;->ᐝ()V

    const/4 v0, 0x0

    .line 1077
    invoke-virtual {p0, p1, v0, v0}, Lcom/appsflyer/AppsFlyerLib;->ˏ(Landroid/content/Context;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public sendDeepLinkData(Landroid/app/Activity;)V
    .locals 6

    const/4 v0, 0x1

    const/4 v1, 0x0

    const/4 v2, 0x2

    if-eqz p1, :cond_0

    .line 646
    invoke-virtual {p1}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v3

    if-eqz v3, :cond_0

    .line 647
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v3

    const-string v4, "sendDeepLinkData"

    new-array v2, v2, [Ljava/lang/String;

    invoke-virtual {p1}, Landroid/app/Activity;->getLocalClassName()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v2, v1

    new-instance v1, Ljava/lang/StringBuilder;

    const-string v5, "activity_intent_"

    invoke-direct {v1, v5}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p1}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    aput-object v1, v2, v0

    invoke-virtual {v3, v4, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    goto :goto_0

    :cond_0
    if-eqz p1, :cond_1

    .line 649
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v3

    const-string v4, "sendDeepLinkData"

    new-array v2, v2, [Ljava/lang/String;

    invoke-virtual {p1}, Landroid/app/Activity;->getLocalClassName()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v2, v1

    const-string v1, "activity_intent_null"

    aput-object v1, v2, v0

    invoke-virtual {v3, v4, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    goto :goto_0

    .line 651
    :cond_1
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "sendDeepLinkData"

    const-string v2, "activity_null"

    filled-new-array {v2}, [Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 654
    :goto_0
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "getDeepLinkData with activity "

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p1}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/Intent;->getDataString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    .line 655
    invoke-virtual {p1}, Landroid/app/Activity;->getApplication()Landroid/app/Application;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/app/Application;)V

    return-void
.end method

.method public sendPushNotificationData(Landroid/app/Activity;)V
    .locals 14

    const/4 v0, 0x1

    const/4 v1, 0x0

    const/4 v2, 0x2

    if-eqz p1, :cond_0

    .line 659
    invoke-virtual {p1}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v3

    if-eqz v3, :cond_0

    .line 660
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v3

    const-string v4, "sendPushNotificationData"

    new-array v5, v2, [Ljava/lang/String;

    invoke-virtual {p1}, Landroid/app/Activity;->getLocalClassName()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v1

    new-instance v1, Ljava/lang/StringBuilder;

    const-string v6, "activity_intent_"

    invoke-direct {v1, v6}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p1}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v6

    invoke-virtual {v6}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v1, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    aput-object v1, v5, v0

    invoke-virtual {v3, v4, v5}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    goto :goto_0

    :cond_0
    if-eqz p1, :cond_1

    .line 662
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v3

    const-string v4, "sendPushNotificationData"

    new-array v5, v2, [Ljava/lang/String;

    invoke-virtual {p1}, Landroid/app/Activity;->getLocalClassName()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v1

    const-string v1, "activity_intent_null"

    aput-object v1, v5, v0

    invoke-virtual {v3, v4, v5}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    goto :goto_0

    .line 664
    :cond_1
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "sendPushNotificationData"

    const-string v3, "activity_null"

    filled-new-array {v3}, [Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v3}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 666
    :goto_0
    instance-of v0, p1, Landroid/app/Activity;

    const/4 v1, 0x0

    if-eqz v0, :cond_2

    move-object v0, p1

    check-cast v0, Landroid/app/Activity;

    invoke-virtual {v0}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v3

    if-eqz v3, :cond_2

    invoke-virtual {v3}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v4

    if-eqz v4, :cond_2

    const-string v5, "af"

    invoke-virtual {v4, v5}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    if-eqz v5, :cond_3

    const-string v6, "Push Notification received af payload = "

    invoke-static {v5}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    invoke-static {v6}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    const-string v6, "af"

    invoke-virtual {v4, v6}, Landroid/os/Bundle;->remove(Ljava/lang/String;)V

    invoke-virtual {v3, v4}, Landroid/content/Intent;->putExtras(Landroid/os/Bundle;)Landroid/content/Intent;

    move-result-object v3

    invoke-virtual {v0, v3}, Landroid/app/Activity;->setIntent(Landroid/content/Intent;)V

    goto :goto_1

    :cond_2
    move-object v5, v1

    :cond_3
    :goto_1
    iput-object v5, p0, Lcom/appsflyer/AppsFlyerLib;->ˋˊ:Ljava/lang/String;

    .line 667
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ˋˊ:Ljava/lang/String;

    if-eqz v0, :cond_a

    .line 668
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v3

    .line 670
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ˊˋ:Ljava/util/Map;

    if-nez v0, :cond_4

    const-string v0, "pushes: initializing pushes history.."

    .line 671
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    .line 672
    new-instance v0, Ljava/util/concurrent/ConcurrentHashMap;

    invoke-direct {v0}, Ljava/util/concurrent/ConcurrentHashMap;-><init>()V

    iput-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ˊˋ:Ljava/util/Map;

    move-wide v7, v3

    goto/16 :goto_4

    .line 675
    :cond_4
    :try_start_0
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v5, "pushPayloadMaxAging"

    const-wide/32 v6, 0x1b7740

    invoke-virtual {v0, v5, v6, v7}, Lcom/appsflyer/AppsFlyerProperties;->getLong(Ljava/lang/String;J)J

    move-result-wide v5

    .line 676
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ˊˋ:Ljava/util/Map;

    invoke-interface {v0}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_1

    move-wide v7, v3

    :cond_5
    :goto_2
    :try_start_1
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v9

    if-eqz v9, :cond_8

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Ljava/lang/Long;

    .line 678
    new-instance v10, Lorg/json/JSONObject;

    iget-object v11, p0, Lcom/appsflyer/AppsFlyerLib;->ˋˊ:Ljava/lang/String;

    invoke-direct {v10, v11}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 679
    new-instance v11, Lorg/json/JSONObject;

    iget-object v12, p0, Lcom/appsflyer/AppsFlyerLib;->ˊˋ:Ljava/util/Map;

    invoke-interface {v12, v9}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v12

    check-cast v12, Ljava/lang/String;

    invoke-direct {v11, v12}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v12, "pid"

    .line 680
    invoke-virtual {v10, v12}, Lorg/json/JSONObject;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v12

    const-string v13, "pid"

    invoke-virtual {v11, v13}, Lorg/json/JSONObject;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v13

    invoke-virtual {v12, v13}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v12

    if-eqz v12, :cond_6

    .line 681
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v5, "PushNotificationMeasurement: A previous payload with same PID was already acknowledged! (old: "

    invoke-direct {v0, v5}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, v11}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v5, ", new: "

    invoke-virtual {v0, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v5, ")"

    invoke-virtual {v0, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    .line 682
    iput-object v1, p0, Lcom/appsflyer/AppsFlyerLib;->ˋˊ:Ljava/lang/String;

    return-void

    .line 687
    :cond_6
    invoke-virtual {v9}, Ljava/lang/Number;->longValue()J

    move-result-wide v10

    const/4 v12, 0x0

    sub-long v10, v3, v10

    cmp-long v12, v10, v5

    if-lez v12, :cond_7

    .line 688
    iget-object v10, p0, Lcom/appsflyer/AppsFlyerLib;->ˊˋ:Ljava/util/Map;

    invoke-interface {v10, v9}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 692
    :cond_7
    invoke-virtual {v9}, Ljava/lang/Number;->longValue()J

    move-result-wide v10

    cmp-long v12, v10, v7

    if-gtz v12, :cond_5

    .line 693
    invoke-virtual {v9}, Ljava/lang/Number;->longValue()J

    move-result-wide v9
    :try_end_1
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_0

    move-wide v7, v9

    goto :goto_2

    :catch_0
    move-exception v0

    goto :goto_3

    :catch_1
    move-exception v0

    move-wide v7, v3

    .line 697
    :goto_3
    new-instance v1, Ljava/lang/StringBuilder;

    const-string v5, "Error while handling push notification measurement: "

    invoke-direct {v1, v5}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1, v0}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 702
    :cond_8
    :goto_4
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "pushPayloadHistorySize"

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/AppsFlyerProperties;->getInt(Ljava/lang/String;I)I

    move-result v0

    .line 703
    iget-object v1, p0, Lcom/appsflyer/AppsFlyerLib;->ˊˋ:Ljava/util/Map;

    invoke-interface {v1}, Ljava/util/Map;->size()I

    move-result v1

    if-ne v1, v0, :cond_9

    .line 704
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "pushes: removing oldest overflowing push (oldest push:"

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, v7, v8}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    const-string v1, ")"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    .line 705
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ˊˋ:Ljava/util/Map;

    invoke-static {v7, v8}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 707
    :cond_9
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ˊˋ:Ljava/util/Map;

    invoke-static {v3, v4}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    iget-object v2, p0, Lcom/appsflyer/AppsFlyerLib;->ˋˊ:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 710
    invoke-virtual {p1}, Landroid/app/Activity;->getApplication()Landroid/app/Application;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/app/Application;)V

    :cond_a
    return-void
.end method

.method public setAdditionalData(Ljava/util/HashMap;)V
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    if-eqz p1, :cond_0

    .line 639
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setAdditionalData"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    invoke-virtual {p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v4

    aput-object v4, v2, v3

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 640
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/util/Map;)V

    .line 641
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object p1

    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Lcom/appsflyer/AppsFlyerProperties;->setCustomData(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public setAndroidIdData(Ljava/lang/String;)V
    .locals 4

    .line 502
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setAndroidIdData"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 503
    iput-object p1, p0, Lcom/appsflyer/AppsFlyerLib;->ˋ:Ljava/lang/String;

    return-void
.end method

.method public setAppId(Ljava/lang/String;)V
    .locals 4

    .line 935
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setAppId"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const-string v0, "appid"

    .line 936
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public setAppInviteOneLink(Ljava/lang/String;)V
    .locals 4

    .line 627
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setAppInviteOneLink"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const-string v0, "setAppInviteOneLink = "

    .line 628
    invoke-static {p1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    if-eqz p1, :cond_0

    .line 629
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "oneLinkSlug"

    invoke-virtual {v0, v1}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    .line 630
    :cond_0
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "onelinkDomain"

    invoke-virtual {v0, v1}, Lcom/appsflyer/AppsFlyerProperties;->remove(Ljava/lang/String;)V

    .line 631
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "onelinkVersion"

    invoke-virtual {v0, v1}, Lcom/appsflyer/AppsFlyerProperties;->remove(Ljava/lang/String;)V

    .line 632
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "onelinkScheme"

    invoke-virtual {v0, v1}, Lcom/appsflyer/AppsFlyerProperties;->remove(Ljava/lang/String;)V

    :cond_1
    const-string v0, "oneLinkSlug"

    .line 634
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public setAppUserId(Ljava/lang/String;)V
    .locals 4
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 592
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setAppUserId"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 593
    invoke-virtual {p0, p1}, Lcom/appsflyer/AppsFlyerLib;->setCustomerUserId(Ljava/lang/String;)V

    return-void
.end method

.method public setCollectAndroidID(Z)V
    .locals 5

    .line 769
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setCollectAndroidID"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    invoke-static {p1}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const-string v0, "collectAndroidId"

    .line 770
    invoke-static {p1}, Ljava/lang/Boolean;->toString(Z)Ljava/lang/String;

    move-result-object p1

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public setCollectFingerPrint(Z)V
    .locals 5
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 780
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setCollectFingerPrint"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    invoke-static {p1}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const-string v0, "collectFingerPrint"

    .line 781
    invoke-static {p1}, Ljava/lang/Boolean;->toString(Z)Ljava/lang/String;

    move-result-object p1

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public setCollectIMEI(Z)V
    .locals 5

    .line 774
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setCollectIMEI"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    invoke-static {p1}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const-string v0, "collectIMEI"

    .line 775
    invoke-static {p1}, Ljava/lang/Boolean;->toString(Z)Ljava/lang/String;

    move-result-object p1

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public setCurrencyCode(Ljava/lang/String;)V
    .locals 4

    .line 958
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setCurrencyCode"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 959
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "currencyCode"

    invoke-virtual {v0, v1, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public setCustomerIdAndTrack(Ljava/lang/String;Landroid/content/Context;)V
    .locals 9
    .param p2    # Landroid/content/Context;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    if-eqz p2, :cond_5

    const-string v0, "waitForCustomerId"

    .line 610
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    const/4 v2, 0x0

    invoke-virtual {v1, v0, v2}, Lcom/appsflyer/AppsFlyerProperties;->getBoolean(Ljava/lang/String;Z)Z

    move-result v0

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    const-string v0, "AppUserId"

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v3

    invoke-virtual {v3, v0}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_0

    const/4 v2, 0x1

    :cond_0
    if-eqz v2, :cond_4

    .line 612
    invoke-virtual {p0, p1}, Lcom/appsflyer/AppsFlyerLib;->setCustomerUserId(Ljava/lang/String;)V

    .line 613
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v2, "CustomerUserId set: "

    invoke-direct {v0, v2}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, " - Initializing AppsFlyer Tacking"

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1, v1}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;Z)V

    .line 614
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object p1

    invoke-virtual {p1, p2}, Lcom/appsflyer/AppsFlyerProperties;->getReferrer(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p1

    const-string v0, "AppsFlyerKey"

    .line 615
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const/4 v5, 0x0

    const/4 v6, 0x0

    if-nez p1, :cond_1

    const-string p1, ""

    :cond_1
    move-object v7, p1

    instance-of p1, p2, Landroid/app/Activity;

    if-eqz p1, :cond_2

    move-object p1, p2

    check-cast p1, Landroid/app/Activity;

    invoke-virtual {p1}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object p1

    :goto_0
    move-object v8, p1

    goto :goto_1

    :cond_2
    const/4 p1, 0x0

    goto :goto_0

    :goto_1
    move-object v2, p0

    move-object v3, p2

    invoke-direct/range {v2 .. v8}, Lcom/appsflyer/AppsFlyerLib;->ˏ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Landroid/content/Intent;)V

    const-string p1, "afUninstallToken"

    .line 616
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    if-eqz p1, :cond_3

    const-string p1, "afUninstallToken"

    .line 617
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p0, p2, p1}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Landroid/content/Context;Ljava/lang/String;)V

    :cond_3
    return-void

    .line 620
    :cond_4
    invoke-virtual {p0, p1}, Lcom/appsflyer/AppsFlyerLib;->setCustomerUserId(Ljava/lang/String;)V

    const-string p2, "waitForCustomerUserId is false; setting CustomerUserID: "

    .line 621
    invoke-static {p1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p2, p1}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-static {p1, v1}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;Z)V

    :cond_5
    return-void
.end method

.method public setCustomerUserId(Ljava/lang/String;)V
    .locals 4

    .line 597
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setCustomerUserId"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const-string v0, "setCustomerUserId = "

    .line 598
    invoke-static {p1}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    const-string v0, "AppUserId"

    .line 599
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public setDebugLog(Z)V
    .locals 5

    .line 491
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setDebugLog"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    invoke-static {p1}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 492
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "shouldLog"

    .line 60810
    invoke-virtual {v0, v1, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Z)V

    .line 493
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    if-eqz p1, :cond_0

    sget-object p1, Lcom/appsflyer/AFLogger$LogLevel;->DEBUG:Lcom/appsflyer/AFLogger$LogLevel;

    goto :goto_0

    :cond_0
    sget-object p1, Lcom/appsflyer/AFLogger$LogLevel;->NONE:Lcom/appsflyer/AFLogger$LogLevel;

    :goto_0
    const-string v1, "logLevel"

    .line 61812
    invoke-virtual {p1}, Lcom/appsflyer/AFLogger$LogLevel;->getLevel()I

    move-result p1

    invoke-virtual {v0, v1, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;I)V

    return-void
.end method

.method protected setDeepLinkData(Landroid/content/Intent;)V
    .locals 2

    if-eqz p1, :cond_0

    :try_start_0
    const-string v0, "android.intent.action.VIEW"

    .line 1064
    invoke-virtual {p1}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 1065
    invoke-virtual {p1}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object p1

    iput-object p1, p0, Lcom/appsflyer/AppsFlyerLib;->ʿ:Landroid/net/Uri;

    .line 1066
    new-instance p1, Ljava/lang/StringBuilder;

    const-string v0, "Unity setDeepLinkData = "

    invoke-direct {p1, v0}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ʿ:Landroid/net/Uri;

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "Exception while setting deeplink data (unity). "

    .line 1069
    invoke-static {v0, p1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    return-void

    :cond_0
    :goto_0
    return-void
.end method

.method public setDeviceTrackingDisabled(Z)V
    .locals 5

    .line 1213
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setDeviceTrackingDisabled"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    invoke-static {p1}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 1214
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "deviceTrackingDisabled"

    invoke-virtual {v0, v1, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Z)V

    return-void
.end method

.method public setExtension(Ljava/lang/String;)V
    .locals 4

    .line 948
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setExtension"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 949
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "sdkExtension"

    invoke-virtual {v0, v1, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public setGCMProjectID(Ljava/lang/String;)V
    .locals 4
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 450
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setGCMProjectID"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const-string v0, "Method \'setGCMProjectNumber\' is deprecated. Please follow the documentation."

    .line 451
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    .line 452
    invoke-virtual {p0, p1}, Lcom/appsflyer/AppsFlyerLib;->enableUninstallTracking(Ljava/lang/String;)V

    return-void
.end method

.method public setGCMProjectNumber(Landroid/content/Context;Ljava/lang/String;)V
    .locals 3
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 472
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object p1

    const-string v0, "setGCMProjectNumber"

    const/4 v1, 0x1

    new-array v1, v1, [Ljava/lang/String;

    const/4 v2, 0x0

    aput-object p2, v1, v2

    invoke-virtual {p1, v0, v1}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const-string p1, "Method \'setGCMProjectNumber\' is deprecated. Please use \'enableUninstallTracking\'."

    .line 473
    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    .line 474
    invoke-virtual {p0, p2}, Lcom/appsflyer/AppsFlyerLib;->enableUninstallTracking(Ljava/lang/String;)V

    return-void
.end method

.method public setGCMProjectNumber(Ljava/lang/String;)V
    .locals 4
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 461
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setGCMProjectNumber"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const-string v0, "Method \'setGCMProjectNumber\' is deprecated. Please follow the documentation."

    .line 462
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    .line 463
    invoke-virtual {p0, p1}, Lcom/appsflyer/AppsFlyerLib;->enableUninstallTracking(Ljava/lang/String;)V

    return-void
.end method

.method public setHostName(Ljava/lang/String;)V
    .locals 2

    const-string v0, "custom_host"

    .line 3242
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public setImeiData(Ljava/lang/String;)V
    .locals 4

    .line 497
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setImeiData"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 498
    iput-object p1, p0, Lcom/appsflyer/AppsFlyerLib;->ˊ:Ljava/lang/String;

    return-void
.end method

.method public setIsUpdate(Z)V
    .locals 5

    .line 953
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setIsUpdate"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    invoke-static {p1}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 954
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "IS_UPDATE"

    invoke-virtual {v0, v1, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Z)V

    return-void
.end method

.method public setLogLevel(Lcom/appsflyer/AFLogger$LogLevel;)V
    .locals 2

    .line 3238
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "logLevel"

    invoke-virtual {p1}, Lcom/appsflyer/AFLogger$LogLevel;->getLevel()I

    move-result p1

    invoke-virtual {v0, v1, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;I)V

    return-void
.end method

.method public setMinTimeBetweenSessions(I)V
    .locals 3

    .line 3256
    sget-object v0, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    int-to-long v1, p1

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v0

    iput-wide v0, p0, Lcom/appsflyer/AppsFlyerLib;->ॱˊ:J

    return-void
.end method

.method public setPreinstallAttribution(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    const-string v0, "setPreinstallAttribution API called"

    .line 2344
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    .line 2345
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    if-eqz p1, :cond_0

    :try_start_0
    const-string v1, "pid"

    .line 2348
    invoke-virtual {v0, v1, p1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    goto :goto_0

    :catch_0
    move-exception p1

    goto :goto_1

    :cond_0
    :goto_0
    if-eqz p2, :cond_1

    const-string p1, "c"

    .line 2351
    invoke-virtual {v0, p1, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    :cond_1
    if-eqz p3, :cond_2

    const-string p1, "af_siteid"

    .line 2354
    invoke-virtual {v0, p1, p3}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_2

    .line 2358
    :goto_1
    invoke-virtual {p1}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object p2

    invoke-static {p2, p1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_2
    :goto_2
    const-string p1, "pid"

    .line 2361
    invoke-virtual {v0, p1}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_3

    const-string p1, "preInstallName"

    .line 2362
    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object p3

    invoke-virtual {p3, p1, p2}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    :cond_3
    const-string p1, "Cannot set preinstall attribution data without a media source"

    .line 2365
    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    return-void
.end method

.method public setUserEmail(Ljava/lang/String;)V
    .locals 4
    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 721
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setUserEmail"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/String;

    const/4 v3, 0x0

    aput-object p1, v2, v3

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const-string v0, "userEmail"

    .line 722
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public varargs setUserEmails(Lcom/appsflyer/AppsFlyerProperties$EmailsCryptType;[Ljava/lang/String;)V
    .locals 7

    .line 731
    new-instance v0, Ljava/util/ArrayList;

    array-length v1, p2

    add-int/lit8 v1, v1, 0x1

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(I)V

    .line 732
    invoke-virtual {p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 733
    invoke-static {p2}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    .line 734
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v1

    const-string v2, "setUserEmails"

    array-length v3, p2

    add-int/lit8 v3, v3, 0x1

    new-array v3, v3, [Ljava/lang/String;

    invoke-interface {v0, v3}, Ljava/util/List;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Ljava/lang/String;

    invoke-virtual {v1, v2, v0}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 736
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    const-string v1, "userEmailsCryptType"

    invoke-virtual {p1}, Lcom/appsflyer/AppsFlyerProperties$EmailsCryptType;->getValue()I

    move-result v2

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;I)V

    .line 737
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 739
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 741
    array-length v2, p2

    const/4 v3, 0x0

    const/4 v4, 0x0

    :goto_0
    if-ge v4, v2, :cond_0

    aget-object v3, p2, v4

    .line 742
    sget-object v5, Lcom/appsflyer/AppsFlyerLib$4;->ॱ:[I

    invoke-virtual {p1}, Ljava/lang/Enum;->ordinal()I

    move-result v6

    aget v5, v5, v6

    packed-switch v5, :pswitch_data_0

    const-string v5, "sha1_el_arr"

    .line 746
    invoke-static {v3}, Lcom/appsflyer/r;->ॱ(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v3}, Ljava/util/AbstractCollection;->add(Ljava/lang/Object;)Z

    :goto_1
    move-object v3, v5

    goto :goto_2

    :pswitch_0
    const-string v5, "plain_el_arr"

    .line 758
    invoke-virtual {v1, v3}, Ljava/util/AbstractCollection;->add(Ljava/lang/Object;)Z

    goto :goto_1

    :pswitch_1
    const-string v5, "sha256_el_arr"

    .line 754
    invoke-static {v3}, Lcom/appsflyer/r;->ˋ(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v3}, Ljava/util/AbstractCollection;->add(Ljava/lang/Object;)Z

    goto :goto_1

    :pswitch_2
    const-string v5, "md5_el_arr"

    .line 750
    invoke-static {v3}, Lcom/appsflyer/r;->ˏ(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v3}, Ljava/util/AbstractCollection;->add(Ljava/lang/Object;)Z

    goto :goto_1

    :goto_2
    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    .line 763
    :cond_0
    invoke-interface {v0, v3, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 764
    new-instance p1, Lorg/json/JSONObject;

    invoke-direct {p1, v0}, Lorg/json/JSONObject;-><init>(Ljava/util/Map;)V

    .line 765
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object p2

    invoke-virtual {p1}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/appsflyer/AppsFlyerProperties;->setUserEmails(Ljava/lang/String;)V

    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x2
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public varargs setUserEmails([Ljava/lang/String;)V
    .locals 2

    .line 726
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "setUserEmails"

    invoke-virtual {v0, v1, p1}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 727
    sget-object v0, Lcom/appsflyer/AppsFlyerProperties$EmailsCryptType;->NONE:Lcom/appsflyer/AppsFlyerProperties$EmailsCryptType;

    invoke-virtual {p0, v0, p1}, Lcom/appsflyer/AppsFlyerLib;->setUserEmails(Lcom/appsflyer/AppsFlyerProperties$EmailsCryptType;[Ljava/lang/String;)V

    return-void
.end method

.method public startTracking(Landroid/app/Application;)V
    .locals 1

    .line 868
    iget-boolean v0, p0, Lcom/appsflyer/AppsFlyerLib;->ˎˎ:Z

    if-nez v0, :cond_0

    const-string p1, "ERROR: AppsFlyer SDK is not initialized! The API call \'startTracking(Application)\' must be called after the \'init(String, AppsFlyerConversionListener)\' API method, which should be called on the Application\'s onCreate."

    .line 869
    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    return-void

    :cond_0
    const/4 v0, 0x0

    .line 873
    invoke-virtual {p0, p1, v0}, Lcom/appsflyer/AppsFlyerLib;->startTracking(Landroid/app/Application;Ljava/lang/String;)V

    return-void
.end method

.method public startTracking(Landroid/app/Application;Ljava/lang/String;)V
    .locals 5

    .line 885
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "startTracking"

    const/4 v2, 0x1

    new-array v3, v2, [Ljava/lang/String;

    const/4 v4, 0x0

    aput-object p2, v3, v4

    invoke-virtual {v0, v1, v3}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const-string v0, "Starting AppsFlyer Tracking: (v%s.%s)"

    const/4 v1, 0x2

    .line 886
    new-array v1, v1, [Ljava/lang/Object;

    const-string v3, "4.8.11"

    aput-object v3, v1, v4

    const-string v3, "383"

    aput-object v3, v1, v2

    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    const-string v0, "Build Number: 383"

    .line 887
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    .line 888
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/appsflyer/AppsFlyerProperties;->loadProperties(Landroid/content/Context;)V

    .line 889
    invoke-static {p2}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    const-string v0, "AppsFlyerKey"

    .line 890
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0, p2}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    .line 891
    invoke-static {p2}, Lcom/appsflyer/f$5;->ˊ(Ljava/lang/String;)V

    goto :goto_0

    :cond_0
    const-string p2, "AppsFlyerKey"

    .line 893
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    invoke-virtual {v0, p2}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    invoke-static {p2}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result p2

    if-eqz p2, :cond_1

    const-string p1, "ERROR: AppsFlyer SDK is not initialized! You must provide AppsFlyer Dev-Key either in the \'init\' API method (should be called on Application\'s onCreate),or in the startTracking API method (should be called on Activity\'s onCreate)."

    .line 894
    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    return-void

    .line 900
    :cond_1
    :goto_0
    invoke-direct {p0, p1}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/app/Application;)V

    return-void
.end method

.method public stopTracking(ZLandroid/content/Context;)V
    .locals 2

    .line 385
    iput-boolean p1, p0, Lcom/appsflyer/AppsFlyerLib;->ˋˋ:Z

    .line 386
    invoke-static {}, Lcom/appsflyer/cache/CacheManager;->getInstance()Lcom/appsflyer/cache/CacheManager;

    move-result-object p1

    invoke-virtual {p1, p2}, Lcom/appsflyer/cache/CacheManager;->clearCache(Landroid/content/Context;)V

    .line 387
    iget-boolean p1, p0, Lcom/appsflyer/AppsFlyerLib;->ˋˋ:Z

    if-eqz p1, :cond_1

    const-string p1, "is_stop_tracking_used"

    const-string v0, "appsflyer-data"

    const/4 v1, 0x0

    .line 54775
    invoke-virtual {p2, v0, v1}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p2

    .line 56777
    invoke-interface {p2}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object p2

    const/4 v0, 0x1

    .line 56778
    invoke-interface {p2, p1, v0}, Landroid/content/SharedPreferences$Editor;->putBoolean(Ljava/lang/String;Z)Landroid/content/SharedPreferences$Editor;

    .line 57781
    sget p1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v0, 0x9

    if-lt p1, v0, :cond_0

    .line 57782
    invoke-interface {p2}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void

    .line 57784
    :cond_0
    invoke-interface {p2}, Landroid/content/SharedPreferences$Editor;->commit()Z

    :cond_1
    return-void
.end method

.method public trackAppLaunch(Landroid/content/Context;Ljava/lang/String;)V
    .locals 8

    .line 1039
    invoke-static {p1}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Landroid/content/Context;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 1042
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ॱˎ:Lcom/appsflyer/e;

    if-nez v0, :cond_0

    .line 1043
    new-instance v0, Lcom/appsflyer/e;

    invoke-direct {v0}, Lcom/appsflyer/e;-><init>()V

    iput-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ॱˎ:Lcom/appsflyer/e;

    .line 1044
    iget-object v0, p0, Lcom/appsflyer/AppsFlyerLib;->ॱˎ:Lcom/appsflyer/e;

    invoke-virtual {v0, p1, p0}, Lcom/appsflyer/e;->ˏ(Landroid/content/Context;Lcom/appsflyer/b;)V

    goto :goto_0

    :cond_0
    const-string v0, "AFInstallReferrer instance already created"

    .line 1046
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    :cond_1
    :goto_0
    const/4 v4, 0x0

    const/4 v5, 0x0

    const-string v6, ""

    const/4 v7, 0x0

    move-object v1, p0

    move-object v2, p1

    move-object v3, p2

    .line 1050
    invoke-direct/range {v1 .. v7}, Lcom/appsflyer/AppsFlyerLib;->ˏ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Landroid/content/Intent;)V

    return-void
.end method

.method public trackEvent(Landroid/content/Context;Ljava/lang/String;Ljava/util/Map;)V
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 1081
    new-instance v0, Lorg/json/JSONObject;

    if-nez p3, :cond_0

    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    goto :goto_0

    :cond_0
    move-object v1, p3

    :goto_0
    invoke-direct {v0, v1}, Lorg/json/JSONObject;-><init>(Ljava/util/Map;)V

    .line 1082
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v1

    const-string v2, "trackEvent"

    const/4 v3, 0x2

    new-array v3, v3, [Ljava/lang/String;

    const/4 v4, 0x0

    aput-object p2, v3, v4

    const/4 v4, 0x1

    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v0

    aput-object v0, v3, v4

    invoke-virtual {v1, v2, v3}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 1083
    invoke-virtual {p0, p1, p2, p3}, Lcom/appsflyer/AppsFlyerLib;->ˏ(Landroid/content/Context;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public trackLocation(Landroid/content/Context;DD)V
    .locals 5

    .line 963
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "trackLocation"

    const/4 v2, 0x2

    new-array v2, v2, [Ljava/lang/String;

    invoke-static {p2, p3}, Ljava/lang/String;->valueOf(D)Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4

    invoke-static {p4, p5}, Ljava/lang/String;->valueOf(D)Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x1

    aput-object v3, v2, v4

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 964
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "af_long"

    .line 965
    invoke-static {p4, p5}, Ljava/lang/Double;->toString(D)Ljava/lang/String;

    move-result-object p4

    invoke-interface {v0, v1, p4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p4, "af_lat"

    .line 966
    invoke-static {p2, p3}, Ljava/lang/Double;->toString(D)Ljava/lang/String;

    move-result-object p2

    invoke-interface {v0, p4, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p2, "af_location_coordinates"

    .line 968
    invoke-virtual {p0, p1, p2, v0}, Lcom/appsflyer/AppsFlyerLib;->ˏ(Landroid/content/Context;Ljava/lang/String;Ljava/util/Map;)V

    return-void
.end method

.method public unregisterConversionListener()V
    .locals 3

    .line 1250
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v0

    const-string v1, "unregisterConversionListener"

    const/4 v2, 0x0

    new-array v2, v2, [Ljava/lang/String;

    invoke-virtual {v0, v1, v2}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 1251
    sput-object v0, Lcom/appsflyer/AppsFlyerLib;->ˏॱ:Lcom/appsflyer/AppsFlyerConversionListener;

    return-void
.end method

.method public updateServerUninstallToken(Landroid/content/Context;Ljava/lang/String;)V
    .locals 1

    if-eqz p2, :cond_0

    .line 485
    new-instance v0, Lcom/appsflyer/d;

    invoke-direct {v0, p2}, Lcom/appsflyer/d;-><init>(Ljava/lang/String;)V

    .line 486
    invoke-static {p1, v0}, Lcom/appsflyer/u;->ॱ(Landroid/content/Context;Lcom/appsflyer/d;)V

    :cond_0
    return-void
.end method

.method public validateAndTrackInAppPurchase(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V
    .locals 13
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    move-object v0, p1

    move-object/from16 v5, p4

    move-object/from16 v6, p5

    move-object/from16 v7, p6

    .line 2782
    invoke-static {}, Lcom/appsflyer/y;->ˋ()Lcom/appsflyer/y;

    move-result-object v1

    const-string v2, "validateAndTrackInAppPurchase"

    const/4 v8, 0x6

    new-array v8, v8, [Ljava/lang/String;

    const/4 v9, 0x0

    aput-object p2, v8, v9

    const/4 v9, 0x1

    aput-object p3, v8, v9

    const/4 v9, 0x2

    aput-object v5, v8, v9

    const/4 v9, 0x3

    aput-object v6, v8, v9

    const/4 v9, 0x4

    aput-object v7, v8, v9

    if-nez p7, :cond_0

    const-string v10, ""

    goto :goto_0

    :cond_0
    invoke-virtual/range {p7 .. p7}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v10

    :goto_0
    const/4 v11, 0x5

    aput-object v10, v8, v11

    invoke-virtual {v1, v2, v8}, Lcom/appsflyer/y;->ˏ(Ljava/lang/String;[Ljava/lang/String;)V

    .line 2784
    invoke-virtual {p0}, Lcom/appsflyer/AppsFlyerLib;->isTrackingStopped()Z

    move-result v1

    if-nez v1, :cond_1

    .line 2785
    new-instance v1, Ljava/lang/StringBuilder;

    const-string v2, "Validate in app called with parameters: "

    invoke-direct {v1, v2}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    :cond_1
    if-eqz p2, :cond_4

    if-eqz v6, :cond_4

    if-eqz p3, :cond_4

    if-eqz v7, :cond_4

    if-nez v5, :cond_2

    goto :goto_3

    .line 2793
    :cond_2
    invoke-static {}, Lcom/appsflyer/AFExecutor;->getInstance()Lcom/appsflyer/AFExecutor;

    move-result-object v1

    invoke-virtual {v1}, Lcom/appsflyer/AFExecutor;->ॱ()Ljava/util/concurrent/ScheduledThreadPoolExecutor;

    move-result-object v11

    .line 2795
    new-instance v12, Lcom/appsflyer/i;

    .line 2796
    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    const-string v2, "AppsFlyerKey"

    .line 2797
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v8

    invoke-virtual {v8, v2}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    instance-of v8, v0, Landroid/app/Activity;

    if-eqz v8, :cond_3

    check-cast v0, Landroid/app/Activity;

    .line 2804
    invoke-virtual {v0}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v0

    :goto_1
    move-object v10, v0

    goto :goto_2

    :cond_3
    const/4 v0, 0x0

    goto :goto_1

    :goto_2
    move-object v0, v12

    move-object v3, p2

    move-object/from16 v4, p3

    move-object/from16 v5, p4

    move-object/from16 v6, p5

    move-object/from16 v7, p6

    move-object/from16 v8, p7

    move-object v9, v11

    invoke-direct/range {v0 .. v10}, Lcom/appsflyer/i;-><init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;Ljava/util/concurrent/ScheduledExecutorService;Landroid/content/Intent;)V

    const-wide/16 v0, 0xa

    .line 2807
    sget-object v2, Ljava/util/concurrent/TimeUnit;->MILLISECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-static {v11, v12, v0, v1, v2}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Ljava/util/concurrent/ScheduledExecutorService;Ljava/lang/Runnable;JLjava/util/concurrent/TimeUnit;)V

    goto :goto_4

    .line 2788
    :cond_4
    :goto_3
    sget-object v0, Lcom/appsflyer/AppsFlyerLib;->ˏ:Lcom/appsflyer/AppsFlyerInAppPurchaseValidatorListener;

    if-eqz v0, :cond_5

    .line 2789
    sget-object v0, Lcom/appsflyer/AppsFlyerLib;->ˏ:Lcom/appsflyer/AppsFlyerInAppPurchaseValidatorListener;

    const-string v1, "Please provide purchase parameters"

    invoke-interface {v0, v1}, Lcom/appsflyer/AppsFlyerInAppPurchaseValidatorListener;->onValidateInAppFailure(Ljava/lang/String;)V

    return-void

    :cond_5
    :goto_4
    return-void
.end method

.method public waitForCustomerUserId(Z)V
    .locals 2

    const-string v0, "initAfterCustomerUserID: "

    .line 605
    invoke-static {p1}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x1

    invoke-static {v0, v1}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;Z)V

    const-string v0, "waitForCustomerId"

    .line 606
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0, p1}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Z)V

    return-void
.end method

.method final ˋ(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 13

    const-string v0, "shouldMonitor"

    .line 189
    invoke-virtual {p2, v0}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    const-string p2, "Turning on monitoring."

    .line 191
    invoke-static {p2}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    .line 192
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object p2

    const-string v1, "shouldMonitor"

    const-string v2, "true"

    invoke-virtual {v0, v2}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v0

    invoke-virtual {p2, v1, v0}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Z)V

    const/4 p2, 0x0

    const-string v0, "START_TRACKING"

    .line 193
    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v1

    invoke-static {p1, p2, v0, v1}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void

    :cond_0
    const-string v0, "****** onReceive called *******"

    .line 197
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    .line 199
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    invoke-virtual {v0}, Lcom/appsflyer/AppsFlyerProperties;->setOnReceiveCalled()V

    const-string v0, "referrer"

    .line 201
    invoke-virtual {p2, v0}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    const-string v0, "Play store referrer: "

    .line 202
    invoke-static {v7}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    if-eqz v7, :cond_4

    const-string v0, "TestIntegrationMode"

    .line 206
    invoke-virtual {p2, v0}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "AppsFlyer_Test"

    .line 208
    invoke-virtual {v1, v0}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v0

    const/16 v1, 0x9

    const/4 v2, 0x0

    if-eqz v0, :cond_2

    const-string v0, "appsflyer-data"

    .line 51696
    invoke-virtual {p1, v0, v2}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 211
    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 212
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->clear()Landroid/content/SharedPreferences$Editor;

    .line 54697
    sget v3, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v3, v1, :cond_1

    .line 54698
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->apply()V

    goto :goto_0

    .line 54700
    :cond_1
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 215
    :goto_0
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    invoke-virtual {v0, v2}, Lcom/appsflyer/AppsFlyerProperties;->setFirstLaunchCalled(Z)V

    const-string v0, "Test mode started.."

    .line 54703
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    .line 54704
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v3

    iput-wide v3, p0, Lcom/appsflyer/AppsFlyerLib;->ˊˊ:J

    :cond_2
    const-string v0, "referrer"

    const-string v3, "appsflyer-data"

    .line 52711
    invoke-virtual {p1, v3, v2}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v2

    .line 54713
    invoke-interface {v2}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v2

    .line 54714
    invoke-interface {v2, v0, v7}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 55717
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v0, v1, :cond_3

    .line 55718
    invoke-interface {v2}, Landroid/content/SharedPreferences$Editor;->apply()V

    goto :goto_1

    .line 55720
    :cond_3
    invoke-interface {v2}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 222
    :goto_1
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    invoke-virtual {v0, v7}, Lcom/appsflyer/AppsFlyerProperties;->setReferrer(Ljava/lang/String;)V

    .line 224
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v0

    invoke-virtual {v0}, Lcom/appsflyer/AppsFlyerProperties;->isFirstLaunchCalled()Z

    move-result v0

    if-eqz v0, :cond_4

    const-string v0, "onReceive: isLaunchCalled"

    .line 225
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    if-eqz v7, :cond_4

    .line 54728
    invoke-virtual {v7}, Ljava/lang/String;->length()I

    move-result v0

    const/4 v1, 0x5

    if-le v0, v1, :cond_4

    .line 54729
    invoke-static {}, Lcom/appsflyer/AFExecutor;->getInstance()Lcom/appsflyer/AFExecutor;

    move-result-object v0

    invoke-virtual {v0}, Lcom/appsflyer/AFExecutor;->ॱ()Ljava/util/concurrent/ScheduledThreadPoolExecutor;

    move-result-object v0

    .line 54734
    new-instance v12, Lcom/appsflyer/AppsFlyerLib$b;

    new-instance v3, Ljava/lang/ref/WeakReference;

    .line 54735
    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    invoke-direct {v3, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v6, 0x0

    const/4 v9, 0x1

    const/4 v11, 0x0

    move-object v1, v12

    move-object v2, p0

    move-object v8, v0

    move-object v10, p2

    invoke-direct/range {v1 .. v11}, Lcom/appsflyer/AppsFlyerLib$b;-><init>(Lcom/appsflyer/AppsFlyerLib;Ljava/lang/ref/WeakReference;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/concurrent/ExecutorService;ZLandroid/content/Intent;B)V

    const-wide/16 p1, 0x5

    .line 54745
    sget-object v1, Ljava/util/concurrent/TimeUnit;->MILLISECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-static {v0, v12, p1, p2, v1}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Ljava/util/concurrent/ScheduledExecutorService;Ljava/lang/Runnable;JLjava/util/concurrent/TimeUnit;)V

    :cond_4
    return-void
.end method

.method final ˋ(Landroid/content/Context;Ljava/lang/String;)V
    .locals 10

    const-string v0, "waitForCustomerId"

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    const/4 v2, 0x0

    invoke-virtual {v1, v0, v2}, Lcom/appsflyer/AppsFlyerProperties;->getBoolean(Ljava/lang/String;Z)Z

    move-result v0

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    const-string v0, "AppUserId"

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v3

    invoke-virtual {v3, v0}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    if-eqz v0, :cond_1

    const-string p1, "CustomerUserId not set, Tracking is disabled"

    .line 1118
    invoke-static {p1, v1}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;Z)V

    return-void

    .line 1122
    :cond_1
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v3, "AppsFlyerKey"

    .line 1123
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v4

    invoke-virtual {v4, v3}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-nez v3, :cond_2

    const-string p1, "[registerUninstall] AppsFlyer\'s SDK cannot send any event without providing DevKey."

    .line 1125
    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    return-void

    .line 1130
    :cond_2
    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v4

    .line 1131
    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v5

    .line 1134
    :try_start_0
    invoke-virtual {v4, v5, v2}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v6

    const-string v7, "app_version_code"

    .line 1135
    iget v8, v6, Landroid/content/pm/PackageInfo;->versionCode:I

    invoke-static {v8}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v8

    invoke-interface {v0, v7, v8}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v7, "app_version_name"

    .line 1136
    iget-object v8, v6, Landroid/content/pm/PackageInfo;->versionName:Ljava/lang/String;

    invoke-interface {v0, v7, v8}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1139
    iget-object v7, v6, Landroid/content/pm/PackageInfo;->applicationInfo:Landroid/content/pm/ApplicationInfo;

    invoke-virtual {v4, v7}, Landroid/content/pm/PackageManager;->getApplicationLabel(Landroid/content/pm/ApplicationInfo;)Ljava/lang/CharSequence;

    move-result-object v4

    invoke-interface {v4}, Ljava/lang/CharSequence;->toString()Ljava/lang/String;

    move-result-object v4

    const-string v7, "app_name"

    .line 1140
    invoke-interface {v0, v7, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1143
    iget-wide v6, v6, Landroid/content/pm/PackageInfo;->firstInstallTime:J

    const-string v4, "yyyy-MM-dd_HHmmssZ"

    .line 1144
    new-instance v8, Ljava/text/SimpleDateFormat;

    sget-object v9, Ljava/util/Locale;->US:Ljava/util/Locale;

    invoke-direct {v8, v4, v9}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    const-string v4, "installDate"

    const-string v9, "UTC"

    .line 1145
    invoke-static {v9}, Ljava/util/TimeZone;->getTimeZone(Ljava/lang/String;)Ljava/util/TimeZone;

    move-result-object v9

    invoke-virtual {v8, v9}, Ljava/text/DateFormat;->setTimeZone(Ljava/util/TimeZone;)V

    new-instance v9, Ljava/util/Date;

    invoke-direct {v9, v6, v7}, Ljava/util/Date;-><init>(J)V

    invoke-virtual {v8, v9}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v6

    invoke-interface {v0, v4, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v4

    const-string v6, "Exception while collecting application version info."

    .line 1147
    invoke-static {v6, v4}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 1149
    :goto_1
    invoke-static {p1, v0}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Landroid/content/Context;Ljava/util/Map;)V

    const-string v4, "AppUserId"

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v6

    invoke-virtual {v6, v4}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    if-eqz v4, :cond_3

    const-string v6, "appUserId"

    .line 1154
    invoke-interface {v0, v6, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_3
    :try_start_1
    const-string v4, "model"

    .line 1159
    sget-object v6, Landroid/os/Build;->MODEL:Ljava/lang/String;

    invoke-interface {v0, v4, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v4, "brand"

    .line 1160
    sget-object v6, Landroid/os/Build;->BRAND:Ljava/lang/String;

    invoke-interface {v0, v4, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_1
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_2

    :catch_1
    move-exception v4

    const-string v6, "Exception while collecting device brand and model."

    .line 1162
    invoke-static {v6, v4}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 1166
    :goto_2
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v4

    const-string v6, "deviceTrackingDisabled"

    invoke-virtual {v4, v6, v2}, Lcom/appsflyer/AppsFlyerProperties;->getBoolean(Ljava/lang/String;Z)Z

    move-result v4

    if-eqz v4, :cond_4

    const-string v4, "deviceTrackingDisabled"

    const-string v6, "true"

    .line 1168
    invoke-interface {v0, v4, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1171
    :cond_4
    invoke-virtual {p1}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v4

    invoke-static {v4}, Lcom/appsflyer/o;->ˊ(Landroid/content/ContentResolver;)Lcom/appsflyer/n;

    move-result-object v4

    if-eqz v4, :cond_5

    const-string v6, "amazon_aid"

    .line 1173
    invoke-virtual {v4}, Lcom/appsflyer/n;->ˋ()Ljava/lang/String;

    move-result-object v7

    invoke-interface {v0, v6, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v6, "amazon_aid_limit"

    .line 1174
    invoke-virtual {v4}, Lcom/appsflyer/n;->ˊ()Z

    move-result v4

    invoke-static {v4}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v4

    invoke-interface {v0, v6, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1177
    :cond_5
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v4

    const-string v6, "advertiserId"

    invoke-virtual {v4, v6}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    if-eqz v4, :cond_6

    const-string v6, "advertiserId"

    .line 1179
    invoke-interface {v0, v6, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_6
    const-string v4, "devkey"

    .line 1182
    invoke-interface {v0, v4, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v3, "uid"

    .line 1183
    new-instance v4, Ljava/lang/ref/WeakReference;

    invoke-direct {v4, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    invoke-static {v4}, Lcom/appsflyer/p;->ˋ(Ljava/lang/ref/WeakReference;)Ljava/lang/String;

    move-result-object v4

    invoke-interface {v0, v3, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v3, "af_gcm_token"

    .line 1184
    invoke-interface {v0, v3, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p2, "appsflyer-data"

    .line 1185
    invoke-virtual {p1, p2, v2}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p2

    const-string v3, "appsFlyerCount"

    invoke-static {p2, v3, v2}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/SharedPreferences;Ljava/lang/String;Z)I

    move-result p2

    const-string v3, "launch_counter"

    .line 1186
    invoke-static {p2}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object p2

    invoke-interface {v0, v3, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p2, "sdk"

    .line 1187
    sget v3, Landroid/os/Build$VERSION;->SDK_INT:I

    invoke-static {v3}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v0, p2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1188
    new-instance p2, Ljava/lang/ref/WeakReference;

    invoke-direct {p2, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v3

    const-string v4, "channel"

    invoke-virtual {v3, v4}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-nez v3, :cond_7

    const-string v3, "CHANNEL"

    invoke-static {p2, v3}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Ljava/lang/ref/WeakReference;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    :cond_7
    if-eqz v3, :cond_8

    const-string p2, "channel"

    .line 1190
    invoke-interface {v0, p2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1194
    :cond_8
    :try_start_2
    new-instance p2, Lcom/appsflyer/l;

    invoke-virtual {p0}, Lcom/appsflyer/AppsFlyerLib;->isTrackingStopped()Z

    move-result v3

    invoke-direct {p2, p1, v3}, Lcom/appsflyer/l;-><init>(Landroid/content/Context;Z)V

    .line 1195
    iput-object v0, p2, Lcom/appsflyer/l;->ˎ:Ljava/util/Map;

    .line 1196
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    sget-object v0, Lcom/appsflyer/AppsFlyerLib;->ˎ:Ljava/lang/String;

    invoke-static {v0}, Lcom/appsflyer/ServerConfigHandler;->getUrl(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p1

    .line 1197
    new-array v0, v1, [Ljava/lang/String;

    aput-object p1, v0, v2

    invoke-virtual {p2, v0}, Landroid/os/AsyncTask;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;
    :try_end_2
    .catch Ljava/lang/Throwable; {:try_start_2 .. :try_end_2} :catch_2

    return-void

    :catch_2
    move-exception p1

    .line 1199
    invoke-virtual {p1}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object p2

    invoke-static {p2, p1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    return-void
.end method

.method final ˎ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZLandroid/content/SharedPreferences;ZLandroid/content/Intent;)Ljava/util/Map;
    .locals 17
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Z",
            "Landroid/content/SharedPreferences;",
            "Z",
            "Landroid/content/Intent;",
            ")",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    move-object/from16 v1, p0

    move-object/from16 v2, p1

    move-object/from16 v3, p2

    move-object/from16 v4, p3

    move-object/from16 v5, p4

    move-object/from16 v6, p7

    move/from16 v7, p8

    .line 1536
    new-instance v8, Ljava/util/HashMap;

    invoke-direct {v8}, Ljava/util/HashMap;-><init>()V

    .line 1537
    invoke-static {v2, v8}, Lcom/appsflyer/o;->ˊ(Landroid/content/Context;Ljava/util/Map;)V

    .line 1539
    new-instance v9, Ljava/util/Date;

    invoke-direct {v9}, Ljava/util/Date;-><init>()V

    invoke-virtual {v9}, Ljava/util/Date;->getTime()J

    move-result-wide v9

    const-string v11, "af_timestamp"

    .line 1540
    invoke-static {v9, v10}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v12

    invoke-interface {v8, v11, v12}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1542
    invoke-static {v2, v9, v10}, Lcom/appsflyer/a;->ˋ(Landroid/content/Context;J)Ljava/lang/String;

    move-result-object v9

    if-eqz v9, :cond_0

    const-string v10, "cksm_v1"

    .line 1544
    invoke-interface {v8, v10, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1548
    :cond_0
    :try_start_0
    invoke-virtual/range {p0 .. p0}, Lcom/appsflyer/AppsFlyerLib;->isTrackingStopped()Z

    move-result v9

    if-nez v9, :cond_2

    .line 1549
    new-instance v9, Ljava/lang/StringBuilder;

    const-string v10, "******* sendTrackingWithEvent: "

    invoke-direct {v9, v10}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    if-eqz v7, :cond_1

    const-string v10, "Launch"

    goto :goto_0

    :cond_1
    move-object v10, v4

    :goto_0
    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v9

    invoke-static {v9}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    goto :goto_1

    :cond_2
    const-string v9, "SDK tracking has been stopped"

    .line 1551
    invoke-static {v9}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    :goto_1
    const-string v9, "AppsFlyer_4.8.11"

    const-string v10, "EVENT_CREATED_WITH_NAME"

    if-eqz v7, :cond_3

    const-string v11, "Launch"

    goto :goto_2

    :cond_3
    move-object v11, v4

    .line 1553
    :goto_2
    invoke-static {v2, v9, v10, v11}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 1554
    invoke-static {}, Lcom/appsflyer/cache/CacheManager;->getInstance()Lcom/appsflyer/cache/CacheManager;

    move-result-object v9

    invoke-virtual {v9, v2}, Lcom/appsflyer/cache/CacheManager;->init(Landroid/content/Context;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_f

    const/4 v9, 0x0

    .line 1558
    :try_start_1
    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v10

    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v11

    const/16 v12, 0x1000

    invoke-virtual {v10, v11, v12}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v10

    .line 1559
    iget-object v10, v10, Landroid/content/pm/PackageInfo;->requestedPermissions:[Ljava/lang/String;

    invoke-static {v10}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v10

    const-string v11, "android.permission.INTERNET"

    .line 1560
    invoke-interface {v10, v11}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v11

    if-nez v11, :cond_4

    const-string v11, "Permission android.permission.INTERNET is missing in the AndroidManifest.xml"

    .line 1561
    invoke-static {v11}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    const-string v11, "PERMISSION_INTERNET_MISSING"

    .line 1562
    invoke-static {v2, v9, v11, v9}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    :cond_4
    const-string v11, "android.permission.ACCESS_NETWORK_STATE"

    .line 1564
    invoke-interface {v10, v11}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v11

    if-nez v11, :cond_5

    const-string v11, "Permission android.permission.ACCESS_NETWORK_STATE is missing in the AndroidManifest.xml"

    .line 1565
    invoke-static {v11}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    :cond_5
    const-string v11, "android.permission.ACCESS_WIFI_STATE"

    .line 1567
    invoke-interface {v10, v11}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v10

    if-nez v10, :cond_6

    const-string v10, "Permission android.permission.ACCESS_WIFI_STATE is missing in the AndroidManifest.xml"

    .line 1568
    invoke-static {v10}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_f

    goto :goto_3

    :catch_0
    move-exception v0

    move-object v10, v0

    :try_start_2
    const-string v11, "Exception while validation permissions. "

    .line 1571
    invoke-static {v11, v10}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_6
    :goto_3
    if-eqz p6, :cond_7

    const-string v10, "af_events_api"

    const-string v11, "1"

    .line 1575
    invoke-interface {v8, v10, v11}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_7
    const-string v10, "brand"

    .line 1577
    sget-object v11, Landroid/os/Build;->BRAND:Ljava/lang/String;

    invoke-interface {v8, v10, v11}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v10, "device"

    .line 1578
    sget-object v11, Landroid/os/Build;->DEVICE:Ljava/lang/String;

    invoke-interface {v8, v10, v11}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v10, "product"

    .line 1579
    sget-object v11, Landroid/os/Build;->PRODUCT:Ljava/lang/String;

    invoke-interface {v8, v10, v11}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v10, "sdk"

    .line 1580
    sget v11, Landroid/os/Build$VERSION;->SDK_INT:I

    invoke-static {v11}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v11

    invoke-interface {v8, v10, v11}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v10, "model"

    .line 1581
    sget-object v11, Landroid/os/Build;->MODEL:Ljava/lang/String;

    invoke-interface {v8, v10, v11}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v10, "deviceType"

    .line 1582
    sget-object v11, Landroid/os/Build;->TYPE:Ljava/lang/String;

    invoke-interface {v8, v10, v11}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-wide/16 v13, 0x0

    const/4 v15, 0x1

    const/4 v12, 0x0

    if-eqz v7, :cond_e

    const-string v10, "appsflyer-data"

    .line 1585
    invoke-virtual {v2, v10, v12}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v10

    const-string v11, "appsFlyerCount"

    invoke-interface {v10, v11}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v10

    xor-int/2addr v10, v15

    if-eqz v10, :cond_c

    .line 1586
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v10

    invoke-virtual {v10}, Lcom/appsflyer/AppsFlyerProperties;->isOtherSdkStringDisabled()Z

    move-result v10

    if-nez v10, :cond_8

    const-string v10, "af_sdks"

    .line 1587
    new-instance v11, Ljava/lang/StringBuilder;

    invoke-direct {v11}, Ljava/lang/StringBuilder;-><init>()V

    const-string v15, "com.tune.Tune"

    iget-object v9, v1, Lcom/appsflyer/AppsFlyerLib;->ˌ:Lcom/appsflyer/t;

    invoke-virtual {v9, v15}, Lcom/appsflyer/t;->ˎ(Ljava/lang/String;)Z

    move-result v9

    invoke-virtual {v11, v9}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v9, "com.adjust.sdk.Adjust"

    iget-object v15, v1, Lcom/appsflyer/AppsFlyerLib;->ˌ:Lcom/appsflyer/t;

    invoke-virtual {v15, v9}, Lcom/appsflyer/t;->ˎ(Ljava/lang/String;)Z

    move-result v9

    invoke-virtual {v11, v9}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v9, "com.kochava.android.tracker.Feature"

    iget-object v15, v1, Lcom/appsflyer/AppsFlyerLib;->ˌ:Lcom/appsflyer/t;

    invoke-virtual {v15, v9}, Lcom/appsflyer/t;->ˎ(Ljava/lang/String;)Z

    move-result v9

    invoke-virtual {v11, v9}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v9, "io.branch.referral.Branch"

    iget-object v15, v1, Lcom/appsflyer/AppsFlyerLib;->ˌ:Lcom/appsflyer/t;

    invoke-virtual {v15, v9}, Lcom/appsflyer/t;->ˎ(Ljava/lang/String;)Z

    move-result v9

    invoke-virtual {v11, v9}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v9, "com.apsalar.sdk.Apsalar"

    iget-object v15, v1, Lcom/appsflyer/AppsFlyerLib;->ˌ:Lcom/appsflyer/t;

    invoke-virtual {v15, v9}, Lcom/appsflyer/t;->ˎ(Ljava/lang/String;)Z

    move-result v9

    invoke-virtual {v11, v9}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v9, "com.localytics.android.Localytics"

    iget-object v15, v1, Lcom/appsflyer/AppsFlyerLib;->ˌ:Lcom/appsflyer/t;

    invoke-virtual {v15, v9}, Lcom/appsflyer/t;->ˎ(Ljava/lang/String;)Z

    move-result v9

    invoke-virtual {v11, v9}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v9, "com.tenjin.android.TenjinSDK"

    iget-object v15, v1, Lcom/appsflyer/AppsFlyerLib;->ˌ:Lcom/appsflyer/t;

    invoke-virtual {v15, v9}, Lcom/appsflyer/t;->ˎ(Ljava/lang/String;)Z

    move-result v9

    invoke-virtual {v11, v9}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v9, "place holder for TD"

    iget-object v15, v1, Lcom/appsflyer/AppsFlyerLib;->ˌ:Lcom/appsflyer/t;

    invoke-virtual {v15, v9}, Lcom/appsflyer/t;->ˎ(Ljava/lang/String;)Z

    move-result v9

    invoke-virtual {v11, v9}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v9, "it.partytrack.sdk.Track"

    iget-object v15, v1, Lcom/appsflyer/AppsFlyerLib;->ˌ:Lcom/appsflyer/t;

    invoke-virtual {v15, v9}, Lcom/appsflyer/t;->ˎ(Ljava/lang/String;)Z

    move-result v9

    invoke-virtual {v11, v9}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v9, "jp.appAdForce.android.LtvManager"

    iget-object v15, v1, Lcom/appsflyer/AppsFlyerLib;->ˌ:Lcom/appsflyer/t;

    invoke-virtual {v15, v9}, Lcom/appsflyer/t;->ˎ(Ljava/lang/String;)Z

    move-result v9

    invoke-virtual {v11, v9}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v11}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v9

    invoke-interface {v8, v10, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1588
    invoke-static/range {p1 .. p1}, Lcom/appsflyer/AppsFlyerLib;->ॱॱ(Landroid/content/Context;)F

    move-result v9

    const-string v10, "batteryLevel"

    .line 1589
    invoke-static {v9}, Ljava/lang/String;->valueOf(F)Ljava/lang/String;

    move-result-object v9

    invoke-interface {v8, v10, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_8
    const/16 v9, 0x12

    const-string v10, "OPPO"

    .line 1591
    sget-object v11, Landroid/os/Build;->BRAND:Ljava/lang/String;

    invoke-virtual {v10, v11}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v10

    if-eqz v10, :cond_9

    const/16 v9, 0x17

    const-string v10, "OPPO device found"

    invoke-static {v10}, Lcom/appsflyer/AFLogger;->afRDLog(Ljava/lang/String;)V

    :cond_9
    sget v10, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v10, v9, :cond_b

    new-instance v9, Ljava/lang/StringBuilder;

    const-string v10, "OS SDK is="

    invoke-direct {v9, v10}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    sget v10, Landroid/os/Build$VERSION;->SDK_INT:I

    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v10, "; use KeyStore"

    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v9

    invoke-static {v9}, Lcom/appsflyer/AFLogger;->afRDLog(Ljava/lang/String;)V

    new-instance v9, Lcom/appsflyer/AFKeystoreWrapper;

    invoke-direct {v9, v2}, Lcom/appsflyer/AFKeystoreWrapper;-><init>(Landroid/content/Context;)V

    invoke-virtual {v9}, Lcom/appsflyer/AFKeystoreWrapper;->ˋ()Z

    move-result v10

    if-nez v10, :cond_a

    new-instance v10, Ljava/lang/ref/WeakReference;

    invoke-direct {v10, v2}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    invoke-static {v10}, Lcom/appsflyer/p;->ˋ(Ljava/lang/ref/WeakReference;)Ljava/lang/String;

    move-result-object v10

    invoke-virtual {v9, v10}, Lcom/appsflyer/AFKeystoreWrapper;->ˏ(Ljava/lang/String;)V

    const-string v10, "KSAppsFlyerId"

    invoke-virtual {v9}, Lcom/appsflyer/AFKeystoreWrapper;->ˊ()Ljava/lang/String;

    move-result-object v11

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v15

    invoke-virtual {v15, v10, v11}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    const-string v10, "KSAppsFlyerRICounter"

    invoke-virtual {v9}, Lcom/appsflyer/AFKeystoreWrapper;->ॱ()I

    move-result v9

    invoke-static {v9}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v9

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v11

    :goto_4
    invoke-virtual {v11, v10, v9}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_5

    :cond_a
    invoke-virtual {v9}, Lcom/appsflyer/AFKeystoreWrapper;->ˎ()V

    const-string v10, "KSAppsFlyerId"

    invoke-virtual {v9}, Lcom/appsflyer/AFKeystoreWrapper;->ˊ()Ljava/lang/String;

    move-result-object v11

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v15

    invoke-virtual {v15, v10, v11}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    const-string v10, "KSAppsFlyerRICounter"

    invoke-virtual {v9}, Lcom/appsflyer/AFKeystoreWrapper;->ॱ()I

    move-result v9

    invoke-static {v9}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v9

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v11

    goto :goto_4

    :cond_b
    new-instance v9, Ljava/lang/StringBuilder;

    const-string v10, "OS SDK is="

    invoke-direct {v9, v10}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    sget v10, Landroid/os/Build$VERSION;->SDK_INT:I

    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v10, "; no KeyStore usage"

    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v9}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v9

    invoke-static {v9}, Lcom/appsflyer/AFLogger;->afRDLog(Ljava/lang/String;)V

    :cond_c
    :goto_5
    const-string v9, "timepassedsincelastlaunch"

    const-string v10, "appsflyer-data"

    .line 1593
    invoke-virtual {v2, v10, v12}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v10

    const-string v11, "AppsFlyerTimePassedSincePrevLaunch"

    invoke-interface {v10, v11, v13, v14}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v10

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v12

    const-string v14, "AppsFlyerTimePassedSincePrevLaunch"

    invoke-static {v2, v14, v12, v13}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/Context;Ljava/lang/String;J)V

    const-wide/16 v14, 0x0

    cmp-long v16, v10, v14

    if-lez v16, :cond_d

    sub-long/2addr v12, v10

    const-wide/16 v10, 0x3e8

    div-long v10, v12, v10

    goto :goto_6

    :cond_d
    const-wide/16 v10, -0x1

    :goto_6
    invoke-static {v10, v11}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v10

    invoke-interface {v8, v9, v10}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1595
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v9

    const-string v10, "oneLinkSlug"

    invoke-virtual {v9, v10}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    if-eqz v9, :cond_11

    const-string v10, "onelink_id"

    invoke-interface {v8, v10, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v9, "ol_ver"

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v10

    const-string v11, "onelinkVersion"

    invoke-virtual {v10, v11}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v10

    invoke-interface {v8, v9, v10}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_7

    :cond_e
    const-string v9, "appsflyer-data"

    const/4 v10, 0x0

    .line 1597
    invoke-virtual {v2, v9, v10}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v9

    invoke-interface {v9}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v10
    :try_end_2
    .catch Ljava/lang/Throwable; {:try_start_2 .. :try_end_2} :catch_f

    :try_start_3
    const-string v11, "prev_event_name"

    const/4 v12, 0x0

    invoke-interface {v9, v11, v12}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v11

    if-eqz v11, :cond_f

    new-instance v12, Lorg/json/JSONObject;

    invoke-direct {v12}, Lorg/json/JSONObject;-><init>()V

    const-string v13, "prev_event_timestamp"

    new-instance v14, Ljava/lang/StringBuilder;

    invoke-direct {v14}, Ljava/lang/StringBuilder;-><init>()V

    const-string v15, "prev_event_timestamp"

    const-wide/16 v6, -0x1

    invoke-interface {v9, v15, v6, v7}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v6

    invoke-virtual {v14, v6, v7}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {v14}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v12, v13, v6}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v6, "prev_event_value"

    const-string v7, "prev_event_value"

    const/4 v13, 0x0

    invoke-interface {v9, v7, v13}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v12, v6, v7}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v6, "prev_event_name"

    invoke-virtual {v12, v6, v11}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v6, "prev_event"

    invoke-virtual {v12}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-interface {v8, v6, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_f
    const-string v6, "prev_event_name"

    invoke-interface {v10, v6, v4}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    const-string v6, "prev_event_value"

    invoke-interface {v10, v6, v5}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    const-string v6, "prev_event_timestamp"

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v11

    invoke-interface {v10, v6, v11, v12}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    sget v6, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v7, 0x9

    if-lt v6, v7, :cond_10

    invoke-interface {v10}, Landroid/content/SharedPreferences$Editor;->apply()V

    goto :goto_7

    :cond_10
    invoke-interface {v10}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_1
    .catch Ljava/lang/Throwable; {:try_start_3 .. :try_end_3} :catch_f

    goto :goto_7

    :catch_1
    move-exception v0

    move-object v6, v0

    :try_start_4
    const-string v7, "Error while processing previous event."

    invoke-static {v7, v6}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_11
    :goto_7
    const-string v6, "KSAppsFlyerId"

    .line 1600
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v7

    invoke-virtual {v7, v6}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v7, "KSAppsFlyerRICounter"

    .line 1601
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v9

    invoke-virtual {v9, v7}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    if-eqz v6, :cond_12

    if-eqz v7, :cond_12

    .line 1602
    invoke-static {v7}, Ljava/lang/Integer;->valueOf(Ljava/lang/String;)Ljava/lang/Integer;

    move-result-object v9

    invoke-virtual {v9}, Ljava/lang/Number;->intValue()I

    move-result v9

    if-lez v9, :cond_12

    const-string v9, "reinstallCounter"

    .line 1603
    invoke-interface {v8, v9, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v7, "originalAppsflyerId"

    .line 1604
    invoke-interface {v8, v7, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_12
    const-string v6, "additionalCustomData"

    .line 1607
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v7

    invoke-virtual {v7, v6}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    if-eqz v6, :cond_13

    const-string v7, "customData"

    .line 1609
    invoke-interface {v8, v7, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_4
    .catch Ljava/lang/Throwable; {:try_start_4 .. :try_end_4} :catch_f

    .line 1613
    :cond_13
    :try_start_5
    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v6

    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Landroid/content/pm/PackageManager;->getInstallerPackageName(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    if-eqz v6, :cond_14

    const-string v7, "installer_package"

    .line 1615
    invoke-interface {v8, v7, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_5
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_2
    .catch Ljava/lang/Throwable; {:try_start_5 .. :try_end_5} :catch_f

    goto :goto_8

    :catch_2
    move-exception v0

    move-object v6, v0

    :try_start_6
    const-string v7, "Exception while getting the app\'s installer package. "

    .line 1618
    invoke-static {v7, v6}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 1621
    :cond_14
    :goto_8
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v6

    const-string v7, "sdkExtension"

    invoke-virtual {v6, v7}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    if-eqz v6, :cond_15

    .line 1622
    invoke-virtual {v6}, Ljava/lang/String;->length()I

    move-result v7

    if-lez v7, :cond_15

    const-string v7, "sdkExtension"

    .line 1623
    invoke-interface {v8, v7, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1626
    :cond_15
    new-instance v6, Ljava/lang/ref/WeakReference;

    invoke-direct {v6, v2}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v7

    const-string v9, "channel"

    invoke-virtual {v7, v9}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    if-nez v7, :cond_16

    const-string v7, "CHANNEL"

    invoke-static {v6, v7}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Ljava/lang/ref/WeakReference;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    .line 1628
    :cond_16
    invoke-static {v2, v7}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/Context;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    if-eqz v6, :cond_17

    const-string v9, "channel"

    .line 1630
    invoke-interface {v8, v9, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_17
    if-eqz v6, :cond_18

    .line 1633
    invoke-virtual {v6, v7}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v9

    if-eqz v9, :cond_19

    :cond_18
    if-nez v6, :cond_1a

    if-eqz v7, :cond_1a

    :cond_19
    const-string v6, "af_latestchannel"

    .line 1635
    invoke-interface {v8, v6, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1a
    const-string v6, "appsflyer-data"

    const/4 v7, 0x0

    .line 1638
    invoke-virtual {v2, v6, v7}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v6

    const-string v7, "INSTALL_STORE"

    invoke-interface {v6, v7}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v7

    if-eqz v7, :cond_1b

    const-string v7, "INSTALL_STORE"

    const/4 v9, 0x0

    invoke-interface {v6, v7, v9}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    goto :goto_a

    :cond_1b
    const-string v6, "appsflyer-data"

    const/4 v7, 0x0

    invoke-virtual {v2, v6, v7}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v6

    const-string v7, "appsFlyerCount"

    invoke-interface {v6, v7}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v6

    const/4 v7, 0x1

    xor-int/2addr v6, v7

    if-eqz v6, :cond_1c

    new-instance v6, Ljava/lang/ref/WeakReference;

    invoke-direct {v6, v2}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    const-string v7, "AF_STORE"

    invoke-static {v6, v7}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Ljava/lang/ref/WeakReference;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    move-object v6, v9

    goto :goto_9

    :cond_1c
    const/4 v6, 0x0

    :goto_9
    const-string v7, "INSTALL_STORE"

    invoke-static {v2, v7, v6}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    :goto_a
    if-eqz v6, :cond_1d

    const-string v7, "af_installstore"

    .line 1640
    invoke-virtual {v6}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v6

    invoke-interface {v8, v7, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_1d
    const-string v6, "appsflyer-data"

    const/4 v7, 0x0

    .line 1643
    invoke-virtual {v2, v6, v7}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v6

    const-string v7, "preInstallName"

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v9

    invoke-virtual {v9, v7}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    if-nez v7, :cond_2e

    const-string v9, "preInstallName"

    invoke-interface {v6, v9}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_1e

    const-string v7, "preInstallName"

    const/4 v9, 0x0

    invoke-interface {v6, v7, v9}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    move-object v7, v6

    goto/16 :goto_15

    :cond_1e
    const-string v6, "appsflyer-data"

    const/4 v9, 0x0

    invoke-virtual {v2, v6, v9}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v6

    const-string v9, "appsFlyerCount"

    invoke-interface {v6, v9}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v6

    const/4 v9, 0x1

    xor-int/2addr v6, v9

    if-eqz v6, :cond_2c

    const-string v6, "ro.appsflyer.preinstall.path"

    invoke-static {v6}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    invoke-static {v6}, Lcom/appsflyer/AppsFlyerLib;->ˏ(Ljava/lang/String;)Ljava/io/File;

    move-result-object v6

    if-eqz v6, :cond_20

    invoke-virtual {v6}, Ljava/io/File;->exists()Z

    move-result v7

    if-nez v7, :cond_1f

    goto :goto_b

    :cond_1f
    const/4 v7, 0x0

    goto :goto_c

    :cond_20
    :goto_b
    const/4 v7, 0x1

    :goto_c
    if-eqz v7, :cond_21

    const-string v6, "AF_PRE_INSTALL_PATH"

    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v7

    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v9

    invoke-static {v6, v7, v9}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Ljava/lang/String;Landroid/content/pm/PackageManager;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    invoke-static {v6}, Lcom/appsflyer/AppsFlyerLib;->ˏ(Ljava/lang/String;)Ljava/io/File;

    move-result-object v6

    :cond_21
    if-eqz v6, :cond_23

    invoke-virtual {v6}, Ljava/io/File;->exists()Z

    move-result v7

    if-nez v7, :cond_22

    goto :goto_d

    :cond_22
    const/4 v7, 0x0

    goto :goto_e

    :cond_23
    :goto_d
    const/4 v7, 0x1

    :goto_e
    if-eqz v7, :cond_24

    const-string v6, "/data/local/tmp/pre_install.appsflyer"

    invoke-static {v6}, Lcom/appsflyer/AppsFlyerLib;->ˏ(Ljava/lang/String;)Ljava/io/File;

    move-result-object v6

    :cond_24
    if-eqz v6, :cond_26

    invoke-virtual {v6}, Ljava/io/File;->exists()Z

    move-result v7

    if-nez v7, :cond_25

    goto :goto_f

    :cond_25
    const/4 v7, 0x0

    goto :goto_10

    :cond_26
    :goto_f
    const/4 v7, 0x1

    :goto_10
    if-eqz v7, :cond_27

    const-string v6, "/etc/pre_install.appsflyer"

    invoke-static {v6}, Lcom/appsflyer/AppsFlyerLib;->ˏ(Ljava/lang/String;)Ljava/io/File;

    move-result-object v6

    :cond_27
    if-eqz v6, :cond_29

    invoke-virtual {v6}, Ljava/io/File;->exists()Z

    move-result v7

    if-nez v7, :cond_28

    goto :goto_11

    :cond_28
    const/4 v7, 0x0

    goto :goto_12

    :cond_29
    :goto_11
    const/4 v7, 0x1

    :goto_12
    if-nez v7, :cond_2a

    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v7

    invoke-static {v6, v7}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Ljava/io/File;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    if-eqz v9, :cond_2a

    move-object v7, v9

    goto :goto_13

    :cond_2a
    const/4 v7, 0x0

    :goto_13
    if-eqz v7, :cond_2b

    goto :goto_14

    :cond_2b
    new-instance v6, Ljava/lang/ref/WeakReference;

    invoke-direct {v6, v2}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    const-string v7, "AF_PRE_INSTALL_NAME"

    invoke-static {v6, v7}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Ljava/lang/ref/WeakReference;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    :cond_2c
    :goto_14
    if-eqz v7, :cond_2d

    const-string v6, "preInstallName"

    invoke-static {v2, v6, v7}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    :cond_2d
    :goto_15
    if-eqz v7, :cond_2e

    const-string v6, "preInstallName"

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v9

    invoke-virtual {v9, v6, v7}, Lcom/appsflyer/AppsFlyerProperties;->set(Ljava/lang/String;Ljava/lang/String;)V

    :cond_2e
    if-eqz v7, :cond_2f

    const-string v6, "af_preinstall_name"

    .line 1645
    invoke-virtual {v7}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v7

    invoke-interface {v8, v6, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1648
    :cond_2f
    new-instance v6, Ljava/lang/ref/WeakReference;

    invoke-direct {v6, v2}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    const-string v7, "AF_STORE"

    invoke-static {v6, v7}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Ljava/lang/ref/WeakReference;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    if-eqz v6, :cond_30

    const-string v7, "af_currentstore"

    .line 1650
    invoke-virtual {v6}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v6

    invoke-interface {v8, v7, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_30
    if-eqz v3, :cond_31

    .line 1653
    invoke-virtual/range {p2 .. p2}, Ljava/lang/String;->length()I

    move-result v6

    if-ltz v6, :cond_31

    const-string v6, "appsflyerKey"

    .line 1654
    invoke-interface {v8, v6, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_16

    :cond_31
    const-string v3, "AppsFlyerKey"

    .line 1656
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v6

    invoke-virtual {v6, v3}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_69

    .line 1657
    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v6

    if-ltz v6, :cond_69

    const-string v6, "appsflyerKey"

    .line 1658
    invoke-interface {v8, v6, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :goto_16
    const-string v3, "AppUserId"

    .line 1663
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v6

    invoke-virtual {v6, v3}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_32

    const-string v6, "appUserId"

    .line 1669
    invoke-interface {v8, v6, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1672
    :cond_32
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v3

    const-string v6, "userEmails"

    invoke-virtual {v3, v6}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_33

    const-string v6, "user_emails"

    .line 1675
    invoke-interface {v8, v6, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_17

    :cond_33
    const-string v3, "userEmail"

    .line 1677
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v6

    invoke-virtual {v6, v3}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_34

    const-string v6, "sha1_el"

    .line 1679
    invoke-static {v3}, Lcom/appsflyer/r;->ॱ(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v8, v6, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_34
    :goto_17
    if-eqz v4, :cond_35

    const-string v3, "eventName"

    .line 1684
    invoke-interface {v8, v3, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    if-eqz v5, :cond_35

    const-string v3, "eventValue"

    .line 1686
    invoke-interface {v8, v3, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_35
    const-string v3, "appid"

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v5

    invoke-virtual {v5, v3}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_36

    const-string v3, "appid"

    const-string v5, "appid"

    .line 1691
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v6

    invoke-virtual {v6, v5}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-interface {v8, v3, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_36
    const-string v3, "currencyCode"

    .line 1693
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v5

    invoke-virtual {v5, v3}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const/4 v5, 0x3

    if-eqz v3, :cond_38

    .line 1695
    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v6

    if-eq v6, v5, :cond_37

    .line 1696
    new-instance v6, Ljava/lang/StringBuilder;

    const-string v7, "WARNING: currency code should be 3 characters!!! \'"

    invoke-direct {v6, v7}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v6, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v7, "\' is not a legal value."

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-static {v6}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    :cond_37
    const-string v6, "currency"

    .line 1698
    invoke-interface {v8, v6, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_38
    const-string v3, "IS_UPDATE"

    .line 1701
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v6

    invoke-virtual {v6, v3}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_39

    const-string v6, "isUpdate"

    .line 1703
    invoke-interface {v8, v6, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1705
    :cond_39
    invoke-virtual/range {p0 .. p1}, Lcom/appsflyer/AppsFlyerLib;->isPreInstalledApp(Landroid/content/Context;)Z

    move-result v3

    const-string v6, "af_preinstalled"

    .line 1706
    invoke-static {v3}, Ljava/lang/Boolean;->toString(Z)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v8, v6, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1708
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v3

    const-string v6, "collectFacebookAttrId"

    const/4 v7, 0x1

    invoke-virtual {v3, v6, v7}, Lcom/appsflyer/AppsFlyerProperties;->getBoolean(Ljava/lang/String;Z)Z

    move-result v3
    :try_end_6
    .catch Ljava/lang/Throwable; {:try_start_6 .. :try_end_6} :catch_f

    if-eqz v3, :cond_3a

    .line 1713
    :try_start_7
    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v3

    const-string v6, "com.facebook.katana"

    const/4 v7, 0x0

    invoke-virtual {v3, v6, v7}, Landroid/content/pm/PackageManager;->getApplicationInfo(Ljava/lang/String;I)Landroid/content/pm/ApplicationInfo;

    .line 1714
    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v3

    invoke-virtual {v1, v3}, Lcom/appsflyer/AppsFlyerLib;->getAttributionId(Landroid/content/ContentResolver;)Ljava/lang/String;

    move-result-object v9
    :try_end_7
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_7 .. :try_end_7} :catch_4
    .catch Ljava/lang/Throwable; {:try_start_7 .. :try_end_7} :catch_3

    goto :goto_19

    :catch_3
    move-exception v0

    move-object v3, v0

    :try_start_8
    const-string v6, "Exception while collecting facebook\'s attribution ID. "

    .line 1720
    invoke-static {v6, v3}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_18
    const/4 v9, 0x0

    goto :goto_19

    :catch_4
    const-string v3, "Exception while collecting facebook\'s attribution ID. "

    .line 1717
    invoke-static {v3}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    goto :goto_18

    :goto_19
    if-eqz v9, :cond_3a

    const-string v3, "fb"

    .line 1723
    invoke-interface {v8, v3, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1727
    :cond_3a
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v3

    const-string v6, "deviceTrackingDisabled"

    const/4 v7, 0x0

    invoke-virtual {v3, v6, v7}, Lcom/appsflyer/AppsFlyerProperties;->getBoolean(Ljava/lang/String;Z)Z

    move-result v3

    if-eqz v3, :cond_3b

    const-string v3, "deviceTrackingDisabled"

    const-string v6, "true"

    invoke-interface {v8, v3, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto/16 :goto_26

    :cond_3b
    const-string v3, "appsflyer-data"

    const/4 v6, 0x0

    invoke-virtual {v2, v3, v6}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v3

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v6

    const-string v7, "collectIMEI"

    const/4 v9, 0x1

    invoke-virtual {v6, v7, v9}, Lcom/appsflyer/AppsFlyerProperties;->getBoolean(Ljava/lang/String;Z)Z

    move-result v6

    const-string v7, "imeiCached"

    const/4 v9, 0x0

    invoke-interface {v3, v7, v9}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    const/16 v9, 0x13

    if-eqz v6, :cond_42

    sget v6, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v6, v9, :cond_3d

    invoke-static/range {p1 .. p1}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Landroid/content/Context;)Z

    move-result v6
    :try_end_8
    .catch Ljava/lang/Throwable; {:try_start_8 .. :try_end_8} :catch_f

    if-nez v6, :cond_3c

    goto :goto_1a

    :cond_3c
    const/4 v6, 0x0

    goto :goto_1b

    :cond_3d
    :goto_1a
    const/4 v6, 0x1

    :goto_1b
    if-eqz v6, :cond_41

    :try_start_9
    const-string v6, "phone"

    invoke-virtual {v2, v6}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroid/telephony/TelephonyManager;

    invoke-virtual {v6}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v10

    const-string v11, "getDeviceId"

    const/4 v12, 0x0

    new-array v13, v12, [Ljava/lang/Class;

    invoke-virtual {v10, v11, v13}, Ljava/lang/Class;->getMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v10

    new-array v11, v12, [Ljava/lang/Object;

    invoke-virtual {v10, v6, v11}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    if-eqz v6, :cond_3e

    goto :goto_1f

    :cond_3e
    iget-object v6, v1, Lcom/appsflyer/AppsFlyerLib;->ˊ:Ljava/lang/String;

    if-eqz v6, :cond_3f

    iget-object v6, v1, Lcom/appsflyer/AppsFlyerLib;->ˊ:Ljava/lang/String;
    :try_end_9
    .catch Ljava/lang/reflect/InvocationTargetException; {:try_start_9 .. :try_end_9} :catch_6
    .catch Ljava/lang/Exception; {:try_start_9 .. :try_end_9} :catch_5
    .catch Ljava/lang/Throwable; {:try_start_9 .. :try_end_9} :catch_f

    goto :goto_1f

    :cond_3f
    if-eqz v7, :cond_40

    goto :goto_1c

    :cond_40
    const/4 v7, 0x0

    :goto_1c
    move-object v6, v7

    goto :goto_1f

    :catch_5
    move-exception v0

    move-object v6, v0

    :try_start_a
    const-string v7, "WARNING: READ_PHONE_STATE is missing. "

    invoke-static {v7, v6}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_1e

    :catch_6
    const-string v6, "WARNING: READ_PHONE_STATE is missing."

    invoke-static {v6}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    goto :goto_1e

    :cond_41
    iget-object v6, v1, Lcom/appsflyer/AppsFlyerLib;->ˊ:Ljava/lang/String;

    if-eqz v6, :cond_43

    :goto_1d
    iget-object v6, v1, Lcom/appsflyer/AppsFlyerLib;->ˊ:Ljava/lang/String;

    goto :goto_1f

    :cond_42
    iget-object v6, v1, Lcom/appsflyer/AppsFlyerLib;->ˊ:Ljava/lang/String;

    if-eqz v6, :cond_43

    goto :goto_1d

    :cond_43
    :goto_1e
    const/4 v6, 0x0

    :goto_1f
    if-eqz v6, :cond_44

    const-string v7, "imeiCached"

    invoke-static {v2, v7, v6}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    const-string v7, "imei"

    invoke-interface {v8, v7, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_20

    :cond_44
    const-string v6, "IMEI was not collected."

    invoke-static {v6}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    :goto_20
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v6

    const-string v7, "collectAndroidId"

    const/4 v10, 0x1

    invoke-virtual {v6, v7, v10}, Lcom/appsflyer/AppsFlyerProperties;->getBoolean(Ljava/lang/String;Z)Z

    move-result v6

    const-string v7, "androidIdCached"

    const/4 v10, 0x0

    invoke-interface {v3, v7, v10}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-eqz v6, :cond_4a

    sget v6, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v6, v9, :cond_46

    invoke-static/range {p1 .. p1}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Landroid/content/Context;)Z

    move-result v6
    :try_end_a
    .catch Ljava/lang/Throwable; {:try_start_a .. :try_end_a} :catch_f

    if-nez v6, :cond_45

    goto :goto_21

    :cond_45
    const/4 v6, 0x0

    goto :goto_22

    :cond_46
    :goto_21
    const/4 v6, 0x1

    :goto_22
    if-eqz v6, :cond_49

    :try_start_b
    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v6

    const-string v7, "android_id"

    invoke-static {v6, v7}, Landroid/provider/Settings$Secure;->getString(Landroid/content/ContentResolver;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    if-eqz v9, :cond_47

    goto :goto_25

    :cond_47
    iget-object v6, v1, Lcom/appsflyer/AppsFlyerLib;->ˋ:Ljava/lang/String;

    if-eqz v6, :cond_48

    iget-object v9, v1, Lcom/appsflyer/AppsFlyerLib;->ˋ:Ljava/lang/String;
    :try_end_b
    .catch Ljava/lang/Exception; {:try_start_b .. :try_end_b} :catch_7
    .catch Ljava/lang/Throwable; {:try_start_b .. :try_end_b} :catch_f

    goto :goto_25

    :cond_48
    if-eqz v3, :cond_4b

    move-object v9, v3

    goto :goto_25

    :catch_7
    move-exception v0

    move-object v3, v0

    :try_start_c
    invoke-virtual {v3}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v6

    invoke-static {v6, v3}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_24

    :cond_49
    iget-object v3, v1, Lcom/appsflyer/AppsFlyerLib;->ˋ:Ljava/lang/String;

    if-eqz v3, :cond_4b

    :goto_23
    iget-object v9, v1, Lcom/appsflyer/AppsFlyerLib;->ˋ:Ljava/lang/String;

    goto :goto_25

    :cond_4a
    iget-object v3, v1, Lcom/appsflyer/AppsFlyerLib;->ˋ:Ljava/lang/String;

    if-eqz v3, :cond_4b

    goto :goto_23

    :cond_4b
    :goto_24
    const/4 v9, 0x0

    :goto_25
    if-eqz v9, :cond_4c

    const-string v3, "androidIdCached"

    invoke-static {v2, v3, v9}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    const-string v3, "android_id"

    invoke-interface {v8, v3, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_26

    :cond_4c
    const-string v3, "Android ID was not collected."

    invoke-static {v3}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V
    :try_end_c
    .catch Ljava/lang/Throwable; {:try_start_c .. :try_end_c} :catch_f

    .line 1730
    :goto_26
    :try_start_d
    new-instance v3, Ljava/lang/ref/WeakReference;

    invoke-direct {v3, v2}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    invoke-static {v3}, Lcom/appsflyer/p;->ˋ(Ljava/lang/ref/WeakReference;)Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_4d

    const-string v6, "uid"

    .line 1732
    invoke-interface {v8, v6, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_d
    .catch Ljava/lang/Exception; {:try_start_d .. :try_end_d} :catch_8
    .catch Ljava/lang/Throwable; {:try_start_d .. :try_end_d} :catch_f

    goto :goto_27

    :catch_8
    move-exception v0

    move-object v3, v0

    .line 1735
    :try_start_e
    new-instance v6, Ljava/lang/StringBuilder;

    const-string v7, "ERROR: could not get uid "

    invoke-direct {v6, v7}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v3}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-static {v6, v3}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_e
    .catch Ljava/lang/Throwable; {:try_start_e .. :try_end_e} :catch_f

    :cond_4d
    :goto_27
    :try_start_f
    const-string v3, "lang"

    .line 1739
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v6

    invoke-virtual {v6}, Ljava/util/Locale;->getDisplayLanguage()Ljava/lang/String;

    move-result-object v6

    invoke-interface {v8, v3, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_f
    .catch Ljava/lang/Exception; {:try_start_f .. :try_end_f} :catch_9
    .catch Ljava/lang/Throwable; {:try_start_f .. :try_end_f} :catch_f

    goto :goto_28

    :catch_9
    move-exception v0

    move-object v3, v0

    :try_start_10
    const-string v6, "Exception while collecting display language name. "

    .line 1741
    invoke-static {v6, v3}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_10
    .catch Ljava/lang/Throwable; {:try_start_10 .. :try_end_10} :catch_f

    :goto_28
    :try_start_11
    const-string v3, "lang_code"

    .line 1745
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v6

    invoke-virtual {v6}, Ljava/util/Locale;->getLanguage()Ljava/lang/String;

    move-result-object v6

    invoke-interface {v8, v3, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_11
    .catch Ljava/lang/Exception; {:try_start_11 .. :try_end_11} :catch_a
    .catch Ljava/lang/Throwable; {:try_start_11 .. :try_end_11} :catch_f

    goto :goto_29

    :catch_a
    move-exception v0

    move-object v3, v0

    :try_start_12
    const-string v6, "Exception while collecting display language code. "

    .line 1747
    invoke-static {v6, v3}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_12
    .catch Ljava/lang/Throwable; {:try_start_12 .. :try_end_12} :catch_f

    :goto_29
    :try_start_13
    const-string v3, "country"

    .line 1751
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v6

    invoke-virtual {v6}, Ljava/util/Locale;->getCountry()Ljava/lang/String;

    move-result-object v6

    invoke-interface {v8, v3, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_13
    .catch Ljava/lang/Exception; {:try_start_13 .. :try_end_13} :catch_b
    .catch Ljava/lang/Throwable; {:try_start_13 .. :try_end_13} :catch_f

    goto :goto_2a

    :catch_b
    move-exception v0

    move-object v3, v0

    :try_start_14
    const-string v6, "Exception while collecting country name. "

    .line 1753
    invoke-static {v6, v3}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_2a
    const-string v3, "platformextension"

    .line 1756
    iget-object v6, v1, Lcom/appsflyer/AppsFlyerLib;->ˌ:Lcom/appsflyer/t;

    invoke-virtual {v6}, Lcom/appsflyer/t;->ˎ()Ljava/lang/String;

    move-result-object v6

    invoke-interface {v8, v3, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1758
    invoke-static {v2, v8}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Landroid/content/Context;Ljava/util/Map;)V

    const-string v3, "yyyy-MM-dd_HHmmssZ"

    .line 1761
    new-instance v6, Ljava/text/SimpleDateFormat;

    sget-object v7, Ljava/util/Locale;->US:Ljava/util/Locale;

    invoke-direct {v6, v3, v7}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    .line 1763
    sget v3, Landroid/os/Build$VERSION;->SDK_INT:I
    :try_end_14
    .catch Ljava/lang/Throwable; {:try_start_14 .. :try_end_14} :catch_f

    const/16 v7, 0x9

    if-lt v3, v7, :cond_4e

    .line 1765
    :try_start_15
    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v3

    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v7

    const/4 v9, 0x0

    invoke-virtual {v3, v7, v9}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v3

    iget-wide v9, v3, Landroid/content/pm/PackageInfo;->firstInstallTime:J

    const-string v3, "installDate"

    const-string v7, "UTC"

    .line 1766
    invoke-static {v7}, Ljava/util/TimeZone;->getTimeZone(Ljava/lang/String;)Ljava/util/TimeZone;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/text/DateFormat;->setTimeZone(Ljava/util/TimeZone;)V

    new-instance v7, Ljava/util/Date;

    invoke-direct {v7, v9, v10}, Ljava/util/Date;-><init>(J)V

    invoke-virtual {v6, v7}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v7

    invoke-interface {v8, v3, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_15
    .catch Ljava/lang/Exception; {:try_start_15 .. :try_end_15} :catch_c
    .catch Ljava/lang/Throwable; {:try_start_15 .. :try_end_15} :catch_f

    goto :goto_2b

    :catch_c
    move-exception v0

    move-object v3, v0

    :try_start_16
    const-string v7, "Exception while collecting install date. "

    .line 1768
    invoke-static {v7, v3}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_16
    .catch Ljava/lang/Throwable; {:try_start_16 .. :try_end_16} :catch_f

    .line 1773
    :cond_4e
    :goto_2b
    :try_start_17
    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v3

    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v7

    const/4 v9, 0x0

    invoke-virtual {v3, v7, v9}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v3

    const-string v7, "versionCode"
    :try_end_17
    .catch Ljava/lang/Throwable; {:try_start_17 .. :try_end_17} :catch_e

    move-object/from16 v10, p7

    .line 1775
    :try_start_18
    invoke-interface {v10, v7, v9}, Landroid/content/SharedPreferences;->getInt(Ljava/lang/String;I)I

    move-result v7

    .line 1777
    iget v11, v3, Landroid/content/pm/PackageInfo;->versionCode:I

    if-le v11, v7, :cond_4f

    const-string v7, "appsflyerConversionDataRequestRetries"

    .line 1780
    invoke-static {v2, v7, v9}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Landroid/content/Context;Ljava/lang/String;I)V

    const-string v7, "versionCode"

    .line 1781
    iget v9, v3, Landroid/content/pm/PackageInfo;->versionCode:I

    invoke-static {v2, v7, v9}, Lcom/appsflyer/AppsFlyerLib;->ˎ(Landroid/content/Context;Ljava/lang/String;I)V

    :cond_4f
    const-string v7, "app_version_code"

    .line 1785
    iget v9, v3, Landroid/content/pm/PackageInfo;->versionCode:I

    invoke-static {v9}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v9

    invoke-interface {v8, v7, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v7, "app_version_name"

    .line 1786
    iget-object v9, v3, Landroid/content/pm/PackageInfo;->versionName:Ljava/lang/String;

    invoke-interface {v8, v7, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1788
    sget v7, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v9, 0x9

    if-lt v7, v9, :cond_52

    .line 1791
    iget-wide v11, v3, Landroid/content/pm/PackageInfo;->firstInstallTime:J

    .line 1792
    iget-wide v13, v3, Landroid/content/pm/PackageInfo;->lastUpdateTime:J

    const-string v3, "date1"

    const-string v7, "UTC"

    .line 1793
    invoke-static {v7}, Ljava/util/TimeZone;->getTimeZone(Ljava/lang/String;)Ljava/util/TimeZone;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/text/DateFormat;->setTimeZone(Ljava/util/TimeZone;)V

    new-instance v7, Ljava/util/Date;

    invoke-direct {v7, v11, v12}, Ljava/util/Date;-><init>(J)V

    invoke-virtual {v6, v7}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v7

    invoke-interface {v8, v3, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v3, "date2"

    const-string v7, "UTC"

    .line 1794
    invoke-static {v7}, Ljava/util/TimeZone;->getTimeZone(Ljava/lang/String;)Ljava/util/TimeZone;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/text/DateFormat;->setTimeZone(Ljava/util/TimeZone;)V

    new-instance v7, Ljava/util/Date;

    invoke-direct {v7, v13, v14}, Ljava/util/Date;-><init>(J)V

    invoke-virtual {v6, v7}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v7

    invoke-interface {v8, v3, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v3, "appsflyer-data"

    const/4 v7, 0x0

    .line 1795
    invoke-virtual {v2, v3, v7}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v3

    const-string v9, "appsFlyerFirstInstall"

    const/4 v11, 0x0

    invoke-interface {v3, v9, v11}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-nez v3, :cond_51

    const-string v3, "appsflyer-data"

    invoke-virtual {v2, v3, v7}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v3

    const-string v7, "appsFlyerCount"

    invoke-interface {v3, v7}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v3

    const/4 v7, 0x1

    xor-int/2addr v3, v7

    if-eqz v3, :cond_50

    const-string v3, "AppsFlyer: first launch detected"

    invoke-static {v3}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    new-instance v3, Ljava/util/Date;

    invoke-direct {v3}, Ljava/util/Date;-><init>()V

    invoke-virtual {v6, v3}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v3

    goto :goto_2c

    :cond_50
    const-string v3, ""

    :goto_2c
    const-string v6, "appsFlyerFirstInstall"

    invoke-static {v2, v6, v3}, Lcom/appsflyer/AppsFlyerLib;->ॱ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    :cond_51
    const-string v6, "AppsFlyer: first launch date: "

    invoke-static {v3}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    invoke-static {v6}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    const-string v6, "firstLaunchDate"

    .line 1796
    invoke-interface {v8, v6, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_18
    .catch Ljava/lang/Throwable; {:try_start_18 .. :try_end_18} :catch_d

    goto :goto_2e

    :catch_d
    move-exception v0

    goto :goto_2d

    :catch_e
    move-exception v0

    move-object/from16 v10, p7

    :goto_2d
    move-object v3, v0

    :try_start_19
    const-string v6, "Exception while collecting app version data "

    .line 1801
    invoke-static {v6, v3}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 1804
    :cond_52
    :goto_2e
    invoke-virtual/range {p5 .. p5}, Ljava/lang/String;->length()I

    move-result v3

    if-lez v3, :cond_53

    const-string v3, "referrer"

    move-object/from16 v6, p5

    .line 1805
    invoke-interface {v8, v3, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_53
    const-string v3, "extraReferrers"

    const/4 v6, 0x0

    .line 1813
    invoke-interface {v10, v3, v6}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_54

    const-string v6, "extraReferrers"

    .line 1815
    invoke-interface {v8, v6, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_54
    const-string v3, "afUninstallToken"

    .line 1819
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v6

    invoke-virtual {v6, v3}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_55

    .line 1821
    invoke-static {v3}, Lcom/appsflyer/d;->ˊ(Ljava/lang/String;)Lcom/appsflyer/d;

    move-result-object v3

    const-string v6, "af_gcm_token"

    .line 1823
    invoke-virtual {v3}, Lcom/appsflyer/d;->ˊ()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v8, v6, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1828
    :cond_55
    invoke-static/range {p1 .. p1}, Lcom/appsflyer/u;->ˎ(Landroid/content/Context;)Z

    move-result v3

    iput-boolean v3, v1, Lcom/appsflyer/AppsFlyerLib;->ˍ:Z

    .line 1829
    new-instance v3, Ljava/lang/StringBuilder;

    const-string v6, "didConfigureTokenRefreshService="

    invoke-direct {v3, v6}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    iget-boolean v6, v1, Lcom/appsflyer/AppsFlyerLib;->ˍ:Z

    invoke-virtual {v3, v6}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v3}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    .line 1830
    iget-boolean v3, v1, Lcom/appsflyer/AppsFlyerLib;->ˍ:Z

    if-nez v3, :cond_56

    const-string v3, "tokenRefreshConfigured"

    .line 1831
    sget-object v6, Ljava/lang/Boolean;->FALSE:Ljava/lang/Boolean;

    invoke-interface {v8, v3, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_56
    move/from16 v3, p8

    if-eqz v3, :cond_58

    .line 1836
    iget-object v6, v1, Lcom/appsflyer/AppsFlyerLib;->ˋˊ:Ljava/lang/String;

    if-eqz v6, :cond_57

    .line 1837
    new-instance v6, Lorg/json/JSONObject;

    iget-object v7, v1, Lcom/appsflyer/AppsFlyerLib;->ˋˊ:Ljava/lang/String;

    invoke-direct {v6, v7}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v7, "isPush"

    const-string v9, "true"

    .line 1838
    invoke-virtual {v6, v7, v9}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v7, "af_deeplink"

    .line 1839
    invoke-virtual {v6}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-interface {v8, v7, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_57
    const/4 v6, 0x0

    .line 1841
    iput-object v6, v1, Lcom/appsflyer/AppsFlyerLib;->ˋˊ:Ljava/lang/String;

    :cond_58
    if-eqz v3, :cond_5b

    if-eqz p9, :cond_59

    const-string v7, "android.intent.action.VIEW"

    .line 1846
    invoke-virtual/range {p9 .. p9}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object v9

    invoke-virtual {v7, v9}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v7

    if-eqz v7, :cond_59

    invoke-virtual/range {p9 .. p9}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object v9

    goto :goto_2f

    :cond_59
    const/4 v9, 0x0

    :goto_2f
    if-eqz v9, :cond_5a

    .line 1848
    invoke-direct {v1, v2, v8, v9}, Lcom/appsflyer/AppsFlyerLib;->ˏ(Landroid/content/Context;Ljava/util/Map;Landroid/net/Uri;)V

    goto :goto_30

    .line 1849
    :cond_5a
    iget-object v6, v1, Lcom/appsflyer/AppsFlyerLib;->ʿ:Landroid/net/Uri;

    if-eqz v6, :cond_5b

    .line 1851
    iget-object v6, v1, Lcom/appsflyer/AppsFlyerLib;->ʿ:Landroid/net/Uri;

    invoke-direct {v1, v2, v8, v6}, Lcom/appsflyer/AppsFlyerLib;->ˏ(Landroid/content/Context;Ljava/util/Map;Landroid/net/Uri;)V

    .line 1856
    :cond_5b
    :goto_30
    iget-boolean v6, v1, Lcom/appsflyer/AppsFlyerLib;->ˊᐝ:Z

    if-eqz v6, :cond_5c

    const-string v6, "testAppMode_retargeting"

    const-string v7, "true"

    .line 1857
    invoke-interface {v8, v6, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1858
    new-instance v6, Lorg/json/JSONObject;

    invoke-direct {v6, v8}, Lorg/json/JSONObject;-><init>(Ljava/util/Map;)V

    .line 1859
    invoke-virtual {v6}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v6

    new-instance v7, Landroid/content/Intent;

    const-string v9, "com.appsflyer.testIntgrationBroadcast"

    invoke-direct {v7, v9}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const-string v9, "params"

    invoke-virtual {v7, v9, v6}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    invoke-virtual {v2, v7}, Landroid/content/Context;->sendBroadcast(Landroid/content/Intent;)V

    const-string v6, "Sent retargeting params to test app"

    .line 1860
    invoke-static {v6}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    .line 1864
    :cond_5c
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v6

    iget-wide v11, v1, Lcom/appsflyer/AppsFlyerLib;->ˊˊ:J

    const/4 v9, 0x0

    sub-long/2addr v6, v11

    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v9

    invoke-virtual {v9, v2}, Lcom/appsflyer/AppsFlyerProperties;->getReferrer(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v9

    const-wide/16 v11, 0x7530

    cmp-long v13, v6, v11

    if-gtz v13, :cond_5d

    if-eqz v9, :cond_5d

    const-string v6, "AppsFlyer_Test"

    invoke-virtual {v9, v6}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v6

    if-eqz v6, :cond_5d

    const/4 v6, 0x1

    goto :goto_31

    :cond_5d
    const/4 v6, 0x0

    :goto_31
    if-eqz v6, :cond_5e

    const-string v6, "testAppMode"

    const-string v7, "true"

    .line 1865
    invoke-interface {v8, v6, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1866
    new-instance v6, Lorg/json/JSONObject;

    invoke-direct {v6, v8}, Lorg/json/JSONObject;-><init>(Ljava/util/Map;)V

    .line 1867
    invoke-virtual {v6}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v6

    new-instance v7, Landroid/content/Intent;

    const-string v9, "com.appsflyer.testIntgrationBroadcast"

    invoke-direct {v7, v9}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const-string v9, "params"

    invoke-virtual {v7, v9, v6}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    invoke-virtual {v2, v7}, Landroid/content/Context;->sendBroadcast(Landroid/content/Intent;)V

    const-string v6, "Sent params to test app"

    .line 1868
    invoke-static {v6}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    const-string v6, "Test mode ended!"

    .line 1869
    invoke-static {v6}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    const-wide/16 v6, 0x0

    iput-wide v6, v1, Lcom/appsflyer/AppsFlyerLib;->ˊˊ:J

    :cond_5e
    const-string v6, "advertiserId"

    .line 1872
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v7

    invoke-virtual {v7, v6}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    if-nez v6, :cond_60

    .line 1873
    invoke-static {v2, v8}, Lcom/appsflyer/o;->ˊ(Landroid/content/Context;Ljava/util/Map;)V

    const-string v6, "advertiserId"

    .line 1874
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v7

    invoke-virtual {v7, v6}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    if-eqz v6, :cond_5f

    const-string v6, "GAID_retry"

    const-string v7, "true"

    .line 1875
    invoke-interface {v8, v6, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_32

    :cond_5f
    const-string v6, "GAID_retry"

    const-string v7, "false"

    .line 1877
    invoke-interface {v8, v6, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1881
    :cond_60
    :goto_32
    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v6

    invoke-static {v6}, Lcom/appsflyer/o;->ˊ(Landroid/content/ContentResolver;)Lcom/appsflyer/n;

    move-result-object v6

    if-eqz v6, :cond_61

    const-string v7, "amazon_aid"

    .line 1883
    invoke-virtual {v6}, Lcom/appsflyer/n;->ˋ()Ljava/lang/String;

    move-result-object v9

    invoke-interface {v8, v7, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v7, "amazon_aid_limit"

    .line 1884
    invoke-virtual {v6}, Lcom/appsflyer/n;->ˊ()Z

    move-result v6

    invoke-static {v6}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v6

    invoke-interface {v8, v7, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1889
    :cond_61
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v6

    invoke-virtual {v6, v2}, Lcom/appsflyer/AppsFlyerProperties;->getReferrer(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v6

    if-eqz v6, :cond_62

    .line 1890
    invoke-virtual {v6}, Ljava/lang/String;->length()I

    move-result v7

    if-lez v7, :cond_62

    const-string v7, "referrer"

    invoke-interface {v8, v7}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v7

    if-nez v7, :cond_62

    const-string v7, "referrer"

    .line 1892
    invoke-interface {v8, v7, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_62
    const-string v6, "true"

    const-string v7, "sentSuccessfully"

    const-string v9, ""

    .line 1894
    invoke-interface {v10, v7, v9}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v6

    const-string v7, "sentRegisterRequestToAF"

    const/4 v9, 0x0

    .line 1896
    invoke-interface {v10, v7, v9}, Landroid/content/SharedPreferences;->getBoolean(Ljava/lang/String;Z)Z

    move-result v7

    const-string v9, "registeredUninstall"

    .line 1897
    invoke-static {v7}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v7

    invoke-interface {v8, v9, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v7, "appsFlyerCount"

    .line 1898
    invoke-static {v10, v7, v3}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/SharedPreferences;Ljava/lang/String;Z)I

    move-result v7

    const-string v9, "counter"

    .line 1899
    invoke-static {v7}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v11

    invoke-interface {v8, v9, v11}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v9, "iaecounter"

    if-eqz v4, :cond_63

    const/4 v4, 0x1

    goto :goto_33

    :cond_63
    const/4 v4, 0x0

    :goto_33
    const-string v11, "appsFlyerInAppEventCount"

    .line 1900
    invoke-static {v10, v11, v4}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/SharedPreferences;Ljava/lang/String;Z)I

    move-result v4

    invoke-static {v4}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v4

    invoke-interface {v8, v9, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    if-eqz v3, :cond_64

    const/4 v4, 0x1

    if-ne v7, v4, :cond_64

    .line 1903
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v4

    invoke-virtual {v4}, Lcom/appsflyer/AppsFlyerProperties;->setFirstLaunchCalled()V

    const-string v4, "waitForCustomerId"

    .line 1904
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v9

    const/4 v11, 0x0

    invoke-virtual {v9, v4, v11}, Lcom/appsflyer/AppsFlyerProperties;->getBoolean(Ljava/lang/String;Z)Z

    move-result v4

    if-eqz v4, :cond_64

    const-string v4, "wait_cid"

    const/4 v9, 0x1

    .line 1905
    invoke-static {v9}, Ljava/lang/Boolean;->toString(Z)Ljava/lang/String;

    move-result-object v11

    invoke-interface {v8, v4, v11}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_64
    const-string v4, "isFirstCall"

    const/4 v9, 0x1

    xor-int/2addr v6, v9

    .line 1908
    invoke-static {v6}, Ljava/lang/Boolean;->toString(Z)Ljava/lang/String;

    move-result-object v6

    invoke-interface {v8, v4, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1911
    new-instance v4, Ljava/util/HashMap;

    invoke-direct {v4}, Ljava/util/HashMap;-><init>()V

    const-string v6, "cpu_abi"

    const-string v9, "ro.product.cpu.abi"

    invoke-static {v9}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    invoke-interface {v4, v6, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v6, "cpu_abi2"

    const-string v9, "ro.product.cpu.abi2"

    invoke-static {v9}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    invoke-interface {v4, v6, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v6, "arch"

    const-string v9, "os.arch"

    invoke-static {v9}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    invoke-interface {v4, v6, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v6, "build_display_id"

    const-string v9, "ro.build.display.id"

    invoke-static {v9}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    invoke-interface {v4, v6, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    if-eqz v3, :cond_68

    iget-boolean v3, v1, Lcom/appsflyer/AppsFlyerLib;->ˉ:Z

    if-eqz v3, :cond_66

    sget-object v3, Lcom/appsflyer/j$d;->ॱ:Lcom/appsflyer/j;

    invoke-static/range {p1 .. p1}, Lcom/appsflyer/j;->ॱ(Landroid/content/Context;)Landroid/location/Location;

    move-result-object v3

    new-instance v6, Ljava/util/HashMap;

    invoke-direct {v6, v5}, Ljava/util/HashMap;-><init>(I)V

    if-eqz v3, :cond_65

    const-string v5, "lat"

    invoke-virtual {v3}, Landroid/location/Location;->getLatitude()D

    move-result-wide v11

    invoke-static {v11, v12}, Ljava/lang/String;->valueOf(D)Ljava/lang/String;

    move-result-object v9

    invoke-interface {v6, v5, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v5, "lon"

    invoke-virtual {v3}, Landroid/location/Location;->getLongitude()D

    move-result-wide v11

    invoke-static {v11, v12}, Ljava/lang/String;->valueOf(D)Ljava/lang/String;

    move-result-object v9

    invoke-interface {v6, v5, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v5, "ts"

    invoke-virtual {v3}, Landroid/location/Location;->getTime()J

    move-result-wide v11

    invoke-static {v11, v12}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v6, v5, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_65
    invoke-interface {v6}, Ljava/util/Map;->isEmpty()Z

    move-result v3

    if-nez v3, :cond_66

    const-string v3, "loc"

    invoke-interface {v4, v3, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_66
    sget-object v3, Lcom/appsflyer/c$c;->ˊ:Lcom/appsflyer/c;

    invoke-virtual {v3, v2}, Lcom/appsflyer/c;->ˊ(Landroid/content/Context;)Lcom/appsflyer/c$e;

    move-result-object v3

    const-string v5, "btl"

    invoke-virtual {v3}, Lcom/appsflyer/c$e;->ˎ()F

    move-result v6

    invoke-static {v6}, Ljava/lang/Float;->toString(F)Ljava/lang/String;

    move-result-object v6

    invoke-interface {v4, v5, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    invoke-virtual {v3}, Lcom/appsflyer/c$e;->ॱ()Ljava/lang/String;

    move-result-object v5

    if-eqz v5, :cond_67

    const-string v5, "btch"

    invoke-virtual {v3}, Lcom/appsflyer/c$e;->ॱ()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v4, v5, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_67
    const/4 v3, 0x2

    if-lt v3, v7, :cond_68

    invoke-static/range {p1 .. p1}, Lcom/appsflyer/f;->ˏ(Landroid/content/Context;)Lcom/appsflyer/f;

    move-result-object v3

    invoke-virtual {v3}, Lcom/appsflyer/f;->ˋ()Ljava/util/List;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/List;->isEmpty()Z

    move-result v5

    if-nez v5, :cond_68

    const-string v5, "sensors"

    invoke-interface {v4, v5, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_68
    invoke-static/range {p1 .. p1}, Lcom/appsflyer/AFScreenManager;->getScreenMetrics(Landroid/content/Context;)Ljava/util/Map;

    move-result-object v2

    const-string v3, "dim"

    invoke-interface {v4, v3, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "deviceData"

    invoke-interface {v8, v2, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1914
    new-instance v2, Lcom/appsflyer/r;

    invoke-direct {v2}, Lcom/appsflyer/r;-><init>()V

    const-string v2, "appsflyerKey"

    invoke-interface {v8, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    const-string v3, "af_timestamp"

    invoke-interface {v8, v3}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    const-string v4, "uid"

    invoke-interface {v8, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const/4 v6, 0x7

    const/4 v7, 0x0

    invoke-virtual {v2, v7, v6}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v5, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v7, v6}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v5, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v2

    sub-int/2addr v2, v6

    invoke-virtual {v3, v2}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v5, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/appsflyer/r;->ॱ(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v3, "af_v"

    .line 1915
    invoke-interface {v8, v3, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1917
    new-instance v2, Lcom/appsflyer/r;

    invoke-direct {v2}, Lcom/appsflyer/r;-><init>()V

    const-string v2, "appsflyerKey"

    invoke-interface {v8, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "af_timestamp"

    invoke-interface {v8, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "uid"

    invoke-interface {v8, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "installDate"

    invoke-interface {v8, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "counter"

    invoke-interface {v8, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "iaecounter"

    invoke-interface {v8, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/appsflyer/r;->ˏ(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/appsflyer/r;->ॱ(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v3, "af_v2"

    .line 1918
    invoke-interface {v8, v3, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v2, "is_stop_tracking_used"

    .line 1922
    invoke-interface {v10, v2}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_6a

    const-string v2, "istu"

    const-string v3, "is_stop_tracking_used"

    const/4 v4, 0x0

    .line 1923
    invoke-interface {v10, v3, v4}, Landroid/content/SharedPreferences;->getBoolean(Ljava/lang/String;Z)Z

    move-result v3

    invoke-static {v3}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v8, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_34

    :cond_69
    const-string v3, "AppsFlyer dev key is missing!!! Please use  AppsFlyerLib.getInstance().setAppsFlyerKey(...) to set it. "

    .line 1660
    invoke-static {v3}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    const-string v3, "AppsFlyer_4.8.11"

    const-string v4, "DEV_KEY_MISSING"

    const/4 v5, 0x0

    .line 1661
    invoke-static {v2, v3, v4, v5}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "AppsFlyer will not track this event."

    .line 1662
    invoke-static {v2}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V
    :try_end_19
    .catch Ljava/lang/Throwable; {:try_start_19 .. :try_end_19} :catch_f

    return-object v5

    :catch_f
    move-exception v0

    move-object v2, v0

    .line 1926
    invoke-virtual {v2}, Ljava/lang/Throwable;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v3

    invoke-static {v3, v2}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_6a
    :goto_34
    return-object v8
.end method

.method final ˎ()V
    .locals 2

    .line 185
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    iput-wide v0, p0, Lcom/appsflyer/AppsFlyerLib;->ʾ:J

    return-void
.end method

.method final ˎ(Ljava/lang/ref/WeakReference;)V
    .locals 10
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/ref/WeakReference<",
            "Landroid/content/Context;",
            ">;)V"
        }
    .end annotation

    .line 972
    invoke-virtual {p1}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    :cond_0
    const-string v0, "app went to background"

    .line 976
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afInfoLog(Ljava/lang/String;)V

    .line 977
    invoke-virtual {p1}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/content/Context;

    const-string v1, "appsflyer-data"

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 978
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0}, Lcom/appsflyer/AppsFlyerProperties;->saveProperties(Landroid/content/SharedPreferences;)V

    .line 981
    iget-wide v3, p0, Lcom/appsflyer/AppsFlyerLib;->ʾ:J

    iget-wide v5, p0, Lcom/appsflyer/AppsFlyerLib;->ʽॱ:J

    sub-long/2addr v3, v5

    .line 983
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    const-string v5, "AppsFlyerKey"

    .line 984
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v6

    invoke-virtual {v6, v5}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    if-nez v5, :cond_1

    const-string p1, "[callStats] AppsFlyer\'s SDK cannot send any event without providing DevKey."

    .line 986
    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    return-void

    :cond_1
    const-string v6, "KSAppsFlyerId"

    .line 989
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v7

    invoke-virtual {v7, v6}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    .line 991
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v7

    const-string v8, "deviceTrackingDisabled"

    invoke-virtual {v7, v8, v2}, Lcom/appsflyer/AppsFlyerProperties;->getBoolean(Ljava/lang/String;Z)Z

    move-result v7

    if-eqz v7, :cond_2

    const-string v7, "deviceTrackingDisabled"

    const-string v8, "true"

    .line 993
    invoke-interface {v1, v7, v8}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 995
    :cond_2
    invoke-virtual {p1}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Landroid/content/Context;

    invoke-virtual {v7}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v7

    invoke-static {v7}, Lcom/appsflyer/o;->ˊ(Landroid/content/ContentResolver;)Lcom/appsflyer/n;

    move-result-object v7

    if-eqz v7, :cond_3

    const-string v8, "amazon_aid"

    .line 997
    invoke-virtual {v7}, Lcom/appsflyer/n;->ˋ()Ljava/lang/String;

    move-result-object v9

    invoke-interface {v1, v8, v9}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v8, "amazon_aid_limit"

    .line 998
    invoke-virtual {v7}, Lcom/appsflyer/n;->ˊ()Z

    move-result v7

    invoke-static {v7}, Ljava/lang/String;->valueOf(Z)Ljava/lang/String;

    move-result-object v7

    invoke-interface {v1, v8, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1000
    :cond_3
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v7

    const-string v8, "advertiserId"

    invoke-virtual {v7, v8}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    if-eqz v7, :cond_4

    const-string v8, "advertiserId"

    .line 1002
    invoke-interface {v1, v8, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_4
    const-string v7, "app_id"

    .line 1004
    invoke-virtual {p1}, Ljava/lang/ref/Reference;->get()Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Landroid/content/Context;

    invoke-virtual {v8}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v8

    invoke-interface {v1, v7, v8}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v7, "devkey"

    .line 1005
    invoke-interface {v1, v7, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v5, "uid"

    .line 1006
    invoke-static {p1}, Lcom/appsflyer/p;->ˋ(Ljava/lang/ref/WeakReference;)Ljava/lang/String;

    move-result-object v7

    invoke-interface {v1, v5, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v5, "time_in_app"

    const-wide/16 v7, 0x3e8

    .line 1007
    div-long/2addr v3, v7

    invoke-static {v3, v4}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object v3

    invoke-interface {v1, v5, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v3, "statType"

    const-string v4, "user_closed_app"

    .line 1008
    invoke-interface {v1, v3, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v3, "platform"

    const-string v4, "Android"

    .line 1009
    invoke-interface {v1, v3, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v3, "launch_counter"

    const-string v4, "appsFlyerCount"

    .line 1010
    invoke-static {v0, v4, v2}, Lcom/appsflyer/AppsFlyerLib;->ˊ(Landroid/content/SharedPreferences;Ljava/lang/String;Z)I

    move-result v4

    invoke-static {v4}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v4

    invoke-interface {v1, v3, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v3, "gcd_conversion_data_timing"

    const-string v4, "appsflyerGetConversionDataTiming"

    const-wide/16 v7, 0x0

    .line 1011
    invoke-interface {v0, v4, v7, v8}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v4

    invoke-static {v4, v5}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v0

    invoke-interface {v1, v3, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v0, "channel"

    .line 1012
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v3

    const-string v4, "channel"

    invoke-virtual {v3, v4}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-nez v3, :cond_5

    const-string v3, "CHANNEL"

    invoke-static {p1, v3}, Lcom/appsflyer/AppsFlyerLib;->ˋ(Ljava/lang/ref/WeakReference;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    :cond_5
    invoke-interface {v1, v0, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p1, "originalAppsflyerId"

    if-eqz v6, :cond_6

    goto :goto_0

    :cond_6
    const-string v6, ""

    .line 1013
    :goto_0
    invoke-interface {v1, p1, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1015
    iget-boolean p1, p0, Lcom/appsflyer/AppsFlyerLib;->ˋᐝ:Z

    if-eqz p1, :cond_8

    .line 1017
    :try_start_0
    new-instance p1, Lcom/appsflyer/l;

    const/4 v0, 0x0

    invoke-virtual {p0}, Lcom/appsflyer/AppsFlyerLib;->isTrackingStopped()Z

    move-result v3

    invoke-direct {p1, v0, v3}, Lcom/appsflyer/l;-><init>(Landroid/content/Context;Z)V

    .line 1018
    iput-object v1, p1, Lcom/appsflyer/l;->ˎ:Ljava/util/Map;

    .line 1019
    invoke-static {}, Ljava/lang/Thread;->currentThread()Ljava/lang/Thread;

    move-result-object v0

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-virtual {v1}, Landroid/os/Looper;->getThread()Ljava/lang/Thread;

    move-result-object v1

    const/4 v3, 0x1

    if-ne v0, v1, :cond_7

    const-string v0, "Main thread detected. Running callStats task in a new thread."

    .line 1020
    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    .line 1021
    new-array v0, v3, [Ljava/lang/String;

    const-string v1, "https://stats.%s/stats"

    invoke-static {v1}, Lcom/appsflyer/ServerConfigHandler;->getUrl(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    aput-object v1, v0, v2

    invoke-virtual {p1, v0}, Landroid/os/AsyncTask;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    return-void

    .line 1023
    :cond_7
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "Running callStats task (on current thread: "

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-static {}, Ljava/lang/Thread;->currentThread()Ljava/lang/Thread;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " )"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    .line 1024
    invoke-virtual {p1}, Lcom/appsflyer/l;->onPreExecute()V

    .line 1025
    new-array v0, v3, [Ljava/lang/String;

    const-string v1, "https://stats.%s/stats"

    invoke-static {v1}, Lcom/appsflyer/ServerConfigHandler;->getUrl(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    aput-object v1, v0, v2

    invoke-virtual {p1, v0}, Lcom/appsflyer/l;->ˋ([Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Lcom/appsflyer/l;->ॱ(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p1

    const-string v0, "Could not send callStats request"

    .line 1028
    invoke-static {v0, p1}, Lcom/appsflyer/AFLogger;->afErrorLog(Ljava/lang/String;Ljava/lang/Throwable;)V

    return-void

    :cond_8
    const-string p1, "Stats call is disabled, ignore ..."

    .line 1032
    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afDebugLog(Ljava/lang/String;)V

    return-void
.end method

.method final ˏ(Landroid/content/Context;Ljava/lang/String;Ljava/util/Map;)V
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 1087
    instance-of v0, p1, Landroid/app/Activity;

    if-eqz v0, :cond_0

    move-object v0, p1

    check-cast v0, Landroid/app/Activity;

    invoke-virtual {v0}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v0

    :goto_0
    move-object v7, v0

    goto :goto_1

    :cond_0
    const/4 v0, 0x0

    goto :goto_0

    :goto_1
    const-string v0, "AppsFlyerKey"

    .line 1088
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object v1

    invoke-virtual {v1, v0}, Lcom/appsflyer/AppsFlyerProperties;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_1

    const-string p1, "[TrackEvent/Launch] AppsFlyer\'s SDK cannot send any event without providing DevKey."

    .line 1090
    invoke-static {p1}, Lcom/appsflyer/AFLogger;->afWarnLog(Ljava/lang/String;)V

    return-void

    .line 1093
    :cond_1
    new-instance v0, Lorg/json/JSONObject;

    if-nez p3, :cond_2

    new-instance p3, Ljava/util/HashMap;

    invoke-direct {p3}, Ljava/util/HashMap;-><init>()V

    :cond_2
    invoke-direct {v0, p3}, Lorg/json/JSONObject;-><init>(Ljava/util/Map;)V

    .line 1094
    invoke-static {}, Lcom/appsflyer/AppsFlyerProperties;->getInstance()Lcom/appsflyer/AppsFlyerProperties;

    move-result-object p3

    invoke-virtual {p3, p1}, Lcom/appsflyer/AppsFlyerProperties;->getReferrer(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p3

    const/4 v3, 0x0

    .line 1095
    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v5

    if-nez p3, :cond_3

    const-string p3, ""

    :cond_3
    move-object v6, p3

    move-object v1, p0

    move-object v2, p1

    move-object v4, p2

    invoke-direct/range {v1 .. v7}, Lcom/appsflyer/AppsFlyerLib;->ˏ(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Landroid/content/Intent;)V

    return-void
.end method

.method final ॱ()V
    .locals 2

    .line 181
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    iput-wide v0, p0, Lcom/appsflyer/AppsFlyerLib;->ʽॱ:J

    return-void
.end method
