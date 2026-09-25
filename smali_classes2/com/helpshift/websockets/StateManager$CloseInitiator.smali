.class final enum Lcom/helpshift/websockets/StateManager$CloseInitiator;
.super Ljava/lang/Enum;
.source "StateManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/websockets/StateManager;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4018
    name = "CloseInitiator"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/websockets/StateManager$CloseInitiator;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/websockets/StateManager$CloseInitiator;

.field public static final enum CLIENT:Lcom/helpshift/websockets/StateManager$CloseInitiator;

.field public static final enum NONE:Lcom/helpshift/websockets/StateManager$CloseInitiator;

.field public static final enum SERVER:Lcom/helpshift/websockets/StateManager$CloseInitiator;


# direct methods
.method static constructor <clinit>()V
    .locals 5

    .line 59
    new-instance v0, Lcom/helpshift/websockets/StateManager$CloseInitiator;

    const-string v1, "NONE"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/websockets/StateManager$CloseInitiator;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/websockets/StateManager$CloseInitiator;->NONE:Lcom/helpshift/websockets/StateManager$CloseInitiator;

    .line 60
    new-instance v0, Lcom/helpshift/websockets/StateManager$CloseInitiator;

    const-string v1, "SERVER"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/websockets/StateManager$CloseInitiator;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/websockets/StateManager$CloseInitiator;->SERVER:Lcom/helpshift/websockets/StateManager$CloseInitiator;

    .line 61
    new-instance v0, Lcom/helpshift/websockets/StateManager$CloseInitiator;

    const-string v1, "CLIENT"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/websockets/StateManager$CloseInitiator;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/websockets/StateManager$CloseInitiator;->CLIENT:Lcom/helpshift/websockets/StateManager$CloseInitiator;

    const/4 v0, 0x3

    .line 58
    new-array v0, v0, [Lcom/helpshift/websockets/StateManager$CloseInitiator;

    sget-object v1, Lcom/helpshift/websockets/StateManager$CloseInitiator;->NONE:Lcom/helpshift/websockets/StateManager$CloseInitiator;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/websockets/StateManager$CloseInitiator;->SERVER:Lcom/helpshift/websockets/StateManager$CloseInitiator;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/websockets/StateManager$CloseInitiator;->CLIENT:Lcom/helpshift/websockets/StateManager$CloseInitiator;

    aput-object v1, v0, v4

    sput-object v0, Lcom/helpshift/websockets/StateManager$CloseInitiator;->$VALUES:[Lcom/helpshift/websockets/StateManager$CloseInitiator;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 58
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/websockets/StateManager$CloseInitiator;
    .locals 1

    .line 58
    const-class v0, Lcom/helpshift/websockets/StateManager$CloseInitiator;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/websockets/StateManager$CloseInitiator;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/websockets/StateManager$CloseInitiator;
    .locals 1

    .line 58
    sget-object v0, Lcom/helpshift/websockets/StateManager$CloseInitiator;->$VALUES:[Lcom/helpshift/websockets/StateManager$CloseInitiator;

    invoke-virtual {v0}, [Lcom/helpshift/websockets/StateManager$CloseInitiator;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/websockets/StateManager$CloseInitiator;

    return-object v0
.end method
