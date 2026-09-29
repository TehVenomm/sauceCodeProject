.class public final Lnet/gogame/gopay/sdk/support/t;
.super Landroid/os/AsyncTask;


# instance fields
.field a:Ljava/lang/String;

.field b:Lnet/gogame/gopay/sdk/support/u;


# direct methods
.method public constructor <init>(Ljava/lang/String;Lnet/gogame/gopay/sdk/support/u;)V
    .locals 0

    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    iput-object p1, p0, Lnet/gogame/gopay/sdk/support/t;->a:Ljava/lang/String;

    iput-object p2, p0, Lnet/gogame/gopay/sdk/support/t;->b:Lnet/gogame/gopay/sdk/support/u;

    return-void
.end method

.method private varargs a([Ljava/lang/String;)Ljava/lang/Boolean;
    .locals 6

    const/4 v0, 0x0

    :try_start_0
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    iget-object v2, p0, Lnet/gogame/gopay/sdk/support/t;->a:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "/data.zip"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    new-instance v2, Ljava/net/URL;

    aget-object p1, p1, v0

    invoke-direct {v2, p1}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    invoke-virtual {v2}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object p1

    invoke-virtual {p1}, Ljava/net/URLConnection;->connect()V

    new-instance p1, Ljava/io/BufferedInputStream;

    invoke-virtual {v2}, Ljava/net/URL;->openStream()Ljava/io/InputStream;

    move-result-object v2

    invoke-direct {p1, v2}, Ljava/io/BufferedInputStream;-><init>(Ljava/io/InputStream;)V

    new-instance v2, Ljava/io/FileOutputStream;

    invoke-direct {v2, v1}, Ljava/io/FileOutputStream;-><init>(Ljava/lang/String;)V

    const/16 v3, 0x400

    new-array v3, v3, [B

    :goto_0
    invoke-virtual {p1, v3}, Ljava/io/InputStream;->read([B)I

    move-result v4

    const/4 v5, -0x1

    if-eq v4, v5, :cond_0

    invoke-virtual {v2, v3, v0, v4}, Ljava/io/OutputStream;->write([BII)V

    goto :goto_0

    :cond_0
    invoke-virtual {v2}, Ljava/io/OutputStream;->flush()V

    invoke-virtual {v2}, Ljava/io/OutputStream;->close()V

    invoke-virtual {p1}, Ljava/io/InputStream;->close()V

    iget-object p1, p0, Lnet/gogame/gopay/sdk/support/t;->a:Ljava/lang/String;

    invoke-static {v1, p1}, Lnet/gogame/gopay/sdk/support/v;->a(Ljava/lang/String;Ljava/lang/String;)V

    new-instance p1, Ljava/io/File;

    invoke-direct {p1, v1}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    invoke-virtual {p1}, Ljava/io/File;->delete()Z

    const/4 p1, 0x1

    invoke-static {p1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    invoke-virtual {p1}, Ljava/lang/Exception;->printStackTrace()V

    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p1

    return-object p1
.end method


# virtual methods
.method protected final synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0

    check-cast p1, [Ljava/lang/String;

    invoke-direct {p0, p1}, Lnet/gogame/gopay/sdk/support/t;->a([Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object p1

    return-object p1
.end method

.method protected final synthetic onPostExecute(Ljava/lang/Object;)V
    .locals 1

    check-cast p1, Ljava/lang/Boolean;

    invoke-super {p0, p1}, Landroid/os/AsyncTask;->onPostExecute(Ljava/lang/Object;)V

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/t;->b:Lnet/gogame/gopay/sdk/support/u;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gopay/sdk/support/t;->b:Lnet/gogame/gopay/sdk/support/u;

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    invoke-interface {v0}, Lnet/gogame/gopay/sdk/support/u;->a()V

    :cond_0
    return-void
.end method

.method protected final onPreExecute()V
    .locals 0

    invoke-super {p0}, Landroid/os/AsyncTask;->onPreExecute()V

    return-void
.end method
