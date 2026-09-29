.class public final enum Lcom/helpshift/account/domainmodel/UserSyncStatus;
.super Ljava/lang/Enum;
.source "UserSyncStatus.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/account/domainmodel/UserSyncStatus;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/account/domainmodel/UserSyncStatus;

.field public static final enum COMPLETED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

.field public static final enum FAILED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

.field public static final enum IN_PROGRESS:Lcom/helpshift/account/domainmodel/UserSyncStatus;

.field public static final enum NOT_STARTED:Lcom/helpshift/account/domainmodel/UserSyncStatus;


# direct methods
.method static constructor <clinit>()V
    .locals 6

    .line 5
    new-instance v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;

    const-string v1, "NOT_STARTED"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/account/domainmodel/UserSyncStatus;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;->NOT_STARTED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    .line 6
    new-instance v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;

    const-string v1, "IN_PROGRESS"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/account/domainmodel/UserSyncStatus;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;->IN_PROGRESS:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    .line 7
    new-instance v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;

    const-string v1, "COMPLETED"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/account/domainmodel/UserSyncStatus;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;->COMPLETED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    .line 8
    new-instance v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;

    const-string v1, "FAILED"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5}, Lcom/helpshift/account/domainmodel/UserSyncStatus;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;->FAILED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    const/4 v0, 0x4

    .line 4
    new-array v0, v0, [Lcom/helpshift/account/domainmodel/UserSyncStatus;

    sget-object v1, Lcom/helpshift/account/domainmodel/UserSyncStatus;->NOT_STARTED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/account/domainmodel/UserSyncStatus;->IN_PROGRESS:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/account/domainmodel/UserSyncStatus;->COMPLETED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    aput-object v1, v0, v4

    sget-object v1, Lcom/helpshift/account/domainmodel/UserSyncStatus;->FAILED:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    aput-object v1, v0, v5

    sput-object v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;->$VALUES:[Lcom/helpshift/account/domainmodel/UserSyncStatus;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 4
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/account/domainmodel/UserSyncStatus;
    .locals 1

    .line 4
    const-class v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/account/domainmodel/UserSyncStatus;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/account/domainmodel/UserSyncStatus;
    .locals 1

    .line 4
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSyncStatus;->$VALUES:[Lcom/helpshift/account/domainmodel/UserSyncStatus;

    invoke-virtual {v0}, [Lcom/helpshift/account/domainmodel/UserSyncStatus;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/account/domainmodel/UserSyncStatus;

    return-object v0
.end method
