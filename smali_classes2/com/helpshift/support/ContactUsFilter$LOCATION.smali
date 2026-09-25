.class public final enum Lcom/helpshift/support/ContactUsFilter$LOCATION;
.super Ljava/lang/Enum;
.source "ContactUsFilter.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/ContactUsFilter;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "LOCATION"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/support/ContactUsFilter$LOCATION;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/support/ContactUsFilter$LOCATION;

.field public static final enum ACTION_BAR:Lcom/helpshift/support/ContactUsFilter$LOCATION;

.field public static final enum QUESTION_ACTION_BAR:Lcom/helpshift/support/ContactUsFilter$LOCATION;

.field public static final enum QUESTION_FOOTER:Lcom/helpshift/support/ContactUsFilter$LOCATION;

.field public static final enum SEARCH_FOOTER:Lcom/helpshift/support/ContactUsFilter$LOCATION;

.field public static final enum SEARCH_RESULT_ACTIVITY_HEADER:Lcom/helpshift/support/ContactUsFilter$LOCATION;


# direct methods
.method static constructor <clinit>()V
    .locals 7

    .line 79
    new-instance v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;

    const-string v1, "ACTION_BAR"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/support/ContactUsFilter$LOCATION;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->ACTION_BAR:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    .line 80
    new-instance v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;

    const-string v1, "SEARCH_FOOTER"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/support/ContactUsFilter$LOCATION;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->SEARCH_FOOTER:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    .line 81
    new-instance v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;

    const-string v1, "QUESTION_FOOTER"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/support/ContactUsFilter$LOCATION;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->QUESTION_FOOTER:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    .line 82
    new-instance v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;

    const-string v1, "QUESTION_ACTION_BAR"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5}, Lcom/helpshift/support/ContactUsFilter$LOCATION;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->QUESTION_ACTION_BAR:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    .line 83
    new-instance v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;

    const-string v1, "SEARCH_RESULT_ACTIVITY_HEADER"

    const/4 v6, 0x4

    invoke-direct {v0, v1, v6}, Lcom/helpshift/support/ContactUsFilter$LOCATION;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->SEARCH_RESULT_ACTIVITY_HEADER:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    const/4 v0, 0x5

    .line 78
    new-array v0, v0, [Lcom/helpshift/support/ContactUsFilter$LOCATION;

    sget-object v1, Lcom/helpshift/support/ContactUsFilter$LOCATION;->ACTION_BAR:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/support/ContactUsFilter$LOCATION;->SEARCH_FOOTER:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/support/ContactUsFilter$LOCATION;->QUESTION_FOOTER:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    aput-object v1, v0, v4

    sget-object v1, Lcom/helpshift/support/ContactUsFilter$LOCATION;->QUESTION_ACTION_BAR:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    aput-object v1, v0, v5

    sget-object v1, Lcom/helpshift/support/ContactUsFilter$LOCATION;->SEARCH_RESULT_ACTIVITY_HEADER:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    aput-object v1, v0, v6

    sput-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->$VALUES:[Lcom/helpshift/support/ContactUsFilter$LOCATION;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 78
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/support/ContactUsFilter$LOCATION;
    .locals 1

    .line 78
    const-class v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/support/ContactUsFilter$LOCATION;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/support/ContactUsFilter$LOCATION;
    .locals 1

    .line 78
    sget-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->$VALUES:[Lcom/helpshift/support/ContactUsFilter$LOCATION;

    invoke-virtual {v0}, [Lcom/helpshift/support/ContactUsFilter$LOCATION;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/support/ContactUsFilter$LOCATION;

    return-object v0
.end method
