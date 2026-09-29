.class Lcom/zopim/android/sdk/chatlog/am;
.super Lcom/zopim/android/sdk/data/observers/ChatLogObserver;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/am;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/observers/ChatLogObserver;-><init>()V

    return-void
.end method


# virtual methods
.method public update(Ljava/util/LinkedHashMap;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/LinkedHashMap<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/ChatLog;",
            ">;)V"
        }
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/am;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$1400(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroid/os/Handler;

    move-result-object v0

    new-instance v1, Lcom/zopim/android/sdk/chatlog/an;

    invoke-direct {v1, p0, p1}, Lcom/zopim/android/sdk/chatlog/an;-><init>(Lcom/zopim/android/sdk/chatlog/am;Ljava/util/LinkedHashMap;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
