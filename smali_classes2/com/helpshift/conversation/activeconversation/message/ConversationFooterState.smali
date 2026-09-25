.class public final enum Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;
.super Ljava/lang/Enum;
.source "ConversationFooterState.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

.field public static final enum ARCHIVAL_MESSAGE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

.field public static final enum AUTHOR_MISMATCH:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

.field public static final enum CONVERSATION_ENDED_MESSAGE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

.field public static final enum CSAT_RATING:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

.field public static final enum NONE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

.field public static final enum REDACTED_STATE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

.field public static final enum REJECTED_MESSAGE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

.field public static final enum START_NEW_CONVERSATION:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;


# direct methods
.method static constructor <clinit>()V
    .locals 10

    .line 5
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    const-string v1, "NONE"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->NONE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    .line 7
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    const-string v1, "CONVERSATION_ENDED_MESSAGE"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->CONVERSATION_ENDED_MESSAGE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    .line 9
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    const-string v1, "START_NEW_CONVERSATION"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->START_NEW_CONVERSATION:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    .line 11
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    const-string v1, "CSAT_RATING"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5}, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->CSAT_RATING:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    .line 13
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    const-string v1, "ARCHIVAL_MESSAGE"

    const/4 v6, 0x4

    invoke-direct {v0, v1, v6}, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->ARCHIVAL_MESSAGE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    .line 15
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    const-string v1, "AUTHOR_MISMATCH"

    const/4 v7, 0x5

    invoke-direct {v0, v1, v7}, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->AUTHOR_MISMATCH:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    .line 17
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    const-string v1, "REJECTED_MESSAGE"

    const/4 v8, 0x6

    invoke-direct {v0, v1, v8}, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->REJECTED_MESSAGE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    .line 19
    new-instance v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    const-string v1, "REDACTED_STATE"

    const/4 v9, 0x7

    invoke-direct {v0, v1, v9}, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->REDACTED_STATE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    const/16 v0, 0x8

    .line 3
    new-array v0, v0, [Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->NONE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->CONVERSATION_ENDED_MESSAGE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->START_NEW_CONVERSATION:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    aput-object v1, v0, v4

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->CSAT_RATING:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    aput-object v1, v0, v5

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->ARCHIVAL_MESSAGE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    aput-object v1, v0, v6

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->AUTHOR_MISMATCH:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    aput-object v1, v0, v7

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->REJECTED_MESSAGE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    aput-object v1, v0, v8

    sget-object v1, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->REDACTED_STATE:Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    aput-object v1, v0, v9

    sput-object v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->$VALUES:[Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 3
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;
    .locals 1

    .line 3
    const-class v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;
    .locals 1

    .line 3
    sget-object v0, Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->$VALUES:[Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    invoke-virtual {v0}, [Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/conversation/activeconversation/message/ConversationFooterState;

    return-object v0
.end method
