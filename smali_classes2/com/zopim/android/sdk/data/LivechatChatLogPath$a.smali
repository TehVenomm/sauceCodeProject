.class Lcom/zopim/android/sdk/data/LivechatChatLogPath$a;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/data/LivechatChatLogPath;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = "a"
.end annotation


# instance fields
.field a:Ljava/lang/String;

.field b:Lcom/zopim/android/sdk/model/ChatLog;

.field final synthetic c:Lcom/zopim/android/sdk/data/LivechatChatLogPath;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/data/LivechatChatLogPath;Ljava/lang/String;Lcom/zopim/android/sdk/model/ChatLog;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$a;->c:Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iput-object p3, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$a;->b:Lcom/zopim/android/sdk/model/ChatLog;

    iput-object p2, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$a;->a:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->access$000()Ljava/lang/String;

    move-result-object v0

    const-string v1, "Message failed to send. Timeout occurred"

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    iget-object v0, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$a;->b:Lcom/zopim/android/sdk/model/ChatLog;

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/model/ChatLog;->setFailed(Z)V

    new-instance v0, Ljava/util/LinkedHashMap;

    invoke-direct {v0, v1}, Ljava/util/LinkedHashMap;-><init>(I)V

    iget-object v1, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$a;->a:Ljava/lang/String;

    iget-object v2, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$a;->b:Lcom/zopim/android/sdk/model/ChatLog;

    invoke-virtual {v0, v1, v2}, Ljava/util/LinkedHashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    iget-object v1, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$a;->c:Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    invoke-static {v1, v0}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->access$200(Lcom/zopim/android/sdk/data/LivechatChatLogPath;Ljava/util/LinkedHashMap;)V

    return-void
.end method
