.class synthetic Lcom/zopim/android/sdk/attachment/ui/b;
.super Ljava/lang/Object;


# static fields
.field static final synthetic a:[I


# direct methods
.method static constructor <clinit>()V
    .locals 3

    invoke-static {}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;->values()[Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lcom/zopim/android/sdk/attachment/ui/b;->a:[I

    :try_start_0
    sget-object v0, Lcom/zopim/android/sdk/attachment/ui/b;->a:[I

    sget-object v1, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;->a:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;->ordinal()I

    move-result v1

    const/4 v2, 0x1

    aput v2, v0, v1
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :try_start_1
    sget-object v0, Lcom/zopim/android/sdk/attachment/ui/b;->a:[I

    sget-object v1, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;->b:Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/attachment/ui/AttachmentSourceSelectorDialog$a;->ordinal()I

    move-result v1

    const/4 v2, 0x2

    aput v2, v0, v1
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    return-void
.end method
