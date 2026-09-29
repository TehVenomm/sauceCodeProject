.class final Lcom/facebook/appevents/AppEventsLoggerImpl$3;
.super Ljava/lang/Object;
.source "AppEventsLoggerImpl.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/facebook/appevents/AppEventsLoggerImpl;->updateUserProperties(Landroid/os/Bundle;Ljava/lang/String;Lcom/facebook/GraphRequest$Callback;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$applicationID:Ljava/lang/String;

.field final synthetic val$callback:Lcom/facebook/GraphRequest$Callback;

.field final synthetic val$parameters:Landroid/os/Bundle;


# direct methods
.method constructor <init>(Landroid/os/Bundle;Ljava/lang/String;Lcom/facebook/GraphRequest$Callback;)V
    .locals 0

    .line 476
    iput-object p1, p0, Lcom/facebook/appevents/AppEventsLoggerImpl$3;->val$parameters:Landroid/os/Bundle;

    iput-object p2, p0, Lcom/facebook/appevents/AppEventsLoggerImpl$3;->val$applicationID:Ljava/lang/String;

    iput-object p3, p0, Lcom/facebook/appevents/AppEventsLoggerImpl$3;->val$callback:Lcom/facebook/GraphRequest$Callback;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 9

    invoke-static {p0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 479
    :cond_0
    :try_start_0
    invoke-static {}, Lcom/facebook/appevents/AnalyticsUserIDStore;->getUserID()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_3

    .line 480
    invoke-virtual {v0}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_1

    goto :goto_0

    .line 488
    :cond_1
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "user_unique_id"

    .line 489
    invoke-virtual {v1, v2, v0}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "custom_data"

    .line 490
    iget-object v2, p0, Lcom/facebook/appevents/AppEventsLoggerImpl$3;->val$parameters:Landroid/os/Bundle;

    invoke-virtual {v1, v0, v2}, Landroid/os/Bundle;->putBundle(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 494
    invoke-static {}, Lcom/facebook/FacebookSdk;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    .line 493
    invoke-static {v0}, Lcom/facebook/internal/AttributionIdentifiers;->getAttributionIdentifiers(Landroid/content/Context;)Lcom/facebook/internal/AttributionIdentifiers;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 495
    invoke-virtual {v0}, Lcom/facebook/internal/AttributionIdentifiers;->getAndroidAdvertiserId()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_2

    const-string v2, "advertiser_id"

    .line 498
    invoke-virtual {v0}, Lcom/facebook/internal/AttributionIdentifiers;->getAndroidAdvertiserId()Ljava/lang/String;

    move-result-object v0

    .line 496
    invoke-virtual {v1, v2, v0}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 501
    :cond_2
    new-instance v4, Landroid/os/Bundle;

    invoke-direct {v4}, Landroid/os/Bundle;-><init>()V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_1

    .line 503
    :try_start_1
    invoke-static {v1}, Lcom/facebook/internal/BundleJSONConverter;->convertToJSON(Landroid/os/Bundle;)Lorg/json/JSONObject;

    move-result-object v0

    .line 504
    new-instance v1, Lorg/json/JSONArray;

    invoke-direct {v1}, Lorg/json/JSONArray;-><init>()V

    .line 505
    invoke-virtual {v1, v0}, Lorg/json/JSONArray;->put(Ljava/lang/Object;)Lorg/json/JSONArray;

    const-string v0, "data"

    .line 508
    invoke-virtual {v1}, Lorg/json/JSONArray;->toString()Ljava/lang/String;

    move-result-object v1

    .line 507
    invoke-virtual {v4, v0, v1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_1

    .line 513
    :try_start_2
    new-instance v0, Lcom/facebook/GraphRequest;

    .line 514
    invoke-static {}, Lcom/facebook/AccessToken;->getCurrentAccessToken()Lcom/facebook/AccessToken;

    move-result-object v2

    sget-object v1, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v3, "%s/user_properties"

    const/4 v7, 0x1

    new-array v5, v7, [Ljava/lang/Object;

    const/4 v6, 0x0

    iget-object v8, p0, Lcom/facebook/appevents/AppEventsLoggerImpl$3;->val$applicationID:Ljava/lang/String;

    aput-object v8, v5, v6

    .line 515
    invoke-static {v1, v3, v5}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    sget-object v5, Lcom/facebook/HttpMethod;->POST:Lcom/facebook/HttpMethod;

    iget-object v6, p0, Lcom/facebook/appevents/AppEventsLoggerImpl$3;->val$callback:Lcom/facebook/GraphRequest$Callback;

    move-object v1, v0

    invoke-direct/range {v1 .. v6}, Lcom/facebook/GraphRequest;-><init>(Lcom/facebook/AccessToken;Ljava/lang/String;Landroid/os/Bundle;Lcom/facebook/HttpMethod;Lcom/facebook/GraphRequest$Callback;)V

    .line 519
    invoke-virtual {v0, v7}, Lcom/facebook/GraphRequest;->setSkipClientToken(Z)V

    .line 520
    invoke-virtual {v0}, Lcom/facebook/GraphRequest;->executeAsync()Lcom/facebook/GraphRequestAsyncTask;

    return-void

    :catch_0
    move-exception v0

    .line 510
    new-instance v1, Lcom/facebook/FacebookException;

    const-string v2, "Failed to construct request"

    invoke-direct {v1, v2, v0}, Lcom/facebook/FacebookException;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v1

    .line 481
    :cond_3
    :goto_0
    sget-object v0, Lcom/facebook/LoggingBehavior;->APP_EVENTS:Lcom/facebook/LoggingBehavior;

    .line 483
    invoke-static {}, Lcom/facebook/appevents/AppEventsLoggerImpl;->access$000()Ljava/lang/String;

    move-result-object v1

    const-string v2, "AppEventsLogger userID cannot be null or empty"

    .line 481
    invoke-static {v0, v1, v2}, Lcom/facebook/internal/Logger;->log(Lcom/facebook/LoggingBehavior;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_2
    .catch Ljava/lang/Throwable; {:try_start_2 .. :try_end_2} :catch_1

    return-void

    :catch_1
    move-exception v0

    .line 521
    invoke-static {v0, p0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method
