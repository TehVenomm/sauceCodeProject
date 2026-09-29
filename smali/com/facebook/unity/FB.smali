.class public Lcom/facebook/unity/FB;
.super Ljava/lang/Object;
.source "FB.java"


# static fields
.field static final FB_UNITY_OBJECT:Ljava/lang/String; = "UnityFacebookSDKPlugin"

.field static ShareDialogMode:Lcom/facebook/share/widget/ShareDialog$Mode; = null

.field static final TAG:Ljava/lang/String; = "com.facebook.unity.FB"

.field private static activateAppCalled:Ljava/util/concurrent/atomic/AtomicBoolean;

.field private static appEventsLogger:Lcom/facebook/appevents/AppEventsLogger;

.field private static clearedIntent:Landroid/content/Intent;

.field private static intent:Landroid/content/Intent;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 65
    new-instance v0, Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-direct {v0}, Ljava/util/concurrent/atomic/AtomicBoolean;-><init>()V

    sput-object v0, Lcom/facebook/unity/FB;->activateAppCalled:Ljava/util/concurrent/atomic/AtomicBoolean;

    .line 66
    sget-object v0, Lcom/facebook/share/widget/ShareDialog$Mode;->AUTOMATIC:Lcom/facebook/share/widget/ShareDialog$Mode;

    sput-object v0, Lcom/facebook/unity/FB;->ShareDialogMode:Lcom/facebook/share/widget/ShareDialog$Mode;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 58
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static ActivateApp()V
    .locals 1
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 447
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v0

    invoke-static {v0}, Lcom/facebook/appevents/AppEventsLogger;->activateApp(Landroid/content/Context;)V

    return-void
.end method

