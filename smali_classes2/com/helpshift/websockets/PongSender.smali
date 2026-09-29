.class Lcom/helpshift/websockets/PongSender;
.super Lcom/helpshift/websockets/PeriodicalFrameSender;
.source "PongSender.java"


# static fields
.field private static final TIMER_NAME:Ljava/lang/String; = "PongSender"


# direct methods
.method public constructor <init>(Lcom/helpshift/websockets/WebSocket;Lcom/helpshift/websockets/PayloadGenerator;)V
    .locals 1

    const-string v0, "PongSender"

    .line 30
    invoke-direct {p0, p1, v0, p2}, Lcom/helpshift/websockets/PeriodicalFrameSender;-><init>(Lcom/helpshift/websockets/WebSocket;Ljava/lang/String;Lcom/helpshift/websockets/PayloadGenerator;)V

    return-void
.end method


# virtual methods
.method protected createFrame([B)Lcom/helpshift/websockets/WebSocketFrame;
    .locals 0

    .line 36
    invoke-static {p1}, Lcom/helpshift/websockets/WebSocketFrame;->createPongFrame([B)Lcom/helpshift/websockets/WebSocketFrame;

    move-result-object p1

    return-object p1
.end method
