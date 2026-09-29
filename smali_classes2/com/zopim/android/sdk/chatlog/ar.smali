.class Lcom/zopim/android/sdk/chatlog/ar;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnClickListener;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ar;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ar;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$400(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroid/widget/EditText;

    move-result-object p1

    invoke-virtual {p1}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ar;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$300(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->send(Ljava/lang/String;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ar;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$400(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroid/widget/EditText;

    move-result-object p1

    const-string v0, ""

    invoke-virtual {p1, v0}, Landroid/widget/EditText;->setText(Ljava/lang/CharSequence;)V

    :cond_0
    return-void
.end method
