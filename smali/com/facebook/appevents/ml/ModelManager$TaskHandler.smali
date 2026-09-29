.class Lcom/facebook/appevents/ml/ModelManager$TaskHandler;
.super Ljava/lang/Object;
.source "ModelManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/facebook/appevents/ml/ModelManager;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "TaskHandler"
.end annotation


# instance fields
.field assetUri:Ljava/lang/String;

.field model:Lcom/facebook/appevents/ml/Model;
    .annotation build Landroidx/annotation/Nullable;
    .end annotation
.end field

.field private onPostExecute:Ljava/lang/Runnable;

.field ruleFile:Ljava/io/File;

.field ruleUri:Ljava/lang/String;
    .annotation build Landroidx/annotation/Nullable;
    .end annotation
.end field

.field thresholds:[F
    .annotation build Landroidx/annotation/Nullable;
    .end annotation
.end field

.field useCase:Ljava/lang/String;

.field versionId:I


# direct methods
.method constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I[F)V
    .locals 0
    .param p3    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p5    # [F
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 369
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 370
    iput-object p1, p0, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->useCase:Ljava/lang/String;

    .line 371
    iput-object p2, p0, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->assetUri:Ljava/lang/String;

    .line 372
    iput-object p3, p0, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->ruleUri:Ljava/lang/String;

    .line 373
    iput p4, p0, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->versionId:I

    .line 374
    iput-object p5, p0, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->thresholds:[F

    return-void
.end method

.method static synthetic access$500(Lcom/facebook/appevents/ml/ModelManager$TaskHandler;)Ljava/lang/Runnable;
    .locals 0

    .line 358
    iget-object p0, p0, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->onPostExecute:Ljava/lang/Runnable;

    return-object p0
.end method

.method static synthetic access$600(Ljava/lang/String;Ljava/lang/String;Lcom/facebook/appevents/internal/FileDownloadTask$Callback;)V
    .locals 0

    .line 358
    invoke-static {p0, p1, p2}, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->download(Ljava/lang/String;Ljava/lang/String;Lcom/facebook/appevents/internal/FileDownloadTask$Callback;)V

    return-void
.end method

.method static build(Lorg/json/JSONObject;)Lcom/facebook/appevents/ml/ModelManager$TaskHandler;
    .locals 8
    .param p0    # Lorg/json/JSONObject;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    const/4 v0, 0x0

    if-nez p0, :cond_0

    return-object v0

    :cond_0
    :try_start_0
    const-string v1, "use_case"

    .line 388
    invoke-virtual {p0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v1, "asset_uri"

    .line 389
    invoke-virtual {p0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    const-string v1, "rules_uri"

    .line 390
    invoke-virtual {p0, v1, v0}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v1, "version_id"

    .line 391
    invoke-virtual {p0, v1}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v6

    const-string v1, "thresholds"

    .line 392
    invoke-virtual {p0, v1}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object p0

    invoke-static {p0}, Lcom/facebook/appevents/ml/ModelManager;->access$400(Lorg/json/JSONArray;)[F

    move-result-object v7

    .line 393
    new-instance p0, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;

    move-object v2, p0

    invoke-direct/range {v2 .. v7}, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I[F)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    return-object v0
.end method

.method private static deleteOldFiles(Ljava/lang/String;I)V
    .locals 6

    .line 431
    invoke-static {}, Lcom/facebook/appevents/ml/Utils;->getMlDir()Ljava/io/File;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 435
    :cond_0
    invoke-virtual {v0}, Ljava/io/File;->listFiles()[Ljava/io/File;

    move-result-object v0

    if-eqz v0, :cond_4

    .line 436
    array-length v1, v0

    if-nez v1, :cond_1

    goto :goto_1

    .line 439
    :cond_1
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "_"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 440
    array-length v1, v0

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_3

    aget-object v3, v0, v2

    .line 441
    invoke-virtual {v3}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v4

    .line 442
    invoke-virtual {v4, p0}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v5

    if-eqz v5, :cond_2

    invoke-virtual {v4, p1}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_2

    .line 443
    invoke-virtual {v3}, Ljava/io/File;->delete()Z

    :cond_2
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_3
    return-void

    :cond_4
    :goto_1
    return-void
.end method

.method private static download(Ljava/lang/String;Ljava/lang/String;Lcom/facebook/appevents/internal/FileDownloadTask$Callback;)V
    .locals 2

    .line 449
    new-instance v0, Ljava/io/File;

    invoke-static {}, Lcom/facebook/appevents/ml/Utils;->getMlDir()Ljava/io/File;

    move-result-object v1

    invoke-direct {v0, v1, p1}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    if-eqz p0, :cond_1

    .line 450
    invoke-virtual {v0}, Ljava/io/File;->exists()Z

    move-result p1

    if-eqz p1, :cond_0

    goto :goto_0

    .line 454
    :cond_0
    new-instance p1, Lcom/facebook/appevents/internal/FileDownloadTask;

    invoke-direct {p1, p0, v0, p2}, Lcom/facebook/appevents/internal/FileDownloadTask;-><init>(Ljava/lang/String;Ljava/io/File;Lcom/facebook/appevents/internal/FileDownloadTask$Callback;)V

    const/4 p0, 0x0

    new-array p0, p0, [Ljava/lang/String;

    invoke-virtual {p1, p0}, Lcom/facebook/appevents/internal/FileDownloadTask;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    return-void

    .line 451
    :cond_1
    :goto_0
    invoke-interface {p2, v0}, Lcom/facebook/appevents/internal/FileDownloadTask$Callback;->onComplete(Ljava/io/File;)V

    return-void
.end method

.method static execute(Lcom/facebook/appevents/ml/ModelManager$TaskHandler;)V
    .locals 1

    .line 400
    invoke-static {p0}, Ljava/util/Collections;->singletonList(Ljava/lang/Object;)Ljava/util/List;

    move-result-object v0

    invoke-static {p0, v0}, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->execute(Lcom/facebook/appevents/ml/ModelManager$TaskHandler;Ljava/util/List;)V

    return-void
.end method

.method static execute(Lcom/facebook/appevents/ml/ModelManager$TaskHandler;Ljava/util/List;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/facebook/appevents/ml/ModelManager$TaskHandler;",
            "Ljava/util/List<",
            "Lcom/facebook/appevents/ml/ModelManager$TaskHandler;",
            ">;)V"
        }
    .end annotation

    .line 404
    iget-object v0, p0, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->useCase:Ljava/lang/String;

    iget v1, p0, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->versionId:I

    invoke-static {v0, v1}, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->deleteOldFiles(Ljava/lang/String;I)V

    .line 406
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v1, p0, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->useCase:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "_"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v1, p0, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->versionId:I

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 407
    iget-object p0, p0, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->assetUri:Ljava/lang/String;

    new-instance v1, Lcom/facebook/appevents/ml/ModelManager$TaskHandler$1;

    invoke-direct {v1, p1}, Lcom/facebook/appevents/ml/ModelManager$TaskHandler$1;-><init>(Ljava/util/List;)V

    invoke-static {p0, v0, v1}, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->download(Ljava/lang/String;Ljava/lang/String;Lcom/facebook/appevents/internal/FileDownloadTask$Callback;)V

    return-void
.end method


# virtual methods
.method setOnPostExecute(Ljava/lang/Runnable;)Lcom/facebook/appevents/ml/ModelManager$TaskHandler;
    .locals 0

    .line 378
    iput-object p1, p0, Lcom/facebook/appevents/ml/ModelManager$TaskHandler;->onPostExecute:Ljava/lang/Runnable;

    return-object p0
.end method
