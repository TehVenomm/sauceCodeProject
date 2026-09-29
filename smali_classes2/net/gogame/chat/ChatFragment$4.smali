.class Lnet/gogame/chat/ChatFragment$4;
.super Ljava/lang/Object;
.source "ChatFragment.java"

# interfaces
.implements Landroid/text/TextWatcher;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/chat/ChatFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/ChatFragment;

.field final synthetic val$sendButton:Landroid/widget/ImageButton;


# direct methods
.method constructor <init>(Lnet/gogame/chat/ChatFragment;Landroid/widget/ImageButton;)V
    .locals 0

    .line 109
    iput-object p1, p0, Lnet/gogame/chat/ChatFragment$4;->this$0:Lnet/gogame/chat/ChatFragment;

    iput-object p2, p0, Lnet/gogame/chat/ChatFragment$4;->val$sendButton:Landroid/widget/ImageButton;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public afterTextChanged(Landroid/text/Editable;)V
    .locals 2

    .line 123
    iget-object v0, p0, Lnet/gogame/chat/ChatFragment$4;->this$0:Lnet/gogame/chat/ChatFragment;

    iget-object v1, p0, Lnet/gogame/chat/ChatFragment$4;->val$sendButton:Landroid/widget/ImageButton;

    invoke-static {v0, v1, p1}, Lnet/gogame/chat/ChatFragment;->access$200(Lnet/gogame/chat/ChatFragment;Landroid/widget/ImageButton;Landroid/text/Editable;)V

    return-void
.end method

.method public beforeTextChanged(Ljava/lang/CharSequence;III)V
    .locals 0

    return-void
.end method

.method public onTextChanged(Ljava/lang/CharSequence;III)V
    .locals 0

    return-void
.end method
