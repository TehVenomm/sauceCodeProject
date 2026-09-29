.class public Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;
.super Ljava/lang/Object;
.source "LiveUpdateDM.java"

# interfaces
.implements Lcom/helpshift/common/platform/network/websockets/IHSWebSocketListener;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;,
        Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$PingTimeoutF;,
        Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$StartTypingTimeoutF;,
        Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;,
        Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;
    }
.end annotation


# static fields
.field public static final MESSAGE_TYPE_EVENT:I = 0x64

.field public static final MESSAGE_TYPE_PING:I = 0x6b

.field private static final TAG:Ljava/lang/String; = "Helpshift_LiveUpdateDM"


# instance fields
.field private final MESSAGE_TYPE_PONG:Ljava/lang/String;

.field final NETWORK_PROPOGATION_DELAY:J

.field private final SDK_VERSION_HEADER_KEY:Ljava/lang/String;

.field private conversationServerId:Ljava/lang/String;

.field disconnectWebSocketF:Lcom/helpshift/common/domain/F;

.field domain:Lcom/helpshift/common/domain/Domain;

.field isAgentTyping:Z

.field isConnecting:Z

.field isTokenRefreshed:Z

.field listener:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;

.field pingCount:Ljava/util/concurrent/atomic/AtomicInteger;

.field platform:Lcom/helpshift/common/platform/Platform;

.field private refreshAuthTokenAndConnectF:Lcom/helpshift/common/domain/F;

.field final sdkVersionHeaderValue:Ljava/lang/String;

.field shouldDisconnectOnConnect:Z

.field socketConnected:Z

.field startTypingId:Ljava/util/concurrent/atomic/AtomicInteger;

.field webSocket:Lcom/helpshift/common/platform/network/websockets/HSWebSocket;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/domain/Domain;Lcom/helpshift/common/platform/Platform;)V
    .locals 3

    .line 82
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 28
    sget-object v0, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v1, 0x3

    invoke-virtual {v0, v1, v2}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v0

    iput-wide v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->NETWORK_PROPOGATION_DELAY:J

    const-string v0, "[110]"

    .line 30
    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->MESSAGE_TYPE_PONG:Ljava/lang/String;

    const-string v0, "hs-sdk-ver"

    .line 31
    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->SDK_VERSION_HEADER_KEY:Ljava/lang/String;

    .line 46
    new-instance v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$1;

    invoke-direct {v0, p0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$1;-><init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;)V

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->disconnectWebSocketF:Lcom/helpshift/common/domain/F;

    .line 71
    new-instance v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$2;

    invoke-direct {v0, p0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$2;-><init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;)V

    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->refreshAuthTokenAndConnectF:Lcom/helpshift/common/domain/F;

    .line 83
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->domain:Lcom/helpshift/common/domain/Domain;

    .line 84
    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->platform:Lcom/helpshift/common/platform/Platform;

    .line 85
    new-instance p1, Ljava/util/concurrent/atomic/AtomicInteger;

    const/4 v0, -0x1

    invoke-direct {p1, v0}, Ljava/util/concurrent/atomic/AtomicInteger;-><init>(I)V

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->pingCount:Ljava/util/concurrent/atomic/AtomicInteger;

    .line 86
    new-instance p1, Ljava/util/concurrent/atomic/AtomicInteger;

    invoke-direct {p1, v0}, Ljava/util/concurrent/atomic/AtomicInteger;-><init>(I)V

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->startTypingId:Ljava/util/concurrent/atomic/AtomicInteger;

    .line 87
    invoke-interface {p2}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object p1

    .line 88
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-interface {p1}, Lcom/helpshift/common/platform/Device;->getPlatformName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p2, v0}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    const-string v0, "-"

    .line 89
    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 90
    invoke-interface {p1}, Lcom/helpshift/common/platform/Device;->getSDKVersion()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 91
    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->sdkVersionHeaderValue:Ljava/lang/String;

    return-void
.end method

