.class public final enum Lcom/zopim/android/sdk/model/Connection$Status;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/model/Connection;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "Status"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/model/Connection$Status;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/zopim/android/sdk/model/Connection$Status;

.field public static final enum CLOSED:Lcom/zopim/android/sdk/model/Connection$Status;

.field public static final enum CONNECTED:Lcom/zopim/android/sdk/model/Connection$Status;

.field public static final enum CONNECTING:Lcom/zopim/android/sdk/model/Connection$Status;

.field public static final enum DISCONNECTED:Lcom/zopim/android/sdk/model/Connection$Status;

.field public static final enum NO_CONNECTION:Lcom/zopim/android/sdk/model/Connection$Status;

.field public static final enum UNKNOWN:Lcom/zopim/android/sdk/model/Connection$Status;


# instance fields
.field final value:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 9

    new-instance v0, Lcom/zopim/android/sdk/model/Connection$Status;

    const-string v1, "NO_CONNECTION"

    const-string v2, "noConnection"

    const/4 v3, 0x0

    invoke-direct {v0, v1, v3, v2}, Lcom/zopim/android/sdk/model/Connection$Status;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/Connection$Status;->NO_CONNECTION:Lcom/zopim/android/sdk/model/Connection$Status;

    new-instance v0, Lcom/zopim/android/sdk/model/Connection$Status;

    const-string v1, "CLOSED"

    const-string v2, "closed"

    const/4 v4, 0x1

    invoke-direct {v0, v1, v4, v2}, Lcom/zopim/android/sdk/model/Connection$Status;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/Connection$Status;->CLOSED:Lcom/zopim/android/sdk/model/Connection$Status;

    new-instance v0, Lcom/zopim/android/sdk/model/Connection$Status;

    const-string v1, "DISCONNECTED"

    const-string v2, "disconnected"

    const/4 v5, 0x2

    invoke-direct {v0, v1, v5, v2}, Lcom/zopim/android/sdk/model/Connection$Status;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/Connection$Status;->DISCONNECTED:Lcom/zopim/android/sdk/model/Connection$Status;

    new-instance v0, Lcom/zopim/android/sdk/model/Connection$Status;

    const-string v1, "CONNECTING"

    const-string v2, "connecting"

    const/4 v6, 0x3

    invoke-direct {v0, v1, v6, v2}, Lcom/zopim/android/sdk/model/Connection$Status;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/Connection$Status;->CONNECTING:Lcom/zopim/android/sdk/model/Connection$Status;

    new-instance v0, Lcom/zopim/android/sdk/model/Connection$Status;

    const-string v1, "CONNECTED"

    const-string v2, "connected"

    const/4 v7, 0x4

    invoke-direct {v0, v1, v7, v2}, Lcom/zopim/android/sdk/model/Connection$Status;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/Connection$Status;->CONNECTED:Lcom/zopim/android/sdk/model/Connection$Status;

    new-instance v0, Lcom/zopim/android/sdk/model/Connection$Status;

    const-string v1, "UNKNOWN"

    const-string v2, "unknown"

    const/4 v8, 0x5

    invoke-direct {v0, v1, v8, v2}, Lcom/zopim/android/sdk/model/Connection$Status;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/model/Connection$Status;->UNKNOWN:Lcom/zopim/android/sdk/model/Connection$Status;

    const/4 v0, 0x6

    new-array v0, v0, [Lcom/zopim/android/sdk/model/Connection$Status;

    sget-object v1, Lcom/zopim/android/sdk/model/Connection$Status;->NO_CONNECTION:Lcom/zopim/android/sdk/model/Connection$Status;

    aput-object v1, v0, v3

    sget-object v1, Lcom/zopim/android/sdk/model/Connection$Status;->CLOSED:Lcom/zopim/android/sdk/model/Connection$Status;

    aput-object v1, v0, v4

    sget-object v1, Lcom/zopim/android/sdk/model/Connection$Status;->DISCONNECTED:Lcom/zopim/android/sdk/model/Connection$Status;

    aput-object v1, v0, v5

    sget-object v1, Lcom/zopim/android/sdk/model/Connection$Status;->CONNECTING:Lcom/zopim/android/sdk/model/Connection$Status;

    aput-object v1, v0, v6

    sget-object v1, Lcom/zopim/android/sdk/model/Connection$Status;->CONNECTED:Lcom/zopim/android/sdk/model/Connection$Status;

    aput-object v1, v0, v7

    sget-object v1, Lcom/zopim/android/sdk/model/Connection$Status;->UNKNOWN:Lcom/zopim/android/sdk/model/Connection$Status;

    aput-object v1, v0, v8

    sput-object v0, Lcom/zopim/android/sdk/model/Connection$Status;->$VALUES:[Lcom/zopim/android/sdk/model/Connection$Status;

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

    iput-object p3, p0, Lcom/zopim/android/sdk/model/Connection$Status;->value:Ljava/lang/String;

    return-void
.end method

.method public static getStatus(Ljava/lang/String;)Lcom/zopim/android/sdk/model/Connection$Status;
    .locals 5
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    invoke-static {}, Lcom/zopim/android/sdk/model/Connection$Status;->values()[Lcom/zopim/android/sdk/model/Connection$Status;

    move-result-object v0

    array-length v1, v0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_1

    aget-object v3, v0, v2

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/Connection$Status;->getValue()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v4, p0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_0

    return-object v3

    :cond_0
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_1
    sget-object p0, Lcom/zopim/android/sdk/model/Connection$Status;->UNKNOWN:Lcom/zopim/android/sdk/model/Connection$Status;

    return-object p0
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/model/Connection$Status;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/model/Connection$Status;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/model/Connection$Status;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/model/Connection$Status;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/model/Connection$Status;->$VALUES:[Lcom/zopim/android/sdk/model/Connection$Status;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/model/Connection$Status;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/model/Connection$Status;

    return-object v0
.end method


# virtual methods
.method public getValue()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/model/Connection$Status;->value:Ljava/lang/String;

    return-object v0
.end method
