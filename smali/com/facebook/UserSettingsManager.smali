.class final Lcom/facebook/UserSettingsManager;
.super Ljava/lang/Object;
.source "UserSettingsManager.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/facebook/UserSettingsManager$UserSetting;
    }
.end annotation


# static fields
.field private static final ADVERTISERID_COLLECTION_FALSE_WARNING:Ljava/lang/String; = "The value for AdvertiserIDCollectionEnabled is currently set to FALSE so you\'re sending app events without collecting Advertiser ID. This can affect the quality of your advertising and analytics results."

.field private static final ADVERTISERID_COLLECTION_NOT_SET_WARNING:Ljava/lang/String; = "You haven\'t set a value for AdvertiserIDCollectionEnabled. Set the flag to TRUE if you want to collect Advertiser ID for better advertising and analytics results. To request user consent before collecting data, set the flag value to FALSE, then change to TRUE once user consent is received. Learn more: https://developers.facebook.com/docs/app-events/getting-started-app-events-android#disable-auto-events."

.field private static final ADVERTISER_ID_KEY:Ljava/lang/String; = "advertiser_id"

.field private static final APPLICATION_FIELDS:Ljava/lang/String; = "fields"

.field private static final AUTOLOG_APPEVENT_NOT_SET_WARNING:Ljava/lang/String; = "Please set a value for AutoLogAppEventsEnabled. Set the flag to TRUE if you want to collect app install, app launch and in-app purchase events automatically. To request user consent before collecting data, set the flag value to FALSE, then change to TRUE once user consent is received. Learn more: https://developers.facebook.com/docs/app-events/getting-started-app-events-android#disable-auto-events."

.field private static final AUTO_APP_LINK_WARNING:Ljava/lang/String; = "You haven\'t set the Auto App Link URL scheme: fb<YOUR APP ID> in AndroidManifest"

.field private static final EVENTS_CODELESS_SETUP_ENABLED:Ljava/lang/String; = "auto_event_setup_enabled"

.field private static final LAST_TIMESTAMP:Ljava/lang/String; = "last_timestamp"

.field private static final TAG:Ljava/lang/String; = "com.facebook.UserSettingsManager"

.field private static final TIMEOUT_7D:J = 0x240c8400L

.field private static final USER_SETTINGS:Ljava/lang/String; = "com.facebook.sdk.USER_SETTINGS"

.field private static final USER_SETTINGS_BITMASK:Ljava/lang/String; = "com.facebook.sdk.USER_SETTINGS_BITMASK"

.field private static final VALUE:Ljava/lang/String; = "value"

.field private static advertiserIDCollectionEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

.field private static autoInitEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

.field private static autoLogAppEventsEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

.field private static codelessSetupEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

.field private static isFetchingCodelessStatus:Ljava/util/concurrent/atomic/AtomicBoolean;

.field private static isInitialized:Ljava/util/concurrent/atomic/AtomicBoolean;

.field private static userSettingPref:Landroid/content/SharedPreferences;


