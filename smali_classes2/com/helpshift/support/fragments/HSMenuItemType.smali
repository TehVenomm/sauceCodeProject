.class public final enum Lcom/helpshift/support/fragments/HSMenuItemType;
.super Ljava/lang/Enum;
.source "HSMenuItemType.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/support/fragments/HSMenuItemType;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/support/fragments/HSMenuItemType;

.field public static final enum SCREENSHOT_ATTACHMENT:Lcom/helpshift/support/fragments/HSMenuItemType;

.field public static final enum START_NEW_CONVERSATION:Lcom/helpshift/support/fragments/HSMenuItemType;


# direct methods
.method static constructor <clinit>()V
    .locals 4

    .line 4
    new-instance v0, Lcom/helpshift/support/fragments/HSMenuItemType;

    const-string v1, "START_NEW_CONVERSATION"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/support/fragments/HSMenuItemType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/fragments/HSMenuItemType;->START_NEW_CONVERSATION:Lcom/helpshift/support/fragments/HSMenuItemType;

    .line 5
    new-instance v0, Lcom/helpshift/support/fragments/HSMenuItemType;

    const-string v1, "SCREENSHOT_ATTACHMENT"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/support/fragments/HSMenuItemType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/fragments/HSMenuItemType;->SCREENSHOT_ATTACHMENT:Lcom/helpshift/support/fragments/HSMenuItemType;

    const/4 v0, 0x2

    .line 3
    new-array v0, v0, [Lcom/helpshift/support/fragments/HSMenuItemType;

    sget-object v1, Lcom/helpshift/support/fragments/HSMenuItemType;->START_NEW_CONVERSATION:Lcom/helpshift/support/fragments/HSMenuItemType;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/support/fragments/HSMenuItemType;->SCREENSHOT_ATTACHMENT:Lcom/helpshift/support/fragments/HSMenuItemType;

    aput-object v1, v0, v3

    sput-object v0, Lcom/helpshift/support/fragments/HSMenuItemType;->$VALUES:[Lcom/helpshift/support/fragments/HSMenuItemType;

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

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/support/fragments/HSMenuItemType;
    .locals 1

    .line 3
    const-class v0, Lcom/helpshift/support/fragments/HSMenuItemType;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/support/fragments/HSMenuItemType;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/support/fragments/HSMenuItemType;
    .locals 1

    .line 3
    sget-object v0, Lcom/helpshift/support/fragments/HSMenuItemType;->$VALUES:[Lcom/helpshift/support/fragments/HSMenuItemType;

    invoke-virtual {v0}, [Lcom/helpshift/support/fragments/HSMenuItemType;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/support/fragments/HSMenuItemType;

    return-object v0
.end method
