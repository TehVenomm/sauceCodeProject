.class public Lnet/gogame/chat/zopim/ZopimChatContext$ChatTimeoutReceiver;
.super Landroid/content/BroadcastReceiver;
.source "ZopimChatContext.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/chat/zopim/ZopimChatContext;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "ChatTimeoutReceiver"
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/zopim/ZopimChatContext;


# direct methods
.method public constructor <init>(Lnet/gogame/chat/zopim/ZopimChatContext;)V
    .locals 0

    .line 400
    iput-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext$ChatTimeoutReceiver;->this$0:Lnet/gogame/chat/zopim/ZopimChatContext;

    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    return-void
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 0

    if-eqz p2, :cond_0

    const-string p1, "chat.action.TIMEOUT"

    .line 404
    invoke-virtual {p2}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    :cond_0
    return-void
.end method
