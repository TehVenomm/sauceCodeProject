.class Lcom/zopim/android/sdk/chatlog/ak;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/aj;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/aj;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ak;->a:Lcom/zopim/android/sdk/chatlog/aj;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/content/DialogInterface;I)V
    .locals 0

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ak;->a:Lcom/zopim/android/sdk/chatlog/aj;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/aj;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$300(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object p1

    invoke-interface {p1}, Lcom/zopim/android/sdk/api/Chat;->endChat()V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ak;->a:Lcom/zopim/android/sdk/chatlog/aj;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/aj;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$800(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ak;->a:Lcom/zopim/android/sdk/chatlog/aj;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/aj;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$900(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Lcom/zopim/android/sdk/prechat/ChatListener;

    move-result-object p1

    if-eqz p1, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ak;->a:Lcom/zopim/android/sdk/chatlog/aj;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/aj;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$900(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Lcom/zopim/android/sdk/prechat/ChatListener;

    move-result-object p1

    invoke-interface {p1}, Lcom/zopim/android/sdk/prechat/ChatListener;->onChatEnded()V

    :cond_0
    return-void
.end method
