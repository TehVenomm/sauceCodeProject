.class public final enum Lcom/zopim/android/sdk/model/ChatLog$Rating;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/model/ChatLog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "Rating"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/model/ChatLog$Rating;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/zopim/android/sdk/model/ChatLog$Rating;

.field public static final enum BAD:Lcom/zopim/android/sdk/model/ChatLog$Rating;

.field public static final enum GOOD:Lcom/zopim/android/sdk/model/ChatLog$Rating;

.field public static final enum UNKNOWN:Lcom/zopim/android/sdk/model/ChatLog$Rating;

.field public static final enum UNRATED:Lcom/zopim/android/sdk/model/ChatLog$Rating;


# instance fields
.field final rating:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 7

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$Rating;

    const-string v1, "GOOD"

    const-string v2, "good"

    const/4 v3, 0x0

    invoke-direct {v0, v1, v3, v2}, Lcom/zopim/android/sdk/model/ChatLog$Rating;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Rating;->GOOD:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$Rating;

    const-string v1, "BAD"

    const-string v2, "bad"

    const/4 v4, 0x1

    invoke-direct {v0, v1, v4, v2}, Lcom/zopim/android/sdk/model/ChatLog$Rating;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Rating;->BAD:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$Rating;

    const-string v1, "UNRATED"

    const-string v2, "unrated"

    const/4 v5, 0x2

    invoke-direct {v0, v1, v5, v2}, Lcom/zopim/android/sdk/model/ChatLog$Rating;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Rating;->UNRATED:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    new-instance v0, Lcom/zopim/android/sdk/model/ChatLog$Rating;

    const-string v1, "UNKNOWN"

    const-string v2, "unknown"

    const/4 v6, 0x3

    invoke-direct {v0, v1, v6, v2}, Lcom/zopim/android/sdk/model/ChatLog$Rating;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Rating;->UNKNOWN:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    const/4 v0, 0x4

    new-array v0, v0, [Lcom/zopim/android/sdk/model/ChatLog$Rating;

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Rating;->GOOD:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    aput-object v1, v0, v3

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Rating;->BAD:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    aput-object v1, v0, v4

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Rating;->UNRATED:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    aput-object v1, v0, v5

    sget-object v1, Lcom/zopim/android/sdk/model/ChatLog$Rating;->UNKNOWN:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    aput-object v1, v0, v6

    sput-object v0, Lcom/zopim/android/sdk/model/ChatLog$Rating;->$VALUES:[Lcom/zopim/android/sdk/model/ChatLog$Rating;

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

    iput-object p3, p0, Lcom/zopim/android/sdk/model/ChatLog$Rating;->rating:Ljava/lang/String;

    return-void
.end method

.method public static getRating(Ljava/lang/String;)Lcom/zopim/android/sdk/model/ChatLog$Rating;
    .locals 5
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    invoke-static {}, Lcom/zopim/android/sdk/model/ChatLog$Rating;->values()[Lcom/zopim/android/sdk/model/ChatLog$Rating;

    move-result-object v0

    array-length v1, v0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_1

    aget-object v3, v0, v2

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog$Rating;->getValue()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v4, p0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_0

    return-object v3

    :cond_0
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_1
    sget-object p0, Lcom/zopim/android/sdk/model/ChatLog$Rating;->UNKNOWN:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    return-object p0
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/model/ChatLog$Rating;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/model/ChatLog$Rating;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/model/ChatLog$Rating;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/model/ChatLog$Rating;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/model/ChatLog$Rating;->$VALUES:[Lcom/zopim/android/sdk/model/ChatLog$Rating;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/model/ChatLog$Rating;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/model/ChatLog$Rating;

    return-object v0
.end method


# virtual methods
.method public getValue()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/model/ChatLog$Rating;->rating:Ljava/lang/String;

    return-object v0
.end method
