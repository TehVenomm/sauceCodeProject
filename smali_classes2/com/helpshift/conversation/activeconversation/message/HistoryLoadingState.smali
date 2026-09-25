.class public final enum Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;
.super Ljava/lang/Enum;
.source "HistoryLoadingState.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

.field public static final enum ERROR:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

.field public static final enum LOADING:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

.field public static final enum NONE:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;


# direct methods
.method static constructor <clinit>()V
    .locals 5

    .line 8
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    const-string v1, "NONE"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->NONE:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    .line 10
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    const-string v1, "LOADING"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->LOADING:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    .line 12
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    const-string v1, "ERROR"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->ERROR:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    const/4 v0, 0x3

    .line 6
    new-array v0, v0, [Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->NONE:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->LOADING:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->ERROR:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    aput-object v1, v0, v4

    sput-object v0, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->$VALUES:[Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 6
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;
    .locals 1

    .line 6
    const-class v0, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;
    .locals 1

    .line 6
    sget-object v0, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->$VALUES:[Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    invoke-virtual {v0}, [Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    return-object v0
.end method
