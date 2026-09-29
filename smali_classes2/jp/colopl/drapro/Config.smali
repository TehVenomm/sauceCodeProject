.class public Ljp/colopl/drapro/Config;
.super Ljava/lang/Object;
.source "Config.java"


# static fields
.field public static final APP_PURCHASE_TYPE:I = 0x0

.field public static final APP_PURCHASE_TYPE_AMAZON:I = 0x1

.field public static final APP_PURCHASE_TYPE_AU:I = 0x2

.field public static final APP_PURCHASE_TYPE_GOOGLE:I = 0x0

.field private static final LAST_BOOT_TIME_LOCAL:Ljava/lang/String; = "lastlBootTimeLocal"

.field private static final LAST_BOOT_TIME_SERVER:Ljava/lang/String; = "lastBootTimeServer"

.field private static final SETTING_KEY_ENABLE_AD_VIEW:Ljava/lang/String; = "enableAdView"

.field private static final SETTING_KEY_LAST_VERSION_CODE:Ljava/lang/String; = "lastVersionCode"

.field private static final SETTING_KEY_SCREEN_LOCK_MODE:Ljava/lang/String; = "screenLockMode"


# instance fields
.field private context:Landroid/content/Context;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 25
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 26
    iput-object p1, p0, Ljp/colopl/drapro/Config;->context:Landroid/content/Context;

    return-void
.end method


# virtual methods
.method public getEnableAdView()Z
    .locals 3

    .line 30
    iget-object v0, p0, Ljp/colopl/drapro/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Landroid/preference/PreferenceManager;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v1, "enableAdView"

    const/4 v2, 0x1

    .line 31
    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getBoolean(Ljava/lang/String;Z)Z

    move-result v0

    return v0
.end method

.method public getLastBootTime(Z)Ljava/lang/String;
    .locals 2

    .line 70
    iget-object v0, p0, Ljp/colopl/drapro/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Landroid/preference/PreferenceManager;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    if-eqz p1, :cond_0

    const-string p1, "lastlBootTimeLocal"

    goto :goto_0

    :cond_0
    const-string p1, "lastBootTimeServer"

    :goto_0
    const-string v1, ""

    .line 71
    invoke-interface {v0, p1, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public getLastVersionCode()I
    .locals 3

    .line 50
    iget-object v0, p0, Ljp/colopl/drapro/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Landroid/preference/PreferenceManager;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v1, "lastVersionCode"

    const/4 v2, 0x0

    .line 51
    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getInt(Ljava/lang/String;I)I

    move-result v0

    return v0
.end method

.method public getScreenLockMode()Z
    .locals 3

    .line 60
    iget-object v0, p0, Ljp/colopl/drapro/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Landroid/preference/PreferenceManager;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v1, "screenLockMode"

    const/4 v2, 0x1

    .line 61
    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getBoolean(Ljava/lang/String;Z)Z

    move-result v0

    return v0
.end method

.method public getSpeakerPurchased(Ljava/lang/String;)Z
    .locals 2

    .line 40
    iget-object v0, p0, Ljp/colopl/drapro/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Landroid/preference/PreferenceManager;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    const/4 v1, 0x0

    .line 41
    invoke-interface {v0, p1, v1}, Landroid/content/SharedPreferences;->getBoolean(Ljava/lang/String;Z)Z

    move-result p1

    return p1
.end method

.method public setEnableAdView(Z)V
    .locals 2

    .line 35
    iget-object v0, p0, Ljp/colopl/drapro/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Landroid/preference/PreferenceManager;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 36
    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    const-string v1, "enableAdView"

    invoke-interface {v0, v1, p1}, Landroid/content/SharedPreferences$Editor;->putBoolean(Ljava/lang/String;Z)Landroid/content/SharedPreferences$Editor;

    move-result-object p1

    invoke-interface {p1}, Landroid/content/SharedPreferences$Editor;->commit()Z

    return-void
.end method

.method public setLastBootTime(ZLjava/lang/String;)V
    .locals 1

    .line 75
    iget-object v0, p0, Ljp/colopl/drapro/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Landroid/preference/PreferenceManager;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 76
    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    if-eqz p1, :cond_0

    const-string p1, "lastlBootTimeLocal"

    goto :goto_0

    :cond_0
    const-string p1, "lastBootTimeServer"

    :goto_0
    invoke-interface {v0, p1, p2}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    move-result-object p1

    invoke-interface {p1}, Landroid/content/SharedPreferences$Editor;->commit()Z

    return-void
.end method

.method public setLastVersionCode(I)V
    .locals 2

    .line 55
    iget-object v0, p0, Ljp/colopl/drapro/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Landroid/preference/PreferenceManager;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 56
    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    const-string v1, "lastVersionCode"

    invoke-interface {v0, v1, p1}, Landroid/content/SharedPreferences$Editor;->putInt(Ljava/lang/String;I)Landroid/content/SharedPreferences$Editor;

    move-result-object p1

    invoke-interface {p1}, Landroid/content/SharedPreferences$Editor;->commit()Z

    return-void
.end method

.method public setScreenLockMode(Z)V
    .locals 2

    .line 65
    iget-object v0, p0, Ljp/colopl/drapro/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Landroid/preference/PreferenceManager;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 66
    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    const-string v1, "screenLockMode"

    invoke-interface {v0, v1, p1}, Landroid/content/SharedPreferences$Editor;->putBoolean(Ljava/lang/String;Z)Landroid/content/SharedPreferences$Editor;

    move-result-object p1

    invoke-interface {p1}, Landroid/content/SharedPreferences$Editor;->commit()Z

    return-void
.end method

.method public setSpearkePurchased(Ljava/lang/String;Z)V
    .locals 1

    .line 45
    iget-object v0, p0, Ljp/colopl/drapro/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Landroid/preference/PreferenceManager;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 46
    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    invoke-interface {v0, p1, p2}, Landroid/content/SharedPreferences$Editor;->putBoolean(Ljava/lang/String;Z)Landroid/content/SharedPreferences$Editor;

    move-result-object p1

    invoke-interface {p1}, Landroid/content/SharedPreferences$Editor;->commit()Z

    return-void
.end method
