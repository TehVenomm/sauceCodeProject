.class public Lcom/zopim/android/sdk/api/ZopimChat$ChatTimeoutReceiver;
.super Landroid/content/BroadcastReceiver;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/api/ZopimChat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "ChatTimeoutReceiver"
.end annotation


# direct methods
.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    return-void
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 0

    if-eqz p2, :cond_2

    const-string p1, "chat.action.TIMEOUT"

    invoke-virtual {p2}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-nez p1, :cond_0

    goto :goto_0

    :cond_0
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1400()Z

    move-result p1

    if-eqz p1, :cond_1

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$200()Ljava/lang/String;

    move-result-object p1

    const-string p2, "Received chat timeout. Ending chat."

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/Logger;->i(Ljava/lang/String;Ljava/lang/String;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object p1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/api/ZopimChat;->endChat()V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object p1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/api/ZopimChat;->hasEnded()Z

    move-result p1

    if-nez p1, :cond_1

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$200()Ljava/lang/String;

    move-result-object p1

    const-string p2, "Chat previously expired. Updating chat state as ended."

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/Logger;->i(Ljava/lang/String;Ljava/lang/String;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object p1

    const/4 p2, 0x1

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1602(Lcom/zopim/android/sdk/api/ZopimChat;Z)Z

    :cond_1
    return-void

    :cond_2
    :goto_0
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$200()Ljava/lang/String;

    move-result-object p1

    const-string p2, "onReceive: intent was null or getAction() was mismatched"

    invoke-static {p1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method
