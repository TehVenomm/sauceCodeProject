.class Lcom/zopim/android/sdk/api/c;
.super Lcom/zopim/android/sdk/data/observers/ChatLogObserver;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/api/ChatService;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/api/ChatService;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/c;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/observers/ChatLogObserver;-><init>()V

    return-void
.end method


# virtual methods
.method public update(Ljava/util/LinkedHashMap;)V
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/LinkedHashMap<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/ChatLog;",
            ">;)V"
        }
    .end annotation

    invoke-virtual {p1}, Ljava/util/LinkedHashMap;->values()Ljava/util/Collection;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_9

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/zopim/android/sdk/model/ChatLog;

    sget-object v1, Lcom/zopim/android/sdk/api/h;->b:[I

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getType()Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v2

    invoke-virtual {v2}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result v2

    aget v1, v1, v2

    const/4 v2, 0x0

    const/4 v3, 0x1

    packed-switch v1, :pswitch_data_0

    goto :goto_0

    :pswitch_0
    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object v1

    if-eqz v1, :cond_3

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object v1

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/Attachment;->getUrl()Ljava/net/URL;

    move-result-object v1

    if-eqz v1, :cond_3

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object v1

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/Attachment;->getName()Ljava/lang/String;

    move-result-object v1

    if-nez v1, :cond_0

    goto/16 :goto_1

    :cond_0
    sget-object v1, Lcom/zopim/android/sdk/api/FileTransfers;->INSTANCE:Lcom/zopim/android/sdk/api/FileTransfers;

    iget-object v1, v1, Lcom/zopim/android/sdk/api/FileTransfers;->mTransfers:Ljava/util/Map;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object v4

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/Attachment;->getName()Ljava/lang/String;

    move-result-object v4

    invoke-interface {v1, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/zopim/android/sdk/api/FileTransfers$a;

    if-nez v1, :cond_1

    sget-object v1, Lcom/zopim/android/sdk/attachment/SdkCache;->INSTANCE:Lcom/zopim/android/sdk/attachment/SdkCache;

    iget-object v4, p0, Lcom/zopim/android/sdk/api/c;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-virtual {v4}, Lcom/zopim/android/sdk/api/ChatService;->getApplicationContext()Landroid/content/Context;

    move-result-object v4

    invoke-virtual {v1, v4}, Lcom/zopim/android/sdk/attachment/SdkCache;->getSdkCacheDir(Landroid/content/Context;)Ljava/io/File;

    move-result-object v1

    new-instance v4, Ljava/io/File;

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v1}, Ljava/io/File;->getPath()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v5, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v1, Ljava/io/File;->separator:Ljava/lang/String;

    invoke-virtual {v5, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object v1

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/Attachment;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v5, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v4, v1}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    new-instance v1, Lcom/zopim/android/sdk/api/FileTransfers$a;

    invoke-direct {v1}, Lcom/zopim/android/sdk/api/FileTransfers$a;-><init>()V

    sget-object v5, Lcom/zopim/android/sdk/api/FileTransfers$b;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    iput-object v5, v1, Lcom/zopim/android/sdk/api/FileTransfers$a;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    iput-object v4, v1, Lcom/zopim/android/sdk/api/FileTransfers$a;->a:Ljava/io/File;

    sget-object v5, Lcom/zopim/android/sdk/api/FileTransfers;->INSTANCE:Lcom/zopim/android/sdk/api/FileTransfers;

    iget-object v5, v5, Lcom/zopim/android/sdk/api/FileTransfers;->mTransfers:Ljava/util/Map;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object v6

    invoke-virtual {v6}, Lcom/zopim/android/sdk/model/Attachment;->getName()Ljava/lang/String;

    move-result-object v6

    invoke-interface {v5, v6, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    invoke-virtual {v0, v4}, Lcom/zopim/android/sdk/model/ChatLog;->setFile(Ljava/io/File;)V

    :cond_1
    sget-object v4, Lcom/zopim/android/sdk/api/h;->a:[I

    iget-object v5, v1, Lcom/zopim/android/sdk/api/FileTransfers$a;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    invoke-virtual {v5}, Lcom/zopim/android/sdk/api/FileTransfers$b;->ordinal()I

    move-result v5

    aget v4, v4, v5

    if-eq v4, v3, :cond_2

    goto/16 :goto_0

    :cond_2
    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object v4

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/Attachment;->getUrl()Ljava/net/URL;

    move-result-object v4

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getFile()Ljava/io/File;

    move-result-object v5

    invoke-static {}, Lcom/zopim/android/sdk/api/ChatService;->access$200()Ljava/lang/String;

    move-result-object v6

    const-string v7, "Starting file download task"

    invoke-static {v6, v7}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    new-instance v6, Lcom/zopim/android/sdk/api/n;

    invoke-direct {v6}, Lcom/zopim/android/sdk/api/n;-><init>()V

    new-instance v7, Lcom/zopim/android/sdk/api/e;

    invoke-direct {v7, p0, v0}, Lcom/zopim/android/sdk/api/e;-><init>(Lcom/zopim/android/sdk/api/c;Lcom/zopim/android/sdk/model/ChatLog;)V

    invoke-virtual {v6, v7}, Lcom/zopim/android/sdk/api/n;->a(Lcom/zopim/android/sdk/api/u;)V

    new-array v3, v3, [Landroid/util/Pair;

    new-instance v7, Landroid/util/Pair;

    invoke-direct {v7, v4, v5}, Landroid/util/Pair;-><init>(Ljava/lang/Object;Ljava/lang/Object;)V

    aput-object v7, v3, v2

    invoke-virtual {v6, v3}, Lcom/zopim/android/sdk/api/n;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    sget-object v3, Lcom/zopim/android/sdk/api/FileTransfers$b;->c:Lcom/zopim/android/sdk/api/FileTransfers$b;

    iput-object v3, v1, Lcom/zopim/android/sdk/api/FileTransfers$a;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    invoke-virtual {v0, v2}, Lcom/zopim/android/sdk/model/ChatLog;->setFailed(Z)V

    goto/16 :goto_0

    :cond_3
    :goto_1
    invoke-static {}, Lcom/zopim/android/sdk/api/ChatService;->access$200()Ljava/lang/String;

    move-result-object v0

    const-string v1, "Attachment url is not available. Skipping download."

    goto/16 :goto_3

    :pswitch_1
    if-eqz v0, :cond_8

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getUploadUrl()Ljava/net/URL;

    move-result-object v1

    if-nez v1, :cond_4

    goto/16 :goto_2

    :cond_4
    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getFile()Ljava/io/File;

    move-result-object v1

    if-nez v1, :cond_5

    invoke-static {}, Lcom/zopim/android/sdk/api/ChatService;->access$200()Ljava/lang/String;

    move-result-object v0

    const-string v1, "Upload file is not available. Skipping upload."

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    goto/16 :goto_0

    :cond_5
    sget-object v1, Lcom/zopim/android/sdk/api/FileTransfers;->INSTANCE:Lcom/zopim/android/sdk/api/FileTransfers;

    iget-object v1, v1, Lcom/zopim/android/sdk/api/FileTransfers;->mTransfers:Ljava/util/Map;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getFileName()Ljava/lang/String;

    move-result-object v4

    invoke-interface {v1, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/zopim/android/sdk/api/FileTransfers$a;

    if-nez v1, :cond_6

    invoke-static {}, Lcom/zopim/android/sdk/api/ChatService;->access$200()Ljava/lang/String;

    move-result-object v0

    const-string v1, "Unexpected, upload info should have been added prior to this. Skipping upload"

    goto :goto_3

    :cond_6
    sget-object v4, Lcom/zopim/android/sdk/api/h;->a:[I

    iget-object v5, v1, Lcom/zopim/android/sdk/api/FileTransfers$a;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    invoke-virtual {v5}, Lcom/zopim/android/sdk/api/FileTransfers$b;->ordinal()I

    move-result v5

    aget v4, v4, v5

    if-eq v4, v3, :cond_7

    invoke-static {}, Lcom/zopim/android/sdk/api/ChatService;->access$200()Ljava/lang/String;

    move-result-object v0

    const-string v1, "Skipping start of already started upload."

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    goto/16 :goto_0

    :cond_7
    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getUploadUrl()Ljava/net/URL;

    move-result-object v4

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/ChatLog;->getFile()Ljava/io/File;

    move-result-object v5

    invoke-static {}, Lcom/zopim/android/sdk/api/ChatService;->access$200()Ljava/lang/String;

    move-result-object v6

    const-string v7, "Starting file upload task"

    invoke-static {v6, v7}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    new-instance v6, Lcom/zopim/android/sdk/api/p;

    invoke-direct {v6}, Lcom/zopim/android/sdk/api/p;-><init>()V

    new-instance v7, Lcom/zopim/android/sdk/api/d;

    invoke-direct {v7, p0, v0}, Lcom/zopim/android/sdk/api/d;-><init>(Lcom/zopim/android/sdk/api/c;Lcom/zopim/android/sdk/model/ChatLog;)V

    invoke-virtual {v6, v7}, Lcom/zopim/android/sdk/api/p;->a(Lcom/zopim/android/sdk/api/u;)V

    new-array v7, v3, [Landroid/util/Pair;

    new-instance v8, Landroid/util/Pair;

    invoke-direct {v8, v5, v4}, Landroid/util/Pair;-><init>(Ljava/lang/Object;Ljava/lang/Object;)V

    aput-object v8, v7, v2

    invoke-virtual {v6, v7}, Lcom/zopim/android/sdk/api/p;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    sget-object v4, Lcom/zopim/android/sdk/api/FileTransfers$b;->c:Lcom/zopim/android/sdk/api/FileTransfers$b;

    iput-object v4, v1, Lcom/zopim/android/sdk/api/FileTransfers$a;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    invoke-virtual {v0, v2}, Lcom/zopim/android/sdk/model/ChatLog;->setFailed(Z)V

    invoke-virtual {v0, v3}, Lcom/zopim/android/sdk/model/ChatLog;->setProgress(I)V

    goto/16 :goto_0

    :cond_8
    :goto_2
    invoke-static {}, Lcom/zopim/android/sdk/api/ChatService;->access$200()Ljava/lang/String;

    move-result-object v0

    const-string v1, "Upload url is not available. Skipping upload."

    :goto_3
    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->w(Ljava/lang/String;Ljava/lang/String;)V

    goto/16 :goto_0

    :cond_9
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
