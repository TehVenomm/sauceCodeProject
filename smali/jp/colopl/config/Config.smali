.class public Ljp/colopl/config/Config;
.super Ljava/lang/Object;
.source "Config.java"


# static fields
.field private static PREF_KEY_PREF_LOCATON_ACCURACY:Ljava/lang/String; = "pref_location_acc"

.field private static PREF_KEY_PREF_LOCATON_LATITUDE:Ljava/lang/String; = "pref_location_lat"

.field private static PREF_KEY_PREF_LOCATON_LONGITUDE:Ljava/lang/String; = "pref_location_lon"

.field private static PREF_KEY_PREF_LOCATON_PROVIDER:Ljava/lang/String; = "pref_location_prov"

.field private static PREF_KEY_PREF_LOCATON_TIME:Ljava/lang/String; = "pref_location_time"

.field private static PREF_REFERRER_AT_INSTALLED:Ljava/lang/String; = "pref_ref_installed"

.field private static final SETTING_KEY_HAS_SETTINGS:Ljava/lang/String; = "hasSettings"

.field private static final TAG:Ljava/lang/String; = "Config"

.field public static debuggable:Z = false

.field private static mDefaultSharedPreferences:Landroid/content/SharedPreferences;


# instance fields
.field private context:Landroid/content/Context;

.field private session:Ljp/colopl/config/Session;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;)V
    .locals 1

    .line 35
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 36
    iput-object p1, p0, Ljp/colopl/config/Config;->context:Landroid/content/Context;

    .line 37
    new-instance v0, Ljp/colopl/config/Session;

    invoke-direct {v0, p1}, Ljp/colopl/config/Session;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Ljp/colopl/config/Config;->session:Ljp/colopl/config/Session;

    return-void
.end method

.method private static declared-synchronized getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;
    .locals 2

    const-class v0, Ljp/colopl/config/Config;

    monitor-enter v0

    .line 41
    :try_start_0
    sget-object v1, Ljp/colopl/config/Config;->mDefaultSharedPreferences:Landroid/content/SharedPreferences;

    if-nez v1, :cond_0

    .line 42
    invoke-static {p0}, Landroid/preference/PreferenceManager;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object p0

    sput-object p0, Ljp/colopl/config/Config;->mDefaultSharedPreferences:Landroid/content/SharedPreferences;

    .line 44
    :cond_0
    sget-object p0, Ljp/colopl/config/Config;->mDefaultSharedPreferences:Landroid/content/SharedPreferences;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit v0

    return-object p0

    :catchall_0
    move-exception p0

    .line 40
    monitor-exit v0

    throw p0
.end method

.method private getPreviousLocationAccuracy(Landroid/content/SharedPreferences;)F
    .locals 2

    .line 210
    sget-object v0, Ljp/colopl/config/Config;->PREF_KEY_PREF_LOCATON_ACCURACY:Ljava/lang/String;

    const/high16 v1, -0x40800000    # -1.0f

    invoke-interface {p1, v0, v1}, Landroid/content/SharedPreferences;->getFloat(Ljava/lang/String;F)F

    move-result p1

    return p1
.end method

