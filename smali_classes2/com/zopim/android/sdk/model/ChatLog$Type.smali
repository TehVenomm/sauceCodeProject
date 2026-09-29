.class public final enum Lcom/zopim/android/sdk/model/ChatLog$Type;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/model/ChatLog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "Type"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/model/ChatLog$Type;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/zopim/android/sdk/model/ChatLog$Type;

.field public static final enum ATTACHMENT_UPLOAD:Lcom/zopim/android/sdk/model/ChatLog$Type;

.field public static final enum CHAT_MSG_AGENT:Lcom/zopim/android/sdk/model/ChatLog$Type;

.field public static final enum CHAT_MSG_SYSTEM:Lcom/zopim/android/sdk/model/ChatLog$Type;

.field public static final enum CHAT_MSG_TRIGGER:Lcom/zopim/android/sdk/model/ChatLog$Type;

.field public static final enum CHAT_MSG_VISITOR:Lcom/zopim/android/sdk/model/ChatLog$Type;

.field public static final enum CHAT_RATING:Lcom/zopim/android/sdk/model/ChatLog$Type;

.field public static final enum MEMBER_JOIN:Lcom/zopim/android/sdk/model/ChatLog$Type;

.field public static final enum MEMBER_LEAVE:Lcom/zopim/android/sdk/model/ChatLog$Type;

.field public static final enum SYSTEM_OFFLINE:Lcom/zopim/android/sdk/model/ChatLog$Type;

.field public static final enum UNKNOWN:Lcom/zopim/android/sdk/model/ChatLog$Type;


# direct methods
.method static constructor <clinit>()V
    .locals 12

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$Type;

    const-string v1, "CHAT_MSG_AGENT"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/zopim/android/sdk/model/ChatLog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_AGENT:Lcom/zopim/android/sdk/model/ChatLog$Type;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$Type;

    const-string v1, "CHAT_MSG_VISITOR"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/zopim/android/sdk/model/ChatLog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_VISITOR:Lcom/zopim/android/sdk/model/ChatLog$Type;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$Type;

    const-string v1, "CHAT_MSG_TRIGGER"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/zopim/android/sdk/model/ChatLog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_TRIGGER:Lcom/zopim/android/sdk/model/ChatLog$Type;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$Type;

    const-string v1, "CHAT_MSG_SYSTEM"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5}, Lcom/zopim/android/sdk/model/ChatLog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_SYSTEM:Lcom/zopim/android/sdk/model/ChatLog$Type;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$Type;

    const-string v1, "MEMBER_LEAVE"

    const/4 v6, 0x4

    invoke-direct {v0, v1, v6}, Lcom/zopim/android/sdk/model/ChatLog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->MEMBER_LEAVE:Lcom/zopim/android/sdk/model/ChatLog$Type;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$Type;

    const-string v1, "MEMBER_JOIN"

    const/4 v7, 0x5

    invoke-direct {v0, v1, v7}, Lcom/zopim/android/sdk/model/ChatLog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->MEMBER_JOIN:Lcom/zopim/android/sdk/model/ChatLog$Type;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$Type;

    const-string v1, "SYSTEM_OFFLINE"

    const/4 v8, 0x6

    invoke-direct {v0, v1, v8}, Lcom/zopim/android/sdk/model/ChatLog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->SYSTEM_OFFLINE:Lcom/zopim/android/sdk/model/ChatLog$Type;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$Type;

    const-string v1, "ATTACHMENT_UPLOAD"

    const/4 v9, 0x7

    invoke-direct {v0, v1, v9}, Lcom/zopim/android/sdk/model/ChatLog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->ATTACHMENT_UPLOAD:Lcom/zopim/android/sdk/model/ChatLog$Type;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$Type;

    const-string v1, "CHAT_RATING"

    const/16 v10, 0x8

    invoke-direct {v0, v1, v10}, Lcom/zopim/android/sdk/model/ChatLog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_RATING:Lcom/zopim/android/sdk/model/ChatLog$Type;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$Type;

    const-string v1, "UNKNOWN"

    const/16 v11, 0x9

    invoke-direct {v0, v1, v11}, Lcom/zopim/android/sdk/model/ChatLog$Type;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->UNKNOWN:Lcom/zopim/android/sdk/model/ChatLog$Type;

    const/16 v0, 0xa

    new-array v0, v0, [Lcom/zopim/android/sdk/model/ChatLog$Type;

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_AGENT:Lcom/zopim/android/sdk/model/ChatLog$Type;

    aput-object v1, v0, v2

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_VISITOR:Lcom/zopim/android/sdk/model/ChatLog$Type;

    aput-object v1, v0, v3

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_TRIGGER:Lcom/zopim/android/sdk/model/ChatLog$Type;

    aput-object v1, v0, v4

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_SYSTEM:Lcom/zopim/android/sdk/model/ChatLog$Type;

    aput-object v1, v0, v5

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->MEMBER_LEAVE:Lcom/zopim/android/sdk/model/ChatLog$Type;

    aput-object v1, v0, v6

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->MEMBER_JOIN:Lcom/zopim/android/sdk/model/ChatLog$Type;

    aput-object v1, v0, v7

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->SYSTEM_OFFLINE:Lcom/zopim/android/sdk/model/ChatLog$Type;

    aput-object v1, v0, v8

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->ATTACHMENT_UPLOAD:Lcom/zopim/android/sdk/model/ChatLog$Type;

    aput-object v1, v0, v9

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_RATING:Lcom/zopim/android/sdk/model/ChatLog$Type;

    aput-object v1, v0, v10

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Type;->UNKNOWN:Lcom/zopim/android/sdk/model/ChatLog$Type;

    aput-object v1, v0, v11

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->$VALUES:[Lcom/zopim/android/sdk/model/ChatLog$Type;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/model/ChatLog$Type;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/model/ChatLog$Type;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/model/ChatLog$Type;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Type;->$VALUES:[Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/model/ChatLog$Type;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/model/ChatLog$Type;

    return-object v0
.end method
