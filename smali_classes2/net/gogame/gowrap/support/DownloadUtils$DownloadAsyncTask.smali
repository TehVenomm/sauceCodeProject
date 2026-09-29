.class public Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;
.super Landroid/os/AsyncTask;
.source "DownloadUtils.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/support/DownloadUtils;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "DownloadAsyncTask"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroid/os/AsyncTask<",
        "Ljava/lang/Void;",
        "Ljava/lang/Void;",
        "Ljava/lang/Void;",
        ">;"
    }
.end annotation


# static fields
.field private static final CONNECT_TIMEOUT:I = 0x2710

.field private static final ETAG_HEADER_NAME:Ljava/lang/String; = "Etag"

.field private static final READ_TIMEOUT:I = 0x2710


# instance fields
.field private final callback:Lnet/gogame/gowrap/support/DownloadUtils$Callback;

.field private final checkEtag:Z

.field private final context:Landroid/content/Context;

.field private final target:Lnet/gogame/gowrap/support/DownloadUtils$Target;

.field private final url:Ljava/net/URL;


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/net/URL;Lnet/gogame/gowrap/support/DownloadUtils$Target;ZLnet/gogame/gowrap/support/DownloadUtils$Callback;)V
    .locals 0

    .line 60
    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    .line 62
    iput-object p1, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->context:Landroid/content/Context;

    .line 63
    iput-object p2, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->url:Ljava/net/URL;

    .line 64
    iput-object p3, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->target:Lnet/gogame/gowrap/support/DownloadUtils$Target;

    .line 65
    iput-boolean p4, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->checkEtag:Z

    .line 66
    iput-object p5, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->callback:Lnet/gogame/gowrap/support/DownloadUtils$Callback;

    return-void
.end method