.method private getPreviousLocationLatitude(Landroid/content/SharedPreferences;)D
    .locals 3

    .line 176
    sget-object v0, Ljp/colopl/config/Config;->PREF_KEY_PREF_LOCATON_LATITUDE:Ljava/lang/String;

    const-string v1, "0.0"

    invoke-interface {p1, v0, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 179
    :try_start_0
    invoke-static {p1}, Ljava/lang/Double;->valueOf(Ljava/lang/String;)Ljava/lang/Double;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Double;->doubleValue()D

    move-result-wide v0
    :try_end_0
    .catch Ljava/lang/NumberFormatException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "Config"

    .line 181
    invoke-virtual {v0}, Ljava/lang/NumberFormatException;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    const-string v0, "Config"

    .line 182
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "getPreviousLocationLatitude: latStr = "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    const-wide/16 v0, 0x0

    :goto_0
    return-wide v0
.end method

.method private getPreviousLocationLongitude(Landroid/content/SharedPreferences;)D
    .locals 3

    .line 188
    sget-object v0, Ljp/colopl/config/Config;->PREF_KEY_PREF_LOCATON_LONGITUDE:Ljava/lang/String;

    const-string v1, "0.0"

    invoke-interface {p1, v0, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 191
    :try_start_0
    invoke-static {p1}, Ljava/lang/Double;->valueOf(Ljava/lang/String;)Ljava/lang/Double;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Double;->doubleValue()D

    move-result-wide v0
    :try_end_0
    .catch Ljava/lang/NumberFormatException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "Config"

    .line 193
    invoke-virtual {v0}, Ljava/lang/NumberFormatException;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    const-string v0, "Config"

    .line 194
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "getPreviousLocationLongitude: lonStr = "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    const-wide/16 v0, 0x0

    :goto_0
    return-wide v0
.end method

.method private getPreviousLocationProvider(Landroid/content/SharedPreferences;)Ljava/lang/String;
    .locals 2

    .line 205
    sget-object v0, Ljp/colopl/config/Config;->PREF_KEY_PREF_LOCATON_PROVIDER:Ljava/lang/String;

    const-string v1, ""

    invoke-interface {p1, v0, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private getPreviousLocationTime(Landroid/content/SharedPreferences;)J
    .locals 3

    .line 200
    sget-object v0, Ljp/colopl/config/Config;->PREF_KEY_PREF_LOCATON_TIME:Ljava/lang/String;

    const-wide/16 v1, 0x0

    invoke-interface {p1, v0, v1, v2}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v0

    return-wide v0
.end method

.method public static getVersionCode(Landroid/content/Context;)I
    .locals 2

    .line 128
    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v0

    .line 130
    :try_start_0
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object p0

    const/16 v1, 0x80

    invoke-virtual {p0, v0, v1}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object p0

    iget p0, p0, Landroid/content/pm/PackageInfo;->versionCode:I
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const/4 p0, 0x0

    :goto_0
    return p0
.end method

.method public static getVersionName(Landroid/content/Context;)Ljava/lang/String;
    .locals 2

    .line 114
    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v0

    .line 116
    :try_start_0
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object p0

    const/16 v1, 0x80

    invoke-virtual {p0, v0, v1}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object p0

    iget-object p0, p0, Landroid/content/pm/PackageInfo;->versionName:Ljava/lang/String;
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const/4 p0, 0x0

    :goto_0
    return-object p0
.end method


# virtual methods
.method public generateToken()Ljava/lang/String;
    .locals 2

    .line 100
    :try_start_0
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    invoke-static {v0, v1}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Ljp/colopl/util/Crypto;->encrypt(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    const/4 v0, 0x0

    return-object v0
.end method

.method public getAndroidTokenCookieName()Ljava/lang/String;
    .locals 3

    .line 63
    iget-object v0, p0, Ljp/colopl/config/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Ljp/colopl/config/Config;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v1, "smartphoneTokenCookieName"

    const/4 v2, 0x0

    .line 64
    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getAppTypeCookieName()Ljava/lang/String;
    .locals 3

    .line 78
    iget-object v0, p0, Ljp/colopl/config/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Ljp/colopl/config/Config;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v1, "appTypeCookieName"

    const-string v2, "installedFrom"

    .line 79
    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getAppTypeCookieValue()Ljava/lang/String;
    .locals 1

    .line 83
    iget-object v0, p0, Ljp/colopl/config/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Ljp/colopl/libs/ColoplAppInfo;->isFromAuOneMarket(Landroid/content/Context;)Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "auone"

    goto :goto_0

    :cond_0
    const-string v0, "google"

    :goto_0
    return-object v0
.end method

.method public getAppVersionCookieName()Ljava/lang/String;
    .locals 3

    .line 68
    iget-object v0, p0, Ljp/colopl/config/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Ljp/colopl/config/Config;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v1, "appVersionCookieName"

    const-string v2, "apv"

    .line 69
    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getOSVersion()I
    .locals 1

    .line 137
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    return v0
.end method

.method public getOSVersionCookieName()Ljava/lang/String;
    .locals 3

    .line 73
    iget-object v0, p0, Ljp/colopl/config/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Ljp/colopl/config/Config;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v1, "osVersionCookieName"

    const-string v2, "osv"

    .line 74
    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getPreviousLocation()Landroid/location/Location;
    .locals 9

    .line 159
    iget-object v0, p0, Ljp/colopl/config/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Ljp/colopl/config/Config;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 160
    invoke-direct {p0, v0}, Ljp/colopl/config/Config;->getPreviousLocationLatitude(Landroid/content/SharedPreferences;)D

    move-result-wide v1

    .line 161
    invoke-direct {p0, v0}, Ljp/colopl/config/Config;->getPreviousLocationLongitude(Landroid/content/SharedPreferences;)D

    move-result-wide v3

    .line 162
    invoke-direct {p0, v0}, Ljp/colopl/config/Config;->getPreviousLocationTime(Landroid/content/SharedPreferences;)J

    move-result-wide v5

    .line 163
    invoke-direct {p0, v0}, Ljp/colopl/config/Config;->getPreviousLocationProvider(Landroid/content/SharedPreferences;)Ljava/lang/String;

    move-result-object v7

    .line 164
    invoke-direct {p0, v0}, Ljp/colopl/config/Config;->getPreviousLocationAccuracy(Landroid/content/SharedPreferences;)F

    move-result v0

    .line 165
    new-instance v8, Landroid/location/Location;

    invoke-direct {v8, v7}, Landroid/location/Location;-><init>(Ljava/lang/String;)V

    .line 166
    invoke-virtual {v8, v1, v2}, Landroid/location/Location;->setLatitude(D)V

    .line 167
    invoke-virtual {v8, v3, v4}, Landroid/location/Location;->setLongitude(D)V

    .line 168
    invoke-virtual {v8, v5, v6}, Landroid/location/Location;->setTime(J)V

    const/4 v1, 0x0

    cmpl-float v1, v0, v1

    if-lez v1, :cond_0

    .line 170
    invoke-virtual {v8, v0}, Landroid/location/Location;->setAccuracy(F)V

    :cond_0
    return-object v8
.end method

.method public getReferrerAtInstalled()Ljava/lang/String;
    .locals 3

    .line 88
    iget-object v0, p0, Ljp/colopl/config/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Ljp/colopl/config/Config;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 89
    sget-object v1, Ljp/colopl/config/Config;->PREF_REFERRER_AT_INSTALLED:Ljava/lang/String;

    const-string v2, ""

    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getRequiredVersion()I
    .locals 3

    .line 58
    iget-object v0, p0, Ljp/colopl/config/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Ljp/colopl/config/Config;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v1, "requiredVersion"

    const/4 v2, 0x0

    .line 59
    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getInt(Ljava/lang/String;I)I

    move-result v0

    return v0
.end method

.method public getSession()Ljp/colopl/config/Session;
    .locals 1

    .line 48
    iget-object v0, p0, Ljp/colopl/config/Config;->session:Ljp/colopl/config/Session;

    return-object v0
.end method

.method public getVersionCode()I
    .locals 1

    .line 123
    iget-object v0, p0, Ljp/colopl/config/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Ljp/colopl/config/Config;->getVersionCode(Landroid/content/Context;)I

    move-result v0

    return v0
.end method

.method public getVersionName()Ljava/lang/String;
    .locals 1

    .line 109
    iget-object v0, p0, Ljp/colopl/config/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Ljp/colopl/config/Config;->getVersionName(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public hasSettings()Z
    .locals 3

    .line 141
    iget-object v0, p0, Ljp/colopl/config/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Ljp/colopl/config/Config;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v1, "hasSettings"

    const/4 v2, 0x0

    .line 142
    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getBoolean(Ljava/lang/String;Z)Z

    move-result v0

    return v0
.end method

.method public isUpdateRequired()Z
    .locals 2

    .line 52
    invoke-virtual {p0}, Ljp/colopl/config/Config;->getRequiredVersion()I

    move-result v0

    .line 53
    invoke-virtual {p0}, Ljp/colopl/config/Config;->getVersionCode()I

    move-result v1

    if-le v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public setPreviousNWLocation(Landroid/location/Location;)V
    .locals 4

    if-nez p1, :cond_0

    return-void

    .line 149
    :cond_0
    iget-object v0, p0, Ljp/colopl/config/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Ljp/colopl/config/Config;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 150
    sget-object v1, Ljp/colopl/config/Config;->PREF_KEY_PREF_LOCATON_LATITUDE:Ljava/lang/String;

    .line 151
    invoke-virtual {p1}, Landroid/location/Location;->getLatitude()D

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/String;->valueOf(D)Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    sget-object v1, Ljp/colopl/config/Config;->PREF_KEY_PREF_LOCATON_LONGITUDE:Ljava/lang/String;

    .line 152
    invoke-virtual {p1}, Landroid/location/Location;->getLongitude()D

    move-result-wide v2

    invoke-static {v2, v3}, Ljava/lang/String;->valueOf(D)Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    sget-object v1, Ljp/colopl/config/Config;->PREF_KEY_PREF_LOCATON_TIME:Ljava/lang/String;

    .line 153
    invoke-virtual {p1}, Landroid/location/Location;->getTime()J

    move-result-wide v2

    invoke-interface {v0, v1, v2, v3}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    sget-object v1, Ljp/colopl/config/Config;->PREF_KEY_PREF_LOCATON_PROVIDER:Ljava/lang/String;

    .line 154
    invoke-virtual {p1}, Landroid/location/Location;->getProvider()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    sget-object v1, Ljp/colopl/config/Config;->PREF_KEY_PREF_LOCATON_ACCURACY:Ljava/lang/String;

    .line 155
    invoke-virtual {p1}, Landroid/location/Location;->hasAccuracy()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-virtual {p1}, Landroid/location/Location;->getAccuracy()F

    move-result p1

    goto :goto_0

    :cond_1
    const/high16 p1, -0x40800000    # -1.0f

    :goto_0
    invoke-interface {v0, v1, p1}, Landroid/content/SharedPreferences$Editor;->putFloat(Ljava/lang/String;F)Landroid/content/SharedPreferences$Editor;

    move-result-object p1

    .line 151
    invoke-static {p1}, Lcom/google/android/zippy/SharedPreferencesCompat;->apply(Landroid/content/SharedPreferences$Editor;)V

    return-void
.end method

.method public setReferrerAtInstalled(Ljava/lang/String;)V
    .locals 2

    .line 93
    iget-object v0, p0, Ljp/colopl/config/Config;->context:Landroid/content/Context;

    invoke-static {v0}, Ljp/colopl/config/Config;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 94
    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    sget-object v1, Ljp/colopl/config/Config;->PREF_REFERRER_AT_INSTALLED:Ljava/lang/String;

    invoke-interface {v0, v1, p1}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    move-result-object p1

    invoke-static {p1}, Lcom/google/android/zippy/SharedPreferencesCompat;->apply(Landroid/content/SharedPreferences$Editor;)V

    return-void
.end method
