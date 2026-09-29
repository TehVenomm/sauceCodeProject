.class public Lnet/gogame/gowrap/ui/utils/ExternalAppLauncher;
.super Ljava/lang/Object;
.source "ExternalAppLauncher.java"


# static fields
.field private static final CHROME_PACKAGE_NAME:Ljava/lang/String; = "com.android.chrome"

.field private static final FACEBOOK_PACKAGE_NAME:Ljava/lang/String; = "com.facebook.katana"

.field private static final HANDLE_FACEBOOK_URLS:Z = true

.field private static final SHARE_BUTTON_ENABLED:Z = false

.field private static final USE_CUSTOM_TABS:Z = true


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 17
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private static doLaunchActivity(Landroid/app/Activity;Landroid/content/Intent;)Z
    .locals 0

    .line 93
    :try_start_0
    invoke-virtual {p0, p1}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V
    :try_end_0
    .catch Landroid/content/ActivityNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    const/4 p0, 0x1

    return p0

    :catch_0
    const/4 p0, 0x0

    return p0
.end method

.method private static getApplicationInfo(Landroid/app/Activity;Ljava/lang/String;)Landroid/content/pm/ApplicationInfo;
    .locals 1

    .line 85
    :try_start_0
    invoke-virtual {p0}, Landroid/app/Activity;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object p0

    const/4 v0, 0x0

    invoke-virtual {p0, p1, v0}, Landroid/content/pm/PackageManager;->getApplicationInfo(Ljava/lang/String;I)Landroid/content/pm/ApplicationInfo;

    move-result-object p0
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    const/4 p0, 0x0

    return-object p0
.end method

.method public static openUrlInExternalBrowser(Landroid/app/Activity;Ljava/lang/String;)Z
    .locals 4

    .line 26
    invoke-static {p1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p1

    .line 29
    invoke-virtual {p1}, Landroid/net/Uri;->getHost()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    invoke-virtual {p1}, Landroid/net/Uri;->getHost()Ljava/lang/String;

    move-result-object v0

    const-string v2, ".facebook.com"

    invoke-virtual {v0, v2}, Ljava/lang/String;->endsWith(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 30
    invoke-virtual {p1}, Landroid/net/Uri;->getPath()Ljava/lang/String;

    move-result-object v0

    const-string v2, "/groups/"

    invoke-virtual {v0, v2}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 42
    new-instance v0, Landroid/content/Intent;

    const-string v2, "android.intent.action.VIEW"

    invoke-direct {v0, v2, p1}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    const-string v2, "com.facebook.katana"

    .line 43
    invoke-virtual {v0, v2}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    .line 44
    invoke-static {p0, v0}, Lnet/gogame/gowrap/ui/utils/ExternalAppLauncher;->doLaunchActivity(Landroid/app/Activity;Landroid/content/Intent;)Z

    move-result v0

    if-eqz v0, :cond_0

    return v1

    .line 51
    :cond_0
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v2, 0x12

    if-lt v0, v2, :cond_1

    .line 52
    invoke-static {p0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsChecker;->isChromeCustomTabsSupported(Landroid/content/Context;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 53
    new-instance v0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;

    invoke-direct {v0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;-><init>()V

    .line 66
    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->build()Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent;

    move-result-object v0

    .line 67
    iget-object v2, v0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent;->intent:Landroid/content/Intent;

    const-string v3, "com.android.chrome"

    invoke-virtual {v2, v3}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    .line 68
    invoke-virtual {v0, p0, p1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent;->launchUrl(Landroid/content/Context;Landroid/net/Uri;)V

    return v1

    .line 73
    :cond_1
    new-instance v0, Landroid/content/Intent;

    const-string v2, "android.intent.action.VIEW"

    invoke-direct {v0, v2, p1}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    const-string v2, "com.android.chrome"

    .line 74
    invoke-virtual {v0, v2}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    .line 75
    invoke-static {p0, v0}, Lnet/gogame/gowrap/ui/utils/ExternalAppLauncher;->doLaunchActivity(Landroid/app/Activity;Landroid/content/Intent;)Z

    move-result v0

    if-eqz v0, :cond_2

    return v1

    .line 80
    :cond_2
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.intent.action.VIEW"

    invoke-direct {v0, v1, p1}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    invoke-static {p0, v0}, Lnet/gogame/gowrap/ui/utils/ExternalAppLauncher;->doLaunchActivity(Landroid/app/Activity;Landroid/content/Intent;)Z

    move-result p0

    return p0
.end method
