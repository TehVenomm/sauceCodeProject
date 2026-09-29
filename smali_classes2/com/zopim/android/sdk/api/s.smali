.class final Lcom/zopim/android/sdk/api/s;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/zopim/android/sdk/api/HttpRequest;


# static fields
.field private static final b:Ljava/lang/String; = "s"

.field private static final d:Ljava/lang/String;


# instance fields
.field private c:Ljava/lang/String;

.field private e:Lcom/zopim/android/sdk/api/u;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/zopim/android/sdk/api/u<",
            "Ljava/lang/Void;",
            ">;"
        }
    .end annotation
.end field

.field private f:Lcom/zopim/android/sdk/api/HttpRequest$ProgressListener;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    invoke-static {v0, v1}, Ljava/lang/Long;->toHexString(J)Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/zopim/android/sdk/api/s;->d:Ljava/lang/String;

    return-void
.end method

.method constructor <init>()V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "POST"

    iput-object v0, p0, Lcom/zopim/android/sdk/api/s;->c:Ljava/lang/String;

    return-void
.end method

.method private a(I)V
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/s;->f:Lcom/zopim/android/sdk/api/HttpRequest$ProgressListener;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/s;->f:Lcom/zopim/android/sdk/api/HttpRequest$ProgressListener;

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/HttpRequest$ProgressListener;->onProgressUpdate(I)V

    :cond_0
    return-void
.end method

