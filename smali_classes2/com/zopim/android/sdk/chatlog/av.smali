.class Lcom/zopim/android/sdk/chatlog/av;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/DialogInterface$OnClickListener;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/av;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/content/DialogInterface;I)V
    .locals 2

    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    move-result-object p1

    const/4 p2, 0x2

    new-array p2, p2, [Lcom/zopim/android/sdk/model/ChatLog$Type;

    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_VISITOR:Lcom/zopim/android/sdk/model/ChatLog$Type;

    const/4 v1, 0x0

    aput-object v0, p2, v1

    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_AGENT:Lcom/zopim/android/sdk/model/ChatLog$Type;

    const/4 v1, 0x1

    aput-object v0, p2, v1

    invoke-virtual {p1, p2}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->countMessages([Lcom/zopim/android/sdk/model/ChatLog$Type;)I

    move-result p1

    if-lez p1, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/av;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$600(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Z

    move-result p1

    if-nez p1, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/av;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$700(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    goto :goto_0

    :cond_0
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/av;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$300(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object p1

    invoke-interface {p1}, Lcom/zopim/android/sdk/api/Chat;->endChat()V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/av;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$800(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/av;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$900(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Lcom/zopim/android/sdk/prechat/ChatListener;

    move-result-object p1

    if-eqz p1, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/av;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$900(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Lcom/zopim/android/sdk/prechat/ChatListener;

    move-result-object p1

    invoke-interface {p1}, Lcom/zopim/android/sdk/prechat/ChatListener;->onChatEnded()V

    :cond_1
    :goto_0
    return-void
.end method
