.class Lnet/gogame/chat/zopim/ZopimChatContext$7;
.super Ljava/lang/Object;
.source "ZopimChatContext.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/chat/zopim/ZopimChatContext;->onOptionClickListener(Ljava/lang/String;)Landroid/view/View$OnClickListener;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/zopim/ZopimChatContext;

.field final synthetic val$text:Ljava/lang/String;


# direct methods
.method constructor <init>(Lnet/gogame/chat/zopim/ZopimChatContext;Ljava/lang/String;)V
    .locals 0

    .line 370
    iput-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext$7;->this$0:Lnet/gogame/chat/zopim/ZopimChatContext;

    iput-object p2, p0, Lnet/gogame/chat/zopim/ZopimChatContext$7;->val$text:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 374
    iget-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext$7;->this$0:Lnet/gogame/chat/zopim/ZopimChatContext;

    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext$7;->val$text:Ljava/lang/String;

    invoke-virtual {p1, v0}, Lnet/gogame/chat/zopim/ZopimChatContext;->send(Ljava/lang/String;)V

    return-void
.end method
