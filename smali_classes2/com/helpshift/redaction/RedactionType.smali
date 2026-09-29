.class public final enum Lcom/helpshift/redaction/RedactionType;
.super Ljava/lang/Enum;
.source "RedactionType.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/redaction/RedactionType;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/redaction/RedactionType;

.field public static final enum CONVERSATION:Lcom/helpshift/redaction/RedactionType;

.field public static final enum USER:Lcom/helpshift/redaction/RedactionType;


# direct methods
.method static constructor <clinit>()V
    .locals 4

    .line 7
    new-instance v0, Lcom/helpshift/redaction/RedactionType;

    const-string v1, "USER"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/redaction/RedactionType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/redaction/RedactionType;->USER:Lcom/helpshift/redaction/RedactionType;

    .line 8
    new-instance v0, Lcom/helpshift/redaction/RedactionType;

    const-string v1, "CONVERSATION"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/redaction/RedactionType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/redaction/RedactionType;->CONVERSATION:Lcom/helpshift/redaction/RedactionType;

    const/4 v0, 0x2

    .line 6
    new-array v0, v0, [Lcom/helpshift/redaction/RedactionType;

    sget-object v1, Lcom/helpshift/redaction/RedactionType;->USER:Lcom/helpshift/redaction/RedactionType;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/redaction/RedactionType;->CONVERSATION:Lcom/helpshift/redaction/RedactionType;

    aput-object v1, v0, v3

    sput-object v0, Lcom/helpshift/redaction/RedactionType;->$VALUES:[Lcom/helpshift/redaction/RedactionType;

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

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/redaction/RedactionType;
    .locals 1

    .line 6
    const-class v0, Lcom/helpshift/redaction/RedactionType;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/redaction/RedactionType;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/redaction/RedactionType;
    .locals 1

    .line 6
    sget-object v0, Lcom/helpshift/redaction/RedactionType;->$VALUES:[Lcom/helpshift/redaction/RedactionType;

    invoke-virtual {v0}, [Lcom/helpshift/redaction/RedactionType;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/redaction/RedactionType;

    return-object v0
.end method
