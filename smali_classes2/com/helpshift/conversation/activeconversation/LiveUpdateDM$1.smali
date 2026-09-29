.class Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$1;
.super Lcom/helpshift/common/domain/F;
.source "LiveUpdateDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;)V
    .locals 0

    .line 46
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$1;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 49
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$1;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->webSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    if-eqz v0, :cond_1

    .line 50
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$1;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-boolean v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->isConnecting:Z

    if-eqz v0, :cond_0

    .line 51
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$1;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    const/4 v1, 0x1

    iput-boolean v1, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->shouldDisconnectOnConnect:Z

    goto :goto_1

    :cond_0
    :try_start_0
    const-string v0, "Helpshift_LiveUpdateDM"

    const-string v1, "Disconnecting web-socket"

    .line 55
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 56
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$1;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->webSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    invoke-virtual {v0}, Lcom/helpshift/common/platform/network/websockets/HSWebSocket;->disconnect()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "Helpshift_LiveUpdateDM"

    const-string v2, "Exception in disconnecting web-socket"

    .line 59
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 61
    :goto_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$1;->this$0:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;

    const/4 v1, 0x0

    iput-object v1, v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->webSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;

    :cond_1
    :goto_1
    return-void
.end method
