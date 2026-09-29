.class public final enum Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;
.super Ljava/lang/Enum;
.source "HSSearch.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/HSSearch;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "HS_SEARCH_OPTIONS"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

.field public static final enum FULL_SEARCH:Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

.field public static final enum KEYWORD_SEARCH:Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

.field public static final enum METAPHONE_SEARCH:Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;


# direct methods
.method static constructor <clinit>()V
    .locals 5

    .line 520
    new-instance v0, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    const-string v1, "FULL_SEARCH"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;->FULL_SEARCH:Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    .line 521
    new-instance v0, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    const-string v1, "METAPHONE_SEARCH"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;->METAPHONE_SEARCH:Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    .line 522
    new-instance v0, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    const-string v1, "KEYWORD_SEARCH"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;->KEYWORD_SEARCH:Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    const/4 v0, 0x3

    .line 519
    new-array v0, v0, [Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    sget-object v1, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;->FULL_SEARCH:Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;->METAPHONE_SEARCH:Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;->KEYWORD_SEARCH:Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    aput-object v1, v0, v4

    sput-object v0, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;->$VALUES:[Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 519
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;
    .locals 1

    .line 519
    const-class v0, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;
    .locals 1

    .line 519
    sget-object v0, Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;->$VALUES:[Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    invoke-virtual {v0}, [Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/support/HSSearch$HS_SEARCH_OPTIONS;

    return-object v0
.end method
