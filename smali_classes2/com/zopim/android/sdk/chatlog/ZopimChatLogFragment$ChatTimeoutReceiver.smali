.class public Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment$ChatTimeoutReceiver;
.super Landroid/content/BroadcastReceiver;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "ChatTimeoutReceiver"
.end annotation


# instance fields
.field final synthetic this$0:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;


# direct methods
.method public constructor <init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment$ChatTimeoutReceiver;->this$0:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    return-void
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 0

    if-eqz p2, :cond_1

    const-string p1, "chat.action.TIMEOUT"

    invoke-virtual {p2}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-nez p1, :cond_0

    goto :goto_0

    :cond_0
    invoke-static {}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$500()Ljava/lang/String;

    move-result-object p1

    const-string p2, "Received chat timeout. Disabling all input."

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment$ChatTimeoutReceiver;->this$0:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    iget-object p2, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment$ChatTimeoutReceiver;->this$0:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p2}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$400(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroid/widget/EditText;

    move-result-object p2

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$1200(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;Landroid/view/View;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment$ChatTimeoutReceiver;->this$0:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$200(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroid/widget/ImageButton;

    move-result-object p1

    const/4 p2, 0x0

    invoke-virtual {p1, p2}, Landroid/widget/ImageButton;->setEnabled(Z)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment$ChatTimeoutReceiver;->this$0:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$100(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroid/widget/ImageButton;

    move-result-object p1

    invoke-virtual {p1, p2}, Landroid/widget/ImageButton;->setEnabled(Z)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment$ChatTimeoutReceiver;->this$0:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$400(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroid/widget/EditText;

    move-result-object p1

    invoke-virtual {p1, p2}, Landroid/widget/EditText;->setFocusable(Z)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment$ChatTimeoutReceiver;->this$0:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$400(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroid/widget/EditText;

    move-result-object p1

    invoke-virtual {p1, p2}, Landroid/widget/EditText;->setEnabled(Z)V

    return-void

    :cond_1
    :goto_0
    invoke-static {}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$500()Ljava/lang/String;

    move-result-object p1

    const-string p2, "onReceive: intent was null or getAction() was mismatched"

    invoke-static {p1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method
