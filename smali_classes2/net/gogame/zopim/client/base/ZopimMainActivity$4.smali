.class Lnet/gogame/zopim/client/base/ZopimMainActivity$4;
.super Ljava/lang/Object;
.source "ZopimMainActivity.java"

# interfaces
.implements Lnet/gogame/chat/MultiChatContext$Listener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/zopim/client/base/ZopimMainActivity;->onCreate(Landroid/os/Bundle;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;

.field final synthetic val$switchButton:Landroid/widget/Button;

.field final synthetic val$zopimSessionConfig:Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;


# direct methods
.method constructor <init>(Lnet/gogame/zopim/client/base/ZopimMainActivity;Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;Landroid/widget/Button;)V
    .locals 0

    .line 136
    iput-object p1, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$4;->this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;

    iput-object p2, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$4;->val$zopimSessionConfig:Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    iput-object p3, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$4;->val$switchButton:Landroid/widget/Button;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onChatContextAdded(Lnet/gogame/chat/ChatContext;)V
    .locals 2

    .line 140
    instance-of v0, p1, Lnet/gogame/chat/chatbot/ChatBotChatContext;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$4;->val$zopimSessionConfig:Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    if-eqz v0, :cond_0

    .line 141
    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$4;->val$switchButton:Landroid/widget/Button;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setVisibility(I)V

    .line 143
    :cond_0
    instance-of p1, p1, Lnet/gogame/chat/zopim/ZopimChatContext;

    if-eqz p1, :cond_1

    .line 144
    iget-object p1, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$4;->val$switchButton:Landroid/widget/Button;

    const/16 v0, 0x8

    invoke-virtual {p1, v0}, Landroid/widget/Button;->setVisibility(I)V

    :cond_1
    return-void
.end method
