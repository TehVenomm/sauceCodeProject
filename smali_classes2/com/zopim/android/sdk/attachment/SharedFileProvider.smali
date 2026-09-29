.class public final enum Lcom/zopim/android/sdk/attachment/SharedFileProvider;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/attachment/SharedFileProvider;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/zopim/android/sdk/attachment/SharedFileProvider;

.field public static final enum INSTANCE:Lcom/zopim/android/sdk/attachment/SharedFileProvider;

.field private static final LOG_TAG:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    new-instance v0, Lcom/zopim/android/sdk/attachment/SharedFileProvider;

    const-string v1, "INSTANCE"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/zopim/android/sdk/attachment/SharedFileProvider;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/attachment/SharedFileProvider;->INSTANCE:Lcom/zopim/android/sdk/attachment/SharedFileProvider;

    const/4 v0, 0x1

    new-array v0, v0, [Lcom/zopim/android/sdk/attachment/SharedFileProvider;

    sget-object v1, Lcom/zopim/android/sdk/attachment/SharedFileProvider;->INSTANCE:Lcom/zopim/android/sdk/attachment/SharedFileProvider;

    aput-object v1, v0, v2

    sput-object v0, Lcom/zopim/android/sdk/attachment/SharedFileProvider;->$VALUES:[Lcom/zopim/android/sdk/attachment/SharedFileProvider;

    const-class v0, Lcom/zopim/android/sdk/attachment/SharedFileProvider;

    invoke-virtual {v0}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/zopim/android/sdk/attachment/SharedFileProvider;->LOG_TAG:Ljava/lang/String;

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

.method public static getProviderUri(Landroid/content/Context;Ljava/io/File;)Landroid/net/Uri;
    .locals 4

    const/4 v0, 0x0

    if-eqz p1, :cond_1

    if-nez p0, :cond_0

    goto :goto_0

    :cond_0
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    sget v2, Lcom/zopim/android/sdk/R$string;->file_provider_authority:I

    invoke-virtual {v1, v2}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v1

    :try_start_0
    invoke-static {p0, v1, p1}, Landroidx/core/content/FileProvider;->getUriForFile(Landroid/content/Context;Ljava/lang/String;Ljava/io/File;)Landroid/net/Uri;

    move-result-object p0
    :try_end_0
    .catch Ljava/lang/IllegalArgumentException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/NullPointerException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    sget-object p1, Lcom/zopim/android/sdk/attachment/SharedFileProvider;->LOG_TAG:Ljava/lang/String;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "FileProvider failed to retrieve file uri. There might be an issue with provider:authority="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {p1, v1, p0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    return-object v0

    :catch_1
    sget-object p0, Lcom/zopim/android/sdk/attachment/SharedFileProvider;->LOG_TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "The selected file can\'t be shared: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p0, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    return-object v0

    :cond_1
    :goto_0
    sget-object p0, Lcom/zopim/android/sdk/attachment/SharedFileProvider;->LOG_TAG:Ljava/lang/String;

    const-string p1, "Can not provide uri. File or context must not be null"

    invoke-static {p0, p1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-object v0
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/attachment/SharedFileProvider;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/attachment/SharedFileProvider;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/attachment/SharedFileProvider;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/attachment/SharedFileProvider;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/attachment/SharedFileProvider;->$VALUES:[Lcom/zopim/android/sdk/attachment/SharedFileProvider;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/attachment/SharedFileProvider;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/attachment/SharedFileProvider;

    return-object v0
.end method
