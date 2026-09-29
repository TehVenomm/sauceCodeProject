.class Lcom/zopim/android/sdk/widget/f;
.super Lcom/zopim/android/sdk/data/observers/ChatLogObserver;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/widget/ChatWidgetService;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/widget/f;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/observers/ChatLogObserver;-><init>()V

    return-void
.end method


# virtual methods
.method public update(Ljava/util/LinkedHashMap;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/LinkedHashMap<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/ChatLog;",
            ">;)V"
        }
    .end annotation

    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    move-result-object p1

    const/4 v0, 0x1

    new-array v0, v0, [Lcom/zopim/android/sdk/model/ChatLog$Type;

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_AGENT:Lcom/zopim/android/sdk/model/ChatLog$Type;

    const/4 v2, 0x0

    aput-object v1, v0, v2

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->countMessages([Lcom/zopim/android/sdk/model/ChatLog$Type;)I

    move-result p1

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/f;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$1600(Lcom/zopim/android/sdk/widget/ChatWidgetService;)I

    move-result v0

    sub-int/2addr p1, v0

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/f;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$1500(Lcom/zopim/android/sdk/widget/ChatWidgetService;)I

    move-result v0

    if-le p1, v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/f;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0, p1}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$1502(Lcom/zopim/android/sdk/widget/ChatWidgetService;I)I

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/f;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$1200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/os/Handler;

    move-result-object v0

    new-instance v1, Lcom/zopim/android/sdk/widget/g;

    invoke-direct {v1, p0, p1}, Lcom/zopim/android/sdk/widget/g;-><init>(Lcom/zopim/android/sdk/widget/f;I)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    :cond_0
    return-void
.end method