.method private b(Ljava/io/File;Ljava/net/URL;)V
    .locals 16
    .param p1    # Ljava/io/File;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
    .param p2    # Ljava/net/URL;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    move-object/from16 v1, p0

    const/4 v2, 0x0

    :try_start_0
    invoke-virtual/range {p2 .. p2}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v0

    move-object v3, v0

    check-cast v3, Ljavax/net/ssl/HttpsURLConnection;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_6
    .catchall {:try_start_0 .. :try_end_0} :catchall_4

    :try_start_1
    iget-object v0, v1, Lcom/zopim/android/sdk/api/s;->c:Ljava/lang/String;

    invoke-virtual {v3, v0}, Ljavax/net/ssl/HttpsURLConnection;->setRequestMethod(Ljava/lang/String;)V

    const/4 v0, 0x1

    invoke-virtual {v3, v0}, Ljavax/net/ssl/HttpsURLConnection;->setDoOutput(Z)V

    const-string v4, "User-Agent"

    const-string v5, "http.agent"

    invoke-static {v5}, Ljava/lang/System;->getProperty(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v3, v4, v5}, Ljavax/net/ssl/HttpsURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    const-string v4, "Accept-Charset"

    const-string v5, "UTF-8"

    invoke-virtual {v3, v4, v5}, Ljavax/net/ssl/HttpsURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    const-string v4, "Content-Type"

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "multipart/form-data; boundary="

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v6, Lcom/zopim/android/sdk/api/s;->d:Ljava/lang/String;

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v3, v4, v5}, Ljavax/net/ssl/HttpsURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v4, 0x0

    invoke-virtual {v3, v4}, Ljavax/net/ssl/HttpsURLConnection;->setInstanceFollowRedirects(Z)V

    sget-wide v5, Lcom/zopim/android/sdk/api/s;->a:J

    long-to-int v5, v5

    invoke-virtual {v3, v5}, Ljavax/net/ssl/HttpsURLConnection;->setReadTimeout(I)V

    invoke-virtual {v3}, Ljavax/net/ssl/HttpsURLConnection;->getOutputStream()Ljava/io/OutputStream;

    move-result-object v5
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_5
    .catchall {:try_start_1 .. :try_end_1} :catchall_3

    :try_start_2
    new-instance v6, Ljava/io/PrintWriter;

    new-instance v7, Ljava/io/OutputStreamWriter;

    const-string v8, "UTF-8"

    invoke-direct {v7, v5, v8}, Ljava/io/OutputStreamWriter;-><init>(Ljava/io/OutputStream;Ljava/lang/String;)V

    invoke-direct {v6, v7, v0}, Ljava/io/PrintWriter;-><init>(Ljava/io/Writer;Z)V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_4
    .catchall {:try_start_2 .. :try_end_2} :catchall_2

    :try_start_3
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    const-string v8, "--"

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v8, Lcom/zopim/android/sdk/api/s;->d:Ljava/lang/String;

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/io/PrintWriter;->append(Ljava/lang/CharSequence;)Ljava/io/PrintWriter;

    move-result-object v7

    const-string v8, "\r\n"

    invoke-virtual {v7, v8}, Ljava/io/PrintWriter;->append(Ljava/lang/CharSequence;)Ljava/io/PrintWriter;

    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    const-string v8, "Content-Disposition: form-data; name=\"binaryFile\"; filename=\""

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual/range {p1 .. p1}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v8, "\""

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/io/PrintWriter;->append(Ljava/lang/CharSequence;)Ljava/io/PrintWriter;

    move-result-object v7

    const-string v8, "\r\n"

    invoke-virtual {v7, v8}, Ljava/io/PrintWriter;->append(Ljava/lang/CharSequence;)Ljava/io/PrintWriter;

    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    const-string v8, "Content-Type: "

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual/range {p1 .. p1}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v8

    invoke-static {v8}, Ljava/net/URLConnection;->guessContentTypeFromName(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/io/PrintWriter;->append(Ljava/lang/CharSequence;)Ljava/io/PrintWriter;

    move-result-object v7

    const-string v8, "\r\n"

    invoke-virtual {v7, v8}, Ljava/io/PrintWriter;->append(Ljava/lang/CharSequence;)Ljava/io/PrintWriter;

    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    const-string v8, "Content-Length: "

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual/range {p1 .. p1}, Ljava/io/File;->length()J

    move-result-wide v8

    invoke-virtual {v7, v8, v9}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/io/PrintWriter;->append(Ljava/lang/CharSequence;)Ljava/io/PrintWriter;

    const-string v7, "Content-Transfer-Encoding: binary"

    invoke-virtual {v6, v7}, Ljava/io/PrintWriter;->append(Ljava/lang/CharSequence;)Ljava/io/PrintWriter;

    move-result-object v7

    const-string v8, "\r\n"

    invoke-virtual {v7, v8}, Ljava/io/PrintWriter;->append(Ljava/lang/CharSequence;)Ljava/io/PrintWriter;

    const-string v7, "\r\n"

    invoke-virtual {v6, v7}, Ljava/io/PrintWriter;->append(Ljava/lang/CharSequence;)Ljava/io/PrintWriter;

    move-result-object v7

    invoke-virtual {v7}, Ljava/io/PrintWriter;->flush()V

    const/16 v7, 0x63

    invoke-virtual/range {p1 .. p1}, Ljava/io/File;->length()J

    move-result-wide v8

    invoke-direct {v1, v0}, Lcom/zopim/android/sdk/api/s;->a(I)V

    new-instance v10, Ljava/io/BufferedInputStream;

    new-instance v0, Ljava/io/FileInputStream;

    move-object/from16 v11, p1

    invoke-direct {v0, v11}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V

    invoke-direct {v10, v0}, Ljava/io/BufferedInputStream;-><init>(Ljava/io/InputStream;)V
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    :try_start_4
    invoke-virtual {v10}, Ljava/io/InputStream;->available()I

    move-result v0

    const/16 v11, 0x1000

    invoke-static {v0, v11}, Ljava/lang/Math;->min(II)I

    move-result v0

    new-array v12, v0, [B

    invoke-virtual {v10, v12, v4, v0}, Ljava/io/InputStream;->read([BII)I

    move-result v0

    sget-object v13, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v14, "Reading bytes from fis"

    invoke-static {v13, v14}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    move v13, v0

    :goto_0
    if-lez v0, :cond_0

    invoke-virtual {v5, v12, v4, v0}, Ljava/io/OutputStream;->write([BII)V

    mul-int v0, v7, v13

    int-to-long v14, v0

    div-long/2addr v14, v8

    long-to-float v0, v14

    invoke-static {v0}, Ljava/lang/Math;->round(F)I

    move-result v0

    invoke-direct {v1, v0}, Lcom/zopim/android/sdk/api/s;->a(I)V

    invoke-virtual {v10}, Ljava/io/InputStream;->available()I

    move-result v0

    invoke-static {v0, v11}, Ljava/lang/Math;->min(II)I

    move-result v0

    invoke-virtual {v10, v12, v4, v0}, Ljava/io/InputStream;->read([BII)I

    move-result v0

    add-int/2addr v13, v0

    goto :goto_0

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v4, "Finished write to output stream. Closing file input stream"

    invoke-static {v0, v4}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v10}, Ljava/io/InputStream;->close()V

    invoke-virtual {v5}, Ljava/io/OutputStream;->flush()V

    const-string v0, "\r\n"

    invoke-virtual {v6, v0}, Ljava/io/PrintWriter;->append(Ljava/lang/CharSequence;)Ljava/io/PrintWriter;

    move-result-object v0

    invoke-virtual {v0}, Ljava/io/PrintWriter;->flush()V

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "--"

    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v4, Lcom/zopim/android/sdk/api/s;->d:Ljava/lang/String;

    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, "--"

    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v6, v0}, Ljava/io/PrintWriter;->append(Ljava/lang/CharSequence;)Ljava/io/PrintWriter;

    move-result-object v0

    const-string v4, "\r\n"

    invoke-virtual {v0, v4}, Ljava/io/PrintWriter;->append(Ljava/lang/CharSequence;)Ljava/io/PrintWriter;

    move-result-object v0

    invoke-virtual {v0}, Ljava/io/PrintWriter;->flush()V

    invoke-virtual {v6}, Ljava/io/PrintWriter;->close()V

    invoke-virtual {v5}, Ljava/io/OutputStream;->close()V

    invoke-virtual {v3}, Ljavax/net/ssl/HttpsURLConnection;->getResponseCode()I

    move-result v0

    invoke-static {v0}, Lcom/zopim/android/sdk/api/HttpRequest$Status;->getStatus(I)Lcom/zopim/android/sdk/api/HttpRequest$Status;

    move-result-object v4

    sget-object v7, Lcom/zopim/android/sdk/api/t;->a:[I

    invoke-virtual {v4}, Lcom/zopim/android/sdk/api/HttpRequest$Status;->ordinal()I

    move-result v4

    aget v4, v7, v4

    packed-switch v4, :pswitch_data_0

    goto :goto_1

    :pswitch_0
    invoke-virtual {v3}, Ljavax/net/ssl/HttpsURLConnection;->getResponseMessage()Ljava/lang/String;

    move-result-object v2

    new-instance v4, Lcom/zopim/android/sdk/api/l$a;

    invoke-direct {v4}, Lcom/zopim/android/sdk/api/l$a;-><init>()V

    sget-object v7, Lcom/zopim/android/sdk/api/ErrorResponse$Kind;->HTTP:Lcom/zopim/android/sdk/api/ErrorResponse$Kind;

    invoke-virtual {v4, v7}, Lcom/zopim/android/sdk/api/l$a;->a(Lcom/zopim/android/sdk/api/ErrorResponse$Kind;)Lcom/zopim/android/sdk/api/l$a;

    move-result-object v4

    invoke-virtual {v4, v0}, Lcom/zopim/android/sdk/api/l$a;->a(I)Lcom/zopim/android/sdk/api/l$a;

    move-result-object v0

    invoke-virtual/range {p2 .. p2}, Ljava/net/URL;->toExternalForm()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v0, v4}, Lcom/zopim/android/sdk/api/l$a;->b(Ljava/lang/String;)Lcom/zopim/android/sdk/api/l$a;

    move-result-object v0

    invoke-virtual {v0, v2}, Lcom/zopim/android/sdk/api/l$a;->c(Ljava/lang/String;)Lcom/zopim/android/sdk/api/l$a;

    move-result-object v0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/api/l$a;->a()Lcom/zopim/android/sdk/api/l;

    move-result-object v0

    iget-object v2, v1, Lcom/zopim/android/sdk/api/s;->e:Lcom/zopim/android/sdk/api/u;

    if-eqz v2, :cond_1

    iget-object v2, v1, Lcom/zopim/android/sdk/api/s;->e:Lcom/zopim/android/sdk/api/u;

    invoke-virtual {v2, v0}, Lcom/zopim/android/sdk/api/u;->b(Lcom/zopim/android/sdk/api/ErrorResponse;)V

    goto :goto_1

    :pswitch_1
    sget-object v4, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    const-string v8, "Request completed. Status "

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v4, v0}, Lcom/zopim/android/sdk/api/Logger;->i(Ljava/lang/String;Ljava/lang/String;)V

    iget-object v0, v1, Lcom/zopim/android/sdk/api/s;->e:Lcom/zopim/android/sdk/api/u;

    if-eqz v0, :cond_1

    iget-object v0, v1, Lcom/zopim/android/sdk/api/s;->e:Lcom/zopim/android/sdk/api/u;

    invoke-virtual {v0, v2}, Lcom/zopim/android/sdk/api/u;->b(Ljava/lang/Object;)V
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_2
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    :cond_1
    :goto_1
    if-eqz v3, :cond_2

    sget-object v0, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v2, "Disconnecting url connection"

    invoke-static {v0, v2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v3}, Ljavax/net/ssl/HttpsURLConnection;->disconnect()V

    :cond_2
    :try_start_5
    sget-object v0, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v2, "Closing print writer"

    invoke-static {v0, v2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v6}, Ljava/io/PrintWriter;->close()V
    :try_end_5
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_0

    goto :goto_2

    :catch_0
    move-exception v0

    sget-object v2, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v3, "Failed to close writer"

    invoke-static {v2, v3, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_2
    if-eqz v5, :cond_3

    :try_start_6
    sget-object v0, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v2, "Closing output stream"

    invoke-static {v0, v2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v5}, Ljava/io/OutputStream;->close()V
    :try_end_6
    .catch Ljava/lang/Exception; {:try_start_6 .. :try_end_6} :catch_1

    goto :goto_3

    :catch_1
    move-exception v0

    sget-object v2, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v3, "Failed to close output stream"

    invoke-static {v2, v3, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_3
    :goto_3
    :try_start_7
    sget-object v0, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v2, "Closing file input stream"

    invoke-static {v0, v2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v10}, Ljava/io/InputStream;->close()V
    :try_end_7
    .catch Ljava/lang/Exception; {:try_start_7 .. :try_end_7} :catch_9

    goto/16 :goto_c

    :catchall_0
    move-exception v0

    goto :goto_8

    :catch_2
    move-exception v0

    goto :goto_5

    :catchall_1
    move-exception v0

    move-object v10, v2

    goto :goto_8

    :catch_3
    move-exception v0

    move-object v10, v2

    goto :goto_5

    :catchall_2
    move-exception v0

    move-object v6, v2

    goto :goto_7

    :catch_4
    move-exception v0

    move-object v6, v2

    goto :goto_4

    :catchall_3
    move-exception v0

    move-object v5, v2

    goto :goto_6

    :catch_5
    move-exception v0

    move-object v5, v2

    move-object v6, v5

    :goto_4
    move-object v10, v6

    :goto_5
    move-object v2, v3

    goto :goto_9

    :catchall_4
    move-exception v0

    move-object v3, v2

    move-object v5, v3

    :goto_6
    move-object v6, v5

    :goto_7
    move-object v10, v6

    :goto_8
    move-object v2, v0

    goto/16 :goto_d

    :catch_6
    move-exception v0

    move-object v5, v2

    move-object v6, v5

    move-object v10, v6

    :goto_9
    :try_start_8
    sget-object v3, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "Error uploading file to "

    invoke-virtual {v4, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-object/from16 v7, p2

    invoke-virtual {v4, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    new-instance v3, Lcom/zopim/android/sdk/api/l$a;

    invoke-direct {v3}, Lcom/zopim/android/sdk/api/l$a;-><init>()V

    sget-object v4, Lcom/zopim/android/sdk/api/ErrorResponse$Kind;->UNEXPECTED:Lcom/zopim/android/sdk/api/ErrorResponse$Kind;

    invoke-virtual {v3, v4}, Lcom/zopim/android/sdk/api/l$a;->a(Lcom/zopim/android/sdk/api/ErrorResponse$Kind;)Lcom/zopim/android/sdk/api/l$a;

    move-result-object v3

    invoke-virtual {v0}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v3, v0}, Lcom/zopim/android/sdk/api/l$a;->a(Ljava/lang/String;)Lcom/zopim/android/sdk/api/l$a;

    move-result-object v0

    invoke-virtual/range {p2 .. p2}, Ljava/net/URL;->toExternalForm()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v3}, Lcom/zopim/android/sdk/api/l$a;->b(Ljava/lang/String;)Lcom/zopim/android/sdk/api/l$a;

    move-result-object v0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/api/l$a;->a()Lcom/zopim/android/sdk/api/l;

    move-result-object v0

    iget-object v3, v1, Lcom/zopim/android/sdk/api/s;->e:Lcom/zopim/android/sdk/api/u;

    if-eqz v3, :cond_4

    iget-object v3, v1, Lcom/zopim/android/sdk/api/s;->e:Lcom/zopim/android/sdk/api/u;

    invoke-virtual {v3, v0}, Lcom/zopim/android/sdk/api/u;->b(Lcom/zopim/android/sdk/api/ErrorResponse;)V
    :try_end_8
    .catchall {:try_start_8 .. :try_end_8} :catchall_5

    :cond_4
    if-eqz v2, :cond_5

    sget-object v0, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v3, "Disconnecting url connection"

    invoke-static {v0, v3}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v2}, Ljavax/net/ssl/HttpsURLConnection;->disconnect()V

    :cond_5
    if-eqz v6, :cond_6

    :try_start_9
    sget-object v0, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v2, "Closing print writer"

    invoke-static {v0, v2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v6}, Ljava/io/PrintWriter;->close()V
    :try_end_9
    .catch Ljava/lang/Exception; {:try_start_9 .. :try_end_9} :catch_7

    goto :goto_a

    :catch_7
    move-exception v0

    sget-object v2, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v3, "Failed to close writer"

    invoke-static {v2, v3, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_6
    :goto_a
    if-eqz v5, :cond_7

    :try_start_a
    sget-object v0, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v2, "Closing output stream"

    invoke-static {v0, v2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v5}, Ljava/io/OutputStream;->close()V
    :try_end_a
    .catch Ljava/lang/Exception; {:try_start_a .. :try_end_a} :catch_8

    goto :goto_b

    :catch_8
    move-exception v0

    sget-object v2, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v3, "Failed to close output stream"

    invoke-static {v2, v3, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_7
    :goto_b
    if-eqz v10, :cond_8

    :try_start_b
    sget-object v0, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v2, "Closing file input stream"

    invoke-static {v0, v2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v10}, Ljava/io/InputStream;->close()V
    :try_end_b
    .catch Ljava/lang/Exception; {:try_start_b .. :try_end_b} :catch_9

    goto :goto_c

    :catch_9
    move-exception v0

    sget-object v2, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v3, "Failed to close file input stream"

    invoke-static {v2, v3, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_8
    :goto_c
    return-void

    :catchall_5
    move-exception v0

    move-object v3, v2

    goto/16 :goto_8

    :goto_d
    if-eqz v3, :cond_9

    sget-object v0, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v4, "Disconnecting url connection"

    invoke-static {v0, v4}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v3}, Ljavax/net/ssl/HttpsURLConnection;->disconnect()V

    :cond_9
    if-eqz v6, :cond_a

    :try_start_c
    sget-object v0, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v3, "Closing print writer"

    invoke-static {v0, v3}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v6}, Ljava/io/PrintWriter;->close()V
    :try_end_c
    .catch Ljava/lang/Exception; {:try_start_c .. :try_end_c} :catch_a

    goto :goto_e

    :catch_a
    move-exception v0

    sget-object v3, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v4, "Failed to close writer"

    invoke-static {v3, v4, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_a
    :goto_e
    if-eqz v5, :cond_b

    :try_start_d
    sget-object v0, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v3, "Closing output stream"

    invoke-static {v0, v3}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v5}, Ljava/io/OutputStream;->close()V
    :try_end_d
    .catch Ljava/lang/Exception; {:try_start_d .. :try_end_d} :catch_b

    goto :goto_f

    :catch_b
    move-exception v0

    sget-object v3, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v4, "Failed to close output stream"

    invoke-static {v3, v4, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_b
    :goto_f
    if-eqz v10, :cond_c

    :try_start_e
    sget-object v0, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v3, "Closing file input stream"

    invoke-static {v0, v3}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v10}, Ljava/io/InputStream;->close()V
    :try_end_e
    .catch Ljava/lang/Exception; {:try_start_e .. :try_end_e} :catch_c

    goto :goto_10

    :catch_c
    move-exception v0

    sget-object v3, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v4, "Failed to close file input stream"

    invoke-static {v3, v4, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_c
    :goto_10
    throw v2

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method


# virtual methods
.method public a(Lcom/zopim/android/sdk/api/HttpRequest$ProgressListener;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/s;->f:Lcom/zopim/android/sdk/api/HttpRequest$ProgressListener;

    return-void
.end method

.method public a(Lcom/zopim/android/sdk/api/u;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/zopim/android/sdk/api/u<",
            "Ljava/lang/Void;",
            ">;)V"
        }
    .end annotation

    iput-object p1, p0, Lcom/zopim/android/sdk/api/s;->e:Lcom/zopim/android/sdk/api/u;

    return-void
.end method

.method public a(Ljava/io/File;Ljava/net/URL;)V
    .locals 2

    if-eqz p1, :cond_3

    invoke-virtual {p1}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_3

    invoke-virtual {p1}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_3

    invoke-virtual {p1}, Ljava/io/File;->exists()Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_1

    :cond_0
    if-eqz p2, :cond_2

    sget-object v0, Landroid/util/Patterns;->WEB_URL:Ljava/util/regex/Pattern;

    invoke-virtual {p2}, Ljava/net/URL;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/regex/Matcher;->matches()Z

    move-result v0

    if-nez v0, :cond_1

    goto :goto_0

    :cond_1
    sget-object v0, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string v1, "Start of upload."

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-direct {p0, p1, p2}, Lcom/zopim/android/sdk/api/s;->b(Ljava/io/File;Ljava/net/URL;)V

    sget-object p1, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string p2, "End of upload."

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    :cond_2
    :goto_0
    sget-object p1, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string p2, "URL validation failed. Upload aborted."

    invoke-static {p1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_3
    :goto_1
    sget-object p1, Lcom/zopim/android/sdk/api/s;->b:Ljava/lang/String;

    const-string p2, "File validation failed. Upload aborted."

    invoke-static {p1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method
