.class public final enum Lcom/helpshift/redaction/RedactionState;
.super Ljava/lang/Enum;
.source "RedactionState.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/redaction/RedactionState;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/redaction/RedactionState;

.field public static final enum COMPLETED:Lcom/helpshift/redaction/RedactionState;

.field public static final enum IN_PROGRESS:Lcom/helpshift/redaction/RedactionState;

.field public static final enum PENDING:Lcom/helpshift/redaction/RedactionState;


# direct methods
.method static constructor <clinit>()V
    .locals 5

    .line 7
    new-instance v0, Lcom/helpshift/redaction/RedactionState;

    const-string v1, "PENDING"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/redaction/RedactionState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/redaction/RedactionState;->PENDING:Lcom/helpshift/redaction/RedactionState;

    .line 8
    new-instance v0, Lcom/helpshift/redaction/RedactionState;

    const-string v1, "IN_PROGRESS"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/redaction/RedactionState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/redaction/RedactionState;->IN_PROGRESS:Lcom/helpshift/redaction/RedactionState;

    .line 9
    new-instance v0, Lcom/helpshift/redaction/RedactionState;

    const-string v1, "COMPLETED"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/redaction/RedactionState;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/redaction/RedactionState;->COMPLETED:Lcom/helpshift/redaction/RedactionState;

    const/4 v0, 0x3

    .line 6
    new-array v0, v0, [Lcom/helpshift/redaction/RedactionState;

    sget-object v1, Lcom/helpshift/redaction/RedactionState;->PENDING:Lcom/helpshift/redaction/RedactionState;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/redaction/RedactionState;->IN_PROGRESS:Lcom/helpshift/redaction/RedactionState;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/redaction/RedactionState;->COMPLETED:Lcom/helpshift/redaction/RedactionState;

    aput-object v1, v0, v4

    sput-object v0, Lcom/helpshift/redaction/RedactionState;->$VALUES:[Lcom/helpshift/redaction/RedactionState;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 6
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/redaction/RedactionState;
    .locals 1

    .line 6
    const-class v0, Lcom/helpshift/redaction/RedactionState;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/redaction/RedactionState;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/redaction/RedactionState;
    .locals 1

    .line 6
    sget-object v0, Lcom/helpshift/redaction/RedactionState;->$VALUES:[Lcom/helpshift/redaction/RedactionState;

    invoke-virtual {v0}, [Lcom/helpshift/redaction/RedactionState;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/redaction/RedactionState;

    return-object v0
.end method
