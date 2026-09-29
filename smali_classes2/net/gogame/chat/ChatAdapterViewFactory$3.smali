.class Lnet/gogame/chat/ChatAdapterViewFactory$3;
.super Ljava/lang/Object;
.source "ChatAdapterViewFactory.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/chat/ChatAdapterViewFactory;->getAgentOptionsView(Landroid/view/View;Landroid/view/ViewGroup;ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/List;Lnet/gogame/chat/ChatAdapterViewFactory$OptionListener;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/ChatAdapterViewFactory;

.field final synthetic val$option:Lnet/gogame/chat/ChatAdapterViewFactory$Option;

.field final synthetic val$optionListener:Lnet/gogame/chat/ChatAdapterViewFactory$OptionListener;


# direct methods
.method constructor <init>(Lnet/gogame/chat/ChatAdapterViewFactory;Lnet/gogame/chat/ChatAdapterViewFactory$OptionListener;Lnet/gogame/chat/ChatAdapterViewFactory$Option;)V
    .locals 0

    .line 233
    iput-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$3;->this$0:Lnet/gogame/chat/ChatAdapterViewFactory;

    iput-object p2, p0, Lnet/gogame/chat/ChatAdapterViewFactory$3;->val$optionListener:Lnet/gogame/chat/ChatAdapterViewFactory$OptionListener;

    iput-object p3, p0, Lnet/gogame/chat/ChatAdapterViewFactory$3;->val$option:Lnet/gogame/chat/ChatAdapterViewFactory$Option;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 237
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$3;->val$optionListener:Lnet/gogame/chat/ChatAdapterViewFactory$OptionListener;

    iget-object v0, p0, Lnet/gogame/chat/ChatAdapterViewFactory$3;->val$option:Lnet/gogame/chat/ChatAdapterViewFactory$Option;

    invoke-interface {p1, v0}, Lnet/gogame/chat/ChatAdapterViewFactory$OptionListener;->onOptionSelected(Lnet/gogame/chat/ChatAdapterViewFactory$Option;)V

    return-void
.end method
