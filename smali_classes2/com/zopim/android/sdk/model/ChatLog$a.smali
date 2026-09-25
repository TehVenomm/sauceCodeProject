.class final enum Lcom/zopim/android/sdk/model/ChatLog$a;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/model/ChatLog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x401a
    name = "a"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/model/ChatLog$a;",
        ">;"
    }
.end annotation


# static fields
.field public static final enum a:Lcom/zopim/android/sdk/model/ChatLog$a;

.field public static final enum b:Lcom/zopim/android/sdk/model/ChatLog$a;

.field public static final enum c:Lcom/zopim/android/sdk/model/ChatLog$a;

.field public static final enum d:Lcom/zopim/android/sdk/model/ChatLog$a;

.field public static final enum e:Lcom/zopim/android/sdk/model/ChatLog$a;

.field private static final synthetic g:[Lcom/zopim/android/sdk/model/ChatLog$a;


# instance fields
.field final f:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 8

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$a;

    const-string v1, "AGENT_SYSTEM"

    const-string v2, "agent:system"

    const/4 v3, 0x0

    invoke-direct {v0, v1, v3, v2}, Lcom/zopim/android/sdk/model/ChatLog$a;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$a;->a:Lcom/zopim/android/sdk/model/ChatLog$a;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$a;

    const-string v1, "AGENT_TRIGGER"

    const-string v2, "agent:trigger"

    const/4 v4, 0x1

    invoke-direct {v0, v1, v4, v2}, Lcom/zopim/android/sdk/model/ChatLog$a;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$a;->b:Lcom/zopim/android/sdk/model/ChatLog$a;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$a;

    const-string v1, "AGENT_MSG"

    const-string v2, "agent"

    const/4 v5, 0x2

    invoke-direct {v0, v1, v5, v2}, Lcom/zopim/android/sdk/model/ChatLog$a;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$a;->c:Lcom/zopim/android/sdk/model/ChatLog$a;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$a;

    const-string v1, "VISITOR_MSG"

    const-string v2, "visitor"

    const/4 v6, 0x3

    invoke-direct {v0, v1, v6, v2}, Lcom/zopim/android/sdk/model/ChatLog$a;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$a;->d:Lcom/zopim/android/sdk/model/ChatLog$a;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$a;

    const-string v1, "UNKNOWN"

    const-string v2, "unknown"

    const/4 v7, 0x4

    invoke-direct {v0, v1, v7, v2}, Lcom/zopim/android/sdk/model/ChatLog$a;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$a;->e:Lcom/zopim/android/sdk/model/ChatLog$a;

    const/4 v0, 0x5

    new-array v0, v0, [Lcom/zopim/android/sdk/model/ChatLog$a;

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$a;->a:Lcom/zopim/android/sdk/model/ChatLog$a;

    aput-object v1, v0, v3

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$a;->b:Lcom/zopim/android/sdk/model/ChatLog$a;

    aput-object v1, v0, v4

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$a;->c:Lcom/zopim/android/sdk/model/ChatLog$a;

    aput-object v1, v0, v5

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$a;->d:Lcom/zopim/android/sdk/model/ChatLog$a;

    aput-object v1, v0, v6

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$a;->e:Lcom/zopim/android/sdk/model/ChatLog$a;

    aput-object v1, v0, v7

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$a;->g:[Lcom/zopim/android/sdk/model/ChatLog$a;

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

    iput-object p3, p0, Lcom/zopim/android/sdk/model/ChatLog$a;->f:Ljava/lang/String;

    return-void
.end method

.method public static a(Ljava/lang/String;)Lcom/zopim/android/sdk/model/ChatLog$a;
    .locals 1
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    if-eqz p0, :cond_5

    invoke-virtual {p0}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    const-string v0, "agent:system"

    invoke-virtual {v0, p0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_1

    sget-object p0, Lcom/zopim/android/sdk/model/ChatLog$a;->a:Lcom/zopim/android/sdk/model/ChatLog$a;

    return-object p0

    :cond_1
    const-string v0, "agent:trigger"

    invoke-virtual {v0, p0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_2

    sget-object p0, Lcom/zopim/android/sdk/model/ChatLog$a;->b:Lcom/zopim/android/sdk/model/ChatLog$a;

    return-object p0

    :cond_2
    const-string v0, "agent"

    invoke-virtual {p0, v0}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_3

    sget-object p0, Lcom/zopim/android/sdk/model/ChatLog$a;->c:Lcom/zopim/android/sdk/model/ChatLog$a;

    return-object p0

    :cond_3
    const-string v0, "visitor"

    invoke-virtual {p0, v0}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result p0

    if-eqz p0, :cond_4

    sget-object p0, Lcom/zopim/android/sdk/model/ChatLog$a;->d:Lcom/zopim/android/sdk/model/ChatLog$a;

    return-object p0

    :cond_4
    sget-object p0, Lcom/zopim/android/sdk/model/ChatLog$a;->e:Lcom/zopim/android/sdk/model/ChatLog$a;

    return-object p0

    :cond_5
    :goto_0
    sget-object p0, Lcom/zopim/android/sdk/model/ChatLog$a;->e:Lcom/zopim/android/sdk/model/ChatLog$a;

    return-object p0
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/model/ChatLog$a;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/model/ChatLog$a;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/model/ChatLog$a;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/model/ChatLog$a;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$a;->g:[Lcom/zopim/android/sdk/model/ChatLog$a;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/model/ChatLog$a;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/model/ChatLog$a;

    return-object v0
.end method


# virtual methods
.method public a()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog$a;->f:Ljava/lang/String;

    return-object v0
.end method
