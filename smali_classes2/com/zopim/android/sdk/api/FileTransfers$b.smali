.class final enum Lcom/zopim/android/sdk/api/FileTransfers$b;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/api/FileTransfers;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4018
    name = "b"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/api/FileTransfers$b;",
        ">;"
    }
.end annotation


# static fields
.field public static final enum a:Lcom/zopim/android/sdk/api/FileTransfers$b;

.field public static final enum b:Lcom/zopim/android/sdk/api/FileTransfers$b;

.field public static final enum c:Lcom/zopim/android/sdk/api/FileTransfers$b;

.field public static final enum d:Lcom/zopim/android/sdk/api/FileTransfers$b;

.field public static final enum e:Lcom/zopim/android/sdk/api/FileTransfers$b;

.field private static final synthetic f:[Lcom/zopim/android/sdk/api/FileTransfers$b;


# direct methods
.method static constructor <clinit>()V
    .locals 7

    new-instance v0, Lcom/zopim/android/sdk/api/FileTransfers$b;

    const-string v1, "UNKNOWN"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/zopim/android/sdk/api/FileTransfers$b;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/api/FileTransfers$b;->a:Lcom/zopim/android/sdk/api/FileTransfers$b;

    new-instance v0, Lcom/zopim/android/sdk/api/FileTransfers$b;

    const-string v1, "SCHEDULED"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/zopim/android/sdk/api/FileTransfers$b;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/api/FileTransfers$b;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    new-instance v0, Lcom/zopim/android/sdk/api/FileTransfers$b;

    const-string v1, "STARTED"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/zopim/android/sdk/api/FileTransfers$b;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/api/FileTransfers$b;->c:Lcom/zopim/android/sdk/api/FileTransfers$b;

    new-instance v0, Lcom/zopim/android/sdk/api/FileTransfers$b;

    const-string v1, "COMPLETED"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5}, Lcom/zopim/android/sdk/api/FileTransfers$b;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/api/FileTransfers$b;->d:Lcom/zopim/android/sdk/api/FileTransfers$b;

    new-instance v0, Lcom/zopim/android/sdk/api/FileTransfers$b;

    const-string v1, "FAILED"

    const/4 v6, 0x4

    invoke-direct {v0, v1, v6}, Lcom/zopim/android/sdk/api/FileTransfers$b;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/api/FileTransfers$b;->e:Lcom/zopim/android/sdk/api/FileTransfers$b;

    const/4 v0, 0x5

    new-array v0, v0, [Lcom/zopim/android/sdk/api/FileTransfers$b;

    sget-object v1, Lcom/zopim/android/sdk/api/FileTransfers$b;->a:Lcom/zopim/android/sdk/api/FileTransfers$b;

    aput-object v1, v0, v2

    sget-object v1, Lcom/zopim/android/sdk/api/FileTransfers$b;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    aput-object v1, v0, v3

    sget-object v1, Lcom/zopim/android/sdk/api/FileTransfers$b;->c:Lcom/zopim/android/sdk/api/FileTransfers$b;

    aput-object v1, v0, v4

    sget-object v1, Lcom/zopim/android/sdk/api/FileTransfers$b;->d:Lcom/zopim/android/sdk/api/FileTransfers$b;

    aput-object v1, v0, v5

    sget-object v1, Lcom/zopim/android/sdk/api/FileTransfers$b;->e:Lcom/zopim/android/sdk/api/FileTransfers$b;

    aput-object v1, v0, v6

    sput-object v0, Lcom/zopim/android/sdk/api/FileTransfers$b;->f:[Lcom/zopim/android/sdk/api/FileTransfers$b;

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

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/api/FileTransfers$b;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/api/FileTransfers$b;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/api/FileTransfers$b;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/api/FileTransfers$b;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/FileTransfers$b;->f:[Lcom/zopim/android/sdk/api/FileTransfers$b;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/api/FileTransfers$b;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/api/FileTransfers$b;

    return-object v0
.end method
