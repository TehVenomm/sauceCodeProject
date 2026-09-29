.class Lcom/zopim/android/sdk/chatlog/ai;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/text/TextWatcher;


# instance fields
.field final synthetic a:Landroid/widget/Button;

.field final synthetic b:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;Landroid/widget/Button;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ai;->b:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/ai;->a:Landroid/widget/Button;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public afterTextChanged(Landroid/text/Editable;)V
    .locals 1

    invoke-interface {p1}, Landroid/text/Editable;->length()I

    move-result p1

    if-lez p1, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ai;->b:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$600(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Z

    move-result p1

    if-nez p1, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ai;->a:Landroid/widget/Button;

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ai;->a:Landroid/widget/Button;

    const/4 v0, 0x0

    :goto_0
    invoke-virtual {p1, v0}, Landroid/widget/Button;->setEnabled(Z)V

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
