.class public final enum Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;
.super Ljava/lang/Enum;
.source "ScreenshotPreviewFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "LaunchSource"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

.field public static final enum ATTACHMENT_DRAFT:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

.field public static final enum GALLERY_APP:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;


# direct methods
.method static constructor <clinit>()V
    .locals 4

    .line 290
    new-instance v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    const-string v1, "ATTACHMENT_DRAFT"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;->ATTACHMENT_DRAFT:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    .line 291
    new-instance v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    const-string v1, "GALLERY_APP"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;->GALLERY_APP:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    const/4 v0, 0x2

    .line 289
    new-array v0, v0, [Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    sget-object v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;->ATTACHMENT_DRAFT:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;->GALLERY_APP:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    aput-object v1, v0, v3

    sput-object v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;->$VALUES:[Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 289
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;
    .locals 1

    .line 289
    const-class v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;
    .locals 1

    .line 289
    sget-object v0, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;->$VALUES:[Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    invoke-virtual {v0}, [Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    return-object v0
.end method
