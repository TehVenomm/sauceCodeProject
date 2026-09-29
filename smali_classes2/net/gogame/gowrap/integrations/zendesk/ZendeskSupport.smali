.class public Lnet/gogame/gowrap/integrations/zendesk/ZendeskSupport;
.super Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;
.source "ZendeskSupport.java"


# static fields
.field public static final INSTANCE:Lnet/gogame/gowrap/integrations/zendesk/ZendeskSupport;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 22
    new-instance v0, Lnet/gogame/gowrap/integrations/zendesk/ZendeskSupport;

    invoke-direct {v0}, Lnet/gogame/gowrap/integrations/zendesk/ZendeskSupport;-><init>()V

    sput-object v0, Lnet/gogame/gowrap/integrations/zendesk/ZendeskSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/zendesk/ZendeskSupport;

    return-void
.end method

.method private constructor <init>()V
    .locals 1

    const-string v0, "zendesk"

    .line 25
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/integrations/AbstractIntegrationSupport;-><init>(Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method protected doInit(Landroid/app/Activity;Lnet/gogame/gowrap/integrations/Config;Lnet/gogame/gowrap/integrations/IntegrationSupport$IntegrationContext;)V
    .locals 5

    .line 35
    sget-object p2, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {p2}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object p2

    if-nez p2, :cond_0

    const-string p2, "goWrap"

    const-string p3, "App ID not set"

    .line 36
    invoke-static {p2, p3}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 39
    :cond_0
    new-instance p2, Ljava/io/File;

    invoke-virtual {p1}, Landroid/app/Activity;->getFilesDir()Ljava/io/File;

    move-result-object p3

    const-string v0, "net/gogame/gowrap/"

    invoke-direct {p2, p3, v0}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 41
    invoke-virtual {p2}, Ljava/io/File;->mkdirs()Z

    .line 44
    new-instance p3, Ljava/io/File;

    const-string v0, "faq.json.gz"

    invoke-direct {p3, p2, v0}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 45
    sget-object p2, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {p2}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object p2

    if-eqz p2, :cond_2

    invoke-virtual {p3}, Ljava/io/File;->exists()Z

    move-result p2

    if-nez p2, :cond_1

    goto :goto_0

    :cond_1
    const-string p2, "goWrap"

    const-string v0, "faq.json.gz already exists in internal storage"

    .line 57
    invoke-static {p2, v0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_1

    :cond_2
    :goto_0
    :try_start_0
    const-string p2, "net/gogame/gowrap/faq.json"

    .line 49
    invoke-static {p1, p2, p3}, Lnet/gogame/gowrap/io/utils/FileUtils;->gzipCopyFromAsset(Landroid/content/Context;Ljava/lang/String;Ljava/io/File;)V

    const-string p2, "goWrap"

    const-string v0, "Initialized faq.json.gz"

    .line 50
    invoke-static {p2, v0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_0
    .catch Ljava/io/FileNotFoundException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception p2

    const-string v0, "goWrap"

    const-string v1, "Could not copy pre-packaged FAQ file"

    .line 54
    invoke-static {v0, v1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto :goto_1

    :catch_1
    const-string p2, "goWrap"

    const-string v0, "Could not copy pre-packaged FAQ file (not found): net/gogame/gowrap/faq.json"

    .line 52
    invoke-static {p2, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 61
    :goto_1
    sget-object p2, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {p2}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object p2

    if-eqz p2, :cond_3

    .line 63
    :try_start_1
    new-instance p2, Ljava/net/URL;

    const-string v0, "%s/zendesk/%s/%s"

    const/4 v1, 0x3

    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    const-string v3, "http://gw-content.gogame.net"

    aput-object v3, v1, v2

    sget-object v2, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    .line 65
    invoke-virtual {v2}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object v2

    const/4 v3, 0x1

    aput-object v2, v1, v3

    const/4 v2, 0x2

    const-string v4, "faq.json.gz"

    aput-object v4, v1, v2

    .line 63
    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    invoke-direct {p2, v0}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    .line 66
    new-instance v0, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;

    invoke-direct {v0, p3}, Lnet/gogame/gowrap/support/DownloadUtils$FileTarget;-><init>(Ljava/io/File;)V

    const/4 p3, 0x0

    invoke-static {p1, p2, v0, v3, p3}, Lnet/gogame/gowrap/support/DownloadUtils;->download(Landroid/content/Context;Ljava/net/URL;Lnet/gogame/gowrap/support/DownloadUtils$Target;ZLnet/gogame/gowrap/support/DownloadUtils$Callback;)V
    :try_end_1
    .catch Ljava/net/MalformedURLException; {:try_start_1 .. :try_end_1} :catch_2

    :catch_2
    :cond_3
    return-void
.end method

.method public isIntegrated()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method
