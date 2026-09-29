.class public final enum Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;
.super Ljava/lang/Enum;
.source "ScreenshotPreviewFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "ScreenshotAction"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

.field public static final enum ADD:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

.field public static final enum CHANGE:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

.field public static final enum REMOVE:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

.field public static final enum SEND:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;


# direct methods
.method static constructor <clinit>()V
    .locals 6

    .line 283
    new-instance v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    const-string v1, "ADD"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;->ADD:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    .line 284
    new-instance v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    const-string v1, "SEND"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;->SEND:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    .line 285
    new-instance v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    const-string v1, "REMOVE"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;->REMOVE:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    .line 286
    new-instance v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    const-string v1, "CHANGE"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;->CHANGE:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    const/4 v0, 0x4

    .line 282
    new-array v0, v0, [Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    sget-object v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;->ADD:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;->SEND:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;->REMOVE:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    aput-object v1, v0, v4

    sget-object v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;->CHANGE:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    aput-object v1, v0, v5

    sput-object v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;->$VALUES:[Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 282
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;
    .locals 1

    .line 282
    const-class v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;
    .locals 1

    .line 282
    sget-object v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;->$VALUES:[Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    invoke-virtual {v0}, [Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$ScreenshotAction;

    return-object v0
.end method
