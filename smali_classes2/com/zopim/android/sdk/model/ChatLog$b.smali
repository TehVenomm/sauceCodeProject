.class final enum Lcom/zopim/android/sdk/model/ChatLog$b;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/model/ChatLog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x401a
    name = "b"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/model/ChatLog$b;",
        ">;"
    }
.end annotation


# static fields
.field public static final enum a:Lcom/zopim/android/sdk/model/ChatLog$b;

.field public static final enum b:Lcom/zopim/android/sdk/model/ChatLog$b;

.field public static final enum c:Lcom/zopim/android/sdk/model/ChatLog$b;

.field public static final enum d:Lcom/zopim/android/sdk/model/ChatLog$b;

.field public static final enum e:Lcom/zopim/android/sdk/model/ChatLog$b;

.field public static final enum f:Lcom/zopim/android/sdk/model/ChatLog$b;

.field public static final enum g:Lcom/zopim/android/sdk/model/ChatLog$b;

.field public static final enum h:Lcom/zopim/android/sdk/model/ChatLog$b;

.field public static final enum i:Lcom/zopim/android/sdk/model/ChatLog$b;

.field public static final enum j:Lcom/zopim/android/sdk/model/ChatLog$b;

.field private static final synthetic l:[Lcom/zopim/android/sdk/model/ChatLog$b;


# instance fields
.field final k:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 13

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$b;

    const-string v1, "CHAT_MSG"

    const-string v2, "chat.msg"

    const/4 v3, 0x0

    invoke-direct {v0, v1, v3, v2}, Lcom/zopim/android/sdk/model/ChatLog$b;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$b;->a:Lcom/zopim/android/sdk/model/ChatLog$b;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$b;

    const-string v1, "MEMBER_JOIN"

    const-string v2, "chat.memberjoin"

    const/4 v4, 0x1

    invoke-direct {v0, v1, v4, v2}, Lcom/zopim/android/sdk/model/ChatLog$b;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$b;->b:Lcom/zopim/android/sdk/model/ChatLog$b;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$b;

    const-string v1, "MEMBER_LEAVE"

    const-string v2, "chat.memberleave"

    const/4 v5, 0x2

    invoke-direct {v0, v1, v5, v2}, Lcom/zopim/android/sdk/model/ChatLog$b;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$b;->c:Lcom/zopim/android/sdk/model/ChatLog$b;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$b;

    const-string v1, "CHAT_EVENT"

    const-string v2, "chat.event"

    const/4 v6, 0x3

    invoke-direct {v0, v1, v6, v2}, Lcom/zopim/android/sdk/model/ChatLog$b;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$b;->d:Lcom/zopim/android/sdk/model/ChatLog$b;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$b;

    const-string v1, "SYSTEM_OFFLINE"

    const-string v2, "system.offline"

    const/4 v7, 0x4

    invoke-direct {v0, v1, v7, v2}, Lcom/zopim/android/sdk/model/ChatLog$b;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$b;->e:Lcom/zopim/android/sdk/model/ChatLog$b;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$b;

    const-string v1, "FILE_UPLOAD"

    const-string v2, "chat.file.upload"

    const/4 v8, 0x5

    invoke-direct {v0, v1, v8, v2}, Lcom/zopim/android/sdk/model/ChatLog$b;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$b;->f:Lcom/zopim/android/sdk/model/ChatLog$b;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$b;

    const-string v1, "CHAT_RATING_REQUEST"

    const-string v2, "chat.request.rating"

    const/4 v9, 0x6

    invoke-direct {v0, v1, v9, v2}, Lcom/zopim/android/sdk/model/ChatLog$b;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$b;->g:Lcom/zopim/android/sdk/model/ChatLog$b;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$b;

    const-string v1, "CHAT_RATING"

    const-string v2, "chat.rating"

    const/4 v10, 0x7

    invoke-direct {v0, v1, v10, v2}, Lcom/zopim/android/sdk/model/ChatLog$b;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$b;->h:Lcom/zopim/android/sdk/model/ChatLog$b;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$b;

    const-string v1, "CHAT_COMMENT"

    const-string v2, "chat.comment"

    const/16 v11, 0x8

    invoke-direct {v0, v1, v11, v2}, Lcom/zopim/android/sdk/model/ChatLog$b;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$b;->i:Lcom/zopim/android/sdk/model/ChatLog$b;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$b;

    const-string v1, "UNKNOWN"

    const-string v2, "unknown"

    const/16 v12, 0x9

    invoke-direct {v0, v1, v12, v2}, Lcom/zopim/android/sdk/model/ChatLog$b;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$b;->j:Lcom/zopim/android/sdk/model/ChatLog$b;

    const/16 v0, 0xa

    new-array v0, v0, [Lcom/zopim/android/sdk/model/ChatLog$b;

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$b;->a:Lcom/zopim/android/sdk/model/ChatLog$b;

    aput-object v1, v0, v3

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$b;->b:Lcom/zopim/android/sdk/model/ChatLog$b;

    aput-object v1, v0, v4

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$b;->c:Lcom/zopim/android/sdk/model/ChatLog$b;

    aput-object v1, v0, v5

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$b;->d:Lcom/zopim/android/sdk/model/ChatLog$b;

    aput-object v1, v0, v6

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$b;->e:Lcom/zopim/android/sdk/model/ChatLog$b;

    aput-object v1, v0, v7

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$b;->f:Lcom/zopim/android/sdk/model/ChatLog$b;

    aput-object v1, v0, v8

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$b;->g:Lcom/zopim/android/sdk/model/ChatLog$b;

    aput-object v1, v0, v9

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$b;->h:Lcom/zopim/android/sdk/model/ChatLog$b;

    aput-object v1, v0, v10

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$b;->i:Lcom/zopim/android/sdk/model/ChatLog$b;

    aput-object v1, v0, v11

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$b;->j:Lcom/zopim/android/sdk/model/ChatLog$b;

    aput-object v1, v0, v12

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$b;->l:[Lcom/zopim/android/sdk/model/ChatLog$b;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;ILjava/lang/String;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    iput-object p3, p0, Lcom/zopim/android/sdk/model/ChatLog$b;->k:Ljava/lang/String;

    return-void
.end method

.method public static a(Ljava/lang/String;)Lcom/zopim/android/sdk/model/ChatLog$b;
    .locals 5
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    invoke-static {}, Lcom/zopim/android/sdk/model/ChatLog$b;->values()[Lcom/zopim/android/sdk/model/ChatLog$b;

    move-result-object v0

    array-length v1, v0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_1

    aget-object v3, v0, v2

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog$b;->a()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v4, p0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_0

    return-object v3

    :cond_0
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_1
    sget-object p0, Lcom/zopim/android/sdk/model/ChatLog$b;->j:Lcom/zopim/android/sdk/model/ChatLog$b;

    return-object p0
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/model/ChatLog$b;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/model/ChatLog$b;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/model/ChatLog$b;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/model/ChatLog$b;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$b;->l:[Lcom/zopim/android/sdk/model/ChatLog$b;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/model/ChatLog$b;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/model/ChatLog$b;

    return-object v0
.end method


# virtual methods
.method public a()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog$b;->k:Ljava/lang/String;

    return-object v0
.end method
