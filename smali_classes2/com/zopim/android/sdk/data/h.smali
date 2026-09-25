.class final enum Lcom/zopim/android/sdk/data/h;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/data/h;",
        ">;"
    }
.end annotation


# static fields
.field public static final enum a:Lcom/zopim/android/sdk/data/h;

.field public static final enum b:Lcom/zopim/android/sdk/data/h;

.field public static final enum c:Lcom/zopim/android/sdk/data/h;

.field public static final enum d:Lcom/zopim/android/sdk/data/h;

.field public static final enum e:Lcom/zopim/android/sdk/data/h;

.field public static final enum f:Lcom/zopim/android/sdk/data/h;

.field public static final enum g:Lcom/zopim/android/sdk/data/h;

.field public static final enum h:Lcom/zopim/android/sdk/data/h;

.field public static final enum i:Lcom/zopim/android/sdk/data/h;

.field private static final j:Ljava/lang/String;

.field private static final synthetic l:[Lcom/zopim/android/sdk/data/h;


# instance fields
.field private final k:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 12

    new-instance v0, Lcom/zopim/android/sdk/data/h;

    const-string v1, "LIVECHAT_CHANNEL_LOG"

    const-string v2, "livechat.channel.log"

    const/4 v3, 0x0

    invoke-direct {v0, v1, v3, v2}, Lcom/zopim/android/sdk/data/h;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/data/h;->a:Lcom/zopim/android/sdk/data/h;

    new-instance v0, Lcom/zopim/android/sdk/data/h;

    const-string v1, "LIVECHAT_PROFILE"

    const-string v2, "livechat.profile"

    const/4 v4, 0x1

    invoke-direct {v0, v1, v4, v2}, Lcom/zopim/android/sdk/data/h;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/data/h;->b:Lcom/zopim/android/sdk/data/h;

    new-instance v0, Lcom/zopim/android/sdk/data/h;

    const-string v1, "LIVECHAT_AGENTS"

    const-string v2, "livechat.agents"

    const/4 v5, 0x2

    invoke-direct {v0, v1, v5, v2}, Lcom/zopim/android/sdk/data/h;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/data/h;->c:Lcom/zopim/android/sdk/data/h;

    new-instance v0, Lcom/zopim/android/sdk/data/h;

    const-string v1, "LIVECHAT_UI"

    const-string v2, "livechat.ui"

    const/4 v6, 0x3

    invoke-direct {v0, v1, v6, v2}, Lcom/zopim/android/sdk/data/h;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/data/h;->d:Lcom/zopim/android/sdk/data/h;

    new-instance v0, Lcom/zopim/android/sdk/data/h;

    const-string v1, "LIVECHAT_DEPARTMENTS"

    const-string v2, "livechat.departments"

    const/4 v7, 0x4

    invoke-direct {v0, v1, v7, v2}, Lcom/zopim/android/sdk/data/h;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/data/h;->e:Lcom/zopim/android/sdk/data/h;

    new-instance v0, Lcom/zopim/android/sdk/data/h;

    const-string v1, "LIVECHAT_ACCOUNT"

    const-string v2, "livechat.account"

    const/4 v8, 0x5

    invoke-direct {v0, v1, v8, v2}, Lcom/zopim/android/sdk/data/h;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/data/h;->f:Lcom/zopim/android/sdk/data/h;

    new-instance v0, Lcom/zopim/android/sdk/data/h;

    const-string v1, "LIVECHAT_SETTINGS_FORMS"

    const-string v2, "livechat.settings.forms"

    const/4 v9, 0x6

    invoke-direct {v0, v1, v9, v2}, Lcom/zopim/android/sdk/data/h;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/data/h;->g:Lcom/zopim/android/sdk/data/h;

    new-instance v0, Lcom/zopim/android/sdk/data/h;

    const-string v1, "CONNECTION"

    const-string v2, "connection"

    const/4 v10, 0x7

    invoke-direct {v0, v1, v10, v2}, Lcom/zopim/android/sdk/data/h;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/data/h;->h:Lcom/zopim/android/sdk/data/h;

    new-instance v0, Lcom/zopim/android/sdk/data/h;

    const-string v1, "UNKNOWN"

    const-string v2, "unknown"

    const/16 v11, 0x8

    invoke-direct {v0, v1, v11, v2}, Lcom/zopim/android/sdk/data/h;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/data/h;->i:Lcom/zopim/android/sdk/data/h;

    const/16 v0, 0x9

    new-array v0, v0, [Lcom/zopim/android/sdk/data/h;

    sget-object v1, Lcom/zopim/android/sdk/data/h;->a:Lcom/zopim/android/sdk/data/h;

    aput-object v1, v0, v3

    sget-object v1, Lcom/zopim/android/sdk/data/h;->b:Lcom/zopim/android/sdk/data/h;

    aput-object v1, v0, v4

    sget-object v1, Lcom/zopim/android/sdk/data/h;->c:Lcom/zopim/android/sdk/data/h;

    aput-object v1, v0, v5

    sget-object v1, Lcom/zopim/android/sdk/data/h;->d:Lcom/zopim/android/sdk/data/h;

    aput-object v1, v0, v6

    sget-object v1, Lcom/zopim/android/sdk/data/h;->e:Lcom/zopim/android/sdk/data/h;

    aput-object v1, v0, v7

    sget-object v1, Lcom/zopim/android/sdk/data/h;->f:Lcom/zopim/android/sdk/data/h;

    aput-object v1, v0, v8

    sget-object v1, Lcom/zopim/android/sdk/data/h;->g:Lcom/zopim/android/sdk/data/h;

    aput-object v1, v0, v9

    sget-object v1, Lcom/zopim/android/sdk/data/h;->h:Lcom/zopim/android/sdk/data/h;

    aput-object v1, v0, v10

    sget-object v1, Lcom/zopim/android/sdk/data/h;->i:Lcom/zopim/android/sdk/data/h;

    aput-object v1, v0, v11

    sput-object v0, Lcom/zopim/android/sdk/data/h;->l:[Lcom/zopim/android/sdk/data/h;

    const-class v0, Lcom/zopim/android/sdk/data/h;

    invoke-virtual {v0}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/zopim/android/sdk/data/h;->j:Ljava/lang/String;

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

    iput-object p3, p0, Lcom/zopim/android/sdk/data/h;->k:Ljava/lang/String;

    return-void
.end method

.method static a(Ljava/lang/String;)Lcom/zopim/android/sdk/data/h;
    .locals 5

    invoke-static {}, Lcom/zopim/android/sdk/data/h;->values()[Lcom/zopim/android/sdk/data/h;

    move-result-object v0

    array-length v1, v0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_1

    aget-object v3, v0, v2

    iget-object v4, v3, Lcom/zopim/android/sdk/data/h;->k:Ljava/lang/String;

    invoke-virtual {v4, p0}, Ljava/lang/String;->contentEquals(Ljava/lang/CharSequence;)Z

    move-result v4

    if-eqz v4, :cond_0

    return-object v3

    :cond_0
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_1
    sget-object v0, Lcom/zopim/android/sdk/data/h;->j:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Unknown protocol path, will return "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v2, Lcom/zopim/android/sdk/data/h;->i:Lcom/zopim/android/sdk/data/h;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v2, ": "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Lcom/zopim/android/sdk/api/Logger;->i(Ljava/lang/String;Ljava/lang/String;)V

    sget-object p0, Lcom/zopim/android/sdk/data/h;->i:Lcom/zopim/android/sdk/data/h;

    return-object p0
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/data/h;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/data/h;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/data/h;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/data/h;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/data/h;->l:[Lcom/zopim/android/sdk/data/h;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/data/h;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/data/h;

    return-object v0
.end method
