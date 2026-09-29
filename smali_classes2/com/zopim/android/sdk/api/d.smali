.class Lcom/zopim/android/sdk/api/d;
.super Lcom/zopim/android/sdk/api/u;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/zopim/android/sdk/api/u<",
        "Ljava/lang/Void;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/model/ChatLog;

.field final synthetic b:Lcom/zopim/android/sdk/api/c;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/api/c;Lcom/zopim/android/sdk/model/ChatLog;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/d;->b:Lcom/zopim/android/sdk/api/c;

    iput-object p2, p0, Lcom/zopim/android/sdk/api/d;->a:Lcom/zopim/android/sdk/model/ChatLog;

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/u;-><init>()V

    return-void
.end method


# virtual methods
.method public a(Lcom/zopim/android/sdk/api/ErrorResponse;)V
    .locals 1

    iget-object p1, p0, Lcom/zopim/android/sdk/api/d;->a:Lcom/zopim/android/sdk/model/ChatLog;

    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Error;->UPLOAD_FAILED_ERROR:Lcom/zopim/android/sdk/model/ChatLog$Error;

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/model/ChatLog;->setError(Lcom/zopim/android/sdk/model/ChatLog$Error;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/api/d;->a:Lcom/zopim/android/sdk/model/ChatLog;

    const/4 v0, 0x1

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/model/ChatLog;->setFailed(Z)V

    sget-object p1, Lcom/zopim/android/sdk/api/FileTransfers;->INSTANCE:Lcom/zopim/android/sdk/api/FileTransfers;

    iget-object p1, p1, Lcom/zopim/android/sdk/api/FileTransfers;->mTransfers:Ljava/util/Map;

    iget-object v0, p0, Lcom/zopim/android/sdk/api/d;->a:Lcom/zopim/android/sdk/model/ChatLog;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getFileName()Ljava/lang/String;

    move-result-object v0

    invoke-interface {p1, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/api/FileTransfers$a;

    sget-object v0, Lcom/zopim/android/sdk/api/FileTransfers$b;->e:Lcom/zopim/android/sdk/api/FileTransfers$b;

    iput-object v0, p1, Lcom/zopim/android/sdk/api/FileTransfers$a;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    move-result-object p1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->broadcast()V

    return-void
.end method

.method public bridge synthetic a(Ljava/lang/Object;)V
    .locals 0

    check-cast p1, Ljava/lang/Void;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/api/d;->a(Ljava/lang/Void;)V

    return-void
.end method

.method public a(Ljava/lang/Void;)V
    .locals 1

    invoke-static {}, Lcom/zopim/android/sdk/api/ChatService;->access$200()Ljava/lang/String;

    move-result-object p1

    const-string v0, "Upload completed"

    invoke-static {p1, v0}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