.method private getErrorCode(Ljava/lang/String;)I
    .locals 3

    const-string v0, "The status line is: "

    .line 180
    invoke-virtual {p1, v0}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p1

    .line 181
    array-length v0, p1

    const/4 v1, 0x2

    if-ne v1, v0, :cond_0

    const/4 v0, 0x1

    .line 182
    aget-object p1, p1, v0

    const-string v2, " +"

    invoke-virtual {p1, v2}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object p1

    .line 183
    array-length v2, p1

    if-lt v2, v1, :cond_0

    .line 184
    aget-object p1, p1, v0

    const-string v0, "403"

    .line 185
    invoke-virtual {v0, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_0

    const/16 p1, 0x193

    return p1

    :cond_0
    const/4 p1, -0x1

    return p1
.end method

.method private getTopicRequest()Ljava/lang/String;
    .locals 2

    .line 196
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "[104, [\""

    .line 197
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "agent_type_act.issue."

    .line 198
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->conversationServerId:Ljava/lang/String;

    .line 199
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "\"]]"

    .line 200
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 201
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method


# virtual methods
.method getWebSocketPath(Lcom/helpshift/auth/dto/WebSocketAuthData;)Ljava/lang/String;
    .locals 6

    .line 205
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getAppId()Ljava/lang/String;

    move-result-object v0

    .line 206
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->platform:Lcom/helpshift/common/platform/Platform;

    invoke-interface {v1}, Lcom/helpshift/common/platform/Platform;->getDomain()Ljava/lang/String;

    move-result-object v1

    const-string v2, "\\."

    invoke-virtual {v1, v2}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v1

    const-string v2, ""

    .line 208
    array-length v3, v1

    const/4 v4, 0x3

    if-ne v3, v4, :cond_0

    const/4 v2, 0x0

    .line 209
    aget-object v2, v1, v2

    :cond_0
    const-string v1, ""

    .line 213
    :try_start_0
    iget-object v3, p1, Lcom/helpshift/auth/dto/WebSocketAuthData;->authToken:Ljava/lang/String;

    const-string v4, "UTF-8"

    invoke-static {v3, v4}, Ljava/net/URLEncoder;->encode(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0

    move-object v1, v3

    goto :goto_0

    :catch_0
    move-exception v3

    const-string v4, "Helpshift_LiveUpdateDM"

    const-string v5, "Exception in encoding authToken"

    .line 216
    invoke-static {v4, v5, v3}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 219
    :goto_0
    invoke-static {v1}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_2

    iget-object v3, p1, Lcom/helpshift/auth/dto/WebSocketAuthData;->webSocketRoute:Ljava/lang/String;

    .line 220
    invoke-static {v3}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_1

    goto :goto_1

    .line 227
    :cond_1
    new-instance v3, Ljava/lang/StringBuilder;

    iget-object p1, p1, Lcom/helpshift/auth/dto/WebSocketAuthData;->webSocketRoute:Ljava/lang/String;

    invoke-direct {v3, p1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    const-string p1, "/subscribe/websocket/?origin_v3="

    .line 228
    invoke-virtual {v3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 229
    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "&platform_id="

    .line 230
    invoke-virtual {v3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 231
    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "&domain="

    .line 232
    invoke-virtual {v3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 233
    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 234
    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1

    :cond_2
    :goto_1
    const/4 p1, 0x0

    return-object p1
.end method

.method isAgentTyping()Z
    .locals 1

    .line 95
    iget-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->isAgentTyping:Z

    return v0
.end method

.method notifyListener()V
    .locals 2

    .line 242
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->listener:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;

    if-eqz v0, :cond_0

    .line 243
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->listener:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;

    iget-boolean v1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->isAgentTyping:Z

    invoke-interface {v0, v1}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;->onAgentTypingUpdate(Z)V

    :cond_0
    return-void
.end method

.method public onConnected(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;)V
    .locals 4

    const-string v0, "Helpshift_LiveUpdateDM"

    const-string v1, "web-socket connected"

    .line 130
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 131
    iput-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->isConnecting:Z

    const/4 v0, 0x1

    .line 132
    iput-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->socketConnected:Z

    .line 133
    iget-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->shouldDisconnectOnConnect:Z

    if-eqz v0, :cond_0

    .line 134
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->disconnectWebSocketF:Lcom/helpshift/common/domain/F;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/F;->f()V

    goto :goto_0

    .line 136
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->listener:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;

    if-eqz v0, :cond_1

    const-string v0, "Helpshift_LiveUpdateDM"

    const-string v1, "Subscribing to conversation topic"

    .line 137
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 138
    invoke-direct {p0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->getTopicRequest()Ljava/lang/String;

    move-result-object v0

    .line 139
    invoke-virtual {p1, v0}, Lcom/helpshift/common/platform/network/websockets/HSWebSocket;->sendMessage(Ljava/lang/String;)V

    .line 143
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$PingTimeoutF;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->pingCount:Ljava/util/concurrent/atomic/AtomicInteger;

    invoke-virtual {v1}, Ljava/util/concurrent/atomic/AtomicInteger;->incrementAndGet()I

    move-result v1

    invoke-direct {v0, p0, v1}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$PingTimeoutF;-><init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;I)V

    sget-object v1, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v2, 0x3c

    invoke-virtual {v1, v2, v3}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v1

    invoke-virtual {p1, v0, v1, v2}, Lcom/helpshift/common/domain/Domain;->runDelayedInParallel(Lcom/helpshift/common/domain/F;J)V

    goto :goto_0

    .line 146
    :cond_1
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->disconnectWebSocketF:Lcom/helpshift/common/domain/F;

    invoke-virtual {p1}, Lcom/helpshift/common/domain/F;->f()V

    :goto_0
    return-void
.end method

.method public onDisconnected()V
    .locals 2

    const-string v0, "Helpshift_LiveUpdateDM"

    const-string v1, "web-socket disconnected"

    .line 152
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 153
    iput-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->socketConnected:Z

    .line 154
    iput-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->shouldDisconnectOnConnect:Z

    return-void
.end method

.method public onError(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;Ljava/lang/String;)V
    .locals 2

    const-string p1, "Helpshift_LiveUpdateDM"

    .line 164
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Error in web-socket connection: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    const/4 p1, 0x0

    .line 165
    iput-boolean p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->isConnecting:Z

    .line 166
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->listener:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;

    if-eqz p1, :cond_1

    .line 167
    invoke-direct {p0, p2}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->getErrorCode(Ljava/lang/String;)I

    move-result p1

    const/16 p2, 0x193

    if-ne p1, p2, :cond_0

    .line 168
    iget-boolean p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->isTokenRefreshed:Z

    if-nez p1, :cond_1

    .line 169
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object p2, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->refreshAuthTokenAndConnectF:Lcom/helpshift/common/domain/F;

    invoke-virtual {p1, p2}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    goto :goto_0

    .line 173
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->scheduleConnectionRetry()V

    :cond_1
    :goto_0
    return-void
.end method

.method public onMessage(Lcom/helpshift/common/platform/network/websockets/HSWebSocket;Ljava/lang/String;)V
    .locals 1

    .line 159
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;

    invoke-direct {v0, p0, p2}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$HandleWebSocketMessageF;-><init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;Ljava/lang/String;)V

    invoke-virtual {p1, v0}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method declared-synchronized registerListener(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;Ljava/lang/String;)V
    .locals 1

    monitor-enter p0

    .line 105
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->listener:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;

    if-nez v0, :cond_0

    .line 106
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->listener:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;

    .line 107
    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->conversationServerId:Ljava/lang/String;

    const/4 p1, 0x0

    .line 108
    iput-boolean p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->isTokenRefreshed:Z

    .line 109
    iput-boolean p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->shouldDisconnectOnConnect:Z

    .line 110
    iget-object p1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance p2, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->pingCount:Ljava/util/concurrent/atomic/AtomicInteger;

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicInteger;->incrementAndGet()I

    move-result v0

    invoke-direct {p2, p0, v0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;-><init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;I)V

    invoke-virtual {p1, p2}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 112
    :cond_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 104
    monitor-exit p0

    throw p1
.end method

.method scheduleConnectionRetry()V
    .locals 5

    .line 238
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->domain:Lcom/helpshift/common/domain/Domain;

    new-instance v1, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->pingCount:Ljava/util/concurrent/atomic/AtomicInteger;

    invoke-virtual {v2}, Ljava/util/concurrent/atomic/AtomicInteger;->incrementAndGet()I

    move-result v2

    invoke-direct {v1, p0, v2}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$ConnectWebSocketF;-><init>(Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;I)V

    sget-object v2, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    const-wide/16 v3, 0xa

    invoke-virtual {v2, v3, v4}, Ljava/util/concurrent/TimeUnit;->toMillis(J)J

    move-result-wide v2

    invoke-virtual {v0, v1, v2, v3}, Lcom/helpshift/common/domain/Domain;->runDelayedInParallel(Lcom/helpshift/common/domain/F;J)V

    return-void
.end method

.method declared-synchronized unregisterListener()V
    .locals 2

    monitor-enter p0

    .line 115
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->listener:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;

    if-eqz v0, :cond_0

    const/4 v0, 0x0

    .line 116
    iput-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->isAgentTyping:Z

    .line 117
    invoke-virtual {p0}, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->notifyListener()V

    .line 120
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->startTypingId:Ljava/util/concurrent/atomic/AtomicInteger;

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicInteger;->incrementAndGet()I

    .line 122
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->pingCount:Ljava/util/concurrent/atomic/AtomicInteger;

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicInteger;->incrementAndGet()I

    const/4 v0, 0x0

    .line 123
    iput-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->listener:Lcom/helpshift/conversation/activeconversation/LiveUpdateDM$TypingIndicatorListener;

    .line 125
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->domain:Lcom/helpshift/common/domain/Domain;

    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/LiveUpdateDM;->disconnectWebSocketF:Lcom/helpshift/common/domain/F;

    invoke-virtual {v0, v1}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 126
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 114
    monitor-exit p0

    throw v0
.end method
