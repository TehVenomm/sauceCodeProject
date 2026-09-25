.class public final enum Lcom/helpshift/conversation/dto/IssueState;
.super Ljava/lang/Enum;
.source "IssueState.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/conversation/dto/IssueState;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/conversation/dto/IssueState;

.field public static final enum AGENT_REPLIED:Lcom/helpshift/conversation/dto/IssueState;

.field public static final enum ARCHIVED:Lcom/helpshift/conversation/dto/IssueState;

.field public static final enum AUTHOR_MISMATCH:Lcom/helpshift/conversation/dto/IssueState;

.field public static final enum COMPLETED_ISSUE_CREATED:Lcom/helpshift/conversation/dto/IssueState;

.field public static final enum NEW:Lcom/helpshift/conversation/dto/IssueState;

.field public static final enum NEW_FOR_AGENT:Lcom/helpshift/conversation/dto/IssueState;

.field public static final enum PENDING_REASSIGNMENT:Lcom/helpshift/conversation/dto/IssueState;

.field public static final enum REJECTED:Lcom/helpshift/conversation/dto/IssueState;

.field public static final enum RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

.field public static final enum RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

.field public static final enum RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

.field public static final enum UNKNOWN:Lcom/helpshift/conversation/dto/IssueState;

.field public static final enum WAITING_FOR_AGENT:Lcom/helpshift/conversation/dto/IssueState;

.field private static final map:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/Integer;",
            "Lcom/helpshift/conversation/dto/IssueState;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field private final value:I


# direct methods
.method static constructor <clinit>()V
    .locals 16

    .line 8
    new-instance v0, Lcom/helpshift/conversation/dto/IssueState;

    const-string v1, "NEW"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2, v2}, Lcom/helpshift/conversation/dto/IssueState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/dto/IssueState;->NEW:Lcom/helpshift/conversation/dto/IssueState;

    .line 9
    new-instance v0, Lcom/helpshift/conversation/dto/IssueState;

    const-string v1, "NEW_FOR_AGENT"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3, v3}, Lcom/helpshift/conversation/dto/IssueState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/dto/IssueState;->NEW_FOR_AGENT:Lcom/helpshift/conversation/dto/IssueState;

    .line 10
    new-instance v0, Lcom/helpshift/conversation/dto/IssueState;

    const-string v1, "AGENT_REPLIED"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4, v4}, Lcom/helpshift/conversation/dto/IssueState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/dto/IssueState;->AGENT_REPLIED:Lcom/helpshift/conversation/dto/IssueState;

    .line 11
    new-instance v0, Lcom/helpshift/conversation/dto/IssueState;

    const-string v1, "WAITING_FOR_AGENT"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5, v5}, Lcom/helpshift/conversation/dto/IssueState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/dto/IssueState;->WAITING_FOR_AGENT:Lcom/helpshift/conversation/dto/IssueState;

    .line 12
    new-instance v0, Lcom/helpshift/conversation/dto/IssueState;

    const-string v1, "RESOLUTION_REQUESTED"

    const/4 v6, 0x4

    invoke-direct {v0, v1, v6, v6}, Lcom/helpshift/conversation/dto/IssueState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

    .line 13
    new-instance v0, Lcom/helpshift/conversation/dto/IssueState;

    const-string v1, "REJECTED"

    const/4 v7, 0x5

    invoke-direct {v0, v1, v7, v7}, Lcom/helpshift/conversation/dto/IssueState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/dto/IssueState;->REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    .line 14
    new-instance v0, Lcom/helpshift/conversation/dto/IssueState;

    const-string v1, "PENDING_REASSIGNMENT"

    const/4 v8, 0x6

    invoke-direct {v0, v1, v8, v8}, Lcom/helpshift/conversation/dto/IssueState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/dto/IssueState;->PENDING_REASSIGNMENT:Lcom/helpshift/conversation/dto/IssueState;

    .line 15
    new-instance v0, Lcom/helpshift/conversation/dto/IssueState;

    const-string v1, "COMPLETED_ISSUE_CREATED"

    const/4 v9, 0x7

    invoke-direct {v0, v1, v9, v9}, Lcom/helpshift/conversation/dto/IssueState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/dto/IssueState;->COMPLETED_ISSUE_CREATED:Lcom/helpshift/conversation/dto/IssueState;

    .line 16
    new-instance v0, Lcom/helpshift/conversation/dto/IssueState;

    const-string v1, "RESOLUTION_ACCEPTED"

    const/16 v10, 0x8

    const/16 v11, 0x65

    invoke-direct {v0, v1, v10, v11}, Lcom/helpshift/conversation/dto/IssueState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    .line 17
    new-instance v0, Lcom/helpshift/conversation/dto/IssueState;

    const-string v1, "RESOLUTION_REJECTED"

    const/16 v11, 0x9

    const/16 v12, 0x66

    invoke-direct {v0, v1, v11, v12}, Lcom/helpshift/conversation/dto/IssueState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    .line 18
    new-instance v0, Lcom/helpshift/conversation/dto/IssueState;

    const-string v1, "ARCHIVED"

    const/16 v12, 0xa

    const/16 v13, 0x67

    invoke-direct {v0, v1, v12, v13}, Lcom/helpshift/conversation/dto/IssueState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/dto/IssueState;->ARCHIVED:Lcom/helpshift/conversation/dto/IssueState;

    .line 19
    new-instance v0, Lcom/helpshift/conversation/dto/IssueState;

    const-string v1, "AUTHOR_MISMATCH"

    const/16 v13, 0xb

    const/16 v14, 0x68

    invoke-direct {v0, v1, v13, v14}, Lcom/helpshift/conversation/dto/IssueState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/dto/IssueState;->AUTHOR_MISMATCH:Lcom/helpshift/conversation/dto/IssueState;

    .line 20
    new-instance v0, Lcom/helpshift/conversation/dto/IssueState;

    const-string v1, "UNKNOWN"

    const/16 v14, 0xc

    const/4 v15, -0x1

    invoke-direct {v0, v1, v14, v15}, Lcom/helpshift/conversation/dto/IssueState;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/conversation/dto/IssueState;->UNKNOWN:Lcom/helpshift/conversation/dto/IssueState;

    const/16 v0, 0xd

    .line 6
    new-array v0, v0, [Lcom/helpshift/conversation/dto/IssueState;

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->NEW:Lcom/helpshift/conversation/dto/IssueState;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->NEW_FOR_AGENT:Lcom/helpshift/conversation/dto/IssueState;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->AGENT_REPLIED:Lcom/helpshift/conversation/dto/IssueState;

    aput-object v1, v0, v4

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->WAITING_FOR_AGENT:Lcom/helpshift/conversation/dto/IssueState;

    aput-object v1, v0, v5

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REQUESTED:Lcom/helpshift/conversation/dto/IssueState;

    aput-object v1, v0, v6

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    aput-object v1, v0, v7

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->PENDING_REASSIGNMENT:Lcom/helpshift/conversation/dto/IssueState;

    aput-object v1, v0, v8

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->COMPLETED_ISSUE_CREATED:Lcom/helpshift/conversation/dto/IssueState;

    aput-object v1, v0, v9

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_ACCEPTED:Lcom/helpshift/conversation/dto/IssueState;

    aput-object v1, v0, v10

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->RESOLUTION_REJECTED:Lcom/helpshift/conversation/dto/IssueState;

    aput-object v1, v0, v11

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->ARCHIVED:Lcom/helpshift/conversation/dto/IssueState;

    aput-object v1, v0, v12

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->AUTHOR_MISMATCH:Lcom/helpshift/conversation/dto/IssueState;

    aput-object v1, v0, v13

    sget-object v1, Lcom/helpshift/conversation/dto/IssueState;->UNKNOWN:Lcom/helpshift/conversation/dto/IssueState;

    aput-object v1, v0, v14

    sput-object v0, Lcom/helpshift/conversation/dto/IssueState;->$VALUES:[Lcom/helpshift/conversation/dto/IssueState;

    .line 22
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    sput-object v0, Lcom/helpshift/conversation/dto/IssueState;->map:Ljava/util/Map;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;II)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)V"
        }
    .end annotation

    .line 26
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    .line 27
    iput p3, p0, Lcom/helpshift/conversation/dto/IssueState;->value:I

    return-void