.method private static ActivateApp(Ljava/lang/String;)V
    .locals 3

    .line 558
    sget-object v0, Lcom/facebook/unity/FB;->activateAppCalled:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x0

    const/4 v2, 0x1

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/atomic/AtomicBoolean;->compareAndSet(ZZ)Z

    move-result v0

    if-nez v0, :cond_0

    .line 559
    sget-object p0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    const-string v0, "Activite app only needs to be called once"

    invoke-static {p0, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    .line 562
    :cond_0
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v0

    if-eqz p0, :cond_1

    .line 565
    invoke-virtual {v0}, Landroid/app/Activity;->getApplication()Landroid/app/Application;

    move-result-object v1

    .line 564
    invoke-static {v1, p0}, Lcom/facebook/appevents/AppEventsLogger;->activateApp(Landroid/app/Application;Ljava/lang/String;)V

    goto :goto_0

    .line 568
    :cond_1
    invoke-virtual {v0}, Landroid/app/Activity;->getApplication()Landroid/app/Application;

    move-result-object p0

    invoke-static {p0}, Lcom/facebook/appevents/AppEventsLogger;->activateApp(Landroid/app/Application;)V

    .line 573
    :goto_0
    new-instance p0, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-direct {p0, v1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    new-instance v1, Lcom/facebook/unity/FB$6;

    invoke-direct {v1, v0}, Lcom/facebook/unity/FB$6;-><init>(Landroid/app/Activity;)V

    invoke-virtual {p0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method public static AppRequest(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 159
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "AppRequest("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 160
    new-instance v0, Landroid/content/Intent;

    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v1

    const-class v2, Lcom/facebook/unity/FBUnityGameRequestActivity;

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    .line 161
    invoke-static {p0}, Lcom/facebook/unity/UnityParams;->parse(Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object p0

    .line 162
    invoke-virtual {p0}, Lcom/facebook/unity/UnityParams;->getStringParams()Landroid/os/Bundle;

    move-result-object p0

    const-string v1, "game_request_params"

    .line 163
    invoke-virtual {v0, v1, p0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Bundle;)Landroid/content/Intent;

    .line 164
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object p0

    invoke-virtual {p0, v0}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method public static ClearAppLink()V
    .locals 2
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 384
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    const-string v1, "ClearAppLink"

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 385
    sget-object v0, Lcom/facebook/unity/FB;->intent:Landroid/content/Intent;

    sput-object v0, Lcom/facebook/unity/FB;->clearedIntent:Landroid/content/Intent;

    return-void
.end method

.method public static FeedShare(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 200
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "FeedShare("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 201
    invoke-static {p0}, Lcom/facebook/unity/UnityParams;->parse(Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object p0

    .line 202
    invoke-virtual {p0}, Lcom/facebook/unity/UnityParams;->getStringParams()Landroid/os/Bundle;

    move-result-object p0

    .line 203
    new-instance v0, Landroid/content/Intent;

    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v1

    const-class v2, Lcom/facebook/unity/FBUnityDialogsActivity;

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "dialog_type"

    .line 204
    sget-object v2, Lcom/facebook/share/widget/ShareDialog$Mode;->FEED:Lcom/facebook/share/widget/ShareDialog$Mode;

    invoke-virtual {v0, v1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    const-string v1, "feed_dialog_params"

    .line 205
    invoke-virtual {v0, v1, p0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Bundle;)Landroid/content/Intent;

    .line 206
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object p0

    invoke-virtual {p0, v0}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method public static FetchDeferredAppLinkData(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    const-string v0, "FetchDeferredAppLinkData"

    .line 323
    invoke-static {v0, p0}, Lcom/facebook/unity/FB;->LogMethodCall(Ljava/lang/String;Ljava/lang/String;)V

    .line 325
    invoke-static {p0}, Lcom/facebook/unity/UnityParams;->parse(Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object p0

    .line 326
    new-instance v0, Lcom/facebook/unity/UnityMessage;

    const-string v1, "OnFetchDeferredAppLinkComplete"

    invoke-direct {v0, v1}, Lcom/facebook/unity/UnityMessage;-><init>(Ljava/lang/String;)V

    const-string v1, "callback_id"

    .line 327
    invoke-virtual {p0, v1}, Lcom/facebook/unity/UnityParams;->hasString(Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v1

    if-eqz v1, :cond_0

    const-string v1, "callback_id"

    const-string v2, "callback_id"

    .line 328
    invoke-virtual {p0, v2}, Lcom/facebook/unity/UnityParams;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, v1, p0}, Lcom/facebook/unity/UnityMessage;->put(Ljava/lang/String;Ljava/io/Serializable;)Lcom/facebook/unity/UnityMessage;

    .line 332
    :cond_0
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object p0

    new-instance v1, Lcom/facebook/unity/FB$2;

    invoke-direct {v1, v0}, Lcom/facebook/unity/FB$2;-><init>(Lcom/facebook/unity/UnityMessage;)V

    .line 331
    invoke-static {p0, v1}, Lcom/facebook/applinks/AppLinkData;->fetchDeferredAppLinkData(Landroid/content/Context;Lcom/facebook/applinks/AppLinkData$CompletionHandler;)V

    return-void
.end method

.method public static GameGroupCreate(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 169
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "GameGroupCreate("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 170
    invoke-static {p0}, Lcom/facebook/unity/UnityParams;->parse(Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object p0

    .line 171
    invoke-virtual {p0}, Lcom/facebook/unity/UnityParams;->getStringParams()Landroid/os/Bundle;

    move-result-object p0

    .line 172
    new-instance v0, Landroid/content/Intent;

    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v1

    const-class v2, Lcom/facebook/unity/FBUnityCreateGameGroupActivity;

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    .line 173
    sget-object v1, Lcom/facebook/unity/FBUnityCreateGameGroupActivity;->CREATE_GAME_GROUP_PARAMS:Ljava/lang/String;

    invoke-virtual {v0, v1, p0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Bundle;)Landroid/content/Intent;

    .line 174
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object p0

    invoke-virtual {p0, v0}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method public static GameGroupJoin(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 179
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "GameGroupJoin("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 180
    invoke-static {p0}, Lcom/facebook/unity/UnityParams;->parse(Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object p0

    .line 181
    invoke-virtual {p0}, Lcom/facebook/unity/UnityParams;->getStringParams()Landroid/os/Bundle;

    move-result-object p0

    .line 182
    new-instance v0, Landroid/content/Intent;

    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v1

    const-class v2, Lcom/facebook/unity/FBUnityJoinGameGroupActivity;

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    .line 183
    sget-object v1, Lcom/facebook/unity/FBUnityJoinGameGroupActivity;->JOIN_GAME_GROUP_PARAMS:Ljava/lang/String;

    invoke-virtual {v0, v1, p0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Bundle;)Landroid/content/Intent;

    .line 184
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object p0

    invoke-virtual {p0, v0}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method public static GetAppLink(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 344
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "GetAppLink("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 345
    invoke-static {p0}, Lcom/facebook/unity/UnityParams;->parse(Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object p0

    const-string v0, "OnGetAppLinkComplete"

    .line 346
    invoke-static {v0, p0}, Lcom/facebook/unity/UnityMessage;->createWithCallbackFromParams(Ljava/lang/String;Lcom/facebook/unity/UnityParams;)Lcom/facebook/unity/UnityMessage;

    move-result-object p0

    .line 351
    sget-object v0, Lcom/facebook/unity/FB;->intent:Landroid/content/Intent;

    const/4 v1, 0x1

    if-nez v0, :cond_0

    const-string v0, "did_complete"

    .line 353
    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-virtual {p0, v0, v1}, Lcom/facebook/unity/UnityMessage;->put(Ljava/lang/String;Ljava/io/Serializable;)Lcom/facebook/unity/UnityMessage;

    .line 354
    invoke-virtual {p0}, Lcom/facebook/unity/UnityMessage;->send()V

    return-void

    .line 359
    :cond_0
    sget-object v0, Lcom/facebook/unity/FB;->intent:Landroid/content/Intent;

    sget-object v2, Lcom/facebook/unity/FB;->clearedIntent:Landroid/content/Intent;

    if-ne v0, v2, :cond_1

    const-string v0, "did_complete"

    .line 360
    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-virtual {p0, v0, v1}, Lcom/facebook/unity/UnityMessage;->put(Ljava/lang/String;Ljava/io/Serializable;)Lcom/facebook/unity/UnityMessage;

    .line 361
    invoke-virtual {p0}, Lcom/facebook/unity/UnityMessage;->send()V

    return-void

    .line 366
    :cond_1
    sget-object v0, Lcom/facebook/unity/FB;->intent:Landroid/content/Intent;

    invoke-static {v0}, Lcom/facebook/applinks/AppLinkData;->createFromAlApplinkData(Landroid/content/Intent;)Lcom/facebook/applinks/AppLinkData;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 369
    invoke-static {p0, v0}, Lcom/facebook/unity/FB;->addAppLinkToMessage(Lcom/facebook/unity/UnityMessage;Lcom/facebook/applinks/AppLinkData;)V

    const-string v0, "url"

    .line 370
    sget-object v1, Lcom/facebook/unity/FB;->intent:Landroid/content/Intent;

    invoke-virtual {v1}, Landroid/content/Intent;->getDataString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p0, v0, v1}, Lcom/facebook/unity/UnityMessage;->put(Ljava/lang/String;Ljava/io/Serializable;)Lcom/facebook/unity/UnityMessage;

    goto :goto_0

    .line 371
    :cond_2
    sget-object v0, Lcom/facebook/unity/FB;->intent:Landroid/content/Intent;

    invoke-virtual {v0}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object v0

    if-eqz v0, :cond_3

    const-string v0, "url"

    .line 373
    sget-object v1, Lcom/facebook/unity/FB;->intent:Landroid/content/Intent;

    invoke-virtual {v1}, Landroid/content/Intent;->getDataString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p0, v0, v1}, Lcom/facebook/unity/UnityMessage;->put(Ljava/lang/String;Ljava/io/Serializable;)Lcom/facebook/unity/UnityMessage;

    goto :goto_0

    :cond_3
    const-string v0, "did_complete"

    .line 376
    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-virtual {p0, v0, v1}, Lcom/facebook/unity/UnityMessage;->put(Ljava/lang/String;Ljava/io/Serializable;)Lcom/facebook/unity/UnityMessage;

    .line 379
    :goto_0
    invoke-virtual {p0}, Lcom/facebook/unity/UnityMessage;->send()V

    return-void
.end method

.method public static GetSdkVersion()Ljava/lang/String;
    .locals 1
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 312
    invoke-static {}, Lcom/facebook/FacebookSdk;->getSdkVersion()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public static GetUserID()Ljava/lang/String;
    .locals 1
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 217
    invoke-static {}, Lcom/facebook/FacebookSdk;->isInitialized()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 220
    :cond_0
    invoke-static {}, Lcom/facebook/appevents/AppEventsLogger;->getUserID()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public static Init(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 81
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Init("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 82
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "couldn\'t parse init params: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p0, v0}, Lcom/facebook/unity/UnityParams;->parse(Ljava/lang/String;Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object p0

    const-string v0, "appId"

    .line 84
    invoke-virtual {p0, v0}, Lcom/facebook/unity/UnityParams;->hasString(Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "appId"

    .line 85
    invoke-virtual {p0, v0}, Lcom/facebook/unity/UnityParams;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    goto :goto_0

    .line 87
    :cond_0
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object p0

    invoke-static {p0}, Lcom/facebook/internal/Utility;->getMetadataApplicationId(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p0

    .line 90
    :goto_0
    invoke-static {p0}, Lcom/facebook/FacebookSdk;->setApplicationId(Ljava/lang/String;)V

    .line 91
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v0

    new-instance v1, Lcom/facebook/unity/FB$1;

    invoke-direct {v1, p0}, Lcom/facebook/unity/FB$1;-><init>(Ljava/lang/String;)V

    invoke-static {v0, v1}, Lcom/facebook/FacebookSdk;->sdkInitialize(Landroid/content/Context;Lcom/facebook/FacebookSdk$InitializeCallback;)V

    return-void
.end method

.method public static IsImplicitPurchaseLoggingEnabled()Z
    .locals 1
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 279
    invoke-static {}, Lcom/facebook/appevents/internal/AutomaticAnalyticsLogger;->isImplicitPurchaseLoggingEnabled()Z

    move-result v0

    return v0
.end method

.method public static LogAppEvent(Ljava/lang/String;)V
    .locals 5
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 244
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "LogAppEvent("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 245
    invoke-static {p0}, Lcom/facebook/unity/UnityParams;->parse(Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object v0

    .line 247
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "parameters"

    .line 248
    invoke-virtual {v0, v2}, Lcom/facebook/unity/UnityParams;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_0

    const-string v1, "parameters"

    .line 249
    invoke-virtual {v0, v1}, Lcom/facebook/unity/UnityParams;->getParamsObject(Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object v1

    .line 250
    invoke-virtual {v1}, Lcom/facebook/unity/UnityParams;->getStringParams()Landroid/os/Bundle;

    move-result-object v1

    :cond_0
    const-string v2, "logPurchase"

    .line 253
    invoke-virtual {v0, v2}, Lcom/facebook/unity/UnityParams;->has(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_1

    .line 254
    invoke-static {}, Lcom/facebook/unity/FB;->getAppEventsLogger()Lcom/facebook/appevents/AppEventsLogger;

    move-result-object p0

    new-instance v2, Ljava/math/BigDecimal;

    const-string v3, "logPurchase"

    .line 255
    invoke-virtual {v0, v3}, Lcom/facebook/unity/UnityParams;->getDouble(Ljava/lang/String;)D

    move-result-wide v3

    invoke-direct {v2, v3, v4}, Ljava/math/BigDecimal;-><init>(D)V

    const-string v3, "currency"

    .line 256
    invoke-virtual {v0, v3}, Lcom/facebook/unity/UnityParams;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Ljava/util/Currency;->getInstance(Ljava/lang/String;)Ljava/util/Currency;

    move-result-object v0

    .line 254
    invoke-virtual {p0, v2, v0, v1}, Lcom/facebook/appevents/AppEventsLogger;->logPurchase(Ljava/math/BigDecimal;Ljava/util/Currency;Landroid/os/Bundle;)V

    goto :goto_0

    :cond_1
    const-string v2, "logEvent"

    .line 259
    invoke-virtual {v0, v2}, Lcom/facebook/unity/UnityParams;->hasString(Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v2

    if-eqz v2, :cond_3

    const-string p0, "valueToSum"

    .line 260
    invoke-virtual {v0, p0}, Lcom/facebook/unity/UnityParams;->has(Ljava/lang/String;)Z

    move-result p0

    if-eqz p0, :cond_2

    .line 261
    invoke-static {}, Lcom/facebook/unity/FB;->getAppEventsLogger()Lcom/facebook/appevents/AppEventsLogger;

    move-result-object p0

    const-string v2, "logEvent"

    .line 262
    invoke-virtual {v0, v2}, Lcom/facebook/unity/UnityParams;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v3, "valueToSum"

    .line 263
    invoke-virtual {v0, v3}, Lcom/facebook/unity/UnityParams;->getDouble(Ljava/lang/String;)D

    move-result-wide v3

    .line 261
    invoke-virtual {p0, v2, v3, v4, v1}, Lcom/facebook/appevents/AppEventsLogger;->logEvent(Ljava/lang/String;DLandroid/os/Bundle;)V

    goto :goto_0

    .line 267
    :cond_2
    invoke-static {}, Lcom/facebook/unity/FB;->getAppEventsLogger()Lcom/facebook/appevents/AppEventsLogger;

    move-result-object p0

    const-string v2, "logEvent"

    .line 268
    invoke-virtual {v0, v2}, Lcom/facebook/unity/UnityParams;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 267
    invoke-virtual {p0, v0, v1}, Lcom/facebook/appevents/AppEventsLogger;->logEvent(Ljava/lang/String;Landroid/os/Bundle;)V

    goto :goto_0

    .line 273
    :cond_3
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "couldn\'t logPurchase or logEvent params: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method private static LogMethodCall(Ljava/lang/String;Ljava/lang/String;)V
    .locals 5

    .line 592
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    sget-object v1, Ljava/util/Locale;->ROOT:Ljava/util/Locale;

    const-string v2, "%s(%s)"

    const/4 v3, 0x2

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    aput-object p0, v3, v4

    const/4 p0, 0x1

    aput-object p1, v3, p0

    invoke-static {v1, v2, v3}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method

.method public static LoginForTVWithPublishPermissions(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 150
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "LoginForTVWithPublishPermissions("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 151
    new-instance v0, Landroid/content/Intent;

    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v1

    const-class v2, Lcom/facebook/unity/FBUnityLoginActivity;

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "login_params"

    .line 152
    invoke-virtual {v0, v1, p0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p0, "login_type"

    .line 153
    sget-object v1, Lcom/facebook/unity/FBUnityLoginActivity$LoginType;->TV_PUBLISH:Lcom/facebook/unity/FBUnityLoginActivity$LoginType;

    invoke-virtual {v0, p0, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    .line 154
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object p0

    invoke-virtual {p0, v0}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method public static LoginWithPublishPermissions(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 123
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "LoginWithPublishPermissions("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 124
    new-instance v0, Landroid/content/Intent;

    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v1

    const-class v2, Lcom/facebook/unity/FBUnityLoginActivity;

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "login_params"

    .line 125
    invoke-virtual {v0, v1, p0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p0, "login_type"

    .line 126
    sget-object v1, Lcom/facebook/unity/FBUnityLoginActivity$LoginType;->PUBLISH:Lcom/facebook/unity/FBUnityLoginActivity$LoginType;

    invoke-virtual {v0, p0, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    .line 127
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object p0

    invoke-virtual {p0, v0}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method public static LoginWithReadPermissions(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 114
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "LoginWithReadPermissions("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 115
    new-instance v0, Landroid/content/Intent;

    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v1

    const-class v2, Lcom/facebook/unity/FBUnityLoginActivity;

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "login_params"

    .line 116
    invoke-virtual {v0, v1, p0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p0, "login_type"

    .line 117
    sget-object v1, Lcom/facebook/unity/FBUnityLoginActivity$LoginType;->READ:Lcom/facebook/unity/FBUnityLoginActivity$LoginType;

    invoke-virtual {v0, p0, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    .line 118
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object p0

    invoke-virtual {p0, v0}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method public static Logout(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 132
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Logout("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, ")"

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 133
    invoke-static {}, Lcom/facebook/login/LoginManager;->getInstance()Lcom/facebook/login/LoginManager;

    move-result-object p0

    invoke-virtual {p0}, Lcom/facebook/login/LoginManager;->logOut()V

    .line 134
    new-instance p0, Lcom/facebook/unity/UnityMessage;

    const-string v0, "OnLogoutComplete"

    invoke-direct {p0, v0}, Lcom/facebook/unity/UnityMessage;-><init>(Ljava/lang/String;)V

    const-string v0, "did_complete"

    const/4 v1, 0x1

    .line 135
    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-virtual {p0, v0, v1}, Lcom/facebook/unity/UnityMessage;->put(Ljava/lang/String;Ljava/io/Serializable;)Lcom/facebook/unity/UnityMessage;

    .line 136
    invoke-virtual {p0}, Lcom/facebook/unity/UnityMessage;->send()V

    return-void
.end method

.method public static OpenFriendFinderDialog(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 452
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "OpenFriendFinderDialog("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 453
    invoke-static {p0}, Lcom/facebook/unity/UnityParams;->parse(Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object p0

    .line 454
    invoke-virtual {p0}, Lcom/facebook/unity/UnityParams;->getStringParams()Landroid/os/Bundle;

    move-result-object p0

    .line 456
    new-instance v0, Landroid/content/Intent;

    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v1

    const-class v2, Lcom/facebook/unity/FBUnityGamingServicesFriendFinderActivity;

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "dialog_params"

    .line 457
    invoke-virtual {v0, v1, p0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Bundle;)Landroid/content/Intent;

    .line 458
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object p0

    invoke-virtual {p0, v0}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method public static RefreshCurrentAccessToken(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    const-string v0, "RefreshCurrentAccessToken"

    .line 390
    invoke-static {v0, p0}, Lcom/facebook/unity/FB;->LogMethodCall(Ljava/lang/String;Ljava/lang/String;)V

    .line 392
    invoke-static {p0}, Lcom/facebook/unity/UnityParams;->parse(Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object p0

    .line 393
    new-instance v0, Lcom/facebook/unity/UnityMessage;

    const-string v1, "OnRefreshCurrentAccessTokenComplete"

    invoke-direct {v0, v1}, Lcom/facebook/unity/UnityMessage;-><init>(Ljava/lang/String;)V

    const-string v1, "callback_id"

    .line 394
    invoke-virtual {p0, v1}, Lcom/facebook/unity/UnityParams;->hasString(Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v1

    if-eqz v1, :cond_0

    const-string v1, "callback_id"

    const-string v2, "callback_id"

    .line 395
    invoke-virtual {p0, v2}, Lcom/facebook/unity/UnityParams;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, v1, p0}, Lcom/facebook/unity/UnityMessage;->put(Ljava/lang/String;Ljava/io/Serializable;)Lcom/facebook/unity/UnityMessage;

    .line 398
    :cond_0
    new-instance p0, Lcom/facebook/unity/FB$3;

    invoke-direct {p0, v0}, Lcom/facebook/unity/FB$3;-><init>(Lcom/facebook/unity/UnityMessage;)V

    invoke-static {p0}, Lcom/facebook/AccessToken;->refreshCurrentAccessTokenAsync(Lcom/facebook/AccessToken$AccessTokenRefreshCallback;)V

    return-void
.end method

.method public static SetAdvertiserIDCollectionEnabled(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 305
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "SetAdvertiserIDCollectionEnabled("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 307
    invoke-static {p0}, Ljava/lang/Boolean;->valueOf(Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p0

    .line 306
    invoke-static {p0}, Lcom/facebook/FacebookSdk;->setAdvertiserIDCollectionEnabled(Z)V

    return-void
.end method

.method public static SetAutoLogAppEventsEnabled(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 298
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "SetAutoLogAppEventsEnabled("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 300
    invoke-static {p0}, Ljava/lang/Boolean;->valueOf(Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p0

    .line 299
    invoke-static {p0}, Lcom/facebook/FacebookSdk;->setAutoLogAppEventsEnabled(Z)V

    return-void
.end method

.method public static SetIntent(Landroid/content/Intent;)V
    .locals 0

    .line 232
    sput-object p0, Lcom/facebook/unity/FB;->intent:Landroid/content/Intent;

    return-void
.end method

.method public static SetLimitEventUsage(Ljava/lang/String;)V
    .locals 3

    .line 236
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "SetLimitEventUsage("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 238
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v0

    invoke-virtual {v0}, Landroid/app/Activity;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    .line 239
    invoke-static {p0}, Ljava/lang/Boolean;->valueOf(Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object p0

    invoke-virtual {p0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p0

    .line 237
    invoke-static {v0, p0}, Lcom/facebook/FacebookSdk;->setLimitEventAndDataUsage(Landroid/content/Context;Z)V

    return-void
.end method

.method public static SetShareDialogMode(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 284
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "SetShareDialogMode("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    const-string v0, "NATIVE"

    .line 285
    invoke-virtual {p0, v0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 286
    sget-object p0, Lcom/facebook/share/widget/ShareDialog$Mode;->NATIVE:Lcom/facebook/share/widget/ShareDialog$Mode;

    sput-object p0, Lcom/facebook/unity/FB;->ShareDialogMode:Lcom/facebook/share/widget/ShareDialog$Mode;

    goto :goto_0

    :cond_0
    const-string v0, "WEB"

    .line 287
    invoke-virtual {p0, v0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 288
    sget-object p0, Lcom/facebook/share/widget/ShareDialog$Mode;->WEB:Lcom/facebook/share/widget/ShareDialog$Mode;

    sput-object p0, Lcom/facebook/unity/FB;->ShareDialogMode:Lcom/facebook/share/widget/ShareDialog$Mode;

    goto :goto_0

    :cond_1
    const-string v0, "FEED"

    .line 289
    invoke-virtual {p0, v0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result p0

    if-eqz p0, :cond_2

    .line 290
    sget-object p0, Lcom/facebook/share/widget/ShareDialog$Mode;->FEED:Lcom/facebook/share/widget/ShareDialog$Mode;

    sput-object p0, Lcom/facebook/unity/FB;->ShareDialogMode:Lcom/facebook/share/widget/ShareDialog$Mode;

    goto :goto_0

    .line 292
    :cond_2
    sget-object p0, Lcom/facebook/share/widget/ShareDialog$Mode;->AUTOMATIC:Lcom/facebook/share/widget/ShareDialog$Mode;

    sput-object p0, Lcom/facebook/unity/FB;->ShareDialogMode:Lcom/facebook/share/widget/ShareDialog$Mode;

    :goto_0
    return-void
.end method

.method public static SetUserAgentSuffix(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 317
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "SetUserAgentSuffix("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 318
    invoke-static {p0}, Lcom/facebook/internal/InternalSettings;->setCustomUserAgent(Ljava/lang/String;)V

    return-void
.end method

.method public static SetUserID(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 211
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "SetUserID("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 212
    invoke-static {p0}, Lcom/facebook/appevents/AppEventsLogger;->setUserID(Ljava/lang/String;)V

    return-void
.end method

.method public static ShareLink(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 189
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "ShareLink("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 190
    invoke-static {p0}, Lcom/facebook/unity/UnityParams;->parse(Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object p0

    .line 191
    invoke-virtual {p0}, Lcom/facebook/unity/UnityParams;->getStringParams()Landroid/os/Bundle;

    move-result-object p0

    .line 192
    new-instance v0, Landroid/content/Intent;

    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v1

    const-class v2, Lcom/facebook/unity/FBUnityDialogsActivity;

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "dialog_type"

    .line 193
    sget-object v2, Lcom/facebook/unity/FB;->ShareDialogMode:Lcom/facebook/share/widget/ShareDialog$Mode;

    invoke-virtual {v0, v1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    const-string v1, "share_dialog_params"

    .line 194
    invoke-virtual {v0, v1, p0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Bundle;)Landroid/content/Intent;

    .line 195
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object p0

    invoke-virtual {p0, v0}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method public static UpdateUserProperties(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 225
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "UpdateUserProperties("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 226
    invoke-static {p0}, Lcom/facebook/unity/UnityParams;->parse(Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object p0

    .line 227
    invoke-virtual {p0}, Lcom/facebook/unity/UnityParams;->getStringParams()Landroid/os/Bundle;

    move-result-object p0

    const/4 v0, 0x0

    .line 228
    invoke-static {p0, v0}, Lcom/facebook/appevents/AppEventsLogger;->updateUserProperties(Landroid/os/Bundle;Lcom/facebook/GraphRequest$Callback;)V

    return-void
.end method

.method public static UploadImageToMediaLibrary(Ljava/lang/String;)V
    .locals 6
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 463
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "UploadImageToMediaLibrary("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 464
    invoke-static {p0}, Lcom/facebook/unity/UnityParams;->parse(Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object p0

    const-string v0, "caption"

    .line 465
    invoke-virtual {p0, v0}, Lcom/facebook/unity/UnityParams;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "imageUri"

    .line 466
    invoke-virtual {p0, v1}, Lcom/facebook/unity/UnityParams;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v1

    .line 470
    invoke-virtual {v1}, Landroid/net/Uri;->getScheme()Ljava/lang/String;

    move-result-object v2

    if-nez v2, :cond_0

    .line 471
    invoke-virtual {v1}, Landroid/net/Uri;->buildUpon()Landroid/net/Uri$Builder;

    move-result-object v1

    const-string v2, "file"

    invoke-virtual {v1, v2}, Landroid/net/Uri$Builder;->scheme(Ljava/lang/String;)Landroid/net/Uri$Builder;

    move-result-object v1

    invoke-virtual {v1}, Landroid/net/Uri$Builder;->build()Landroid/net/Uri;

    move-result-object v1

    :cond_0
    const-string v2, "shouldLaunchMediaDialog"

    .line 473
    invoke-virtual {p0, v2}, Lcom/facebook/unity/UnityParams;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Ljava/lang/Boolean;->parseBoolean(Ljava/lang/String;)Z

    move-result v2

    .line 475
    new-instance v3, Lcom/facebook/unity/UnityMessage;

    const-string v4, "OnUploadImageToMediaLibraryComplete"

    invoke-direct {v3, v4}, Lcom/facebook/unity/UnityMessage;-><init>(Ljava/lang/String;)V

    const-string v4, "callback_id"

    .line 476
    invoke-virtual {p0, v4}, Lcom/facebook/unity/UnityParams;->hasString(Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v4

    if-eqz v4, :cond_1

    const-string v4, "callback_id"

    const-string v5, "callback_id"

    .line 477
    invoke-virtual {p0, v5}, Lcom/facebook/unity/UnityParams;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v3, v4, p0}, Lcom/facebook/unity/UnityMessage;->put(Ljava/lang/String;Ljava/io/Serializable;)Lcom/facebook/unity/UnityMessage;

    .line 480
    :cond_1
    new-instance p0, Lcom/facebook/gamingservices/GamingImageUploader;

    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v4

    invoke-direct {p0, v4}, Lcom/facebook/gamingservices/GamingImageUploader;-><init>(Landroid/content/Context;)V

    .line 482
    :try_start_0
    new-instance v4, Lcom/facebook/unity/FB$4;

    invoke-direct {v4, v3}, Lcom/facebook/unity/FB$4;-><init>(Lcom/facebook/unity/UnityMessage;)V

    invoke-virtual {p0, v0, v1, v2, v4}, Lcom/facebook/gamingservices/GamingImageUploader;->uploadToMediaLibrary(Ljava/lang/String;Landroid/net/Uri;ZLcom/facebook/GraphRequest$Callback;)V
    :try_end_0
    .catch Ljava/io/FileNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    .line 504
    invoke-virtual {p0}, Ljava/io/FileNotFoundException;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v3, p0}, Lcom/facebook/unity/UnityMessage;->sendError(Ljava/lang/String;)V

    :goto_0
    return-void
.end method

.method public static UploadVideoToMediaLibrary(Ljava/lang/String;)V
    .locals 5
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 510
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "UploadVideoToMediaLibrary("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 511
    invoke-static {p0}, Lcom/facebook/unity/UnityParams;->parse(Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object p0

    const-string v0, "caption"

    .line 512
    invoke-virtual {p0, v0}, Lcom/facebook/unity/UnityParams;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "videoUri"

    .line 513
    invoke-virtual {p0, v1}, Lcom/facebook/unity/UnityParams;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v1

    .line 517
    invoke-virtual {v1}, Landroid/net/Uri;->getScheme()Ljava/lang/String;

    move-result-object v2

    if-nez v2, :cond_0

    .line 518
    invoke-virtual {v1}, Landroid/net/Uri;->buildUpon()Landroid/net/Uri$Builder;

    move-result-object v1

    const-string v2, "file"

    invoke-virtual {v1, v2}, Landroid/net/Uri$Builder;->scheme(Ljava/lang/String;)Landroid/net/Uri$Builder;

    move-result-object v1

    invoke-virtual {v1}, Landroid/net/Uri$Builder;->build()Landroid/net/Uri;

    move-result-object v1

    .line 521
    :cond_0
    new-instance v2, Lcom/facebook/unity/UnityMessage;

    const-string v3, "OnUploadVideoToMediaLibraryComplete"

    invoke-direct {v2, v3}, Lcom/facebook/unity/UnityMessage;-><init>(Ljava/lang/String;)V

    const-string v3, "callback_id"

    .line 522
    invoke-virtual {p0, v3}, Lcom/facebook/unity/UnityParams;->hasString(Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v3

    if-eqz v3, :cond_1

    const-string v3, "callback_id"

    const-string v4, "callback_id"

    .line 523
    invoke-virtual {p0, v4}, Lcom/facebook/unity/UnityParams;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v2, v3, p0}, Lcom/facebook/unity/UnityMessage;->put(Ljava/lang/String;Ljava/io/Serializable;)Lcom/facebook/unity/UnityMessage;

    .line 526
    :cond_1
    new-instance p0, Lcom/facebook/gamingservices/GamingVideoUploader;

    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v3

    invoke-direct {p0, v3}, Lcom/facebook/gamingservices/GamingVideoUploader;-><init>(Landroid/content/Context;)V

    .line 528
    :try_start_0
    new-instance v3, Lcom/facebook/unity/FB$5;

    invoke-direct {v3, v2}, Lcom/facebook/unity/FB$5;-><init>(Lcom/facebook/unity/UnityMessage;)V

    invoke-virtual {p0, v0, v1, v3}, Lcom/facebook/gamingservices/GamingVideoUploader;->uploadToMediaLibrary(Ljava/lang/String;Landroid/net/Uri;Lcom/facebook/GraphRequest$OnProgressCallback;)V
    :try_end_0
    .catch Ljava/io/FileNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    .line 553
    invoke-virtual {p0}, Ljava/io/FileNotFoundException;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v2, p0}, Lcom/facebook/unity/UnityMessage;->sendError(Ljava/lang/String;)V

    :goto_0
    return-void
.end method

.method static synthetic access$000(Ljava/lang/String;)V
    .locals 0

    .line 58
    invoke-static {p0}, Lcom/facebook/unity/FB;->ActivateApp(Ljava/lang/String;)V

    return-void
.end method

.method static synthetic access$100(Lcom/facebook/unity/UnityMessage;Lcom/facebook/applinks/AppLinkData;)V
    .locals 0

    .line 58
    invoke-static {p0, p1}, Lcom/facebook/unity/FB;->addAppLinkToMessage(Lcom/facebook/unity/UnityMessage;Lcom/facebook/applinks/AppLinkData;)V

    return-void
.end method

.method private static addAppLinkToMessage(Lcom/facebook/unity/UnityMessage;Lcom/facebook/applinks/AppLinkData;)V
    .locals 2

    if-nez p1, :cond_0

    const-string p1, "did_complete"

    const/4 v0, 0x1

    .line 597
    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    invoke-virtual {p0, p1, v0}, Lcom/facebook/unity/UnityMessage;->put(Ljava/lang/String;Ljava/io/Serializable;)Lcom/facebook/unity/UnityMessage;

    return-void

    :cond_0
    const-string v0, "ref"

    .line 601
    invoke-virtual {p1}, Lcom/facebook/applinks/AppLinkData;->getRef()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p0, v0, v1}, Lcom/facebook/unity/UnityMessage;->put(Ljava/lang/String;Ljava/io/Serializable;)Lcom/facebook/unity/UnityMessage;

    const-string v0, "target_url"

    .line 602
    invoke-virtual {p1}, Lcom/facebook/applinks/AppLinkData;->getTargetUri()Landroid/net/Uri;

    move-result-object v1

    invoke-virtual {v1}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p0, v0, v1}, Lcom/facebook/unity/UnityMessage;->put(Ljava/lang/String;Ljava/io/Serializable;)Lcom/facebook/unity/UnityMessage;

    .line 604
    :try_start_0
    invoke-virtual {p1}, Lcom/facebook/applinks/AppLinkData;->getArgumentBundle()Landroid/os/Bundle;

    move-result-object v0

    if-eqz v0, :cond_1

    const-string v0, "extras"

    .line 606
    invoke-virtual {p1}, Lcom/facebook/applinks/AppLinkData;->getArgumentBundle()Landroid/os/Bundle;

    move-result-object p1

    .line 605
    invoke-static {p1}, Lcom/facebook/internal/BundleJSONConverter;->convertToJSON(Landroid/os/Bundle;)Lorg/json/JSONObject;

    move-result-object p1

    .line 606
    invoke-virtual {p1}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    .line 605
    invoke-virtual {p0, v0, p1}, Lcom/facebook/unity/UnityMessage;->put(Ljava/lang/String;Ljava/io/Serializable;)Lcom/facebook/unity/UnityMessage;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    .line 609
    sget-object p1, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    invoke-virtual {p0}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object p0

    invoke-static {p1, p0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :cond_1
    :goto_0
    return-void
.end method

.method private static getAppEventsLogger()Lcom/facebook/appevents/AppEventsLogger;
    .locals 1

    .line 69
    sget-object v0, Lcom/facebook/unity/FB;->appEventsLogger:Lcom/facebook/appevents/AppEventsLogger;

    if-nez v0, :cond_0

    .line 70
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v0

    invoke-virtual {v0}, Landroid/app/Activity;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Lcom/facebook/appevents/AppEventsLogger;->newLogger(Landroid/content/Context;)Lcom/facebook/appevents/AppEventsLogger;

    move-result-object v0

    sput-object v0, Lcom/facebook/unity/FB;->appEventsLogger:Lcom/facebook/appevents/AppEventsLogger;

    .line 72
    :cond_0
    sget-object v0, Lcom/facebook/unity/FB;->appEventsLogger:Lcom/facebook/appevents/AppEventsLogger;

    return-object v0
.end method

.method public static getKeyHash()Ljava/lang/String;
    .locals 4
    .annotation build Landroid/annotation/TargetApi;
        value = 0x8
    .end annotation

    .line 424
    :try_start_0
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v0

    if-nez v0, :cond_0

    const-string v0, ""

    return-object v0

    .line 429
    :cond_0
    invoke-virtual {v0}, Landroid/app/Activity;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    .line 430
    invoke-virtual {v0}, Landroid/app/Activity;->getPackageName()Ljava/lang/String;

    move-result-object v0

    const/16 v2, 0x40

    .line 429
    invoke-virtual {v1, v0, v2}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v0

    .line 432
    iget-object v0, v0, Landroid/content/pm/PackageInfo;->signatures:[Landroid/content/pm/Signature;

    array-length v1, v0

    if-lez v1, :cond_1

    const/4 v1, 0x0

    aget-object v0, v0, v1

    const-string v2, "SHA"

    .line 433
    invoke-static {v2}, Ljava/security/MessageDigest;->getInstance(Ljava/lang/String;)Ljava/security/MessageDigest;

    move-result-object v2

    .line 434
    invoke-virtual {v0}, Landroid/content/pm/Signature;->toByteArray()[B

    move-result-object v0

    invoke-virtual {v2, v0}, Ljava/security/MessageDigest;->update([B)V

    .line 435
    invoke-virtual {v2}, Ljava/security/MessageDigest;->digest()[B

    move-result-object v0

    invoke-static {v0, v1}, Landroid/util/Base64;->encodeToString([BI)Ljava/lang/String;

    move-result-object v0

    .line 436
    sget-object v1, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "KeyHash: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/security/NoSuchAlgorithmException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    :cond_1
    const-string v0, ""

    return-object v0
.end method

.method public static getUnityActivity()Landroid/app/Activity;
    .locals 1

    .line 76
    invoke-static {}, Lcom/facebook/unity/UnityReflection;->GetUnityActivity()Landroid/app/Activity;

    move-result-object v0

    return-object v0
.end method

.method public static loginForTVWithReadPermissions(Ljava/lang/String;)V
    .locals 3
    .annotation build Lcom/facebook/unity/UnityCallable;
    .end annotation

    .line 141
    sget-object v0, Lcom/facebook/unity/FB;->TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "loginForTVWithReadPermissions("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 142
    new-instance v0, Landroid/content/Intent;

    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v1

    const-class v2, Lcom/facebook/unity/FBUnityLoginActivity;

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "login_params"

    .line 143
    invoke-virtual {v0, v1, p0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p0, "login_type"

    .line 144
    sget-object v1, Lcom/facebook/unity/FBUnityLoginActivity$LoginType;->TV_READ:Lcom/facebook/unity/FBUnityLoginActivity$LoginType;

    invoke-virtual {v0, p0, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    .line 145
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object p0

    invoke-virtual {p0, v0}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method private static startActivity(Ljava/lang/Class;Ljava/lang/String;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    .line 584
    new-instance v0, Landroid/content/Intent;

    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object v1

    invoke-direct {v0, v1, p0}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    .line 585
    invoke-static {p1}, Lcom/facebook/unity/UnityParams;->parse(Ljava/lang/String;)Lcom/facebook/unity/UnityParams;

    move-result-object p0

    .line 586
    invoke-virtual {p0}, Lcom/facebook/unity/UnityParams;->getStringParams()Landroid/os/Bundle;

    move-result-object p0

    const-string p1, "activity_params"

    .line 587
    invoke-virtual {v0, p1, p0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Bundle;)Landroid/content/Intent;

    .line 588
    invoke-static {}, Lcom/facebook/unity/FB;->getUnityActivity()Landroid/app/Activity;

    move-result-object p0

    invoke-virtual {p0, v0}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method
