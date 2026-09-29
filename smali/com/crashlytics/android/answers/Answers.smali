.class public Lcom/crashlytics/android/answers/Answers;
.super Lio/fabric/sdk/android/Kit;
.source "Answers.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lio/fabric/sdk/android/Kit<",
        "Ljava/lang/Boolean;",
        ">;"
    }
.end annotation


# static fields
.field static final CRASHLYTICS_API_ENDPOINT:Ljava/lang/String; = "com.crashlytics.ApiEndpoint"

.field static final FIRST_LAUNCH_INTERVAL_IN_MS:J = 0x36ee80L

.field static final PREFKEY_ANALYTICS_LAUNCHED:Ljava/lang/String; = "analytics_launched"

.field static final SESSION_ANALYTICS_FILE_EXTENSION:Ljava/lang/String; = ".tap"

.field static final SESSION_ANALYTICS_FILE_NAME:Ljava/lang/String; = "session_analytics.tap"

.field private static final SESSION_ANALYTICS_TO_SEND_DIR:Ljava/lang/String; = "session_analytics_to_send"

.field public static final TAG:Ljava/lang/String; = "Answers"


# instance fields
.field private installedAt:J

.field private preferenceStore:Lio/fabric/sdk/android/services/persistence/PreferenceStore;

.field sessionAnalyticsManager:Lcom/crashlytics/android/answers/SessionAnalyticsManager;

.field private versionCode:Ljava/lang/String;

.field private versionName:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 31
    invoke-direct {p0}, Lio/fabric/sdk/android/Kit;-><init>()V

    return-void
.end method

.method public static getInstance()Lcom/crashlytics/android/answers/Answers;
    .locals 1

    .line 55
    const-class v0, Lcom/crashlytics/android/answers/Answers;

    invoke-static {v0}, Lio/fabric/sdk/android/Fabric;->getKit(Ljava/lang/Class;)Lio/fabric/sdk/android/Kit;

    move-result-object v0

    check-cast v0, Lcom/crashlytics/android/answers/Answers;

    return-object v0
.end method

