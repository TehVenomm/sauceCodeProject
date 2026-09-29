.class public final enum Lcom/helpshift/migration/MigrationState;
.super Ljava/lang/Enum;
.source "MigrationState.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/migration/MigrationState;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/migration/MigrationState;

.field public static final enum COMPLETED:Lcom/helpshift/migration/MigrationState;

.field public static final enum FAILED:Lcom/helpshift/migration/MigrationState;

.field public static final enum IN_PROGRESS:Lcom/helpshift/migration/MigrationState;

.field public static final enum NOT_STARTED:Lcom/helpshift/migration/MigrationState;


# direct methods
.method static constructor <clinit>()V
    .locals 6

    .line 4
    new-instance v0, Lcom/helpshift/migration/MigrationState;

    const-string v1, "NOT_STARTED"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/migration/MigrationState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/migration/MigrationState;->NOT_STARTED:Lcom/helpshift/migration/MigrationState;

    .line 5
    new-instance v0, Lcom/helpshift/migration/MigrationState;

    const-string v1, "IN_PROGRESS"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/migration/MigrationState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/migration/MigrationState;->IN_PROGRESS:Lcom/helpshift/migration/MigrationState;

    .line 6
    new-instance v0, Lcom/helpshift/migration/MigrationState;

    const-string v1, "COMPLETED"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/migration/MigrationState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/migration/MigrationState;->COMPLETED:Lcom/helpshift/migration/MigrationState;

    .line 7
    new-instance v0, Lcom/helpshift/migration/MigrationState;

    const-string v1, "FAILED"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5}, Lcom/helpshift/migration/MigrationState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/migration/MigrationState;->FAILED:Lcom/helpshift/migration/MigrationState;

    const/4 v0, 0x4

    .line 3
    new-array v0, v0, [Lcom/helpshift/migration/MigrationState;

    sget-object v1, Lcom/helpshift/migration/MigrationState;->NOT_STARTED:Lcom/helpshift/migration/MigrationState;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/migration/MigrationState;->IN_PROGRESS:Lcom/helpshift/migration/MigrationState;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/migration/MigrationState;->COMPLETED:Lcom/helpshift/migration/MigrationState;

    aput-object v1, v0, v4

    sget-object v1, Lcom/helpshift/migration/MigrationState;->FAILED:Lcom/helpshift/migration/MigrationState;

    aput-object v1, v0, v5

    sput-object v0, Lcom/helpshift/migration/MigrationState;->$VALUES:[Lcom/helpshift/migration/MigrationState;

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

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/migration/MigrationState;
    .locals 1

    .line 3
    const-class v0, Lcom/helpshift/migration/MigrationState;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/migration/MigrationState;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/migration/MigrationState;
    .locals 1

    .line 3
    sget-object v0, Lcom/helpshift/migration/MigrationState;->$VALUES:[Lcom/helpshift/migration/MigrationState;

    invoke-virtual {v0}, [Lcom/helpshift/migration/MigrationState;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/migration/MigrationState;

    return-object v0
.end method
