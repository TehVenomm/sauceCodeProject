.class Lcom/helpshift/HelpshiftUnityAPI$HelpshiftDelegate;
.super Ljava/lang/Object;
.source "HelpshiftUnityAPI.java"

# interfaces
.implements Lcom/helpshift/support/Support$Delegate;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/HelpshiftUnityAPI;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "HelpshiftDelegate"
.end annotation


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 394
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method synthetic constructor <init>(Lcom/helpshift/HelpshiftUnityAPI$1;)V
    .locals 0

    .line 394
    invoke-direct {p0}, Lcom/helpshift/HelpshiftUnityAPI$HelpshiftDelegate;-><init>()V

    return-void
.end method

.method private convertHelpshiftUserModelToMap(Lcom/helpshift/HelpshiftUser;)Ljava/util/Map;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/HelpshiftUser;",
            ")",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 479
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "identifier"

    .line 480
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getIdentifier()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "email"

    .line 481
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getEmail()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "name"

    .line 482
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "authToken"

    .line 483
    invoke-virtual {p1}, Lcom/helpshift/HelpshiftUser;->getAuthToken()Ljava/lang/String;

    move-result-object p1

    invoke-interface {v0, v1, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-object v0
.end method


# virtual methods
.method public authenticationFailed(Lcom/helpshift/HelpshiftUser;Lcom/helpshift/delegate/AuthenticationFailureReason;)V
    .locals 2

    .line 470
    invoke-static {}, Lcom/helpshift/HelpshiftUnityAPI;->access$000()Ljava/lang/String;

    move-result-object v0

    .line 472
    invoke-direct {p0, p1}, Lcom/helpshift/HelpshiftUnityAPI$HelpshiftDelegate;->convertHelpshiftUserModelToMap(Lcom/helpshift/HelpshiftUser;)Ljava/util/Map;

    move-result-object p1

    const-string v1, "authFailureReason"

    .line 473
    invoke-virtual {p2}, Lcom/helpshift/delegate/AuthenticationFailureReason;->getValue()I

    move-result p2

    invoke-static {p2}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object p2

    invoke-interface {p1, v1, p2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p2, "authenticationFailed"

    .line 475
    new-instance v1, Lorg/json/JSONObject;

    invoke-direct {v1, p1}, Lorg/json/JSONObject;-><init>(Ljava/util/Map;)V

    invoke-virtual {v1}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p2, p1}, Lcom/helpshift/util/UnityUtils;->sendUnityMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public conversationEnded()V
    .locals 3

    .line 489
    invoke-static {}, Lcom/helpshift/HelpshiftUnityAPI;->access$000()Ljava/lang/String;

    move-result-object v0

    const-string v1, "conversationEnded"

    const-string v2, ""

    .line 490
    invoke-static {v0, v1, v2}, Lcom/helpshift/util/UnityUtils;->sendUnityMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public didReceiveNotification(I)V
    .locals 2

    .line 464
    invoke-static {}, Lcom/helpshift/HelpshiftUnityAPI;->access$000()Ljava/lang/String;

    move-result-object v0

    const-string v1, "didReceiveNotificationCount"

    .line 465
    invoke-static {p1}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, v1, p1}, Lcom/helpshift/util/UnityUtils;->sendUnityMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public displayAttachmentFile(Landroid/net/Uri;)V
    .locals 2

    .line 451
    invoke-virtual {p1}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v0

    .line 452
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-static {v1, p1}, Lcom/helpshift/util/FileUtil;->getFileExtension(Landroid/content/Context;Landroid/net/Uri;)Ljava/lang/String;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 453
    invoke-static {}, Lcom/helpshift/HelpshiftUnityAPI;->access$200()Ljava/util/ArrayList;

    move-result-object v1

    if-eqz v1, :cond_0

    invoke-static {}, Lcom/helpshift/HelpshiftUnityAPI;->access$200()Ljava/util/ArrayList;

    move-result-object v1

    invoke-virtual {v1, p1}, Ljava/util/ArrayList;->contains(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 454
    invoke-static {}, Lcom/helpshift/HelpshiftUnityAPI;->access$000()Ljava/lang/String;

    move-result-object p1

    const-string v1, "displayAttachmentFile"

    .line 455
    invoke-static {p1, v1, v0}, Lcom/helpshift/util/UnityUtils;->sendUnityMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 458
    :cond_0
    sget-object p1, Lcom/helpshift/common/exception/PlatformException;->NO_APPS_FOR_OPENING_ATTACHMENT:Lcom/helpshift/common/exception/PlatformException;

    const/4 v0, 0x0

    invoke-static {p1, v0}, Lcom/helpshift/support/util/SnackbarUtil;->showSnackbar(Lcom/helpshift/common/exception/ExceptionType;Landroid/view/View;)V

    :goto_0
    return-void
.end method

.method public displayAttachmentFile(Ljava/io/File;)V
    .locals 2

    .line 437
    invoke-virtual {p1}, Ljava/io/File;->getAbsolutePath()Ljava/lang/String;

    move-result-object v0

    .line 438
    invoke-virtual {p1}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object p1

    .line 439
    invoke-static {p1}, Lcom/helpshift/util/FileUtil;->getFileExtensionFromFileName(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 440
    invoke-static {}, Lcom/helpshift/HelpshiftUnityAPI;->access$200()Ljava/util/ArrayList;

    move-result-object v1

    if-eqz v1, :cond_0

    invoke-static {}, Lcom/helpshift/HelpshiftUnityAPI;->access$200()Ljava/util/ArrayList;

    move-result-object v1

    invoke-virtual {v1, p1}, Ljava/util/ArrayList;->contains(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 441
    invoke-static {}, Lcom/helpshift/HelpshiftUnityAPI;->access$000()Ljava/lang/String;

    move-result-object p1

    const-string v1, "displayAttachmentFile"

    .line 442
    invoke-static {p1, v1, v0}, Lcom/helpshift/util/UnityUtils;->sendUnityMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 445
    :cond_0
    sget-object p1, Lcom/helpshift/common/exception/PlatformException;->NO_APPS_FOR_OPENING_ATTACHMENT:Lcom/helpshift/common/exception/PlatformException;

    const/4 v0, 0x0

    invoke-static {p1, v0}, Lcom/helpshift/support/util/SnackbarUtil;->showSnackbar(Lcom/helpshift/common/exception/ExceptionType;Landroid/view/View;)V

    :goto_0
    return-void
.end method

.method public newConversationStarted(Ljava/lang/String;)V
    .locals 2

    .line 409
    invoke-static {}, Lcom/helpshift/HelpshiftUnityAPI;->access$000()Ljava/lang/String;

    move-result-object v0

    const-string v1, "newConversationStarted"

    .line 410
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/UnityUtils;->sendUnityMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public sessionBegan()V
    .locals 3

    .line 397
    invoke-static {}, Lcom/helpshift/HelpshiftUnityAPI;->access$000()Ljava/lang/String;

    move-result-object v0

    const-string v1, "helpshiftSessionBegan"

    const-string v2, ""

    .line 398
    invoke-static {v0, v1, v2}, Lcom/helpshift/util/UnityUtils;->sendUnityMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public sessionEnded()V
    .locals 3

    .line 403
    invoke-static {}, Lcom/helpshift/HelpshiftUnityAPI;->access$000()Ljava/lang/String;

    move-result-object v0

    const-string v1, "helpshiftSessionEnded"

    const-string v2, ""

    .line 404
    invoke-static {v0, v1, v2}, Lcom/helpshift/util/UnityUtils;->sendUnityMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public userCompletedCustomerSatisfactionSurvey(ILjava/lang/String;)V
    .locals 3

    .line 421
    invoke-static {}, Lcom/helpshift/HelpshiftUnityAPI;->access$000()Ljava/lang/String;

    move-result-object v0

    .line 422
    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    if-nez v1, :cond_0

    .line 424
    :try_start_0
    new-instance v1, Lorg/json/JSONObject;

    invoke-direct {v1}, Lorg/json/JSONObject;-><init>()V

    const-string v2, "rating"

    .line 425
    invoke-virtual {v1, v2, p1}, Lorg/json/JSONObject;->put(Ljava/lang/String;I)Lorg/json/JSONObject;

    const-string p1, "feedback"

    .line 426
    invoke-virtual {v1, p1, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string p1, "userCompletedCustomerSatisfactionSurvey"

    .line 427
    invoke-virtual {v1}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {v0, p1, p2}, Lcom/helpshift/util/UnityUtils;->sendUnityMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string p2, "Helpshift_UnityAPI"

    const-string v0, "Error in userCompletedCustomerSatisfactionSurvey"

    .line 430
    invoke-static {p2, v0, p1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_0
    :goto_0
    return-void
.end method

.method public userRepliedToConversation(Ljava/lang/String;)V
    .locals 2

    .line 415
    invoke-static {}, Lcom/helpshift/HelpshiftUnityAPI;->access$000()Ljava/lang/String;

    move-result-object v0

    const-string v1, "userRepliedToConversation"

    .line 416
    invoke-static {v0, v1, p1}, Lcom/helpshift/util/UnityUtils;->sendUnityMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
