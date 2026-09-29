.class final Lcom/zopim/android/sdk/api/j;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/zopim/android/sdk/api/HttpRequest;


# static fields
.field private static final b:Ljava/lang/String; = "j"


# instance fields
.field private c:Lcom/zopim/android/sdk/api/u;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/zopim/android/sdk/api/u<",
            "Ljava/io/File;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private b(Ljava/net/URL;Ljava/io/File;)V
    .locals 9

    const/4 v0, 0x0

    :try_start_0
    invoke-virtual {p1}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v1

    check-cast v1, Ljavax/net/ssl/HttpsURLConnection;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_4
    .catchall {:try_start_0 .. :try_end_0} :catchall_3

    :try_start_1
    const-string v2, "User-Agent"

    const-string v3, "http.agent"

    invoke-static {v3}, Ljava/lang/System;->getProperty(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v2, v3}, Ljavax/net/ssl/HttpsURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "Accept-Charset"

    const-string v3, "UTF-8"

    invoke-virtual {v1, v2, v3}, Ljavax/net/ssl/HttpsURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v2, 0x0

    invoke-virtual {v1, v2}, Ljavax/net/ssl/HttpsURLConnection;->setInstanceFollowRedirects(Z)V

    sget-wide v3, Lcom/zopim/android/sdk/api/j;->a:J

    long-to-int v3, v3

    invoke-virtual {v1, v3}, Ljavax/net/ssl/HttpsURLConnection;->setReadTimeout(I)V

    invoke-virtual {v1}, Ljavax/net/ssl/HttpsURLConnection;->getResponseCode()I

    move-result v3

    sget-object v4, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string v5, "response connection.getResponseMessage()"

    invoke-static {v4, v5}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    invoke-static {v3}, Lcom/zopim/android/sdk/api/HttpRequest$Status;->getStatus(I)Lcom/zopim/android/sdk/api/HttpRequest$Status;

    move-result-object v4

    sget-object v5, Lcom/zopim/android/sdk/api/k;->a:[I

    invoke-virtual {v4}, Lcom/zopim/android/sdk/api/HttpRequest$Status;->ordinal()I

    move-result v4

    aget v4, v5, v4

    packed-switch v4, :pswitch_data_0

    goto/16 :goto_1

    :pswitch_0
    invoke-virtual {v1}, Ljavax/net/ssl/HttpsURLConnection;->getResponseMessage()Ljava/lang/String;

    move-result-object p2

    new-instance v2, Lcom/zopim/android/sdk/api/l$a;

    invoke-direct {v2}, Lcom/zopim/android/sdk/api/l$a;-><init>()V

    sget-object v4, Lcom/zopim/android/sdk/api/ErrorResponse$Kind;->HTTP:Lcom/zopim/android/sdk/api/ErrorResponse$Kind;

    invoke-virtual {v2, v4}, Lcom/zopim/android/sdk/api/l$a;->a(Lcom/zopim/android/sdk/api/ErrorResponse$Kind;)Lcom/zopim/android/sdk/api/l$a;

    move-result-object v2

    invoke-virtual {v2, v3}, Lcom/zopim/android/sdk/api/l$a;->a(I)Lcom/zopim/android/sdk/api/l$a;

    move-result-object v2

    invoke-virtual {p1}, Ljava/net/URL;->toExternalForm()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Lcom/zopim/android/sdk/api/l$a;->b(Ljava/lang/String;)Lcom/zopim/android/sdk/api/l$a;

    move-result-object v2

    invoke-virtual {v2, p2}, Lcom/zopim/android/sdk/api/l$a;->c(Ljava/lang/String;)Lcom/zopim/android/sdk/api/l$a;

    move-result-object p2

    invoke-virtual {p2}, Lcom/zopim/android/sdk/api/l$a;->a()Lcom/zopim/android/sdk/api/l;

    move-result-object p2

    iget-object v2, p0, Lcom/zopim/android/sdk/api/j;->c:Lcom/zopim/android/sdk/api/u;

    if-eqz v2, :cond_2

    iget-object v2, p0, Lcom/zopim/android/sdk/api/j;->c:Lcom/zopim/android/sdk/api/u;

    invoke-virtual {v2, p2}, Lcom/zopim/android/sdk/api/u;->a(Lcom/zopim/android/sdk/api/ErrorResponse;)V

    goto/16 :goto_1

    :pswitch_1
    const-string v3, "Content-Disposition"

    invoke-virtual {v1, v3}, Ljavax/net/ssl/HttpsURLConnection;->getHeaderField(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1}, Ljavax/net/ssl/HttpsURLConnection;->getContentType()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1}, Ljavax/net/ssl/HttpsURLConnection;->getContentLength()I

    move-result v5

    sget-object v6, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    const-string v8, "Content-Type = "

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v6, v4}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    sget-object v4, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "Content-Disposition = "

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v4, v3}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    sget-object v3, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Content-Length = "

    invoke-virtual {v4, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v1}, Ljavax/net/ssl/HttpsURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v3
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_3
    .catchall {:try_start_1 .. :try_end_1} :catchall_2

    :try_start_2
    new-instance v4, Ljava/io/BufferedOutputStream;

    new-instance v5, Ljava/io/FileOutputStream;

    invoke-direct {v5, p2}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V

    invoke-direct {v4, v5}, Ljava/io/BufferedOutputStream;-><init>(Ljava/io/OutputStream;)V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_1
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    const/16 v0, 0x1000

    :try_start_3
    new-array v0, v0, [B

    :goto_0
    invoke-virtual {v3, v0}, Ljava/io/InputStream;->read([B)I

    move-result v5

    const/4 v6, -0x1

    if-eq v5, v6, :cond_0

    invoke-virtual {v4, v0, v2, v5}, Ljava/io/BufferedOutputStream;->write([BII)V

    goto :goto_0

    :cond_0
    invoke-virtual {v4}, Ljava/io/BufferedOutputStream;->flush()V

    invoke-virtual {v4}, Ljava/io/BufferedOutputStream;->close()V

    invoke-virtual {v3}, Ljava/io/InputStream;->close()V

    sget-object v0, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "File downloaded "

    invoke-virtual {v2, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/io/File;->getPath()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v2, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v0, v2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/api/j;->c:Lcom/zopim/android/sdk/api/u;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/j;->c:Lcom/zopim/android/sdk/api/u;

    invoke-virtual {v0, p2}, Lcom/zopim/android/sdk/api/u;->a(Ljava/lang/Object;)V
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_0
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    :cond_1
    move-object v0, v3

    goto :goto_2

    :catchall_0
    move-exception p1

    goto/16 :goto_8

    :catch_0
    move-exception p2

    goto :goto_4

    :catchall_1
    move-exception p1

    move-object v4, v0

    goto/16 :goto_8

    :catch_1
    move-exception p2

    move-object v4, v0

    goto :goto_4

    :cond_2
    :goto_1
    move-object v4, v0

    :goto_2
    if-eqz v1, :cond_3

    sget-object p1, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string p2, "Disconnecting url connection"

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v1}, Ljavax/net/ssl/HttpsURLConnection;->disconnect()V

    :cond_3
    if-eqz v0, :cond_4

    :try_start_4
    sget-object p1, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string p2, "Closing input stream"

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v0}, Ljava/io/InputStream;->close()V
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_2

    goto :goto_3

    :catch_2
    move-exception p1

    sget-object p2, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string v0, "Failed to close output stream"

    invoke-static {p2, v0, p1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_4
    :goto_3
    if-eqz v4, :cond_8

    :try_start_5
    sget-object p1, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string p2, "Closing file output stream"

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v4}, Ljava/io/BufferedOutputStream;->close()V
    :try_end_5
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_6

    goto/16 :goto_7

    :catchall_2
    move-exception p1

    move-object v4, v0

    goto/16 :goto_9

    :catch_3
    move-exception p2

    move-object v3, v0

    move-object v4, v3

    :goto_4
    move-object v0, v1

    goto :goto_5

    :catchall_3
    move-exception p1

    move-object v1, v0

    move-object v4, v1

    goto/16 :goto_9

    :catch_4
    move-exception p2

    move-object v3, v0

    move-object v4, v3

    :goto_5
    :try_start_6
    sget-object v1, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Error downloading file from "

    invoke-virtual {v2, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    new-instance v1, Lcom/zopim/android/sdk/api/l$a;

    invoke-direct {v1}, Lcom/zopim/android/sdk/api/l$a;-><init>()V

    sget-object v2, Lcom/zopim/android/sdk/api/ErrorResponse$Kind;->UNEXPECTED:Lcom/zopim/android/sdk/api/ErrorResponse$Kind;

    invoke-virtual {v1, v2}, Lcom/zopim/android/sdk/api/l$a;->a(Lcom/zopim/android/sdk/api/ErrorResponse$Kind;)Lcom/zopim/android/sdk/api/l$a;

    move-result-object v1

    invoke-virtual {p2}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v1, p2}, Lcom/zopim/android/sdk/api/l$a;->a(Ljava/lang/String;)Lcom/zopim/android/sdk/api/l$a;

    move-result-object p2

    invoke-virtual {p1}, Ljava/net/URL;->toExternalForm()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/zopim/android/sdk/api/l$a;->b(Ljava/lang/String;)Lcom/zopim/android/sdk/api/l$a;

    move-result-object p1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/api/l$a;->a()Lcom/zopim/android/sdk/api/l;

    move-result-object p1

    iget-object p2, p0, Lcom/zopim/android/sdk/api/j;->c:Lcom/zopim/android/sdk/api/u;

    if-eqz p2, :cond_5

    iget-object p2, p0, Lcom/zopim/android/sdk/api/j;->c:Lcom/zopim/android/sdk/api/u;

    invoke-virtual {p2, p1}, Lcom/zopim/android/sdk/api/u;->a(Lcom/zopim/android/sdk/api/ErrorResponse;)V
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_4

    :cond_5
    if-eqz v0, :cond_6

    sget-object p1, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string p2, "Disconnecting url connection"

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v0}, Ljavax/net/ssl/HttpsURLConnection;->disconnect()V

    :cond_6
    if-eqz v3, :cond_7

    :try_start_7
    sget-object p1, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string p2, "Closing input stream"

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v3}, Ljava/io/InputStream;->close()V
    :try_end_7
    .catch Ljava/lang/Exception; {:try_start_7 .. :try_end_7} :catch_5

    goto :goto_6

    :catch_5
    move-exception p1

    sget-object p2, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string v0, "Failed to close output stream"

    invoke-static {p2, v0, p1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_7
    :goto_6
    if-eqz v4, :cond_8

    :try_start_8
    sget-object p1, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string p2, "Closing file output stream"

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v4}, Ljava/io/BufferedOutputStream;->close()V
    :try_end_8
    .catch Ljava/lang/Exception; {:try_start_8 .. :try_end_8} :catch_6

    goto :goto_7

    :catch_6
    move-exception p1

    sget-object p2, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string v0, "Failed to close file output stream"

    invoke-static {p2, v0, p1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_8
    :goto_7
    return-void

    :catchall_4
    move-exception p1

    move-object v1, v0

    :goto_8
    move-object v0, v3

    :goto_9
    if-eqz v1, :cond_9

    sget-object p2, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string v2, "Disconnecting url connection"

    invoke-static {p2, v2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v1}, Ljavax/net/ssl/HttpsURLConnection;->disconnect()V

    :cond_9
    if-eqz v0, :cond_a

    :try_start_9
    sget-object p2, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string v1, "Closing input stream"

    invoke-static {p2, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v0}, Ljava/io/InputStream;->close()V
    :try_end_9
    .catch Ljava/lang/Exception; {:try_start_9 .. :try_end_9} :catch_7

    goto :goto_a

    :catch_7
    move-exception p2

    sget-object v0, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string v1, "Failed to close output stream"

    invoke-static {v0, v1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_a
    :goto_a
    if-eqz v4, :cond_b

    :try_start_a
    sget-object p2, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string v0, "Closing file output stream"

    invoke-static {p2, v0}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v4}, Ljava/io/BufferedOutputStream;->close()V
    :try_end_a
    .catch Ljava/lang/Exception; {:try_start_a .. :try_end_a} :catch_8

    goto :goto_b

    :catch_8
    move-exception p2

    sget-object v0, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string v1, "Failed to close file output stream"

    invoke-static {v0, v1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_b
    :goto_b
    throw p1

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method


# virtual methods
.method public a(Lcom/zopim/android/sdk/api/u;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/zopim/android/sdk/api/u<",
            "Ljava/io/File;",
            ">;)V"
        }
    .end annotation

    iput-object p1, p0, Lcom/zopim/android/sdk/api/j;->c:Lcom/zopim/android/sdk/api/u;

    return-void
.end method

.method public a(Ljava/net/URL;Ljava/io/File;)V
    .locals 2
    .param p1    # Ljava/net/URL;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Ljava/io/File;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    if-eqz p2, :cond_3

    invoke-virtual {p2}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_3

    invoke-virtual {p2}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_1

    :cond_0
    if-eqz p1, :cond_2

    sget-object v0, Landroid/util/Patterns;->WEB_URL:Ljava/util/regex/Pattern;

    invoke-virtual {p1}, Ljava/net/URL;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/regex/Matcher;->matches()Z

    move-result v0

    if-nez v0, :cond_1

    goto :goto_0

    :cond_1
    sget-object v0, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string v1, "Start of download."

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-direct {p0, p1, p2}, Lcom/zopim/android/sdk/api/j;->b(Ljava/net/URL;Ljava/io/File;)V

    sget-object p1, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string p2, "End of download."

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    :cond_2
    :goto_0
    sget-object p1, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string p2, "URL validation failed. Upload aborted."

    invoke-static {p1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_3
    :goto_1
    sget-object p1, Lcom/zopim/android/sdk/api/j;->b:Ljava/lang/String;

    const-string p2, "File validation failed. Upload aborted."

    invoke-static {p1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method
