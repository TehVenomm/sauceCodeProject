.class Lnet/gogame/chat/ChatFragment$2;
.super Ljava/lang/Object;
.source "ChatFragment.java"

# interfaces
.implements Landroid/view/View$OnKeyListener;


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

.field final synthetic val$editText:Landroid/widget/EditText;

.field final synthetic val$self:Lnet/gogame/chat/ChatFragment;


# direct methods
.method constructor <init>(Lnet/gogame/chat/ChatFragment;Lnet/gogame/chat/ChatFragment;Landroid/widget/EditText;)V
    .locals 0

    .line 70
    iput-object p1, p0, Lnet/gogame/chat/ChatFragment$2;->this$0:Lnet/gogame/chat/ChatFragment;

    iput-object p2, p0, Lnet/gogame/chat/ChatFragment$2;->val$self:Lnet/gogame/chat/ChatFragment;

    iput-object p3, p0, Lnet/gogame/chat/ChatFragment$2;->val$editText:Landroid/widget/EditText;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onKey(Landroid/view/View;ILandroid/view/KeyEvent;)Z
    .locals 0

    .line 74
    invoke-virtual {p3}, Landroid/view/KeyEvent;->getAction()I

    move-result p1

    if-nez p1, :cond_0

    const/16 p1, 0x42

    if-ne p2, p1, :cond_0

    .line 76
    iget-object p1, p0, Lnet/gogame/chat/ChatFragment$2;->val$self:Lnet/gogame/chat/ChatFragment;

    invoke-static {p1}, Lnet/gogame/chat/ChatFragment;->access$000(Lnet/gogame/chat/ChatFragment;)Lnet/gogame/chat/ChatAdapter;

    move-result-object p1

    invoke-virtual {p1}, Lnet/gogame/chat/ChatAdapter;->getChatContext()Lnet/gogame/chat/ChatContext;

    move-result-object p1

    iget-object p2, p0, Lnet/gogame/chat/ChatFragment$2;->val$editText:Landroid/widget/EditText;

    invoke-virtual {p2}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object p2

    invoke-virtual {p2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-interface {p1, p2}, Lnet/gogame/chat/ChatContext;->send(Ljava/lang/String;)V

    .line 77
    iget-object p1, p0, Lnet/gogame/chat/ChatFragment$2;->val$editText:Landroid/widget/EditText;

    const-string p2, ""

    invoke-virtual {p1, p2}, Landroid/widget/EditText;->setText(Ljava/lang/CharSequence;)V

    const/4 p1, 0x1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method
