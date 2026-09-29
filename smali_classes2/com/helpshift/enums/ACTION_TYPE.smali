.class public final enum Lcom/helpshift/enums/ACTION_TYPE;
.super Ljava/lang/Enum;
.source "ACTION_TYPE.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/enums/ACTION_TYPE;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/enums/ACTION_TYPE;

.field public static final enum LAUNCH_APP:Lcom/helpshift/enums/ACTION_TYPE;

.field public static final enum OPEN_DEEP_LINK:Lcom/helpshift/enums/ACTION_TYPE;

.field public static final enum SHOW_ALERT_TO_RATE_APP:Lcom/helpshift/enums/ACTION_TYPE;

.field public static final enum SHOW_CONVERSATION:Lcom/helpshift/enums/ACTION_TYPE;

.field public static final enum SHOW_FAQS:Lcom/helpshift/enums/ACTION_TYPE;

.field public static final enum SHOW_FAQ_SECTION:Lcom/helpshift/enums/ACTION_TYPE;

.field public static final enum SHOW_INBOX:Lcom/helpshift/enums/ACTION_TYPE;

.field public static final enum SHOW_SINGLE_FAQ:Lcom/helpshift/enums/ACTION_TYPE;


# instance fields
.field private final val:I


# direct methods
.method static constructor <clinit>()V
    .locals 11

    .line 4
    new-instance v0, Lcom/helpshift/enums/ACTION_TYPE;

    const-string v1, "OPEN_DEEP_LINK"

    const/4 v2, 0x0

    const/4 v3, 0x1

    invoke-direct {v0, v1, v2, v3}, Lcom/helpshift/enums/ACTION_TYPE;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/enums/ACTION_TYPE;->OPEN_DEEP_LINK:Lcom/helpshift/enums/ACTION_TYPE;

    .line 5
    new-instance v0, Lcom/helpshift/enums/ACTION_TYPE;

    const-string v1, "SHOW_FAQS"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v3, v4}, Lcom/helpshift/enums/ACTION_TYPE;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/enums/ACTION_TYPE;->SHOW_FAQS:Lcom/helpshift/enums/ACTION_TYPE;

    .line 6
    new-instance v0, Lcom/helpshift/enums/ACTION_TYPE;

    const-string v1, "SHOW_FAQ_SECTION"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v4, v5}, Lcom/helpshift/enums/ACTION_TYPE;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/enums/ACTION_TYPE;->SHOW_FAQ_SECTION:Lcom/helpshift/enums/ACTION_TYPE;

    .line 7
    new-instance v0, Lcom/helpshift/enums/ACTION_TYPE;

    const-string v1, "SHOW_CONVERSATION"

    const/4 v6, 0x4

    invoke-direct {v0, v1, v5, v6}, Lcom/helpshift/enums/ACTION_TYPE;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/enums/ACTION_TYPE;->SHOW_CONVERSATION:Lcom/helpshift/enums/ACTION_TYPE;

    .line 8
    new-instance v0, Lcom/helpshift/enums/ACTION_TYPE;

    const-string v1, "SHOW_SINGLE_FAQ"

    const/4 v7, 0x5

    invoke-direct {v0, v1, v6, v7}, Lcom/helpshift/enums/ACTION_TYPE;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/enums/ACTION_TYPE;->SHOW_SINGLE_FAQ:Lcom/helpshift/enums/ACTION_TYPE;

    .line 9
    new-instance v0, Lcom/helpshift/enums/ACTION_TYPE;

    const-string v1, "SHOW_ALERT_TO_RATE_APP"

    const/4 v8, 0x6

    invoke-direct {v0, v1, v7, v8}, Lcom/helpshift/enums/ACTION_TYPE;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/enums/ACTION_TYPE;->SHOW_ALERT_TO_RATE_APP:Lcom/helpshift/enums/ACTION_TYPE;

    .line 10
    new-instance v0, Lcom/helpshift/enums/ACTION_TYPE;

    const-string v1, "LAUNCH_APP"

    const/4 v9, 0x7

    invoke-direct {v0, v1, v8, v9}, Lcom/helpshift/enums/ACTION_TYPE;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/enums/ACTION_TYPE;->LAUNCH_APP:Lcom/helpshift/enums/ACTION_TYPE;

    .line 11
    new-instance v0, Lcom/helpshift/enums/ACTION_TYPE;

    const-string v1, "SHOW_INBOX"

    const/16 v10, 0x8

    invoke-direct {v0, v1, v9, v10}, Lcom/helpshift/enums/ACTION_TYPE;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/helpshift/enums/ACTION_TYPE;->SHOW_INBOX:Lcom/helpshift/enums/ACTION_TYPE;

    .line 3
    new-array v0, v10, [Lcom/helpshift/enums/ACTION_TYPE;

    sget-object v1, Lcom/helpshift/enums/ACTION_TYPE;->OPEN_DEEP_LINK:Lcom/helpshift/enums/ACTION_TYPE;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/enums/ACTION_TYPE;->SHOW_FAQS:Lcom/helpshift/enums/ACTION_TYPE;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/enums/ACTION_TYPE;->SHOW_FAQ_SECTION:Lcom/helpshift/enums/ACTION_TYPE;

    aput-object v1, v0, v4

    sget-object v1, Lcom/helpshift/enums/ACTION_TYPE;->SHOW_CONVERSATION:Lcom/helpshift/enums/ACTION_TYPE;

    aput-object v1, v0, v5

    sget-object v1, Lcom/helpshift/enums/ACTION_TYPE;->SHOW_SINGLE_FAQ:Lcom/helpshift/enums/ACTION_TYPE;

    aput-object v1, v0, v6

    sget-object v1, Lcom/helpshift/enums/ACTION_TYPE;->SHOW_ALERT_TO_RATE_APP:Lcom/helpshift/enums/ACTION_TYPE;

    aput-object v1, v0, v7

    sget-object v1, Lcom/helpshift/enums/ACTION_TYPE;->LAUNCH_APP:Lcom/helpshift/enums/ACTION_TYPE;

    aput-object v1, v0, v8

    sget-object v1, Lcom/helpshift/enums/ACTION_TYPE;->SHOW_INBOX:Lcom/helpshift/enums/ACTION_TYPE;

    aput-object v1, v0, v9

    sput-object v0, Lcom/helpshift/enums/ACTION_TYPE;->$VALUES:[Lcom/helpshift/enums/ACTION_TYPE;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;II)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)V"
        }
    .end annotation

    .line 15
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    .line 16
    iput p3, p0, Lcom/helpshift/enums/ACTION_TYPE;->val:I

    return-void
