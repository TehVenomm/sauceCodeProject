.class final enum Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4018
    name = "a"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;",
        ">;"
    }
.end annotation


# static fields
.field public static final enum a:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

.field public static final enum b:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

.field private static final synthetic c:[Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;


# direct methods
.method static constructor <clinit>()V
    .locals 4

    new-instance v0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    const-string v1, "GALLERY"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;->a:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    new-instance v0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    const-string v1, "CAMERA"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;->b:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    const/4 v0, 0x2

    new-array v0, v0, [Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    sget-object v1, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;->a:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    aput-object v1, v0, v2

    sget-object v1, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;->b:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    aput-object v1, v0, v3

    sput-object v0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;->c:[Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;->c:[Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    return-object v0
.end method
