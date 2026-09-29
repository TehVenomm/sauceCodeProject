.class public Lcom/google/firebase/messaging/MessageForwardingService;
.super Landroid/app/IntentService;
.source "MessageForwardingService.java"


# static fields
.field public static final ACTION_REMOTE_INTENT:Ljava/lang/String; = "com.google.android.c2dm.intent.RECEIVE"

.field private static final TAG:Ljava/lang/String; = "FIREBASE_MSG_FWDR"


# direct methods
.method public constructor <init>()V
    .locals 1

    const-string v0, "FIREBASE_MSG_FWDR"

    .line 34
    invoke-direct {p0, v0}, Landroid/app/IntentService;-><init>(Ljava/lang/String;)V

    return-void
.end method

.method static handleIntent(Landroid/content/Context;Landroid/content/Intent;Lcom/google/firebase/messaging/cpp/MessageWriter;)V
    .locals 5

    const-string v0, "FIREBASE_MSG_FWDR"

    const-string v1, "onHandleIntent "

    if-nez p1, :cond_0

    const-string v2, "null intent"

    goto :goto_0

    .line 51
    :cond_0
    invoke-virtual {p1}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object v2

    if-nez v2, :cond_1

    const-string v2, "(null)"

    goto :goto_0

    :cond_1
    invoke-virtual {p1}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object v2

    :goto_0
    invoke-static {v2}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v3

    if-eqz v3, :cond_2

    invoke-virtual {v1, v2}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    goto :goto_1

    :cond_2
    new-instance v2, Ljava/lang/String;

    invoke-direct {v2, v1}, Ljava/lang/String;-><init>(Ljava/lang/String;)V

    move-object v1, v2

    .line 46
    :goto_1
    invoke-static {v0, v1}, Lcom/google/firebase/messaging/cpp/DebugLogging;->log(Ljava/lang/String;Ljava/lang/String;)V

    if-eqz p1, :cond_6

    .line 53
    invoke-virtual {p1}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_6

    .line 54
    invoke-virtual {p1}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object v0

    const-string v1, "com.google.android.c2dm.intent.RECEIVE"

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_6

    .line 55
    invoke-virtual {p1}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v0

    const-string v1, "FIREBASE_MSG_FWDR"

    const-string v2, "extras: "

    if-nez v0, :cond_3

    const-string v3, "(null)"

    goto :goto_2

    .line 56
    :cond_3
    invoke-virtual {v0}, Landroid/os/Bundle;->toString()Ljava/lang/String;

    move-result-object v3

    :goto_2
    invoke-static {v3}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v4

    if-eqz v4, :cond_4

    invoke-virtual {v2, v3}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    goto :goto_3

    :cond_4
    new-instance v3, Ljava/lang/String;

    invoke-direct {v3, v2}, Ljava/lang/String;-><init>(Ljava/lang/String;)V

    move-object v2, v3

    :goto_3
    invoke-static {v1, v2}, Lcom/google/firebase/messaging/cpp/DebugLogging;->log(Ljava/lang/String;Ljava/lang/String;)V

    if-eqz v0, :cond_6

    .line 58
    new-instance v1, Lcom/google/firebase/messaging/RemoteMessage;

    invoke-direct {v1, v0}, Lcom/google/firebase/messaging/RemoteMessage;-><init>(Landroid/os/Bundle;)V

    const-string v0, "FIREBASE_MSG_FWDR"

    const-string v2, "message: "

    .line 60
    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v3}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v4

    if-eqz v4, :cond_5

    invoke-virtual {v2, v3}, Ljava/lang/String;->concat(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    goto :goto_4

    :cond_5
    new-instance v3, Ljava/lang/String;

    invoke-direct {v3, v2}, Ljava/lang/String;-><init>(Ljava/lang/String;)V

    move-object v2, v3

    :goto_4
    invoke-static {v0, v2}, Lcom/google/firebase/messaging/cpp/DebugLogging;->log(Ljava/lang/String;Ljava/lang/String;)V

    .line 62
    invoke-virtual {v1}, Lcom/google/firebase/messaging/RemoteMessage;->getFrom()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_6

    invoke-virtual {v1}, Lcom/google/firebase/messaging/RemoteMessage;->getMessageId()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_6

    const/4 v0, 0x1

    .line 63
    invoke-virtual {p1}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object p1

    invoke-virtual {p2, p0, v1, v0, p1}, Lcom/google/firebase/messaging/cpp/MessageWriter;->writeMessage(Landroid/content/Context;Lcom/google/firebase/messaging/RemoteMessage;ZLandroid/net/Uri;)V

    :cond_6
    return-void
.end method


# virtual methods
.method protected onHandleIntent(Landroid/content/Intent;)V
    .locals 1

    .line 40
    invoke-static {}, Lcom/google/firebase/messaging/cpp/MessageWriter;->defaultInstance()Lcom/google/firebase/messaging/cpp/MessageWriter;

    move-result-object v0

    invoke-static {p0, p1, v0}, Lcom/google/firebase/messaging/MessageForwardingService;->handleIntent(Landroid/content/Context;Landroid/content/Intent;Lcom/google/firebase/messaging/cpp/MessageWriter;)V

    return-void
.end method
