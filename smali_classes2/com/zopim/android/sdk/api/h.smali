.class synthetic Lcom/zopim/android/sdk/api/h;
.super Ljava/lang/Object;


# static fields
.field static final synthetic a:[I

.field static final synthetic b:[I


# direct methods
.method static constructor <clinit>()V
    .locals 4

    invoke-static {}, Lcom/zopim/android/sdk/model/ChatLog$Type;->values()[Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lcom/zopim/android/sdk/api/h;->b:[I

    const/4 v0, 0x1

    :try_start_0
    sget-object v1, Lcom/zopim/android/sdk/api/h;->b:[I

    sget-object v2, Lcom/zopim/android/sdk/model/ChatLog$Type;->ATTACHMENT_UPLOAD:Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-virtual {v2}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result v2

    aput v0, v1, v2
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :try_start_1
    sget-object v1, Lcom/zopim/android/sdk/api/h;->b:[I

    sget-object v2, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_AGENT:Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-virtual {v2}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result v2

    const/4 v3, 0x2

    aput v3, v1, v2
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    invoke-static {}, Lcom/zopim/android/sdk/api/FileTransfers$b;->values()[Lcom/zopim/android/sdk/api/FileTransfers$b;

    move-result-object v1

    array-length v1, v1

    new-array v1, v1, [I

    sput-object v1, Lcom/zopim/android/sdk/api/h;->a:[I

    :try_start_2
    sget-object v1, Lcom/zopim/android/sdk/api/h;->a:[I

    sget-object v2, Lcom/zopim/android/sdk/api/FileTransfers$b;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    invoke-virtual {v2}, Lcom/zopim/android/sdk/api/FileTransfers$b;->ordinal()I

    move-result v2

    aput v0, v1, v2
    :try_end_2
    .catch Ljava/lang/NoSuchFieldError; {:try_start_2 .. :try_end_2} :catch_2

    :catch_2
    return-void
.end method