.end method

.method public static fromInt(I)Lcom/helpshift/conversation/dto/IssueState;
    .locals 6

    .line 33
    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->map:Ljava/util/Map;

    invoke-interface {v0}, Ljava/util/Map;->size()I

    move-result v0

    if-nez v0, :cond_0

    .line 34
    invoke-static {}, Lcom/helpshift/conversation/dto/IssueState;->values()[Lcom/helpshift/conversation/dto/IssueState;

    move-result-object v0

    array-length v1, v0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_0

    aget-object v3, v0, v2

    .line 35
    sget-object v4, Lcom/helpshift/conversation/dto/IssueState;->map:Ljava/util/Map;

    iget v5, v3, Lcom/helpshift/conversation/dto/IssueState;->value:I

    invoke-static {v5}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v5

    invoke-interface {v4, v5, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 39
    :cond_0
    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->map:Ljava/util/Map;

    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p0

    invoke-interface {v0, p0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Lcom/helpshift/conversation/dto/IssueState;

    if-nez p0, :cond_1

    .line 41
    sget-object p0, Lcom/helpshift/conversation/dto/IssueState;->UNKNOWN:Lcom/helpshift/conversation/dto/IssueState;

    return-object p0

    :cond_1
    return-object p0
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/conversation/dto/IssueState;
    .locals 1

    .line 6
    const-class v0, Lcom/helpshift/conversation/dto/IssueState;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/conversation/dto/IssueState;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/conversation/dto/IssueState;
    .locals 1

    .line 6
    sget-object v0, Lcom/helpshift/conversation/dto/IssueState;->$VALUES:[Lcom/helpshift/conversation/dto/IssueState;

    invoke-virtual {v0}, [Lcom/helpshift/conversation/dto/IssueState;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/conversation/dto/IssueState;

    return-object v0
.end method


# virtual methods
.method public getValue()I
    .locals 1

    .line 49
    iget v0, p0, Lcom/helpshift/conversation/dto/IssueState;->value:I

    return v0
.end method
