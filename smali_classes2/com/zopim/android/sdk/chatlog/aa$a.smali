.class final enum Lcom/zopim/android/sdk/chatlog/aa$a;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/chatlog/aa;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4018
    name = "a"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/chatlog/aa$a;",
        ">;"
    }
.end annotation


# static fields
.field public static final enum a:Lcom/zopim/android/sdk/chatlog/aa$a;

.field public static final enum b:Lcom/zopim/android/sdk/chatlog/aa$a;

.field public static final enum c:Lcom/zopim/android/sdk/chatlog/aa$a;

.field public static final enum d:Lcom/zopim/android/sdk/chatlog/aa$a;

.field public static final enum e:Lcom/zopim/android/sdk/chatlog/aa$a;

.field public static final enum f:Lcom/zopim/android/sdk/chatlog/aa$a;

.field public static final enum g:Lcom/zopim/android/sdk/chatlog/aa$a;

.field public static final enum h:Lcom/zopim/android/sdk/chatlog/aa$a;

.field private static final synthetic j:[Lcom/zopim/android/sdk/chatlog/aa$a;


# instance fields
.field final i:I


# direct methods
.method static constructor <clinit>()V
    .locals 10

    new-instance v0, Lcom/zopim/android/sdk/chatlog/aa$a;

    const-string v1, "UNKNOWN"

    const/4 v2, 0x0

    const/4 v3, -0x1

    invoke-direct {v0, v1, v2, v3}, Lcom/zopim/android/sdk/chatlog/aa$a;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/zopim/android/sdk/chatlog/aa$a;->a:Lcom/zopim/android/sdk/chatlog/aa$a;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/aa$a;

    const-string v1, "VISITOR"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3, v2}, Lcom/zopim/android/sdk/chatlog/aa$a;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/zopim/android/sdk/chatlog/aa$a;->b:Lcom/zopim/android/sdk/chatlog/aa$a;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/aa$a;

    const-string v1, "AGENT"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4, v3}, Lcom/zopim/android/sdk/chatlog/aa$a;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/zopim/android/sdk/chatlog/aa$a;->c:Lcom/zopim/android/sdk/chatlog/aa$a;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/aa$a;

    const-string v1, "AGENT_TYPING"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5, v4}, Lcom/zopim/android/sdk/chatlog/aa$a;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/zopim/android/sdk/chatlog/aa$a;->d:Lcom/zopim/android/sdk/chatlog/aa$a;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/aa$a;

    const-string v1, "CHAT_EVENT"

    const/4 v6, 0x4

    invoke-direct {v0, v1, v6, v5}, Lcom/zopim/android/sdk/chatlog/aa$a;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/zopim/android/sdk/chatlog/aa$a;->e:Lcom/zopim/android/sdk/chatlog/aa$a;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/aa$a;

    const-string v1, "MEMBER_EVENT"

    const/4 v7, 0x5

    invoke-direct {v0, v1, v7, v6}, Lcom/zopim/android/sdk/chatlog/aa$a;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/zopim/android/sdk/chatlog/aa$a;->f:Lcom/zopim/android/sdk/chatlog/aa$a;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/aa$a;

    const-string v1, "ATTACHMENT_UPLOAD"

    const/4 v8, 0x6

    invoke-direct {v0, v1, v8, v7}, Lcom/zopim/android/sdk/chatlog/aa$a;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/zopim/android/sdk/chatlog/aa$a;->g:Lcom/zopim/android/sdk/chatlog/aa$a;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/aa$a;

    const-string v1, "CHAT_RATING"

    const/4 v9, 0x7

    invoke-direct {v0, v1, v9, v9}, Lcom/zopim/android/sdk/chatlog/aa$a;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/zopim/android/sdk/chatlog/aa$a;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    const/16 v0, 0x8

    new-array v0, v0, [Lcom/zopim/android/sdk/chatlog/aa$a;

    sget-object v1, Lcom/zopim/android/sdk/chatlog/aa$a;->a:Lcom/zopim/android/sdk/chatlog/aa$a;

    aput-object v1, v0, v2

    sget-object v1, Lcom/zopim/android/sdk/chatlog/aa$a;->b:Lcom/zopim/android/sdk/chatlog/aa$a;

    aput-object v1, v0, v3

    sget-object v1, Lcom/zopim/android/sdk/chatlog/aa$a;->c:Lcom/zopim/android/sdk/chatlog/aa$a;

    aput-object v1, v0, v4

    sget-object v1, Lcom/zopim/android/sdk/chatlog/aa$a;->d:Lcom/zopim/android/sdk/chatlog/aa$a;

    aput-object v1, v0, v5

    sget-object v1, Lcom/zopim/android/sdk/chatlog/aa$a;->e:Lcom/zopim/android/sdk/chatlog/aa$a;

    aput-object v1, v0, v6

    sget-object v1, Lcom/zopim/android/sdk/chatlog/aa$a;->f:Lcom/zopim/android/sdk/chatlog/aa$a;

    aput-object v1, v0, v7

    sget-object v1, Lcom/zopim/android/sdk/chatlog/aa$a;->g:Lcom/zopim/android/sdk/chatlog/aa$a;

    aput-object v1, v0, v8

    sget-object v1, Lcom/zopim/android/sdk/chatlog/aa$a;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    aput-object v1, v0, v9

    sput-object v0, Lcom/zopim/android/sdk/chatlog/aa$a;->j:[Lcom/zopim/android/sdk/chatlog/aa$a;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;II)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)V"
        }
    .end annotation

    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    iput p3, p0, Lcom/zopim/android/sdk/chatlog/aa$a;->i:I

    return-void
.end method

.method static a(I)Lcom/zopim/android/sdk/chatlog/aa$a;
    .locals 5

    invoke-static {}, Lcom/zopim/android/sdk/chatlog/aa$a;->values()[Lcom/zopim/android/sdk/chatlog/aa$a;

    move-result-object v0

    array-length v1, v0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_1

    aget-object v3, v0, v2

    invoke-virtual {v3}, Lcom/zopim/android/sdk/chatlog/aa$a;->a()I

    move-result v4

    if-ne v4, p0, :cond_0

    return-object v3

    :cond_0
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_1
    sget-object p0, Lcom/zopim/android/sdk/chatlog/aa$a;->a:Lcom/zopim/android/sdk/chatlog/aa$a;

    return-object p0
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/chatlog/aa$a;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/chatlog/aa$a;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/chatlog/aa$a;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/chatlog/aa$a;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/chatlog/aa$a;->j:[Lcom/zopim/android/sdk/chatlog/aa$a;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/chatlog/aa$a;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/chatlog/aa$a;

    return-object v0
.end method


# virtual methods
.method a()I
    .locals 1

    iget v0, p0, Lcom/zopim/android/sdk/chatlog/aa$a;->i:I

    return v0
.end method
