.class Lnet/gogame/chat/ChatAdapterViewFactory$4;
.super Ljava/lang/Object;
.source "ChatAdapterViewFactory.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/chat/ChatAdapterViewFactory;->getVisitorAttachmentView(Landroid/view/View;Landroid/view/ViewGroup;Landroid/net/Uri;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/ChatAdapterViewFactory;

.field final synthetic val$uri:Landroid/net/Uri;


# direct methods
.method constructor <init>(Lnet/gogame/chat/ChatAdapterViewFactory;Landroid/net/Uri;)V
    .locals 0

    .line 286
    iput-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$4;->this$0:Lnet/gogame/chat/ChatAdapterViewFactory;

    iput-object p2, p0, Lnet/gogame/chat/ChatAdapterViewFactory$4;->val$uri:Landroid/net/Uri;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 290
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$4;->this$0:Lnet/gogame/chat/ChatAdapterViewFactory;

    invoke-static {p1}, Lnet/gogame/chat/ChatAdapterViewFactory;->access$100(Lnet/gogame/chat/ChatAdapterViewFactory;)Lnet/gogame/chat/UIContext;

    move-result-object p1

    iget-object v0, p0, Lnet/gogame/chat/ChatAdapterViewFactory$4;->val$uri:Landroid/net/Uri;

    invoke-virtual {v0}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-interface {p1, v0}, Lnet/gogame/chat/UIContext;->showImage(Ljava/lang/String;)V

    return-void
.end method
