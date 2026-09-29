.class public final enum Lcom/helpshift/widget/TextViewState$TextViewStatesError;
.super Ljava/lang/Enum;
.source "TextViewState.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/widget/TextViewState;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "TextViewStatesError"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/widget/TextViewState$TextViewStatesError;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/widget/TextViewState$TextViewStatesError;

.field public static final enum EMPTY:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

.field public static final enum INVALID_EMAIL:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

.field public static final enum LESS_THAN_MINIMUM_LENGTH:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

.field public static final enum ONLY_SPECIAL_CHARACTERS:Lcom/helpshift/widget/TextViewState$TextViewStatesError;


# direct methods
.method static constructor <clinit>()V
    .locals 6

    .line 35
    new-instance v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    const-string v1, "EMPTY"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/widget/TextViewState$TextViewStatesError;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->EMPTY:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    .line 36
    new-instance v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    const-string v1, "LESS_THAN_MINIMUM_LENGTH"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/widget/TextViewState$TextViewStatesError;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->LESS_THAN_MINIMUM_LENGTH:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    .line 37
    new-instance v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    const-string v1, "ONLY_SPECIAL_CHARACTERS"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/widget/TextViewState$TextViewStatesError;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->ONLY_SPECIAL_CHARACTERS:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    .line 38
    new-instance v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    const-string v1, "INVALID_EMAIL"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5}, Lcom/helpshift/widget/TextViewState$TextViewStatesError;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->INVALID_EMAIL:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    const/4 v0, 0x4

    .line 34
    new-array v0, v0, [Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    sget-object v1, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->EMPTY:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->LESS_THAN_MINIMUM_LENGTH:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->ONLY_SPECIAL_CHARACTERS:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    aput-object v1, v0, v4

    sget-object v1, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->INVALID_EMAIL:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    aput-object v1, v0, v5

    sput-object v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->$VALUES:[Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 34
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/widget/TextViewState$TextViewStatesError;
    .locals 1

    .line 34
    const-class v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/widget/TextViewState$TextViewStatesError;
    .locals 1

    .line 34
    sget-object v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->$VALUES:[Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    invoke-virtual {v0}, [Lcom/helpshift/widget/TextViewState$TextViewStatesError;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    return-object v0
.end method