.method private initializeSessionAnalytics(Landroid/content/Context;)V
    .locals 18
    .annotation build Landroid/annotation/SuppressLint;
        value = {
            "CommitPrefEdits"
        }
    .end annotation

    .annotation build Landroid/annotation/TargetApi;
        value = 0xe
    .end annotation

    move-object/from16 v1, p0

    move-object/from16 v2, p1

    .line 185
    :try_start_0
    new-instance v0, Lcom/crashlytics/android/answers/SessionEventTransform;

    invoke-direct {v0}, Lcom/crashlytics/android/answers/SessionEventTransform;-><init>()V

    .line 186
    new-instance v3, Lio/fabric/sdk/android/services/common/SystemCurrentTimeProvider;

    invoke-direct {v3}, Lio/fabric/sdk/android/services/common/SystemCurrentTimeProvider;-><init>()V

    .line 187
    new-instance v4, Lio/fabric/sdk/android/services/events/GZIPQueueFileEventStorage;

    invoke-virtual/range {p0 .. p0}, Lcom/crashlytics/android/answers/Answers;->getContext()Landroid/content/Context;

    move-result-object v5

    invoke-virtual/range {p0 .. p0}, Lcom/crashlytics/android/answers/Answers;->getSdkDirectory()Ljava/io/File;

    move-result-object v6

    const-string v7, "session_analytics.tap"

    const-string v8, "session_analytics_to_send"

    invoke-direct {v4, v5, v6, v7, v8}, Lio/fabric/sdk/android/services/events/GZIPQueueFileEventStorage;-><init>(Landroid/content/Context;Ljava/io/File;Ljava/lang/String;Ljava/lang/String;)V

    .line 190
    new-instance v5, Lcom/crashlytics/android/answers/SessionAnalyticsFilesManager;

    invoke-direct {v5, v2, v0, v3, v4}, Lcom/crashlytics/android/answers/SessionAnalyticsFilesManager;-><init>(Landroid/content/Context;Lcom/crashlytics/android/answers/SessionEventTransform;Lio/fabric/sdk/android/services/common/CurrentTimeProvider;Lio/fabric/sdk/android/services/events/EventsStorage;)V

    .line 193
    invoke-virtual/range {p0 .. p0}, Lcom/crashlytics/android/answers/Answers;->getIdManager()Lio/fabric/sdk/android/services/common/IdManager;

    move-result-object v0

    .line 195
    invoke-virtual {v0}, Lio/fabric/sdk/android/services/common/IdManager;->getDeviceIdentifiers()Ljava/util/Map;

    move-result-object v3

    .line 198
    invoke-virtual/range {p1 .. p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v7

    .line 199
    invoke-virtual {v0}, Lio/fabric/sdk/android/services/common/IdManager;->getAppInstallIdentifier()Ljava/lang/String;

    move-result-object v9

    .line 200
    sget-object v4, Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;->ANDROID_ID:Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;

    invoke-interface {v3, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    move-object v10, v4

    check-cast v10, Ljava/lang/String;

    .line 202
    sget-object v4, Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;->ANDROID_ADVERTISING_ID:Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;

    invoke-interface {v3, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    move-object v11, v4

    check-cast v11, Ljava/lang/String;

    .line 204
    sget-object v4, Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;->FONT_TOKEN:Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;

    invoke-interface {v3, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    move-object v12, v3

    check-cast v12, Ljava/lang/String;

    .line 206
    invoke-static/range {p1 .. p1}, Lio/fabric/sdk/android/services/common/CommonUtils;->resolveBuildId(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v13

    .line 207
    invoke-virtual {v0}, Lio/fabric/sdk/android/services/common/IdManager;->getOsVersionString()Ljava/lang/String;

    move-result-object v14

    .line 208
    invoke-virtual {v0}, Lio/fabric/sdk/android/services/common/IdManager;->getModelName()Ljava/lang/String;

    move-result-object v15

    .line 209
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v8

    .line 211
    new-instance v0, Lcom/crashlytics/android/answers/SessionEventMetadata;

    iget-object v3, v1, Lcom/crashlytics/android/answers/Answers;->versionCode:Ljava/lang/String;

    iget-object v4, v1, Lcom/crashlytics/android/answers/Answers;->versionName:Ljava/lang/String;

    move-object v6, v0

    move-object/from16 v16, v3

    move-object/from16 v17, v4

    invoke-direct/range {v6 .. v17}, Lcom/crashlytics/android/answers/SessionEventMetadata;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 218
    invoke-virtual/range {p0 .. p0}, Lcom/crashlytics/android/answers/Answers;->getContext()Landroid/content/Context;

    move-result-object v3

    invoke-virtual {v3}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object v3

    check-cast v3, Landroid/app/Application;

    if-eqz v3, :cond_0

    .line 219
    sget v4, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v6, 0xe

    if-lt v4, v6, :cond_0

    .line 221
    new-instance v4, Lio/fabric/sdk/android/services/network/DefaultHttpRequestFactory;

    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v6

    invoke-direct {v4, v6}, Lio/fabric/sdk/android/services/network/DefaultHttpRequestFactory;-><init>(Lio/fabric/sdk/android/Logger;)V

    invoke-static {v3, v0, v5, v4}, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;->build(Landroid/app/Application;Lcom/crashlytics/android/answers/SessionEventMetadata;Lcom/crashlytics/android/answers/SessionAnalyticsFilesManager;Lio/fabric/sdk/android/services/network/HttpRequestFactory;)Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;

    move-result-object v0

    iput-object v0, v1, Lcom/crashlytics/android/answers/Answers;->sessionAnalyticsManager:Lcom/crashlytics/android/answers/SessionAnalyticsManager;

    goto :goto_0

    .line 225
    :cond_0
    new-instance v3, Lio/fabric/sdk/android/services/network/DefaultHttpRequestFactory;

    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v4

    invoke-direct {v3, v4}, Lio/fabric/sdk/android/services/network/DefaultHttpRequestFactory;-><init>(Lio/fabric/sdk/android/Logger;)V

    invoke-static {v2, v0, v5, v3}, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->build(Landroid/content/Context;Lcom/crashlytics/android/answers/SessionEventMetadata;Lcom/crashlytics/android/answers/SessionAnalyticsFilesManager;Lio/fabric/sdk/android/services/network/HttpRequestFactory;)Lcom/crashlytics/android/answers/SessionAnalyticsManager;

    move-result-object v0

    iput-object v0, v1, Lcom/crashlytics/android/answers/Answers;->sessionAnalyticsManager:Lcom/crashlytics/android/answers/SessionAnalyticsManager;

    .line 230
    :goto_0
    iget-wide v3, v1, Lcom/crashlytics/android/answers/Answers;->installedAt:J

    invoke-virtual {v1, v3, v4}, Lcom/crashlytics/android/answers/Answers;->isFirstLaunch(J)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 231
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v0

    const-string v3, "Answers"

    const-string v4, "First launch"

    invoke-interface {v0, v3, v4}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 232
    iget-object v0, v1, Lcom/crashlytics/android/answers/Answers;->sessionAnalyticsManager:Lcom/crashlytics/android/answers/SessionAnalyticsManager;

    invoke-virtual {v0}, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->onInstall()V

    .line 233
    iget-object v0, v1, Lcom/crashlytics/android/answers/Answers;->preferenceStore:Lio/fabric/sdk/android/services/persistence/PreferenceStore;

    iget-object v3, v1, Lcom/crashlytics/android/answers/Answers;->preferenceStore:Lio/fabric/sdk/android/services/persistence/PreferenceStore;

    invoke-interface {v3}, Lio/fabric/sdk/android/services/persistence/PreferenceStore;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v3

    const-string v4, "analytics_launched"

    const/4 v5, 0x1

    invoke-interface {v3, v4, v5}, Landroid/content/SharedPreferences$Editor;->putBoolean(Ljava/lang/String;Z)Landroid/content/SharedPreferences$Editor;

    move-result-object v3

    invoke-interface {v0, v3}, Lio/fabric/sdk/android/services/persistence/PreferenceStore;->save(Landroid/content/SharedPreferences$Editor;)Z
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v0

    const-string v3, "Crashlytics failed to initialize session analytics."

    .line 237
    invoke-static {v2, v3, v0}, Lio/fabric/sdk/android/services/common/CommonUtils;->logControlledError(Landroid/content/Context;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_1
    :goto_1
    return-void
.end method


# virtual methods
.method protected doInBackground()Ljava/lang/Boolean;
    .locals 5

    .line 146
    invoke-virtual {p0}, Lcom/crashlytics/android/answers/Answers;->getContext()Landroid/content/Context;

    move-result-object v0

    .line 148
    invoke-direct {p0, v0}, Lcom/crashlytics/android/answers/Answers;->initializeSessionAnalytics(Landroid/content/Context;)V

    const/4 v1, 0x0

    .line 150
    :try_start_0
    invoke-static {}, Lio/fabric/sdk/android/services/settings/Settings;->getInstance()Lio/fabric/sdk/android/services/settings/Settings;

    move-result-object v2

    invoke-virtual {v2}, Lio/fabric/sdk/android/services/settings/Settings;->awaitSettingsData()Lio/fabric/sdk/android/services/settings/SettingsData;

    move-result-object v2

    if-nez v2, :cond_0

    .line 152
    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    return-object v0

    .line 155
    :cond_0
    iget-object v3, v2, Lio/fabric/sdk/android/services/settings/SettingsData;->featuresData:Lio/fabric/sdk/android/services/settings/FeaturesSettingsData;

    iget-boolean v3, v3, Lio/fabric/sdk/android/services/settings/FeaturesSettingsData;->collectAnalytics:Z

    if-eqz v3, :cond_1

    .line 156
    iget-object v0, p0, Lcom/crashlytics/android/answers/Answers;->sessionAnalyticsManager:Lcom/crashlytics/android/answers/SessionAnalyticsManager;

    iget-object v2, v2, Lio/fabric/sdk/android/services/settings/SettingsData;->analyticsSettingsData:Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;

    invoke-virtual {p0}, Lcom/crashlytics/android/answers/Answers;->getOverridenSpiEndpoint()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->setAnalyticsSettingsData(Lio/fabric/sdk/android/services/settings/AnalyticsSettingsData;Ljava/lang/String;)V

    const/4 v0, 0x1

    .line 158
    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    return-object v0

    :cond_1
    const-string v2, "Disabling analytics collection based on settings flag value."

    .line 160
    invoke-static {v0, v2}, Lio/fabric/sdk/android/services/common/CommonUtils;->logControlled(Landroid/content/Context;Ljava/lang/String;)V

    .line 162
    iget-object v0, p0, Lcom/crashlytics/android/answers/Answers;->sessionAnalyticsManager:Lcom/crashlytics/android/answers/SessionAnalyticsManager;

    invoke-virtual {v0}, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->disable()V

    .line 163
    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    .line 166
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v2

    const-string v3, "Answers"

    const-string v4, "Error dealing with settings"

    invoke-interface {v2, v3, v4, v0}, Lio/fabric/sdk/android/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 168
    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    return-object v0
.end method

.method protected bridge synthetic doInBackground()Ljava/lang/Object;
    .locals 1

    .line 31
    invoke-virtual {p0}, Lcom/crashlytics/android/answers/Answers;->doInBackground()Ljava/lang/Boolean;

    move-result-object v0

    return-object v0
.end method

.method getAnalyticsLaunched()Z
    .locals 3

    .line 249
    iget-object v0, p0, Lcom/crashlytics/android/answers/Answers;->preferenceStore:Lio/fabric/sdk/android/services/persistence/PreferenceStore;

    invoke-interface {v0}, Lio/fabric/sdk/android/services/persistence/PreferenceStore;->get()Landroid/content/SharedPreferences;

    move-result-object v0

    const-string v1, "analytics_launched"

    const/4 v2, 0x0

    invoke-interface {v0, v1, v2}, Landroid/content/SharedPreferences;->getBoolean(Ljava/lang/String;Z)Z

    move-result v0

    return v0
.end method

.method public getIdentifier()Ljava/lang/String;
    .locals 1

    const-string v0, "com.crashlytics.sdk.android:answers"

    return-object v0
.end method

.method getOverridenSpiEndpoint()Ljava/lang/String;
    .locals 2

    .line 245
    invoke-virtual {p0}, Lcom/crashlytics/android/answers/Answers;->getContext()Landroid/content/Context;

    move-result-object v0

    const-string v1, "com.crashlytics.ApiEndpoint"

    invoke-static {v0, v1}, Lio/fabric/sdk/android/services/common/CommonUtils;->getStringsFileValue(Landroid/content/Context;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method getSdkDirectory()Ljava/io/File;
    .locals 1

    .line 277
    new-instance v0, Lio/fabric/sdk/android/services/persistence/FileStoreImpl;

    invoke-direct {v0, p0}, Lio/fabric/sdk/android/services/persistence/FileStoreImpl;-><init>(Lio/fabric/sdk/android/Kit;)V

    invoke-virtual {v0}, Lio/fabric/sdk/android/services/persistence/FileStoreImpl;->getFilesDir()Ljava/io/File;

    move-result-object v0

    return-object v0
.end method

.method public getVersion()Ljava/lang/String;
    .locals 1

    const-string v0, "1.2.2.56"

    return-object v0
.end method

.method installedRecently(J)Z
    .locals 3

    .line 272
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    sub-long/2addr v0, p1

    const-wide/32 p1, 0x36ee80

    cmp-long v2, v0, p1

    if-gez v2, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method isFirstLaunch(J)Z
    .locals 1

    .line 262
    invoke-virtual {p0}, Lcom/crashlytics/android/answers/Answers;->getAnalyticsLaunched()Z

    move-result v0

    if-nez v0, :cond_0

    invoke-virtual {p0, p1, p2}, Lcom/crashlytics/android/answers/Answers;->installedRecently(J)Z

    move-result p1

    if-eqz p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method public logEvent(Ljava/lang/String;)V
    .locals 1

    .line 70
    new-instance v0, Lcom/crashlytics/android/answers/EventAttributes;

    invoke-direct {v0}, Lcom/crashlytics/android/answers/EventAttributes;-><init>()V

    invoke-virtual {p0, p1, v0}, Lcom/crashlytics/android/answers/Answers;->logEvent(Ljava/lang/String;Lcom/crashlytics/android/answers/EventAttributes;)V

    return-void
.end method

.method public logEvent(Ljava/lang/String;Lcom/crashlytics/android/answers/EventAttributes;)V
    .locals 1

    if-eqz p1, :cond_2

    if-eqz p2, :cond_1

    .line 97
    iget-object v0, p0, Lcom/crashlytics/android/answers/Answers;->sessionAnalyticsManager:Lcom/crashlytics/android/answers/SessionAnalyticsManager;

    if-eqz v0, :cond_0

    .line 98
    iget-object v0, p0, Lcom/crashlytics/android/answers/Answers;->sessionAnalyticsManager:Lcom/crashlytics/android/answers/SessionAnalyticsManager;

    iget-object p2, p2, Lcom/crashlytics/android/answers/EventAttributes;->attributes:Ljava/util/Map;

    invoke-virtual {v0, p1, p2}, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->onCustom(Ljava/lang/String;Ljava/util/Map;)V

    :cond_0
    return-void

    .line 95
    :cond_1
    new-instance p1, Ljava/lang/NullPointerException;

    const-string p2, "attributes must not be null"

    invoke-direct {p1, p2}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 92
    :cond_2
    new-instance p1, Ljava/lang/NullPointerException;

    const-string p2, "eventName must not be null"

    invoke-direct {p1, p2}, Ljava/lang/NullPointerException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public onException(Lio/fabric/sdk/android/services/common/Crash$FatalException;)V
    .locals 1

    .line 109
    iget-object v0, p0, Lcom/crashlytics/android/answers/Answers;->sessionAnalyticsManager:Lcom/crashlytics/android/answers/SessionAnalyticsManager;

    if-eqz v0, :cond_0

    .line 110
    iget-object v0, p0, Lcom/crashlytics/android/answers/Answers;->sessionAnalyticsManager:Lcom/crashlytics/android/answers/SessionAnalyticsManager;

    invoke-virtual {p1}, Lio/fabric/sdk/android/services/common/Crash$FatalException;->getSessionId()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->onCrash(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public onException(Lio/fabric/sdk/android/services/common/Crash$LoggedException;)V
    .locals 1

    .line 103
    iget-object v0, p0, Lcom/crashlytics/android/answers/Answers;->sessionAnalyticsManager:Lcom/crashlytics/android/answers/SessionAnalyticsManager;

    if-eqz v0, :cond_0

    .line 104
    iget-object v0, p0, Lcom/crashlytics/android/answers/Answers;->sessionAnalyticsManager:Lcom/crashlytics/android/answers/SessionAnalyticsManager;

    invoke-virtual {p1}, Lio/fabric/sdk/android/services/common/Crash$LoggedException;->getSessionId()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Lcom/crashlytics/android/answers/SessionAnalyticsManager;->onError(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method protected onPreExecute()Z
    .locals 5
    .annotation build Landroid/annotation/SuppressLint;
        value = {
            "NewApi"
        }
    .end annotation

    const/4 v0, 0x0

    .line 118
    :try_start_0
    new-instance v1, Lio/fabric/sdk/android/services/persistence/PreferenceStoreImpl;

    invoke-direct {v1, p0}, Lio/fabric/sdk/android/services/persistence/PreferenceStoreImpl;-><init>(Lio/fabric/sdk/android/Kit;)V

    iput-object v1, p0, Lcom/crashlytics/android/answers/Answers;->preferenceStore:Lio/fabric/sdk/android/services/persistence/PreferenceStore;

    .line 120
    invoke-virtual {p0}, Lcom/crashlytics/android/answers/Answers;->getContext()Landroid/content/Context;

    move-result-object v1

    .line 121
    invoke-virtual {v1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v2

    .line 122
    invoke-virtual {v1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v3

    .line 124
    invoke-virtual {v2, v3, v0}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v2

    .line 126
    iget v3, v2, Landroid/content/pm/PackageInfo;->versionCode:I

    invoke-static {v3}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v3

    iput-object v3, p0, Lcom/crashlytics/android/answers/Answers;->versionCode:Ljava/lang/String;

    .line 127
    iget-object v3, v2, Landroid/content/pm/PackageInfo;->versionName:Ljava/lang/String;

    if-nez v3, :cond_0

    const-string v3, "0.0"

    goto :goto_0

    :cond_0
    iget-object v3, v2, Landroid/content/pm/PackageInfo;->versionName:Ljava/lang/String;

    :goto_0
    iput-object v3, p0, Lcom/crashlytics/android/answers/Answers;->versionName:Ljava/lang/String;

    .line 130
    sget v3, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v4, 0x9

    if-lt v3, v4, :cond_1

    .line 131
    iget-wide v1, v2, Landroid/content/pm/PackageInfo;->firstInstallTime:J

    iput-wide v1, p0, Lcom/crashlytics/android/answers/Answers;->installedAt:J

    goto :goto_1

    .line 133
    :cond_1
    invoke-virtual {v1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v2

    invoke-virtual {v1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v2, v1, v0}, Landroid/content/pm/PackageManager;->getApplicationInfo(Ljava/lang/String;I)Landroid/content/pm/ApplicationInfo;

    move-result-object v1

    .line 135
    new-instance v2, Ljava/io/File;

    iget-object v1, v1, Landroid/content/pm/ApplicationInfo;->sourceDir:Ljava/lang/String;

    invoke-direct {v2, v1}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    invoke-virtual {v2}, Ljava/io/File;->lastModified()J

    move-result-wide v1

    iput-wide v1, p0, Lcom/crashlytics/android/answers/Answers;->installedAt:J
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    :goto_1
    const/4 v0, 0x1

    return v0

    :catch_0
    move-exception v1

    .line 139
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v2

    const-string v3, "Answers"

    const-string v4, "Error setting up app properties"

    invoke-interface {v2, v3, v4, v1}, Lio/fabric/sdk/android/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    return v0
.end method
