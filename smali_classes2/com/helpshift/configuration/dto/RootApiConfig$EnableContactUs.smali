.class public final enum Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;
.super Ljava/lang/Enum;
.source "RootApiConfig.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/configuration/dto/RootApiConfig;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "EnableContactUs"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

.field public static final enum AFTER_MARKING_ANSWER_UNHELPFUL:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

.field public static final enum AFTER_VIEWING_FAQS:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

.field public static final enum ALWAYS:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

.field public static final enum NEVER:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;


# instance fields
.field private final value:I


# direct methods
.method static constructor <clinit>()V
    .locals 6

    .line 50
    new-instance v0, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    const-string v1, "ALWAYS"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2, v2}, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->ALWAYS:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    .line 51
    new-instance v0, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    const-string v1, "NEVER"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3, v3}, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->NEVER:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    .line 52
    new-instance v0, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    const-string v1, "AFTER_VIEWING_FAQS"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4, v4}, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->AFTER_VIEWING_FAQS:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    .line 53
    new-instance v0, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    const-string v1, "AFTER_MARKING_ANSWER_UNHELPFUL"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5, v5}, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->AFTER_MARKING_ANSWER_UNHELPFUL:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    const/4 v0, 0x4

    .line 49
    new-array v0, v0, [Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    sget-object v1, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->ALWAYS:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->NEVER:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->AFTER_VIEWING_FAQS:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    aput-object v1, v0, v4

    sget-object v1, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->AFTER_MARKING_ANSWER_UNHELPFUL:Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    aput-object v1, v0, v5

    sput-object v0, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->$VALUES:[Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;II)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)V"
        }
    .end annotation

    .line 57
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    .line 58
    iput p3, p0, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->value:I

    return-void
.end method

.method public static fromInt(I)Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;
    .locals 5

    .line 62
    invoke-static {}, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->values()[Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    move-result-object v0

    array-length v1, v0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_1

    aget-object v3, v0, v2

    .line 63
    invoke-virtual {v3}, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->getValue()I

    move-result v4

    if-ne v4, p0, :cond_0

    return-object v3

    :cond_0
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_1
    const/4 p0, 0x0

    return-object p0
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;
    .locals 1

    .line 49
    const-class v0, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;
    .locals 1

    .line 49
    sget-object v0, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->$VALUES:[Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    invoke-virtual {v0}, [Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;

    return-object v0
.end method


# virtual methods
.method public getValue()I
    .locals 1

    .line 71
    iget v0, p0, Lcom/helpshift/configuration/dto/RootApiConfig$EnableContactUs;->value:I

    return v0
.end method
