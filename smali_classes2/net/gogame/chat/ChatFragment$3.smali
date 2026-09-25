.class Lnet/gogame/chat/ChatFragment$3;
.super Ljava/lang/Object;
.source "ChatFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


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

.field final synthetic val$sendButton:Landroid/widget/ImageButton;


# direct methods
.method constructor <init>(Lnet/gogame/chat/ChatFragment;Landroid/widget/ImageButton;Landroid/widget/EditText;Lnet/gogame/chat/ChatFragment;)V
    .locals 0

    .line 85
    iput-object p1, p0, Lnet/gogame/chat/ChatFragment$3;->this$0:Lnet/gogame/chat/ChatFragment;

    iput-object p2, p0, Lnet/gogame/chat/ChatFragment$3;->val$sendButton:Landroid/widget/ImageButton;

    iput-object p3, p0, Lnet/gogame/chat/ChatFragment$3;->val$editText:Landroid/widget/EditText;

    iput-object p4, p0, Lnet/gogame/chat/ChatFragment$3;->val$self:Lnet/gogame/chat/ChatFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2

    .line 89
    iget-object p1, p0, Lnet/gogame/chat/ChatFragment$3;->this$0:Lnet/gogame/chat/ChatFragment;

    iget-object v0, p0, Lnet/gogame/chat/ChatFragment$3;->val$sendButton:Landroid/widget/ImageButton;

    iget-object v1, p0, Lnet/gogame/chat/ChatFragment$3;->val$editText:Landroid/widget/EditText;

    invoke-virtual {v1}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v1

    invoke-static {p1, v0, v1}, Lnet/gogame/chat/ChatFragment;->access$100(Lnet/gogame/chat/ChatFragment;Landroid/widget/ImageButton;Landroid/text/Editable;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 90
    iget-object p1, p0, Lnet/gogame/chat/ChatFragment$3;->val$self:Lnet/gogame/chat/ChatFragment;

    invoke-static {p1}, Lnet/gogame/chat/ChatFragment;->access$000(Lnet/gogame/chat/ChatFragment;)Lnet/gogame/chat/ChatAdapter;

    move-result-object p1

    invoke-virtual {p1}, Lnet/gogame/chat/ChatAdapter;->getChatContext()Lnet/gogame/chat/ChatContext;

    move-result-object p1

    iget-object v0, p0, Lnet/gogame/chat/ChatFragment$3;->val$editText:Landroid/widget/EditText;

    invoke-virtual {v0}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-interface {p1, v0}, Lnet/gogame/chat/ChatContext;->send(Ljava/lang/String;)V

    .line 91
    iget-object p1, p0, Lnet/gogame/chat/ChatFragment$3;->val$editText:Landroid/widget/EditText;

    const-string v0, ""

    invoke-virtual {p1, v0}, Landroid/widget/EditText;->setText(Ljava/lang/CharSequence;)V

    goto :goto_0

    .line 99
    :cond_0
    new-instance p1, Landroid/content/Intent;

    invoke-direct {p1}, Landroid/content/Intent;-><init>()V

    const-string v0, "image/*"

    .line 100
    invoke-virtual {p1, v0}, Landroid/content/Intent;->setType(Ljava/lang/String;)Landroid/content/Intent;

    const-string v0, "android.intent.action.GET_CONTENT"

    .line 101
    invoke-virtual {p1, v0}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    .line 102
    iget-object v0, p0, Lnet/gogame/chat/ChatFragment$3;->this$0:Lnet/gogame/chat/ChatFragment;

    const-string v1, "Select picture"

    invoke-static {p1, v1}, Landroid/content/Intent;->createChooser(Landroid/content/Intent;Ljava/lang/CharSequence;)Landroid/content/Intent;

    move-result-object p1

    const/16 v1, 0x1389

    invoke-virtual {v0, p1, v1}, Lnet/gogame/chat/ChatFragment;->startActivityForResult(Landroid/content/Intent;I)V

    :goto_0
    return-void
.end method
