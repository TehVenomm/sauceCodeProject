.class public final enum Lcom/helpshift/conversation/states/ConversationCSATState;
.super Ljava/lang/Enum;
.source "ConversationCSATState.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/conversation/states/ConversationCSATState;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/conversation/states/ConversationCSATState;

.field public static final enum NONE:Lcom/helpshift/conversation/states/ConversationCSATState;

.field public static final enum SUBMITTED_NOT_SYNCED:Lcom/helpshift/conversation/states/ConversationCSATState;

.field public static final enum SUBMITTED_SYNCED:Lcom/helpshift/conversation/states/ConversationCSATState;


# instance fields
.field private final value:I


# direct methods
.method static constructor <clinit>()V
    .locals 5

    .line 5
    new-instance v0, Lcom/helpshift/conversation/states/ConversationCSATState;

    const-string v1, "NONE"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2, v2}, Lcom/helpshift/conversation/states/ConversationCSATState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/states/ConversationCSATState;->NONE:Lcom/helpshift/conversation/states/ConversationCSATState;

    .line 7
    new-instance v0, Lcom/helpshift/conversation/states/ConversationCSATState;

    const-string v1, "SUBMITTED_NOT_SYNCED"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3, v3}, Lcom/helpshift/conversation/states/ConversationCSATState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/states/ConversationCSATState;->SUBMITTED_NOT_SYNCED:Lcom/helpshift/conversation/states/ConversationCSATState;

    .line 9
    new-instance v0, Lcom/helpshift/conversation/states/ConversationCSATState;

    const-string v1, "SUBMITTED_SYNCED"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4, v4}, Lcom/helpshift/conversation/states/ConversationCSATState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/states/ConversationCSATState;->SUBMITTED_SYNCED:Lcom/helpshift/conversation/states/ConversationCSATState;

    const/4 v0, 0x3

    .line 3
    new-array v0, v0, [Lcom/helpshift/conversation/states/ConversationCSATState;

    sget-object v1, Lcom/helpshift/conversation/states/ConversationCSATState;->NONE:Lcom/helpshift/conversation/states/ConversationCSATState;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/conversation/states/ConversationCSATState;->SUBMITTED_NOT_SYNCED:Lcom/helpshift/conversation/states/ConversationCSATState;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/conversation/states/ConversationCSATState;->SUBMITTED_SYNCED:Lcom/helpshift/conversation/states/ConversationCSATState;

    aput-object v1, v0, v4

    sput-object v0, Lcom/helpshift/conversation/states/ConversationCSATState;->$VALUES:[Lcom/helpshift/conversation/states/ConversationCSATState;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;II)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)V"
        }
    .end annotation

    .line 13
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    .line 14
    iput p3, p0, Lcom/helpshift/conversation/states/ConversationCSATState;->value:I

    return-void
.end method

.method public static fromInt(I)Lcom/helpshift/conversation/states/ConversationCSATState;
    .locals 5

    .line 20
    invoke-static {}, Lcom/helpshift/conversation/states/ConversationCSATState;->values()[Lcom/helpshift/conversation/states/ConversationCSATState;

    move-result-object v0

    array-length v1, v0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_1

    aget-object v3, v0, v2

    .line 21
    iget v4, v3, Lcom/helpshift/conversation/states/ConversationCSATState;->value:I

    if-ne v4, p0, :cond_0

    goto :goto_1

    :cond_0
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_1
    const/4 v3, 0x0

    :goto_1
    if-nez v3, :cond_2

    .line 27
    sget-object v3, Lcom/helpshift/conversation/states/ConversationCSATState;->NONE:Lcom/helpshift/conversation/states/ConversationCSATState;

    :cond_2
    return-object v3
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/conversation/states/ConversationCSATState;
    .locals 1

    .line 3
    const-class v0, Lcom/helpshift/conversation/states/ConversationCSATState;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/conversation/states/ConversationCSATState;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/conversation/states/ConversationCSATState;
    .locals 1

    .line 3
    sget-object v0, Lcom/helpshift/conversation/states/ConversationCSATState;->$VALUES:[Lcom/helpshift/conversation/states/ConversationCSATState;

    invoke-virtual {v0}, [Lcom/helpshift/conversation/states/ConversationCSATState;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/conversation/states/ConversationCSATState;

    return-object v0
.end method


# virtual methods
.method public getValue()I
    .locals 1

    .line 31
    iget v0, p0, Lcom/helpshift/conversation/states/ConversationCSATState;->value:I

    return v0
.end method
