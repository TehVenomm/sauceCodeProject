.class public final enum Lcom/helpshift/delegate/AuthenticationFailureReason;
.super Ljava/lang/Enum;
.source "AuthenticationFailureReason.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/delegate/AuthenticationFailureReason;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/delegate/AuthenticationFailureReason;

.field public static final enum AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/delegate/AuthenticationFailureReason;

.field public static final enum INVALID_AUTH_TOKEN:Lcom/helpshift/delegate/AuthenticationFailureReason;


# instance fields
.field private value:I


# direct methods
.method static constructor <clinit>()V
    .locals 4

    .line 4
    new-instance v0, Lcom/helpshift/delegate/AuthenticationFailureReason;

    const-string v1, "AUTH_TOKEN_NOT_PROVIDED"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2, v2}, Lcom/helpshift/delegate/AuthenticationFailureReason;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/delegate/AuthenticationFailureReason;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/delegate/AuthenticationFailureReason;

    .line 5
    new-instance v0, Lcom/helpshift/delegate/AuthenticationFailureReason;

    const-string v1, "INVALID_AUTH_TOKEN"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3, v3}, Lcom/helpshift/delegate/AuthenticationFailureReason;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/delegate/AuthenticationFailureReason;->INVALID_AUTH_TOKEN:Lcom/helpshift/delegate/AuthenticationFailureReason;

    const/4 v0, 0x2

    .line 3
    new-array v0, v0, [Lcom/helpshift/delegate/AuthenticationFailureReason;

    sget-object v1, Lcom/helpshift/delegate/AuthenticationFailureReason;->AUTH_TOKEN_NOT_PROVIDED:Lcom/helpshift/delegate/AuthenticationFailureReason;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/delegate/AuthenticationFailureReason;->INVALID_AUTH_TOKEN:Lcom/helpshift/delegate/AuthenticationFailureReason;

    aput-object v1, v0, v3

    sput-object v0, Lcom/helpshift/delegate/AuthenticationFailureReason;->$VALUES:[Lcom/helpshift/delegate/AuthenticationFailureReason;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;II)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)V"
        }
    .end annotation

    .line 9
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    .line 10
    iput p3, p0, Lcom/helpshift/delegate/AuthenticationFailureReason;->value:I

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/delegate/AuthenticationFailureReason;
    .locals 1

    .line 3
    const-class v0, Lcom/helpshift/delegate/AuthenticationFailureReason;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/delegate/AuthenticationFailureReason;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/delegate/AuthenticationFailureReason;
    .locals 1

    .line 3
    sget-object v0, Lcom/helpshift/delegate/AuthenticationFailureReason;->$VALUES:[Lcom/helpshift/delegate/AuthenticationFailureReason;

    invoke-virtual {v0}, [Lcom/helpshift/delegate/AuthenticationFailureReason;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/delegate/AuthenticationFailureReason;

    return-object v0
.end method


# virtual methods
.method public getValue()I
    .locals 1

    .line 14
    iget v0, p0, Lcom/helpshift/delegate/AuthenticationFailureReason;->value:I

    return v0
.end method
