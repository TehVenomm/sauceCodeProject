.class Lnet/gogame/zopim/client/base/ZopimMainActivity$2;
.super Ljava/lang/Object;
.source "ZopimMainActivity.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


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

.field final synthetic val$viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

.field final synthetic val$zopimSessionConfig:Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;


# direct methods
.method constructor <init>(Lnet/gogame/zopim/client/base/ZopimMainActivity;Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;Lnet/gogame/chat/ChatAdapterViewFactory;)V
    .locals 0

    .line 109
    iput-object p1, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$2;->this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;

    iput-object p2, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$2;->val$zopimSessionConfig:Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    iput-object p3, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$2;->val$viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 4

    .line 113
    new-instance p1, Lnet/gogame/chat/zopim/ZopimChatContext;

    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$2;->this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;

    iget-object v1, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$2;->val$zopimSessionConfig:Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    iget-object v2, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$2;->val$viewFactory:Lnet/gogame/chat/ChatAdapterViewFactory;

    iget-object v3, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$2;->this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;

    .line 114
    invoke-static {v3}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->access$000(Lnet/gogame/zopim/client/base/ZopimMainActivity;)Lnet/gogame/chat/UIContext;

    move-result-object v3

    invoke-direct {p1, v0, v1, v2, v3}, Lnet/gogame/chat/zopim/ZopimChatContext;-><init>(Landroidx/fragment/app/FragmentActivity;Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;Lnet/gogame/chat/ChatAdapterViewFactory;Lnet/gogame/chat/UIContext;)V

    .line 115
    invoke-virtual {p1}, Lnet/gogame/chat/zopim/ZopimChatContext;->start()V

    .line 116
    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$2;->this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;

    invoke-static {v0}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->access$100(Lnet/gogame/zopim/client/base/ZopimMainActivity;)Lnet/gogame/chat/MultiChatContext;

    move-result-object v0

    invoke-virtual {v0, p1}, Lnet/gogame/chat/MultiChatContext;->addChatContext(Lnet/gogame/chat/ChatContext;)V

    return-void
.end method
