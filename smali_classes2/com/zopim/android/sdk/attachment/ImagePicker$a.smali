.class Lcom/zopim/android/sdk/attachment/ImagePicker$a;
.super Landroid/os/AsyncTask;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/attachment/ImagePicker;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = "a"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroid/os/AsyncTask<",
        "Landroid/util/Pair<",
        "Landroid/content/Context;",
        "Ljava/util/List<",
        "Landroid/net/Uri;",
        ">;>;",
        "Ljava/lang/Void;",
        "Ljava/util/List<",
        "Lcom/zopim/android/sdk/attachment/ImagePicker$b;",
        ">;>;"
    }
.end annotation


# instance fields
.field final a:Lcom/zopim/android/sdk/attachment/ImagePicker$Callback;

.field final synthetic b:Lcom/zopim/android/sdk/attachment/ImagePicker;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/attachment/ImagePicker;Lcom/zopim/android/sdk/attachment/ImagePicker$Callback;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/attachment/ImagePicker$a;->b:Lcom/zopim/android/sdk/attachment/ImagePicker;

    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    iput-object p2, p0, Lcom/zopim/android/sdk/attachment/ImagePicker$a;->a:Lcom/zopim/android/sdk/attachment/ImagePicker$Callback;

    return-void
.end method

.method private a(Landroid/content/Context;Ljava/util/List;)Ljava/util/List;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/List<",
            "Landroid/net/Uri;",
            ">;)",
            "Ljava/util/List<",
            "Lcom/zopim/android/sdk/attachment/ImagePicker$b;",
            ">;"
        }
    .end annotation

    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    invoke-interface {p2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/net/Uri;

    invoke-static {p1, v1}, Lcom/zopim/android/sdk/attachment/UriToFileUtil;->getFile(Landroid/content/Context;Landroid/net/Uri;)Ljava/io/File;

    move-result-object v2

    new-instance v3, Lcom/zopim/android/sdk/attachment/ImagePicker$b;

    iget-object v4, p0, Lcom/zopim/android/sdk/attachment/ImagePicker$a;->b:Lcom/zopim/android/sdk/attachment/ImagePicker;

    invoke-direct {v3, v4, v1, v2}, Lcom/zopim/android/sdk/attachment/ImagePicker$b;-><init>(Lcom/zopim/android/sdk/attachment/ImagePicker;Landroid/net/Uri;Ljava/io/File;)V

    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_0
    return-object v0
.end method


# virtual methods
.method protected varargs a([Landroid/util/Pair;)Ljava/util/List;
    .locals 14
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "([",
            "Landroid/util/Pair<",
            "Landroid/content/Context;",
            "Ljava/util/List<",
            "Landroid/net/Uri;",
            ">;>;)",
            "Ljava/util/List<",
            "Lcom/zopim/android/sdk/attachment/ImagePicker$b;",
            ">;"
        }
    .end annotation

    const/4 v0, 0x0

    aget-object v1, p1, v0

    iget-object v1, v1, Landroid/util/Pair;->first:Ljava/lang/Object;

    check-cast v1, Landroid/content/Context;

    aget-object p1, p1, v0

    iget-object p1, p1, Landroid/util/Pair;->second:Ljava/lang/Object;

    check-cast p1, Ljava/util/List;

    invoke-direct {p0, v1, p1}, Lcom/zopim/android/sdk/attachment/ImagePicker$a;->a(Landroid/content/Context;Ljava/util/List;)Ljava/util/List;

    move-result-object p1

    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3}, Ljava/util/ArrayList;-><init>()V

    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/zopim/android/sdk/attachment/ImagePicker$b;

    invoke-virtual {v4}, Lcom/zopim/android/sdk/attachment/ImagePicker$b;->a()Z

    move-result v5

    if-eqz v5, :cond_0

    invoke-interface {v2, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_0
    invoke-interface {v3, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    invoke-interface {v3}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_2
    :goto_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_9

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/zopim/android/sdk/attachment/ImagePicker$b;

    const/4 v4, 0x0

    const/4 v5, 0x1

    :try_start_0
    sget-object v6, Lcom/zopim/android/sdk/attachment/SdkCache;->INSTANCE:Lcom/zopim/android/sdk/attachment/SdkCache;

    invoke-virtual {v6, v1}, Lcom/zopim/android/sdk/attachment/SdkCache;->getSdkCacheDir(Landroid/content/Context;)Ljava/io/File;

    move-result-object v6

    new-instance v7, Ljava/io/File;

    new-instance v8, Ljava/lang/StringBuilder;

    invoke-direct {v8}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v8, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    sget-object v6, Ljava/io/File;->separator:Ljava/lang/String;

    invoke-virtual {v8, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v6, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v9, "attachment-%s.jpg"

    new-array v10, v5, [Ljava/lang/Object;

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v11

    invoke-static {v11, v12}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v11

    aput-object v11, v10, v0

    invoke-static {v6, v9, v10}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v8, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v8}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-direct {v7, v6}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v6

    invoke-virtual {v3}, Lcom/zopim/android/sdk/attachment/ImagePicker$b;->b()Landroid/net/Uri;

    move-result-object v8

    invoke-virtual {v6, v8}, Landroid/content/ContentResolver;->openInputStream(Landroid/net/Uri;)Ljava/io/InputStream;

    move-result-object v6
    :try_end_0
    .catch Ljava/io/FileNotFoundException; {:try_start_0 .. :try_end_0} :catch_7
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_5
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    :try_start_1
    new-instance v8, Ljava/io/FileOutputStream;

    invoke-direct {v8, v7}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V
    :try_end_1
    .catch Ljava/io/FileNotFoundException; {:try_start_1 .. :try_end_1} :catch_4
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_3
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    const/16 v4, 0x400

    :try_start_2
    new-array v4, v4, [B

    :goto_2
    invoke-virtual {v6, v4}, Ljava/io/InputStream;->read([B)I

    move-result v9

    if-lez v9, :cond_3

    invoke-virtual {v8, v4, v0, v9}, Ljava/io/FileOutputStream;->write([BII)V

    goto :goto_2

    :cond_3
    new-instance v4, Lcom/zopim/android/sdk/attachment/ImagePicker$b;

    iget-object v9, p0, Lcom/zopim/android/sdk/attachment/ImagePicker$a;->b:Lcom/zopim/android/sdk/attachment/ImagePicker;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/attachment/ImagePicker$b;->b()Landroid/net/Uri;

    move-result-object v10

    invoke-direct {v4, v9, v10, v7}, Lcom/zopim/android/sdk/attachment/ImagePicker$b;-><init>(Lcom/zopim/android/sdk/attachment/ImagePicker;Landroid/net/Uri;Ljava/io/File;)V

    invoke-interface {v2, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    invoke-virtual {v7}, Ljava/io/File;->deleteOnExit()V
    :try_end_2
    .catch Ljava/io/FileNotFoundException; {:try_start_2 .. :try_end_2} :catch_2
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_1
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    if-eqz v6, :cond_4

    :try_start_3
    invoke-virtual {v6}, Ljava/io/InputStream;->close()V
    :try_end_3
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_0

    goto :goto_3

    :catch_0
    move-exception v3

    invoke-static {}, Lcom/zopim/android/sdk/attachment/ImagePicker;->access$000()Ljava/lang/String;

    move-result-object v4

    const-string v5, "Failed to close file input stream."

    invoke-static {v4, v5, v3}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_4
    :goto_3
    :try_start_4
    invoke-virtual {v8}, Ljava/io/FileOutputStream;->close()V
    :try_end_4
    .catch Ljava/io/IOException; {:try_start_4 .. :try_end_4} :catch_9

    goto/16 :goto_1

    :catchall_0
    move-exception p1

    goto :goto_4

    :catch_1
    move-exception v4

    move-object v13, v6

    move-object v6, v4

    move-object v4, v13

    goto :goto_5

    :catch_2
    move-exception v4

    move-object v13, v6

    move-object v6, v4

    move-object v4, v13

    goto :goto_7

    :catchall_1
    move-exception p1

    move-object v8, v4

    :goto_4
    move-object v4, v6

    goto/16 :goto_9

    :catch_3
    move-exception v7

    move-object v8, v4

    move-object v4, v6

    move-object v6, v7

    goto :goto_5

    :catch_4
    move-exception v7

    move-object v8, v4

    move-object v4, v6

    move-object v6, v7

    goto :goto_7

    :catchall_2
    move-exception p1

    move-object v8, v4

    goto :goto_9

    :catch_5
    move-exception v6

    move-object v8, v4

    :goto_5
    :try_start_5
    invoke-static {}, Lcom/zopim/android/sdk/attachment/ImagePicker;->access$000()Ljava/lang/String;

    move-result-object v7

    sget-object v9, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v10, "IO Error copying file, uri: %s"

    new-array v5, v5, [Ljava/lang/Object;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/attachment/ImagePicker$b;->b()Landroid/net/Uri;

    move-result-object v3

    aput-object v3, v5, v0

    invoke-static {v9, v10, v5}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    invoke-static {v7, v3, v6}, Lcom/zopim/android/sdk/api/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_3

    if-eqz v4, :cond_5

    :try_start_6
    invoke-virtual {v4}, Ljava/io/InputStream;->close()V
    :try_end_6
    .catch Ljava/io/IOException; {:try_start_6 .. :try_end_6} :catch_6

    goto :goto_6

    :catch_6
    move-exception v3

    invoke-static {}, Lcom/zopim/android/sdk/attachment/ImagePicker;->access$000()Ljava/lang/String;

    move-result-object v4

    const-string v5, "Failed to close file input stream."

    invoke-static {v4, v5, v3}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_5
    :goto_6
    if-eqz v8, :cond_2

    :try_start_7
    invoke-virtual {v8}, Ljava/io/FileOutputStream;->close()V
    :try_end_7
    .catch Ljava/io/IOException; {:try_start_7 .. :try_end_7} :catch_9

    goto/16 :goto_1

    :catch_7
    move-exception v6

    move-object v8, v4

    :goto_7
    :try_start_8
    invoke-static {}, Lcom/zopim/android/sdk/attachment/ImagePicker;->access$000()Ljava/lang/String;

    move-result-object v7

    sget-object v9, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v10, "File not found error copying file, uri: %s"

    new-array v5, v5, [Ljava/lang/Object;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/attachment/ImagePicker$b;->b()Landroid/net/Uri;

    move-result-object v3

    aput-object v3, v5, v0

    invoke-static {v9, v10, v5}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    invoke-static {v7, v3, v6}, Lcom/zopim/android/sdk/api/Logger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_8
    .catchall {:try_start_8 .. :try_end_8} :catchall_3

    if-eqz v4, :cond_6

    :try_start_9
    invoke-virtual {v4}, Ljava/io/InputStream;->close()V
    :try_end_9
    .catch Ljava/io/IOException; {:try_start_9 .. :try_end_9} :catch_8

    goto :goto_8

    :catch_8
    move-exception v3

    invoke-static {}, Lcom/zopim/android/sdk/attachment/ImagePicker;->access$000()Ljava/lang/String;

    move-result-object v4

    const-string v5, "Failed to close file input stream."

    invoke-static {v4, v5, v3}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_6
    :goto_8
    if-eqz v8, :cond_2

    :try_start_a
    invoke-virtual {v8}, Ljava/io/FileOutputStream;->close()V
    :try_end_a
    .catch Ljava/io/IOException; {:try_start_a .. :try_end_a} :catch_9

    goto/16 :goto_1

    :catch_9
    move-exception v3

    invoke-static {}, Lcom/zopim/android/sdk/attachment/ImagePicker;->access$000()Ljava/lang/String;

    move-result-object v4

    const-string v5, "Failed to close file output stream."

    invoke-static {v4, v5, v3}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    goto/16 :goto_1

    :catchall_3
    move-exception p1

    :goto_9
    if-eqz v4, :cond_7

    :try_start_b
    invoke-virtual {v4}, Ljava/io/InputStream;->close()V
    :try_end_b
    .catch Ljava/io/IOException; {:try_start_b .. :try_end_b} :catch_a

    goto :goto_a

    :catch_a
    move-exception v0

    invoke-static {}, Lcom/zopim/android/sdk/attachment/ImagePicker;->access$000()Ljava/lang/String;

    move-result-object v1

    const-string v2, "Failed to close file input stream."

    invoke-static {v1, v2, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_7
    :goto_a
    if-eqz v8, :cond_8

    :try_start_c
    invoke-virtual {v8}, Ljava/io/FileOutputStream;->close()V
    :try_end_c
    .catch Ljava/io/IOException; {:try_start_c .. :try_end_c} :catch_b

    goto :goto_b

    :catch_b
    move-exception v0

    invoke-static {}, Lcom/zopim/android/sdk/attachment/ImagePicker;->access$000()Ljava/lang/String;

    move-result-object v1

    const-string v2, "Failed to close file output stream."

    invoke-static {v1, v2, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_8
    :goto_b
    throw p1

    :cond_9
    return-object v2
.end method

.method protected a(Ljava/util/List;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/zopim/android/sdk/attachment/ImagePicker$b;",
            ">;)V"
        }
    .end annotation

    invoke-super {p0, p1}, Landroid/os/AsyncTask;->onPostExecute(Ljava/lang/Object;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/attachment/ImagePicker$a;->a:Lcom/zopim/android/sdk/attachment/ImagePicker$Callback;

    if-eqz v0, :cond_2

    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/zopim/android/sdk/attachment/ImagePicker$b;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/attachment/ImagePicker$b;->a()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-virtual {v1}, Lcom/zopim/android/sdk/attachment/ImagePicker$b;->c()Ljava/io/File;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    iget-object p1, p0, Lcom/zopim/android/sdk/attachment/ImagePicker$a;->a:Lcom/zopim/android/sdk/attachment/ImagePicker$Callback;

    invoke-interface {p1, v0}, Lcom/zopim/android/sdk/attachment/ImagePicker$Callback;->onSuccess(Ljava/util/List;)V

    :cond_2
    return-void
.end method

.method protected synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0

    check-cast p1, [Landroid/util/Pair;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/attachment/ImagePicker$a;->a([Landroid/util/Pair;)Ljava/util/List;

    move-result-object p1

    return-object p1
.end method

.method protected synthetic onPostExecute(Ljava/lang/Object;)V
    .locals 0

    check-cast p1, Ljava/util/List;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/attachment/ImagePicker$a;->a(Ljava/util/List;)V

    return-void
.end method