# direct methods
.method static constructor <clinit>()V
    .locals 4

    .line 48
    new-instance v0, Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x0

    invoke-direct {v0, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;-><init>(Z)V

    sput-object v0, Lcom/facebook/UserSettingsManager;->isInitialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    .line 49
    new-instance v0, Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-direct {v0, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;-><init>(Z)V

    sput-object v0, Lcom/facebook/UserSettingsManager;->isFetchingCodelessStatus:Ljava/util/concurrent/atomic/AtomicBoolean;

    .line 57
    new-instance v0, Lcom/facebook/UserSettingsManager$UserSetting;

    const-string v2, "com.facebook.sdk.AutoInitEnabled"

    const/4 v3, 0x1

    invoke-direct {v0, v3, v2}, Lcom/facebook/UserSettingsManager$UserSetting;-><init>(ZLjava/lang/String;)V

    sput-object v0, Lcom/facebook/UserSettingsManager;->autoInitEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    .line 60
    new-instance v0, Lcom/facebook/UserSettingsManager$UserSetting;

    const-string v2, "com.facebook.sdk.AutoLogAppEventsEnabled"

    invoke-direct {v0, v3, v2}, Lcom/facebook/UserSettingsManager$UserSetting;-><init>(ZLjava/lang/String;)V

    sput-object v0, Lcom/facebook/UserSettingsManager;->autoLogAppEventsEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    .line 63
    new-instance v0, Lcom/facebook/UserSettingsManager$UserSetting;

    const-string v2, "com.facebook.sdk.AdvertiserIDCollectionEnabled"

    invoke-direct {v0, v3, v2}, Lcom/facebook/UserSettingsManager$UserSetting;-><init>(ZLjava/lang/String;)V

    sput-object v0, Lcom/facebook/UserSettingsManager;->advertiserIDCollectionEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    .line 66
    new-instance v0, Lcom/facebook/UserSettingsManager$UserSetting;

    const-string v2, "auto_event_setup_enabled"

    invoke-direct {v0, v1, v2}, Lcom/facebook/UserSettingsManager$UserSetting;-><init>(ZLjava/lang/String;)V

    sput-object v0, Lcom/facebook/UserSettingsManager;->codelessSetupEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    return-void
.end method

.method constructor <init>()V
    .locals 0

    .line 45
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static synthetic access$000()Lcom/facebook/UserSettingsManager$UserSetting;
    .locals 1

    .line 45
    sget-object v0, Lcom/facebook/UserSettingsManager;->advertiserIDCollectionEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    return-object v0
.end method

.method static synthetic access$100()Lcom/facebook/UserSettingsManager$UserSetting;
    .locals 1

    .line 45
    sget-object v0, Lcom/facebook/UserSettingsManager;->codelessSetupEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    return-object v0
.end method

.method static synthetic access$200(Lcom/facebook/UserSettingsManager$UserSetting;)V
    .locals 0

    .line 45
    invoke-static {p0}, Lcom/facebook/UserSettingsManager;->writeSettingToCache(Lcom/facebook/UserSettingsManager$UserSetting;)V

    return-void
.end method

.method static synthetic access$300()Ljava/util/concurrent/atomic/AtomicBoolean;
    .locals 1

    .line 45
    sget-object v0, Lcom/facebook/UserSettingsManager;->isFetchingCodelessStatus:Ljava/util/concurrent/atomic/AtomicBoolean;

    return-object v0
.end method

.method public static getAdvertiserIDCollectionEnabled()Z
    .locals 1

    .line 381
    invoke-static {}, Lcom/facebook/UserSettingsManager;->initializeIfNotInitialized()V

    .line 382
    sget-object v0, Lcom/facebook/UserSettingsManager;->advertiserIDCollectionEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-virtual {v0}, Lcom/facebook/UserSettingsManager$UserSetting;->getValue()Z

    move-result v0

    return v0
.end method

.method public static getAutoInitEnabled()Z
    .locals 1

    .line 351
    invoke-static {}, Lcom/facebook/UserSettingsManager;->initializeIfNotInitialized()V

    .line 352
    sget-object v0, Lcom/facebook/UserSettingsManager;->autoInitEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-virtual {v0}, Lcom/facebook/UserSettingsManager$UserSetting;->getValue()Z

    move-result v0

    return v0
.end method

.method public static getAutoLogAppEventsEnabled()Z
    .locals 1

    .line 366
    invoke-static {}, Lcom/facebook/UserSettingsManager;->initializeIfNotInitialized()V

    .line 367
    sget-object v0, Lcom/facebook/UserSettingsManager;->autoLogAppEventsEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-virtual {v0}, Lcom/facebook/UserSettingsManager$UserSetting;->getValue()Z

    move-result v0

    return v0
.end method

.method public static getCodelessSetupEnabled()Z
    .locals 1

    .line 386
    invoke-static {}, Lcom/facebook/UserSettingsManager;->initializeIfNotInitialized()V

    .line 387
    sget-object v0, Lcom/facebook/UserSettingsManager;->codelessSetupEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-virtual {v0}, Lcom/facebook/UserSettingsManager$UserSetting;->getValue()Z

    move-result v0

    return v0
.end method

.method private static initializeCodelessSetupEnabledAsync()V
    .locals 7

    .line 138
    sget-object v0, Lcom/facebook/UserSettingsManager;->codelessSetupEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-static {v0}, Lcom/facebook/UserSettingsManager;->readSettingFromCache(Lcom/facebook/UserSettingsManager$UserSetting;)V

    .line 139
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    .line 140
    sget-object v2, Lcom/facebook/UserSettingsManager;->codelessSetupEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    iget-object v2, v2, Lcom/facebook/UserSettingsManager$UserSetting;->value:Ljava/lang/Boolean;

    if-eqz v2, :cond_0

    sget-object v2, Lcom/facebook/UserSettingsManager;->codelessSetupEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    iget-wide v2, v2, Lcom/facebook/UserSettingsManager$UserSetting;->lastTS:J

    sub-long v2, v0, v2

    const-wide/32 v4, 0x240c8400

    cmp-long v6, v2, v4

    if-gez v6, :cond_0

    return-void

    .line 143
    :cond_0
    sget-object v2, Lcom/facebook/UserSettingsManager;->codelessSetupEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    const/4 v3, 0x0

    iput-object v3, v2, Lcom/facebook/UserSettingsManager$UserSetting;->value:Ljava/lang/Boolean;

    .line 144
    sget-object v2, Lcom/facebook/UserSettingsManager;->codelessSetupEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    const-wide/16 v3, 0x0

    iput-wide v3, v2, Lcom/facebook/UserSettingsManager$UserSetting;->lastTS:J

    .line 147
    sget-object v2, Lcom/facebook/UserSettingsManager;->isFetchingCodelessStatus:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v3, 0x0

    const/4 v4, 0x1

    invoke-virtual {v2, v3, v4}, Ljava/util/concurrent/atomic/AtomicBoolean;->compareAndSet(ZZ)Z

    move-result v2

    if-nez v2, :cond_1

    return-void

    .line 151
    :cond_1
    invoke-static {}, Lcom/facebook/FacebookSdk;->getExecutor()Ljava/util/concurrent/Executor;

    move-result-object v2

    new-instance v3, Lcom/facebook/UserSettingsManager$1;

    invoke-direct {v3, v0, v1}, Lcom/facebook/UserSettingsManager$1;-><init>(J)V

    invoke-interface {v2, v3}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static initializeIfNotInitialized()V
    .locals 4

    .line 101
    invoke-static {}, Lcom/facebook/FacebookSdk;->isInitialized()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 105
    :cond_0
    sget-object v0, Lcom/facebook/UserSettingsManager;->isInitialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x1

    const/4 v2, 0x0

    invoke-virtual {v0, v2, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;->compareAndSet(ZZ)Z

    move-result v0

    if-nez v0, :cond_1

    return-void

    .line 109
    :cond_1
    invoke-static {}, Lcom/facebook/FacebookSdk;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    const-string v3, "com.facebook.sdk.USER_SETTINGS"

    .line 110
    invoke-virtual {v0, v3, v2}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v0

    sput-object v0, Lcom/facebook/UserSettingsManager;->userSettingPref:Landroid/content/SharedPreferences;

    const/4 v0, 0x3

    .line 112
    new-array v0, v0, [Lcom/facebook/UserSettingsManager$UserSetting;

    sget-object v3, Lcom/facebook/UserSettingsManager;->autoLogAppEventsEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    aput-object v3, v0, v2

    sget-object v2, Lcom/facebook/UserSettingsManager;->advertiserIDCollectionEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    aput-object v2, v0, v1

    const/4 v1, 0x2

    sget-object v2, Lcom/facebook/UserSettingsManager;->autoInitEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    aput-object v2, v0, v1

    invoke-static {v0}, Lcom/facebook/UserSettingsManager;->initializeUserSetting([Lcom/facebook/UserSettingsManager$UserSetting;)V

    .line 113
    invoke-static {}, Lcom/facebook/UserSettingsManager;->initializeCodelessSetupEnabledAsync()V

    .line 114
    invoke-static {}, Lcom/facebook/UserSettingsManager;->logWarnings()V

    .line 115
    invoke-static {}, Lcom/facebook/UserSettingsManager;->logIfSDKSettingsChanged()V

    return-void
.end method

.method private static varargs initializeUserSetting([Lcom/facebook/UserSettingsManager$UserSetting;)V
    .locals 3

    const/4 v0, 0x0

    .line 119
    :goto_0
    array-length v1, p0

    if-ge v0, v1, :cond_3

    .line 120
    aget-object v1, p0, v0

    .line 121
    sget-object v2, Lcom/facebook/UserSettingsManager;->codelessSetupEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    if-ne v1, v2, :cond_0

    .line 122
    invoke-static {}, Lcom/facebook/UserSettingsManager;->initializeCodelessSetupEnabledAsync()V

    goto :goto_1

    .line 124
    :cond_0
    iget-object v2, v1, Lcom/facebook/UserSettingsManager$UserSetting;->value:Ljava/lang/Boolean;

    if-nez v2, :cond_1

    .line 125
    invoke-static {v1}, Lcom/facebook/UserSettingsManager;->readSettingFromCache(Lcom/facebook/UserSettingsManager$UserSetting;)V

    .line 126
    iget-object v2, v1, Lcom/facebook/UserSettingsManager$UserSetting;->value:Ljava/lang/Boolean;

    if-nez v2, :cond_2

    .line 127
    invoke-static {v1}, Lcom/facebook/UserSettingsManager;->loadSettingFromManifest(Lcom/facebook/UserSettingsManager$UserSetting;)V

    goto :goto_1

    .line 131
    :cond_1
    invoke-static {v1}, Lcom/facebook/UserSettingsManager;->writeSettingToCache(Lcom/facebook/UserSettingsManager$UserSetting;)V

    :cond_2
    :goto_1
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_3
    return-void
.end method

.method private static loadSettingFromManifest(Lcom/facebook/UserSettingsManager$UserSetting;)V
    .locals 3

    .line 221
    invoke-static {}, Lcom/facebook/UserSettingsManager;->validateInitialized()V

    .line 223
    :try_start_0
    invoke-static {}, Lcom/facebook/FacebookSdk;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    .line 224
    invoke-virtual {v0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    .line 226
    invoke-virtual {v0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v0

    const/16 v2, 0x80

    .line 225
    invoke-virtual {v1, v0, v2}, Landroid/content/pm/PackageManager;->getApplicationInfo(Ljava/lang/String;I)Landroid/content/pm/ApplicationInfo;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 228
    iget-object v1, v0, Landroid/content/pm/ApplicationInfo;->metaData:Landroid/os/Bundle;

    if-eqz v1, :cond_0

    iget-object v1, v0, Landroid/content/pm/ApplicationInfo;->metaData:Landroid/os/Bundle;

    iget-object v2, p0, Lcom/facebook/UserSettingsManager$UserSetting;->key:Ljava/lang/String;

    invoke-virtual {v1, v2}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 230
    iget-object v0, v0, Landroid/content/pm/ApplicationInfo;->metaData:Landroid/os/Bundle;

    iget-object v1, p0, Lcom/facebook/UserSettingsManager$UserSetting;->key:Ljava/lang/String;

    iget-boolean v2, p0, Lcom/facebook/UserSettingsManager$UserSetting;->defaultVal:Z

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result v0

    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    iput-object v0, p0, Lcom/facebook/UserSettingsManager$UserSetting;->value:Ljava/lang/Boolean;
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    .line 234
    sget-object v0, Lcom/facebook/UserSettingsManager;->TAG:Ljava/lang/String;

    invoke-static {v0, p0}, Lcom/facebook/internal/Utility;->logd(Ljava/lang/String;Ljava/lang/Exception;)V

    :cond_0
    :goto_0
    return-void
.end method

.method static logIfAutoAppLinkEnabled()V
    .locals 4

    .line 314
    :try_start_0
    invoke-static {}, Lcom/facebook/FacebookSdk;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    .line 315
    invoke-virtual {v0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    .line 316
    invoke-virtual {v0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v2

    const/16 v3, 0x80

    .line 315
    invoke-virtual {v1, v2, v3}, Landroid/content/pm/PackageManager;->getApplicationInfo(Ljava/lang/String;I)Landroid/content/pm/ApplicationInfo;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 317
    iget-object v2, v1, Landroid/content/pm/ApplicationInfo;->metaData:Landroid/os/Bundle;

    if-eqz v2, :cond_1

    iget-object v1, v1, Landroid/content/pm/ApplicationInfo;->metaData:Landroid/os/Bundle;

    const-string v2, "com.facebook.sdk.AutoAppLinkEnabled"

    const/4 v3, 0x0

    .line 318
    invoke-virtual {v1, v2, v3}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 319
    new-instance v1, Lcom/facebook/appevents/InternalAppEventsLogger;

    invoke-direct {v1, v0}, Lcom/facebook/appevents/InternalAppEventsLogger;-><init>(Landroid/content/Context;)V

    .line 320
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    .line 321
    invoke-static {}, Lcom/facebook/internal/Utility;->isAutoAppLinkSetup()Z

    move-result v2

    if-nez v2, :cond_0

    const-string v2, "SchemeWarning"

    const-string v3, "You haven\'t set the Auto App Link URL scheme: fb<YOUR APP ID> in AndroidManifest"

    .line 322
    invoke-virtual {v0, v2, v3}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 323
    sget-object v2, Lcom/facebook/UserSettingsManager;->TAG:Ljava/lang/String;

    const-string v3, "You haven\'t set the Auto App Link URL scheme: fb<YOUR APP ID> in AndroidManifest"

    invoke-static {v2, v3}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :cond_0
    const-string v2, "fb_auto_applink"

    .line 325
    invoke-virtual {v1, v2, v0}, Lcom/facebook/appevents/InternalAppEventsLogger;->logEvent(Ljava/lang/String;Landroid/os/Bundle;)V
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :cond_1
    return-void
.end method

.method private static logIfSDKSettingsChanged()V
    .locals 12

    .line 261
    sget-object v0, Lcom/facebook/UserSettingsManager;->isInitialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 265
    :cond_0
    invoke-static {}, Lcom/facebook/FacebookSdk;->isInitialized()Z

    move-result v0

    if-nez v0, :cond_1

    return-void

    .line 269
    :cond_1
    invoke-static {}, Lcom/facebook/FacebookSdk;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    .line 273
    sget-object v1, Lcom/facebook/UserSettingsManager;->autoInitEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-virtual {v1}, Lcom/facebook/UserSettingsManager$UserSetting;->getValue()Z

    move-result v1

    const/4 v2, 0x0

    shl-int/2addr v1, v2

    or-int/2addr v1, v2

    .line 274
    sget-object v3, Lcom/facebook/UserSettingsManager;->autoLogAppEventsEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-virtual {v3}, Lcom/facebook/UserSettingsManager$UserSetting;->getValue()Z

    move-result v3

    shl-int/lit8 v3, v3, 0x1

    or-int/2addr v1, v3

    .line 275
    sget-object v3, Lcom/facebook/UserSettingsManager;->advertiserIDCollectionEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-virtual {v3}, Lcom/facebook/UserSettingsManager$UserSetting;->getValue()Z

    move-result v3

    shl-int/lit8 v3, v3, 0x2

    or-int/2addr v1, v3

    .line 277
    sget-object v3, Lcom/facebook/UserSettingsManager;->userSettingPref:Landroid/content/SharedPreferences;

    const-string v4, "com.facebook.sdk.USER_SETTINGS_BITMASK"

    invoke-interface {v3, v4, v2}, Landroid/content/SharedPreferences;->getInt(Ljava/lang/String;I)I

    move-result v3

    if-eq v3, v1, :cond_4

    .line 279
    sget-object v4, Lcom/facebook/UserSettingsManager;->userSettingPref:Landroid/content/SharedPreferences;

    invoke-interface {v4}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v4

    const-string v5, "com.facebook.sdk.USER_SETTINGS_BITMASK"

    invoke-interface {v4, v5, v1}, Landroid/content/SharedPreferences$Editor;->putInt(Ljava/lang/String;I)Landroid/content/SharedPreferences$Editor;

    move-result-object v4

    invoke-interface {v4}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 283
    :try_start_0
    invoke-virtual {v0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v4

    .line 285
    invoke-virtual {v0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v5

    const/16 v6, 0x80

    .line 284
    invoke-virtual {v4, v5, v6}, Landroid/content/pm/PackageManager;->getApplicationInfo(Ljava/lang/String;I)Landroid/content/pm/ApplicationInfo;

    move-result-object v4

    if-eqz v4, :cond_3

    .line 287
    iget-object v5, v4, Landroid/content/pm/ApplicationInfo;->metaData:Landroid/os/Bundle;

    if-eqz v5, :cond_3

    const-string v5, "com.facebook.sdk.AutoInitEnabled"

    const-string v6, "com.facebook.sdk.AutoLogAppEventsEnabled"

    const-string v7, "com.facebook.sdk.AdvertiserIDCollectionEnabled"

    .line 288
    filled-new-array {v5, v6, v7}, [Ljava/lang/String;

    move-result-object v5

    const/4 v6, 0x3

    .line 293
    new-array v6, v6, [Z
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_1

    fill-array-data v6, :array_0

    const/4 v7, 0x0

    const/4 v8, 0x0

    .line 294
    :goto_0
    :try_start_1
    array-length v9, v5

    if-ge v2, v9, :cond_2

    .line 295
    iget-object v9, v4, Landroid/content/pm/ApplicationInfo;->metaData:Landroid/os/Bundle;

    aget-object v10, v5, v2

    invoke-virtual {v9, v10}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v9

    shl-int/2addr v9, v2

    or-int/2addr v7, v9

    .line 296
    iget-object v9, v4, Landroid/content/pm/ApplicationInfo;->metaData:Landroid/os/Bundle;

    aget-object v10, v5, v2

    aget-boolean v11, v6, v2

    invoke-virtual {v9, v10, v11}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result v9
    :try_end_1
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_1 .. :try_end_1} :catch_0

    shl-int/2addr v9, v2

    or-int/2addr v8, v9

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :catch_0
    :cond_2
    move v2, v7

    goto :goto_1

    :catch_1
    :cond_3
    const/4 v8, 0x0

    .line 302
    :goto_1
    new-instance v4, Lcom/facebook/appevents/InternalAppEventsLogger;

    invoke-direct {v4, v0}, Lcom/facebook/appevents/InternalAppEventsLogger;-><init>(Landroid/content/Context;)V

    .line 303
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v5, "usage"

    .line 304
    invoke-virtual {v0, v5, v2}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v2, "initial"

    .line 305
    invoke-virtual {v0, v2, v8}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v2, "previous"

    .line 306
    invoke-virtual {v0, v2, v3}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v2, "current"

    .line 307
    invoke-virtual {v0, v2, v1}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v1, "fb_sdk_settings_changed"

    .line 308
    invoke-virtual {v4, v1, v0}, Lcom/facebook/appevents/InternalAppEventsLogger;->logEventImplicitly(Ljava/lang/String;Landroid/os/Bundle;)V

    :cond_4
    return-void

    nop

    :array_0
    .array-data 1
        0x1t
        0x1t
        0x1t
    .end array-data
.end method

.method private static logWarnings()V
    .locals 3

    .line 240
    :try_start_0
    invoke-static {}, Lcom/facebook/FacebookSdk;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    .line 241
    invoke-virtual {v0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    .line 243
    invoke-virtual {v0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v0

    const/16 v2, 0x80

    .line 242
    invoke-virtual {v1, v0, v2}, Landroid/content/pm/PackageManager;->getApplicationInfo(Ljava/lang/String;I)Landroid/content/pm/ApplicationInfo;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 245
    iget-object v1, v0, Landroid/content/pm/ApplicationInfo;->metaData:Landroid/os/Bundle;

    if-eqz v1, :cond_2

    .line 247
    iget-object v1, v0, Landroid/content/pm/ApplicationInfo;->metaData:Landroid/os/Bundle;

    const-string v2, "com.facebook.sdk.AutoLogAppEventsEnabled"

    invoke-virtual {v1, v2}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_0

    .line 248
    sget-object v1, Lcom/facebook/UserSettingsManager;->TAG:Ljava/lang/String;

    const-string v2, "Please set a value for AutoLogAppEventsEnabled. Set the flag to TRUE if you want to collect app install, app launch and in-app purchase events automatically. To request user consent before collecting data, set the flag value to FALSE, then change to TRUE once user consent is received. Learn more: https://developers.facebook.com/docs/app-events/getting-started-app-events-android#disable-auto-events."

    invoke-static {v1, v2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 250
    :cond_0
    iget-object v0, v0, Landroid/content/pm/ApplicationInfo;->metaData:Landroid/os/Bundle;

    const-string v1, "com.facebook.sdk.AdvertiserIDCollectionEnabled"

    invoke-virtual {v0, v1}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_1

    .line 251
    sget-object v0, Lcom/facebook/UserSettingsManager;->TAG:Ljava/lang/String;

    const-string v1, "You haven\'t set a value for AdvertiserIDCollectionEnabled. Set the flag to TRUE if you want to collect Advertiser ID for better advertising and analytics results. To request user consent before collecting data, set the flag value to FALSE, then change to TRUE once user consent is received. Learn more: https://developers.facebook.com/docs/app-events/getting-started-app-events-android#disable-auto-events."

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 253
    :cond_1
    invoke-static {}, Lcom/facebook/UserSettingsManager;->getAdvertiserIDCollectionEnabled()Z

    move-result v0

    if-nez v0, :cond_2

    .line 254
    sget-object v0, Lcom/facebook/UserSettingsManager;->TAG:Ljava/lang/String;

    const-string v1, "The value for AdvertiserIDCollectionEnabled is currently set to FALSE so you\'re sending app events without collecting Advertiser ID. This can affect the quality of your advertising and analytics results."

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :cond_2
    return-void
.end method

.method private static readSettingFromCache(Lcom/facebook/UserSettingsManager$UserSetting;)V
    .locals 3

    .line 207
    invoke-static {}, Lcom/facebook/UserSettingsManager;->validateInitialized()V

    .line 209
    :try_start_0
    sget-object v0, Lcom/facebook/UserSettingsManager;->userSettingPref:Landroid/content/SharedPreferences;

    iget-object v1, p0, Lcom/facebook/UserSettingsManager$UserSetting;->key:Ljava/lang/String;

    const-string v2, ""

    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 210
    invoke-virtual {v0}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-nez v1, :cond_0

    .line 211
    new-instance v1, Lorg/json/JSONObject;

    invoke-direct {v1, v0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v0, "value"

    .line 212
    invoke-virtual {v1, v0}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v0

    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    iput-object v0, p0, Lcom/facebook/UserSettingsManager$UserSetting;->value:Ljava/lang/Boolean;

    const-string v0, "last_timestamp"

    .line 213
    invoke-virtual {v1, v0}, Lorg/json/JSONObject;->getLong(Ljava/lang/String;)J

    move-result-wide v0

    iput-wide v0, p0, Lcom/facebook/UserSettingsManager$UserSetting;->lastTS:J
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    .line 216
    sget-object v0, Lcom/facebook/UserSettingsManager;->TAG:Ljava/lang/String;

    invoke-static {v0, p0}, Lcom/facebook/internal/Utility;->logd(Ljava/lang/String;Ljava/lang/Exception;)V

    :cond_0
    :goto_0
    return-void
.end method

.method public static setAdvertiserIDCollectionEnabled(Z)V
    .locals 2

    .line 371
    sget-object v0, Lcom/facebook/UserSettingsManager;->advertiserIDCollectionEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-static {p0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p0

    iput-object p0, v0, Lcom/facebook/UserSettingsManager$UserSetting;->value:Ljava/lang/Boolean;

    .line 372
    sget-object p0, Lcom/facebook/UserSettingsManager;->advertiserIDCollectionEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    iput-wide v0, p0, Lcom/facebook/UserSettingsManager$UserSetting;->lastTS:J

    .line 373
    sget-object p0, Lcom/facebook/UserSettingsManager;->isInitialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {p0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result p0

    if-eqz p0, :cond_0

    .line 374
    sget-object p0, Lcom/facebook/UserSettingsManager;->advertiserIDCollectionEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-static {p0}, Lcom/facebook/UserSettingsManager;->writeSettingToCache(Lcom/facebook/UserSettingsManager$UserSetting;)V

    goto :goto_0

    .line 376
    :cond_0
    invoke-static {}, Lcom/facebook/UserSettingsManager;->initializeIfNotInitialized()V

    :goto_0
    return-void
.end method

.method public static setAutoInitEnabled(Z)V
    .locals 2

    .line 341
    sget-object v0, Lcom/facebook/UserSettingsManager;->autoInitEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-static {p0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p0

    iput-object p0, v0, Lcom/facebook/UserSettingsManager$UserSetting;->value:Ljava/lang/Boolean;

    .line 342
    sget-object p0, Lcom/facebook/UserSettingsManager;->autoInitEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    iput-wide v0, p0, Lcom/facebook/UserSettingsManager$UserSetting;->lastTS:J

    .line 343
    sget-object p0, Lcom/facebook/UserSettingsManager;->isInitialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {p0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result p0

    if-eqz p0, :cond_0

    .line 344
    sget-object p0, Lcom/facebook/UserSettingsManager;->autoInitEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-static {p0}, Lcom/facebook/UserSettingsManager;->writeSettingToCache(Lcom/facebook/UserSettingsManager$UserSetting;)V

    goto :goto_0

    .line 346
    :cond_0
    invoke-static {}, Lcom/facebook/UserSettingsManager;->initializeIfNotInitialized()V

    :goto_0
    return-void
.end method

.method public static setAutoLogAppEventsEnabled(Z)V
    .locals 2

    .line 356
    sget-object v0, Lcom/facebook/UserSettingsManager;->autoLogAppEventsEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-static {p0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p0

    iput-object p0, v0, Lcom/facebook/UserSettingsManager$UserSetting;->value:Ljava/lang/Boolean;

    .line 357
    sget-object p0, Lcom/facebook/UserSettingsManager;->autoLogAppEventsEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    iput-wide v0, p0, Lcom/facebook/UserSettingsManager$UserSetting;->lastTS:J

    .line 358
    sget-object p0, Lcom/facebook/UserSettingsManager;->isInitialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {p0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result p0

    if-eqz p0, :cond_0

    .line 359
    sget-object p0, Lcom/facebook/UserSettingsManager;->autoLogAppEventsEnabled:Lcom/facebook/UserSettingsManager$UserSetting;

    invoke-static {p0}, Lcom/facebook/UserSettingsManager;->writeSettingToCache(Lcom/facebook/UserSettingsManager$UserSetting;)V

    goto :goto_0

    .line 361
    :cond_0
    invoke-static {}, Lcom/facebook/UserSettingsManager;->initializeIfNotInitialized()V

    :goto_0
    return-void
.end method

.method private static validateInitialized()V
    .locals 2

    .line 334
    sget-object v0, Lcom/facebook/UserSettingsManager;->isInitialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 335
    :cond_0
    new-instance v0, Lcom/facebook/FacebookSdkNotInitializedException;

    const-string v1, "The UserSettingManager has not been initialized successfully"

    invoke-direct {v0, v1}, Lcom/facebook/FacebookSdkNotInitializedException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private static writeSettingToCache(Lcom/facebook/UserSettingsManager$UserSetting;)V
    .locals 4

    .line 192
    invoke-static {}, Lcom/facebook/UserSettingsManager;->validateInitialized()V

    .line 194
    :try_start_0
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    const-string v1, "value"

    .line 195
    iget-object v2, p0, Lcom/facebook/UserSettingsManager$UserSetting;->value:Ljava/lang/Boolean;

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "last_timestamp"

    .line 196
    iget-wide v2, p0, Lcom/facebook/UserSettingsManager$UserSetting;->lastTS:J

    invoke-virtual {v0, v1, v2, v3}, Lorg/json/JSONObject;->put(Ljava/lang/String;J)Lorg/json/JSONObject;

    .line 197
    sget-object v1, Lcom/facebook/UserSettingsManager;->userSettingPref:Landroid/content/SharedPreferences;

    invoke-interface {v1}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v1

    iget-object p0, p0, Lcom/facebook/UserSettingsManager$UserSetting;->key:Ljava/lang/String;

    .line 198
    invoke-virtual {v0}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-interface {v1, p0, v0}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    move-result-object p0

    .line 199
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 200
    invoke-static {}, Lcom/facebook/UserSettingsManager;->logIfSDKSettingsChanged()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    .line 202
    sget-object v0, Lcom/facebook/UserSettingsManager;->TAG:Ljava/lang/String;

    invoke-static {v0, p0}, Lcom/facebook/internal/Utility;->logd(Ljava/lang/String;Ljava/lang/Exception;)V

    :goto_0
    return-void
.end method
