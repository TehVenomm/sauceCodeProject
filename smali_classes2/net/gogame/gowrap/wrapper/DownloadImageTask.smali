.class public Lnet/gogame/gowrap/wrapper/DownloadImageTask;
.super Landroid/os/AsyncTask;
.source "DownloadImageTask.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroid/os/AsyncTask<",
        "Ljava/lang/String;",
        "Ljava/lang/Void;",
        "Landroid/graphics/Bitmap;",
        ">;"
    }
.end annotation


# instance fields
.field private imageView:Landroid/widget/ImageView;


# direct methods
.method public constructor <init>(Landroid/widget/ImageView;)V
    .locals 0

    .line 18
    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    .line 20
    iput-object p1, p0, Lnet/gogame/gowrap/wrapper/DownloadImageTask;->imageView:Landroid/widget/ImageView;

    return-void
.end method


# virtual methods
.method protected varargs doInBackground([Ljava/lang/String;)Landroid/graphics/Bitmap;
    .locals 5

    const/4 v0, 0x0

    .line 24
    aget-object p1, p1, v0

    const/4 v0, 0x0

    .line 28
    :try_start_0
    new-instance v1, Ljava/net/URL;

    invoke-direct {v1, p1}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1}, Ljava/net/URL;->openStream()Ljava/io/InputStream;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 29
    :try_start_1
    invoke-static {p1}, Landroid/graphics/BitmapFactory;->decodeStream(Ljava/io/InputStream;)Landroid/graphics/Bitmap;

    move-result-object v1
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    if-eqz p1, :cond_0

    .line 35
    :try_start_2
    invoke-virtual {p1}, Ljava/io/InputStream;->close()V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0

    :catch_0
    :cond_0
    move-object v0, v1

    goto :goto_1

    :catch_1
    move-exception v1

    goto :goto_0

    :catchall_0
    move-exception p1

    move-object v4, v0

    move-object v0, p1

    move-object p1, v4

    goto :goto_2

    :catch_2
    move-exception v1

    move-object p1, v0

    :goto_0
    :try_start_3
    const-string v2, "goWrap"

    const-string v3, "Error"

    .line 31
    invoke-static {v2, v3, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    if-eqz p1, :cond_1

    .line 35
    :try_start_4
    invoke-virtual {p1}, Ljava/io/InputStream;->close()V
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_3

    :catch_3
    :cond_1
    :goto_1
    return-object v0

    :catchall_1
    move-exception v0

    :goto_2
    if-eqz p1, :cond_2

    :try_start_5
    invoke-virtual {p1}, Ljava/io/InputStream;->close()V
    :try_end_5
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_4

    .line 40
    :catch_4
    :cond_2
    throw v0
.end method

.method protected bridge synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0

    .line 14
    check-cast p1, [Ljava/lang/String;

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/wrapper/DownloadImageTask;->doInBackground([Ljava/lang/String;)Landroid/graphics/Bitmap;

    move-result-object p1

    return-object p1
.end method

.method protected onPostExecute(Landroid/graphics/Bitmap;)V
    .locals 1

    .line 45
    iget-object v0, p0, Lnet/gogame/gowrap/wrapper/DownloadImageTask;->imageView:Landroid/widget/ImageView;

    invoke-virtual {v0, p1}, Landroid/widget/ImageView;->setImageBitmap(Landroid/graphics/Bitmap;)V

    return-void
.end method

.method protected bridge synthetic onPostExecute(Ljava/lang/Object;)V
    .locals 0

    .line 14
    check-cast p1, Landroid/graphics/Bitmap;

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/wrapper/DownloadImageTask;->onPostExecute(Landroid/graphics/Bitmap;)V

    return-void
.end method