# virtual methods
.method protected bridge synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0

    .line 47
    check-cast p1, [Ljava/lang/Void;

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->doInBackground([Ljava/lang/Void;)Ljava/lang/Void;

    move-result-object p1

    return-object p1
.end method

.method protected varargs doInBackground([Ljava/lang/Void;)Ljava/lang/Void;
    .locals 12

    const/4 p1, 0x0

    .line 72
    :try_start_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Etag/"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->url:Ljava/net/URL;

    invoke-virtual {v1}, Ljava/net/URL;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 73
    iget-boolean v1, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->checkEtag:Z

    const/4 v2, 0x2

    const/4 v3, 0x3

    const/16 v4, 0xc8

    const/16 v5, 0x2710

    const/4 v6, 0x1

    const/4 v7, 0x0

    if-eqz v1, :cond_8

    const-string v1, "goWrap"

    const-string v8, "Checking if we need to download %s"

    .line 74
    new-array v9, v6, [Ljava/lang/Object;

    iget-object v10, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->url:Ljava/net/URL;

    aput-object v10, v9, v7

    invoke-static {v8, v9}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v8

    invoke-static {v1, v8}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 76
    iget-object v1, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->context:Landroid/content/Context;

    invoke-static {v1, v0}, Lnet/gogame/gowrap/support/PreferenceUtils;->getPreference(Landroid/content/Context;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_3

    if-eqz v1, :cond_8

    .line 81
    :try_start_1
    iget-object v8, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->url:Ljava/net/URL;

    invoke-virtual {v8}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v8

    check-cast v8, Ljava/net/HttpURLConnection;
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    :try_start_2
    const-string v9, "HEAD"

    .line 82
    invoke-virtual {v8, v9}, Ljava/net/HttpURLConnection;->setRequestMethod(Ljava/lang/String;)V

    .line 83
    invoke-virtual {v8, v5}, Ljava/net/HttpURLConnection;->setConnectTimeout(I)V

    .line 84
    invoke-virtual {v8, v5}, Ljava/net/HttpURLConnection;->setReadTimeout(I)V

    .line 85
    invoke-static {v8}, Lnet/gogame/gowrap/support/HttpUtils;->drainQuietly(Ljava/net/HttpURLConnection;)V

    .line 86
    invoke-virtual {v8}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v9

    if-eq v9, v4, :cond_1

    const-string v0, "goWrap"

    const-string v1, "%s: %d %s"

    .line 87
    new-array v3, v3, [Ljava/lang/Object;

    iget-object v4, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->url:Ljava/net/URL;

    aput-object v4, v3, v7

    .line 88
    invoke-virtual {v8}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v4

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    aput-object v4, v3, v6

    .line 89
    invoke-virtual {v8}, Ljava/net/HttpURLConnection;->getResponseMessage()Ljava/lang/String;

    move-result-object v4

    aput-object v4, v3, v2

    .line 87
    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    if-eqz v8, :cond_0

    .line 117
    :try_start_3
    invoke-virtual {v8}, Ljava/net/HttpURLConnection;->disconnect()V
    :try_end_3
    .catch Ljava/lang/Exception; {:try_start_3 .. :try_end_3} :catch_3

    :cond_0
    return-object p1

    .line 93
    :cond_1
    :try_start_4
    invoke-virtual {v8}, Ljava/net/HttpURLConnection;->getHeaderFields()Ljava/util/Map;

    move-result-object v9

    if-eqz v9, :cond_2

    const-string v10, "Etag"

    .line 96
    invoke-interface {v9, v10}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v10

    if-eqz v10, :cond_2

    const-string v10, "Etag"

    .line 97
    invoke-interface {v9, v10}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v10

    check-cast v10, Ljava/util/List;

    invoke-interface {v10}, Ljava/util/List;->isEmpty()Z

    move-result v10

    if-nez v10, :cond_2

    const-string v10, "Etag"

    .line 98
    invoke-interface {v9, v10}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Ljava/util/List;

    invoke-interface {v9, v7}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Ljava/lang/String;

    goto :goto_0

    :cond_2
    move-object v9, p1

    :goto_0
    if-nez v9, :cond_3

    const-string v1, "goWrap"

    const-string v9, "Etag not found for %s"

    .line 102
    new-array v10, v6, [Ljava/lang/Object;

    iget-object v11, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->url:Ljava/net/URL;

    aput-object v11, v10, v7

    invoke-static {v9, v10}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v9

    invoke-static {v1, v9}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_2

    .line 103
    :cond_3
    invoke-virtual {v1, v9}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_6

    const-string v0, "goWrap"

    const-string v1, "Local file is the same as the remote file: %s"

    .line 104
    new-array v2, v6, [Ljava/lang/Object;

    iget-object v3, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->url:Ljava/net/URL;

    aput-object v3, v2, v7

    invoke-static {v1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 106
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->callback:Lnet/gogame/gowrap/support/DownloadUtils$Callback;
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    if-eqz v0, :cond_4

    .line 108
    :try_start_5
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->callback:Lnet/gogame/gowrap/support/DownloadUtils$Callback;

    invoke-interface {v0}, Lnet/gogame/gowrap/support/DownloadUtils$Callback;->onDownloadSucceeded()V
    :try_end_5
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_0
    .catchall {:try_start_5 .. :try_end_5} :catchall_0

    goto :goto_1

    :catch_0
    move-exception v0

    :try_start_6
    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 110
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_0

    :cond_4
    :goto_1
    if-eqz v8, :cond_5

    .line 117
    :try_start_7
    invoke-virtual {v8}, Ljava/net/HttpURLConnection;->disconnect()V

    :cond_5
    return-object p1

    :cond_6
    :goto_2
    if-eqz v8, :cond_8

    invoke-virtual {v8}, Ljava/net/HttpURLConnection;->disconnect()V

    goto :goto_4

    :catchall_0
    move-exception v0

    goto :goto_3

    :catchall_1
    move-exception v0

    move-object v8, p1

    :goto_3
    if-eqz v8, :cond_7

    invoke-virtual {v8}, Ljava/net/HttpURLConnection;->disconnect()V

    .line 119
    :cond_7
    throw v0

    :cond_8
    :goto_4
    const-string v1, "goWrap"

    const-string v8, "Downloading %s"

    .line 123
    new-array v9, v6, [Ljava/lang/Object;

    iget-object v10, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->url:Ljava/net/URL;

    aput-object v10, v9, v7

    invoke-static {v8, v9}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v8

    invoke-static {v1, v8}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_7
    .catch Ljava/lang/Exception; {:try_start_7 .. :try_end_7} :catch_3

    .line 127
    :try_start_8
    iget-object v1, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->url:Ljava/net/URL;

    invoke-virtual {v1}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v1

    check-cast v1, Ljava/net/HttpURLConnection;
    :try_end_8
    .catchall {:try_start_8 .. :try_end_8} :catchall_5

    :try_start_9
    const-string v8, "GET"

    .line 128
    invoke-virtual {v1, v8}, Ljava/net/HttpURLConnection;->setRequestMethod(Ljava/lang/String;)V

    .line 129
    invoke-virtual {v1, v5}, Ljava/net/HttpURLConnection;->setConnectTimeout(I)V

    .line 130
    invoke-virtual {v1, v5}, Ljava/net/HttpURLConnection;->setReadTimeout(I)V

    .line 131
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v5

    if-eq v5, v4, :cond_b

    const-string v0, "goWrap"

    const-string v4, "%s: %d %s"

    .line 132
    new-array v3, v3, [Ljava/lang/Object;

    iget-object v5, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->url:Ljava/net/URL;

    aput-object v5, v3, v7

    .line 133
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getResponseCode()I

    move-result v5

    invoke-static {v5}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v5

    aput-object v5, v3, v6

    .line 134
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getResponseMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v3, v2

    .line 132
    invoke-static {v4, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v0, v2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 135
    invoke-static {v1}, Lnet/gogame/gowrap/support/HttpUtils;->drainQuietly(Ljava/net/HttpURLConnection;)V

    .line 136
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->callback:Lnet/gogame/gowrap/support/DownloadUtils$Callback;
    :try_end_9
    .catchall {:try_start_9 .. :try_end_9} :catchall_4

    if-eqz v0, :cond_9

    .line 138
    :try_start_a
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->callback:Lnet/gogame/gowrap/support/DownloadUtils$Callback;

    invoke-interface {v0}, Lnet/gogame/gowrap/support/DownloadUtils$Callback;->onDownloadFailed()V
    :try_end_a
    .catch Ljava/lang/Exception; {:try_start_a .. :try_end_a} :catch_1
    .catchall {:try_start_a .. :try_end_a} :catchall_4

    goto :goto_5

    :catch_1
    move-exception v0

    :try_start_b
    const-string v2, "goWrap"

    const-string v3, "Exception"

    .line 140
    invoke-static {v2, v3, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I
    :try_end_b
    .catchall {:try_start_b .. :try_end_b} :catchall_4

    :cond_9
    :goto_5
    if-eqz v1, :cond_a

    .line 182
    :try_start_c
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V
    :try_end_c
    .catch Ljava/lang/Exception; {:try_start_c .. :try_end_c} :catch_3

    :cond_a
    return-object p1

    .line 146
    :cond_b
    :try_start_d
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getHeaderFields()Ljava/util/Map;

    move-result-object v2

    if-eqz v2, :cond_c

    const-string v3, "Etag"

    .line 149
    invoke-interface {v2, v3}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    if-eqz v3, :cond_c

    const-string v3, "Etag"

    .line 150
    invoke-interface {v2, v3}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/util/List;

    invoke-interface {v3}, Ljava/util/List;->isEmpty()Z

    move-result v3

    if-nez v3, :cond_c

    const-string v3, "Etag"

    .line 151
    invoke-interface {v2, v3}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/List;

    invoke-interface {v2, v7}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    goto :goto_6

    :cond_c
    move-object v2, p1

    :goto_6
    if-eqz v2, :cond_d

    .line 155
    iget-object v3, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->target:Lnet/gogame/gowrap/support/DownloadUtils$Target;

    invoke-interface {v3, v2}, Lnet/gogame/gowrap/support/DownloadUtils$Target;->setEtag(Ljava/lang/String;)V

    .line 157
    :cond_d
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v3
    :try_end_d
    .catchall {:try_start_d .. :try_end_d} :catchall_4

    .line 159
    :try_start_e
    iget-object v4, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->target:Lnet/gogame/gowrap/support/DownloadUtils$Target;

    invoke-interface {v4}, Lnet/gogame/gowrap/support/DownloadUtils$Target;->getOutputStream()Ljava/io/OutputStream;

    move-result-object v4
    :try_end_e
    .catchall {:try_start_e .. :try_end_e} :catchall_3

    .line 161
    :try_start_f
    invoke-static {v3, v4}, Lnet/gogame/gowrap/io/utils/IOUtils;->copy(Ljava/io/InputStream;Ljava/io/OutputStream;)J
    :try_end_f
    .catchall {:try_start_f .. :try_end_f} :catchall_2

    .line 163
    :try_start_10
    invoke-static {v4}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V

    .line 165
    iget-object v4, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->target:Lnet/gogame/gowrap/support/DownloadUtils$Target;

    invoke-interface {v4}, Lnet/gogame/gowrap/support/DownloadUtils$Target;->close()V

    const-string v4, "goWrap"

    const-string v5, "Downloaded %s"

    .line 166
    new-array v6, v6, [Ljava/lang/Object;

    iget-object v8, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->url:Ljava/net/URL;

    aput-object v8, v6, v7

    invoke-static {v5, v6}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v5

    invoke-static {v4, v5}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 167
    iget-boolean v4, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->checkEtag:Z

    if-eqz v4, :cond_e

    .line 168
    iget-object v4, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->context:Landroid/content/Context;

    invoke-static {v4, v0, v2}, Lnet/gogame/gowrap/support/PreferenceUtils;->setPreference(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    .line 170
    :cond_e
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->callback:Lnet/gogame/gowrap/support/DownloadUtils$Callback;
    :try_end_10
    .catchall {:try_start_10 .. :try_end_10} :catchall_3

    if-eqz v0, :cond_f

    .line 172
    :try_start_11
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->callback:Lnet/gogame/gowrap/support/DownloadUtils$Callback;

    invoke-interface {v0}, Lnet/gogame/gowrap/support/DownloadUtils$Callback;->onDownloadSucceeded()V
    :try_end_11
    .catch Ljava/lang/Exception; {:try_start_11 .. :try_end_11} :catch_2
    .catchall {:try_start_11 .. :try_end_11} :catchall_3

    goto :goto_7

    :catch_2
    move-exception v0

    :try_start_12
    const-string v2, "goWrap"

    const-string v4, "Exception"

    .line 174
    invoke-static {v2, v4, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I
    :try_end_12
    .catchall {:try_start_12 .. :try_end_12} :catchall_3

    .line 178
    :cond_f
    :goto_7
    :try_start_13
    invoke-static {v3}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V
    :try_end_13
    .catchall {:try_start_13 .. :try_end_13} :catchall_4

    if-eqz v1, :cond_11

    .line 182
    :try_start_14
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V
    :try_end_14
    .catch Ljava/lang/Exception; {:try_start_14 .. :try_end_14} :catch_3

    goto :goto_9

    :catchall_2
    move-exception v0

    .line 163
    :try_start_15
    invoke-static {v4}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V

    .line 164
    throw v0
    :try_end_15
    .catchall {:try_start_15 .. :try_end_15} :catchall_3

    :catchall_3
    move-exception v0

    .line 178
    :try_start_16
    invoke-static {v3}, Lnet/gogame/gowrap/io/utils/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    .line 179
    throw v0
    :try_end_16
    .catchall {:try_start_16 .. :try_end_16} :catchall_4

    :catchall_4
    move-exception v0

    goto :goto_8

    :catchall_5
    move-exception v0

    move-object v1, p1

    :goto_8
    if-eqz v1, :cond_10

    .line 182
    :try_start_17
    invoke-virtual {v1}, Ljava/net/HttpURLConnection;->disconnect()V

    .line 184
    :cond_10
    throw v0
    :try_end_17
    .catch Ljava/lang/Exception; {:try_start_17 .. :try_end_17} :catch_3

    :catch_3
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 186
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 187
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->callback:Lnet/gogame/gowrap/support/DownloadUtils$Callback;

    if-eqz v0, :cond_11

    .line 189
    :try_start_18
    iget-object v0, p0, Lnet/gogame/gowrap/support/DownloadUtils$DownloadAsyncTask;->callback:Lnet/gogame/gowrap/support/DownloadUtils$Callback;

    invoke-interface {v0}, Lnet/gogame/gowrap/support/DownloadUtils$Callback;->onDownloadFailed()V
    :try_end_18
    .catch Ljava/lang/Exception; {:try_start_18 .. :try_end_18} :catch_4

    goto :goto_9

    :catch_4
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 191
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_11
    :goto_9
    return-object p1
.end method
