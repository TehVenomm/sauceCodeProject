.class public final enum Lcom/helpshift/support/util/AppSessionConstants$Screen;
.super Ljava/lang/Enum;
.source "AppSessionConstants.java"

# interfaces
.implements Ljava/io/Serializable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/util/AppSessionConstants;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "Screen"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/support/util/AppSessionConstants$Screen;",
        ">;",
        "Ljava/io/Serializable;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/support/util/AppSessionConstants$Screen;

.field public static final enum CONVERSATION:Lcom/helpshift/support/util/AppSessionConstants$Screen;

.field public static final enum CONVERSATION_INFO:Lcom/helpshift/support/util/AppSessionConstants$Screen;

.field public static final enum NEW_CONVERSATION:Lcom/helpshift/support/util/AppSessionConstants$Screen;

.field public static final enum SCREENSHOT_PREVIEW:Lcom/helpshift/support/util/AppSessionConstants$Screen;


# direct methods
.method static constructor <clinit>()V
    .locals 6

    .line 15
    new-instance v0, Lcom/helpshift/support/util/AppSessionConstants$Screen;

    const-string v1, "NEW_CONVERSATION"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/support/util/AppSessionConstants$Screen;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/util/AppSessionConstants$Screen;->NEW_CONVERSATION:Lcom/helpshift/support/util/AppSessionConstants$Screen;

    .line 16
    new-instance v0, Lcom/helpshift/support/util/AppSessionConstants$Screen;

    const-string v1, "CONVERSATION"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/support/util/AppSessionConstants$Screen;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/util/AppSessionConstants$Screen;->CONVERSATION:Lcom/helpshift/support/util/AppSessionConstants$Screen;

    .line 17
    new-instance v0, Lcom/helpshift/support/util/AppSessionConstants$Screen;

    const-string v1, "CONVERSATION_INFO"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/support/util/AppSessionConstants$Screen;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/util/AppSessionConstants$Screen;->CONVERSATION_INFO:Lcom/helpshift/support/util/AppSessionConstants$Screen;

    .line 18
    new-instance v0, Lcom/helpshift/support/util/AppSessionConstants$Screen;

    const-string v1, "SCREENSHOT_PREVIEW"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5}, Lcom/helpshift/support/util/AppSessionConstants$Screen;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/util/AppSessionConstants$Screen;->SCREENSHOT_PREVIEW:Lcom/helpshift/support/util/AppSessionConstants$Screen;

    const/4 v0, 0x4

    .line 14
    new-array v0, v0, [Lcom/helpshift/support/util/AppSessionConstants$Screen;

    sget-object v1, Lcom/helpshift/support/util/AppSessionConstants$Screen;->NEW_CONVERSATION:Lcom/helpshift/support/util/AppSessionConstants$Screen;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/support/util/AppSessionConstants$Screen;->CONVERSATION:Lcom/helpshift/support/util/AppSessionConstants$Screen;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/support/util/AppSessionConstants$Screen;->CONVERSATION_INFO:Lcom/helpshift/support/util/AppSessionConstants$Screen;

    aput-object v1, v0, v4

    sget-object v1, Lcom/helpshift/support/util/AppSessionConstants$Screen;->SCREENSHOT_PREVIEW:Lcom/helpshift/support/util/AppSessionConstants$Screen;

    aput-object v1, v0, v5

    sput-object v0, Lcom/helpshift/support/util/AppSessionConstants$Screen;->$VALUES:[Lcom/helpshift/support/util/AppSessionConstants$Screen;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 14
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/support/util/AppSessionConstants$Screen;
    .locals 1

    .line 14
    const-class v0, Lcom/helpshift/support/util/AppSessionConstants$Screen;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/support/util/AppSessionConstants$Screen;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/support/util/AppSessionConstants$Screen;
    .locals 1

    .line 14
    sget-object v0, Lcom/helpshift/support/util/AppSessionConstants$Screen;->$VALUES:[Lcom/helpshift/support/util/AppSessionConstants$Screen;

    invoke-virtual {v0}, [Lcom/helpshift/support/util/AppSessionConstants$Screen;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/support/util/AppSessionConstants$Screen;

    return-object v0
.end method
