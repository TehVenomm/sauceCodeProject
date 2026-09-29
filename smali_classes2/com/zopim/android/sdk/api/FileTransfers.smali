.class public final enum Lcom/zopim/android/sdk/api/FileTransfers;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/api/FileTransfers$a;,
        Lcom/zopim/android/sdk/api/FileTransfers$b;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/api/FileTransfers;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/zopim/android/sdk/api/FileTransfers;

.field public static final enum INSTANCE:Lcom/zopim/android/sdk/api/FileTransfers;

.field private static final LOG_TAG:Ljava/lang/String;


# instance fields
.field mTransfers:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/api/FileTransfers$a;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 3

    new-instance v0, Lcom/zopim/android/sdk/api/FileTransfers;

    const-string v1, "INSTANCE"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/zopim/android/sdk/api/FileTransfers;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/api/FileTransfers;->INSTANCE:Lcom/zopim/android/sdk/api/FileTransfers;

    const/4 v0, 0x1

    new-array v0, v0, [Lcom/zopim/android/sdk/api/FileTransfers;

    sget-object v1, Lcom/zopim/android/sdk/api/FileTransfers;->INSTANCE:Lcom/zopim/android/sdk/api/FileTransfers;

    aput-object v1, v0, v2

    sput-object v0, Lcom/zopim/android/sdk/api/FileTransfers;->$VALUES:[Lcom/zopim/android/sdk/api/FileTransfers;

    const-class v0, Lcom/zopim/android/sdk/api/FileTransfers;

    invoke-virtual {v0}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/zopim/android/sdk/api/FileTransfers;->LOG_TAG:Ljava/lang/String;

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

    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1}, Ljava/util/HashMap;-><init>()V

    iput-object p1, p0, Lcom/zopim/android/sdk/api/FileTransfers;->mTransfers:Ljava/util/Map;

    return-void
.end method

.method private createUniqueName(Ljava/io/File;)Ljava/lang/String;
    .locals 5

    invoke-virtual {p1}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    invoke-virtual {p1}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v0

    goto :goto_0

    :cond_0
    const-string v0, ""

    :goto_0
    const-string v1, " "

    const-string v2, "-"

    invoke-virtual {v0, v1, v2}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x0

    const/4 v2, 0x0

    :cond_1
    :try_start_0
    iget-object v3, p0, Lcom/zopim/android/sdk/api/FileTransfers;->mTransfers:Ljava/util/Map;

    invoke-interface {v3, v0}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_2

    invoke-virtual {p1}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/zopim/android/sdk/attachment/UriToFileUtil;->getExtension(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v3, v0}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v3

    aget-object v3, v3, v1

    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "-"

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    add-int/lit8 v2, v2, 0x1

    int-to-short v2, v2

    invoke-virtual {v4, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0
    :try_end_0
    .catch Ljava/lang/IndexOutOfBoundsException; {:try_start_0 .. :try_end_0} :catch_0

    const/16 v3, 0x7fff

    if-lt v2, v3, :cond_1

    :cond_2
    return-object v0

    :catch_0
    sget-object p1, Lcom/zopim/android/sdk/api/FileTransfers;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Error generating unique file name. Will use the actual file name."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    const/4 p1, 0x0

    return-object p1
.end method

.method private findTransfer(Ljava/io/File;)Ljava/util/Map$Entry;
    .locals 4
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/io/File;",
            ")",
            "Ljava/util/Map$Entry<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/api/FileTransfers$a;",
            ">;"
        }
    .end annotation

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return-object v0

    :cond_0
    iget-object v1, p0, Lcom/zopim/android/sdk/api/FileTransfers;->mTransfers:Ljava/util/Map;

    invoke-interface {v1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_1
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map$Entry;

    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/zopim/android/sdk/api/FileTransfers$a;

    if-eqz v3, :cond_1

    iget-object v3, v3, Lcom/zopim/android/sdk/api/FileTransfers$a;->a:Ljava/io/File;

    invoke-virtual {p1, v3}, Ljava/io/File;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_1

    return-object v2

    :cond_2
    return-object v0
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/api/FileTransfers;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/api/FileTransfers;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/api/FileTransfers;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/api/FileTransfers;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/FileTransfers;->$VALUES:[Lcom/zopim/android/sdk/api/FileTransfers;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/api/FileTransfers;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/api/FileTransfers;

    return-object v0
.end method


# virtual methods
.method add(Ljava/io/File;)Ljava/lang/String;
    .locals 2
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    if-eqz p1, :cond_2

    invoke-virtual {p1}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_0

    goto :goto_0

    :cond_0
    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/FileTransfers;->findTransfer(Ljava/io/File;)Ljava/util/Map$Entry;

    move-result-object v0

    if-eqz v0, :cond_1

    invoke-interface {v0}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/api/FileTransfers$a;

    sget-object v1, Lcom/zopim/android/sdk/api/FileTransfers$b;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    iput-object v1, p1, Lcom/zopim/android/sdk/api/FileTransfers$a;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    invoke-interface {v0}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    return-object p1

    :cond_1
    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/FileTransfers;->createUniqueName(Ljava/io/File;)Ljava/lang/String;

    move-result-object v0

    new-instance v1, Lcom/zopim/android/sdk/api/FileTransfers$a;

    invoke-direct {v1}, Lcom/zopim/android/sdk/api/FileTransfers$a;-><init>()V

    iput-object p1, v1, Lcom/zopim/android/sdk/api/FileTransfers$a;->a:Ljava/io/File;

    sget-object p1, Lcom/zopim/android/sdk/api/FileTransfers$b;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    iput-object p1, v1, Lcom/zopim/android/sdk/api/FileTransfers$a;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    sget-object p1, Lcom/zopim/android/sdk/api/FileTransfers;->INSTANCE:Lcom/zopim/android/sdk/api/FileTransfers;

    iget-object p1, p1, Lcom/zopim/android/sdk/api/FileTransfers;->mTransfers:Ljava/util/Map;

    invoke-interface {p1, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-object v0

    :cond_2
    :goto_0
    sget-object p1, Lcom/zopim/android/sdk/api/FileTransfers;->LOG_TAG:Ljava/lang/String;

    const-string v0, "File validation failed. Can not add file to scheduled set."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    const-string p1, ""

    return-object p1
.end method

.method find(Ljava/io/File;)Lcom/zopim/android/sdk/api/FileTransfers$a;
    .locals 4

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return-object v0

    :cond_0
    iget-object v1, p0, Lcom/zopim/android/sdk/api/FileTransfers;->mTransfers:Ljava/util/Map;

    invoke-interface {v1}, Ljava/util/Map;->values()Ljava/util/Collection;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_1
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/zopim/android/sdk/api/FileTransfers$a;

    iget-object v3, v2, Lcom/zopim/android/sdk/api/FileTransfers$a;->a:Ljava/io/File;

    invoke-virtual {p1, v3}, Ljava/io/File;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_1

    return-object v2

    :cond_2
    return-object v0
.end method

.method public findFile(Ljava/lang/String;)Ljava/io/File;
    .locals 2

    const/4 v0, 0x0

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/api/FileTransfers;->LOG_TAG:Ljava/lang/String;

    const-string v1, "File name must not be null. Can not find file."

    invoke-static {p1, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-object v0

    :cond_0
    iget-object v1, p0, Lcom/zopim/android/sdk/api/FileTransfers;->mTransfers:Ljava/util/Map;

    invoke-interface {v1, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/api/FileTransfers$a;

    if-eqz p1, :cond_1

    iget-object v0, p1, Lcom/zopim/android/sdk/api/FileTransfers$a;->a:Ljava/io/File;

    :cond_1
    return-object v0
.end method