.end method

.method public static getEnum(I)Lcom/helpshift/enums/ACTION_TYPE;
    .locals 5

    .line 29
    invoke-static {}, Lcom/helpshift/enums/ACTION_TYPE;->values()[Lcom/helpshift/enums/ACTION_TYPE;

    move-result-object v0

    array-length v1, v0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_1

    aget-object v3, v0, v2

    .line 30
    iget v4, v3, Lcom/helpshift/enums/ACTION_TYPE;->val:I

    if-ne p0, v4, :cond_0

    return-object v3

    :cond_0
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 34
    :cond_1
    sget-object p0, Lcom/helpshift/enums/ACTION_TYPE;->LAUNCH_APP:Lcom/helpshift/enums/ACTION_TYPE;

    return-object p0
.end method

.method public static getEnum(Ljava/lang/String;)Lcom/helpshift/enums/ACTION_TYPE;
    .locals 0

    .line 21
    :try_start_0
    invoke-static {p0}, Ljava/lang/Integer;->parseInt(Ljava/lang/String;)I

    move-result p0

    invoke-static {p0}, Lcom/helpshift/enums/ACTION_TYPE;->getEnum(I)Lcom/helpshift/enums/ACTION_TYPE;

    move-result-object p0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    .line 24
    :catch_0
    sget-object p0, Lcom/helpshift/enums/ACTION_TYPE;->LAUNCH_APP:Lcom/helpshift/enums/ACTION_TYPE;

    return-object p0
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/enums/ACTION_TYPE;
    .locals 1

    .line 3
    const-class v0, Lcom/helpshift/enums/ACTION_TYPE;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/enums/ACTION_TYPE;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/enums/ACTION_TYPE;
    .locals 1

    .line 3
    sget-object v0, Lcom/helpshift/enums/ACTION_TYPE;->$VALUES:[Lcom/helpshift/enums/ACTION_TYPE;

    invoke-virtual {v0}, [Lcom/helpshift/enums/ACTION_TYPE;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/enums/ACTION_TYPE;

    return-object v0
.end method
