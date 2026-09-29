.class public final enum Lcom/zopim/android/sdk/attachment/SdkCache;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/attachment/SdkCache;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/zopim/android/sdk/attachment/SdkCache;

.field private static final CACHE_DIR:Ljava/lang/String; = "zopim_sdk_file_cache"

.field public static final enum INSTANCE:Lcom/zopim/android/sdk/attachment/SdkCache;

.field private static final LOG_TAG:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    new-instance v0, Lcom/zopim/android/sdk/attachment/SdkCache;

    const-string v1, "INSTANCE"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/zopim/android/sdk/attachment/SdkCache;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/attachment/SdkCache;->INSTANCE:Lcom/zopim/android/sdk/attachment/SdkCache;

    const/4 v0, 0x1

    new-array v0, v0, [Lcom/zopim/android/sdk/attachment/SdkCache;

    sget-object v1, Lcom/zopim/android/sdk/attachment/SdkCache;->INSTANCE:Lcom/zopim/android/sdk/attachment/SdkCache;

    aput-object v1, v0, v2

    sput-object v0, Lcom/zopim/android/sdk/attachment/SdkCache;->$VALUES:[Lcom/zopim/android/sdk/attachment/SdkCache;

    const-class v0, Lcom/zopim/android/sdk/attachment/SdkCache;

    invoke-virtual {v0}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/zopim/android/sdk/attachment/SdkCache;->LOG_TAG:Ljava/lang/String;

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

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/attachment/SdkCache;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/attachment/SdkCache;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/attachment/SdkCache;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/attachment/SdkCache;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/attachment/SdkCache;->$VALUES:[Lcom/zopim/android/sdk/attachment/SdkCache;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/attachment/SdkCache;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/attachment/SdkCache;

    return-object v0
.end method


# virtual methods
.method public deleteCache(Landroid/content/Context;)V
    .locals 3

    sget-object v0, Lcom/zopim/android/sdk/attachment/SdkCache;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Clearing cached files"

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    if-nez p1, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/attachment/SdkCache;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Context must not be null. File cache will not be deleted."

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :cond_0
    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/attachment/SdkCache;->getSdkCacheDir(Landroid/content/Context;)Ljava/io/File;

    move-result-object p1

    invoke-virtual {p1}, Ljava/io/File;->listFiles()[Ljava/io/File;

    move-result-object p1

    if-eqz p1, :cond_1

    array-length v0, p1

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_1

    aget-object v2, p1, v1

    invoke-virtual {v2}, Ljava/io/File;->delete()Z

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_1
    return-void
.end method

.method public getSdkCacheDir(Landroid/content/Context;)Ljava/io/File;
    .locals 2

    invoke-virtual {p1}, Landroid/content/Context;->getCacheDir()Ljava/io/File;

    move-result-object p1

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p1}, Ljava/io/File;->getPath()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v1, Ljava/io/File;->separator:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "zopim_sdk_file_cache"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    new-instance v1, Ljava/io/File;

    invoke-direct {v1, v0}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1}, Ljava/io/File;->exists()Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    invoke-virtual {v1}, Ljava/io/File;->mkdir()Z

    move-result v0

    :goto_0
    if-nez v0, :cond_1

    return-object p1

    :cond_1
    return-object v1
.end method
