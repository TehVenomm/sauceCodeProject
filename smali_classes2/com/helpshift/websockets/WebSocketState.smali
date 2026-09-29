.class public final enum Lcom/helpshift/websockets/WebSocketState;
.super Ljava/lang/Enum;
.source "WebSocketState.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/websockets/WebSocketState;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/websockets/WebSocketState;

.field public static final enum CLOSED:Lcom/helpshift/websockets/WebSocketState;

.field public static final enum CLOSING:Lcom/helpshift/websockets/WebSocketState;

.field public static final enum CONNECTING:Lcom/helpshift/websockets/WebSocketState;

.field public static final enum CREATED:Lcom/helpshift/websockets/WebSocketState;

.field public static final enum OPEN:Lcom/helpshift/websockets/WebSocketState;


# direct methods
.method static constructor <clinit>()V
    .locals 7

    .line 88
    new-instance v0, Lcom/helpshift/websockets/WebSocketState;

    const-string v1, "CREATED"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/websockets/WebSocketState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/websockets/WebSocketState;->CREATED:Lcom/helpshift/websockets/WebSocketState;

    .line 95
    new-instance v0, Lcom/helpshift/websockets/WebSocketState;

    const-string v1, "CONNECTING"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/websockets/WebSocketState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/websockets/WebSocketState;->CONNECTING:Lcom/helpshift/websockets/WebSocketState;

    .line 103
    new-instance v0, Lcom/helpshift/websockets/WebSocketState;

    const-string v1, "OPEN"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/websockets/WebSocketState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/websockets/WebSocketState;->OPEN:Lcom/helpshift/websockets/WebSocketState;

    .line 110
    new-instance v0, Lcom/helpshift/websockets/WebSocketState;

    const-string v1, "CLOSING"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5}, Lcom/helpshift/websockets/WebSocketState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/websockets/WebSocketState;->CLOSING:Lcom/helpshift/websockets/WebSocketState;

    .line 116
    new-instance v0, Lcom/helpshift/websockets/WebSocketState;

    const-string v1, "CLOSED"

    const/4 v6, 0x4

    invoke-direct {v0, v1, v6}, Lcom/helpshift/websockets/WebSocketState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/websockets/WebSocketState;->CLOSED:Lcom/helpshift/websockets/WebSocketState;

    const/4 v0, 0x5

    .line 84
    new-array v0, v0, [Lcom/helpshift/websockets/WebSocketState;

    sget-object v1, Lcom/helpshift/websockets/WebSocketState;->CREATED:Lcom/helpshift/websockets/WebSocketState;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/websockets/WebSocketState;->CONNECTING:Lcom/helpshift/websockets/WebSocketState;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/websockets/WebSocketState;->OPEN:Lcom/helpshift/websockets/WebSocketState;

    aput-object v1, v0, v4

    sget-object v1, Lcom/helpshift/websockets/WebSocketState;->CLOSING:Lcom/helpshift/websockets/WebSocketState;

    aput-object v1, v0, v5

    sget-object v1, Lcom/helpshift/websockets/WebSocketState;->CLOSED:Lcom/helpshift/websockets/WebSocketState;

    aput-object v1, v0, v6

    sput-object v0, Lcom/helpshift/websockets/WebSocketState;->$VALUES:[Lcom/helpshift/websockets/WebSocketState;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 84
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/websockets/WebSocketState;
    .locals 1

    .line 84
    const-class v0, Lcom/helpshift/websockets/WebSocketState;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/websockets/WebSocketState;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/websockets/WebSocketState;
    .locals 1

    .line 84
    sget-object v0, Lcom/helpshift/websockets/WebSocketState;->$VALUES:[Lcom/helpshift/websockets/WebSocketState;

    invoke-virtual {v0}, [Lcom/helpshift/websockets/WebSocketState;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/websockets/WebSocketState;

    return-object v0
.end method
