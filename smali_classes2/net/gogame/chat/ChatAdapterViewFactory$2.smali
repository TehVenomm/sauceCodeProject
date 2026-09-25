.class Lnet/gogame/chat/ChatAdapterViewFactory$2;
.super Ljava/lang/Object;
.source "ChatAdapterViewFactory.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/chat/ChatAdapterViewFactory;->getAgentAttachmentView(Landroid/view/View;Landroid/view/ViewGroup;ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/ChatAdapterViewFactory;

.field final synthetic val$attachmentUri:Ljava/lang/String;


# direct methods
.method constructor <init>(Lnet/gogame/chat/ChatAdapterViewFactory;Ljava/lang/String;)V
    .locals 0

    .line 166
    iput-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$2;->this$0:Lnet/gogame/chat/ChatAdapterViewFactory;

    iput-object p2, p0, Lnet/gogame/chat/ChatAdapterViewFactory$2;->val$attachmentUri:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 170
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$2;->this$0:Lnet/gogame/chat/ChatAdapterViewFactory;

    invoke-static {p1}, Lnet/gogame/chat/ChatAdapterViewFactory;->access$100(Lnet/gogame/chat/ChatAdapterViewFactory;)Lnet/gogame/chat/UIContext;

    move-result-object p1

    iget-object v0, p0, Lnet/gogame/chat/ChatAdapterViewFactory$2;->val$attachmentUri:Ljava/lang/String;

    invoke-interface {p1, v0}, Lnet/gogame/chat/UIContext;->showImage(Ljava/lang/String;)V

    return-void
.end method
