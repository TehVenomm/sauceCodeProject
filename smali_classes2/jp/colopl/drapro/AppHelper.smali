.class public Ljp/colopl/drapro/AppHelper;
.super Ljava/lang/Object;
.source "AppHelper.java"


# static fields
.field public static activity:Ljp/colopl/drapro/StartActivity; = null

.field private static config:Ljp/colopl/drapro/Config; = null

.field public static isQuitDialogOpened:Z = false

.field private static isShowingDialog:Z = false

.field private static shopMode:Z = false


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 32
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static BootAffiliateBrowser(Z)Z
    .locals 5

    .line 250
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Landroid/preference/PreferenceManager;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v1, "ALREADY_REFERRER_SENT"

    const/4 v2, 0x0

    .line 251
    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getBoolean(Ljava/lang/String;Z)Z

    move-result v1

    if-eqz v1, :cond_0

    return v2

    .line 258
    :cond_0
    invoke-static {}, Ljp/colopl/drapro/AppHelper;->GetInstallReferrerAtInstall()Ljava/lang/String;

    move-result-object v1

    const-string v3, ""

    :try_start_0
    const-string v4, "utf-8"

    .line 262
    invoke-static {v1, v4}, Ljava/net/URLEncoder;->encode(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-object v1, v3

    .line 267
    :goto_0
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "http://s.colo.pl/ad/"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    if-eqz p0, :cond_1

    const-string p0, "cnt2_drapro.php"

    goto :goto_1

    :cond_1
    const-string p0, "cnt_drapro.php"

    :goto_1
    invoke-virtual {v3, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "?referrer="

    invoke-virtual {v3, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    const/4 v1, 0x1

    if-eqz p0, :cond_2

    .line 270
    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    const-string v2, "ALREADY_REFERRER_SENT"

    invoke-interface {v0, v2, v1}, Landroid/content/SharedPreferences$Editor;->putBoolean(Ljava/lang/String;Z)Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 273
    new-instance v0, Landroid/content/Intent;

    const-string v2, "android.intent.action.VIEW"

    invoke-static {p0}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p0

    invoke-direct {v0, v2, p0}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    const/high16 p0, 0x10000000

    .line 275
    invoke-virtual {v0, p0}, Landroid/content/Intent;->addFlags(I)Landroid/content/Intent;

    .line 276
    sget-object p0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    const v2, 0x87e85

    invoke-virtual {p0, v0, v2}, Ljp/colopl/drapro/StartActivity;->startActivityForResult(Landroid/content/Intent;I)V

    goto :goto_2

    :cond_2
    const/4 v1, 0x0

    :goto_2
    return v1
.end method

.method public static CheckReferrerSendToAppBrowser()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public static GetDeviceAutoRotateSetting()I
    .locals 2

    .line 64
    :try_start_0
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    .line 65
    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v0

    const-string v1, "accelerometer_rotation"

    invoke-static {v0, v1}, Landroid/provider/Settings$System;->getInt(Landroid/content/ContentResolver;Ljava/lang/String;)I

    move-result v0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public static GetInstallReferrerAtInstall()Ljava/lang/String;
    .locals 1

    .line 238
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    check-cast v0, Ljp/colopl/drapro/ColoplApplication;

    invoke-virtual {v0}, Ljp/colopl/drapro/ColoplApplication;->getConfig()Ljp/colopl/config/Config;

    move-result-object v0

    .line 239
    invoke-virtual {v0}, Ljp/colopl/config/Config;->getReferrerAtInstalled()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public static GoGooglePlayMyself()V
    .locals 4

    .line 149
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.intent.action.VIEW"

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "market://details?id="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v3, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    .line 150
    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v2

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    .line 151
    sget-object v1, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v1, v0}, Ljp/colopl/drapro/StartActivity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method public static OpenURL(Ljava/lang/String;)V
    .locals 2

    .line 286
    :try_start_0
    invoke-static {p0}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p0

    .line 287
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.intent.action.VIEW"

    invoke-direct {v0, v1, p0}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    .line 288
    sget-object p0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {p0, v0}, Ljp/colopl/drapro/StartActivity;->startActivity(Landroid/content/Intent;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    return-void
.end method

.method public static ProcessKillCommit()V
    .locals 1

    .line 144
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->finish()V

    .line 145
    invoke-static {}, Landroid/os/Process;->myPid()I

    move-result v0

    invoke-static {v0}, Landroid/os/Process;->killProcess(I)V

    return-void
.end method

.method public static ShowInvitationCodeView(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    .line 157
    sget-boolean v0, Ljp/colopl/drapro/AppHelper;->isShowingDialog:Z

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x1

    .line 158
    sput-boolean v0, Ljp/colopl/drapro/AppHelper;->isShowingDialog:Z

    .line 159
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    new-instance v1, Ljp/colopl/drapro/AppHelper$2;

    invoke-direct {v1, p2, p0, p1}, Ljp/colopl/drapro/AppHelper$2;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Ljp/colopl/drapro/StartActivity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method static synthetic access$002(Z)Z
    .locals 0

    .line 32
    sput-boolean p0, Ljp/colopl/drapro/AppHelper;->isShowingDialog:Z

    return p0
.end method

.method public static checkInstallPackage(Ljava/lang/String;)I
    .locals 4

    .line 296
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    const/16 v1, 0x80

    .line 297
    invoke-virtual {v0, v1}, Landroid/content/pm/PackageManager;->getInstalledApplications(I)Ljava/util/List;

    move-result-object v0

    .line 298
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    const/4 v2, 0x1

    if-eqz v1, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/content/pm/ApplicationInfo;

    .line 299
    iget v3, v1, Landroid/content/pm/ApplicationInfo;->flags:I

    and-int/2addr v3, v2

    if-ne v3, v2, :cond_1

    goto :goto_0

    .line 302
    :cond_1
    iget-object v1, v1, Landroid/content/pm/ApplicationInfo;->packageName:Ljava/lang/String;

    invoke-virtual {p0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    goto :goto_1

    :cond_2
    const/4 v2, 0x0

    :goto_1
    return v2
.end method

.method public static getLocale()Ljava/lang/String;
    .locals 2

    .line 232
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/res/Resources;->getConfiguration()Landroid/content/res/Configuration;

    move-result-object v0

    iget-object v0, v0, Landroid/content/res/Configuration;->locale:Ljava/util/Locale;

    invoke-virtual {v0}, Ljava/util/Locale;->getCountry()Ljava/lang/String;

    move-result-object v0

    const-string v1, "CLIENT LOCALE"

    .line 233
    invoke-static {v1, v0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    return-object v0
.end method

.method public static getPurchaseType()I
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public static getScreenLockMode()Ljava/lang/String;
    .locals 1

    .line 56
    sget-object v0, Ljp/colopl/drapro/AppHelper;->config:Ljp/colopl/drapro/Config;

    invoke-virtual {v0}, Ljp/colopl/drapro/Config;->getScreenLockMode()Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "true"

    goto :goto_0

    :cond_0
    const-string v0, "false"

    :goto_0
    return-object v0
.end method

.method public static getShopMode()Z
    .locals 1

    .line 94
    sget-boolean v0, Ljp/colopl/drapro/AppHelper;->shopMode:Z

    return v0
.end method

.method public static init(Landroid/app/Activity;)V
    .locals 0

    .line 39
    check-cast p0, Ljp/colopl/drapro/StartActivity;

    sput-object p0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    return-void
.end method

.method public static isAdsRemoved()I
    .locals 1

    .line 86
    sget-object v0, Ljp/colopl/drapro/AppHelper;->config:Ljp/colopl/drapro/Config;

    invoke-virtual {v0}, Ljp/colopl/drapro/Config;->getEnableAdView()Z

    move-result v0

    xor-int/lit8 v0, v0, 0x1

    return v0
.end method

.method public static quit()V
    .locals 2

    .line 100
    sget-boolean v0, Ljp/colopl/drapro/AppHelper;->isQuitDialogOpened:Z

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x1

    .line 101
    sput-boolean v0, Ljp/colopl/drapro/AppHelper;->isQuitDialogOpened:Z

    .line 102
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    new-instance v1, Ljp/colopl/drapro/AppHelper$1;

    invoke-direct {v1}, Ljp/colopl/drapro/AppHelper$1;-><init>()V

    invoke-virtual {v0, v1}, Ljp/colopl/drapro/StartActivity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static resetPackagePreferences()V
    .locals 3

    .line 313
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    .line 314
    invoke-virtual {v0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 315
    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 316
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->clear()Landroid/content/SharedPreferences$Editor;

    .line 317
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    return-void
.end method

.method public static setConfig(Ljp/colopl/drapro/Config;)V
    .locals 0

    .line 44
    sput-object p0, Ljp/colopl/drapro/AppHelper;->config:Ljp/colopl/drapro/Config;

    return-void
.end method

.method public static setScreenLockMode(Ljava/lang/String;)V
    .locals 1

    const-string v0, "true"

    .line 74
    invoke-virtual {p0, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p0

    .line 75
    sget-object v0, Ljp/colopl/drapro/AppHelper;->config:Ljp/colopl/drapro/Config;

    invoke-virtual {v0, p0}, Ljp/colopl/drapro/Config;->setScreenLockMode(Z)V

    .line 76
    sget-object v0, Ljp/colopl/drapro/AppHelper;->activity:Ljp/colopl/drapro/StartActivity;

    if-eqz p0, :cond_0

    .line 78
    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->ReleaseWakeLock()Z

    goto :goto_0

    .line 81
    :cond_0
    invoke-virtual {v0}, Ljp/colopl/drapro/StartActivity;->AcquireWakeLock()V

    :goto_0
    return-void
.end method

.method public static setShopMode(Z)V
    .locals 0

    .line 90
    sput-boolean p0, Ljp/colopl/drapro/AppHelper;->shopMode:Z

    return-void
.end method

.method public static showList(Ljava/lang/String;)I
    .locals 3

    .line 336
    new-instance v0, Landroid/content/Intent;

    sget-object v1, Ljp/colopl/drapro/AppConsts;->appContext:Landroid/content/Context;

    const-class v2, Ljp/colopl/libs/AssetService;

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "asset"

    .line 337
    invoke-virtual {v0, v1, p0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    .line 338
    sget-object p0, Ljp/colopl/drapro/AppConsts;->appContext:Landroid/content/Context;

    invoke-virtual {p0, v0}, Landroid/content/Context;->startService(Landroid/content/Intent;)Landroid/content/ComponentName;

    const/4 p0, 0x0

    return p0
.end method

.method public static testCrash()V
    .locals 2

    .line 344
    new-instance v0, Ljava/lang/Thread;

    new-instance v1, Ljp/colopl/drapro/AppHelper$3;

    invoke-direct {v1}, Ljp/colopl/drapro/AppHelper$3;-><init>()V

    invoke-direct {v0, v1}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;)V

    .line 350
    invoke-virtual {v0}, Ljava/lang/Thread;->start()V

    return-void
.end method

.method public static trackUserRegEventAppsFlyer(Ljava/lang/String;)V
    .locals 0

    return-void
.end method
