.class Lnet/gogame/chat/zopim/ZopimChatContext$6;
.super Ljava/lang/Object;
.source "ZopimChatContext.java"

# interfaces
.implements Lnet/gogame/chat/ChatAdapterViewFactory$RatingListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/chat/zopim/ZopimChatContext;->getView(Ljava/lang/Object;ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/zopim/ZopimChatContext;


# direct methods
.method constructor <init>(Lnet/gogame/chat/zopim/ZopimChatContext;)V
    .locals 0

    .line 296
    iput-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext$6;->this$0:Lnet/gogame/chat/zopim/ZopimChatContext;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onRatingChanged(Lnet/gogame/chat/ChatContext$Rating;)V
    .locals 1

    .line 300
    iget-object v0, p0, Lnet/gogame/chat/zopim/ZopimChatContext$6;->this$0:Lnet/gogame/chat/zopim/ZopimChatContext;

    invoke-virtual {v0, p1}, Lnet/gogame/chat/zopim/ZopimChatContext;->send(Lnet/gogame/chat/ChatContext$Rating;)V

    return-void
.end method
