.class final Lcom/facebook/appevents/AppEventsLoggerImpl$1;
.super Ljava/lang/Object;
.source "AppEventsLoggerImpl.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/facebook/appevents/AppEventsLoggerImpl;->initializeLib(Landroid/content/Context;Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$context:Landroid/content/Context;

.field final synthetic val$logger:Lcom/facebook/appevents/AppEventsLoggerImpl;


# direct methods
.method constructor <init>(Landroid/content/Context;Lcom/facebook/appevents/AppEventsLoggerImpl;)V
    .locals 0

    .line 130
    iput-object p1, p0, Lcom/facebook/appevents/AppEventsLoggerImpl$1;->val$context:Landroid/content/Context;

    iput-object p2, p0, Lcom/facebook/appevents/AppEventsLoggerImpl$1;->val$logger:Lcom/facebook/appevents/AppEventsLoggerImpl;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 13

    invoke-static {p0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 133
    :cond_0
    :try_start_0
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "com.facebook.core.Core"

    const-string v2, "com.facebook.login.Login"

    const-string v3, "com.facebook.share.Share"

    const-string v4, "com.facebook.places.Places"

    const-string v5, "com.facebook.messenger.Messenger"

    const-string v6, "com.facebook.applinks.AppLinks"

    const-string v7, "com.facebook.marketing.Marketing"

    const-string v8, "com.facebook.gamingservices.GamingServices"

    const-string v9, "com.facebook.all.All"

    const-string v10, "com.android.billingclient.api.BillingClient"

    const-string v11, "com.android.vending.billing.IInAppBillingService"

    .line 135
    filled-new-array/range {v1 .. v11}, [Ljava/lang/String;

    move-result-object v1

    const-string v2, "core_lib_included"

    const-string v3, "login_lib_included"

    const-string v4, "share_lib_included"

    const-string v5, "places_lib_included"

    const-string v6, "messenger_lib_included"

    const-string v7, "applinks_lib_included"

    const-string v8, "marketing_lib_included"

    const-string v9, "gamingservices_lib_included"

    const-string v10, "all_lib_included"

    const-string v11, "billing_client_lib_included"

    const-string v12, "billing_service_lib_included"

    .line 150
    filled-new-array/range {v2 .. v12}, [Ljava/lang/String;

    move-result-object v2

    .line 166
    array-length v3, v1

    array-length v4, v2

    if-ne v3, v4, :cond_3

    const/4 v3, 0x0

    const/4 v4, 0x0

    const/4 v5, 0x0

    .line 172
    :goto_0
    array-length v6, v1

    if-ge v4, v6, :cond_1

    .line 173
    aget-object v6, v1, v4

    .line 174
    aget-object v7, v2, v4
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_1

    .line 177
    :try_start_1
    invoke-static {v6}, Ljava/lang/Class;->forName(Ljava/lang/String;)Ljava/lang/Class;

    const/4 v6, 0x1

    .line 178
    invoke-virtual {v0, v7, v6}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V
    :try_end_1
    .catch Ljava/lang/ClassNotFoundException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_1

    shl-int/2addr v6, v4

    or-int/2addr v5, v6

    :catch_0
    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    .line 183
    :cond_1
    :try_start_2
    iget-object v1, p0, Lcom/facebook/appevents/AppEventsLoggerImpl$1;->val$context:Landroid/content/Context;

    const-string v2, "com.facebook.sdk.appEventPreferences"

    invoke-virtual {v1, v2, v3}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v1

    const-string v2, "kitsBitmask"

    .line 186
    invoke-interface {v1, v2, v3}, Landroid/content/SharedPreferences;->getInt(Ljava/lang/String;I)I

    move-result v2

    if-eq v2, v5, :cond_2

    .line 188
    invoke-interface {v1}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v1

    const-string v2, "kitsBitmask"

    invoke-interface {v1, v2, v5}, Landroid/content/SharedPreferences$Editor;->putInt(Ljava/lang/String;I)Landroid/content/SharedPreferences$Editor;

    move-result-object v1

    invoke-interface {v1}, Landroid/content/SharedPreferences$Editor;->apply()V

    .line 189
    iget-object v1, p0, Lcom/facebook/appevents/AppEventsLoggerImpl$1;->val$logger:Lcom/facebook/appevents/AppEventsLoggerImpl;

    const-string v2, "fb_sdk_initialize"

    const/4 v3, 0x0

    invoke-virtual {v1, v2, v3, v0}, Lcom/facebook/appevents/AppEventsLoggerImpl;->logEventImplicitly(Ljava/lang/String;Ljava/lang/Double;Landroid/os/Bundle;)V

    :cond_2
    return-void

    .line 167
    :cond_3
    new-instance v0, Lcom/facebook/FacebookException;

    const-string v1, "Number of class names and key names should match"

    invoke-direct {v0, v1}, Lcom/facebook/FacebookException;-><init>(Ljava/lang/String;)V

    throw v0
    :try_end_2
    .catch Ljava/lang/Throwable; {:try_start_2 .. :try_end_2} :catch_1

    :catch_1
    move-exception v0

    .line 191
    invoke-static {v0, p0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method
